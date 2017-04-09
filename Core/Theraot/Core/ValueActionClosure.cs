﻿// <auto-generated />

using System;

namespace Theraot.Core
{
    [global::System.Diagnostics.DebuggerNonUserCode]
    public class ValueActionClosure    
    {
        private readonly Action _target;
        
        public ValueActionClosure(Action target)
        {
            _target = target ?? ActionHelper.GetNoopAction();
        }

        public void Invoke()
        {
            _target.Invoke();
        }

        public Action InvokeAction()
        {
            return new Action(Invoke);
        }
    }

    [global::System.Diagnostics.DebuggerNonUserCode]
    public class ValueActionClosure<T>    
    {
        private readonly Action<T> _target;
        private readonly T _obj;
        
        public ValueActionClosure(Action<T> target, T obj)
        {
            _target = target ?? ActionHelper.GetNoopAction<T>();
            _obj = obj;
        }

        public void Invoke()
        {
            _target.Invoke(_obj);
        }

        public Action InvokeAction()
        {
            return new Action(Invoke);
        }
    }

    [global::System.Diagnostics.DebuggerNonUserCode]
    public class ValueActionClosure<T1, T2>    
    {
        private readonly Action<T1, T2> _target;
        private readonly T1 _arg1;
        private readonly T2 _arg2;
        
        public ValueActionClosure(Action<T1, T2> target, T1 arg1, T2 arg2)
        {
            _target = target ?? ActionHelper.GetNoopAction<T1, T2>();
            _arg1 = arg1;
            _arg2 = arg2;
        }

        public void Invoke()
        {
            _target.Invoke(_arg1, _arg2);
        }

        public Action InvokeAction()
        {
            return new Action(Invoke);
        }
    }

    [global::System.Diagnostics.DebuggerNonUserCode]
    public class ValueActionClosure<T1, T2, T3>    
    {
        private readonly Action<T1, T2, T3> _target;
        private readonly T1 _arg1;
        private readonly T2 _arg2;
        private readonly T3 _arg3;
        
        public ValueActionClosure(Action<T1, T2, T3> target, T1 arg1, T2 arg2, T3 arg3)
        {
            _target = target ?? ActionHelper.GetNoopAction<T1, T2, T3>();
            _arg1 = arg1;
            _arg2 = arg2;
            _arg3 = arg3;
        }

        public void Invoke()
        {
            _target.Invoke(_arg1, _arg2, _arg3);
        }

        public Action InvokeAction()
        {
            return new Action(Invoke);
        }
    }

    [global::System.Diagnostics.DebuggerNonUserCode]
    public class ValueActionClosure<T1, T2, T3, T4>    
    {
        private readonly Action<T1, T2, T3, T4> _target;
        private readonly T1 _arg1;
        private readonly T2 _arg2;
        private readonly T3 _arg3;
        private readonly T4 _arg4;
        
        public ValueActionClosure(Action<T1, T2, T3, T4> target, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            _target = target ?? ActionHelper.GetNoopAction<T1, T2, T3, T4>();
            _arg1 = arg1;
            _arg2 = arg2;
            _arg3 = arg3;
            _arg4 = arg4;
        }

        public void Invoke()
        {
            _target.Invoke(_arg1, _arg2, _arg3, _arg4);
        }

        public Action InvokeAction()
        {
            return new Action(Invoke);
        }
    }

    [global::System.Diagnostics.DebuggerNonUserCode]
    public class ValueActionClosure<T1, T2, T3, T4, T5>    
    {
        private readonly Action<T1, T2, T3, T4, T5> _target;
        private readonly T1 _arg1;
        private readonly T2 _arg2;
        private readonly T3 _arg3;
        private readonly T4 _arg4;
        private readonly T5 _arg5;
        
        public ValueActionClosure(Action<T1, T2, T3, T4, T5> target, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            _target = target ?? ActionHelper.GetNoopAction<T1, T2, T3, T4, T5>();
            _arg1 = arg1;
            _arg2 = arg2;
            _arg3 = arg3;
            _arg4 = arg4;
            _arg5 = arg5;
        }

        public void Invoke()
        {
            _target.Invoke(_arg1, _arg2, _arg3, _arg4, _arg5);
        }

        public Action InvokeAction()
        {
            return new Action(Invoke);
        }
    }

    [global::System.Diagnostics.DebuggerNonUserCode]
    public class ValueActionClosure<T1, T2, T3, T4, T5, T6>    
    {
        private readonly Action<T1, T2, T3, T4, T5, T6> _target;
        private readonly T1 _arg1;
        private readonly T2 _arg2;
        private readonly T3 _arg3;
        private readonly T4 _arg4;
        private readonly T5 _arg5;
        private readonly T6 _arg6;
        
