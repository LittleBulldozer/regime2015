using UnityEngine;
using System.Collections.Generic;

public class SelectGroup : MonoBehaviour
{
    public List<Selectable> selectables = new List<Selectable>();

    public void Register(Selectable selectable)
    {
        selectables.Add(selectable);
    }

    public void UnselectAll(Selectable exception)
    {
        foreach (var selectable in selectables)
        {
            if (selectable == exception)
            {
                continue;
            }

            selectable.Unselect();
        }
    }
}
