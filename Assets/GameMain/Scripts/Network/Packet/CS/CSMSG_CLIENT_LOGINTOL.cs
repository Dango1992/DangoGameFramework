using System.Collections;
using System.Collections.Generic;
using Dango;
using Dango.Network;
using UnityEngine;

namespace Client
{
    public sealed partial class MSG_CLIENT_LOGINTOL : CSPacketBase
    {
        public override void Clear()
        {
            UserId = 0;
            
        }
        public override MsgDefine MessageDefine()
        {
            return MsgDefine.Client_MSG_CLIENT_LOGINTOL_ID;
        }
    }
}