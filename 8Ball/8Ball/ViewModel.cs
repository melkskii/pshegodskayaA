using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BL;
using Contract;
using System.Reflection;
using System.IO;
using System.Net.Mail;
using System.Net;

namespace _8Ball
{
    class ViewModel:INotifyPropertyChanged
    {
        private string _email;
        public string description { get; set; }
        public string email
        {
            get { return _email; }
            set
            {
                _email = value;
                DoPropertyChanged("email");
            }
        }
        private string _answer;
        public ICommand Shake { get; set; }
        public ICommand SendEmail { get; set; }
        public string answer
        {
            get { return _answer; }
            set
            {
                _answer = value;
                DoPropertyChanged("answer");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void DoPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        public ViewModel()
        {
            // Logger.InitLogger();
            _answer = "";
            description = "Concentrate on your yes-no question and shake the magic 8 ball. ";
            Shake = new Shake();
            SendEmail = new SendEmail();
        }
    }
}
