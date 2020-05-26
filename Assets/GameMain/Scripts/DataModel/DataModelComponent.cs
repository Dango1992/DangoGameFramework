using System;
using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;
using Dango.Core;
using Dango.Network;
using UnityEngine;
using UnityGameFramework.Runtime;


namespace Dango
{
    /// <summary>
    /// 基础组件。
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Framework/DataModel")]
    public partial class DataModelComponent : GameFrameworkComponent
    {
        
        private void Start()
        {
            DataRegister();
            Subscribe();
        }
        
        private void OnDestroy()
        {
            Unsubscribe();
            DataClear();
        }
        
        private void DataRegister()
        {
            Register<TestDataModel>();
        }
    }
}
