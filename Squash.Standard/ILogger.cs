using System;
using System.Collections.Generic;
using System.Text;

namespace Squash
{
    public interface ILogger
    {
        void Info(string message);
        void Error(string message);
    }
}
