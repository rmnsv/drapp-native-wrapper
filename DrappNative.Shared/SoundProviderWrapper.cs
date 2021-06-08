using System;
using System.Runtime.InteropServices;

namespace DrappNative
{
    public static class SoundProviderWrapper
    {
#if DRAPP_ANDROID
        const string DllName = "libdrapp-native.so";
#else
        const string DllName = "__Internal";
#endif

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void SoundProviderCallback(IntPtr wave, int size);

        [DllImport(DllName, EntryPoint = nameof(CreateSoundProviderClass))]
        internal static extern SoundProviderSafeHandle CreateSoundProviderClass();

        [DllImport(DllName, EntryPoint = nameof(DisposeSoundProviderClass))]
        internal static extern void DisposeSoundProviderClass(IntPtr ptr);

        [DllImport(DllName, EntryPoint = nameof(PutSound))]
        internal static extern int PutSound(SoundProviderSafeHandle ptr, IntPtr wave, int size);

        [DllImport(DllName, EntryPoint = nameof(GetSound))]
        internal static extern void GetSound(SoundProviderSafeHandle ptr, int id, [MarshalAs(UnmanagedType.FunctionPtr)] SoundProviderCallback callback);
    }
}