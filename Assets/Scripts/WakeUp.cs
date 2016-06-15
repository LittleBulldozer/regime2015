using UnityEngine;
using System.Collections;

public class WakeUp : MonoBehaviour
{
    public float time = 1f;

    public GameObject target;

	void Start ()
    {
        StartCoroutine(PleaseWakeUp());
	}

    IEnumerator PleaseWakeUp()
    {
        yield return new WaitForSeconds(time);

        target.SetActive(true);
    }
}
