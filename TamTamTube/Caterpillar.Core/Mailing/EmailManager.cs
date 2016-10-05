using Caterpillar.Core.Collections;
using System;

namespace Caterpillar.Core.Mailing
{
    public sealed class EmailManager
    {
        public EmailModelBase EmailModel { get; set; }

        public EmailManager(EmailModelBase emailModel)
        {
            this.EmailModel = emailModel;
        }

        public string PrepareEmailTemplate(string emailTemplateHtml, string placeHolderCodes, StringToStringDictionary emailData, string placeHoldersSeperator = "|") 
        {
            string[] _placeHolders = placeHolderCodes.Split(new string[] { placeHoldersSeperator }, StringSplitOptions.RemoveEmptyEntries);
            string emailAsHtml = PrepareEmailTemplate(emailTemplateHtml, _placeHolders, emailData);
            return emailAsHtml;
        }

        public string PrepareEmailTemplate(string emailTemplateHtml, string[] placeHolderCodes, StringToStringDictionary emailData)
        {
            if (placeHolderCodes != null && placeHolderCodes.Length > 0 && emailData != null && emailData.Count > 0)
            {
                foreach (string eachPlaceHolder in placeHolderCodes)
                {
                    emailTemplateHtml = emailTemplateHtml.Replace(eachPlaceHolder, emailData[eachPlaceHolder]);
                }
            }
            return emailTemplateHtml;
        }
    }
}
