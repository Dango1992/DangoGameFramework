using System.Collections;
using System.Collections.Generic;
using Dango.Network;
using UnityEngine;

namespace LS
{
    public sealed partial class MSG_LOGINSERVER_VALIDATE : SCPacketBase
    {
        public override void Clear()
        {
            
        }

        public override MsgDefine MessageDefine()
        {
            return MsgDefine.LS_MSG_LOGINSERVER_VALIDATE_ID;
        }
    }
}