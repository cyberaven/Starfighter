using System;

namespace Core
{
    public class CoreEventStorage: IDisposable
    {
        private static CoreEventStorage _instance;

        public AxisValueEvent axisValueChanged = new AxisValueEvent();
        public KeyCodeEvent actionKeyPressed = new KeyCodeEvent(); 
        
        public static CoreEventStorage GetInstance()
        {
            return _instance ?? (_instance = new CoreEventStorage());
        }
        
        public void Dispose()
        {
            
        }
    }
}