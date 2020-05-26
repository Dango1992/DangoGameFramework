using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using GameFramework.Procedure;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace Dango
{
    public class ProcedureLoading : ProcedureBase
    {
        protected override void OnEnter (ProcedureOwner procedureOwner) {

            base.OnEnter (procedureOwner);
            
            Debug.Log("加载场景");
        }

        public override bool UseNativeDialog { get; }
    }

}