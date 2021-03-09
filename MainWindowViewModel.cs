using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace LogViewExample
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            _random = new Random();
            _words = _testData.Split(' ').ToList();
            _maxword = _words.Count - 1;

            Enumerable.Range(0, 200000).ToList().ForEach(x => LogEntries.Add(GetRandomEntry()));

            _timer = new Timer(x => AddRandomEntry(), null, 1000, 10);
        }

        public ObservableCollection<LogEntryViewModel> LogEntries { get; } = new ObservableCollection<LogEntryViewModel>();

        private void AddRandomEntry()
        {
            Application.Current?.Dispatcher.BeginInvoke((Action)(() => LogEntries.Add(GetRandomEntry())));
        }

        private LogEntryViewModel GetRandomEntry()
        {
            LogEntryViewModel vm = new LogEntryViewModel(new LogEntry())
            {
                Index = _index++,
                DateTime = DateTime.Now,
                Message = GetRandomMessage(),
                Contents = new List<LogEntryViewModel>()
            };

            if (_random.Next(1, 10) == 1)
                vm.Contents = Enumerable.Range(5, _random.Next(5, 10)).Select(i => GetRandomEntry()).ToList();

            return vm;
        }

        private string GetRandomMessage()
        {
            return string.Join(" ", Enumerable.Range(5, _random.Next(10, 50)).Select(x => _words[_random.Next(0, _maxword)]));
        }

        private string _testData = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum";

        private List<string> _words;
        private int _maxword;
        private int _index;
        private Timer _timer;
        private Random _random;
    }
}
