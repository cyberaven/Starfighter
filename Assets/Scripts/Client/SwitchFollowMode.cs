using UnityEngine;
using UnityEngine.UI;

namespace Client
{
    public class SwitchFollowMode : MonoBehaviour
{
    [SerializeField] private Text _buttonText;

    public void SwitchCameraMode()
    {
        FollowMode.active = !FollowMode.active;
    }

    private void LateUpdate()
    {
        switch (FollowMode.active)
        {
            case true:
            {
                _buttonText.text = ">cam<";
                break;
            }
            case false:
            {
                _buttonText.text = "<cam>";
                break;
            }
        }
    }
}
}