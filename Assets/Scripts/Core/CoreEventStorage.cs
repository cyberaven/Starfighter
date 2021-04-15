using System;

namespace Core
{
    public class CoreEventStorage: IDisposable
    {
        private static CoreEventStorage _instance;

        public AxisValueEvent AxisValueChanged = new AxisValueEvent();
        
        public static CoreEventStorage GetInstance()
        {
            return _instance ?? (_instance = new CoreEventStorage());
        }
        
        public void Dispose()
        {
            
        }
    }
}