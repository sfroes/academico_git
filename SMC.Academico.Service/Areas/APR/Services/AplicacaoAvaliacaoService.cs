using SMC.Academico.Domain.Areas.APR.DomainServices;
using SMC.Academico.Domain.Areas.APR.Specifications;
using SMC.Academico.Domain.Areas.APR.ValueObjects;
using SMC.Academico.Domain.Areas.ORT.DomainServices;
using SMC.Academico.Domain.Areas.ORT.ValueObjects;
using SMC.Academico.ServiceContract.Areas.APR.Data;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORT.Data;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.APR.Services
{
    public class AplicacaoAvaliacaoService : SMCServiceBase, IAplicacaoAvaliacaoService
    {
        #region DomainServices

        private TrabalhoAcademicoDomainService TrabalhoAcademicoDomainService { get => Create<TrabalhoAcademicoDomainService>(); }

        private AplicacaoAvaliacaoDomainService AplicacaoAvaliacaoDomainService { get => Create<AplicacaoAvaliacaoDomainService>(); }

        #endregion Domain Services

        #region [ Service ]

        private IInstituicaoNivelCalendarioService InstituicaoNivelCalendarioService { get => Create<IInstituicaoNivelCalendarioService>(); }

        #endregion [ Service ]

        //public SMCPagerData<AvaliacaoTrabalhoAcademicoListaData> BuscarAvaliacoesTrabalhoAcademico(AvaliacaoTrabalhoAcademicoFiltroData filtro)
        //{
        //    var resultado = BuscarAvaliacoesTrabalhoAcademico(filtro.SeqTrabalhoAcademico.Value);
        //    return resultado.Transform<SMCPagerData<AvaliacaoTrabalhoAcademicoListaData>>();
        //}

        //public SMCPagerData<AvaliacaoTrabalhoAcademicoListaData> BuscarAvaliacoesTrabalhoAcademico(long seqTrabalhoAcademico)
        //{
        //    var entity = this.TrabalhoAcademicoDomainService.BuscarAvaliacoesTrabalhoAcademico(seqTrabalhoAcademico);
        //    return entity.Transform<SMCPagerData<AvaliacaoTrabalhoAcademicoListaData>>();
        //}

        public List<AvaliacaoTrabalhoAcademicoListaData> BuscarListaComponenteAvaliacoesTrabalhoAcademico(long seqTrabalhoAcademico)
        {
            var entity = this.TrabalhoAcademicoDomainService.BuscarAvaliacoesTrabalhoAcademico(seqTrabalhoAcademico);
            return entity.TransformList<AvaliacaoTrabalhoAcademicoListaData>();
        }

        public AvaliacaoTrabalhoAcademicoBancaExaminadoraData BuscarAvaliacoesTrabalhoAcademicoBancaExaminadoraInsert(AvaliacaoTrabalhoAcademicoBancaExaminadoraData model)
        {
            model.SeqNivelEnsino = TrabalhoAcademicoDomainService.AlterarTrabalhoAcademico(model.SeqTrabalhoAcademico.Value).SeqNivelEnsino.Value;
            model.TiposEvento = InstituicaoNivelCalendarioService.BuscarTiposEventosTrabalhoAcademico(model.SeqTrabalhoAcademico, model.SeqOrigemAvaliacao);
            model.DataDepositoSecretaria = TrabalhoAcademicoDomainService.BuscarDataDepositoSecretaria(model.SeqTrabalhoAcademico.Value);

            return model;
        }

        public AvaliacaoTrabalhoAcademicoBancaExaminadoraData BuscarAplicacaoAvaliacaoTrabalhoAcademicoBancaExaminadora(AvaliacaoTrabalhoAcademicoFiltroData filtro)
        {
            var entity = this.AplicacaoAvaliacaoDomainService.BuscarAplicacaoAvaliacaoTrabalhoAcademicoBancaExaminadora(filtro.Seq.GetValueOrDefault(), filtro.SeqTrabalhoAcademico.GetValueOrDefault())
                                                             .Transform<AvaliacaoTrabalhoAcademicoBancaExaminadoraData>();

            entity.TiposEvento = InstituicaoNivelCalendarioService.BuscarTiposEventosTrabalhoAcademico(entity.SeqTrabalhoAcademico, entity.SeqOrigemAvaliacao);

            return entity;
        }

        public AvaliacaoTrabalhoAcademicoBancaExaminadoraData BuscarDetalhesCancelamentoAplicacaoAvaliacaoBancaExaminadora(AvaliacaoTrabalhoAcademicoFiltroData filtro)
        {
            var entity = this.AplicacaoAvaliacaoDomainService.BuscarDetalhesCancelamentoAplicacaoAvaliacaoBancaExaminadora(filtro.Seq.Value)
                .Transform<AvaliacaoTrabalhoAcademicoBancaExaminadoraData>();

            return entity;
        }

        public LancamentoNotaBancaExaminadoraData BuscarLancamentoNotaBancaExaminadoraInsert(AvaliacaoTrabalhoAcademicoFiltroData filtro)
        {
            var entity = this.AplicacaoAvaliacaoDomainService.BuscarLancamentoNotaBancaExaminadoraInsert(filtro.Seq.Value)
               .Transform<LancamentoNotaBancaExaminadoraData>();

            return entity;
        }

        public bool ApuracaoNota(long seqAplicacaoAvaliacao)
        {
            return this.AplicacaoAvaliacaoDomainService.ApuracaoNota(seqAplicacaoAvaliacao);
        }

        public long SalvarAplicacaoAvaliacaoTrabalhoAcademicoBancaExaminadora(AvaliacaoTrabalhoAcademicoBancaExaminadoraData avaliacao)
        {
            return AplicacaoAvaliacaoDomainService.SalvarAplicacaoAvaliacaoTrabalhoAcademicoBancaExaminadora(avaliacao.Transform<AvaliacaoTrabalhoAcademicoBancaExaminadoraVO>());
        }

        public List<BancasAgendadasData> BuscarBancasAgendadasPorPeriodo(BancasAgendadasFiltroData filtro)
        {
            var filtroData = filtro.Transform<BancasAgendadasFiltroVO>();
            var resultado = AplicacaoAvaliacaoDomainService.BuscarBancasAgendadasPorPeriodo(filtroData);
            return resultado.TransformList<BancasAgendadasData>();
        }

        /// <summary>
        /// Verificar se situação de aprovação é igual "Aprovado", de acordo com o 
        /// critério associado ao componente da avaliação em questão e a nota ou conceito lançado.
        /// </summary>
        public bool CriterioAprovacaoAprovado(LancamentoNotaBancaExaminadoraData lancamento)
        {
            return AplicacaoAvaliacaoDomainService.CriterioAprovacaoAprovado(lancamento.Transform<LancamentoNotaBancaExaminadoraVO>());
        }

        public void ExcluirAplicacaoAvaliacaoTrabalhoAcademicoBancaExaminadora(AvaliacaoTrabalhoAcademicoFiltroData filtro)
        {
            this.AplicacaoAvaliacaoDomainService.ExcluirAplicacaoAvaliacaoTrabalhoAcademicoBancaExaminadora(filtro.Seq.Value);
        }

        public bool ExibirMensagemAprovacaoPublicacaoBibliotecaObrigatoria(LancamentoNotaBancaExaminadoraData lancamento)
        {
            return AplicacaoAvaliacaoDomainService.ExibirMensagemAprovacaoPublicacaoBibliotecaObrigatoria(lancamento.Transform<LancamentoNotaBancaExaminadoraVO>());
        }

        /// <summary>
        /// Buscar a proxima silga da avaliação
        /// </summary>
        /// <param name="filtro">Parametros de pesquisa</param>
        /// <returns>Sigla formatada</returns>
        public string BuscarProximaSiglaAvaliacao(AplicacaoAvaliacaoFiltroData filtro)
        {
            return AplicacaoAvaliacaoDomainService.BuscarProximaSiglaAvaliacao(filtro.Transform<AplicacaoAvaliacaoFilterSpecification>());
        }

        /// <summary>
        /// Buscar todas as aplicações avaliacoes
        /// </summary>
        /// <param name="seqOrigemAvaliacao">Parametros de pesquisa</param>
        /// <returns>Lista de aplicações avaliações</returns>
        public List<AplicacaoAvaliacaoData> BuscarAplicacoesAvaliacoes(AplicacaoAvaliacaoFiltroData filtro)
        {
            return AplicacaoAvaliacaoDomainService.BuscarAplicacoesAvaliacoes(filtro.Transform<AplicacaoAvaliacaoFilterSpecification>()).TransformList<AplicacaoAvaliacaoData>();
        }

        /// <summary>
        /// Busca a quantidade de avaliações aplicadas nas divisões de uma turma
        /// </summary>
        /// <param name="seqAlunoHistorico">Sequencial do aluno histórico</param>
        /// <param name="seqOrigemAvaliacao">Sequencial da origem de avaliação da turma do aluno</param>
        /// <returns>Quandidade de avaliações aplicadadas para as divisoes da origem informada</returns>
        public int BuscarQuantidadeAvaliacoesAlunoPorOrigemTurma(long seqAlunoHistorico, long seqOrigemAvaliacao)
        {
            return AplicacaoAvaliacaoDomainService.BuscarQuantidadeAvaliacoesAlunoPorOrigemTurma(seqAlunoHistorico, seqOrigemAvaliacao);
        }
    }
}
