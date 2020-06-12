using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dango.Other
{
    public abstract class CommandBase <T> : ICommand
    {
        public object buffer { get; protected set; }
        public abstract void Clear();
        public abstract void Do();
        public abstract void Undo();

        public virtual void Fill(object _buffer)
        {
            this.buffer = _buffer;
        }
    }
}
