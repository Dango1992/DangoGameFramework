using System.Collections;
using System.Collections.Generic;
using GameFramework.Event;
using UnityEngine;
using Dango.Core;

namespace Dango
{
    public partial class DataModelComponent
    {
        private List<IDataModel> _list = new List<IDataModel>();
        private void Subscribe()
        {
            foreach (var o in _list)
            {
                o.Subscribe();
            }
        }
        
        private void Unsubscribe()
        {
            foreach (var o in _list)
            {
                o.Unsubscribe();
            }
        }

        private void Register<T>() where T : class, IDataModel,new()
        {
            _list.Add(UnitSingleton<T>.Instance);
        }
        
        private void UnRegister<T>() where T : class, IDataModel,new()
        {
            _list.Remove(UnitSingleton<T>.Instance);
        }
//        
//        private void OnNotify(object sender, GameEventArgs e)
//        {
//            foreach (var o in _list)
//            {
//                o.Notify(sender,e);
//            }
//        }
//        
        public T Singleton<T>() where T : class, IDataModel,new ()
        {
            return UnitSingleton<T>.Instance;
        }

        public void DataClear()
        {
            foreach (var o in _list)
            {
                o.Clear();
            }
        }
            
    }
}
