using System;
using System.Runtime.InteropServices;
using EasyHook;

namespace EoL
{
    public class Bypass
    {
        private static LocalHook _hook;

        public struct _SYSTEMTIME
        {
            // Token: 0x040006C3 RID: 1731
            public ushort wYear;

            // Token: 0x040006C4 RID: 1732
            public ushort wMonth;

            // Token: 0x040006C5 RID: 1733
            public ushort wDayOfWeek;

            // Token: 0x040006C6 RID: 1734
            public ushort wDay;

            // Token: 0x040006C7 RID: 1735
            public ushort wHour;

            // Token: 0x040006C8 RID: 1736
            public ushort wMinute;

            // Token: 0x040006C9 RID: 1737
            public ushort wSecond;

            // Token: 0x040006CA RID: 1738
            public ushort wMilliseconds;
        }

        [UnmanagedFunctionPointer(CallingConvention.StdCall, SetLastError = true)]
        private delegate void GetSystemTimeDelegate(IntPtr lpSystemTime);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern void GetSystemTime(IntPtr lpSystemTime);

        private unsafe static void GetSystemTimeHooked(IntPtr lpSystemTime)
        {
            GetSystemTime(lpSystemTime);
            _SYSTEMTIME* ptr = (_SYSTEMTIME*)((void*)lpSystemTime);
            ptr->wYear = 2020;
        }

        public static bool IsHooked
        {
            get
            {
                return Bypass._hook != null;
            }
        }

        public static void Hook()
        {
            _hook = LocalHook.Create(LocalHook.GetProcAddress("kernel32.dll", "GetSystemTime"), new GetSystemTimeDelegate(GetSystemTimeHooked), null);
            _hook.ThreadACL.SetInclusiveACL(new int[1]);
            
        }

        public static void Unhook()
        {
            _hook.Dispose();
        }
    }
}