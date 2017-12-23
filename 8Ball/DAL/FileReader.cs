using Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL
{
    public class FileReader : IDAL
    {
        public List<string> DataReader(string XMLPath)
        {
            var path = Path.GetFullPath(XMLPath);
            if (!File.Exists(path))
            {
                Logger.Log.Error("XML file isn't exist");
                throw new FileNotFoundException("XML file isn't exist");
            }
            try
            {
                XDocument xmlDocument = XDocument.Load(path);
                List<string> content = new List<string>();
                foreach (var element in xmlDocument.Root.Elements())
                {
                    content.Add(element.Attributes().FirstOrDefault(x => x.Name == "answertext").Value);
                }
                return content;


            }
            catch (Exception ex)
            {
                Logger.Log.Error("XML file reading error");
                throw new InvalidDataException("XML file reading error", ex);
            }
        }
    }
}
