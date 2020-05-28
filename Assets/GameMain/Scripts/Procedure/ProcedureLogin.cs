using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using GameFramework.Event;
using GameFramework.Procedure;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace Dango
{
    public class ProcedureLogin : ProcedureBase
    {
        public override bool UseNativeDialog { get; }
        
        protected override void OnEnter (ProcedureOwner procedureOwner) {

            base.OnEnter (procedureOwner);
            
            //GameEntry.UI.OpenUIForm(UIFormId.LoginForm, this);
            GameEntry.ILRuntime.Func("HotfixProject.MyClass", "SayHello", null, null);
        }
    }
}

