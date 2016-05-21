using UnityEngine;
using System.Collections;

public class CardCanvasScaleGroup : MonoBehaviour
{
    public void ShowWithDelap()
    {
        GetComponent<Animator>().Play("Show", 0, 0);
    }

    public void ShowImmediate()
    {
        GetComponent<Animator>().Play("ShowImmediate", 0, 0);
    }
}
