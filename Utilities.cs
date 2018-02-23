using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2
{
    class Utilities
    {
        public static int ReadNum(int start, int end, string UserInput, int otherwise = 0)
        {
            int i;
            for (i = start; i <= end; i++)
            {
                if (i.ToString() == UserInput)
                {
                    return i;
                }
            }
            return otherwise;
        }
        public static Int32 stringToInt(String input)
        {
            Int32 output;
            output = Int32.Parse(input);
            return output;
        }

    }
}
