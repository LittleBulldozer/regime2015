using UnityEngine;
using System;

[AttributeUsage(AttributeTargets.Class)]
public class ActionAttribute : Attribute
{
	public ActionAttribute(string label, params ActionGenere [] generes)
	{
		this.label = label;
		this.genres = genres;
	}

	public string label;
	public ActionGenere [] genres;
}
