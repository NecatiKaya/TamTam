using System;
using System.Collections.Generic;

namespace Caterpillar.Core.Security
{
    public class CaterpillarPrincipalForSerialization
    {
        #region | Properties |

        public string UserName { get; set; }

        public Guid UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<string> Rights { get; set; }

        public Guid PersonId { get; set; }

        #endregion | Properties |

        #region | Constructors |

        public CaterpillarPrincipalForSerialization()
        {
            this.Rights = new List<string>();
        }

        #endregion | Constructors |

        public CaterpillarPrincipal MapCaterpillarPrincipal()
        {
            CaterpillarPrincipal principal = new CaterpillarPrincipal(this.UserName);
            principal.FirstName = this.FirstName;
            principal.LastName = this.LastName;
            principal.UserId = this.UserId;
            principal.UserName = this.UserName;
            principal.Rights.AddRange(this.Rights);
            principal.PersonId = this.PersonId;
            return principal;
        }
    }
}
