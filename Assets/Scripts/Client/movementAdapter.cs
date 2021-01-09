namespace Client
{
    public interface IMovementAdapter
    {
        EngineState getMovement();
        float GetThrustSpeed();
        float GetManeurSpeed();
        float GetShipAngle();
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