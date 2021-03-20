using System.Collections.Generic;
using System;

namespace Net.Utils
{

    public interface IDispatcher
    {
        Guid Invoke(Action fn);
    }

    public class Dispatcher : IDispatcher
    {
        public Dictionary<Guid, Action> pending = new Dictionary<Guid, Action>();
        private static Dispatcher _instance;

        public bool IsInPending(Guid guid)
        {
            lock (pending)
            {
                return pending.ContainsKey(guid);
            }
        }
        
        public Guid Invoke(Action fn)
        {
            lock (pending)
            {
                var guid = Guid.NewGuid();
                pending.Add(guid, fn);
                return guid;
            }
        }

        public void InvokePending()
        {
            lock (pending)
            {
                foreach (var action in pending)
                {
                    action.Value();
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