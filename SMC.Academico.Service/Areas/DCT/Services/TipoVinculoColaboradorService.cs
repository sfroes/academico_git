using SMC.Academico.Domain.Areas.DCT.DomainServices;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.DCT.Services
{
    public class TipoVinculoColaboradorService : SMCServiceBase, ITipoVinculoColaboradorService
    {
        #region [ Services ]

        private TipoVinculoColaboradorDomainService TipoVinculoColaboradorDomainService
        {
            get { return this.Create<TipoVinculoColaboradorDomainService>(); }
        }

        #endregion [ Services ]

        public List<SMCDatasourceItem> BuscarTipoVinculoColaboradorSelect()
        {
            return this.TipoVinculoColaboradorDomainService
                .SearchProjectionAll(p => new SMCDatasourceItem() { Seq = p.Seq, Descricao = p.Descricao }, o => o.Descricao)
                .ToList();
        }

        /// <summary>
        /// Buscar todos os tipos de vinculo baseado nas configurações da instituição.
        /// </summary>
        /// <param name="seqEntidadeVinculo">Sequencial da entidade para filtrar apenas os vinculos configurados para o tipo desta</param>
        /// <param name="criaVinculoInstitucional">Retorna apenas os tipos de vínculo que permitem criar vínculo institucional</param>
        /// <returns>Dados dos vínculos configurados na instituição ou apenas os vinculos do tipo de entidade informado</returns>
        public List<SMCDatasourceItem> BuscarTipoVinculoColaboradorDeEntidadesVinculoSelect(long? seqEntidadeVinculo = null, bool? criaVinculoInstitucional = null)

        {
            return this.TipoVinculoColaboradorDomainService.BuscarTipoVinculoColaboradorDeEntidadesVinculoSelect(seqEntidadeVinculo, criaVinculoInstitucional);
        }

        /// <summary>
        /// Retornar se tipo vinculo colaborador necessita acompanhamento de supervisor
        /// </summary>
        /// <param name="seqTipoVinculoColaborador">Sequencial tipo vinculo colaborador</param>
        /// <returns>Retornar boleano necessita acompanhamento de supervisor </returns>
        public bool RetornarTipoVinculoNecessitaAcompanhamento(long seqTipoVinculoColaborador)
        {
            return this.TipoVinculoColaboradorDomainService.RetornarTipoVinculoNecessitaAcompanhamento(seqTipoVinculoColaborador);
        }

        public List<SMCDatasourceItem> BuscarTipoVinculoColaboradorPorEntidadesSelect(List<long> seqsEntidadesResponsaveis)

        {
            return this.TipoVinculoColaboradorDomainService.BuscarTipoVinculoColaboradorPorEntidadesSelect(seqsEntidadesResponsaveis);
        }
    }
}