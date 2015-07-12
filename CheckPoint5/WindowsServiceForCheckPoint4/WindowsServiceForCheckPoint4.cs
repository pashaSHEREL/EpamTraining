using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Security;
using System.ServiceProcess;
using System.Threading.Tasks;
using Bll;

namespace WindowsServiceForCheckPoint4
{
    public partial class WindowsServiceForCheckPoint4 : ServiceBase
    {
        private static readonly string _directoryForRead = ConfigurationManager.AppSettings["directoryForRead"];
        private static readonly string _directoryMoveTo = ConfigurationManager.AppSettings["directoryMoveTo"];
        private readonly FileSystemWatcher _watcher = new FileSystemWatcher {Path = _directoryForRead, Filter = "*.csv"};

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
                IDataBaseWorker worker = new DataBaseWorker(_directoryForRead, _directoryMoveTo);
                Task.Factory.StartNew(worker.AddAllInDataBase, e.FullPath);
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