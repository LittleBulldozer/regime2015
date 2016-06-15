using UnityEngine;
using System.Collections;

public class PaperButton : MonoBehaviour
{
    void OnMouseDown()
    {
        if (TheWorld.cardCanvas.gameObject.activeSelf == false)
        {
            TheWorld.cardCanvas.gameObject.SetActive(true);
            TheWorld.cardCanvas.scaleGroup.ShowImmediate();
        }
    }
}
