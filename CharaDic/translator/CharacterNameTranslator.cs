using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharaDic.translator
{
    class CharacterNameTranslator
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        public CharacterNameTranslator()
        {
            string dbPath = System.IO.Directory.GetCurrentDirectory() + @"\db.txt";
            using (System.IO.StreamReader file = new System.IO.StreamReader(dbPath))
            {
                string read;
                while(!file.EndOfStream)
                {
                    read = file.ReadLine();
                    string[] tokens = read.Split(new char[1] { ' ' }, 2);
                    if (tokens.Length == 2)
                    {
                        dic[tokens[0]] = tokens[1];
                        Console.WriteLine(tokens[0] + " " + tokens[1]);
                    }
                }
                file.Close();
            }
        }

        private Boolean isValid(string token)
        {
            if (token == " ")
                return false;
            return dic.ContainsKey(token);
        }

        public string translate(string name)
        {
            string result = "";
            string curr = "";

            name = name.ToLower();

            for(int i=0; i < name.Length; i++)
            {
                if ('a' <= name[i] && name[i] <= 'z')
                    curr += name[i];
                else if (name[i] == ' ')
                {
                    result += ' ';
                    curr = "";
                    continue;
                }
                 
                if (i + 1 < name.Length &&
                    !isValid(name[i+1].ToString()) &&
                    isValid(curr + name[i+1]))
                {
                    if (i + 3 < name.Length && name[i + 1] == 't' && name[i+2] == 's' && name[i+3] == 'u')
                    {

                    }
                    else if (i + 2 >= name.Length || !isValid(name[i + 2].ToString()))
                        continue;
                }
                if (dic.ContainsKey(curr))
                {
                    result += dic[curr];
                    curr = "";
                }
            }
            return result;
        }
    }
}
