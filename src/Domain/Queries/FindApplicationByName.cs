using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reconfig.Common.CQRS;
using Reconfig.Domain.Model;

namespace Reconfig.Domain.Queries
{
    public class FindApplicationByName : IQuery<Application>
    {
        public FindApplicationByName(string appName)
        {
            ApplicationName = appName;
        }

        public string ApplicationName { get; set; }
    }
}
