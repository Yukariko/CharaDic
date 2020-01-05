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
        List<VNCharacter> chars = null;
        CharacterDictionary dic = null;

        public MainWindow()
        {
            InitializeComponent();
        }


        public async void getChars(string url)
        {
            chars = await VNDBParser.GetCharacters(url);
            Console.WriteLine(chars.Count);
            translateAll(chars);
            CharacterListBox.ItemsSource = chars;
        }

        public void translateAll(List<VNCharacter> chars)
        {
            foreach (var character in chars)
                character.korName = cntranslator.translate(character.engName);
        }

        private void FindGameBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                getChars(FindGameBox.Text);
            }
        }


        private void DictionaryPushBox_Click(object sender, RoutedEventArgs e)
        {
            //CharacterDictionary dic = new CharacterDictionary(@"C:\Program Files (x86)\ChangShinSoft\ezTrans XP\Ehnd\UserDict_@Hdor#name.txt", "Ehnd");
            if (DictionaryPathBox.Text == "" || chars == null)
                return;
            dic = new CharacterDictionary(DictionaryPathBox.Text, "Ehnd");
            dic.push(chars);
        }

        private void DictionaryRestoreBox_Click(object sender, RoutedEventArgs e)
        {
            if (dic == null)
                return;
            dic.restore();
        }
    }
}
