using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.TempateFull.Application
{
    public  class ApplicationConfiguration(IConfiguration configuration)
    {
        public string GetServiceName() =>  configuration.GetValue<string>("ServiceName")!;
        
    }
}
