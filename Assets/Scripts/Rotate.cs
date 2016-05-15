using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

    public float speed = 1f;

    public enum XYZ
    {
        X
        , Y
        , Z
    }

    public XYZ axis;

	// Update is called once per frame
	void Update () {

        transform.Rotate((
            axis == XYZ.X
            ? Vector3.right
            : (axis == XYZ.Y
            ? Vector3.up
            : Vector3.forward)), speed * Time.deltaTime);
	}
}
