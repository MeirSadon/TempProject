using System;
using System.Runtime.Serialization;

namespace FlightManagementProject.DAO
{
    [Serializable]
    public class UserNameIsAlreadyExistException : ApplicationException
    {
        public UserNameIsAlreadyExistException()
        {
        }

        public UserNameIsAlreadyExistException(string message) : base(message)
        {
        }

        public UserNameIsAlreadyExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserNameIsAlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}