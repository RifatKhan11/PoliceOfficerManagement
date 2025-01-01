namespace PoliceOfficerManagement.Helpers
{
    public static class LanguageProcessing
    {
        public static string EnglishToBangla(string number)
        {
            if (number == null) return string.Empty;
            number = number.Replace("0", "০").Replace("1", "১").Replace("2", "২").Replace("3", "৩").Replace("4", "৪").Replace("5", "৫").Replace("6", "৬").Replace("7", "৭").Replace("8", "৮").Replace("9", "৯");
            return number;
        }

        public static string BanglaDateShort(DateTime? date)
        {
            if (date == null) return string.Empty;
            var today = date?.Day.ToString();
            var month = date?.ToString("MM");
            var year = date?.Year.ToString();

            today = today.Replace("0", "০").Replace("1", "১").Replace("2", "২").Replace("3", "৩").Replace("4", "৪").Replace("5", "৫").Replace("6", "৬").Replace("7", "৭").Replace("8", "৮").Replace("9", "৯");

            month = month.Replace("0", "০").Replace("1", "১").Replace("2", "২").Replace("3", "৩").Replace("4", "৪").Replace("5", "৫").Replace("6", "৬").Replace("7", "৭").Replace("8", "৮").Replace("9", "৯");

            year = year.Replace("0", "০").Replace("1", "১").Replace("2", "২").Replace("3", "৩").Replace("4", "৪").Replace("5", "৫").Replace("6", "৬").Replace("7", "৭").Replace("8", "৮").Replace("9", "৯");


            var TodayDate = today + "/" + month + "/" + year;
            return TodayDate;
        }

        public static string BanglaDate(DateTime? date)
        {
            if (date == null) return string.Empty;
            var day = date?.DayOfWeek.ToString();
            var today = date?.Day.ToString();
            var month = date?.ToString("MMMM");
            var year = date?.Year.ToString();

            day = day.Replace("Monday", "সোমবার").Replace("Tuesday", "মঙ্গলবার").Replace("Wednesday", "বুধবার").Replace("Thursday", "বৃহস্পতিবার").Replace("Friday", "শুক্রবার").Replace("Saturday", "শনিবার").Replace("Sunday", "রবিবার");

            today = today.Replace("0", "০").Replace("1", "১").Replace("2", "২").Replace("3", "৩").Replace("4", "৪").Replace("5", "৫").Replace("6", "৬").Replace("7", "৭").Replace("8", "৮").Replace("9", "৯");

            year = year.Replace("0", "০").Replace("1", "১").Replace("2", "২").Replace("3", "৩").Replace("4", "৪").Replace("5", "৫").Replace("6", "৬").Replace("7", "৭").Replace("8", "৮").Replace("9", "৯");

            month = month.Replace("January", "জানুয়ারী").Replace("February", "ফেব্র্রুয়ারি").Replace("March", "মার্চ ").Replace("April", "এপ্রিল").Replace("May", "মে").Replace("June", "জুন").Replace("July", "জুলাই").Replace("August", "আগস্ট ").Replace("September", "সেপ্টেম্বর").Replace("October", "অক্টোবর ").Replace("November", "নভেম্বর").Replace("December", "ডিসেম্বর");



            var TodayDate = day + ", " + today + " " + month + " " + year + " খ্রিঃ";
            return TodayDate;
        }

        public static string BanglaWeekDay(string dayName)
        {
            var day = dayName;

            day = day.Replace("Monday", "সোমবার").Replace("Tuesday", "মঙ্গলবার").Replace("Wednesday", "বুধবার").Replace("Thursday", "বৃহস্পতিবার").Replace("Friday", "শুক্রবার").Replace("Saturday", "শনিবার").Replace("Sunday", "রবিবার");

            return day;
        }

        public static string BanglaMonth(string month)
        {
            month = month.Replace("Jan", "জানুয়ারি").Replace("Feb", "ফেব্রুয়ারি").Replace("Mar", "মার্চ").Replace("Apr", "এপ্রিল").Replace("May", "মে").Replace("Jun", "জুন").Replace("Jul", "জুলাই").Replace("Aug", "আগস্ট").Replace("Sep", "সেপ্টেম্বর").Replace("Oct", "অক্টোবর").Replace("Nov", "নভেম্বর").Replace("Dec", "ডিসেম্বর");

            return month;
        }
        public static string MonthInBanglaBySl(int month)
        {
            string monthName = "";
            if (month == 1) { monthName = "জানুয়ারি"; }
            else if (month == 2) { monthName = "ফেব্রুয়ারি"; }
            else if (month == 3) { monthName = "মার্চ";}
            else if (month == 4) { monthName = "এপ্রিল";}
            else if (month == 5) { monthName = "মে";}
            else if (month == 6) { monthName = "জুন";}
            else if (month == 7) { monthName = "জুলাই";}
            else if (month == 8) { monthName = "আগস্ট";}
            else if (month == 9) { monthName = "সেপ্টেম্বর";}
            else if (month == 10) { monthName = "অক্টোবর";}
            else if (month == 11) {monthName = "নভেম্বর";}
            else if (month == 12) {monthName = "ডিসেম্বর";}
            return monthName;
        }

    }
}
