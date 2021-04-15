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
                        Dictionary<string, string> reStruct = new Dictionary<string, string>();
                        string patternName = @"\b\p{Lu}\w*";
                        string patternWorkTime = @"\d{2}-\d{2}";
                        string patternRestTime = @"\d{2}-\d{2}";
            string cab = @"\bкаб\w*|\bпомещ\w*";
            Regex regexCab = new Regex(cab);
            MatchCollection matchCab = regexCab.Matches(source);
                        Regex regexName = new Regex(patternName);
                        MatchCollection matchesName = regexName.Matches(source);

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
                                    bool res = Regex.IsMatch(item, patternName);
                                    if (res)
                                    {
                                        reStruct.Add("Работник:", item);
                                    }
                                    else
                                    {
                                        if(Regex.IsMatch(parceItem[0], @"\d+")&&Regex.IsMatch(parceItem[1], @"^каб\w* | ^пом\w*"))
                                        {
                                            reStruct.Add("Кабинет:", parceItem[0]);
                                        }
                                        
                                    }
                                    break;
                            }
                        }

                }
        
       
        static void Main(string[] args)
        {
            string str = "Иванов Иван,97 кабинет помещение,пн-ср,06:00-15:00,11:00-12:00";
            ParceData(str);

        }
    }
}
