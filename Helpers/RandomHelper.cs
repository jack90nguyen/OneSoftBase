using System.Text;

namespace OneSoft.Helpers;

public class RandomHelper
{
  private static readonly Random _random = new();
  private static readonly string _colors = "0123456789abcdef";
  private static readonly string _characters = "0123456789abcdefghijklmnopqrstuvwxyz";
  private static readonly string _lastname = "Nguyễn Trần Lê Phạm Hoàng Huỳnh Phan Võ Đặng Bùi Đỗ Hồ Ngô Dương Lý";
  private static readonly string _firstname = "Huy Khang Bảo Minh Phúc Anh Khoa Phát Đạt Khôi Long Nam Duy Quân Kiệt Thịnh Tuấn Hưng Hoàng Hiếu Nhân Trí Tài Phong Nguyên An Phú Thành Đức Dũng Lộc Khánh Vinh Tiến Nghĩa Thiện Hào Hải Đăng Quang Lâm Nhật Trung Thắng Tú Hùng Tâm Sang Sơn Thái Cường Vũ Toàn Ân Thuận Bình Trường Danh Kiên Phước Thiên Tân Việt Khải Tín Dương Tùng Quý Hậu Trọng Triết Luân Phương Quốc Thông Khiêm Hòa Thanh Tường Kha Vỹ Bách Khanh Mạnh Lợi Đại Hiệp Đông Nhựt Giang Kỳ Phi Tấn Văn Vương Công Hiển Linh Ngọc Vĩ";
  private static readonly string[] _quotes = new string[]
  {
    "Chào buổi sáng, ngày mới, hãy tận dụng tối đa thời gian trong ngày và sử dụng nó một cách khôn ngoan hôm nay là duy nhất. ",
    "Chào buổi sáng, chào ngày mới, chào sự khởi đầu mới, mong ước mới, hy vọng mới. ",
    "Một ngày mới mang theo những hi vọng mới, hãy cười tươi lên nhé. ",
    "Ngày mới giống như tờ giấy trắng và bạn sẽ là người vẽ lên đó. Vì vậy, hãy vẽ sao cho mình cho một bức tranh đẹp nhé. ",
    "Bạn có biết, ngày hôm nay luôn là một ngày tuyệt vời để tạo ra một ngày hôm qua tốt đẹp hơn cho ngày mai. Vì thế, chúng ta hãy sống trọn vẹn cho ngày hôm nay nhé. ",
    "Ranh giới giữa một ngày tồi tệ và tốt lành rất mỏng manh. Ngày hôm nay có tươi đẹp hay không là do chính thái độ của bạn quyết định. ",
    "Nếu bạn không rời khỏi chiếc giường êm ấm và bắt đầu hành động thì bạn sẽ chẳng bao giờ thực hiện được giấc mơ của mình cả. ",
    "Hãy bắt đầu ngày hôm nay bằng niềm vui, hạnh phúc và quên đi cuộc chiến mà chúng ta đã có ngày hôm qua bởi hôm qua đã là quá khứ. ",
    "Mỗi một ngày mới đến là một cơ hội để chúng ta làm nhiều điều tốt đẹp hơn trong cuộc sống. ",
    "Cái nhìn của bạn sẽ quyết định hôm nay là một ngày tuyệt vời hay một ngày kinh hoàng. Vì vậy hãy luôn lạc quan vui vẻ nhé vì thử thách cũng chính là cơ hội và mọi thứ rồi sẽ tốt đẹp cả thôi. ",
  };


  // <summary>
  /// Tạo một số ngẫu nhiên từ min đến max
  /// </summary>
  public static int Number(int min, int max)
  {
    return _random.Next(min, max + 1);
  }

  /// <summary>
  /// Hàm tạo mã tùy chọn độ dài
  /// </summary>
  public static string Code(int length)
  {
    var result = new StringBuilder();
    for (int i = 0; i < length; i++)
      result.Append(_characters[Number(0, 35)]);
    return result.ToString().ToUpper();
  }

  /// <summary>
  /// Hàm tạo ID mặc định: 3[Date] + 5[Random]
  /// </summary>
  public static string ID()
  {
    return ID(8);
  }

  /// <summary>
  /// Hàm tạo ID tùy chọn độ dài
  /// </summary>
  public static string ID(int length)
  {
    var date = DateTime.Today;
    var result = new StringBuilder();
    result.Append(_characters[date.Year - 2020]);
    result.Append(_characters[date.Month]);
    result.Append(_characters[date.Day]);
    result.Append(Code(length - 3));
    return result.ToString().ToUpper();
  }

  /// <summary>
  /// Hàm tạo mã màu ngẫu nhiên
  /// </summary>
  public static string Color()
  {
    string result = "#";
    for (int i = 0; i < 6; i++)
      result += _colors[Number(0, 15)];
    return result.ToString();
  }

  /// <summary>
  /// Hàm tạo tên ngẫu nhiên
  /// </summary>
  public static string FristName()
  {
    var data = _firstname.Split(' ');
    return data[Number(0, data.Length - 1)];
  }

  /// <summary>
  /// Hàm tạo họ ngẫu nhiên
  /// </summary>
  public static string LastName()
  {
    var data = _lastname.Split(' ');
    return data[Number(0, data.Length - 1)];
  }

  /// <summary>
  /// Hàm tạo họ tên ngẫu nhiên
  /// </summary>
  public static string FullName()
  {
    var last = LastName();
    var middle = FristName();
    var frist = FristName();
    return $"{last} {middle} {frist}";
  }

  /// <summary>
  /// Hàm tạo số điện thoại ngẫu nhiên
  /// </summary>
  public static string Phone()
  {
    return "09" + Number(10000000, 99999999);
  }

  /// <summary>
  /// Hàm tạo email ngẫu nhiên
  /// </summary>
  public static string Email()
  {
    var result = FullName().Replace(" ", "").ToLower();
    return StringHelper.VnNoSigns(result) + "@gmail.com";
  }

  /// <summary>
  /// Hàm tạo thời gian ngẫu nhiên
  /// </summary>
  public static DateTime Date()
  {
    var min = new DateTime(1990, 1, 1);
    return Date(min, DateTime.Today);
  }

  /// <summary>
  /// Hàm tạo thời gian ngẫu nhiên
  /// </summary>
  public static DateTime Date(DateTime min, DateTime max)
  {
    long tick = _random.NextInt64(min.Ticks, max.Ticks);
    DateTime result = new(tick);
    return result;
  }

  /// <summary>
  /// Hàm tạo đoạn văn bản ngẫu ngẫu nhiên
  /// </summary>
  public static string Words(int length)
  {
    var result = new StringBuilder();
    for (int i = 0; i < length; i++)
      result.Append(_quotes[Number(0, 9)]);
    return result.ToString();
  }


}
