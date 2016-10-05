using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TamTam.Services.VideoService
{
    /// <summary>
    /// USed for smart search operation
    /// </summary>
    public class LuceneHelper
    {
        #region | Global Variables |

        private const string _cacheKey = "CacheKey";

        private const string _dataString = "Data";

        private static readonly string _indexDirectory = System.Configuration.ConfigurationManager.AppSettings["LuceneIndexDirectory"];

        private const string _emtptyString = " ";

        //private FSDirectory _ramDirectory = FSDirectory.Open(new System.IO.DirectoryInfo(_indexDirectory));

        /// <summary>
        /// For demo application, data is indexed on RAM. For realworld scenario please make use of FSDirectory type.
        /// </summary>
        //private static RAMDirectory _ramDirectory = null;

        /// <summary>
        /// For types in Lucene assambl that requeires verrsion, simply holds the version not to rewrite everytime.
        /// </summary>
        private Lucene.Net.Util.Version _luceneVersion = Lucene.Net.Util.Version.LUCENE_30;

        #endregion | Global Variables |

        #region | Consructors |

        public LuceneHelper()
        {
            //if (_ramDirectory == null)
            //{
            //    _ramDirectory = new RAMDirectory();
            //}
        }

        #endregion | Consructors |

        #region | Public Methods |

        /// <summary>
        /// Adds data to index. Only cache field names are index. Because all data is stored in cache by a key. So if you have the key you have the data.
        /// </summary>
        /// <param name="initialIndexData"></param>
        public void AddToIndex(string key, object value)
        {
            HashSet<string> stopWords = new HashSet<string>();
            using (StandardAnalyzer analyzer = new StandardAnalyzer(_luceneVersion, stopWords))
            {
                using (FSDirectory dir = FSDirectory.Open(_indexDirectory))
                {
                    using (IndexWriter writer = new IndexWriter(dir, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
                    {
                        string jsonString = JsonConvert.SerializeObject(value);
                        Document docToAdd = new Document();
                        docToAdd.Add(new Field(_cacheKey, key, Field.Store.YES, Field.Index.ANALYZED));
                        docToAdd.Add(new Field(_dataString, jsonString, Field.Store.YES, Field.Index.ANALYZED));
                        writer.AddDocument(docToAdd);
                        writer.Optimize();
                        writer.Commit();
                        writer.Dispose();
                    }
                }                             
            }
        }

        /// <summary>
        /// Searches over index that uses "contains" algorithms.
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public string[] Search(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return new string[0];
            }

            if (!System.IO.Directory.EnumerateFiles(_indexDirectory).Any())
            {
                return new string[0];
            }

            string searchQuery = CreateSearchQuery(searchText);
            HashSet<string> stopWords = new HashSet<string>();
            List<string> result = new List<string>();
            using (StandardAnalyzer analyzer = new StandardAnalyzer(_luceneVersion, stopWords))
            {                
                QueryParser parser = new QueryParser(_luceneVersion, _cacheKey, analyzer);
                Query query = parser.Parse( searchQuery);
                using (FSDirectory dir = FSDirectory.Open(_indexDirectory))
                {
                    using (IndexSearcher searcher = new IndexSearcher(dir))
                    {
                        //Do the search            
                        TopDocs foundItems = searcher.Search(query, int.MaxValue);
                        Document tempDoc = null;
                        if (foundItems != null)
                        {
                            if (foundItems.ScoreDocs != null)
                            {
                                for (int i = 0; i < foundItems.ScoreDocs.Length; i++)
                                {
                                    tempDoc = searcher.Doc(foundItems.ScoreDocs[i].Doc);
                                    result.Add(tempDoc.Get(_cacheKey));
                                }
                            }
                        }
                    }
                }                
            }
            
            return result.ToArray();
        }

        /// <summary>
        /// Searches over index that uses "contains" algorithms.
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public List<KeyValuePair<string, T>> Search<T>(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return default(List<KeyValuePair<string, T>>);
            }

            if (!System.IO.Directory.EnumerateFiles(_indexDirectory).Any())
            {
                return default(List<KeyValuePair<string, T>>);
            }

            string searchQuery = CreateSearchQuery(searchText);
            HashSet<string> stopWords = new HashSet<string>();
            List<KeyValuePair<string, T>> result = new List<KeyValuePair<string, T>>();
            using (StandardAnalyzer analyzer = new StandardAnalyzer(_luceneVersion, stopWords))
            {
                QueryParser parser = new QueryParser(_luceneVersion, _cacheKey, analyzer);
                Query query = parser.Parse(searchQuery);
                using (FSDirectory dir = FSDirectory.Open(_indexDirectory))
                {
                    using (IndexSearcher searcher = new IndexSearcher(dir))
                    {
                        //Do the search            
                        TopDocs foundItems = searcher.Search(query, int.MaxValue);
                        Document tempDoc = null;
                        T tempT = default(T);
                        string tempKey = null;
                        if (foundItems != null)
                        {
                            if (foundItems.ScoreDocs != null)
                            {
                                for (int i = 0; i < foundItems.ScoreDocs.Length; i++)
                                {
                                    tempDoc = searcher.Doc(foundItems.ScoreDocs[i].Doc);
                                    tempKey = tempDoc.Get(_cacheKey);
                                    tempT = JsonConvert.DeserializeObject<T>(tempDoc.Get(_dataString));
                                    result.Add(new KeyValuePair<string, T>(tempKey, tempT));
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }

        #endregion | Public Methods |

        #region | Private Methods |

        /// <summary>
        /// Creates lucene search query
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        private string CreateSearchQuery(string searchText)
        {
            string searchQuery = string.Empty;
            string[] parts = searchText.Split(new string[] { _emtptyString }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < parts.Length; i++)
            {
                searchQuery += parts[i] + "* ";
            }
            searchQuery = searchQuery.Trim();
            return searchQuery;
        }

        #endregion | Private Methods |
    }
}