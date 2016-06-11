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
        var stateInfo = anim.GetCurrentAnimatorStateInfo(1);
        if (stateInfo.IsName("ContentFront") && isOn)
        {
            if (stateInfo.normalizedTime < .7f)
            {
                anim.Play("ContentBack", 1, 1f);
            }
            else if (stateInfo.normalizedTime < 1f)
            {
                anim.Play("ContentFront", 1, 1f);
                return;
            }
        }

        if (stateInfo.IsName("ContentBack") && !isOn)
        {
            if (stateInfo.normalizedTime < .7f)
            {
                anim.Play("ContentFront", 1, 1f);
            }
            else if (stateInfo.normalizedTime < 1f)
            {
                anim.Play("ContentBack", 1, 1f);
                return;
            }
        }

        anim.SetBool("ContentIsFront", !isOn);
    }
}
