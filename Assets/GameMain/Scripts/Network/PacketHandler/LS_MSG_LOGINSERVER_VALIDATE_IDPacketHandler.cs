using System.Collections;
using System.Collections.Generic;
using GameFramework.Network;
using Dango.Network;
using UnityEngine;

namespace Dango
{
    public class LS_MSG_LOGINSERVER_VALIDATE_IDPacketHandler : CTM_PacketHandler
    {
        public override void Handle(object sender, Packet packet)
        {
            Debug.Log("Hi");
        }

        public override MsgDefine MessageDefine()
        {
            return MsgDefine.LS_MSG_LOGINSERVER_VALIDATE_ID;
        }
    }

}

