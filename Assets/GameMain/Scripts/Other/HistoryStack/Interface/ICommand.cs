using System.Collections;
using System.Collections.Generic;
using GameFramework;
using UnityEngine;

namespace Dango.Other
{
    public interface ICommand : IReference
    {
        void Do();
        void Undo();
    }
}


