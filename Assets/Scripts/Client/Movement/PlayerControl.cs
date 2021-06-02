using System;
using Net.Core;
using Net.PackageData.EventsData;
using Net.Utils;
using UnityEngine;

namespace Client.Movement
{   
    public class PlayerControl: IMovementAdapter
    {
        private MovementEventData _lastMovement;
        
        public PlayerControl()
        {
            NetEventStorage.GetInstance().sendMoves.AddListener(SendMovement);
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

        public float GetThrustSpeed()
        { 
            return Input.GetAxis("Jump") * 5f;
        }

        public float GetSideManeurSpeed()
        { 
            return Input.GetAxis("Horizontal");
        }

        public float GetStraightManeurSpeed()
        { 
            return Input.GetAxis("Vertical");
        }

        public float GetShipAngle()
        { 
            return Input.GetAxis("Rotation") * 4.5f;
        }

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
    }
}