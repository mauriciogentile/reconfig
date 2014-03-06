using System;

namespace Reconfig.Domain.Exceptions
{
    public class TemplateNotFoundException : Exception
    {
        public TemplateNotFoundException(string templateName)
            : base(string.Format("Template '{0}' was not found", templateName))
        {
        }
    }
}
