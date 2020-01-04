using CharaDic.net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharaDic.file
{
    public interface DictionaryType
    {
        string transform(VNCharacter character);
    }

    public class AralTransDic : DictionaryType
    {
        public string transform(VNCharacter character)
        {
            return character.japName.Replace(" ", "") + "\t" + character.korName;
        }
    }

    public class EhndDic : DictionaryType
    {
        public string transform(VNCharacter character)
        {
            return character.japName.Replace(" ", "") + "\t" + character.korName + "#N	1	VHn	//인간";
        }
    }
}
