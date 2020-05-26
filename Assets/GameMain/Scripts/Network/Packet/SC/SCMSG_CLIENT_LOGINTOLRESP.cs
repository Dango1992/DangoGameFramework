using System.Collections;
using System.Collections.Generic;
using Dango.Network;
using UnityEngine;


namespace LS
{
    public sealed partial class MSG_CLIENT_LOGINTOLRESP : SCPacketBase
    {
        public override void Clear()
        {
            
        }
        public override MsgDefine MessageDefine()
        {
            return MsgDefine.LS_MSG_CLIENT_LOGINTOLRESP_ID;
        }
    }
}