using Client;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public void SwitchCameraMode()
    {
        FollowMode.active = !FollowMode.active;
    }   
}
