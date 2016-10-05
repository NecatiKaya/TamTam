using System.IO;
using System.Web;
using System.Web.Optimization;

namespace Caterpillar.Web.Extensions
{
    public static class BundleConfigExtensions
    {
        public static Bundle IncludeSubdirectoriesOf(this Bundle bundle, string path, string searchPattern)
        {
            // Get the current and root paths for `DirectoryInfo`.
            var absolutePath = HttpContext.Current.Server.MapPath(path);
            var rootPath = HttpContext.Current.Server.MapPath("~/");
            var directoryInfo = new DirectoryInfo(absolutePath);
            foreach (var directory in directoryInfo.GetDirectories())
            {
                // Swap out the absolute path format for a URL.
                var directoryPath = directory.FullName;
                directoryPath = directoryPath
                  .Replace(rootPath, "~/")
                  .Replace('\\', '/');
                bundle.IncludeDirectory(directoryPath, searchPattern);
                bundle.IncludeSubdirectoriesOf(directoryPath, searchPattern);
            }
            return bundle;
        }
    }
}
