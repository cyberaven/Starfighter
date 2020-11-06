using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Core;
using ScriptableObjects;

namespace Core
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField]
        private UserType userType;

        [SerializeField]
        public KeyConfig keyBinding;

        protected override void Awake()
        {
            base.Awake();
        }
        
        void Start()
        {  
            //Load all Settings
            InitAxes(keyBinding);
            //Init and Start All Systems
            //Connect to Server
        }

        
        void Update()
        {

        }

        void OnExit()
        {
            //Save all shit
            //Close all connections
            //Destroy all shit
            //Clean all mem
            //Close application
        }

        private void InitAxes(KeyConfig keys){
            
        }
    }
}