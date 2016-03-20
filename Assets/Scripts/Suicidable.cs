using UnityEngine;
using System.Collections;

public class Suicidable : MonoBehaviour
{
    public void Die()
    {
        Destroy(gameObject);
    }
}
