using UnityEngine;
using System.Collections;

public class DontMove : MonoBehaviour
{
    Vector3 pos;
    Quaternion rot;
    
	void Start ()
    {
        var RT = GetComponent<RectTransform>();
        pos = RT.position;
        rot = RT.rotation;
	}
	
	void LateUpdate ()
    {
        transform.position = pos;
        transform.rotation = rot;
	}
}
