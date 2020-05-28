using System;
using System.Collections.Generic;
using UnityEngine;
using Dango;

namespace HotfixProject
{
    public class MyClass
    {
        public static void SayHello()
        {
            Debug.Log("这个是由热更代码实例化的UI!");
            GameEntry.UI.OpenUIForm(UIFormId.LoginForm, null);
        }
    }

    public class MyClass3 : UGuiForm
    {
        
    }
}
