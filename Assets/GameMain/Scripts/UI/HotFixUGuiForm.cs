using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dango.HotFix
{
    public class UIDelegateClass
    {
        public delegate void OnCloseDelegateMethod(bool isShutdown, object userData);
        public delegate void OnCoverDelegateMethod(); 
        public delegate void OnDepthChangedDelegateMethod(int uiGroupDepth, int depthInUIGroup);
        public delegate void OnInitDelegateMethod(object userData); 
        public delegate void OnOpenDelegateMethod(object userData);
        public delegate void OnPauseDelegateMethod(); 
        public delegate void OnRecycleDelegateMethod();
        public delegate void OnRefocusDelegateMethod(object userData); 
        public delegate void OnResumeDelegateMethod();
        public delegate void OnRevealDelegateMethod(); 
        public delegate void OnUpdateDelegateMethod(float elapseSeconds, float realElapseSeconds);
        
        public OnCloseDelegateMethod onCloseMethod;
        public OnCoverDelegateMethod onCoverMethod; 
        public OnDepthChangedDelegateMethod onDepthChangedMethod;
        public OnInitDelegateMethod onInitMethod; 
        public OnOpenDelegateMethod onOpenMethod;
        public OnPauseDelegateMethod onPauseMethod; 
        public OnRecycleDelegateMethod onRecycleMethod;
        public OnRefocusDelegateMethod onRefocusMethod; 
        public OnResumeDelegateMethod onResumeMethod;
        public OnRevealDelegateMethod onRevealMethod; 
        public OnUpdateDelegateMethod onUpdateMethod;
        
        public void OnClose(bool isShutdown, object userData)
        {
            onCloseMethod?.Invoke(isShutdown,userData);
        }

        public void OnCover()
        {
            onCoverMethod?.Invoke();
        }

        public void OnDepthChanged(int uiGroupDepth, int depthInUIGroup)
        {
            onDepthChangedMethod?.Invoke(uiGroupDepth,depthInUIGroup);
        }

        public void OnInit(object userData)
        {
            onInitMethod?.Invoke(userData);
        }

        public void OnOpen(object userData)
        {
            onOpenMethod?.Invoke(userData);
        }

        public void OnPause()
        {
            onPauseMethod?.Invoke();
        }

        public void OnRecycle()
        {
            onRecycleMethod?.Invoke();
        }

        public void OnRefocus(object userData)
        {
            onRefocusMethod?.Invoke(userData);
        }

        public void OnResume()
        {
            onResumeMethod?.Invoke();
        }

        public void OnReveal()
        {
            onRevealMethod?.Invoke();
        }

        public void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            onUpdateMethod?.Invoke(elapseSeconds,realElapseSeconds);
        }
        
    }

    public class HotFixUGuiForm : UGuiForm
    {
        [SerializeField]
        private string typeFullName;

        private object classInstance;
        
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
            classInstance = GameEntry.ILRuntime.Instantiate(this.typeFullName, this.gameObject);
            
            GameEntry.ILRuntime.Execute(classInstance,this.typeFullName,"OnInit",1,userData);
        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            
            GameEntry.ILRuntime.Execute(classInstance,this.typeFullName,"OnOpen",1,userData);
        }

        protected override void OnPause()
        {
            base.OnPause();
        }

        protected override void OnRecycle()
        {
            base.OnRecycle();
        }

        protected override void OnRefocus(object userData)
        {
            base.OnRefocus(userData);
        }

        protected override void OnResume()
        {
            base.OnResume();
        }

        protected override void OnReveal()
        {
            base.OnReveal();
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds,realElapseSeconds);
        }
    }
}