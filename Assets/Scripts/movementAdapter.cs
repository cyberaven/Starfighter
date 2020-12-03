using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class movementAdapter
{
    public abstract engineState getMovement();
}

public struct engineState
{
    public bool topLeft;
    public bool topRight;
    public bool botLeft;
    public bool botRight;
    public bool Thrust;   
}