using System;
using Microsoft.Win32.SafeHandles;

namespace Drapp.Native
{
    internal class DrumMathSafeHandle : SafeHandleMinusOneIsInvalid
    {
        internal DrumMathSafeHandle() : base(true) {}

        public IntPtr Ptr => handle;

        protected override bool ReleaseHandle()
        {
            DrumMathWrapper.DisposeDrumMathClass(handle);
            return true;
        }
    }
}