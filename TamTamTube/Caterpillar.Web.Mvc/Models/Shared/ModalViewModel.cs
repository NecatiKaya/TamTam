using Caterpillar.Core.Collections;
using System.Collections.Generic;

namespace Caterpillar.Web.Mvc.Models.Shared
{
    public class ModalViewModel
    {
        public ModalViewModel()
        {
            this.Errors = new StringToStringDictionary();
            /// TODO nkaya language service
            this.ErrorTitle = "Hata listesi aşağıdaki gibidir:";
        }

        public bool IsSuccess { get; set; }
        public string Header { get; set; }
        public string ErrorTitle { get; set; }
        public StringToStringDictionary Errors { get; set; }
        public object Data { get; set; }
        public string SuccessMessage { get; set; }

        ///TODO nkaya, Language servis implemente edilecek
        public static ModalViewModel ForSuccess(string successMessage = "İşleminiz başarıyla yapıldı.") 
        {
            ModalViewModel model = new ModalViewModel();
            ///TODO nkaya, Language servis implemente edilecek
            model.Header = "İşlem Sonucu - Başarılı";
            model.ErrorTitle = null;
            model.IsSuccess = true;
            model.Errors.Clear();
            model.SuccessMessage = successMessage;
            return model;
        }

        public static ModalViewModel ForError(string errorCode, string errorText, string title = null)
        {
            ModalViewModel model = PrepareForError();
            if (!string.IsNullOrWhiteSpace(title))
            {
                model.ErrorTitle = title;
            }
            model.Errors.Add(errorCode, errorText);
            return model;
        }

        public static ModalViewModel ForError(StringToStringDictionary errors, string title = null)
        {
            ModalViewModel model = PrepareForError();
            foreach (KeyValuePair<string, string> error in errors)
            {
                model.Errors.Add(error.Key, error.Value);
            }
            if (!string.IsNullOrWhiteSpace(title))
            {
                model.ErrorTitle = title;
            }
            return model;
        }

        private static ModalViewModel PrepareForError()
        {
            ModalViewModel model = new ModalViewModel();
            ///TODO nkaya, Language servis implemente edilecek
            model.Header = "İşlem Sonucu - Hata";
            model.IsSuccess = false;
            model.SuccessMessage = null;
            return model;
        }
    }
}