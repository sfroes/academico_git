using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CAM.Specifications;
using SMC.Academico.Domain.Areas.CAM.ValueObjects;
using SMC.Academico.ServiceContract.Areas.CAM.Data;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Service.Areas.CAM.Services
{
    public class ProcessoSeletivoOfertaService : SMCServiceBase, IProcessoSeletivoOfertaService
    {
        #region DomainServices
        private ProcessoSeletivoOfertaDomainService ProcessoSeletivoOfertaDomainService => Create<ProcessoSeletivoOfertaDomainService>();
        #endregion

        public SMCPagerData<ProcessoSeletivoOfertaListaData> BuscarProcessosSeletivoOferta(ProcessoSeletivoOfertaFiltroData filtro)
        {
            var spec = filtro.Transform<ProcessoSeletivoOfertaSpecification>();
            spec.SetOrderBy(o => o.CampanhaOferta.TipoOferta.Descricao);
            spec.SetOrderBy(o => o.CampanhaOferta.Descricao);
            var lista = ProcessoSeletivoOfertaDomainService.SearchProjectionBySpecification(spec, x => new ProcessoSeletivoOfertaListaData
            {
                Seq = x.Seq,
                SeqCampanhaOferta = x.SeqCampanhaOferta,
                TipoOferta = x.CampanhaOferta.TipoOferta.Descricao,
                TipoOfertaToken = x.CampanhaOferta.TipoOferta.Token,
                Oferta = x.CampanhaOferta.Descricao,
                Vagas = x.QuantidadeVagas,
                VagasBase = x.QuantidadeVagas,
                Ocupadas = x.QuantidadeVagasOcupadas,
                PossuiVinculoConvocacao = x.ProcessoSeletivo.Convocacoes.Count > 0

            }, out int total);
            return new SMCPagerData<ProcessoSeletivoOfertaListaData>(lista, total);
        }

        /// <summary>
        /// Método que recupera uma lista de convocações, conforme o processo seletivo
        /// </summary>
        /// <param name="seqProcessoSeletivo"></param>
        /// <returns>Lista de convocações do processo seletivo da oferta</returns>
        public List<ConvocacaoData> BuscarConvocacoesProcessoSeletivo(long seqProcessoSeletivo)
        {
            return ProcessoSeletivoOfertaDomainService.BuscarConvocacoesProcessoSeletivo(seqProcessoSeletivo).TransformList<ConvocacaoData>();
        }


        /// <summary>
        /// RN_CAM_036 Exclusão de oferta do processo seletivo
        /// </summary>
        /// <param name="seqProcessoSeletivoOferta"></param>
        public void Excluir(long seqProcessoSeletivoOferta)
        {
            ProcessoSeletivoOfertaDomainService.Excluir(seqProcessoSeletivoOferta);
        }

        /// <summary>
        /// RN_CAM_037 Atualização das vagas das ofertas do processo seletivo
        /// </summary>
        /// <param name="data"></param>
        public void AtualizarVagasOfertasProcessoSeletivo(VagasProcessoSeletivoOfertaData data)
        {
            ProcessoSeletivoOfertaDomainService.AtualizarVagasOfertasProcessoSeletivo(data.Transform<VagasProcessoSeletivoOfertaVO>());
        }

        /// <summary>
        /// RN_CAM_068 - Copiar vagas da campanha
        /// </summary>
        /// <param name="data"></param>
        public void CopiarVagasCampanha_RN_CAM_068(VagasProcessoSeletivoOfertaData data)
        {
            ProcessoSeletivoOfertaDomainService.CopiarVagasCampanha_RN_CAM_068(data.Transform<VagasProcessoSeletivoOfertaVO>());
        }

        /// <summary>
        /// RN_CAM_069 - Usar vagas disponíveis na campanha
        /// </summary>
        /// <param name="data"></param>
        public void UsarVagasDisponiveis_RN_CAM_069(VagasProcessoSeletivoOfertaData data)
        {
            ProcessoSeletivoOfertaDomainService.UsarVagasDisponiveis_RN_CAM_069(data.Transform<VagasProcessoSeletivoOfertaVO>());
        }
    }
}
