using Microsoft.Win32.SafeHandles;
using System;

namespace DrappNative
{
    internal class SoundMixerSafeHandle : SafeHandleMinusOneIsInvalid
    {
        internal SoundMixerSafeHandle() : base(true) { }

        public IntPtr Ptr => handle;

        protected override bool ReleaseHandle()
        {
            SoundMixerWrapper.DisposeSoundMixerClass(handle);
            return true;
        }
    }
}
