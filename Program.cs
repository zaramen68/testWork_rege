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
 
           public static string ParceData(string source)
           {
            int n = 0;
            string workTime ="";
            DateTime startWTime = new DateTime();
            DateTime finishWTime = new DateTime();
            Dictionary<string, string> reStruct = new Dictionary<string, string>();
            string patternName = @"\b\p{Lu}\w*";
            string patternTime = @"\d{2}\:\d{2}";
            string patternRestTime = @"\d{2}-\d{2}";
            string patterDays = @"\p{Ll}{2}|\p{Ll}{2}-\p{Ll}{2}";
            string cab = @"\bкаб\w*|\bпомещ\w*";
            string cabNum = @"\d+";

            string patMonday = "пн";
            string patTuesday = "вт";
            string patWednesday = "ср";
            string patThursday = "чт";
            string patFriday = "пт";
            string patSaturday = "сб";
            string patSunday = "вс";



            Regex regexCab = new Regex(cab);
            Regex regexCabNum = new Regex(cabNum);
            Regex regexDays = new Regex(patterDays);
            Regex regexName = new Regex(patternName);
            Regex regexTime = new Regex(patternTime);
            

            string[] parceString = source.Split(',');
            foreach (string item in parceString)
                {
                MatchCollection matchesName = regexName.Matches(item);
                if (matchesName.Count == 2)
                {
                    reStruct.Add("Работник:", item);
                    continue;
                }

                MatchCollection matchCab = regexCab.Matches(item);
                MatchCollection matchCabNum = regexCabNum.Matches(item);
                if (matchCab.Count == 1 && matchCabNum.Count == 1)
                {
                    foreach (Match mat in matchCabNum){
                        reStruct.Add("Кабинет:", mat.Value);
                    }
                    
                    continue;
                }

                MatchCollection matchDays = regexDays.Matches(item);
                if (matchDays.Count > 0)
                {
                    string iItem = Regex.Replace(item, patMonday, "понедельник");
                    iItem = Regex.Replace(iItem, patTuesday, "вторник");
                    iItem = Regex.Replace(iItem, patWednesday, "среда");
                    iItem = Regex.Replace(iItem, patThursday, "четверг");
                    iItem = Regex.Replace(iItem, patFriday, "пятница");
                    iItem = Regex.Replace(iItem, patSaturday, "суббота");
                    iItem = Regex.Replace(iItem, patSunday, "воскресенье");

                    reStruct.Add("Рабочие дни:", iItem);
                    continue;
                }
                MatchCollection matchTime = regexTime.Matches(item);
                if (matchTime.Count == 2)
                {
                    string[] timeList = item.Split('-');
                    if (n == 0)
                    {
                        workTime = item;
                        startWTime = DateTime.Parse(timeList[0]);
                        finishWTime = DateTime.Parse(timeList[1]);
                        n++;
                        
                    }
                    else
                    {
                        if (DateTime.Parse(timeList[0]) > startWTime)
                        {
                            reStruct.Add("Время работы:", workTime);
                            reStruct.Add("Обеденное время:", item);

                        }
                        else
                        {
                            reStruct.Add("Время работы:", item);
                            reStruct.Add("Обеденное время:", workTime);
                        }
                    }

                    continue;

                }

          
            }
            //string resultStr = "Работник:" + reStruct["Работник:"] + "/n" + "Кабинет:" + reStruct["Кабинет:"] + "/n" + "Рабочие дни:" +
            //        reStruct["Рабочие дни:"] + "/n" + "Время работы:" + reStruct["Время работы:"] + "/n" + "Обеденное время:" + reStruct["Обеденное время:"];
            //string resultStr = @"Работник: {reStruct["Работник:"]} /n Кабинет: {reStruct["Кабинет:"]} /n Рабочие дни: { reStruct["Рабочие дни:"]} /n Время работы: { reStruct["Время работы:"]}  /n Обеденное время: { reStruct["Обеденное время:"]}";
            string resultStr = "Работник:" + reStruct["Работник:"] + '\n' + "Кабинет:" + reStruct["Кабинет:"] + '\n' + "Рабочие дни:" +
                    reStruct["Рабочие дни:"] + '\n' + "Время работы:" + reStruct["Время работы:"] + '\n' + "Обеденное время:" + reStruct["Обеденное время:"];
            return resultStr;
            }
        
       
        static void Main(string[] args)
        {
            string str = "Иванов Иван, 97 кабинет, пн-ср, 06:00 - 15:00, 11:00-12:00";
            string result = ParceData(str);
            Console.WriteLine(result);
            Console.WriteLine(str);

        }
    }
}
