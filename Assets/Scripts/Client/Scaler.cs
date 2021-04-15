using UnityEngine;

namespace Client
{
    public class Scaler : MonoBehaviour
    {
        private Camera _cam;
        
        private void Awake()
        {
            _cam = FindObjectOfType<Camera>();
        }
        
        // Update is called once per frame
        private void LateUpdate()
        {
            //TODO: допилить скалирование по триггеру
            transform.localScale = new Vector3(_cam.orthographicSize / 20, _cam.orthographicSize / 20);
        }
    }
}
