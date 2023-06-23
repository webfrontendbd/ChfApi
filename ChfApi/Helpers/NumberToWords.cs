namespace ChfApi.Helpers
{
    public static class NumberToWords
    {
        private static string[] ones = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        private static string[] tens = { "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
        private static string[] thousands = { "", "thousand", "million", "billion", "trillion" };
        private static string separator = " ";

        public static string ConvertAmountToWords(double amount)
        {
            if (amount == 0)
            {
                return ones[0];
            }

            string amountString = amount.ToString("F2"); // Format amount with two decimal places
            string[] parts = amountString.Split('.');
            long wholeNumberPart = long.Parse(parts[0]);
            int decimalPart = int.Parse(parts[1]);

            string amountInWords = ConvertToWords(wholeNumberPart) + " Only";
            if (decimalPart > 0)
            {
                amountInWords += separator + ConvertToWords(decimalPart) + " paisa";
            }

            return amountInWords;
        }

        private static string ConvertToWords(long number)
        {
            if (number < 20)
            {
                return ones[number];
            }

            if (number < 100)
            {
                long tensDigit = number / 10;
                long onesDigit = number % 10;
                string words = tens[tensDigit - 2];
                if (onesDigit > 0)
                {
                    words += separator + ones[onesDigit];
                }
                return words;
            }

            if (number < 1000)
            {
                long hundredsDigit = number / 100;
                long remainingNumber = number % 100;
                string words = ones[hundredsDigit] + separator + "hundred";
                if (remainingNumber > 0)
                {
                    words += separator + ConvertToWords(remainingNumber);
                }
                return words;
            }

            for (int i = 0; i < thousands.Length; i++)
            {
                long divisor = (long)Math.Pow(1000, i + 1);
                if (number < divisor)
                {
                    long quotient = number / (divisor / 1000);
                    long remainder = number % (divisor / 1000);
                    string words = ConvertToWords(quotient) + separator + thousands[i];
                    if (remainder > 0)
                    {
                        words += separator + ConvertToWords(remainder);
                    }
                    return words;
                }
            }

            return "";
        }
    }
}
