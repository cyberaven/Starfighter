using Core;
using Net.PackageData.EventsData;

namespace Client.Movement
{
    public interface IMovementAdapter
    {
        EngineState getMovement();
        float GetThrustSpeed();
        float GetSideManeurSpeed();
        float GetStraightManeurSpeed();
        float GetShipAngle();
        void UpdateMovementActionData(MovementEventData data);
        void OnStateChange(UnitState newState);
    }

    public struct EngineState
    {
        public bool TopLeft;
        public bool TopRight;
        public bool BotLeft;
        public bool BotRight;
        public bool Thrust;

        public override string ToString()
        {
            return $"thrust: {Thrust}, topLeft: {TopLeft}, topRight: {TopRight}, botLeft:{BotLeft}, botRight:{BotRight}";
        }
    }
}