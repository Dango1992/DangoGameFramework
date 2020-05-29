using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using GameFramework;
using ILRuntime.CLR.Method;
using ILRuntime.CLR.TypeSystem;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Dango
{
    /// <summary>
    /// 基础组件。
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Framework/ILRuntime")]
    public class ILRuntimeComponent : GameFrameworkComponent
    {
        ILRuntime.Runtime.Enviorment.AppDomain appdomain;

        System.IO.MemoryStream fs;
        System.IO.MemoryStream p;

        private void Start()
        {
            StartCoroutine(LoadILRuntime());
        }

        private void OnDestroy()
        {
            if (fs != null)
                fs.Close();
            if (p != null)
                p.Close();
            fs = null;
            p = null;
        }

        IEnumerator LoadILRuntime()
        {
            appdomain = new ILRuntime.Runtime.Enviorment.AppDomain();
#if UNITY_ANDROID
    WWW www = new WWW(Application.streamingAssetsPath + "/HotfixProject.dll");
#else
            WWW www = new WWW("file:///" + Application.streamingAssetsPath + "/HotfixProject.dll");
#endif
            while (!www.isDone)
                yield return null;
            if (!string.IsNullOrEmpty(www.error))
                Debug.LogError(www.error);
            byte[] dll = www.bytes;
            www.Dispose();
#if UNITY_ANDROID
    www = new WWW(Application.streamingAssetsPath + "/HotfixProject.pdb");
#else
            www = new WWW("file:///" + Application.streamingAssetsPath + "/HotfixProject.pdb");
#endif
            while (!www.isDone)
                yield return null;
            if (!string.IsNullOrEmpty(www.error))
                Debug.LogError(www.error);
            byte[] pdb = www.bytes;
            fs = new MemoryStream(dll);
            p = new MemoryStream(pdb);

            appdomain.LoadAssembly(fs, p, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
        }

        public void Invoke(string type, string method, object instance, params object[] p)
        {
            appdomain?.Invoke(type, method, p);
        }
        
        public ILClassInstance CreateInstance(string typeFullName, params object[] args)
        {
            ILClassInstance ilInstance = null;
            object instance = appdomain.Instantiate(typeFullName, args);

            if (instance != null)
            {
                ilInstance = ReferencePool.Acquire<ILClassInstance>();

                ilInstance.classInstance = instance;
                ilInstance.type = appdomain.LoadedTypes[typeFullName];
            }

            return ilInstance;
        }

        public void ExecuteMethod(ILClassInstance classInstance, string methodName, int paramCount,
            params object[] paras)
        {
            if (classInstance.classInstance == null || classInstance.type == null)
            {
                return;
            }
            
            IMethod method = classInstance.type.GetMethod(methodName, paramCount);
            appdomain.Invoke(method, classInstance.classInstance, paras);
        }
    }
}