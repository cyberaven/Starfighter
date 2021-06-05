using Client;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class RotationPanelScript : MonoBehaviour

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
        _image.fillAmount = _ship.shipRotation.magnitude*Mathf.Rad2Deg / 90;
    }
}
