using UnityEngine;
using UnityEngine.UI;

namespace Client
{
    public class SwitchFollowMode : MonoBehaviour
{
    [SerializeField]
    private Text _buttonText;
    private CameraMotion _cameraMotion;
    private void Start()
    {
        _cameraMotion = FindObjectOfType<CameraMotion>();
    }
    
    public void SwitchCameraMode()
    {
        _cameraMotion.SwitchFollowMode();
    }

    private void LateUpdate()
    {
        if (_cameraMotion.GetFollowMode())
        {
            _buttonText.text = ">cam<";
            return;
        }
        _buttonText.text = "<cam>";
    }
}
}