using HotChocolate;

namespace socialize_api.GraphQL
{
    /// <summary>
    /// Handles the custom errors to be returned from GraphQL.
    /// </summary>
    public class ErrorFilter : IErrorFilter
    {
        #region Methods
        /// <summary>
        /// returns the error with custom error message when any error is raised.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns>The error.</returns>
        public IError OnError(IError error)
        {
            return error.WithMessage(error.Exception.Message);
        }
        #endregion Methods
    }
}
