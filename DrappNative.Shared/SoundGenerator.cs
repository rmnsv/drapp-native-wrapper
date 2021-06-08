using System;
using System.Runtime.InteropServices;

namespace DrappNative
{
    public class SoundGenerator : IDisposable
    {
        private readonly SoundGeneratorSafeHandle _handle;

        public SoundGenerator(int sampleRate)
        {
            _handle = SoundGeneratorWrapper.CreateSoundGeneratorClass(sampleRate);
        }

        public byte[] TestGetSineWave16BitPcm(int samples, double frequencyOfTone)
        {
            byte[] byteArray = null;

            SoundGeneratorWrapper.SoundGeneratorCallback callback = (wave, size) =>
            {
                byteArray = new byte[size];
                Marshal.Copy(wave, byteArray, 0, size);
            };

            SoundGeneratorWrapper.TestGetSineWave16BitPcm(_handle, samples, frequencyOfTone, callback);
            return byteArray;
        }

        protected virtual void Dispose(bool disposing)
        {
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
