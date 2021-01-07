using System;
using Net.Utils;

namespace Net.PackageData
{
    [Serializable]
    public class ConnectData
    {
        public string login;
        public string password;
        public AccountType accountType;
        public string multicastGroupIp = "";
        public int portToSend = 0;
        public int portToReceive = 0;
    }
}
