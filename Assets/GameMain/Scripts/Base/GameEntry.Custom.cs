//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using Dango.Network;
using UnityEngine;

namespace Dango
{
    /// <summary>
    /// 游戏入口。
    /// </summary>
    public partial class GameEntry : MonoBehaviour
    {
        public static BuiltinDataComponent BuiltinData
        {
            get;
            private set;
        }
        
        /// <summary>
        /// 获取数据模型组件。
        /// </summary>
        public static DataModelComponent DataModel
        {
            get;
            private set;
        }

//        
//        /// <summary>
//        /// 获取网络组件。
//        /// </summary>
//        public static NetworkManager Network
//        {
//            get;
//            private set;
//        }

        private static void InitCustomComponents()
        {
            BuiltinData = UnityGameFramework.Runtime.GameEntry.GetComponent<BuiltinDataComponent>();
            DataModel = UnityGameFramework.Runtime.GameEntry.GetComponent<DataModelComponent>();
            //Network = UnityGameFramework.Runtime.GameEntry.GetComponent<NetworkManager>();
        }
    }
}
