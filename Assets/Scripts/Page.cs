using UnityEngine;
using System.Collections;

public class Page : MonoBehaviour
{
    public virtual void SetPage(int idx)
    {
        throw new System.Exception("Don't call me!");
    }
}
