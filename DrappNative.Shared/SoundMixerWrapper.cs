using System;
using System.Runtime.InteropServices;

namespace DrappNative
{
    public static class SoundMixerWrapper
    {

#if DRAPP_ANDROID
        const string DllName = "libdrapp-native.so";
#else
        const string DllName = "__Internal";
#endif

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void SoundMixerCallback(IntPtr wave, int size);

        [DllImport(DllName, EntryPoint = nameof(CreateSoundMixerClass))]
        internal static extern SoundMixerSafeHandle CreateSoundMixerClass();

        [DllImport(DllName, EntryPoint = nameof(DisposeSoundMixerClass))]
        internal static extern void DisposeSoundMixerClass(IntPtr ptr);

        [DllImport(DllName, EntryPoint = nameof(TextMix2SinWaves))]
        internal static extern void TextMix2SinWaves(SoundMixerSafeHandle ptr, int samplesCount1, double frequencyOfTone1, float gain1, int samplesCount2, double frequencyOfTone2, float gain2, [MarshalAs(UnmanagedType.FunctionPtr)] SoundMixerCallback callBack);

        [DllImport(DllName, EntryPoint = nameof(TextMixSinWaveWithSoundSample))]
        internal static extern void TextMixSinWaveWithSoundSample(SoundMixerSafeHandle ptr, int samples1, double frequencyOfTone1, float gain1, IntPtr wave2, int size2, float gain2, [MarshalAs(UnmanagedType.FunctionPtr)] SoundMixerCallback callBack);

        [DllImport(DllName, EntryPoint = nameof(TextMix2SoundSamples))]
        internal static extern void TextMix2SoundSamples(SoundMixerSafeHandle ptr, IntPtr wave1, int size1, float gain1, IntPtr wave2, int size2, float gain2, [MarshalAs(UnmanagedType.FunctionPtr)] SoundMixerCallback callBack);
    }
}
