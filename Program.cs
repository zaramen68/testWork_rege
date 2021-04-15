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
            string patterDays = @"\w{2} "
            string cab = @"\bкаб\w*|\bпомещ\w*";
            string cabNum = @"\d+";
            Regex regexCab = new Regex(cab);
            Regex regexCabNum = new Regex(cabNum);
            
            Regex regexName = new Regex(patternName);
            

            string[] parceString = source.Split(',');
            foreach (string item in parceString)
                {
                MatchCollection matchesName = regexName.Matches(source);
                if (matchesName.Count == 2)
                {
                    reStruct.Add("Работник:", item);
                    continue;
                }

                MatchCollection matchCab = regexCab.Matches(source);
                MatchCollection matchCabNum = regexCabNum.Matches(source);
                if (matchCab.Count == 1 && matchCabNum.Count == 1)
                {
                    reStruct.Add("Кабинет:", matchCabNum.ToString());
                    continue;
                }

               
            }

    }
        
       
        static void Main(string[] args)
        {
            string str = "Иванов Иван, 97 кабинет помещение, пн-ср, 06:00-15:00, 11:00-12:00";
            ParceData(str);

        }
    }
}
