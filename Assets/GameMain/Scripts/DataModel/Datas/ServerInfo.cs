using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dango
{
    public struct ServerInfo:IClear
    {
        public string ip;
        public int prot;
        public string checkOutText;
        
        public void Clear()
        {
            ip = string.Empty;
            prot = 0;
            checkOutText = string.Empty;
        }
    }
}

