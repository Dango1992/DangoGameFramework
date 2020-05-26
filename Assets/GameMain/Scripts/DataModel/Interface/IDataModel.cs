using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dango.Core
{
    public interface IDataModel : INotify
    {
        void Clear();
        void Subscribe();
        void Unsubscribe();
    }
}

