using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NoteUI : MonoBehaviour
{
    public GameObject C;
    public RectTransform EB;
    public RectTransform frontPageMask;
    public RectTransform frontPageBackSide;
    public Flippable flippable;
    public RawImage backPageShadow;
    public Color shadowColor;
    public int animPatternCount;
    public int reverseAnimPatternCount;

    public void NextPage()
    {
        prevPageIndex = book.currentPageIndex;
        book.currentPageIndex++;
        LetsPlay(false, animPatternCount);
    }

    public void PrevPage()
    {
        prevPageIndex = book.currentPageIndex;
        book.currentPageIndex--;
        LetsPlay(true, reverseAnimPatternCount);
    }

    public void NextTargetPage(int pageIndex)
    {
        prevPageIndex = book.currentPageIndex;
        book.currentPageIndex = pageIndex;
        LetsPlay(false, animPatternCount);
    }

    public void PrevTargetPage(int pageIndex)
    {
        prevPageIndex = book.currentPageIndex;
        book.currentPageIndex = pageIndex;
        LetsPlay(true, reverseAnimPatternCount);
    }

    public void BackPage()
    {
        book.currentPageIndex = prevPageIndex;
        LetsPlay(true, reverseAnimPatternCount);
    }

    Book book;
    int prevPageIndex;

    void LetsPlay(bool inverse, int patternCounts)
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
            invToken = "Reverse_";  
        }

        var stateName = string.Format("Flip_{0}Pattern_{1}", invToken, Random.Range(1, patternCounts + 1));
        anim.Play(stateName, -1, 0);

        backPageShadow.color = shadowColor;
        
        StartCoroutine(Coco(inverse, stateName));
    }

    IEnumerator Coco(bool inverse, string stateName)
    {
        var anim = C.GetComponent<Animator>();

        flippable.GetComponent<RawImage>().enabled = false;

        // 이렇게 안하면 같은 animation플레이 할 때 play 안되는 버그 발생...
        yield return null;

        flippable.enabled = true;

        book.PreloadPage(inverse);

        int activeCount = 0;
            
        while (true)
        {
            var info = anim.GetCurrentAnimatorStateInfo(0);
            if (info.IsName(stateName) && info.normalizedTime >= 0.99f)
            {
                break;  
            }

            float t = info.normalizedTime;
            if (inverse)
            {
                t = 1 - t;
            }
            backPageShadow.color = (1 - t) * shadowColor + t * new Color(1,1,1,0);

            if (activeCount++ > 3)
            {
                flippable.GetComponent<RawImage>().enabled = true;
            }

            yield return null;
        }

        book.SwitchPage(inverse);

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
        RT.position = EB.position;
        flippable.enabled = false;
        frontPageMask.position = RT.position;
        frontPageMask.rotation = Quaternion.identity;
        frontPageBackSide.position = RT.position;
        frontPageBackSide.rotation = Quaternion.identity;
    }

    void Start()
    {
        ResetCorner();
    }

    void Awake()
    {
       book = GetComponent<Book>();
    }
}
