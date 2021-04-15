using UnityEngine;

namespace Client
{
    public class Follow : MonoBehaviour
    {
        // Start is called before the first frame update
        public GameObject Player;
        private Vector3 _offset;
    
        void Start()
        {
            _offset = Vector3.zero + Vector3.up*40;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            transform.position = Player.transform.position + _offset;
        }
    }
}
