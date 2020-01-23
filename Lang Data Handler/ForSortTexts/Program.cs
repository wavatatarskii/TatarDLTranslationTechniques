using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace ForSortTexts
{
    class Program
    {
        static void Main(string[] args)
        {
            //получение директории где находится exe
            string path = (Assembly.GetExecutingAssembly().Location);//получение пути файла

            {//ограничение зоны видимости
                string[] s2 = path.Split('\\');
                path = path.TrimEnd(s2[s2.Length - 1].ToCharArray());//удаление имени и расширения файла из пути
            }
            //if (false)
            //{
            //    for (int i = 1; i <= 154; i++)
            //    {
            //        string path1 = path + i.ToString() + ".txt";
            //        string[] Mass = File.ReadAllLines($"{path1}", System.Text.Encoding.Unicode);//у нас Unicode , а не ACSII
            //        for (int j = 0; j < Mass.Length; j++)
            //        {
            //            using (StreamWriter SW = new StreamWriter(new FileStream($"{path}\\output.txt", FileMode.Append, FileAccess.Write), Encoding.Unicode))
            //            {

            //                SW.WriteLine(Mass[j]);
            //                SW.Flush();
            //            }
            //        }
            //    }
            //}

            string[] Mass = File.ReadAllLines($"{path}\\60000tt.txt", System.Text.Encoding.Unicode);
            string[] Mass2 = File.ReadAllLines($"{path}\\60000ru.txt", System.Text.Encoding.Unicode);
            int quantity = 0;
            {
                //удаление символов из начала строк
                
                ////for (int i = 0; i < Mass.Length; i++) { quantity += Mass[i].Length; }
                //for (int i = 0; i < Mass.Length; i++)
                //{
                //    //int kk = 0;
                //    for (int j = 0; j < 10; j++)
                //    {
                //        char[] ar = Mass[i].ToCharArray();
                //        if (ar[0] == '—' || ar[0] == ')' || ar[0] == '(' || ar[0] == '-' || ar[0] == '.' || ar[0] == ' ')
                //        {
                //            Mass[i] = Mass[i].Remove(0, 1);
                //            quantity++;
                //        }
                //    }

                //удаление строк с ? «»
                //for (int j = 0; j < Mass[i].Length && kk == 0; j++)
                //{
                //    if (ar[j] == '?' || ar[j] == '«' || ar[j] == '»')
                //    {
                //        kk++;
                //        quantity++;
                //        Mass[i] = "0";
                //    }
                //}
                // Console.WriteLine(i + "\t" + quantity);

                //}
            }

            //for (int i = 0; i < Mass.Length; i++)
            //{
            //    int k = 0;
            //    char[] ar = Mass[i].ToCharArray();
            //    for (int j = 0; j < Mass[i].Length; j++)
            //    {
            //        if (ar[j] == '.' || ar[j] == '!') k++;
            //    }
            //    if (k % 2 == 0||k==4) 
            //    using (StreamWriter SW = new StreamWriter(new FileStream($"{path}\\errors.txt", FileMode.Append, FileAccess.Write), Encoding.Unicode))
            //    {

            //        SW.WriteLine(i);
            //        SW.Flush();
            //    }
            //}
            


                for (int i = 0; i < Mass.Length; i++)
            {
                //if (Mass[i] != "0" && Mass[i].ToCharArray().Length != 0)
                //{
                    using (StreamWriter SW = new StreamWriter(new FileStream($"{path}\\tatTABru60000.txt", FileMode.Append, FileAccess.Write), Encoding.Unicode))
                    {

                        SW.WriteLine(Mass[i]+"\t"+Mass2[i]);
                        SW.Flush();
                    if (i > 60000)
                        quantity++;
                    }
                //}
                //Console.WriteLine("{0}+{1}", i, Mass.Length);
            }

            Console.WriteLine("{0}", quantity);

        }
    }
}
