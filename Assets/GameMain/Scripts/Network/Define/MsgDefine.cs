using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dango.Network
{
    public enum MsgDefine
    {
        Unefined = 0,
        Client_MSG_CLIENT_LOGINTOL_ID = 502,
        MSG_CLIENT_LOGINTOG_ID=503,
        LS_MSG_LOGINSERVER_VALIDATE_ID = 504,
        MSG_GAMESERVER_LOGINRESP_ID = 505,
        DBS_MSG_DBSERVER_ROLEINFO_ID = 507,
        LS_MSG_CLIENT_LOGINTOLRESP_ID = 508,
        Client_MSG_CLIENT_WEAKNET_LOGINTOG_ID = 509,
    }
}
