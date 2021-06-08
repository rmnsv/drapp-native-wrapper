using System;
using System.Runtime.InteropServices;

namespace Drapp.Native
{
    internal static class DrumMathWrapper
    {

#if DRAPP_ANDROID
        const string DllName = "libdrapp-native.so";
#else
        const string DllName = "__Internal";
#endif

        [DllImport(DllName, EntryPoint = nameof(CreateDrumMathClass))]
        internal static extern DrumMathSafeHandle CreateDrumMathClass();
        
        [DllImport(DllName, EntryPoint = nameof(DisposeDrumMathClass))]
        internal static extern void DisposeDrumMathClass(IntPtr ptr);
        
        [DllImport(DllName, EntryPoint = nameof(DrumMathBpmToMs))]
        internal static extern double DrumMathBpmToMs(DrumMathSafeHandle ptr, int bpm);
    }
}