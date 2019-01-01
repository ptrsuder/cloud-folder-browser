using System.Net;

namespace YandexDiskSharp
{
    public class DiskException : WebException
    {
        public DiskException(Models.Exception exception, System.Exception innerException, WebExceptionStatus status, WebResponse response) : base(exception?.Message, innerException, status, response)
        {
            Description = exception.Description;
            Error = exception.Error;
        }

        #region ~Properties~
        
        /// <summary>
        /// Техническое описание ошибки.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Уникальный код ошибки.
        /// </summary>
        public string Error { get; }

        #endregion
    }
}
