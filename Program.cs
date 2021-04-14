using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace testWork
{
    
    class Program
    {
 
           public static void ParceData(string source)
                {
                        string pattern = @"\b\p{Lu}\w*";
                        Regex regex = new Regex(pattern);
                        MatchCollection matches = regex.Matches(source);

                        string[] parceString = source.Split(',');
                        foreach (string item in parceString)
                        {
                            string[] parceItem = item.Split(' ');
                            switch (parceItem.Length)
                            {
                                case 1:
                                   // проверить на дату, время
                                    break;
                                case 2:
                                    // может быть имя фамилия или кабинет
                                    bool res = Regex.IsMatch(source, pattern);
                                    break;
                            }
                        }

                }
        
       
        static void Main(string[] args)
        {
            string str = "Иванов Иван, 97 кабинет, пн-ср, 06:00-15:00, 11:00-12:00";
            ParceData(str);

        }
    }
}
