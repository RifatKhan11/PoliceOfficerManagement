namespace PoliceOfficerManagement.Helpers
{
    public class MonthYear
    {
        public static ReturnMonthYear GetYearAndMonth(string monthYear)
        {
            var year = monthYear.Split("-")[1];
            var month = monthYear.Split("-")[0];
            var returnData = new ReturnMonthYear
            {
                year = Convert.ToInt32(year)
            };
            if (month == "Jan" || month == "January")
            {
                returnData.month = 1;
            }
            else if (month == "Feb" || month == "February")
            {
                returnData.month = 2;
            }
            else if (month == "Mar" || month == "March")
            {
                returnData.month = 3;
            }
            else if (month == "Apr" || month == "April")
            {
                returnData.month = 4;
            }
            else if (month == "May")
            {
                returnData.month = 5;
            }
            else if (month == "Jun" || month == "June")
            {
                returnData.month = 6;
            }
            else if (month == "Jul" || month == "July")
            {
                returnData.month = 7;
            }
            else if (month == "Aug" || month == "August")
            {
                returnData.month = 8;
            }
            else if (month == "Sep" || month == "September")
            {
                returnData.month = 9;
            }
            else if (month == "Oct" || month == "October")
            {
                returnData.month = 10;
            }
            else if (month == "Nov" || month == "November")
            {
                returnData.month = 11;
            }
            else if (month == "Dec" || month == "December")
            {
                returnData.month = 12;
            }
            return returnData;
        }
        public static string MonthNumberToMonthName(int month)
        {
            string[] monthName = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            string monthNamedata = monthName[month-1];
            return monthNamedata;
        }
    }
    public class ReturnMonthYear
    {
        public int month { get; set; }
        public int year { get; set; }
    }
}
