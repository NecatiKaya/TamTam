using System.Collections.Generic;

namespace Caterpillar.Core.Mailing
{
    public class EmailModelBase
    {
        public EmailModelBase()
        {
            this.To = new List<string>();
            this.CC = new List<string>();
            this.BCC = new List<string>();
        }

        public virtual string TemplateName { get; set; }

        public string Subject { get; set; }

        public string From { get; set; }

        private List<string> _to;
        public List<string> To
        {
            get { return _to ?? (_to = new List<string>()); }
            set { _to = value; }
        }

        private List<string> _cc;
        public List<string> CC
        {
            get { return _cc ?? (_cc = new List<string>()); }
            set { _cc = value; }
        }

        private List<string> _bcc;
        public List<string> BCC
        {
            get { return _bcc ?? (_bcc = new List<string>()); }
            set { _bcc = value; }
        }

        public EmailSettings EmailSetting { get; set; }
    }
}
