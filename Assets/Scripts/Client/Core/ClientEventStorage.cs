using System;

namespace Client.Core
{
    public class ClientEventStorage: IDisposable
    {
        private static ClientEventStorage _instance;
        
        public ClientEvent InitPilot = new ClientEvent();
        public ClientEvent InitNavigator = new ClientEvent();
        
        
        public static ClientEventStorage GetInstance()
        {
            return _instance ?? (_instance = new ClientEventStorage());
        }
        
        public void Dispose()
        {
            InitPilot.RemoveAllListeners();
            InitNavigator.RemoveAllListeners();
        }
    }
}