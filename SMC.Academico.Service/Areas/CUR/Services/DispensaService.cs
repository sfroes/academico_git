using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;

namespace SMC.Academico.Service.Areas.CUR.Services
{
    public class DispensaService : SMCServiceBase, IDispensaService
    {
        #region [ DomainService ]

        private DispensaDomainService DispensaDomainService
        {
            get { return this.Create<DispensaDomainService>(); }
        }

        #endregion

        /// <summary>
        /// Busca uma dispensa com seus respectivos grupos de componentes
        /// Estruturados de acordo com as definições de tela
        /// </summary>
        /// <param name="seq">Sequencial da dispensa</param>
        /// <returns>Objeto dispensa com seus detalhes</returns>
        public DispensaData BuscarDispensa(long seq)
        {
            return DispensaDomainService.BuscarDispensa(seq).Transform<DispensaData>();
        }

        /// <summary>
        /// Busca as dispensas que atendam os filtros informados e com a paginação correta
        /// </summary>
        /// <param name="filtros">Filtros da listagem de dispensas</param>
        /// <returns>SMCPagerData com a lista de dispensas</returns>
        public SMCPagerData<DispensaData> BuscarDispensas(DispensaFiltroData filtros)
        {            
            var dispensas = DispensaDomainService.BuscarDispensas(filtros.Transform<DispensaFilterSpecification>());

            return dispensas.Transform<SMCPagerData<DispensaData>>();
        }

        /// <summary>
        /// Grava uma dispensa com seus respectivos grupos de componentes
        /// </summary>
        /// <param name="dispensa">Dados da dispensa a ser gravado</param>
        /// <returns>Sequencial da dispensa gravado</returns>
        public long SalvarDispensa(DispensaData dispensa)
        {
            return DispensaDomainService.SalvarDispensa(dispensa.Transform<DispensaVO>());
        }

        /// <summary>
        /// Grava uma dispensa com suas matrizes de exceção
        /// </summary>
        /// <param name="dispensa">Dados da dispensa a ser gravado</param>
        /// <returns>Sequencial da dispensa gravado</returns>
        public long SalvarDispensaMatriz(DispensaData dispensa)
        {
            return DispensaDomainService.SalvarDispensaMatriz(dispensa.Transform<DispensaVO>());
        }

        /// <summary>
        /// Exclui uma dispensa
        /// </summary>
        /// <param name="seq">Sequencial da dispensa</param>
        public void ExcluirDispensa(long seq)
        {
            this.DispensaDomainService.ExcluirDispensa(seq);
        }
    }
}
