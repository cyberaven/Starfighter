using UnityEngine;

public class Follow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player;
    private Vector3 offset;
    void Start()
    {
        offset = Vector3.zero + Vector3.up*10;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Player.transform.position + offset;
    }
}
