using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caterpillar.Web.Rest
{
    /// <summary>
    /// Contains type of Http methods (verbs) 
    /// </summary>
    public enum RestMethod
    {
        Delete = 1,
        Get = 2,
        Head = 3,
        Options = 4,
        Post = 5,
        Put = 6,
        Trace = 7
    }
}
