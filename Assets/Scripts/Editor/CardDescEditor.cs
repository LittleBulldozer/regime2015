using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(CardDesc))]
public class CardDescEditor : Editor
{
	[MenuItem("Assets/Create/Card")]
	public static void CreateCardDesc()
	{
		ScriptableObjectUtility.CreateAsset<CardDesc>();
	}

	public void OnEnable()
	{
		infoStyle = new GUIStyle();
		infoStyle.normal.textColor = Color.white;
		cardTemplateTex = Resources.Load<Texture2D>("NewMagicTemplate");

		titleStyle = new GUIStyle();
		descStyle = new GUIStyle();
		descStyle.wordWrap = true;
	}

	public override bool HasPreviewGUI()
	{
		return true;
	}

	public override void OnInteractivePreviewGUI(Rect rt, GUIStyle bg)
	{
		if (desc.sprite == null)
		{
			EditorGUI.LabelField(rt, "sprite가 없음", infoStyle);
			return;
		}

		var auto = new LayoutHelper(300, 400, rt);
		EditorGUI.DrawPreviewTexture(auto.Rect(0, 0, 300, 400), cardTemplateTex);
		var spriteEditor = Editor.CreateEditor(desc.sprite, null);
		var tex = spriteEditor.RenderStaticPreview("", null, 250, 200);
		EditorGUI.DrawPreviewTexture(auto.Rect(25, 78, 250, 168), tex);
		titleStyle.fontSize = (int)(auto.scale * 18);
		EditorGUI.LabelField(auto.Rect(25, 30, 250, 50), desc.title, titleStyle);
		descStyle.fontSize = (int)(auto.scale * 15);
		EditorGUI.LabelField(auto.Rect(25, 280, 250, 150), desc.description, descStyle);
	}

	GUIStyle infoStyle;
	GUIStyle titleStyle;
	GUIStyle descStyle;
	Texture cardTemplateTex;

	CardDesc desc
	{
		get
		{
			return target as CardDesc;
		}
	}

	delegate Rect Rector();
	delegate Vector2 Pointer(Vector2 point);

	class LayoutHelper
	{
		public float scale;

		public LayoutHelper(float W, float H, Rect rt)
		{
			this.W = W;
			this.H = H;
			this.rt = rt;

			var standardRatio = (float)H / W;
			var realRatio = rt.height / rt.width;

			scale = realRatio > standardRatio ?
				rt.width / W // // sharp device
				: rt.height / H;  //blunt device
		}

		public Rect Rect(float x, float y, float width, float height)
		{
			var p0 = new Vector2(x, y);
			var p1 = new Vector2(x + width, y + height);
			p0 = TransformPoint(p0);
			p1 = TransformPoint(p1);

			return new Rect(rt.x + p0.x, rt.y + p0.y, p1.x - p0.x, p1.y - p0.y);	
		}

		float W;
		float H;
		Rect rt;

		Vector2 CentralizePoint(Vector2 p)
		{
			return new Vector2(p.x - 0.5f * W, p.y - 0.5f * H);
		}

		Vector2 ScalePoint(Vector2 p)
		{
			return new Vector2(p.x * scale, p.y * scale);
		}

		Vector2 DecentralizedPoint(Vector2 p)
		{
			return new Vector2(p.x + 0.5f * rt.width, p.y + 0.5f * rt.height);
		}

		Vector2 TransformPoint(Vector2 p)
		{
			return DecentralizedPoint(ScalePoint(CentralizePoint(p)));
		}
	}
}
