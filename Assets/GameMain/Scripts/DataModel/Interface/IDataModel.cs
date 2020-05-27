using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dango.Core
{
    public interface IDataModel : INotify
    {
        int Id { get; }
        void Clear();
        void Subscribe();
        void Unsubscribe();
    }
}

