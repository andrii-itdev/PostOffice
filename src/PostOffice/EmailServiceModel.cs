using FluentEmail.Core;
using FluentEmail.Smtp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PostOffice
{
    public class EmailServiceModel
    {
        public MailAddress From { get; protected set; }
        public string Password { get; protected set; }
        public MailAddress To { get; protected set; }
        public string Title { get; protected set; }
        public string Content { get; protected set; }

        public EmailServiceModel(string fromEmail, string password, string to, string title, string content)
        {
            this.From = new MailAddress(fromEmail);
            this.Password = password;
            this.To = new MailAddress(to);
            this.Title = title;
            this.Content = content;
        }

        public void TrySendEmail()
        {
            try
            {
                SendEmail();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"{ex.ToString()}", "Error sending email");
            }
        }

        public void SendEmail()
        {
            var sender = new SmtpSender(() => new System.Net.Mail.SmtpClient("smtp." + From.Host)
            {
                EnableSsl = true,
                DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(this.From.Address, this.Password),
                Port = 587,
            }
            );

            Email.DefaultSender = sender;

            var email = Email
                .From(this.From.Address)
                .To(this.To.Address)
                .Subject(this.Title)
                .Body(this.Content)
                .Send();
        }
    }
}
