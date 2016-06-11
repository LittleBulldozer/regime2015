using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Toggle))]
public class CardClick : MonoBehaviour
{
    public Button selectButton;
    public Toggle contentToggle;

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
            selectButton.gameObject.SetActive(true);
            selectButton.onClick.AddListener(OnSelected);
            transform.SetSiblingIndex(transform.parent.childCount - 1);
            contentToggle.gameObject.SetActive(true);
            contentToggle.onValueChanged.AddListener(OnContentToggled);
        }
        else
        {
            contentToggle.onValueChanged.RemoveListener(OnContentToggled);
            contentToggle.gameObject.SetActive(false);
            selectButton.onClick.RemoveListener(OnSelected);
            selectButton.gameObject.SetActive(false);
            anim.SetTrigger("Back");
        }
    }

    void OnSelected()
    {
        TheWorld.gameFlow.NotifyCardSelected();
    }

    void OnContentToggled(bool isOn)
    {
        anim.SetBool("ContentIsFront", !isOn);
    }
}
