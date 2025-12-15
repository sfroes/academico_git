using SMC.Academico.EntityRepository;
using SMC.Academico.UnitTests.Constants;
using System.Collections.Generic;

namespace SMC.Academico.UnitTests.Ioc
{
    public class TestContainerAcademico: SMCTestContainer<AcademicoContext>
    {
        public TestContainerAcademico() : base(CONTEXT.ACADEMICO)
        { }

        public void CreateRepository<TEntity>(IEnumerable<TEntity> data) where TEntity : class
        {
            CreateRepository(data, CONTEXT.ACADEMICO);
        }
    }
}
