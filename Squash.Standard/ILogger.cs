using System;
using System.Collections.Generic;
using System.Text;

namespace Squash
{
    public interface ILogger
    {
		void Debug(string message);
		void Info(string message);
        void Error(string message);
    }
}
