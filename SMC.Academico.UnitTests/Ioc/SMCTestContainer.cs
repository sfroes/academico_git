using Moq;
using SMC.Framework;
using SMC.Framework.Entity;
using SMC.Framework.Ioc;
using SMC.Framework.Repository;
using SMC.Framework.UnitOfWork;
using SMC.Framework.Unity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.UnitTests.Ioc
{
    public class SMCTestContainer<TContext> where TContext: SMCDbContext
    {
        private SMCContainerManager _Manager;
        private Mock<TContext> _ContextMock;
        private Mock<ISMCUnitOfWork> _MockUnit;

        public SMCContainerManager Manager => _Manager;

        public Mock<ISMCUnitOfWork> UnitOfWorkMock => _MockUnit;

        public Mock<TContext> ConextMock => _ContextMock;

        public SMCTestContainer(string name, SMCContainerManager manager = null)
        {
            _Manager = manager ?? new SMCContainerManager();
            CreateContext(name);
        }

        private void CreateContext(string name)
        {
            var smcUnityContainer = _Manager.Container as SMCUnityContainer;
            smcUnityContainer.RegisterRepositoryProvider<TContext>(name);

            _ContextMock = CreateMock<TContext>();
            _MockUnit = CreateMock<ISMCUnitOfWork>();
        }

        public void CreateRepository<TEntity>(IEnumerable<TEntity> data, string name) where TEntity: class
        {
            var repository = new SMCRepository<TEntity>(name);
            _Manager.Container.RegisterInstance<ISMCRepository<TEntity>>(repository);

            var mockSet = new Mock<DbSet<TEntity>>();
            var querable = data.AsQueryable();
            mockSet.As<IQueryable<TEntity>>().Setup(m => m.Provider).Returns(querable.Provider);
            mockSet.As<IQueryable<TEntity>>().Setup(m => m.Expression).Returns(querable.Expression);
            mockSet.As<IQueryable<TEntity>>().Setup(m => m.ElementType).Returns(querable.ElementType);
            mockSet.As<IQueryable<TEntity>>().Setup(m => m.GetEnumerator()).Returns(querable.GetEnumerator());
            mockSet.Setup(s => s.AsNoTracking()).Returns(mockSet.Object);
            mockSet.Setup(s => s.Include(It.IsAny<string>())).Returns(mockSet.Object);
            _ContextMock.Setup(s => s.Set<TEntity>()).Returns(mockSet.Object);
            _ContextMock.Setup(s => s.Insert(It.IsAny<TEntity>())).Returns<TEntity>(entity =>
            {
                if (entity is ISMCSeq)
                {
                    var e = entity as ISMCSeq;
                    e.Seq = data.SMCAny() ? data.Cast<ISMCSeq>().Max(m => m.Seq) + 1 : 1;
                }
                return entity;
            });
            _ContextMock.Setup(s => s.UpdateFields(It.IsAny<TEntity>(), It.IsAny<Expression<Func<TEntity, object>>[]>()));
        }

        /// <summary>
        /// Cria um mock e registra no container
        /// </summary>
        /// <typeparam name="T">Tipo do mock a ser criado</typeparam>
        /// <param name="register">Quando setado registra o mock ao criar</param>
        /// <returns>Retorna o mock criado</returns>
        public Mock<T> CreateMock<T>(string name = null, bool register = true) where T : class
        {
            var mock = new Mock<T>();
            if (register)
            {
                if (!string.IsNullOrEmpty(name))
                {
                    _Manager.Container.RegisterInstance(name, mock.Object);
                }
                else
                {
                    _Manager.Container.RegisterInstance(mock.Object);
                }
            }
            return mock;
        }
    }
}
