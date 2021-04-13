using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Client
{
    public class SwitchFollowMode : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _buttonText;
        [SerializeField] private CameraMotion _cameraMotion;

        private void Start()
        {
            _buttonText = transform.GetComponentInChildren<TextMeshProUGUI>();
        }
        
        public void SwitchButtonText()
        {
            switch (_cameraMotion.GetFollowMode())
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