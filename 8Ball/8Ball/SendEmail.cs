using BL;
using Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace _8Ball
{
    class SendEmail : ICommand
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
                // Logger.Log.Error("Data connection is not found!");
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
                // Logger.Log.Error("ViewModel can't be null");
                throw new ArgumentNullException("Модель представления не можеть быть null");
            };
            if (String.IsNullOrWhiteSpace(vm.email))
            {
                //  Logger.Log.Error("Email is empty");
                vm.email = "Email is empty";

            }
            if (String.IsNullOrWhiteSpace(vm.answer))
            {
                // Logger.Log.Error("Shake it first");
                vm.email = "Shake it first";
            }
            bool wrongEmail;
            try
            {

                MailAddress from = new MailAddress(vm.email);
                wrongEmail = true;

            }
            catch
            {
                // Logger.Log.Error("Email is wrong");
                wrongEmail = false;

            }
            if (wrongEmail == true)
            {
                fp.SendEmail(vm.email, vm.answer);
                vm.email = null;

            }
            else vm.email = "wrong email";

             
            
        }
    }
}
