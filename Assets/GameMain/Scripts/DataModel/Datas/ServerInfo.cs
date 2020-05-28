using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dango
{
    public struct ServerInfo:IClear
    {
        public string ip;
        public int port;
        public string checkOutText;
        
        public void Clear()
        {
            ip = string.Empty;
            port = 0;
            checkOutText = string.Empty;
        }
    }
}

