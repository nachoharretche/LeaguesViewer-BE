using System;
using System.Collections.Generic;
using System.Text;

namespace LeagueViewer.Services
{
    public class BusinessLogicException : Exception
    {
        public BusinessLogicException() : base()
        {

        }
        public BusinessLogicException(string message) : base(message)
        {

        }
        public BusinessLogicException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
