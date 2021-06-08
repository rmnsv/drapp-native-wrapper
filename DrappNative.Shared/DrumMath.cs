using System;

namespace Drapp.Native
{
    public class DrumMath : IDisposable
    {
        private readonly DrumMathSafeHandle _handle;

        public DrumMath()
        {
            _handle = DrumMathWrapper.CreateDrumMathClass();
        }

        public double BpmToMs(int bpm)
        {
            return DrumMathWrapper.DrumMathBpmToMs(_handle, bpm);
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