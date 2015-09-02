using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Manager.Utilities.Converters
{
    public class InWordsAmountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return GetAmountInWords((int)value) + " Only";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        String GetWord(int digit)
        {
            switch(digit)
            {
                case 1: return "One";
                case 2: return "Two";
                case 3: return "Three";
                case 4: return "Four";
                case 5: return "Five";
                case 6: return "Six";
                case 7: return "Seven";
                case 8: return "Eight";
                case 9: return "Nine";
                case 10: return "Ten";
                case 11: return "Eleven";
                case 12: return "Twelve";
                case 13: return "Thirteen";
                case 14: return "Fourteen";
                case 15: return "Fifteen";
                case 16: return "Sixteen";
                case 17: return "Seventeen";
                case 18: return "Eighteen";
                case 19: return "Nineteen";
                case 20: return "Twenty";
                case 30: return "Thirty";
                case 40: return "Forty";
                case 50: return "Fifty";
                case 60: return "Sixty";
                case 70: return "Seventy";
                case 80: return "Eighty";
                case 90: return "Ninety";
                case 100: return "Hundred";
                case 1000: return "Thousand";
                case 100000: return "Lakh";
                case 10000000: return "Crore";
            }
            return null;
        }

        String GetAmountInWords_helper(int amount)
        {
            String words = "";
            String phrase;
            int place = 1;
            int digit;
            int tensUnitsNumber = amount - ((amount / 100) * 100);

            while (amount != 0)
            {
                digit = amount % 10;

                if (place == 10)
                {
                    if (digit == 1)
                    {
                        phrase = GetWord(tensUnitsNumber);
                        words = "";
                    }
                    else
                        phrase = GetWord(digit * 10);
                }
                else
                    phrase = GetWord(digit);
                if (place > 10 && digit != 0)
                    phrase += " " + GetWord(place);

                words = phrase + " " + words;
                amount /= 10;
                place *= 10;
            }

            return words;
        }

        String GetAmountInWords(int amount)
        {
            String words = "";
            String stepPhrase;
            int subNumber = amount;
            int step = 1;

            while(amount > 0)
            {
                subNumber = amount;
                stepPhrase = "";
                if(step == 1)
                {
                    subNumber = subNumber - ((subNumber / 1000) * 1000);
                    amount = amount / 1000;
                    step *= 1000;
                }
                else
                {
                    subNumber = subNumber - ((subNumber / 100) * 100);
                    amount = amount / 100;
                    if(subNumber != 0 || amount == 0)
                        stepPhrase = " " + GetWord(step);
                    step *= 100;
                }
                words = GetAmountInWords_helper(subNumber) + stepPhrase + " " + words;
            }

            return words;
        }
    }
}
