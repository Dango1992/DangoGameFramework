using System;
using System.Collections.Generic;
using UnityEngine;

namespace HotfixProject.HotFix.UI
{
    public class HotFixForm : IHotFixForm
    {
        public HotFixForm(GameObject gameObject) : base(gameObject) { }

        protected override void OnClose(bool isShutdown, object userData)
        {

        }

        protected override void OnCover()
        {

        }

        protected override void OnDepthChanged(int uiGroupDepth, int depthInUIGroup)
        {

        }

        protected override void OnInit(object userData)
        {
            
        }

        protected override void OnOpen(object userData)
        {

        }

        protected override void OnPause()
        {

        }

        protected override void OnRecycle()
        {

        }

        protected override void OnRefocus(object userData)
        {

        }

        protected override void OnResume()
        {

        }

        protected override void OnReveal()
        {

        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {

        }
    }
}


