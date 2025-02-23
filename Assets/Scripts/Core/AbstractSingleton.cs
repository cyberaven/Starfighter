﻿using UnityEngine;

namespace Core
{
    public abstract class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;
        
        public static T instance
        {
            get
            {
                if ( _instance == null )
                {
                    _instance = FindObjectOfType<T> ();
                    if ( _instance == null )
                    {
                        var obj = new GameObject ();
                        obj.name = typeof ( T ).Name;
                        _instance = obj.AddComponent<T> ();
                    }
                }
                return _instance;
            }
        }

        protected virtual void Awake ()
        {
            if ( _instance == null )
            {
                _instance = this as T;
                DontDestroyOnLoad ( gameObject );
            }
            else
            {
                Destroy ( gameObject );
            }
        }

    }
}