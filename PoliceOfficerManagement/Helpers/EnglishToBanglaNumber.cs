namespace PoliceOfficerManagement.Helpers
{
    public static class EnglishToBanglaNumber
    {
        public static String ConvertEnglishNumToBanglaNum(string number)
        {
            var en = "0123456789";
            var bn = "০১২৩৪৫৬৭৮৯";
            if (number == null)
            {
                number = "";

            }

            var bnNum = "";

            for (int i = 0; i < number.Length; i++)
            {
                if (en.IndexOf(number[i]) < 0)
                {
                    bnNum += number[i];
                }
                else
                {
                    var bnNumber = bn[en.IndexOf(number[i])];
                    bnNum += bnNumber;
                }

            }
            return bnNum;
        }

        public static string DaysToBanglaYears(int days)
        {
            int years = days / 365;
            int months = (days - (years * 365)) / 30;
            int day = (days - ((years * 365) + (months * 30)));
            return ConvertEnglishNumToBanglaNum(years.ToString()) + " বছর " + ConvertEnglishNumToBanglaNum(months.ToString()) + " মাস " + ConvertEnglishNumToBanglaNum(day.ToString()) + " দিন";
        }
        public static string DaysToEnglishYears(int days)
        {
            int years = days / 365;
            int months = (days - (days % 365)) / 30;
            int day = (days - (years * 365) + (months * 30));
            return years + " years " + months + " months " + day + " days";
        }

        public static string EnglishToBanglaDate(DateTime datetime) //dd/MM/yyyy
        {
            var banglaDate = "";

            var year = datetime.Year;
            var month = datetime.Month;
            var date = datetime.Day;
            //var code = "455666655435";

            var code = new List<int>
            {
                4, 5, 5, 6, 6, 6, 6, 5, 5, 4, 3, 5
            };
            var enMonths = new List<int>
            {
                4, 5,6,7,8,9,10,11,12,1,2,3
				//"4","5","6","7","8","9","10","11","12","1","2","3"
			};
            var bnMonths = new List<int>
            {
                1,2,3,4,5,6,7,8,9,10,11,12
				//"1","2","3","4","5","6","7","8","9","10","11","12"
			};
            var bnMonthNames = new List<string>
            {
                "বৈশাখ","জৈষ্ঠ্য","আষাঢ়","শ্রাবণ","ভাদ্র","আশ্বিন","কার্তিক","অগ্রহায়ণ","পৌষ","মাঘ","ফাল্গুন","চৈত্র"
            };

            //var bnMonthNames = new List<string>
            //{
            //    "জানুয়ারি","ফেব্রুয়ারী","মার্চ","এপ্রিল","মে","জুন","জুলাই","আগস্ট","সেপ্টেম্বর","অক্টোবর","নভেম্বর","ডিসেম্বর"
            //};

            var enIndex = enMonths.IndexOf(month);
            var bnNum = bnMonths[enIndex];
            var bnMonthName = bnMonthNames[bnNum - 1];

            var bnCode = code[bnNum - 1];
            var bnDateStart = bnCode + 10;

            var today = 0;
            if (date > bnDateStart)
            {
                today = date - bnDateStart;
            }
            else if (date == bnDateStart)
            {
                today = 1;
            }
            else
            {
                var mIn = bnMonthNames.IndexOf(bnMonthName);
                if (mIn == 0)
                {
                    mIn = 12;
                }
                today = 30 - (bnDateStart - date);
                bnMonthName = bnMonthNames[mIn - 1];
            }

            var bnYear = year - 593;

            banglaDate = bnMonthName + " " + ConvertEnglishNumToBanglaNum(today.ToString()) + ", " + ConvertEnglishNumToBanglaNum(bnYear.ToString());

            return banglaDate;
        }


        public static string EnglishToBanglishDate(DateTime datetime) //dd/MM/yyyy
        {
            var banglaDate = "";

            var year = datetime.Year;
            var month = datetime.Month;
            var date = datetime.Day;



            var bnMonthNames = new List<string>
            {
                "জানুয়ারি","ফেব্রুয়ারী","মার্চ","এপ্রিল","মে","জুন","জুলাই","আগস্ট","সেপ্টেম্বর","অক্টোবর","নভেম্বর","ডিসেম্বর"
            };

            var monthName = bnMonthNames[month - 1];




            banglaDate = ConvertEnglishNumToBanglaNum(date.ToString()) + " " + monthName + ", " + ConvertEnglishNumToBanglaNum(year.ToString());

            return banglaDate;
        }



        public static string EnglishToEnglishMonth(string month)
        {
            string[] monthName = { "জানুয়ারী", "ফেব্রুয়ারি", "মার্চ", "এপ্রিল", "মে", "জুন", "জুলাই", "আগস্ট", "সেপ্টেম্বর", "অক্টোবর", "নভেম্বর", "ডিসেম্বর" };
            var mn = int.Parse(month);
            string monthNamedata = monthName[mn - 1];

            return monthNamedata;

        }




    }
}
