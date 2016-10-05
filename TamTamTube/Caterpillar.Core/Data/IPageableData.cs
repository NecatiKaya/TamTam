
namespace Caterpillar.Core.Data
{
    /// <summary>
    /// Contains data paging information for data paging operations in RDMS
    /// </summary>
    public interface IPageableData
    {
        /// <summary>
        /// Gets or sets page index shows currnet page number
        /// </summary>
        int PageIndex { get; set; }

        /// <summary>
        /// Gets or sets total row count in one age.
        /// </summary>
        int RowCount { get; set; }

        /// <summary>
        /// Gets or sets total row count of data.
        /// </summary>
        int TotalRowCount { get; set; }
    }
}
