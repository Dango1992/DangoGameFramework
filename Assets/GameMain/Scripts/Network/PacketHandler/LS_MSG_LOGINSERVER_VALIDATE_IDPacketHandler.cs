using System.Collections;
using System.Collections.Generic;
using GameFramework.Network;
using Dango.Network;
using GameFramework;
using LS;
using UnityEngine;

namespace Dango
{
    public class LS_MSG_LOGINSERVER_VALIDATE_IDPacketHandler : CTM_PacketHandler
    {
        public override void Handle(object sender, Packet packet)
        {
            TestDataEventArgs eventArgs = ReferencePool.Acquire<TestDataEventArgs>();
            var msg = packet as MSG_LOGINSERVER_VALIDATE;
            
            eventArgs.Fill(new ServerInfo(){ ip = msg.Ip,prot = msg.Port,checkOutText = msg.CheckOutText.ToStringUtf8()});
            GameEntry.Event.Fire(this,eventArgs);
        }

        public override MsgDefine MessageDefine()
        {
            return MsgDefine.LS_MSG_LOGINSERVER_VALIDATE_ID;
        }
    }

}

