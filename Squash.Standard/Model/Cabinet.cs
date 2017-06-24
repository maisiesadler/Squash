using System.IO;

namespace Squash
{
    internal class Cabinet
    {
        private string _filePath;
        private int _currentRowIndex;
        private string[] _contents;

        internal Cabinet(string filePath)
        {
            _filePath = filePath;

            _currentRowIndex = 0;
            _contents = File.ReadAllLines(filePath);
        }

        public string GetCurrentLine()
        {
            if (_currentRowIndex > _contents.Length - 1)
                return null;
            return _contents[_currentRowIndex].Trim();
        }

        public string PeekNextLine()
        {
            var peekIndex = _currentRowIndex + 1;

            if (peekIndex > _contents.Length - 1)
                return null;

            while (string.IsNullOrWhiteSpace(_contents[peekIndex]))
            {
                peekIndex++;
                if (peekIndex > _contents.Length - 1)
                    return null;
            }

            return _contents[peekIndex].Trim();
        }

        public string GetNextLine()
        {
            _currentRowIndex++;

            if (_currentRowIndex > _contents.Length)
                return null;

            //ignore empty lines
            while (string.IsNullOrWhiteSpace(GetCurrentLine()))
            {
                _currentRowIndex++;

                if (_currentRowIndex > _contents.Length)
                    return null;
            }

            return GetCurrentLine();
        }
    }
}