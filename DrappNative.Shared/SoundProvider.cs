using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace DrappNative
{
    public class SoundProvider : IDisposable
    {
        private readonly SoundProviderSafeHandle _handle;
        private readonly List<IntPtr> _ptrs;

        public SoundProvider()
        {
            _handle = SoundProviderWrapper.CreateSoundProviderClass();
            _ptrs = new List<IntPtr>();
        }

        public int PutSound(byte[] wave, int size)
        {
            IntPtr arrayPtr = Marshal.AllocHGlobal(size);
            Marshal.Copy(wave, 0, arrayPtr, size);

            int id = SoundProviderWrapper.PutSound(_handle, arrayPtr, size);

            _ptrs.Add(arrayPtr);

            return id;
        }

        public byte[] GetSound(int id)
        {
            byte[] byteArray = null;

            SoundProviderWrapper.SoundProviderCallback callback = (wave, size) =>
            {
                byteArray = new byte[size];
                Marshal.Copy(wave, byteArray, 0, size);
            };

            SoundProviderWrapper.GetSound(_handle, id, callback);

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