﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Windows;

namespace Squiggle.Utilities
{
    public static class Async
    {
        public static void Invoke(Action action)
        {
            Invoke(action, () => { });
        }

        public static void Invoke(Action action, Action onComplete)
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    Trace.WriteLine("Exception occured in async operation: " + ex.ToString());
                }
                Application.Current.Dispatcher.Invoke(onComplete, null);
            });
        }
    }
}