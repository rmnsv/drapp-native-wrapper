using System;
using System.Runtime.InteropServices;

namespace DrappNative
{
    public static class SoundGeneratorWrapper
    {

#if DRAPP_ANDROID
        const string DllName = "libdrapp-native.so";
#else
        const string DllName = "__Internal";
#endif

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void SoundGeneratorCallback(IntPtr wave, int size);

        [DllImport(DllName, EntryPoint = nameof(CreateSoundGeneratorClass))]
        internal static extern SoundGeneratorSafeHandle CreateSoundGeneratorClass(int sampleRate);

        [DllImport(DllName, EntryPoint = nameof(DisposeSoundGeneratorClass))]
        internal static extern void DisposeSoundGeneratorClass(IntPtr ptr);

        [DllImport(DllName, EntryPoint = nameof(TestGetSineWave16BitPcm))]
        internal static extern void TestGetSineWave16BitPcm(SoundGeneratorSafeHandle ptr, int samples, double frequencyOfTone, [MarshalAs(UnmanagedType.FunctionPtr)] SoundGeneratorCallback callBack);
    }
}