        public ValueActionClosure(Action<T1, T2, T3, T4, T5, T6> target, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            _target = target ?? ActionHelper.GetNoopAction<T1, T2, T3, T4, T5, T6>();
            _arg1 = arg1;
            _arg2 = arg2;
            _arg3 = arg3;
            _arg4 = arg4;
            _arg5 = arg5;
            _arg6 = arg6;
        }

        public void Invoke()
        {
            _target.Invoke(_arg1, _arg2, _arg3, _arg4, _arg5, _arg6);
        }

        public Action InvokeAction()
        {
            return new Action(Invoke);
        }
    }

    [global::System.Diagnostics.DebuggerNonUserCode]
    public class ValueActionClosure<T1, T2, T3, T4, T5, T6, T7>    
    {
        private readonly Action<T1, T2, T3, T4, T5, T6, T7> _target;
        private readonly T1 _arg1;
        private readonly T2 _arg2;
        private readonly T3 _arg3;
        private readonly T4 _arg4;
        private readonly T5 _arg5;
        private readonly T6 _arg6;
        private readonly T7 _arg7;
        
        public ValueActionClosure(Action<T1, T2, T3, T4, T5, T6, T7> target, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            _target = target ?? ActionHelper.GetNoopAction<T1, T2, T3, T4, T5, T6, T7>();
            _arg1 = arg1;
            _arg2 = arg2;
            _arg3 = arg3;
            _arg4 = arg4;
            _arg5 = arg5;
            _arg6 = arg6;
            _arg7 = arg7;
        }

        public void Invoke()
        {
            _target.Invoke(_arg1, _arg2, _arg3, _arg4, _arg5, _arg6, _arg7);
        }

        public Action InvokeAction()
        {
            return new Action(Invoke);
        }
    }

    [global::System.Diagnostics.DebuggerNonUserCode]
    public class ValueActionClosure<T1, T2, T3, T4, T5, T6, T7, T8>    
    {
        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8> _target;
        private readonly T1 _arg1;
        private readonly T2 _arg2;
        private readonly T3 _arg3;
        private readonly T4 _arg4;
        private readonly T5 _arg5;
        private readonly T6 _arg6;
        private readonly T7 _arg7;
        private readonly T8 _arg8;
        
        public ValueActionClosure(Action<T1, T2, T3, T4, T5, T6, T7, T8> target, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            _target = target ?? ActionHelper.GetNoopAction<T1, T2, T3, T4, T5, T6, T7, T8>();
            _arg1 = arg1;
            _arg2 = arg2;
            _arg3 = arg3;
            _arg4 = arg4;
            _arg5 = arg5;
            _arg6 = arg6;
            _arg7 = arg7;
            _arg8 = arg8;
        }

        public void Invoke()
        {
            _target.Invoke(_arg1, _arg2, _arg3, _arg4, _arg5, _arg6, _arg7, _arg8);
        }

        public Action InvokeAction()
        {
            return new Action(Invoke);
        }
    }

    [global::System.Diagnostics.DebuggerNonUserCode]
    public class ValueActionClosure<T1, T2, T3, T4, T5, T6, T7, T8, T9>    
    {
        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> _target;
        private readonly T1 _arg1;
        private readonly T2 _arg2;
        private readonly T3 _arg3;
        private readonly T4 _arg4;
        private readonly T5 _arg5;
        private readonly T6 _arg6;
        private readonly T7 _arg7;
        private readonly T8 _arg8;
        private readonly T9 _arg9;
        
        public ValueActionClosure(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> target, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            _target = target ?? ActionHelper.GetNoopAction<T1, T2, T3, T4, T5, T6, T7, T8, T9>();
            _arg1 = arg1;
            _arg2 = arg2;
            _arg3 = arg3;
            _arg4 = arg4;
            _arg5 = arg5;
            _arg6 = arg6;
            _arg7 = arg7;
            _arg8 = arg8;
            _arg9 = arg9;
        }

        public void Invoke()
        {
            _target.Invoke(_arg1, _arg2, _arg3, _arg4, _arg5, _arg6, _arg7, _arg8, _arg9);
        }

        public Action InvokeAction()
        {
            return new Action(Invoke);
        }
    }

    [global::System.Diagnostics.DebuggerNonUserCode]
    public class ValueActionClosure<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>    
    {
        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> _target;
        private readonly T1 _arg1;
        private readonly T2 _arg2;
        private readonly T3 _arg3;
        private readonly T4 _arg4;
        private readonly T5 _arg5;
        private readonly T6 _arg6;
        private readonly T7 _arg7;
        private readonly T8 _arg8;
        private readonly T9 _arg9;
        private readonly T10 _arg10;
        
