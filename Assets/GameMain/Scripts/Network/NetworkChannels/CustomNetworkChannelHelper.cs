﻿using GameFramework;
using GameFramework.Event;
using GameFramework.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Dango.Network
{
    public class CustomNetworkChannelHelper : INetworkChannelHelper
    {
        private readonly Dictionary<int, Type> m_ServerToClientPacketTypes = new Dictionary<int, Type>();
        private readonly MemoryStream m_CachedStream = new MemoryStream(1024 * 8);

        private readonly List<byte[]> byteses = new List<byte[]>()
            {new byte[PacketDefine.PacketSizeLength], new byte[2], new byte[1]};

        private INetworkChannel m_NetworkChannel = null;

        /// <summary>
        /// unsigned short	usSize;
        /// unsigned short	usType;
        /// bool bHasNext;
        /// </summary>
        public int PacketHeaderLength
        {
            get { return PacketDefine.PacketSizeLength + 3; }
        }

        /// <summary>
        /// 初始化网络频道辅助器。
        /// </summary>
        /// <param name="networkChannel">网络频道。</param>
        public void Initialize(INetworkChannel networkChannel)
        {
            m_NetworkChannel = networkChannel;

            // 反射注册包和包处理函数。
            Type packetBaseType = typeof(SCPacketBase);
            Type packetHandlerBaseType = typeof(CTM_PacketHandler);//修改为自定义Handler
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type[] types = assembly.GetTypes();
            for (int i = 0; i < types.Length; i++)
            {
                if (!types[i].IsClass || types[i].IsAbstract)
                {
                    continue;
                }

                if (types[i].BaseType == packetBaseType)
                {
                    PacketBase packetBase = (PacketBase) Activator.CreateInstance(types[i]);
                    Type packetType = GetServerToClientPacketType(packetBase.Id);
                    if (packetType != null)
                    {
                        Log.Warning("Already exist packet type '{0}', check '{1}' or '{2}'?.", packetBase.Id.ToString(),
                            packetType.Name, packetBase.GetType().Name);
                        continue;
                    }

                    m_ServerToClientPacketTypes.Add(packetBase.Id, types[i]);
                }
                else if (types[i].BaseType == packetHandlerBaseType)
                {
                    IPacketHandler packetHandler = (IPacketHandler) Activator.CreateInstance(types[i]);
                    m_NetworkChannel.RegisterHandler(packetHandler);
                }
            }

            GameEntry.Event.Subscribe(UnityGameFramework.Runtime.NetworkConnectedEventArgs.EventId, OnNetworkConnected);
            GameEntry.Event.Subscribe(UnityGameFramework.Runtime.NetworkClosedEventArgs.EventId, OnNetworkClosed);
            GameEntry.Event.Subscribe(UnityGameFramework.Runtime.NetworkMissHeartBeatEventArgs.EventId,
                OnNetworkMissHeartBeat);
            GameEntry.Event.Subscribe(UnityGameFramework.Runtime.NetworkErrorEventArgs.EventId, OnNetworkError);
            GameEntry.Event.Subscribe(UnityGameFramework.Runtime.NetworkCustomErrorEventArgs.EventId,
                OnNetworkCustomError);
        }

        /// <summary>
        /// 关闭并清理网络频道辅助器。
        /// </summary>
        public void Shutdown()
        {
            GameEntry.Event.Unsubscribe(UnityGameFramework.Runtime.NetworkConnectedEventArgs.EventId,
                OnNetworkConnected);
            GameEntry.Event.Unsubscribe(UnityGameFramework.Runtime.NetworkClosedEventArgs.EventId, OnNetworkClosed);
            GameEntry.Event.Unsubscribe(UnityGameFramework.Runtime.NetworkMissHeartBeatEventArgs.EventId,
                OnNetworkMissHeartBeat);
            GameEntry.Event.Unsubscribe(UnityGameFramework.Runtime.NetworkErrorEventArgs.EventId, OnNetworkError);
            GameEntry.Event.Unsubscribe(UnityGameFramework.Runtime.NetworkCustomErrorEventArgs.EventId,
                OnNetworkCustomError);

            m_NetworkChannel = null;
        }

        /// <summary>
        /// 准备进行连接。
        /// </summary>
        public void PrepareForConnecting()
        {
            m_NetworkChannel.Socket.ReceiveBufferSize = 1024 * 64;
            m_NetworkChannel.Socket.SendBufferSize = 1024 * 64;
        }

        /// <summary>
        /// 发送心跳消息包。
        /// </summary>
        /// <returns>是否发送心跳消息包成功。</returns>
        public bool SendHeartBeat()
        {
            //m_NetworkChannel.Send(ReferencePool.Acquire<CSHeartBeat>());
            return true;
        }

        /// <summary>
        /// 序列化消息包。
        /// </summary>
        /// <typeparam name="T">消息包类型。</typeparam>
        /// <param name="packet">要序列化的消息包。</param>
        /// <param name="destination">要序列化的目标流。</param>
        /// <returns>是否序列化成功。</returns>
        public bool Serialize<T>(T packet, Stream destination) where T : Packet
        {
            //        struct MsgHeadEx
            //        {
            //            ushort usSize;
            //            ushort usType;
            //            bool bHasNext;
            //        };
            PacketBase packetImpl = packet as PacketBase;
            if (packetImpl == null)
            {
                Log.Warning("Packet is invalid.");
                return false;
            }

            if (packetImpl.PacketType != PacketType.ClientToServer)
            {
                Log.Warning("Send packet invalid.");
                return false;
            }
            
            m_CachedStream.Seek(PacketHeaderLength, SeekOrigin.Begin);
            m_CachedStream.SetLength(PacketHeaderLength);
            ProtobufHelper.ToStream(packet, m_CachedStream);

            CTM_CSPacketHeader packetHeader = ReferencePool.Acquire<CTM_CSPacketHeader>();
            packetHeader.PacketLength = (int)m_CachedStream.Length;
            packetHeader.Id = (ushort)packet.Id;
            packetHeader.HasNext = 0;

            m_CachedStream.Position = 0;
            this.byteses[0].WriteTo(0, (ushort) packetHeader.PacketLength);
            this.byteses[1].WriteTo(0, packetHeader.Id);
            this.byteses[2][0] = packetHeader.HasNext;

            int index = 0;
            foreach (var bytes in this.byteses)
            {
                Array.Copy(bytes, 0, m_CachedStream.GetBuffer(), index, bytes.Length);
                index += bytes.Length;
            }
            
            ReferencePool.Release(packetHeader);
            ReferencePool.Release(packet);
            m_CachedStream.WriteTo(destination);
            
            return true;
        }

        /// <summary>
        /// 反序列消息包头。
        /// </summary>
        /// <param name="source">要反序列化的来源流。</param>
        /// <param name="customErrorData">用户自定义错误数据。</param>
        /// <returns></returns>
        public IPacketHeader DeserializePacketHeader(Stream source, out object customErrorData)
        {
            // 注意：此函数并不在主线程调用！
            customErrorData = null;

            CTM_SCPacketHeader scHeader = ReferencePool.Acquire<CTM_SCPacketHeader>();
            MemoryStream memoryStream = source as MemoryStream;
            
            if (memoryStream != null)
            {
                ushort packetSize = BitConverter.ToUInt16(memoryStream.GetBuffer(), 0);
                ushort msgType = BitConverter.ToUInt16(memoryStream.GetBuffer(), PacketDefine.MsgTypeIndex);
                byte hasNext = memoryStream.GetBuffer()[PacketDefine.HasNextIndex];

                scHeader.PacketLength = packetSize - PacketDefine.MessageIdentifyLength;
                scHeader.Id = msgType;
                scHeader.HasNext = hasNext;
                
                return scHeader;
            }

            return null;
        }

        /// <summary>
        /// 反序列化消息包。
        /// </summary>
        /// <param name="packetHeader">消息包头。</param>
        /// <param name="source">要反序列化的来源流。</param>
        /// <param name="customErrorData">用户自定义错误数据。</param>
        /// <returns>反序列化后的消息包。</returns>
        public Packet DeserializePacket(IPacketHeader packetHeader, Stream source, out object customErrorData)
        {
            customErrorData = null;
            
            CTM_SCPacketHeader header = packetHeader as CTM_SCPacketHeader;
            if (header == null)
            {
                Log.Warning("Packet header is invalid.");
                return null;
            }

            Packet packet = null;
            if (header.IsValid)
            {
                Type packetType = GetServerToClientPacketType(header.Id);

                if (packetType != null && source is MemoryStream)
                {
                    packet = (Packet) ProtobufHelper.FromStream(ReferencePool.Acquire(packetType), (MemoryStream) source);
                }
                else
                {
                    Log.Warning("Can not deserialize packet for packet id '{0}'.", header.Id.ToString());
                }
            }
            else
            {
                Log.Warning("Packet header is invalid.");
            }
            
            ReferencePool.Release(header);
            return packet;
        }

        private Type GetServerToClientPacketType(int id)
        {
            Type type = null;
            if (m_ServerToClientPacketTypes.TryGetValue(id, out type))
            {
                return type;
            }

            return null;
        }

        private void OnNetworkConnected(object sender, GameEventArgs e)
        {
            UnityGameFramework.Runtime.NetworkConnectedEventArgs ne =
                (UnityGameFramework.Runtime.NetworkConnectedEventArgs) e;
            if (ne.NetworkChannel != m_NetworkChannel)
            {
                return;
            }

            Log.Info("Network channel '{0}' connected, local address '{1}', remote address '{2}'.",
                ne.NetworkChannel.Name, ne.NetworkChannel.Socket.LocalEndPoint.ToString(),
                ne.NetworkChannel.Socket.RemoteEndPoint.ToString());
        }

        private void OnNetworkClosed(object sender, GameEventArgs e)
        {
            UnityGameFramework.Runtime.NetworkClosedEventArgs
                ne = (UnityGameFramework.Runtime.NetworkClosedEventArgs) e;
            if (ne.NetworkChannel != m_NetworkChannel)
            {
                return;
            }

            Log.Info("Network channel '{0}' closed.", ne.NetworkChannel.Name);
        }

        private void OnNetworkMissHeartBeat(object sender, GameEventArgs e)
        {
            UnityGameFramework.Runtime.NetworkMissHeartBeatEventArgs ne =
                (UnityGameFramework.Runtime.NetworkMissHeartBeatEventArgs) e;
            if (ne.NetworkChannel != m_NetworkChannel)
            {
                return;
            }

            Log.Info("Network channel '{0}' miss heart beat '{1}' times.", ne.NetworkChannel.Name,
                ne.MissCount.ToString());

            if (ne.MissCount < 2)
            {
                return;
            }

            ne.NetworkChannel.Close();
        }

        private void OnNetworkError(object sender, GameEventArgs e)
        {
            UnityGameFramework.Runtime.NetworkErrorEventArgs ne = (UnityGameFramework.Runtime.NetworkErrorEventArgs) e;
            
            Debug.Log(e.Id);
            if (ne.NetworkChannel != m_NetworkChannel)
            {
                return;
            }

            Log.Info("Network channel '{0}' error, error code is '{1}', error message is '{2}'.",
                ne.NetworkChannel.Name, ne.ErrorCode.ToString(), ne.ErrorMessage);

            ne.NetworkChannel.Close();
        }

        private void OnNetworkCustomError(object sender, GameEventArgs e)
        {
            UnityGameFramework.Runtime.NetworkCustomErrorEventArgs ne =
                (UnityGameFramework.Runtime.NetworkCustomErrorEventArgs) e;
            
            if (ne.NetworkChannel != m_NetworkChannel)
            {
                return;
            }
        }
    }
}