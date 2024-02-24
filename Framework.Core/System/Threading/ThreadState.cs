﻿#if LESSTHAN_NETSTANDARD13

#pragma warning disable CA1008 // Enums should have zero value
#pragma warning disable S2346 // Flags enumerations zero-value members should be named "None".

namespace System.Threading
{
    [Flags]
    [Runtime.InteropServices.ComVisible(true)]
    public enum ThreadState
    {
        Running = 0,
        StopRequested = 1,
        SuspendRequested = 2,
        Background = 4,
        Unstarted = 8,
        Stopped = 16,
        WaitSleepJoin = 32,
        Suspended = 64,
        AbortRequested = 128,
        Aborted = 256
    }
}

#endif