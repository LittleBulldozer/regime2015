using UnityEngine;
using System.Collections;

[Action("Popup", ActionGenere.SYSTEM)]
public class PopupAction : Action
{
	public PopupView popupView;
	public string title;
	public Sprite image;
	public string description;

	public override void RunAction ()
	{
        var newView = Instantiate(popupView);
        newView.Set(title, image, description);
        newView.transform.SetParent(TheWorld.eventCanvas.transform, false);
	}
}
