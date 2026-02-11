using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaidRemake.LockedMapHandle
{
	public static class AlternativeMap
	{
		private static List<string> mapList = new List<string>();

		private static int currIndex = 0;

		public static void Init()
		{
			mapList = LockedMapForm.Instance.getAlternativeMap;
			currIndex = 0;
		}

		public static int Count()
		{
			mapList = LockedMapForm.Instance.getAlternativeMap;
			return mapList.Count;
		}

		public static string GetNext()
		{
			if (mapList.Count > 0)
			{
				if (currIndex >= mapList.Count)
					currIndex = 0;
				currIndex++;
				return mapList[currIndex - 1];
			}
			return null;
		}
	}
}
