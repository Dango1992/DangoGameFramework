//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using ProtoBuf;
using System;

namespace Dango.Network
{
    [Serializable, ProtoContract(Name = @"SCHeartBeat")]
    public class SCHeartBeat : SCPacketBase
    {
        public SCHeartBeat()
        {
        }

        public override int Id
        {
            get
            {
                return 2;
            }
        }

        public override MsgDefine MessageDefine()
        {
            return MsgDefine.Unefined;
        }

        public override void Clear()
        {
        }
    }
}
