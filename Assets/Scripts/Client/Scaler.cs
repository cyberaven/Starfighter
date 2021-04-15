using UnityEngine;

namespace Client
{
    public class Scaler : MonoBehaviour
    {
        // Update is called once per frame
        void LateUpdate()
            //TODO: допилить скалирование по триггеру
        {
            transform.localScale = new Vector3(Camera.current.orthographicSize / 20, Camera.current.orthographicSize / 20);
        }
    }
}
