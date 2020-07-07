using Bit.ViewModel;
using System;
using System.Collections.Generic;

namespace CrmSolution.Client.MobileApp.Impl
{
    public class CrmSolutionExceptionHandler : BitExceptionHandler
    {
        public override void OnExceptionReceived(Exception exp, IDictionary<string, string> properties = null)
        {
#if DEBUG

            System.Diagnostics.Debugger.Break();

#endif

            base.OnExceptionReceived(exp, properties);
        }
    }
}
