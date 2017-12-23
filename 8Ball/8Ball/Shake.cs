using BL;
using Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace _8Ball
{
    class Shake : ICommand
    {
        public event EventHandler CanExecuteChanged;
        
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (String.IsNullOrWhiteSpace(_8Ball.Properties.Settings.Default.DataConnection))
            {
                // Logger.Log.Error("Data connection is not found");
                throw new ArgumentNullException("DataConnection is not found");

            }

            Config config = new Config();
            config.DataPath = Path.GetFullPath(_8Ball.Properties.Settings.Default.DataConnection);
            config.DataReaderAssembly = Path.GetFullPath(_8Ball.Properties.Settings.Default.DALAssembly);
            config.DataReader = Path.GetFullPath(_8Ball.Properties.Settings.Default.ReaderType);

            FileProcessing fp = new FileProcessing(config);

            var vm = parameter as ViewModel;
            if (vm == null)
            {
                Logger.Log.Error("ViewModel can't be null");
                throw new ArgumentNullException("Модель представления не можеть быть null");
            }
            vm.answer = fp.Shake(fp.Answers);          
        }
    }
}
