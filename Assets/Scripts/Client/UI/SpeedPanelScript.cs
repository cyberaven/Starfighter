using Client;
using UnityEngine;
using UnityEngine.UI;

public class SpeedPanelScript : MonoBehaviour
{
    private PlayerScript _ship;
    [SerializeField] private Image _image;
    // Start is called before the first frame update

    
    public void Init(PlayerScript ship)
    {
        _ship = ship;
    }
    
    // Update is called once per frame
    void Update()
    {
        _image.fillAmount = _ship.shipSpeed.magnitude / 100;
    }
}
