using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

public class CSFactory
{
	public delegate string TokenHandlerFn();

	public class TokenHandler
	{
		public string token;
		public TokenHandlerFn fn;

		public TokenHandler(string token, TokenHandlerFn fn)
		{
			this.token = token;
			this.fn = fn;
		}
	}

	public static string MakeFromTemplate(string template
		, params TokenHandler [] handlers)
	{
		var str = template;

		foreach (var handler in handlers)
		{
			var tokenstr = string.Format("#{0}#", handler.token);
			str = str.Replace(tokenstr, handler.fn());
		}

		return str;
	}
}
