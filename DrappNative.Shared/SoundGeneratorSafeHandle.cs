using System;
using Microsoft.Win32.SafeHandles;

namespace DrappNative
{
    internal class SoundGeneratorSafeHandle : SafeHandleMinusOneIsInvalid
    {
        internal SoundGeneratorSafeHandle() : base(true) { }

        public IntPtr Ptr => handle;

        protected override bool ReleaseHandle()
        {
            SoundGeneratorWrapper.DisposeSoundGeneratorClass(handle);
            return true;
        }
    }
}
