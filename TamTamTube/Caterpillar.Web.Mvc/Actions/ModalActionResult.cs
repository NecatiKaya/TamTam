using System.Web.Mvc;

namespace Caterpillar.Web.Mvc.Actions
{
    public class ModalActionResult : PartialViewResult
    {
        public ModalActionResult()
        {

        }

        public ModalActionResult(object model)
        {
            this.ViewData = new ViewDataDictionary(model);
            this.ViewName = "_ModalPartilaView";
        }
    }
}
