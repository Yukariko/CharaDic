using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CharaDic.file;
using CharaDic.net;
using CharaDic.translator;

namespace CharaDic
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        CharacterNameTranslator cntranslator = new CharacterNameTranslator();

        public MainWindow()
        {
            InitializeComponent();
            printChars();
        }


        public async void printChars()
        {
            var chars = await VNDBParser.GetCharacters(13188);
            Console.WriteLine(chars.Count);
            translateAll(chars);
            foreach (var character in chars)
            {
                Console.WriteLine(character.japName + " " + character.engName + " " + character.korName);
            }
            CharacterDictionary dic = new CharacterDictionary(@"C:\Program Files (x86)\ChangShinSoft\ezTrans XP\Ehnd\UserDict_@Hdor#name.txt", "Ehnd");
            dic.push(chars);
        }

        public void translateAll(List<VNCharacter> chars)
        {
            foreach (var character in chars)
                character.korName = cntranslator.translate(character.engName);
        }
    }
}
