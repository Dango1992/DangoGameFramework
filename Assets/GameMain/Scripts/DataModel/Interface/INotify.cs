using System.Collections;
using System.Collections.Generic;
using GameFramework.Event;
using UnityEngine;

namespace Dango.Core
{
    public interface INotify
    {
        void OnNotify(object sender, GameEventArgs e);
    }
}
