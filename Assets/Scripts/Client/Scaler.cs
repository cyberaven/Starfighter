using UnityEngine;

namespace Client
{
    public class Scaler : MonoBehaviour
    {
        private Camera _cam;

        private void Start()
        {
            _cam = FindObjectOfType<Camera>();
        }
        
        // Update is called once per frame
        private void LateUpdate()
        {
            //TODO: допилить скалирование по триггеру
            transform.localScale = new Vector3(Camera.current.orthographicSize / 20, Camera.current.orthographicSize / 20);
        }
    }
}
