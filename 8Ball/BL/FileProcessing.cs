using Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class FileProcessing
    {
        public List<string> Answers { get; set; }
        public string answer { get; set; }
        public FileProcessing(Config config)
        {
            if (!File.Exists(config.DataReaderAssembly))
            {
                Logger.Log.Error("Assembly isn't exist");
                throw new ArgumentException("Assembly isn't exist");

            }

            if (config == null)
            {
                Logger.Log.Error("Config is null");
                throw new ArgumentNullException("Config is null");

            }
            Assembly assembly = null;
            Type foundClass = null;
            try
            {
                assembly = Assembly.LoadFile(config.DataReaderAssembly);
                Logger.Log.Info("assembly loaded");
                foundClass = assembly.GetExportedTypes().FirstOrDefault(x => x.GetInterface("IDAL") != null);
                Logger.Log.Info("reader class loaded");

            }
            catch (Exception ex)
            {
                Logger.Log.Error("Can't create reader", ex);
                throw new InvalidOperationException("Can't get reader");
            }

            if (foundClass == null)
            {
               Logger.Log.Error("Can't find class with IDAL interface");
                throw new InvalidOperationException("Can't find class with IDAL interface");
            }
            IDAL rd = Activator.CreateInstance(assembly.FullName, foundClass.FullName).Unwrap() as IDAL;
            if (rd == null)
            {
                Logger.Log.Error("Can't create instance");
                throw new InvalidOperationException("Can't create instance");
            }
            if (!File.Exists(config.DataPath))
            {
                Logger.Log.Error("DataPath isn't exist ");
                throw new FileNotFoundException("DataPath isn't exist ");

            }
            Answers = rd.DataReader(config.DataPath);

        }
        public string Shake(List<string> listOfAnswers)
        {
            Random rnd = new Random();
            return listOfAnswers[rnd.Next(listOfAnswers.Count)];
        }
        public void SendEmail(string email, string answer)
        {
            try
            {
                
                MailAddress from = new MailAddress(Properties.Settings.Default.login, "Magic 8 Ball");
                 MailAddress to = new MailAddress(email);
                 MailMessage m = new MailMessage(from, to);
                 m.Subject = "Fortunetelling";
                 m.Body = answer;
                 SmtpClient smtp = new SmtpClient(Properties.Settings.Default.smtp,Properties.Settings.Default.port);
                 smtp.Credentials = new NetworkCredential(Properties.Settings.Default.login, Properties.Settings.Default.password);
                 smtp.EnableSsl = true;
                 smtp.Send(m);

            }
            catch(Exception ex)
            {
                Logger.Log.Error("Message sending error");
                throw new InvalidDataException("Message sending error", ex);
            }
            
          
            
           
        }
    }
}
