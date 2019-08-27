using System;

namespace LeagueViewer.Repository
{
    public class PersistentStoreException : Exception
    {
        public PersistentStoreException() : base()
        {

        }

        public PersistentStoreException(string message) : base(message)
        {

        }

        public PersistentStoreException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
