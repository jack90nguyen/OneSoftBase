
namespace AppCore.Helpers;

public class DateHelper
{
  /// <summary>
  /// Đổi cách hiển thị thời gian thành khoản thời gian
  /// </summary>
  public static string ConvertToTime(DateTime? date)
  {
    DateTime now = DateTime.Now;
    string postTime = string.Empty;
    if (date != null)
    {
      if (DateTime.Compare(date.Value, now) <= 0)
      {
        TimeSpan spanMe = now.Subtract(date.Value);
        if (spanMe.Days < 1)
        {
          if (spanMe.Hours < 1)
          {
            if (spanMe.Minutes < 1)
            {
              if (spanMe.Seconds < 5)
                postTime = "vừa xong";
              else
                postTime = spanMe.Seconds + " giây trước";
            }
            else
              postTime = spanMe.Minutes + " phút trước";
          }
          else
            postTime = spanMe.Hours + " giờ trước";
        }
        else if (spanMe.Days < 30)
        {
          postTime = spanMe.Days + " ngày trước";
        }
        else if (spanMe.Days < 365)
        {
          postTime = Convert.ToInt32(spanMe.Days / 30) + " tháng trước";
        }
        else if (spanMe.Days > 365)
        {
          postTime = Convert.ToInt32(spanMe.Days / 365) + " năm trước";
        }
      }
      else
      {
        TimeSpan spanMe = date.Value.Subtract(now);
        if (spanMe.Days < 1)
        {
          if (spanMe.Hours < 1)
          {
            if (spanMe.Minutes < 1)
            {
              if (spanMe.Seconds < 5)
                postTime = "bây giờ";
              else
                postTime = spanMe.Seconds + " giây nữa";
            }
            else
              postTime = spanMe.Minutes + " phút nữa";
          }
          else
            postTime = spanMe.Hours + " giờ nữa";
        }
        else if (spanMe.Days < 30)
        {
          postTime = spanMe.Days + " ngày nữa";
        }
        else if (spanMe.Days < 365)
        {
          postTime = Convert.ToInt32(spanMe.Days / 30) + " tháng nữa";
        }
        else if (spanMe.Days > 365)
        {
          postTime = Convert.ToInt32(spanMe.Days / 365) + " năm nữa";
        }
      }
    }

    return postTime;
  }

  /// <summary>
  /// Chuyển thời gian thành thứ trong tuần bằng tiếng Việt
  /// </summary>
  public static string ConvertWeekdays(DateTime value)
  {
    var date = value.ToString("ddd");

    if (date.Contains("Mon"))
      return date.Replace("Mon", "T2");
    else if (date.Contains("Tue"))
      return date.Replace("Tue", "T3");
    else if (date.Contains("Wed"))
      return date.Replace("Wed", "T4");
    else if (date.Contains("Thu"))
      return date.Replace("Thu", "T5");
    else if (date.Contains("Fri"))
      return date.Replace("Fri", "T6");
    else if (date.Contains("Sat"))
      return date.Replace("Sat", "T7");
    else
      return date.Replace("Sun", "CN");
  }

  /// <summary>
  /// Đổi cách hiển thị thời gian, có thứ trong tuần và ngày
  /// </summary>
  public static string ConvertDateWeek(long tick)
  {
    var value = new DateTime(tick);
    return string.Format("{0} - {1:dd/MM/yyyy}", ConvertWeekdays(value), value);
  }

  /// <summary>
  /// Danh sách ngày trong tháng, dùng cho lịch
  /// </summary>
  public static List<List<long>> DaysInMonth(DateTime date)
  {
    var start = Convert.ToDateTime(string.Format("{0:yyyy-MM-01}", date));
    var end = start.AddMonths(1).AddDays(-1);

    var list = new List<long>();
    if (date.DayOfWeek == DayOfWeek.Tuesday)
      list = new List<long>() { 0 };
    else if (date.DayOfWeek == DayOfWeek.Wednesday)
      list = new List<long>() { 0, 0 };
    else if (date.DayOfWeek == DayOfWeek.Thursday)
      list = new List<long>() { 0, 0, 0 };
    else if (date.DayOfWeek == DayOfWeek.Friday)
      list = new List<long>() { 0, 0, 0, 0 };
    else if (date.DayOfWeek == DayOfWeek.Saturday)
      list = new List<long>() { 0, 0, 0, 0, 0 };
    else if (date.DayOfWeek == DayOfWeek.Sunday)
      list = new List<long>() { 0, 0, 0, 0, 0, 0 };
    for (DateTime d = start; d <= end; d = d.AddDays(1))
      list.Add(d.Ticks);

    var results = new List<List<long>>();

    int day = 0;
    while (day < list.Count)
    {
      var week = new List<long>();
      for (int i = 0; i < 7; i++)
      {
        week.Add(list[day]);
        day++;
        if (day == list.Count)
        {
          while (i + 1 < 7)
          {
            week.Add(0);
            i++;
          }
          break;
        }
      }
      results.Add(week);
    }

    return results;
  }

  /// <summary>
  /// Mốc thời gian trong ngày
  /// </summary>
  public static List<string> TimesInDay(int hourMin, int hourMax)
  {
    var results = new List<string>();
    for (int hour = hourMin; hour <= hourMax; hour++)
    {
      for (int minute = 0; minute < 60; minute += 5)
      {
        results.Add(string.Format("{0:00}:{1:00}", hour, minute));
      }
    }
    return results;
  }

}
