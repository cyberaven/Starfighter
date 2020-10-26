namespace Enums
{

    public enum UserType
    {
        Pilot,
        Navigator,
    }

    public enum UnitState
    {
        InFlight,
        IsDocked,
        IsDead,
    }

    public enum GameState
    {
        InMenu,
        InGame,
        OnExit //?
    }

    public enum InterpolationType
    {
        NoInterpolation,
        Linear,
        Square
    }
}
