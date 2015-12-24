using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NoteUI : MonoBehaviour
{
    public GameObject C;
    public RectTransform EB;
    public Flippable flippable;
    public Image backPage;
    public Color shadowColor;
    public int animPatternCount;

    public void NextPage()
    {
        LetsPlay(false);
    }

    public void PrevPage()
    {
        LetsPlay(true);
    }

    void LetsPlay(bool inverse)
    {
        var anim = C.GetComponent<Animator>();
        if (anim.enabled == true)
        {
            throw new System.Exception("Animator already enabled");
        }
        anim.enabled = true;
        var invToken = "";
        if (inverse)
        {
            invToken = "Inv_";  
        }

        var stateName = string.Format("Flip_{0}Pattern_{1}", invToken, Random.Range(1, animPatternCount + 1));
        anim.Play(stateName, -1, 0);

        backPage.color = shadowColor;

        StartCoroutine(Coco(stateName));
    }

    IEnumerator Coco(string stateName)
    {
        var anim = C.GetComponent<Animator>();

        // 이렇게 안하면 같은 animation플레이 할 때 play 안되는 버그 발생...
        yield return null;

        while (true)
        {
            var info = anim.GetCurrentAnimatorStateInfo(0);
            if (info.IsName(stateName) && info.normalizedTime >= 0.99f)
            {
                break;  
            }

            backPage.color = (1 - info.normalizedTime) * shadowColor + info.normalizedTime * Color.white;

            yield return null;
        }

        ResetCorner();
    }

    void ResetCorner()
    {
        var anim = C.GetComponent<Animator>();
        if (anim.enabled == false)
        {
            throw new System.Exception("Animator already disabled");
        }
        anim.enabled = false;
        var RT = C.GetComponent<RectTransform>();
        RT.position = EB.position + new Vector3(10, 10, 0);
    }

    void Start()
    {
        ResetCorner();
    }
}
