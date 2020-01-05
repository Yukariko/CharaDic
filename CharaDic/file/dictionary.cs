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

            if (File.Exists(path))
                File.Copy(path, path + ".bak", false);

            lines = File.ReadAllLines(path + ".bak");

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
        public void restore()
        {
            File.Copy(path + ".bak", path, true);
        }
    }
}
