using SMC.Academico.Domain.Areas.ORT.DomainServices;
using SMC.Academico.Domain.Areas.ORT.Specifications;
using SMC.Academico.Domain.Areas.ORT.ValueObjects;
using SMC.Academico.ServiceContract.Areas.ORT.Data;
using SMC.Academico.ServiceContract.Areas.ORT.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.ORT.Services
{
    public class OrientacaoService : SMCServiceBase, IOrientacaoService
    {
        #region DomainService

        private OrientacaoDomainService OrientacaoDomainService
        {
            get { return this.Create<OrientacaoDomainService>(); }
        }

        #endregion DomainService

        public SMCPagerData<OrientacaoListarData> BuscarOrientacoes(OrientacaoFiltroData filtro)
        {
            return this.OrientacaoDomainService.BuscarOrientacoes(filtro.Transform<OrientacaoFilterSpecification>()).Transform<SMCPagerData<OrientacaoListarData>>();
        }

        public long SalvarOrientacao(OrientacaoData dadosOrientacao)
        {
            return this.OrientacaoDomainService.SalvarOrientacao(dadosOrientacao.Transform<OrientacaoVO>());
        }

        public (string ParticipacaoOrientacaoObrigatoria, string OrientadoresSemVinculosAtivos) ValidarOrientacoes(OrientacaoData dadosOrientacao)
        {
            var dadosOrientacaoVO = dadosOrientacao.Transform<OrientacaoVO>();
            var participacaoOrientacaoObrigatoria = this.OrientacaoDomainService.ValidarParticipacaoOrientacao(dadosOrientacaoVO).OrientacoesFaltantes;
            var orientacoesSemVinculosAtivos = this.OrientacaoDomainService.ValidarVinculosAtivosOrientacoes(dadosOrientacaoVO);

            return (participacaoOrientacaoObrigatoria, orientacoesSemVinculosAtivos);
        }

        public OrientacaoData AlterarOrientacao(long seq)
        {
            return this.OrientacaoDomainService.AlterarOrientacao(seq).Transform<OrientacaoData>();
        }

        /// <summary>
        /// Buscar todas as orientação de uma determinda divisao de turma
        /// </summary>
        /// <param name="seqDivisaoTurma">Sequencial de uma divisão de turma</param>
        /// <returns>Todas as orientações de uma divisão de turma</returns>
        public SMCPagerData<OrientacaoTurmaData> BuscarOrientacoesPorDivisaoTurma(OrientacaoTurmaFiltroData filtro)
        {
            return this.OrientacaoDomainService.BuscarOrientacoesPorDivisaoTurma(filtro.Transform<OrientacaoTurmaFiltroVO>()).Transform<SMCPagerData<OrientacaoTurmaData>>();
        }

        /// <summary>
        /// Buscar orientação de uma determinado filtro
        /// </summary>
        /// <param name="filtro">Parametros para filtrar a orientação da turma</param>
        /// <returns>Orientação de uma divisão de turma</returns>
        public OrientacaoTurmaData BuscarOrientacaoPorDivisaoTurma(OrientacaoTurmaFiltroData filtro)
        {
            return this.OrientacaoDomainService.BuscarOrientacaoPorDivisaoTurma(filtro.Transform<OrientacaoTurmaFiltroVO>()).Transform<OrientacaoTurmaData>();
        }

        /// <summary>
        /// Buscar orientação de uma determinado filtro
        /// </summary>
        /// <param name="filtro">Parametros para filtrar a orientação da turma</param>
        /// <returns>Orientação de uma divisão de turma</returns>
        public long SalvarOrientacaoTurma(OrientacaoTurmaData dadosOrietacao)
        {
            return this.OrientacaoDomainService.SalvarOrientacaoTurma(dadosOrietacao.Transform<OrientacaoTurmaVO>());
        }

        /// <summary>
        /// Exlcluir orientação
        /// </summary>
        /// <param name="seq">Sequencial da orientação</param>
        public void ExcluirOrientacao(long seq)
        {
            this.OrientacaoDomainService.ExcluirOrientacao(seq);
        }

        /// <summary>
        /// Buscar orientacoes do aluno
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <returns>Lista das orientações do aluno</returns>
        public List<OrientacaoData> BuscarOrientacoesPorAluno(long seqPessoaAtuacao)
        {
            return OrientacaoDomainService.BuscarOrientacoesPorAluno(seqPessoaAtuacao).TransformList<OrientacaoData>();
        }

        public List<OrientadoresRelatorioData> BuscarOrientacoesRelatorio(OrientacaoFiltroData filtro)
        {
            var retorno = this.OrientacaoDomainService.BuscarOrientacoesRelatorio(filtro.Transform<OrientacaoFilterSpecification>()).TransformList<OrientadoresRelatorioData>();

            return retorno;
        }

    }
}