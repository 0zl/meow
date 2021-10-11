using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.Utils
{
	class StringUtils
	{
		public static string GetBetween(string strSource, string strStart, string strEnd)
		{
			if (strSource.Contains(strStart) && strSource.Contains(strEnd))
			{
				int Start, End;
				int IndexStart = 0;

				Start = strSource.IndexOf(strStart, IndexStart) + strStart.Length;
				End = strSource.IndexOf(strEnd, Start);
				IndexStart = Start;

				return strSource.Substring(Start, End - Start);
			}
			return null;
		}
	}
}
