using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dango.HotFix
{
    public class HotFixUGuiForm : UGuiForm
    {
        
        public override void Close()
        {
            Close(false);
        }

        public override void Close(bool ignoreFade)
        {
            base.Close(ignoreFade);
        }

        public override void PlayUISound(int uiSoundId)
        {
            base.PlayUISound(uiSoundId);
        }
        
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
        }
        
        protected override void OnRecycle()
        {
            base.OnRecycle();
        }
        
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
        }
        
        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
        }

        protected override void OnPause()
        {
            base.OnPause();
        }
        
        protected override void OnResume()
        {
            base.OnResume();
        }
        
        protected override void OnCover()
        {
            base.OnCover();
        }
        
        protected override void OnReveal()
        {
            base.OnReveal();
        }
        
        protected override void OnRefocus(object userData)
        {
            base.OnRefocus(userData);
        }
        
        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
        }

        protected override void OnDepthChanged(int uiGroupDepth, int depthInUIGroup)
        {
            base.OnDepthChanged(uiGroupDepth, depthInUIGroup);
        }
    }
}

