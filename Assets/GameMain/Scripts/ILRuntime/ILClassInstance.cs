using System.Collections;
using System.Collections.Generic;
using GameFramework;
using ILRuntime.CLR.TypeSystem;
using UnityEngine;

namespace Dango
{
    public class ILClassInstance : IReference
    {
        public object classInstance;
        public IType type;

        public void Clear()
        {
            this.classInstance = null;
            this.type = null;
        }
    }
}