namespace PoliceOfficerManagement.Helpers
{
    public static class utility
    {
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

            var x = "গত";
            if (date?.Date == DateTime.Now.Date)
            {
                x = "অদ্য";
            }

            var TodayDate = x + " রোজ " + day + ", " + today + " " + month + " " + year + " খ্রিঃ";
            return TodayDate;
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

        public static string EnglishToBangla(string number)
        {
            if (number == null) return string.Empty;
            number = number.Replace("0", "০").Replace("1", "১").Replace("2", "২").Replace("3", "৩").Replace("4", "৪").Replace("5", "৫").Replace("6", "৬").Replace("7", "৭").Replace("8", "৮").Replace("9", "৯");
            return number;
        }

        public static string BanglaTime(string time)
        {
            if (time == null) return string.Empty;
            var str = time.Split(':');//11:5
            string x = "সকাল";
            if (Convert.ToInt32(str[0]) >= 4 && Convert.ToInt32(str[0]) <= 5)
            {
                x = "ভোর";
            }
            else if (Convert.ToInt32(str[0]) <= 3)
            {
                x = "রাত";
            }
            else if (Convert.ToInt32(str[0]) <= 11)
            {
                x = "সকাল";
            }
            else if (Convert.ToInt32(str[0]) <= 14)
            {
                x = "দুপুর";
            }
            else if (Convert.ToInt32(str[0]) <= 17)
            {
                x = "বিকাল";
            }
            else if (Convert.ToInt32(str[0]) <= 19)
            {
                x = "সন্ধ্যা";
            }
            else
            {
                x = "রাত";
            }

            if (Convert.ToInt32(str[0]) > 12)
            {
                string first = (Convert.ToInt32(str[0]) - 12).ToString();
                time = first + ":" + str[1];
            }

            time = time.Replace("0", "০").Replace("1", "১").Replace("2", "২").Replace("3", "৩").Replace("4", "৪").Replace("5", "৫").Replace("6", "৬").Replace("7", "৭").Replace("8", "৮").Replace("9", "৯");


            var TodayDate = x + " " + time;
            return TodayDate;
        }
    }
}
