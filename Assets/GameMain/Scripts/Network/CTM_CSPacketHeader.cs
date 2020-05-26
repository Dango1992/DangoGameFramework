using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dango.Network
{
    public class CTM_CSPacketHeader : CSPacketHeader
    {
        public byte HasNext
        {
            get;
            set;
        }

        public override PacketType PacketType {
            get {
                return PacketType.ClientToServer;
            }
        }
    }
}

