using System;
using Config;
using Core.InputManager;
using Net.Core;
using Net.PackageData.EventsData;
using Net.Utils;
using UnityEngine;
using EventType = Net.Utils.EventType;

namespace Client.Movement
{   
    public class PlayerControl: IMovementAdapter
    {
        private MovementEventData _lastMovement;
        private KeyConfig _keyConfig;
        
        public PlayerControl()
        {
            NetEventStorage.GetInstance().sendMoves.AddListener(SendMovement);
            NetEventStorage.GetInstance().sendAction.AddListener(SendAction);
            _keyConfig = InputManager.instance.keyConfig;
        }
        
        public EngineState getMovement()
        {
            var state = new EngineState();
            if(GetThrustSpeed() != 0)
            {
                state.Thrust = true;
            }
            if(GetShipAngle() < 0)
            {
                state.TopRight = true;
                state.BotLeft = true;
            }
            if(GetShipAngle() > 0)
            {
                state.TopLeft = true;
                state.BotRight = true;
            }
            if(GetStraightManeurSpeed() < 0)
            {
                state.TopLeft = true;
                state.TopRight = true;
            }
            if(GetStraightManeurSpeed() > 0)
            {
                state.BotLeft = true;
                state.BotRight = true;
            }
            if(GetSideManeurSpeed() > 0)
            {
                state.TopLeft = true;
                state.BotLeft = true;
            }
            if(GetSideManeurSpeed() < 0)
            {
                state.TopRight = true;
                state.BotRight = true;
            }
            return state;
        }

        public float GetThrustSpeed() => Input.GetAxis("Jump") * 5f;

        public float GetSideManeurSpeed() => Input.GetAxis("Horizontal");

        public float GetStraightManeurSpeed() => Input.GetAxis("Vertical");

        public float GetShipAngle() => Input.GetAxis("Rotation") * 4.5f;

        public bool GetDockAction() => Input.GetKeyDown(_keyConfig.dock);

        public bool GetFireAction() => Input.GetKeyDown(_keyConfig.fire);
        
        public void UpdateMovementActionData(MovementEventData data)
        {
            _lastMovement = data;
        }

        public async void SendMovement(StarfighterUdpClient udpClient)
        {
            try
            {
                var movementData = new MovementEventData()
                {
                    rotationValue = GetShipAngle(),
                    sideManeurValue = GetSideManeurSpeed(),
                    straightManeurValue = GetStraightManeurSpeed(),
                    thrustValue = GetThrustSpeed()
                };
                UpdateMovementActionData(movementData);
                var result = await udpClient.SendEventPackage(movementData, Net.Utils.EventType.MoveEvent);
            }
            catch (Exception ex)
            {
                Debug.unityLogger.LogException(ex);
            }
        }

        public async void SendAction(StarfighterUdpClient udpClient)
        {
            try
            {
                if (GetDockAction())
                {
                    var result = await udpClient.SendEventPackage(null, EventType.DockEvent);
                }

                if (GetFireAction())
                {
                    var result = await udpClient.SendEventPackage(null, EventType.FireEvent);
                }
            }
            catch (Exception ex)
            {
                Debug.unityLogger.LogException(ex);
            }
        }
    }
}