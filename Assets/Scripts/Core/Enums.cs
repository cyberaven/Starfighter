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

    public enum UnitType
    {
        Ship,
        Asteroid,
        WayPoint,
        WorldObject
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
