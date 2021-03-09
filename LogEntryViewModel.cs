using System;
using System.Collections.Generic;
using System.Linq;

namespace LogViewExample
{
    public class LogEntryViewModel : PropertyChangedBase
    {
        public LogEntryViewModel(LogEntry logEntry)
        {
            _logEntry = logEntry;
        }

        public DateTime DateTime 
        {
            get => _logEntry.DateTime;
            set => _logEntry.DateTime = value;
        }

        public int Index
        {
            get => _logEntry.Index;
            set => _logEntry.Index = value;
        }

        public string Message
        {
            get => _logEntry.Message;
            set => _logEntry.Message = value;
        }

        public List<LogEntryViewModel> Contents
        {
            get => _logEntry.Contents.Select(c => new LogEntryViewModel(c)).ToList();
            set => _logEntry.Contents = value.Select(c => c._logEntry).ToList();
        }

        protected LogEntry _logEntry;
    }
}
