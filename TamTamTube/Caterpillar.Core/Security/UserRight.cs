using System;

namespace Caterpillar.Core.Security
{
    public class UserRight
    {
        //Aşağıdaki property'e gerek yok. Zaten kullanıcı nesnesinin içine eklenecek bu nesne.
        //public Guid UserId { get; set; }
        public Guid RightId { get; set; }

        public string RightName { get; set; }
    }
}
