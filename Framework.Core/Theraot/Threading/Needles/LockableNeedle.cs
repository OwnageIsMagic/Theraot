#if FAT

using System;
using System.Collections.Generic;
using System.Threading;

namespace Theraot.Threading.Needles
{
    public sealed class LockableNeedle<T> : Needle<T>
    {
        private readonly LockableContext _context;
        private readonly NeedleLock<Thread> _needleLock;

        public LockableNeedle(T value, LockableContext context)
            : base(value)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _needleLock = new NeedleLock<Thread>(_context.Context);
        }

        public override T Value
        {
            get => base.Value;

            set
            {
                if (_context.TryGetSlot(out var slot))
                {
                    CaptureAndWait(slot);
                    ThreadingHelper.MemoryBarrier();
                    base.Value = value;
                    ThreadingHelper.MemoryBarrier();
                }
                else
                {
                    throw new InvalidOperationException("The current thread has not entered the LockableContext of this LockableNeedle.");
                }
            }
        }

        public void CaptureAndWait()
        {
            if (!_context.TryGetSlot(out var slot))
            {
                throw new InvalidOperationException("The current thread has not entered the LockableContext of this LockableNeedle.");
            }
            CaptureAndWait(slot);
        }

        public bool TryUpdate(T newValue, T expectedValue)
        {
            CaptureAndWait();
            if (!EqualityComparer<T>.Default.Equals(base.Value, expectedValue))
            {
                return false;
            }
            ThreadingHelper.MemoryBarrier();
            base.Value = newValue;
            ThreadingHelper.MemoryBarrier();
            return true;
        }

        public bool TryUpdate(T newValue, T expectedValue, IEqualityComparer<T> comparer)
        {
            CaptureAndWait();
            if (!comparer.Equals(base.Value, expectedValue))
            {
                return false;
            }
            ThreadingHelper.MemoryBarrier();
            base.Value = newValue;
            ThreadingHelper.MemoryBarrier();
            return true;
        }

        public T Update(Func<T, T> updateValueFactory)
        {
            if (updateValueFactory == null)
            {
                throw new ArgumentNullException(nameof(updateValueFactory));
            }
            CaptureAndWait();
            var result = updateValueFactory(base.Value);
            base.Value = result;
            ThreadingHelper.MemoryBarrier();
            return result;
        }

        private void Capture(LockableSlot slot)
        {
            var lockSlot = slot.LockSlot;
            if (lockSlot == null)
            {
                throw new InvalidOperationException("The current thread has not entered the LockableContext of this LockableNeedle.");
            }
            _needleLock.Capture(lockSlot);
            slot.Add(_needleLock);
        }

        private void CaptureAndWait(LockableSlot slot)
        {
            Capture(slot);
            var thread = Thread.CurrentThread;
            // The reason while I cannot make an smarter wait function:
            // If another thread changed _needleLock.Value after the check but before the starting to wait, the wait will not finish.
            ThreadingHelper.SpinWaitUntil(() => _needleLock.Value == thread);
        }
    }
}

#endif