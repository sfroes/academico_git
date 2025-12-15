using SMC.Academico.Domain.Areas.DCT.DomainServices;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.DCT.Services
{
    public class InstituicaoTipoEntidadeVinculoColaboradorService : SMCServiceBase, IInstituicaoTipoEntidadeVinculoColaboradorService
    {
        #region [ Service ]

        private InstituicaoTipoEntidadeVinculoColaboradorDomainService InstituicaoTipoEntidadeVinculoColaboradorDomainService
        {
            get { return this.Create<InstituicaoTipoEntidadeVinculoColaboradorDomainService>(); }
        }

        #endregion [ Service ]

        /// <summary>
        /// Busca os tipos de vinculos de colaborador na instituição para o tipo de uma entidade
        /// </summary>
        /// <param name="seqEntidadeVinculo">Sequencial da entidade vinculada</param>
        /// <param name="seqColaboradorVinculo">Sequencial do colaborador vinculo</param>
        /// <returns>Dados dos tipos de vinculo na instituição para o tipo da entidade informada</returns>
        public List<SMCDatasourceItem> BuscarTiposVinculoColaboradorPorEntidadeSelect(long seqEntidadeVinculo, long? seqColaboradorVinculo)
        {
            return this.InstituicaoTipoEntidadeVinculoColaboradorDomainService.BuscarTiposVinculoColaboradorPorEntidadeSelect(seqEntidadeVinculo, seqColaboradorVinculo);
        }
    }
}