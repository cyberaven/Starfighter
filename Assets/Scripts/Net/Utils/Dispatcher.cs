using System.Collections.Generic;
using System;

namespace Net.Utils
{

    public interface IDispatcher
    {
        void Invoke(Action fn);
    }

    public class Dispatcher : IDispatcher
    {
        public List<Action> pending = new List<Action>();
        private static Dispatcher _instance;

        public void Invoke(Action fn)
        {
            lock (pending)
            {
                pending.Add(fn);
            }
        }

        public void InvokePending()
        {
            lock (pending)
            {
                foreach (var action in pending)
                {
                    action();
                }

                pending.Clear();
            }
        }

        public static Dispatcher Instance
        {
            get
            {
                if (_instance == null)
                {
                    // Instance singleton on first use.
                    _instance = new Dispatcher();
                }

                return _instance;
            }
        }
    }
}