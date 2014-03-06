using System;

namespace Reconfig.Domain.Exceptions
{
    public class ParsingException : ApplicationException
    {
        public ParsingException(string template, string details)
            : base(string.Format("An error has occurred while processing the template '{0}'. Please double check the template definition and/or the provided tokens. Details: {1}", template, details))
        {
        }
    }
}
