using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class StickTo : MonoBehaviour
{
    public RectTransform target;

    RectTransform RT;

    void Awake()
    {
        RT = GetComponent<RectTransform>();
    }

	void LateUpdate ()
    {
        if (target != null)
        {
            RT.position = target.position;
            RT.rotation = target.rotation;
        }
	}
}
