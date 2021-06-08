using System;
using Microsoft.Win32.SafeHandles;

namespace DrappNative
{
    internal class SoundProviderSafeHandle : SafeHandleMinusOneIsInvalid
    {
        internal SoundProviderSafeHandle() : base(true) { }

        public IntPtr Ptr => handle;

        protected override bool ReleaseHandle()
        {
            SoundProviderWrapper.DisposeSoundProviderClass(handle);
            return true;
        }
    }
}