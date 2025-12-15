using SMC.Academico.Common;
using SMC.Framework.Domain;
using System.Configuration;

namespace SMC.Academico.Domain
{
    public class AcademicoContextDomain<TEntity> : SMCDomainServiceBase<TEntity> where TEntity : class
    {
        public AcademicoContextDomain() : base(AcademicoContextDomain.EF)
        {

        }
    }


    public class AcademicoContextDomain : SMCDomainServiceBase
    {
        public static string EF = ConfigurationManager.AppSettings["EFContext"];

        public AcademicoContextDomain() : base(EF)
        {

        }
    }
}
