using SMC.Academico.Service.Ioc;
using SMC.Framework.Service;

namespace SMC.Academico.ServiceHost.Ioc
{
    public class ServiceHostIocMapping : SMCServiceHostIocMapping
    {
        protected override void Configure()
        {
            this.Services
                .Map<ServiceIocMapping>();
        }
    }
}