        public ValueActionClosure(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> target, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            _target = target ?? ActionHelper.GetNoopAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>();
            _arg1 = arg1;
            _arg2 = arg2;
            _arg3 = arg3;
            _arg4 = arg4;
            _arg5 = arg5;
            _arg6 = arg6;
            _arg7 = arg7;
            _arg8 = arg8;
            _arg9 = arg9;
            _arg10 = arg10;
        }

        public void Invoke()
        {
            _target.Invoke(_arg1, _arg2, _arg3, _arg4, _arg5, _arg6, _arg7, _arg8, _arg9, _arg10);
        }

        public Action InvokeAction()
        {
            return new Action(Invoke);
        }
    }

    [global::System.Diagnostics.DebuggerNonUserCode]
    public class ValueActionClosure<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>    
    {
        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> _target;
        private readonly T1 _arg1;
        private readonly T2 _arg2;
        private readonly T3 _arg3;
        private readonly T4 _arg4;
        private readonly T5 _arg5;
        private readonly T6 _arg6;
        private readonly T7 _arg7;
        private readonly T8 _arg8;
        private readonly T9 _arg9;
        private readonly T10 _arg10;
        private readonly T11 _arg11;
        
        public ValueActionClosure(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> target, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            _target = target ?? ActionHelper.GetNoopAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>();
            _arg1 = arg1;
            _arg2 = arg2;
            _arg3 = arg3;
            _arg4 = arg4;
            _arg5 = arg5;
            _arg6 = arg6;
            _arg7 = arg7;
            _arg8 = arg8;
            _arg9 = arg9;
            _arg10 = arg10;
            _arg11 = arg11;
        }

        public void Invoke()
        {
            _target.Invoke(_arg1, _arg2, _arg3, _arg4, _arg5, _arg6, _arg7, _arg8, _arg9, _arg10, _arg11);
        }

        public Action InvokeAction()
        {
            return new Action(Invoke);
        }
    }

    [global::System.Diagnostics.DebuggerNonUserCode]
    public class ValueActionClosure<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>    
    {
        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> _target;
        private readonly T1 _arg1;
        private readonly T2 _arg2;
        private readonly T3 _arg3;
        private readonly T4 _arg4;
        private readonly T5 _arg5;
        private readonly T6 _arg6;
        private readonly T7 _arg7;
        private readonly T8 _arg8;
        private readonly T9 _arg9;
        private readonly T10 _arg10;
        private readonly T11 _arg11;
        private readonly T12 _arg12;
        
        public ValueActionClosure(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> target, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        {
            _target = target ?? ActionHelper.GetNoopAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>();
            _arg1 = arg1;
            _arg2 = arg2;
            _arg3 = arg3;
            _arg4 = arg4;
            _arg5 = arg5;
            _arg6 = arg6;
            _arg7 = arg7;
            _arg8 = arg8;
            _arg9 = arg9;
            _arg10 = arg10;
            _arg11 = arg11;
            _arg12 = arg12;
        }

        public void Invoke()
        {
            _target.Invoke(_arg1, _arg2, _arg3, _arg4, _arg5, _arg6, _arg7, _arg8, _arg9, _arg10, _arg11, _arg12);
        }

        public Action InvokeAction()
        {
            return new Action(Invoke);
        }
    }

    [global::System.Diagnostics.DebuggerNonUserCode]
    public class ValueActionClosure<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>    
    {
        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> _target;
        private readonly T1 _arg1;
        private readonly T2 _arg2;
        private readonly T3 _arg3;
        private readonly T4 _arg4;
        private readonly T5 _arg5;
        private readonly T6 _arg6;
        private readonly T7 _arg7;
        private readonly T8 _arg8;
        private readonly T9 _arg9;
        private readonly T10 _arg10;
        private readonly T11 _arg11;
        private readonly T12 _arg12;
        private readonly T13 _arg13;
        
        public ValueActionClosure(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> target, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            _target = target ?? ActionHelper.GetNoopAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();
            _arg1 = arg1;
            _arg2 = arg2;
            _arg3 = arg3;
            _arg4 = arg4;
            _arg5 = arg5;
            _arg6 = arg6;
            _arg7 = arg7;
            _arg8 = arg8;
            _arg9 = arg9;
            _arg10 = arg10;
            _arg11 = arg11;
            _arg12 = arg12;
            _arg13 = arg13;
        }

        public void Invoke()
        {
            _target.Invoke(_arg1, _arg2, _arg3, _arg4, _arg5, _arg6, _arg7, _arg8, _arg9, _arg10, _arg11, _arg12, _arg13);
        }

        public Action InvokeAction()
        {
            return new Action(Invoke);
        }
    }

