namespace PoliceOfficerManagement.Helpers
{
    public static class InWords
    {
        private static readonly string[] Units = {
            "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten",
            "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"
        };

        private static readonly string[] Tens = {
            "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"
        };

        private static readonly string[] ThousandsGroups = { "", "Thousand", "Million", "Billion" };

        public static string Convert(long number)
        {
            if (number == 0)
                return Units[0];

            if (number < 0)
                return "Minus " + Convert(-number);

            var parts = new System.Collections.Generic.List<string>();
            int groupIndex = 0;

            while (number > 0)
            {
                int numberToProcess = (int)(number % 1000);
                number /= 1000;

                if (numberToProcess != 0)
                {
                    string numberToWords = ConvertLessThanOneThousand(numberToProcess);
                    parts.Insert(0, numberToWords + (groupIndex > 0 ? " " + ThousandsGroups[groupIndex] : ""));
                }

                groupIndex++;
            }

            return string.Join(" ", parts);
        }

        private static string ConvertLessThanOneThousand(int number)
        {
            string words;

            if (number % 100 < 20)
            {
                words = Units[number % 100];
                number /= 100;
            }
            else
            {
                words = Units[number % 10];
                number /= 10;

                words = Tens[number % 10] + (words != "" ? " " + words : "");
                number /= 10;
            }

            if (number == 0)
                return words;

            return Units[number] + " Hundred" + (words != "" ? " And " + words : "");
        }
    }
}
