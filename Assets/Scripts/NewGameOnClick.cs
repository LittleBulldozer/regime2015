using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewGameOnClick : MonoBehaviour
{
    Button btn;

	void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
	}
	
	void OnClick ()
    {
        btn.onClick.RemoveListener(OnClick);

        SceneManager.LoadSceneAsync("game");
	}
}
