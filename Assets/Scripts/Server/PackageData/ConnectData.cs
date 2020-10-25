using UnityEngine;
using System.Collections.Generic;
using System;
using Server.Utils.Enums;

namespace Server.PackageData
{
    [Serializable]
    public class ConnectData
    {
        //TODO: Connection data
        public string Login;
        public string Password;
        public AccountType AccountType;
    }
}