    [global::System.Diagnostics.DebuggerNonUserCode]
    public class ValueActionClosure<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>    
    {
        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> _target;
        private readonly T1 _arg1;
        private readonly T2 _arg2;
        private readonly T3 _arg3;
        private readonly T4 _arg4;
        private readonly T5 _arg5;
        private readonly T6 _arg6;
        private readonly T7 _arg7;
        private readonly T8 _arg8;
        private readonly T9 _arg9;
        private readonly T10 _arg10;
        private readonly T11 _arg11;
        private readonly T12 _arg12;
        private readonly T13 _arg13;
        private readonly T14 _arg14;
        
        public ValueActionClosure(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> target, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            _target = target ?? ActionHelper.GetNoopAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
            _arg1 = arg1;
            _arg2 = arg2;
            _arg3 = arg3;
            _arg4 = arg4;
            _arg5 = arg5;
            _arg6 = arg6;
            _arg7 = arg7;
            _arg8 = arg8;
            _arg9 = arg9;
            _arg10 = arg10;
            _arg11 = arg11;
            _arg12 = arg12;
            _arg13 = arg13;
            _arg14 = arg14;
        }

        public void Invoke()
        {
            _target.Invoke(_arg1, _arg2, _arg3, _arg4, _arg5, _arg6, _arg7, _arg8, _arg9, _arg10, _arg11, _arg12, _arg13, _arg14);
        }

        public Action InvokeAction()
        {
            return new Action(Invoke);
        }
    }

    [global::System.Diagnostics.DebuggerNonUserCode]
    public class ValueActionClosure<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>    
    {
        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> _target;
        private readonly T1 _arg1;
        private readonly T2 _arg2;
        private readonly T3 _arg3;
        private readonly T4 _arg4;
        private readonly T5 _arg5;
        private readonly T6 _arg6;
        private readonly T7 _arg7;
        private readonly T8 _arg8;
        private readonly T9 _arg9;
        private readonly T10 _arg10;
        private readonly T11 _arg11;
        private readonly T12 _arg12;
        private readonly T13 _arg13;
        private readonly T14 _arg14;
        private readonly T15 _arg15;
        
        public ValueActionClosure(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> target, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            _target = target ?? ActionHelper.GetNoopAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();
            _arg1 = arg1;
            _arg2 = arg2;
            _arg3 = arg3;
            _arg4 = arg4;
            _arg5 = arg5;
            _arg6 = arg6;
            _arg7 = arg7;
            _arg8 = arg8;
            _arg9 = arg9;
            _arg10 = arg10;
            _arg11 = arg11;
            _arg12 = arg12;
            _arg13 = arg13;
            _arg14 = arg14;
            _arg15 = arg15;
        }

        public void Invoke()
        {
            _target.Invoke(_arg1, _arg2, _arg3, _arg4, _arg5, _arg6, _arg7, _arg8, _arg9, _arg10, _arg11, _arg12, _arg13, _arg14, _arg15);
        }

        public Action InvokeAction()
        {
            return new Action(Invoke);
        }
    }

    [global::System.Diagnostics.DebuggerNonUserCode]
    public class ValueActionClosure<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>    
    {
        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> _target;
        private readonly T1 _arg1;
        private readonly T2 _arg2;
        private readonly T3 _arg3;
        private readonly T4 _arg4;
        private readonly T5 _arg5;
        private readonly T6 _arg6;
        private readonly T7 _arg7;
        private readonly T8 _arg8;
        private readonly T9 _arg9;
        private readonly T10 _arg10;
        private readonly T11 _arg11;
        private readonly T12 _arg12;
        private readonly T13 _arg13;
        private readonly T14 _arg14;
        private readonly T15 _arg15;
        private readonly T16 _arg16;
        
        public ValueActionClosure(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> target, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
        {
            _target = target ?? ActionHelper.GetNoopAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();
            _arg1 = arg1;
            _arg2 = arg2;
            _arg3 = arg3;
            _arg4 = arg4;
            _arg5 = arg5;
            _arg6 = arg6;
            _arg7 = arg7;
            _arg8 = arg8;
            _arg9 = arg9;
            _arg10 = arg10;
            _arg11 = arg11;
            _arg12 = arg12;
            _arg13 = arg13;
            _arg14 = arg14;
            _arg15 = arg15;
            _arg16 = arg16;
        }

        public void Invoke()
        {
            _target.Invoke(_arg1, _arg2, _arg3, _arg4, _arg5, _arg6, _arg7, _arg8, _arg9, _arg10, _arg11, _arg12, _arg13, _arg14, _arg15, _arg16);
        }

        public Action InvokeAction()
        {
            return new Action(Invoke);
        }
    }
}
