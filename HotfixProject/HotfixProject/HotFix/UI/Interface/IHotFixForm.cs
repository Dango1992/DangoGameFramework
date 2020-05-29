using System;
using System.Collections.Generic;
using UnityEngine;

namespace HotfixProject.HotFix.UI
{
    public abstract class IHotFixForm
    {
        protected GameObject gameObject;

        public IHotFixForm(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }

        protected abstract void OnInit(object userData);

        protected abstract void OnRecycle();

        protected abstract void OnOpen(object userData);

        protected abstract void OnClose(bool isShutdown, object userData);

        protected abstract void OnPause();

        protected abstract void OnResume();

        protected abstract void OnCover();

        protected abstract void OnReveal();

        protected abstract void OnRefocus(object userData);

        protected abstract void OnUpdate(float elapseSeconds, float realElapseSeconds);

        protected abstract void OnDepthChanged(int uiGroupDepth, int depthInUIGroup);
    }
}
