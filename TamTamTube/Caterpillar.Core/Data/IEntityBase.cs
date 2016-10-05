using System;

namespace Caterpillar.Core.Data
{
    /// <summary>
    ///    All entity base properities
    /// </summary>
    public interface IEntityBase
    {
        bool IsActive { get; set; }

        string CreatedBy { get; set; }

        Nullable<DateTime> CreatedDate { get; set; }

        string UpdatedBy { get; set; }

        Nullable<DateTime> UpdateDate { get; set; }
    }
}
