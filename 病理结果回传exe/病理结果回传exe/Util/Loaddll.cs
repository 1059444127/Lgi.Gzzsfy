using System;
using System.Runtime.InteropServices;

namespace SendPisResult.Util
{
    class Loaddll
    {
        [DllImport("kernel32.dll")]
        public extern static IntPtr LoadLibrary(string path);

        [DllImport("kernel32.dll")]
        public extern static IntPtr LoadLibraryEx(string path, IntPtr lib,UInt32 flag);

        [DllImport("kernel32.dll")]
        public extern static IntPtr GetProcAddress(IntPtr lib, string funcName);

        [DllImport("kernel32.dll")]
        public extern static bool FreeLibrary(IntPtr lib);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("user32", EntryPoint = "CallWindowProc")]
        public static extern int CallWindowProc(IntPtr lpPrevWndFunc, int hwnd, int MSG, int wParam, int lParam);

    }
    public class LoadDllapi
    {
        private UInt32 LOAD_WITH_ALTERED_SEARCH_PATH = 0x00000008;
        IntPtr DllLib;//DLL文件名柄     
        #region 构造函数      
        public LoadDllapi()
        { }
        public LoadDllapi(string dllpath)
        {
            DllLib = Loaddll.LoadLibrary(dllpath);
        }
        #endregion
        /// <summary>      
        /// 析构函数      
        /// </summary>      
        ~LoadDllapi()
        {
            Loaddll.FreeLibrary(DllLib);//释放名柄      
        }
        public void freeLoadDll()
        {

            Loaddll.FreeLibrary(DllLib);//释放名柄      
            DllLib = IntPtr.Zero;
        }
        public IntPtr initPath(string dllpath)
        {
            if (DllLib == IntPtr.Zero)
            {
                DllLib = Loaddll.LoadLibrary(dllpath);
            }
            return DllLib;
        }

        public IntPtr initPathEx(string dllpath)
        {
            if (DllLib == IntPtr.Zero)
            {
                DllLib = Loaddll.LoadLibraryEx(dllpath, DllLib, LOAD_WITH_ALTERED_SEARCH_PATH);
            }
            return DllLib;
        }
        /// <summary>      
        /// 获取ＤＬＬ中一个方法的委托      
        /// </summary>      
        /// <param name="methodname"></param>      
        /// <param name="methodtype"></param>      
        /// <returns></returns>      
        public Delegate InvokeMethod(string methodname, Type methodtype)
        {
            IntPtr MethodPtr = Loaddll.GetProcAddress(DllLib, methodname);

            return (Delegate)Marshal.GetDelegateForFunctionPointer(MethodPtr, methodtype);
        }
    }


}
