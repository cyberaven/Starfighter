using System;

namespace Client.Core
{
    public class ClientEventStorage: IDisposable
    {
        private static ClientEventStorage _instance;
        
        public PlayerScriptEvent InitPilot = new PlayerScriptEvent();
        public PlayerScriptEvent InitNavigator = new PlayerScriptEvent();
        public PlayerScriptEvent InitSpectator = new PlayerScriptEvent();
        public PlayerScriptEvent InitAdmin = new PlayerScriptEvent();
        public PlayerScriptEvent InitModerator = new PlayerScriptEvent();
        
        
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