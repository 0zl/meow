using EasyHook;
using System;
using System.Runtime.InteropServices;

namespace Grimoire.FlashEoLHook
{
	public class EoLHook
	{
		public static bool IsHooked
		{
			get
			{
				return EoLHook._hook != null;
			}
		}

		public static void Hook()
		{
			EoLHook._hook = LocalHook.Create(LocalHook.GetProcAddress("kernel32.dll", "GetSystemTime"), new EoLHook.GetSystemTimeDelegate(EoLHook.GetSystemTimeHooked), null);
			EoLHook._hook.ThreadACL.SetInclusiveACL(new int[1]);
		}

		public static void Unhook()
		{
			EoLHook._hook.Dispose();
		}

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern void GetSystemTime(IntPtr lpSystemTime);

		private unsafe static void GetSystemTimeHooked(IntPtr lpSystemTime)
		{
			EoLHook.GetSystemTime(lpSystemTime);
			EoLHook._SYSTEMTIME* ptr = (EoLHook._SYSTEMTIME*)((void*)lpSystemTime);
			ptr->wYear = 2020;
		}

		private static LocalHook _hook;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, SetLastError = true)]
		private delegate void GetSystemTimeDelegate(IntPtr lpSystemTime);

		public struct _SYSTEMTIME
		{
			public ushort wYear;

			public ushort wMonth;

			public ushort wDayOfWeek;

			public ushort wDay;

			public ushort wHour;

			public ushort wMinute;

			public ushort wSecond;

			public ushort wMilliseconds;
		}
	}
}
