using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharaDic.net
{
    public class VNCharacter
    {
        public string japName { get; set; }
        public string engName { get; set; }
        public string korName { get; set; }

        public VNCharacter(string japName, string engName)
        {
            this.japName = japName;
            this.engName = engName;
        }
    }
    class VNDBParser
    {
        static async public Task<List<VNCharacter>> GetCharacters(int vnId)
        {
            string url = "https://vndb.org/v" + vnId;
            //            await VNDB.sem.WaitAsync();
            try
            {
                var web = new HtmlAgilityPack.HtmlWeb();
                HtmlDocument doc = await web.LoadFromWebAsync(url);
                
                List<VNCharacter> chars = new List<VNCharacter>();

                var characterList = doc.DocumentNode.SelectNodes("//div[contains(@class,'charsum_bubble')]");
                foreach (var character in characterList)
                {
                    //int id = ExtractId(character.SelectSingleNode("./div[@class='name']/a").GetAttributeValue("href", ""));
                    string engName = character.SelectSingleNode("./div[@class='name']/a").InnerText;
                    string japName = character.SelectSingleNode("./div[@class='name']/a").GetAttributeValue("title", "");

                    chars.Add(new VNCharacter(japName, engName));
                }

                return chars;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
//            VNDB.sem.Release();
        }
    }
}
