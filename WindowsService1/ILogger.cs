using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace WindowsService1
{

   public interface ILogger 
    {
        void LogWarningMessage(string message);
        void LogInfoMessage(string message);
        void LogErrorMessage(string message);
        void LogSuccessMessage(string message);

    }
}
