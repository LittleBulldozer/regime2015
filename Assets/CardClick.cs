using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Toggle))]
public class CardClick : MonoBehaviour
{
    public Button selectButton;

    Animator anim;
    Toggle toggle;

    void Awake()
    {
        anim = GetComponent<Animator>();
        toggle = GetComponent<Toggle>();

        toggle.onValueChanged.AddListener(OnValueChanged);
    }

    void OnValueChanged(bool isOn)
    {
        if (isOn)
        {
            anim.SetTrigger("GoCenter");
            selectButton.onClick.AddListener(OnSelected);
        }
        else
        {
            anim.SetTrigger("Back");
            selectButton.onClick.RemoveListener(OnSelected);
        }

        transform.SetSiblingIndex(transform.parent.childCount - 1);
    }

    void OnSelected()
    {
        TheWorld.gameFlow.NotifyCardSelected();
    }
}
