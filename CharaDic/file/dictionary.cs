using CharaDic.net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharaDic.file
{
    

    public class CharacterDictionary
    {
        public string path;
        public DictionaryType dicType;
        public CharacterDictionary(string path, string type = "")
        {
            this.path = path;
            setType(type);
            
        }
    
        public void setType(string type)
        {
            if (type == "Aral")
                dicType = (DictionaryType)new AralTransDic();
            else if (type == "Ehnd")
                dicType = (DictionaryType)new EhndDic();
        }
        
        public void push(List<VNCharacter> chars)
        {
            string[] lines;

            if (!File.Exists(path + ".bak"))
            {
                lines = File.ReadAllLines(path);
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(path + ".bak"))
                {
                    foreach (var line in lines)
                    {
                        file.WriteLine(line);
                    }

                    file.Close();
                }
            }

            else
            {
                lines = File.ReadAllLines(path + ".bak");
            }

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path))
            {
       
                foreach(var character in chars)
                {
                    file.WriteLine(dicType.transform(character));
                }

                foreach(var line in lines)
                {
                    file.WriteLine(line);
                }

                file.Close();
            }
        }
    }
}
