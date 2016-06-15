using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class BlackShadow : MonoBehaviour
{
    public bool blockDie = false;

    Toggle toggle;

    void Awake()
    {
        toggle = GetComponent<Toggle>();

        toggle.onValueChanged.AddListener(OnValueChanged);
    }

    void OnValueChanged(bool isOn)
    {
        bool die = false;

        do
        {
            if (toggle.group.AnyTogglesOn() == false && isOn == false)
            {
                die = true;
                break;
            }
            
            if (isOn && blockDie == false)
            {
                die = true;
                break;
            }

        } while (false);

        if (die)
        {
            TheWorld.cardCanvas.gameObject.SetActive(false);
        }

        blockDie = false;
    }
}
