using UnityEngine;
using System;

[AttributeUsage(AttributeTargets.Class)]
public class ActionAttribute : Attribute
{
	public ActionAttribute(string label, params ActionGenere [] gg)
	{
		this.label = label;
		this.genres = gg;
	}

	public string label;
	public ActionGenere [] genres;
}
