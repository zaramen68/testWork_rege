﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Globalization;

namespace testWork
{
    
    class Program
    {
        public static DateTime ParceDate(string source)
        {
            int year;
            int day, month;
            int defaultMillenium = 2000;
            string sYear;
            DateTime resDate=new DateTime();
            Calendar myCal = CultureInfo.InvariantCulture.Calendar;
            string ddmm;
            
            string patternYear4 = @"\d{4}";
            string patternYear2 = @"\d{2}";
           
            
            Regex reYear4 = new Regex(patternYear4);
            Regex reYear2 = new Regex(patternYear2);

            MatchCollection maY4 = reYear4.Matches(source);
           

            ddmm = Regex.Replace(source, patternYear4, "");
            //MatchCollection maDDMM = reDay.Matches(ddmm);
            MatchCollection maY2 = reYear2.Matches(ddmm);
            if (maY4.Count>0)
            {
 
                sYear = maY4[0].Value;
                resDate=new DateTime(Int32.Parse(sYear), 1, 1, new GregorianCalendar());
                        
                switch (maY2.Count)
                {
                    case 1:
                        // Неделя

                        resDate = myCal.AddWeeks(resDate, Int32.Parse(maY2[0].Value)!=0 ? Int32.Parse(maY2[0].Value)-1 : 0  );
                        int dw = (int) myCal.GetDayOfWeek(resDate);
                        resDate = resDate.AddDays((resDate.Day <= 6 && resDate.Month == 1)? 0: (1 - dw));
                        break;
                    case 2:
                        // День и месяц
                        month = Int32.Parse(maY2[1].Value);
                        day = Int32.Parse(maY2[0].Value);
                        resDate = new DateTime(Int32.Parse(maY4[0].Value), month , day);
                        break;                
                }
            }
            else
            {
                
                switch (maY2.Count)
                {
                    case 2:  // Неделя
                        sYear = maY2[1].Value;
                        year = Int32.Parse(sYear) + defaultMillenium;
                        resDate = new DateTime(year, 1, 1, new GregorianCalendar());                    
                        resDate = myCal.AddWeeks(resDate, Int32.Parse(maY2[0].Value) != 0 ? Int32.Parse(maY2[0].Value) - 1 : 0);
                        int dw = (int)myCal.GetDayOfWeek(resDate);
                        resDate = resDate.AddDays((resDate.Day <= 6 && resDate.Month == 1) ? 0 : (1 - dw));
                        break;
                    case 3:  // День и месяц
                        
                        sYear = maY2[2].Value;
                        year = Int32.Parse(sYear) + defaultMillenium;
                        resDate = new DateTime(year, Int32.Parse(maY2[1].Value), Int32.Parse(maY2[0].Value));
                        break;


                }
            }
           
            
            
            return resDate;
        }
 
           public static string ParceData(string source)
           {
            int n = 0;
            string workTime ="";
            DateTime startWTime = new DateTime();
            DateTime finishWTime = new DateTime();
            Dictionary<string, string> reStruct = new Dictionary<string, string>();
            string patternName = @"\b\p{Lu}\w*";
            string patternTime = @"\d{2}\:\d{2}";
           
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
                    
                    reStruct.Add("Кабинет:", matchCabNum[0].Value);
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

            string resultStr = "Работник:" + reStruct["Работник:"] + '\n' + "Кабинет:" + reStruct["Кабинет:"] + '\n' + "Рабочие дни:" +
                    reStruct["Рабочие дни:"] + '\n' + "Время работы:" + reStruct["Время работы:"] + '\n' + "Обеденное время:" + reStruct["Обеденное время:"];
            return resultStr;
            }
        
       
        static void Main(string[] args)
        {

            DateTime dateTime = ParceDate("08.20");
            string str = "Иванов Иван, 97 кабинет, пн-ср, 06:00 - 15:00, 11:00-12:00";
            string result = ParceData(str);
            Console.WriteLine(result);
            Console.WriteLine(str);

        }
    }
}
