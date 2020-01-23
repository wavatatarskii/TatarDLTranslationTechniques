using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO; //для файлов
using System.Reflection; //для получения пути

namespace Translator
{
    public partial class Form1 : Form
    {
        string path;//путь к дирректории
        string[] Mass;//массив из файла
        int length;//тут хранимся вся длина массива
        int currentstring=71139;//текущая позиция в массиве 
        string m = "";//буфер обмена с татарским текстом
        public Form1()
        {
            InitializeComponent();

            //получение директории где находится exe
            path = (Assembly.GetExecutingAssembly().Location);              //получение пути файла
            {//ограничение зоны видимости
                string[] s2 = path.Split('\\');
                path = path.TrimEnd(s2[s2.Length - 1].ToCharArray());       //удаление имени и расширения файла из пути
            }
            //чтение из файла
            Mass = File.ReadAllLines($"{path}\\input.txt", System.Text.Encoding.Unicode);//у нас Unicode , а не ACSII
            length = Mass.Length;//кол-во строк в массиве
            label2.Text = length.ToString();
        }

        private void Button1_Click(object sender, EventArgs e)
        {   //тут вставка в буфер обмена
            
            if (currentstring == length) { Application.Exit(); }//чтобы вышел, когда всё прочитает
            int charamount=0;
            int QuantityOfStrings = 0;//кол-во строк в массиве для буфера обмена
            //currentstring глобальная
            //length = Mass.Length глобальная
            int previouspisition = currentstring;
            
            for (; currentstring < length; currentstring++)// считаем кол-во строк нужных
            {
                QuantityOfStrings++;
                charamount += Mass[currentstring].Length;
                if (charamount >= 800||(currentstring-previouspisition)>6)
                {
                    charamount -= Mass[currentstring].Length;
                    QuantityOfStrings--;
                    break;
                }
            }
            //тут заносим в массив строк нужные строки
            m="";//тут будут хранится все нужные строки 

            for (int i = QuantityOfStrings; i > 0; i--)
            {
                m = m +'\n'+ Mass[previouspisition + (QuantityOfStrings - i)];//правильный порядок
            }

            Clipboard.Clear();
            Clipboard.SetText(m);
            button1.Enabled = false;
            button2.Enabled = true;
            label1.Text = currentstring.ToString();
        }

        private void Button2_Click(object sender, EventArgs e)
        {//тут копирование из буфера обмена в файл
            string a = Clipboard.GetText();
            char[] ms = a.ToCharArray();
            int erroror = 0;
            //for (int i = 0; i < ms.Length && erroror == 0; i++)
            //{
            //    if (ms[i] == 'Ә' || ms[i] == 'ә' || ms[i] == 'Ө' || ms[i] == 'ө'
            //        || ms[i] == 'Ү' || ms[i] == 'ү' || ms[i] == 'Җ' || ms[i] == 'җ'
            //        || ms[i] == 'Ң' || ms[i] == 'ң' || ms[i] == 'Һ' || ms[i] == 'һ') erroror++;
            //}
            if (erroror > 0) Clipboard.SetText(m);
            else
            {
                //////////////////добавляем после .!? символ l///////////////////////////////////////
                {//ограничение зоны видимости

                    for (int j = a.Length - 1; j >= 0; j--)
                    {
                        //проверка для троеточий (3 это когда один символ и ... после него) вдруг такое может быть
                        //первое условие для одинарных символов, второе для ...
                        if (j == a.Length - 1||j==2||j==1||j==0)//если последний элемент вопрос
                        {
                            if (ms[j] == '?') a = a.Insert(j + 1, "l");
                        }
                        else
                        {
                            if (((ms[j] == '.' || ms[j] == '!' || ms[j] == '?') && (ms[j - 1] != '.') && (ms[j + 1] != '.')) || (ms[j] == '.' && ms[j - 1] == '.' && ms[j - 2] == '.')) a = a.Insert(j + 1, "l");//добавление символа l после .!?
                        }

                    }
                }
                //запись в файл
                string[] mass2 = a.Split('l');//строки для предложений из абзаца

                for (int j = 0; j < mass2.Length; j++)
                {
                    Console.WriteLine("Идет запись " + (j + 1) + " из " + mass2.Length);

                    char[] ms2 = mass2[j].ToCharArray();
                    if (ms2.Length != 0)
                    {
                        if (ms2[ms2.Length - 1] != '?' && (ms2[ms2.Length - 1] == '.' || ms2[ms2.Length - 1] == '!'))//отсеиваем предложения с ? и предложения без знака в конце
                        {

                            using (StreamWriter SW = new StreamWriter(new FileStream($"{path}\\ru.txt", FileMode.Append, FileAccess.Write), Encoding.Unicode))
                            {

                                SW.WriteLine(mass2[j]);
                                SW.Flush();
                            }

                        }
                    }
                    //Console.Clear();
                }


                button1.Enabled = true;
                button2.Enabled = false;
            }
        }//////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void Button3_Click(object sender, EventArgs e)
        {
            int quantityofbags = 0;
            for (int i = 0; i < length; i++)
            {
                if (Mass[i].Length >= 800) quantityofbags++;
                
            }
            if (quantityofbags == 0) {
                button1.Enabled = true;
                button3.Enabled = false;
            }
            else MessageBox.Show("ТУТ ГДЕ ТО ЕСТЬ БОЛЕЕ 800 символов строка");

        }
    }
}
