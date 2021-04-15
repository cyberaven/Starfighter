namespace Enums
{

    public enum UserType
    {   
        Admin,
        Pilot,
        Navigator,
        Spectator,
        Moderator //игротех
    }

    public enum UnitState
    {
        InFlight,
        IsMoored,
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
