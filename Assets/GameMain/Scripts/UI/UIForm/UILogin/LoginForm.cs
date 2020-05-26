using System.Collections;
using System.Collections.Generic;
using System.Net;
using Client;
using GameFramework;
using GameFramework.Event;
using GameFramework.Network;
using Dango.Network;
using UnityEngine;

namespace Dango
{
    public class LoginForm : UGuiForm
    {
        protected override void OnOpen(object userData)
        {
            GameEntry.Event.Subscribe(UnityGameFramework.Runtime.NetworkConnectedEventArgs.EventId, OnNetworkConnected);
            base.OnOpen(userData);
            
            if (m_CustomNetworkChannelHelper != null) {
                m_CustomNetworkChannelHelper.Shutdown();
            }
            if (m_NetworkChannel != null) {
                m_NetworkChannel.Close();
            }
            m_CustomNetworkChannelHelper = new CustomNetworkChannelHelper();

            m_NetworkChannel = GameEntry.Network.CreateNetworkChannel("test",
                GameFramework.Network.ServiceType.TcpWithSyncReceive, m_CustomNetworkChannelHelper);
            m_CustomNetworkChannelHelper.Initialize(m_NetworkChannel);
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
        }

        private void OnDealWithNetworkMsg(object sender, GameEventArgs e)
        {
        }
        
        CustomNetworkChannelHelper m_CustomNetworkChannelHelper;
        GameFramework.Network.INetworkChannel m_NetworkChannel;

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

            //获取DataModel
            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("连接");
                
                m_NetworkChannel.Connect(IPAddress.Parse("10.0.3.230"), 5999);
            }

            if (Input.GetKeyDown(KeyCode.B))
            { 
                Debug.Log("登录");
                MSG_CLIENT_LOGINTOL loginToLMessage = new MSG_CLIENT_LOGINTOL();

               m_NetworkChannel.Send(loginToLMessage);
            }
            
            if (Input.GetKeyDown(KeyCode.C))
            {
                Debug.Log("获取用户id");
                Debug.Log(GameEntry.DataModel.Singleton<TestDataModel>().GetUser()); 
            }
        }
        
        private void OnNetworkConnected(object sender, GameEventArgs e) {
            UnityGameFramework.Runtime.NetworkConnectedEventArgs ne = (UnityGameFramework.Runtime.NetworkConnectedEventArgs)e;
            
            Debug.Log("链接成功");
        }
    }
}