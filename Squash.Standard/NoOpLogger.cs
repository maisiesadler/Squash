namespace Squash
{
    public class NoOpLogger : ILogger
    {
        public void Error(string message)
        {
		}

		public void Info(string message)
		{
		}

		public void Debug(string message)
		{
		}
    }
}