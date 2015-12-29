using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Book : MonoBehaviour
{
    public delegate void PreloadEvent(bool inverse);
    public PreloadEvent preloadEvent;

    public delegate void SwitchEvent();
    public SwitchEvent switchEvent;

    public RectTransform shownContents;
    public RectTransform hiddenContents;
    public RectTransform backContents;

    [System.NonSerialized]
    public int currentPageIndex = 0;

    public void SetCurrentPageIndex(int pageIndex)
    {
        currentPageIndex = pageIndex;

        UpdatePageImmediate();
    }

    public bool IsDirty()
    {
        return isDirty;
    }

    public int GetCachedPageIndex()
    {
        return cachedPageIndex;
    }

    public void SkipStoreInfo()
    {
        skipStoreInfo = true;
    }

    [System.Serializable]
    public class ItemDesc
    {
        public List<GameObject> rootContents = new List<GameObject>();
        public List<GameObject> pictureBoneContents = new List<GameObject>();
    }

    [System.Serializable]
    public class PageDesc
    {
        public ItemDesc item0Contents;
        public ItemDesc item1Contents;
        public ItemDesc item2Contents;
        public List<GameObject> rootContents;
        public Sprite bg;
    }

    [SerializeField]
    public List<PageDesc> pages;


    public void StoreCachedContentsInfo()
    {
        var pageDesc = new PageDesc();
        pageDesc.item0Contents = GetCachedItemContents(0);
        pageDesc.item1Contents = GetCachedItemContents(1);
        pageDesc.item2Contents = GetCachedItemContents(2);
        pageDesc.rootContents = GetRootContents();
        pageDesc.bg = shownContents.parent.GetComponent<Image>().sprite;

        while (pages.Count <= cachedPageIndex)
        {
            pages.Add(null);
        }

        pages[cachedPageIndex] = pageDesc;

        isDirty = false;
    }

    public void MoveShownContents(RectTransform targetContainer)
    {
        MoveShownItemContents(targetContainer, 0);
        MoveShownItemContents(targetContainer, 1);
        MoveShownItemContents(targetContainer, 2);
        MoveShownRootContents(targetContainer);

        var targetImage = targetContainer.parent.GetComponent<Image>();
        if (targetImage != null)
        {
            targetImage.sprite = shownContents.parent.GetComponent<Image>().sprite;
        }
    }

    // phase 0
    public void PreloadPage(bool inverse)
    {
        if (inverse)
        {
            MoveShownContents(backContents);
            GetAppropriateContents(shownContents);
        }
        else
        {
            GetAppropriateContents(backContents);
        }

        if (preloadEvent != null)
        {
            preloadEvent(inverse);
        }
    }

    // phase 1
    public void SwitchPage(bool inverse)
    {
        MoveShownContents(hiddenContents);
        GetAppropriateContents(shownContents);

        if (switchEvent != null)
        {
            switchEvent();
        }
    }

    int cachedPageIndex = -1;
    bool isDirty = false;
    bool skipStoreInfo = false;

#if UNITY_EDITOR
    void Update()
    {
        if (Application.isPlaying == false)
        {
            isDirty = true;

            if (cachedPageIndex != currentPageIndex)
            {
                UpdatePageImmediate();
            }
        }
    }
#endif

    void UpdatePageImmediate()
    {
        if (Application.isPlaying == false && cachedPageIndex >= 0)
        {
            if (skipStoreInfo)
            {
                skipStoreInfo = false;
                isDirty = false;
            }
            else
            {
                StoreCachedContentsInfo();
            }
        }

        MoveShownContents(hiddenContents);

        GetAppropriateContents(shownContents);

        cachedPageIndex = currentPageIndex;
    }

    ItemDesc GetCachedItemContents(int itemIndex)
    {
        var itemDesc = new ItemDesc();
        var itemNode = shownContents.Find(string.Format("Item{0}", itemIndex));
        var pictureBone = itemNode.Find("PictureBone");

        foreach (Transform child in pictureBone)
        {
            itemDesc.pictureBoneContents.Add(child.gameObject);
        }

        foreach (Transform child in itemNode)
        {
            if (child.name != "PictureBone")
            {
                itemDesc.rootContents.Add(child.gameObject);
            }
        }

        return itemDesc;
    }

    Regex GetItemRegex()
    {
        return new Regex("Item[0-2]$");
    }

    List<GameObject> GetRootContents()
    {
        List<GameObject> ret = new List<GameObject>();
        var re = GetItemRegex();
        foreach (Transform child in shownContents)
        {
            if (re.IsMatch(child.name) == false)
            {
                ret.Add(child.gameObject);
            }
        }

        return ret;
    }

    delegate bool CheckFilter(Transform a);
    static void ChangeParent(Transform from, Transform to, CheckFilter filter)
    {
        // foreach에서 바로 setparent하면 버그남.
        var list = new List<Transform>();
        foreach(Transform child in from)
        {
            if (filter(child))
            {
                list.Add(child);
            }
        }

        foreach (var child in list)
        {
            child.SetParent(to, false);
        }
    }

    void MoveShownItemContents(RectTransform targetContainer, int itemIndex)
    {
        var fromItemNode = shownContents.Find(string.Format("Item{0}", itemIndex));
        var fromPictureBone = fromItemNode.Find("PictureBone");
        var toItemNode = targetContainer.Find(string.Format("Item{0}", itemIndex));
        var toPictureBone = toItemNode.Find("PictureBone");

        ChangeParent(fromPictureBone, toPictureBone, x => true);
        ChangeParent(fromItemNode, toItemNode, x => x.name != "PictureBone");
    }

    void MoveShownRootContents(RectTransform targetContainer)
    {
        var re = GetItemRegex();
        ChangeParent(shownContents, targetContainer, x => re.IsMatch(x.name) == false);
    }

    void GetItemContents(RectTransform targetContainer, int itemIndex, ItemDesc itemDesc)
    {
        var toItemNode = targetContainer.Find(string.Format("Item{0}", itemIndex));
        var toPictureBone = toItemNode.Find("PictureBone");

        foreach (var obj in itemDesc.pictureBoneContents)
        {
            obj.transform.SetParent(toPictureBone, false);
        }

        foreach (var obj in itemDesc.rootContents)
        {
            obj.transform.SetParent(toItemNode, false);
        }
    }

    void GetRootContents(RectTransform targetContainer, List<GameObject> list)
    {
        foreach (var obj in list)
        {
            obj.transform.SetParent(targetContainer, false);
        }
    }

    void GetAppropriateContents(RectTransform targetContainer)
    {
        if (pages.Count <= currentPageIndex)
        {
            return;
        }

        var pageDesc = pages[currentPageIndex];
        if (pageDesc == null)
        {
            return;
        }

        GetItemContents(targetContainer, 0, pageDesc.item0Contents);
        GetItemContents(targetContainer, 1, pageDesc.item1Contents);
        GetItemContents(targetContainer, 2, pageDesc.item2Contents);
        if (pageDesc.rootContents != null)
        {
            GetRootContents(targetContainer, pageDesc.rootContents);
        }

        targetContainer.parent.GetComponent<Image>().sprite = pageDesc.bg;
    }
}
