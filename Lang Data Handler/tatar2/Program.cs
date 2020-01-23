using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace tatar2
{
    class Program
    {
        static void Main(string[] args)
        {
            //получение директории где находится exe
            string path= (Assembly.GetExecutingAssembly().Location);//получение пути файла
            
            {//ограничение зоны видимости
                string[] s2 = path.Split('\\');
                path = path.TrimEnd(s2[s2.Length - 1].ToCharArray());//удаление имени и расширения файла из пути
            }

            //считывание из файла

            //using (FileStream fstream = File.OpenRead($"{path}\\input.txt"))
            //{
            //    // преобразуем строку в байты
            //    byte[] array = new byte[fstream.Length];
            //    // считываем данные
            //    fstream.Read(array, 0, array.Length);
            //    // декодируем байты в строку
            //    string textFromFile = System.Text.Encoding.Unicode.GetString(array);//у нас Unicode , а не ACSII
            //    Console.WriteLine($"Текст из файла: {textFromFile}");

            //}

            //либо так cчитываем
            string[] Mass = File.ReadAllLines($"{path}\\input.txt", System.Text.Encoding.Unicode);//у нас Unicode , а не ACSII

            //Цикл для не использования вопросительных предложений

            for (int i = 0; i < Mass.Length; i++)               //цикл для массива абзацей
            {
                                                                //поиск вхождений l (английская)
                                                                //(в татарских текстах нет английских символов,но вдруг=> удаляем)
                ////////////////////удаление l из текста//////////////////////////////////////////////
                {//ограничение зоны видимости
                    char[] ms = Mass[i].ToCharArray();
                    for (int j = Mass[i].Length-1; j>=0 ; j--)
                    {
                        if (ms[j] == 'l') Mass[i]=Mass[i].Remove(j, 1);//удаление символа l
                    }
                }
                ////////////////////добавляем после .!? символ l///////////////////////////////////////
                {//ограничение зоны видимости
                    char[] ms = Mass[i].ToCharArray();          //создали заново тк это уже измененная строка
                    //обход ошибок датасета
                    for (int j = 0; j < Mass[i].Length; j++) if (ms[0] == '.' && ms[1] == '.' && ms[2] == '.')Mass[i]=Mass[i].Remove(0, 3);
                    ms = Mass[i].ToCharArray();          //создали заново тк это уже измененная строка
                    //

                    for (int j = Mass[i].Length - 1; j >= 0; j--)
                    {

                        //проверка для троеточий (3 это когда один символ и ... после него) вдруг такое может быть
                        //первое условие для одинарных символов, второе для ...
                        if (j == Mass[i].Length - 1||j==0||j==1||j==2)
                        {
                            if (ms[j] == '?') Mass[i] = Mass[i].Insert(j + 1, "l");
                        }
                        else
                        {
                            if (((ms[j] == '.' || ms[j] == '!' || ms[j] == '?') && (ms[j - 1] != '.') && (ms[j + 1] != '.')) || (ms[j] == '.' && ms[j - 1] == '.' && ms[j - 2] == '.')) Mass[i] = Mass[i].Insert(j + 1, "l");//добавление символа l после .!?
                        }
                        
                    }
                }


                string[] mass2 = Mass[i].Split('l');            //строки для предложений из абзаца
                for (int j = 0; j < mass2.Length; j++)
                {
                    Console.WriteLine("Идет запись "+(j+1)+" из "+ mass2.Length);
                    
                    char[] ms2 = mass2[j].ToCharArray();
                    if (ms2.Length != 0)
                    {
                        if (ms2[ms2.Length - 1] != '?'&& (ms2[ms2.Length - 1] == '.'|| ms2[ms2.Length - 1] == '!'))//отсеиваем предложения с ? и предложения без знака в конце
                        {
                            if (mass2[j].Length < 800)
                            {
                                using (StreamWriter SW = new StreamWriter(new FileStream($"{path}\\output.txt", FileMode.Append, FileAccess.Write), Encoding.Unicode))
                                {

                                    SW.WriteLine(mass2[j]);
                                    SW.Flush();
                                }
                            }
                        }
                    }
                    //Console.Clear();
                }//тут конец строк из абзаца конкретного
                
            }//тут конец абзацей
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //тут удаляем дубликаты строк и удаляем строки с один словом
            Mass = File.ReadAllLines($"{path}\\output.txt", System.Text.Encoding.Unicode);//у нас Unicode , а не ACSII
            //удаление пробела из начала предложения, если есть
            for (int i = 0; i < Mass.Length; i++)
            {
                char[] charMass34 = Mass[i].ToCharArray();
                if (charMass34[0] == ' ') Mass[i] = Mass[i].Remove(0, 1);
            }
            //удаление дубликатов
            int dubl = 0;
            for (int i = 0;i<Mass.Length ; i++)
            {
                for (int j = i + 1; j < Mass.Length; j++)
                {
                    if (Mass[j] != "0" && Mass[i] == Mass[j])
                    {
                        Mass[j] = "0";
                        dubl++;
                    }
                }
            }
            //удаление однословных предложений или слово сочетаний возможных типо set of в английском, тк на них переводчик выдает ещё мусор
            int odnosl = 0;
            for(int i = 0; i < Mass.Length; i++)
            {
                int QuantityOfSpace = 0;
                char[] charMass33 = Mass[i].ToArray();
                for (int j = 0; j<Mass[i].Length; j++) if (charMass33[j] == ' ') QuantityOfSpace++;
                if (QuantityOfSpace <= 1)
                {
                    Mass[i] = "0";
                    odnosl++;
                }
            }
            //удаление предложений с двоеточиями или с ""
            int dvoetoc = 0;
            for(int i = 0; i < Mass.Length; i++)
            {
                char[] charMass35 = Mass[i].ToCharArray();
                for (int j = 0; j < Mass[i].Length && Mass[i] != "0"; j++) if (charMass35[j] == ':'|| charMass35[j] == '"')
                    {
                        Mass[i] = "0";
                        dvoetoc++;
                    }
            }
            
            //запись в правильный файл
            int errors = 0;
            for (int i = 0; i < Mass.Length; i++)
            {
                if (Mass[i] != "0"&&Mass[i].ToCharArray().Length!=0)
                {
                    using (StreamWriter SW = new StreamWriter(new FileStream($"{path}\\output2.txt", FileMode.Append, FileAccess.Write), Encoding.Unicode))
                    {

                        SW.WriteLine(Mass[i]);
                        SW.Flush();
                    }
                }
                else errors++;
            }

            Console.WriteLine("Убрано суммарно {0} из них {1} дублированных, {2} словосочетаний и {3} с двоеточиями и кавычками", errors,dubl,odnosl,dvoetoc);
            Console.WriteLine("Нажми любую кнопку для окончания");
            Console.ReadKey();
        }
    }
}
