using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HotfixProject.HotFix.UI
{
    public class TestForm : HotFixForm
    {
        [BindComponent("Text")]
        private Text text;
        [BindComponent("Button")]
        private Button button;

        public TestForm(GameObject gameObject) : base(gameObject) { }

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
            base.OnInit(userData);
            Debug.Log(gameObject);

            text.text = "成功修改Text";
            button.onClick.AddListener(OnClick);//Todo 这部分代码有问题= =！,主工程无法直接绑定DLL中函数
        }

        protected override void OnOpen(object userData)
        {
            Debug.Log("ui opened!");
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

        private float timer = 0f;
        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            timer += elapseSeconds;
            if (timer >= 3.0f)
            {
                Debug.Log("OnUpdate");
                timer = 0;
            }
        }

        private void OnClick()
        {
            Debug.Log("Hello World!");
        }
    }
}
