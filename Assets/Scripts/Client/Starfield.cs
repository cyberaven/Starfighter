using UnityEngine;
using System.Collections;

public class Starfield: MonoBehaviour 
{
    public Camera camera;
    private Vector2 lastScreenSize = new Vector2();	
		
    void OnEnable() 
    {
        if (!camera)
        {
            Debug.Log ("Camera is not set");
            enabled = false;			
        }
    }
	
    void Update () 
    {
        if (Screen.width != lastScreenSize.x || Screen.height != lastScreenSize.y)
            updateSize();
    }
	
    void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos.x = camera.transform.position.x;
        pos.y = camera.transform.position.y;
        transform.position = pos;
    }
	
    private void updateSize()
    {
        lastScreenSize.x = Screen.width; 
        lastScreenSize.y = Screen.height;
							 
        float maxSize = lastScreenSize.x > lastScreenSize.y ? lastScreenSize.x : lastScreenSize.y;	
        maxSize /= 10;
        transform.localScale = new Vector3(maxSize, 1, maxSize);			
    }
}