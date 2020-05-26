using System.Collections;
using System.Collections.Generic;
using GameFramework.Network;
using UnityEngine;

namespace Dango.Network
{
    public abstract class CTM_PacketHandler : PacketHandlerBase
    {
        public override int Id
        {
            get { return (int)MessageDefine(); }
        }
        
        public abstract MsgDefine MessageDefine();
    }
}

