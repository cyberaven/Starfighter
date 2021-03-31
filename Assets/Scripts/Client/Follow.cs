using Client;
using Client.Core;
using UnityEngine;

public class Follow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player;
    private Vector3 offset;
    public Camera navigatorCamera;
    void Start()
        {
        offset = Vector3.zero + Vector3.up*10;
        }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Player.transform.position + offset;
        ZoomCamera();
    }
    void ZoomCamera()
    {
        navigatorCamera.orthographicSize += Input.GetAxis("Mouse ScrollWheel") * 50;
        if(navigatorCamera.orthographicSize < 20) navigatorCamera.orthographicSize = 20;
        if(navigatorCamera.orthographicSize > 200) navigatorCamera.orthographicSize = 200;
    }
}
