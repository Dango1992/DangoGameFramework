using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using GameFramework.Event;
using UnityEngine;
using Dango.Core;

namespace Dango
{
    public partial class DataModelComponent
    {
        private Dictionary<int,IDataModel> m_modelDictionary = new Dictionary<int, IDataModel>();
        
        /// <summary>
        /// 反射注册所有model
        /// </summary>
        private void DataRegister()
        {
            //模仿channel的反射注册
            Type baseType = typeof(DataModelBase);
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type[] types = assembly.GetTypes();
            for (int i = 0; i < types.Length; i++)
            {
                if (!types[i].IsClass || types[i].IsAbstract)
                {
                    continue;
                }

                if (types[i].BaseType == baseType)
                {
                    DataModelBase modelBase = (DataModelBase) Activator.CreateInstance(types[i]);
                    Register(modelBase.Id,modelBase);
                }
            }
        }
        
        /// <summary>
        /// 注册所有model事件
        /// </summary>
        private void Subscribe()
        {
            foreach (var model in m_modelDictionary)
            {
                model.Value.Subscribe();
            }
        }
        
        /// <summary>
        /// 取消注册所有model事件
        /// </summary>
        private void Unsubscribe()
        {
            foreach (var model in m_modelDictionary)
            {
                model.Value.Unsubscribe();
            }
        }
        
        /// <summary>
        /// 事件通知所有model
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnNotify(object sender, GameEventArgs e)
        {
            foreach (var model in m_modelDictionary)
            {
                model.Value.OnNotify(sender,e);
            }
        }

        /// <summary>
        /// 泛型注册model函数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Register<T>() where T : class, IDataModel,new()
        {
            Register(typeof(T).GetHashCode(),new T());
        }
        
        /// <summary>
        /// 注册model函数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="model"></param>
        public void Register(int key,IDataModel model)
        {
            if (!m_modelDictionary.ContainsKey(key))
            {
                m_modelDictionary.Add(key,model);
            }
        }

        /// <summary>
        /// 泛型取消注册model函数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void UnRegister<T>() where T : class, IDataModel,new()
        {
            UnRegister(typeof(T).GetHashCode());
        }
        
        /// <summary>
        /// 取消注册model函数
        /// </summary>
        /// <param name="key"></param>
        public void UnRegister(int key)
        {
            if (m_modelDictionary.ContainsKey(key))
            {
                m_modelDictionary[key].Unsubscribe();
                m_modelDictionary.Remove(key);
            }
        }

        /// <summary>
        /// 泛型获取model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetModel<T>() where T : class, IDataModel,new ()
        {
            int key = typeof(T).GetHashCode();
            T model = GetModel(key) as T;

            return model;
        }

        /// <summary>
        /// 通过ID获取model
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IDataModel GetModel(int key)
        {
            if (m_modelDictionary.ContainsKey(key))
            {
                return m_modelDictionary[key];
            }

            return null;
        }

        /// <summary>
        /// 清除所有model数据
        /// </summary>
        public void DataClear()
        {
            foreach (var model in m_modelDictionary)
            {
                model.Value.Clear();
            }
        }
            
    }
}
