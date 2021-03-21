using Net.PackageData.EventsData;

namespace Client.Movement
{
    public class BlankControl: IMovementAdapter
    {
        public EngineState getMovement()
        {
            return new EngineState();
        }

        public float GetThrustSpeed()
        {
            return 0;
        }

        public float GetSideManeurSpeed()
        {
            return 0;
        }

        public float GetStraightManeurSpeed()
        {
            return 0;
        }

        public float GetShipAngle()
        {
            return 0;
        }

        public void UpdateMovementActionData(MovementEventData data)
        { }
    }
}