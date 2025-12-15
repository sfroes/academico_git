using SMC.Framework.Entity;

namespace SMC.Academico.EntityRepository.Ioc
{
    /// <summary>
    /// Classe para mapeamento do repositório do Entity.
    /// </summary>
    public class EntityIocMapping : SMCEntityIocMapping
    {
        /// <summary>
        /// Configura o container de Ioc.
        /// </summary>
        /// <param name="container">Container de Ioc.</param>
        protected override void Configure()
        {
            this.Providers
                .Register<AcademicoContext>();
        }
    }
}
