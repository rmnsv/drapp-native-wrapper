using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace DrappNative
{
    public class SoundMixer : IDisposable
    {
        private readonly SoundMixerSafeHandle _handle;
        private readonly List<IntPtr> _ptrs;

        public SoundMixer()
        {
            _handle = SoundMixerWrapper.CreateSoundMixerClass();
            _ptrs = new List<IntPtr>();
        }

        public byte[] TextMix2SinWaves(int samplesCount1, double frequencyOfTone1, float gain1, int samplesCount2, double frequencyOfTone2, float gain2)
        {
            byte[] byteArray = null;

            SoundMixerWrapper.SoundMixerCallback callback = (wave, size) =>
            {
                byteArray = new byte[size];
                Marshal.Copy(wave, byteArray, 0, size);
            };

            SoundMixerWrapper.TextMix2SinWaves(_handle, samplesCount1, frequencyOfTone1, gain1, samplesCount2, frequencyOfTone2, gain2, callback);
            return byteArray;
        }

        public byte[] TextMixSinWaveWithSoundSample(int samples1, double frequencyOfTone1, float gain1, byte[] wave2, int size2, float gain2)
        {
            byte[] byteArray = null;

            IntPtr arrayPtr2 = Marshal.AllocHGlobal(size2);
            Marshal.Copy(wave2, 0, arrayPtr2, size2);

            _ptrs.Add(arrayPtr2);

            SoundMixerWrapper.SoundMixerCallback callback = (wave, size) =>
            {
                byteArray = new byte[size];
                Marshal.Copy(wave, byteArray, 0, size);
            };

            SoundMixerWrapper.TextMixSinWaveWithSoundSample(_handle, samples1, frequencyOfTone1, gain1, arrayPtr2, size2, gain2, callback);
            return byteArray;
        }

        public byte[] TextMix2SoundSamples(byte[] wave1, int size1, float gain1, byte[] wave2, int size2, float gain2)
        {
            byte[] byteArray = null;

            IntPtr arrayPtr1 = Marshal.AllocHGlobal(size1);
            Marshal.Copy(wave1, 0, arrayPtr1, size1);
            _ptrs.Add(arrayPtr1);

            IntPtr arrayPtr2 = Marshal.AllocHGlobal(size2);
            Marshal.Copy(wave2, 0, arrayPtr2, size2);
            _ptrs.Add(arrayPtr2);

            SoundMixerWrapper.SoundMixerCallback callback = (wave, size) =>
            {
                byteArray = new byte[size];
                Marshal.Copy(wave, byteArray, 0, size);
            };

            SoundMixerWrapper.TextMix2SoundSamples(_handle, arrayPtr1, size1, gain1, arrayPtr2, size2, gain2, callback);
            return byteArray;
        }

        protected virtual void Dispose(bool disposing)
        {
            foreach (IntPtr ptrr in _ptrs)
            {
                Marshal.FreeHGlobal(ptrr);
            }

            if (_handle != null && !_handle.IsInvalid)
                _handle.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
