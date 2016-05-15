using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class Book : MonoBehaviour
{
    public delegate void PreloadEvent(bool inverse);
    public PreloadEvent preloadEvent;

    public delegate void SwitchEvent();
    public SwitchEvent switchEvent;

    public Page pagePrefab;

    public RectTransform frontSpace;
    public RectTransform hiddenSpace;
    public RectTransform backSpace;

    public List<RectTransform> managedObjs = new List<RectTransform>();

    [System.NonSerialized]
    public int currentPageIndex = 0;

    public void SetCurrentPageIndex(int pageIndex)
    {
        currentPageIndex = pageIndex;

        frontPage.SetPage(currentPageIndex);
    }
    
    // phase 0
    public void PreloadPage(bool inverse)
    {
        if (inverse)
        {
            SwitchFrontBackPage();
            frontPage.SetPage(currentPageIndex);
        }
        else
        {
            backPage.SetPage(currentPageIndex);
        }

        if (preloadEvent != null)
        {
            preloadEvent(inverse);
        }
    }

    // phase 1
    public void SwitchPage(bool inverse)
    {
        frontPage.SetPage(currentPageIndex);

        if (switchEvent != null)
        {
            switchEvent();
        }
    }

    Page frontPage;
    Page backPage;

    void Awake()
    {
        frontPage = Instantiate(pagePrefab);
        frontPage.transform.SetParent(frontSpace, false);
        backPage = Instantiate(pagePrefab);
        backPage.transform.SetParent(backSpace, false);
    }

    void OnEnable()
    {
        frontPage.SetPage(currentPageIndex);

        if (switchEvent != null)
        {
            switchEvent();
        }
    }

    void SwitchFrontBackPage()
    {
        frontPage.transform.SetParent(backSpace.transform);
        backPage.transform.SetParent(frontSpace.transform);
        var temp = backPage;
        backPage = frontPage;
        frontPage = temp;
    }
}
