using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace LibForCore.StopwatchHelp
{
    public class StopwatchHelper : IDisposable
    {
        private Stopwatch _stopwatch = new Stopwatch();
        private Action<TimeSpan> _callback;

        public StopwatchHelper()
        {
            _stopwatch.Start();
        }

        public StopwatchHelper(Action<TimeSpan> callback):this()
        {
            _callback = callback;
        }

        public void Dispose()
        {
            _stopwatch.Stop();
            if(_callback != null)
            {
                _callback(Result);
            }
        }

        public TimeSpan Result
        {
            get { return _stopwatch.Elapsed; }
        }
    }

    /*範例*/
    /*
        using (var tester = new StopwatchHelper(ts => log.Debug(ts.ToString())))
        {
            // 想測試執行時間的Code區段
        }

        // 當離開using statment時就會寫入log
    */
}
