namespace Core
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
