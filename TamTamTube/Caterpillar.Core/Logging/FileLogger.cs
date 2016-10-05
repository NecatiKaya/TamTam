using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caterpillar.Core.Logging
{
    /// <summary>
    /// Implements ILogger to log to a file
    /// </summary>
    public class FileLogger : ILogger
    {
        #region | Constructors |

        public FileLogger()
        {
            FileDirectory = System.Configuration.ConfigurationManager.AppSettings["FileLogPath"];
            if (string.IsNullOrWhiteSpace(FileDirectory))
            {
                FileDirectory = Environment.CurrentDirectory;
            }
        }

        #endregion | Constructors |

        #region | Properties |
        
        /// <summary>
        /// Gets or sets directory for file logging
        /// </summary>
        public string FileDirectory { get; set; }

        #endregion | Properties |

        #region | ILogger Implementation |

        /// <summary>
        /// Logs message to file
        /// </summary>
        /// <param name="log"></param>
        public void Log(string log)
        {
            string fileName = System.IO.Path.Combine(FileDirectory,string.Format("{0}_{1}.txt", DateTime.Now.Ticks.ToString(), Guid.NewGuid().ToString()));
            if (!System.IO.Directory.Exists(FileDirectory))
            {
                System.IO.Directory.CreateDirectory(FileDirectory);
            }
            System.IO.File.WriteAllText(fileName, log, Encoding.UTF8);
        }

        #endregion | ILogger Implementation |
    }
}
