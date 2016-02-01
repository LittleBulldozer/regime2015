using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Selectable : MonoBehaviour
{
    public SelectGroup selectGroup;
    public UnityEvent selectEvent;
    public UnityEvent unselectEvent;

    public void Awake()
    {
        if (selectGroup != null)
        {
            selectGroup.Register(this);
        }
    }

    public void Unselect()
    {
        unselectEvent.Invoke();
        state = State.Unselected;
    }

    public void Select()
    {
        if (selectGroup != null)
        {
            selectGroup.UnselectAll(this);
        }

        selectEvent.Invoke();
        state = State.Selected;
    }

    public void Toggle()
    {
        switch (state)
        {
            case State.Unselected:
                Select();
                break;

            case State.Selected:
                Unselect();
                break;

            default:
                throw new System.Exception("Unhandled state : " + state);
        }
    }

    enum State
    {
        Unselected,
        Selected
    }

    State state = State.Unselected;

}
