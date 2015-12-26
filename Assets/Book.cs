using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

[ExecuteInEditMode]
public class Book : MonoBehaviour
{
    public RectTransform shownContents;
    public RectTransform hiddenContents;

    [System.NonSerialized]
    public int currentPageIndex = 0;

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

        while (pages.Count <= cachedPageIndex)
        {
            pages.Add(null);
        }

        pages[cachedPageIndex] = pageDesc;

        isDirty = false;
    }

    public void HideContents()
    {
        HideItemContents(0);
        HideItemContents(1);
        HideItemContents(2);
        HideRootContents();
    }

    int cachedPageIndex = -1;
    bool isDirty = false;
    bool skipStoreInfo = false;


    void Update()
    {
        isDirty = true;

        if (cachedPageIndex != currentPageIndex)
        {
            UpdatePage();
            cachedPageIndex = currentPageIndex;
        }
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

    void HideItemContents(int itemIndex)
    {
        var fromItemNode = shownContents.Find(string.Format("Item{0}", itemIndex));
        var fromPictureBone = fromItemNode.Find("PictureBone");
        var toItemNode = hiddenContents.Find(string.Format("Item{0}", itemIndex));
        var toPictureBone = toItemNode.Find("PictureBone");

        ChangeParent(fromPictureBone, toPictureBone, x => true);
        ChangeParent(fromItemNode, toItemNode, x => x.name != "PictureBone");
    }

    void HideRootContents()
    {
        var re = GetItemRegex();
        ChangeParent(shownContents, hiddenContents, x => re.IsMatch(x.name) == false);
    }

    void TransferItemContents(int itemIndex, ItemDesc itemDesc)
    {
        var toItemNode = shownContents.Find(string.Format("Item{0}", itemIndex));
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

    void TransferRootContents(List<GameObject> list)
    {
        foreach (var obj in list)
        {
            obj.transform.SetParent(shownContents, false);
        }
    }

    void GetAppropriateContents()
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

        TransferItemContents(0, pageDesc.item0Contents);
        TransferItemContents(1, pageDesc.item1Contents);
        TransferItemContents(2, pageDesc.item2Contents);
        if (pageDesc.rootContents != null)
        {
            TransferRootContents(pageDesc.rootContents);
        }
    }

    void UpdatePage()
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

        HideContents();

        GetAppropriateContents();
    }
}
