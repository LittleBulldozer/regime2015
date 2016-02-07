using UnityEngine;
using System.Collections;

[Action("Popup", ActionGenere.SYSTEM)]
public class PopupAction : Action
{
	public RectTransform popupUI;
	public string title;
	public Sprite image;
	public string description;

	public override void RunAction ()
	{
		
	}
}
