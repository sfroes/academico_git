using SMC.Academico.Domain.Repositories;
using SMC.Framework.Repository;

namespace SMC.Academico.DataRepository.Ioc
{
    /// <summary>
    /// Classe com o mapeamento da interface de repositório.
    /// </summary>
    public class DataRepositoryIocMapping : SMCRepositoryIocMapping
    {
        protected override void Configure()
        {
            this.Repositories
                //.Register<IIntegracaoAcademicoRepository, IntegracaoAcademicoRepository>()
                .Register<IAcademicoRepository, AcademicoRepository>();
        }
    }
}