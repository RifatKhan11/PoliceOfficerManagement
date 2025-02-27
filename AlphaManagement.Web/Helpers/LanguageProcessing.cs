using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Helpers
{
    public static class LanguageProcessing
    {
        public static string EnglishToBangla(string number)
        {
            if (number == null) return string.Empty;
            number = number.Replace("0", "০").Replace("1", "১").Replace("2", "২").Replace("3", "৩").Replace("4", "৪").Replace("5", "৫").Replace("6", "৬").Replace("7", "৭").Replace("8", "৮").Replace("9", "৯");
            return number;
        }
        public static string BanglaToEnglish(string number)
        {
            if (number == null) return string.Empty;
            number = number.Replace("০", "0").Replace("১", "1").Replace("২", "2").Replace("৩", "3").Replace("৪", "4").Replace("৫", "5").Replace("৬", "6").Replace("৭", "7").Replace("৮", "8").Replace("৯", "9");
            return number;
        }

        public static string BanglaDateShort(DateTime? date)
        {
            if (date == null) return string.Empty;
            var today = date?.Day.ToString();
            var month = date?.ToString("MMM");
            var year = date?.Year.ToString();

            today = today.Replace("0", "০").Replace("1", "১").Replace("2", "২").Replace("3", "৩").Replace("4", "৪").Replace("5", "৫").Replace("6", "৬").Replace("7", "৭").Replace("8", "৮").Replace("9", "৯");

            month = month.Replace("0", "০").Replace("1", "১").Replace("2", "২").Replace("3", "৩").Replace("4", "৪").Replace("5", "৫").Replace("6", "৬").Replace("7", "৭").Replace("8", "৮").Replace("9", "৯");

            year = year.Replace("0", "০").Replace("1", "১").Replace("2", "২").Replace("3", "৩").Replace("4", "৪").Replace("5", "৫").Replace("6", "৬").Replace("7", "৭").Replace("8", "৮").Replace("9", "৯");


            var TodayDate = today + "/" + month + "/" + year;
            return TodayDate;
        }

        public static string BanglaDate(string banglaDate)
        {
            var date = new DateTime?();
            if (banglaDate == null) { return string.Empty; }
            else
            {
                date = Convert.ToDateTime(banglaDate);
            }
            var today = date?.Day.ToString();
            var month = date?.ToString("MMM");
            var year = date?.Year.ToString();

            today = today.Replace("0", "০").Replace("1", "১").Replace("2", "২").Replace("3", "৩").Replace("4", "৪").Replace("5", "৫").Replace("6", "৬").Replace("7", "৭").Replace("8", "৮").Replace("9", "৯");

            month = month.Replace("Jan", "০১").Replace("Feb", "০২").Replace("Mar", "০৩").Replace("Apr", "০৪").Replace("May", "০৫").Replace("Jun", "০৬").Replace("Jul", "০৭").Replace("Aug", "০৮").Replace("Sep", "০৯").Replace("Oct", "১০").Replace("Nov", "১১").Replace("Dec", "১২");

            year = year.Replace("0", "০").Replace("1", "১").Replace("2", "২").Replace("3", "৩").Replace("4", "৪").Replace("5", "৫").Replace("6", "৬").Replace("7", "৭").Replace("8", "৮").Replace("9", "৯");


            var TodayDate = today + "/" + month + "/" + year;
            return TodayDate;
        }

        public static string EnlishToBanglaDateConvert(string date)
        {
            if (date == "" || date == null )
            {
                return "";
            }
            else
            {
                var today = date.Split('-')[0];
                var month = date.Split('-')[1];
                var year = date.Split('-')[2];

                    today = today.Replace("0", "০").Replace("1", "১").Replace("2", "২").Replace("3", "৩").Replace("4", "৪").Replace("5", "৫").Replace("6", "৬").Replace("7", "৭").Replace("8", "৮").Replace("9", "৯");

                month = month.Replace("Jan", "০১").Replace("Feb", "০২").Replace("Mar", "০৩").Replace("Apr", "০৪").Replace("May", "০৫").Replace("Jun", "০৬").Replace("Jul", "০৭").Replace("Aug", "০৮").Replace("Sep", "০৯").Replace("Oct", "১০").Replace("Nov", "১১").Replace("Dec", "১২");

                year = year.Replace("0", "০").Replace("1", "১").Replace("2", "২").Replace("3", "৩").Replace("4", "৪").Replace("5", "৫").Replace("6", "৬").Replace("7", "৭").Replace("8", "৮").Replace("9", "৯").Replace("0", "০").Replace("1", "১").Replace("2", "২").Replace("3", "৩").Replace("4", "৪").Replace("5", "৫").Replace("6", "৬").Replace("7", "৭").Replace("8", "৮").Replace("9", "৯");


                var TodayDate = today + "/" + month + "/" + year;
                return TodayDate;
            }
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

        public static string BanglaDays(DateTime? date)
        {
            if (date == null) return string.Empty;
            var day = date?.DayOfWeek.ToString();
            day = day.Replace("Monday", "সোমবার").Replace("Tuesday", "মঙ্গলবার").Replace("Wednesday", "বুধবার").Replace("Thursday", "বৃহস্পতিবার").Replace("Friday", "শুক্রবার").Replace("Saturday", "শনিবার").Replace("Sunday", "রবিবার");
            var TodayDate = day;
            return TodayDate;
        }

        public static string BanglaMonth(string month)
        {
            month = month.Replace("Jan", "জানুয়ারী").Replace("Feb", "ফেব্র্রুয়ারি").Replace("Mar", "মার্চ ").Replace("Apr", "এপ্রিল").Replace("May", "মে").Replace("Jun", "জুন").Replace("Jul", "জুলাই").Replace("Aug", "আগস্ট ").Replace("Sep", "সেপ্টেম্বর").Replace("Oct", "অক্টোবর ").Replace("Nov", "নভেম্বর").Replace("Dec", "ডিসেম্বর");

            return month;
        }

        public static string DayTime(string time)
        {
            time = time.Replace("Morning", "পূর্বাহ্নে").Replace("Afternoon", "অপরাহ্নে");

            return time;
        }
        public static string EnlishToBanglaDegreeConvert(string degree)
        {
                if (degree != "" || degree != null)
                {
                    var degreeBn = "";

                    degreeBn = degree.Replace("S.S.C", "এস,এস,সি").Replace("S.S.C Vocational", "এস,এস,সি ভোকেশনাল").Replace("S.S.C Equivalent", "এস,এস,সি সমমান").Replace("Dakhil", "দাখিল").Replace("O Level / Cambridge", "ও লেভেল ক্যামব্রিজ").Replace("Trade Certificate", "ট্রেড সার্টিফিকেট").Replace("H.S.C", "এইচ,এস,সি").Replace("H.S.C Equivalent", "এইচ,এস,সি সমমান").Replace("Alim", "আলিম").Replace("Business Management", "ব্যবসায় ব্যবস্থপনা").Replace("Diploma", "ডিপ্লোমা").Replace("A Level / Sr. Cambridge", "এ লেভেল ক্যামব্রিজ").Replace("Honors", "অনার্স").Replace("Pass Course", "পাস কোর্স").Replace("A & B Section", "এ & বি সেকশন").Replace("BAMS/BHMS/BUMS", "বিএএমএস/বিএইচএমএস/বিইউএমএস").Replace("B.Sc (Agricultural Science)", "বিএসসি কৃষি (সম্মান)").Replace("B.Sc (Engineering/Architecture)", "বিএসসি (ইঞ্জিনিয়ারিং/কৃষি)").Replace("M.B.B.S / B.D.S", "এমবিবিএস/বিডিএস").Replace("Others", "অন্যান্য").Replace("M.A", "এম,এ").Replace("M.Com", "এম.কম").Replace("M.S.S", "এম,এস,এস").Replace("M.Sc/MS", "এমসি/এমএস").Replace("M.Sc", "এম,এস,সি").Replace("L.L.M", "এল,এল,এম").Replace("MBA", "এম,বি,এ").Replace("Engineering", "ইঞ্জিনিয়ারিং").Replace("Computer Science", "কম্পিউটার সাইন্স").Replace("M.P.S", "এম,পি,এস");
                    return degreeBn;
                }
                else
                {
                    return "";
                }
        }
        public static string BanglaToEnglishConvert(string number)
        {
                var en = "0123456789";
                var bn = "০১২৩৪৫৬৭৮৯";

                var bnNum = "";

                for (var i = 0; i < number.Length; i++)
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
    }
}
