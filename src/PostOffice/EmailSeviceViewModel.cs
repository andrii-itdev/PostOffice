using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PostOffice
{
    public class EmailSeviceViewModel
    {
        public string From { get; set; }
        public string Password { get; set; }
        public string To { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public ICommand SendCommand { get; set; }

        public EmailSeviceViewModel()
        {
            SendCommand = new RelayCommand<object>(OnSendExecute);
        }

        private void OnSendExecute(object sender)
        {
            EmailServiceModel serviceModel = new EmailServiceModel( 
                this.From, this.Password, this.To, this.Title, this.Content );
            Task.Run(serviceModel.TrySendEmail);
        }
    }
}
