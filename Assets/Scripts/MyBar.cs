using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[ExecuteInEditMode]
public class MyBar : MonoBehaviour
{
    // 0 ~ 100
    public Observable<float> progress = new Observable<float>(0);
    public Image content;
    public Text leftText;
    public Text rightText;

	void Awake ()
    {
        progress.Listen(UpdateText);
        progress.Value = content.fillAmount * 100f;
    }

#if UNITY_EDITOR
    void Update()
    {
        progress.Value = content.fillAmount * 100f;
    }
#endif

    void UpdateText(float newValue, float prev)
    {
        if (newValue > 50f)
        {
            leftText.gameObject.SetActive(true);
            rightText.gameObject.SetActive(false);
            leftText.text = newValue.ToString();
        }
        else
        {
            leftText.gameObject.SetActive(false);
            rightText.gameObject.SetActive(true);
            rightText.text = newValue.ToString();
        }
    }
}
