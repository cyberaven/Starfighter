using System;
using Core;

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
        public EventDataEvent SetPointEvent = new EventDataEvent();
        public CoreEvent DockingAvailable = new CoreEvent(); //green
        public CoreEvent DockableUnitsInRange = new CoreEvent(); //yellow
        public CoreEvent IsDocked = new CoreEvent(); //blue
        public CoreEvent NoOneToDock = new CoreEvent(); //clear

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