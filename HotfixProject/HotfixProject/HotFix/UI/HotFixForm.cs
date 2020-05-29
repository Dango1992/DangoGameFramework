using System;
using System.Collections.Generic;
using System.Reflection;
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
            InitFieldsAttribute();
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

        private void InitFieldsAttribute()
        {
            Type type = this.GetType();
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
            int len = fields.Length;
            for (int i = 0; i < len; i++)
            {
                object[] objs = fields[i].GetCustomAttributes(typeof(BindComponentAttribute), false);
                if (objs.Length != 0)
                {
                    BindComponentAttribute attri = (BindComponentAttribute)objs[0];
                    Transform transform = this.gameObject.transform.Find(attri.Component);
                    if (transform == null)
                    {
                        Debug.LogError(this.gameObject.name + "类的BindUIComponent(\"" + attri.Component + "\")没有匹配的GameObject");
                        continue;
                    }
                    Component componet = transform.GetComponent(fields[i].FieldType);
                    if (componet == null)
                    {
                        Debug.LogError(this.gameObject.name + "类的BindUIComponent(\"" + attri.Component + "\")没有匹配的" + fields[i].FieldType.ToString() + "组件");
                        continue;
                    }
                    fields[i].SetValue(this, componet);
                }
            }
        }
    }
}


