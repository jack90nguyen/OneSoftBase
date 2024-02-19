using System.Net;
using System.Net.Mail;

namespace AppCore.Services;

public class EmailService
{
  private static string MailSend = "noreply.onetez@gmail.com";
  private static string MailPass = "iktlfiwuejuylfat";
  private static string MailServer = "smtp.gmail.com";
  private static int MailPort = 587;


  /// <summary>
  /// Hàm gửi email
  /// </summary>
  private static bool SendMail(string email, string title, string content, string[] bcc, out string msg)
  {
    try
    {
      if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(email.Trim()))
      {
        email = email.Trim();

        MailMessage mailMessage = new MailMessage();
        mailMessage.From = new MailAddress(MailSend, "FASTDO");
        mailMessage.To.Add(email);
        mailMessage.Subject = title;
        mailMessage.Body = content;
        mailMessage.IsBodyHtml = true;

        if (bcc != null && bcc.Count() > 0)
        {
          foreach (string b in bcc)
          {
            if (!string.IsNullOrEmpty(b.Trim()))
              mailMessage.Bcc.Add(b.Trim());
          }
        }

        SmtpClient mailClient = new SmtpClient(MailServer, MailPort);
        mailClient.Timeout = 15000;
        mailClient.EnableSsl = true;
        mailClient.Credentials = new NetworkCredential(MailSend, MailPass);
        mailClient.Send(mailMessage);

        msg = "Đã gửi email thành công!";
        return true;
      }
      else
      {
        msg = "Không có email nhận";
      }
    }
    catch (Exception ex)
    {
      msg = "Không thể gửi email: " + ex.Message;
    }
    return false;
  }


  /// <summary>
  /// Gửi email xác thực quên mật khẩu
  /// </summary>
  public static bool ForgotPassword(string email, string code, out string msg)
  {
    string title = "FASTDO | Mã xác thực tài khoản";
    string content = $"<br>Tên đăng nhập: {email}";
    content += $"<br>Mã xác thực: {code}";

    return SendMail(email, title, content, null, out msg);
  }
}