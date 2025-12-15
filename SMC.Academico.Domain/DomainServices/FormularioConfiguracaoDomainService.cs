using SMC.Academico.Domain.Models;
using SMC.Academico.Domain.Specifications;

namespace SMC.Academico.Domain.DomainServices
{
    public class FormularioConfiguracaoDomainService : AcademicoContextDomain<FormularioConfiguracao>
    {
        public FormularioConfiguracao BuscarConfiguracaoFormularioProposta(string token)
        {
            return this.SearchByKey(new FormularioConfiguracaoFilterSpecification() { Token = token });
        }
    }
}