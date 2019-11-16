using OpenPop.Pop3;
using System;
using System.Threading;

namespace Automation.Utility.Email
{
    public class EmailUtil
    {
        private static Pop3Client ConnectToServer(string server, string userName, string password)
        {
            Pop3Client pop3Client = new Pop3Client();

            bool isConnect = false;
            int connectTryCount = 0;
            while (!isConnect)
            {
                try
                {
                    pop3Client.Connect(server, 995, true);
                    isConnect = true;
                }
                catch (Exception)
                {
                    Thread.Sleep(5000);
                    if (connectTryCount++ > 5)
                    {
                        break;
                    }
                }
            }

            pop3Client.Authenticate(userName, password, AuthenticationMethod.UsernameAndPassword);
            return pop3Client;
        }

        public static string GetVerificationCodeFromEmail(string server, string userName, string password, string subjectLine)
        {
            string verficationCode = string.Empty;
            var pop3Client = ConnectToServer(server, userName, password);

            try
            {
                int count = pop3Client.GetMessageCount();

                for (int i = count; i > 0; i--)
                {
                    var message = pop3Client.GetMessage(i);

                    System.Net.Mail.MailMessage oMail = message.ToMailMessage();

                    if (oMail.Subject.Contains(subjectLine))
                    {
                        string body = oMail.Body;
                        string codeHint = "Verification Code: ";

                        int startIndex = body.IndexOf(codeHint) + codeHint.Length;

                        while (char.IsNumber(body[startIndex]))
                        {
                            verficationCode += body[startIndex++];
                        }

                        if (verficationCode.Length > 0)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ep)
            {
                Console.WriteLine(ep.Message);
            }

            return verficationCode;
        }


        public static void GetPasswordLinkFromEmail(string server, string userName, string password, string subjectLine, out string docLink, out string docPassword)
        {
            docPassword = string.Empty;
            docLink = string.Empty;

            var pop3Client = ConnectToServer(server, userName, password);

            try
            {
                int count = pop3Client.GetMessageCount();

                for (int i = count; i > 0; i--)
                {
                    var message = pop3Client.GetMessage(i);

                    System.Net.Mail.MailMessage oMail = message.ToMailMessage();

                    if (oMail.Subject.Contains(subjectLine))
                    {
                        string bodyText = string.Empty;

                        var body = message.FindFirstHtmlVersion();

                        if (body != null)
                        {
                            bodyText = body.GetBodyAsText();
                        }
                        else
                        {
                            body = message.FindFirstPlainTextVersion();
                            if (body != null)
                            {
                                bodyText = body.GetBodyAsText();
                            }
                        }

                        string docPasswordHint = "Password:&nbsp;";

                        int startIndex = bodyText.IndexOf(docPasswordHint) + docPasswordHint.Length;

                        while (char.IsNumber(bodyText[startIndex]))
                        {
                            docPassword += bodyText[startIndex++];
                        }



                        int linkIndex = bodyText.IndexOf("Link:");
                        string href = "href=";
                        int hrefIndex = bodyText.IndexOf(href, linkIndex) + href.Length + 1;
                        int attrbIndex = bodyText.IndexOf('\"', hrefIndex);

                        if (attrbIndex < 0)
                        {
                            attrbIndex = bodyText.IndexOf('\'', hrefIndex);
                        }

                        docLink = bodyText.Substring(hrefIndex, attrbIndex - hrefIndex);

                        if (docPassword.Length > 0)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ep)
            {
                Console.WriteLine(ep.Message);
            }
        }
    }
}
