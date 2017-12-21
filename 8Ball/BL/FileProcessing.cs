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
                throw new ArgumentException("Assembly isn't exist");
            //REVIEW: В каждой следующей строчке может быть исключение
            var assembly = Assembly.LoadFile(config.DataReaderAssembly);
            var foundClass = assembly.GetExportedTypes().FirstOrDefault(x => x.GetInterface("IDAL") != null);
            if (foundClass == null)
                throw new InvalidOperationException("Can't find class with IDAL interface");
            IDAL rd = Activator.CreateInstance(assembly.FullName, foundClass.FullName).Unwrap() as IDAL;
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
                //REVIEW: все адреса, url - в настройки
                MailAddress from = new MailAddress("awesome.magic@yandex.ru", "Magic 8 Ball");
                 MailAddress to = new MailAddress(email);
                 MailMessage m = new MailMessage(from, to);
                 m.Subject = "Fortunetelling";
                 m.Body = answer;
                 SmtpClient smtp = new SmtpClient("smtp.yandex.ru", 587);
                 smtp.Credentials = new NetworkCredential("awesome.magic@yandex.ru", "mypassword");
                 smtp.EnableSsl = true;
                 smtp.Send(m);

            }
            catch(Exception ex)
            {
                //REVIEW: Логировать исключение
                throw new InvalidDataException("Message sending error", ex);
            }
            
          
            
           
        }
    }
}
