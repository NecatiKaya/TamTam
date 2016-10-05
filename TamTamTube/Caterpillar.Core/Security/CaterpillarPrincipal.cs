using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace Caterpillar.Core.Security
{
    public class CaterpillarPrincipal : IPrincipal
    {
        #region | Properties |

        public Guid PersonId { get; set; }

        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<string> Rights { get; set; }

        #endregion | Properties |

        #region | Constructors |

        public CaterpillarPrincipal(string userName)
        {
            this.Identity = new GenericIdentity(userName);
            this.Rights = new List<string>();
            this.UserName = userName;
        }

        #endregion | Constructors |

        #region | IPrincipal Implementation |

        public IIdentity Identity { get; private set; }

        public bool IsInRole(string rights)
        {
            return this.Rights.Contains(rights);
        }

        #endregion | IPrincipal Implementation |
    }
}
