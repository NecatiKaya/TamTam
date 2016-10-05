using System;
using System.Data;

namespace Caterpillar.Core.Data
{
    public class AdoNetUnitOfWork : IUnitOfWork, IDisposable
    {
        #region | Private Members |

        private bool _disposed = false;

        private IDbConnection _connection;

        private bool _ownsConnection = false;

        private IDbTransaction _transaction;

        #endregion | Private Members |

        #region | Constructors And Finalizers |

        public AdoNetUnitOfWork(IDbConnection connection, bool ownsConnection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            _connection = connection;
            _ownsConnection = ownsConnection;
            _transaction = connection.BeginTransaction();
        }

        // finalizer simply calls Dispose(false)
        ~AdoNetUnitOfWork()
        {
            Dispose(false);
        }

        #endregion | Constructors And Finalizers |

        #region | IUnitOfWork Implementation |

        public IDbCommand CreateCommand() 
        {
            if (this._connection == null)
            {
                throw new ArgumentNullException("connection");
            }

            return this._connection.CreateCommand();
        }

        public IDbTransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            if (this._connection == null)
            {
                throw new InvalidOperationException("Connection is not initialized. Check your connection.");
            }

            this._connection.Open();
            return _connection.BeginTransaction(isolationLevel);
        }

        public void SaveChanges()
        {
            if (_transaction == null)
                throw new InvalidOperationException("Transaction have already been commited. Check your transaction handling.");

            _transaction.Commit();
            _transaction.Dispose();
            _transaction = null;
        }

        #endregion | IUnitOfWork Implementation |

        #region | IDisposable Implementation |

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion | IDisposable Implementation |

        #region | Protected Members |

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                // if this is a dispose call dispose on all state you
                // hold, and take yourself off the Finalization queue.
                if (disposing)
                {
                    /* Disposing managed resources [*/

                    if (_transaction != null)
                    {
                        _transaction.Rollback();
                        _transaction.Dispose();
                        _transaction = null;
                    }

                    if (_connection != null && _ownsConnection)
                    {
                        _connection.Close();
                        _connection.Dispose();
                        _connection = null;
                    }

                    /*] Disposing managed resources */
                }

                // free your own state (unmanaged objects)
                //AdditionalCleanup();

                this._disposed = true;
            }
        }

        #endregion | Protected Members |
    }
}
