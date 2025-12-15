using SMC.Academico.Common.Areas.APR.Includes;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Academico.Domain.Areas.APR.Specifications;
using SMC.Academico.Domain.Areas.APR.Validators;
using SMC.Academico.Domain.Areas.APR.ValueObjects;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.APR.DomainServices
{
    public class CriterioAprovacaoDomainService : AcademicoContextDomain<CriterioAprovacao>
    {
        #region [ DomainService ]

        private ConfiguracaoComponenteDomainService ConfiguracaoComponenteDomainService
        {
            get { return this.Create<ConfiguracaoComponenteDomainService>(); }
        }

        private CurriculoCursoOfertaDomainService CurriculoCursoOfertaDomainService
        {
            get { return this.Create<CurriculoCursoOfertaDomainService>(); }
        }

        private EscalaApuracaoDomainService EscalaApuracaoDomainService
        {
            get { return this.Create<EscalaApuracaoDomainService>(); }
        }

        private InstituicaoNivelCriterioAprovacaoDomainService InstituicaoNivelCriterioAprovacaoDomainService
        {
            get { return this.Create<InstituicaoNivelCriterioAprovacaoDomainService>(); }
        }

        private AlunoDomainService AlunoDomainService { get => Create<AlunoDomainService>(); }

        private CriterioAprovacaoCursoDomainService CriterioAprovacaoCursoDomainService { get => Create<CriterioAprovacaoCursoDomainService>(); }

        private AlunoHistoricoDomainService AlunoHistoricoDomainService { get => Create<AlunoHistoricoDomainService>(); }

        private CursoOfertaLocalidadeTurnoDomainService CursoOfertaLocalidadeTurnoDomainService { get => Create<CursoOfertaLocalidadeTurnoDomainService>(); }

        #endregion [ DomainService ]

        /// <summary>
        /// Busca o critério de aprovação selecionado
        /// </summary>
        /// <param name="seq">Sequencial do critério de aprovação</param>
        /// <returns>Dados dos critério de aprovação</returns>
        public CriterioAprovacao BuscarCriterioAprovacao(long seq)
        {
            var criterio = this.SearchByKey(new SMCSeqSpecification<CriterioAprovacao>(seq), IncludesCriterioAprovacao.EscalaApuracao);
            return criterio;
        }

        /// <summary>
        /// Busca os critérios de aprovação que sejam do nível de ensino do curso do curriculo curso oferta informado
        /// </summary>
        /// <param name="seqCurriculoCursoOferta">Sequencial do currículo curso oferta do curso com o nível de ensino em questão</param>
        /// <returns>Dados dos critérios de aprovação</returns>
        public List<SMCDatasourceItem> BuscarCriteriosAprovacaoNivelEnsinoPorCurriculoCursoOfertaSelect(long seqCurriculoCursoOferta)
        {
            // Recupera nível ensino do curso associado ao curriculo curso oferta
            long seqNivelEnsino = this.CurriculoCursoOfertaDomainService
                .SearchProjectionByKey(new SMCSeqSpecification<CurriculoCursoOferta>(seqCurriculoCursoOferta), p => p.CursoOferta.Curso.SeqNivelEnsino);

            return BuscarCriteriosAprovacaoPorNivelEnsino(seqNivelEnsino);
        }

        /// <summary>
        /// Recupera os critérios de aprovação disponíveis na instituição para o nível de ensino informado
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do nível de ensino</param>
        /// <returns>Dados dos critérios de aprovação</returns>
        public List<SMCDatasourceItem> BuscarCriteriosAprovacaoPorNivelEnsino(long seqNivelEnsino)
        {
            // Recupera todas as escalas do nível de ensino do curso
            var specCriteriosNivel = new InstituicaoNivelCriterioAprovacaoFilterSpecification() { SeqNivelEnsino = seqNivelEnsino };
            var seqsCriteriosNivel = this.InstituicaoNivelCriterioAprovacaoDomainService
                .SearchProjectionBySpecification(specCriteriosNivel, p => p.SeqCriterioAprovacao);

            var specInNivel = new SMCContainsSpecification<CriterioAprovacao, long>(p => p.Seq, seqsCriteriosNivel.ToArray());

            var selectItens = this.SearchProjectionBySpecification(specInNivel,
                p => new SMCDatasourceItem() { Seq = p.Seq, Descricao = p.Descricao }).OrderBy(o => o.Descricao);

            return selectItens
                .ToList();
        }

        /// <summary>
        /// Busca os critérios de aprovação que sejam do nível de ensino da configuração do componente
        /// </summary>
        /// <param name="seqConfiguracaoComponente">Sequencial da configuração do componente com o nível de ensino em questão</param>
        /// <returns>Dados dos critérios de aprovação</returns>
        public List<SMCDatasourceItem> BuscarCriteriosAprovacaoNivelEnsinoPorConfiguracaoComponenteSelect(long seqConfiguracaoComponente)
        {
            // Recupera nível ensino do curso associado ao curriculo curso oferta
            long seqNivelEnsino = this.ConfiguracaoComponenteDomainService.BuscarConfiguracaoComponenteNivelEnsino(seqConfiguracaoComponente);

            // Recupera todas as escalas do nível de ensino do curso
            var specCriteriosNivel = new InstituicaoNivelCriterioAprovacaoFilterSpecification() { SeqNivelEnsino = seqNivelEnsino };
            var seqsCriteriosNivel = this.InstituicaoNivelCriterioAprovacaoDomainService
                .SearchProjectionBySpecification(specCriteriosNivel, p => p.SeqCriterioAprovacao);

            var specInNivel = new SMCContainsSpecification<CriterioAprovacao, long>(p => p.Seq, seqsCriteriosNivel.ToArray());

            var selectItens = this.SearchProjectionBySpecification(specInNivel,
                p => new SMCDatasourceItem()
                {
                    Seq = p.Seq,
                    Descricao = p.Descricao,
                    DataAttributes = new List<SMCKeyValuePair>() { new SMCKeyValuePair() { Key = "apura-nota", Value = p.ApuracaoNota.ToString() }
                                                                 , new SMCKeyValuePair() { Key = "apura-frequencia", Value = p.ApuracaoFrequencia.ToString() } }
                }).OrderBy(o => o.Descricao);

            return selectItens
                .ToList();
        }

        public long SalvarCriterioAprovacao(CriterioAprovacao criterioAprovacao)
        {
            if (criterioAprovacao.SeqEscalaApuracao.HasValue)
            {
                criterioAprovacao.EscalaApuracao = this.EscalaApuracaoDomainService
                    .SearchByKey(new SMCSeqSpecification<EscalaApuracao>(criterioAprovacao.SeqEscalaApuracao.Value));
            }

            SaveEntity(criterioAprovacao, new CriterioAprovacaoValidator());

            return criterioAprovacao.Seq;
        }

        /// <summary>
        /// Recupera os critérios de aprovação pelos filtros informados
        /// </summary>
        /// <param name="filtroVO">Dados dos filtros</param>
        /// <returns>Dados dos critérios de aprovação</returns>
        public List<SMCDatasourceItem> BuscarCriteriosAprovacaoSelect(CriterioAprovacaoFiltroVO filtroVO)
        {
            if (filtroVO.ConsiderarMatriz && filtroVO.SeqAluno.HasValue && filtroVO.SeqCicloLetivo.HasValue && filtroVO.SeqComponenteCurricular.HasValue)
            {
                return BuscarCriteriosAprovacaoPorComponenteAlunoSelect(filtroVO);
            }

            var spec = filtroVO.Transform<CriterioAprovacaoFilterSpecification>();
            spec.SetOrderBy(o => o.Descricao);
            return SearchProjectionBySpecification(spec, p => new SMCDatasourceItem()
            {
                Seq = p.Seq,
                Descricao = p.Descricao
            }).ToList();
        }

        /// <summary>
        /// Recupera os critérios de aprovação pelos filtros informados
        /// </summary>
        /// <param name="filtroVO">Dados dos filtros</param>
        /// <returns>Dados dos critérios de aprovação</returns>
        public List<SMCDatasourceItem> BuscarCriteriosAprovacaoPorComponenteAlunoSelect(CriterioAprovacaoFiltroVO filtroVO)
        {
            var retorno = new List<SMCDatasourceItem>();
            var specAluno = new SMCSeqSpecification<Aluno>(filtroVO.SeqAluno.GetValueOrDefault());

            // Tenta recuperar o critério de aprovação do componente selecionado na matriz do aluno no histórico mais recente com o ciclo letivo informado
            var criterioAprovacaoComponenteAluno = AlunoDomainService.SearchProjectionByKey(specAluno, p => p
                .Historicos.FirstOrDefault(f => f.Atual)
                .HistoricosCicloLetivo.Where(w => w.SeqCicloLetivo == filtroVO.SeqCicloLetivo).OrderByDescending(o => o.Seq).FirstOrDefault()
                .PlanosEstudo.FirstOrDefault()
                .MatrizCurricularOferta.MatrizCurricular
                .ConfiguracoesComponente.Where(w => w.ConfiguracaoComponente.SeqComponenteCurricular == filtroVO.SeqComponenteCurricular).FirstOrDefault()
                .CriterioAprovacao);

            // Caso o aluno tenha o componente informado na sua matriz, será retornado apenas o critério de avaliação configurado nesta
            if (criterioAprovacaoComponenteAluno != null)
            {
                retorno.Add(new SMCDatasourceItem(criterioAprovacaoComponenteAluno.Seq, criterioAprovacaoComponenteAluno.Descricao));
            }
            // Caso contrário, lista todas as configurações do componente no curso do aluno
            else
            {
                // Recupera o curso do aluno no histórico mais recente com o ciclo letivo informado
                var specHistorico = new AlunoHistoricoFilterSpecification()
                {
                    SeqAluno = filtroVO.SeqAluno,
                    SeqCicloLetivo = filtroVO.SeqCicloLetivo
                };
                specHistorico.SetOrderByDescending(o => o.Seq);
                var configAluno = AlunoHistoricoDomainService.SearchProjectionByKey(specHistorico, p => new
                {
                    p.SeqEntidadeVinculo,
                    p.SeqCursoOfertaLocalidadeTurno
                });

                long[] seqsCursosOfertaLocalidadeTurnoPrograma = null;
                if (configAluno != null && !configAluno.SeqCursoOfertaLocalidadeTurno.HasValue)
                {
                    seqsCursosOfertaLocalidadeTurnoPrograma = CursoOfertaLocalidadeTurnoDomainService
                        .BuscarSeqsCursoOfertaLocalidadePorEntidadeResponsavel(configAluno.SeqEntidadeVinculo);
                }

                // Recupera todos os critérios amarrados ao CursoOfertaLocalidadeTurno informado
                // (ou todos configurados para algum curso oferta localidade turno caso o aluno não tenha um no seu histórico)
                var specCriterios = new CriterioAprovacaoCursoFilterSpecification
                {
                    SeqCursoOfertaLocalidadeTurno = configAluno?.SeqCursoOfertaLocalidadeTurno,
                    SeqsCursoOfertaLocalidadeTurno = seqsCursosOfertaLocalidadeTurnoPrograma
                };
                return CriterioAprovacaoCursoDomainService.SearchProjectionBySpecification(specCriterios, p => new SMCDatasourceItem()
                {
                    Seq = p.SeqCriterioAprovacao,
                    Descricao = p.CriterioAprovacao.Descricao
                }, true).OrderBy(o => o.Descricao).ToList();
            }
            return retorno;
        }
    }
}