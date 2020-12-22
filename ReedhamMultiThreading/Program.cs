using BankOfBitsAndBytes;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;


namespace BankOfBitsNByte
{
    class Program
    {
        static BankOfBitsNBytes bbb = new BankOfBitsNBytes();
        static int amountOfMoney = 0;
        static int MaxAcceptableCharLength { get { return BankOfBitsNBytes.acceptablePasswordChars.Length; } }

        private static object lockingTheThread = new object();
        static Thread[] t1 = new Thread[26];
        
        static void Main(string[] args)
        {
            MainEntry();
            Console.Read();
        }

        private static void MainEntry()
        {
            RealMainMethod();
            if(amountOfMoney<5000)
            {
                Console.WriteLine("We robbed the Bank ");
                return;
            }
           
        }

        private static void RealMainMethod()
        {
            while (amountOfMoney < 5000)
            {
                int[] intArray = new int[bbb.passwordLength];
                for (int i = 0; i < MaxAcceptableCharLength; i++)
                {
                    //Thread t = new Thread(new ThreadStart(() => CraftedFunCtion(intArray, i)));
                    t1[i] =new Thread(new ThreadStart(() => CraftedFunCtion(intArray, i)));
                    t1[i].Start();
                    //t1[i].Name = i.ToString();

                }
                //for (int i = 0; i < MaxAcceptableCharLength; i++)
                //{
                //    t1[i].Start();
                //}
                
            }

        }

        private static void  CraftedFunCtion(int[] intArray, int i)
        {
            char[] passwordGuessCharArray = ConvertIntToCharArray(intArray, BankOfBitsNBytes.acceptablePasswordChars);
            DebugOutputCharArray(passwordGuessCharArray);
            amountOfMoney += bbb.WithdrawMoney(passwordGuessCharArray);
            intArray[intArray.Length - 1]++;
            intArray = CheckingElement(intArray, i);
            
        }

        private static int[] CheckingElement(int[] intArray,int firstInt)
        {
            if (intArray[intArray.Length - 1] >= MaxAcceptableCharLength)
            {
                IncrementIntArray(ref intArray, MaxAcceptableCharLength,firstInt);
            }

            return intArray;
        }

        public static void IncrementIntArray(ref int[] toIncrese, int maxAcceptableChar,int firstInt)
        {
            toIncrese[toIncrese.Length - 1]++;
            toIncrese[0] = firstInt;
          
            for (int i = toIncrese.Length -1; i >= 0; i--)
            {

                if (toIncrese[i] >= maxAcceptableChar)
                {
                    toIncrese[i] = 0;
                }
                else
                {
                    return;
                }
            }

        }




        public static char[] ConvertIntToCharArray(int[] toconvert, char[] intConversionArray)
        {
            char[] toReturn = new char[toconvert.Length];
            for (int i = 0; i < toconvert.Length; i++)
            {
                toReturn[i] = intConversionArray[toconvert[i]];
            }
            return toReturn;
        }



        private static void DebugOutputCharArray(char[] toString)
        {
            Console.WriteLine(new string(toString));
        }

    }
}