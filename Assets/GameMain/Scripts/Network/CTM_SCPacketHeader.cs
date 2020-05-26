using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dango.Network
{
    
    public class CTM_SCPacketHeader :PacketHeaderBase
    {
        public override PacketType PacketType
        {
            get
            {
                return PacketType.ServerToClient;
            }
        }
        
        public byte HasNext
        {
            get;
            set;
        }
    }

}

