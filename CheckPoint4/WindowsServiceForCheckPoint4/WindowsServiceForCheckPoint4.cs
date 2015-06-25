using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using Bll;

namespace WindowsServiceForCheckPoint4
{
    public partial class WindowsServiceForCheckPoint4 : ServiceBase
    {
        private readonly FileSystemWatcher _watcher = new FileSystemWatcher {Path = @"d:\test1\", Filter = "*.csv"};

        public WindowsServiceForCheckPoint4()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _watcher.Created += CreatedEventHandler;
            _watcher.EnableRaisingEvents = true;
        }

        protected void CreatedEventHandler(object source, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Created)
            {
                Bll.IDataBaseWorker worker = new DataBaseWorker(_watcher.Path, @"d:\test2\");
                worker.AddAllInDataBase();
            }
        }

        protected override void OnStop()
        {
            _watcher.Created -= CreatedEventHandler;
            _watcher.EnableRaisingEvents = false;
        }

        private void eventLog1_EntryWritten(object sender, EntryWrittenEventArgs e)
        {
        }
    }
}