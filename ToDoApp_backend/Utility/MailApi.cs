using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp_backend.Utility
{
    public class MailApi
    {

        public MailApi() { }

        /// <summary>
        /// メール送信
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <param name="fromAddress">送信元のメールアドレス</param>
        /// <param name="toAddress">送信先のメールアドレス</param>
        /// <param name="from">送信者名</param>
        /// <param name="to">受信者</param>
        public static void SendMail(long userId, string fromAddress, string toAddress, string to)
        {
            try
            {
                var body = new StringBuilder();
                body.AppendLine("TODOリスト　新規アカウント作成完了のお知らせ\n");
                body.AppendLine($"{to}様\n");
                body.AppendLine("\n");
                body.AppendLine("新規アカウント作成完了致しました。以下に新規ユーザーIDを記載します。\n");
                body.AppendLine($"【UserId】：{userId}\n");
                body.AppendLine("ご確認よろしくお願いします。\n");

                using (MailMessage msg = new MailMessage
                {
                    From = new MailAddress(fromAddress, "自動送信メール"),
                    Subject = "新規アカウント作成完了のお知らせ",
                    Body = body.ToString()
                })
                {
                    msg.To.Add(new MailAddress(toAddress, to));

                    SmtpClient sc = new SmtpClient();
                    if (toAddress.Contains("gmail.com"))
                    {
                        sc = new SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 587,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            Credentials = new System.Net.NetworkCredential(toAddress, "7215MKIA"),
                            EnableSsl = true
                        };
                    }
                    else
                    {
                        sc = new SmtpClient
                        {
                            //SMTPサーバーなどを設定する
                            Host = "localhost",
                            Port = 25,
                            DeliveryMethod = SmtpDeliveryMethod.Network
                        };
                    }

                    sc.Send(msg);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

    }
}
