using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dango.Network
{
    public static class PacketDefine {
        public static readonly int PacketSizeLength = 2;
        public static readonly int MsgTypeIndex = 2;
        public static readonly int HasNextIndex = 4;
        public static readonly int MessageIdentifyLength = 5;
    }
}



