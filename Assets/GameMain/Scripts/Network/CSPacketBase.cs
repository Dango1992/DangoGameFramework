//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using Dango.Network;

namespace Dango
{
    public abstract class CSPacketBase : PacketBase
    {
        public override PacketType PacketType
        {
            get
            {
                return PacketType.ClientToServer;
            }
        }
        
        public override int Id
        {
            get { return (int)MessageDefine(); }
        }
        
        public abstract MsgDefine MessageDefine();
    }
}
