using System;
using System.Collections;
using System.Collections.Generic;
using GameFramework;
using ILRuntime.CLR.TypeSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Dango.HotFix
{
    public class HotFixUGuiForm : UGuiForm
    {
        [SerializeField]
        private string typeFullName;

        private ILClassInstance classInstance;
        
        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown,userData);
        }

        protected override void OnCover()
        {
            base.OnCover();
        }

        protected override void OnDepthChanged(int uiGroupDepth, int depthInUIGroup)
        {
            base.OnDepthChanged(uiGroupDepth,depthInUIGroup);
        }

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            
            classInstance = GameEntry.ILRuntime.CreateInstance(this.typeFullName, this.gameObject);
            GameEntry.ILRuntime.ExecuteMethod(classInstance,"OnInit",1,userData);
        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            
            GameEntry.ILRuntime.ExecuteMethod(classInstance,"OnOpen",1,userData);
        }

        protected override void OnPause()
        {
            base.OnPause();
            
            GameEntry.ILRuntime.ExecuteMethod(classInstance,"OnPause",0);
        }

        protected override void OnRecycle()
        {
            base.OnRecycle();
            
            GameEntry.ILRuntime.ExecuteMethod(classInstance,"OnRecycle",0);
        }

        protected override void OnRefocus(object userData)
        {
            base.OnRefocus(userData);
            
            GameEntry.ILRuntime.ExecuteMethod(classInstance,"OnRefocus",1,userData);
        }

        protected override void OnResume()
        {
            base.OnResume();
            
            GameEntry.ILRuntime.ExecuteMethod(classInstance,"OnResume",0);
        }

        protected override void OnReveal()
        {
            base.OnReveal();
            
            GameEntry.ILRuntime.ExecuteMethod(classInstance,"OnReveal",0);
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds,realElapseSeconds);
            
            GameEntry.ILRuntime.ExecuteMethod(classInstance,"OnUpdate",2,elapseSeconds,realElapseSeconds);
        }

        protected void OnDestroy()
        {
            if (classInstance != null)
            {
                ReferencePool.Release(classInstance);
            }
        }
    }
}