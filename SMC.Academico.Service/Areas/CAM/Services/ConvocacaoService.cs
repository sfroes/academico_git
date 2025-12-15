using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Academico.Domain.Areas.CAM.Specifications;
using SMC.Academico.ServiceContract.Areas.CAM.Data;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using SMC.SGA.Administrativo.Areas.CAM.Models;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.CAM.Services
{
    public class ConvocacaoService : SMCServiceBase, IConvocacaoService
    {
        #region Domain Service

        private ConvocacaoDomainService ConvocacaoDomainService
        {
            get { return Create<ConvocacaoDomainService>(); }
        }

        private IngressanteDomainService IngressanteDomainService
        {
            get { return Create<IngressanteDomainService>(); }
        }

        private ChamadaDomainService ChamadaDomainService
        {
            get { return Create<ChamadaDomainService>(); }
        }

        private IProcessoService ProcessoService
        {
            get { return Create<IProcessoService>(); }
        }

        #endregion Domain Service

        public bool VerificarExistenciaConvocados(long seqInstituicao, long seqChamada)
        {
            return ConvocacaoDomainService.VerificarExistenciaConvocados(seqInstituicao, seqChamada);
        }

        public SMCPagerData<ConvocacaoListaData> ListarConvocacoes(ConvocacaoFiltroData filtro)
        {
            var ret = ConvocacaoDomainService.ListarConvocacoes(filtro.Transform<ConvocacaoFilterSpecification>());
            return ret.Transform<SMCPagerData<ConvocacaoListaData>>();
        }

        public long SalvarConvocacao(ConvocacaoData convocacao)
        {
            var convocacaoDominio = convocacao.Transform<Convocacao>();
            return ConvocacaoDomainService.SalvarConvocacao(convocacaoDominio);
        }

        /// <summary>
        /// Realiza as validações de acordo com RN_ALN_043 e libera os ingressantes convocados pela chamada para realizar a matrícula
        /// </summary>
        /// <param name="seqInstituicao">Sequencial da instituição (necessario para utilizar RawQuery)</param>
        /// <param name="seqChamada">Sequencial da chamada</param>
        /// <returns>Retorna um objeto com os impedimentos validados</returns>
        public ConvocacaoImpedimentosDeMatriculaData VerificarImpedimentosExecutarMatriculaPorChamada(long seqInstituicao, long seqChamada)
        {
            return ConvocacaoDomainService.VerificarImpedimentosExecutarMatriculaPorChamada(seqInstituicao, seqChamada).Transform<ConvocacaoImpedimentosDeMatriculaData>();
        }

        public ConvocacaoData AlterarConvocacao(long seq)
        {
            var result = ConvocacaoDomainService.SearchByKey(new SMCSeqSpecification<Convocacao>(seq), a => a.Chamadas[0].GrupoEscalonamento);
            var resultconvocacao = ConvocacaoDomainService.SearchByKey(new SMCSeqSpecification<Convocacao>(seq), a => a.CampanhaCicloLetivo);
            ConvocacaoData convocacaoData = result.Transform<ConvocacaoData>();

            // Recupera o processo referente a campanha ciclo e processo seletivo, utilizado como parâmetro no lookup de grupo de escalonamento
            var filtroCampanha = new ProcessoFiltroData() { SeqCampanhaCicloLetivo = convocacaoData.SeqCampanhaCicloLetivo, SeqProcessoSeletivo = convocacaoData.SeqProcessoSeletivo };
            convocacaoData.SeqProcesso = ProcessoService.BuscarProcesso(filtroCampanha)?.Seq ?? 0;

            //Lista todos os grupos escalonamento do banco para validar a alteração
            convocacaoData.SeqsGrupoEscalonamentoBanco = convocacaoData.Chamadas.Select(s => s.SeqGrupoEscalonamento).ToList();

            foreach (var chamada in convocacaoData.Chamadas)
            {
                chamada.SeqGrupoEscalonamentoBanco = chamada.SeqGrupoEscalonamento;
                chamada.SeqCampanha = resultconvocacao.CampanhaCicloLetivo.SeqCampanha;
            }

            return convocacaoData;
        }

        public List<SMCDatasourceItem> BuscarConvocacoesPorCampanhaCicloLetivoProcessoSeletivoSelect(long seqCampanha, long? seqCicloLetivo = null, long? seqTipoProcessoSeletivo = null, long? seqProcessoSeletivo = null)
        {
            var spec = new ConvocacaoFilterSpecification()
            {
                SeqCampanha = seqCampanha,
                SeqCicloLetivo = seqCicloLetivo,
                SeqTipoProcessoSeletivo = seqTipoProcessoSeletivo,
                SeqProcessoSeletivo = seqProcessoSeletivo
            };

            var result = ConvocacaoDomainService.SearchProjectionBySpecification(spec, c => new SMCDatasourceItem()
            {
                Seq = c.Seq,
                Descricao = c.Descricao
            }).ToList();

            return result;
        }
    }
}