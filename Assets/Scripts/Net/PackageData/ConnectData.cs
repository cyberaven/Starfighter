using System;
using Core;

namespace Net.PackageData
{
    [Serializable]
    public class ConnectData
    {
        public string login;
        public string password;
        public UserType accountType;
        public string multicastGroupIp = "";
        public int portToSend = 0;
        public int portToReceive = 0;
    }
}
