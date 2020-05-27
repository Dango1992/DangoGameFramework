using System.Collections;
using System.Collections.Generic;
using Dango.Core;
using GameFramework.Event;
using UnityEngine;

namespace Dango
{
    public abstract class DataModelBase : IDataModel
    {
        public abstract int Id { get; }
        public abstract void OnNotify(object sender, GameEventArgs e);
        public abstract void Clear();
        public abstract void Subscribe();
        public abstract void Unsubscribe();
    }
}

