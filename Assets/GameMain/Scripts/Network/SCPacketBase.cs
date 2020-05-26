//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace Dango.Network
{
    public abstract class SCPacketBase : PacketBase
    {
        public override PacketType PacketType
        {
            get
            {
                return PacketType.ServerToClient;
            }
        }
        
        public override int Id
        {
            get { return (int)MessageDefine(); }
        }
        
        public abstract MsgDefine MessageDefine();
    }
}
