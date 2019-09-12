using System;
using System.Collections.Generic;
using System.Linq;

namespace WILDCARD
{
    class Program
    {
        // -1 : has never been calculated
        // 1 : Inputs are matched
        // 0 : Inputs are not matched
        static int[,] cache = new int[101, 101];
        static string WildCard = "he?e";
        static string FileName = "help";

        List<string> inputList = new List<string> {
            "help",
            "heap",
            "henp"
            };
        static void Main(string[] args)
        {
            var aa = IsValid("???*", "hepa");
            Console.WriteLine(aa);

            for(int i = 0; i < 101; i++)
            {
                for(int j = 0; j < 101; j++)
                {
                    cache[i, j] = -1;
                }
            }
            var bb = MatchMemoized(0, 0);
            Console.WriteLine(bb);
        }

        static bool IsValid(string wildCard, string fileName)
        {
            int lengthOfWildCard = wildCard.Length;
            int lengthOfFileName = fileName.Length;
            int index = 0;

            while (index < lengthOfWildCard
                && index < lengthOfFileName
                && (wildCard[index] == '?' || wildCard[index] == fileName[index]))
            {
                index++;
            }

            if (lengthOfWildCard == index) //final character?
            {
                //same length?
                return lengthOfFileName == lengthOfWildCard;
            }

            if (wildCard[index] == '*')
            {
                int cnt = 0;
                while (cnt + index <= lengthOfFileName)
                {
                    if (IsValid(wildCard.Substring(index + 1), fileName.Substring(cnt + index)))
                    {
                        return true;
                    }
                    else
                    {
                        cnt++;
                    }
                }
            }

            return false;
        }


        static bool MatchMemoized(int w, int s)
        {
            //Memoization
            int ret = cache[w, s];
            int orgW = w;
            int orgS = s;

            if (ret != -1)
            {
                return ret == 1;
            }

            while(s < WildCard.Length 
                && w < FileName.Length
                && (WildCard[w] == '?' || WildCard[w] == FileName[s]))
            {
                w++;
                s++;
            }

            if(w == WildCard.Length)
            {
                cache[orgW, orgS] = s == FileName.Length ? 1 : 0;
                return s == FileName.Length;
            }

            if(WildCard[w] == '*')
            {
                for(int skip = 0; skip+s <= FileName.Length; skip++)
                {
                    if (MatchMemoized(w+1, s+skip))
                    {
                        cache[orgW, orgS] = 1;
                        return true;
                    }
                }
            }


            cache[orgW, orgS] = 0;
            return false;
        }
    }
}
