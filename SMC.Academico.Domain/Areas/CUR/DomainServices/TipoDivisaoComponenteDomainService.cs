using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Common.Areas.CUR.Includes;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CUR.DomainServices
{
    public class TipoDivisaoComponenteDomainService : AcademicoContextDomain<TipoDivisaoComponente>
    {
        #region [ DomainService ]

        private InstituicaoNivelDomainService InstituicaoNivelDomainService
        {
            get { return this.Create<InstituicaoNivelDomainService>(); }
        }

        private ComponenteCurricularDomainService ComponenteCurricularDomainService
        {
            get { return this.Create<ComponenteCurricularDomainService>(); }
        }

        private AlunoDomainService AlunoDomainService => Create<AlunoDomainService>();

        private DivisaoComponenteDomainService DivisaoComponenteDomainService
        {
            get { return this.Create<DivisaoComponenteDomainService>(); }
        }

        private InstituicaoNivelTipoComponenteCurricularDomainService InstituicaoNivelTipoComponenteCurricularDomainService
        {
            get { return this.Create<InstituicaoNivelTipoComponenteCurricularDomainService>(); }
        }

        #endregion [ DomainService ]

        /// <summary>
        /// Busca os tipo divisão componente de acordo com o tipo componente curricular
        /// </summary>
        /// <param name="seqTipoComponenteCurricular">Sequencia do Tipo Componente selecionado</param>
        /// <returns>Lista de tipos de divisão do componente</returns>
        public List<SMCDatasourceItem> BuscarTipoDivisaoComponenteSelect(long seqTipoComponenteCurricular)
        {
            TipoDivisaoComponenteFilterSpecification spec = new TipoDivisaoComponenteFilterSpecification();

            if (seqTipoComponenteCurricular > 0)
                spec.SeqTipoComponenteCurricular = seqTipoComponenteCurricular;

            var tiposDivisao = this.SearchBySpecification(spec, IncludesTipoDivisaoComponente.Modalidade);

            // Monta o retorno
            List<SMCDatasourceItem> lista = new List<SMCDatasourceItem>();
            foreach (var tipo in tiposDivisao)
            {
                if (tipo.Modalidade == null)
                    lista.Add(new SMCDatasourceItem(tipo.Seq, tipo.Descricao));
                else
                    lista.Add(new SMCDatasourceItem(tipo.Seq, string.Format("{0} - {1}", tipo.Descricao, tipo.Modalidade.Descricao)));
            }
            return lista;
        }

        /// <summary>
        /// Busca os tipo divisão componente de acordo com o componente curricular
        /// </summary>
        /// <param name="seqConfiguracaoComponente">Sequencia do Tipo Componente selecionado</param>
        /// <returns>Lista de tipos de divisão do componente</returns>
        public List<SMCDatasourceItem> BuscarTipoDivisaoComponentePorComponenteSelect(long seqComponenteCurricular)
        {
            if (seqComponenteCurricular == 0)
                return new List<SMCDatasourceItem>();

            var componente = ComponenteCurricularDomainService.SearchProjectionByKey(new SMCSeqSpecification<ComponenteCurricular>(seqComponenteCurricular),
                x => new
                {
                    SeqNivelEnsinoResponsavel = x.NiveisEnsino.FirstOrDefault(y => y.Responsavel).SeqNivelEnsino,
                    SeqTipoComponente = x.SeqTipoComponenteCurricular
                });

            var instituicaoFilter = new InstituicaoNivelTipoComponenteCurricularFilterSpecification
            {
                SeqNivelEnsino = componente.SeqNivelEnsinoResponsavel,
                SeqTipoComponenteCurricular = componente.SeqTipoComponente
            };

            var divisoes = InstituicaoNivelTipoComponenteCurricularDomainService.SearchProjectionByKey(instituicaoFilter, x =>
                x.TiposDivisaoComponente.Select(y => new SMCDatasourceItem
                {
                    Seq = y.SeqTipoDivisaoComponente,
                    Descricao = y.TipoDivisaoComponente.Descricao
                })
            ).OrderBy(o => o.Descricao);

            return divisoes.ToList();
        }

        /// <summary>
        /// Busca o tipo divisão componente por divisão de componente
        /// </summary>
        /// <param name="seqDivisaoComponente">Sequencia do divisão componente</param>
        /// <returns>Dados tipo divisão componente</returns>
        public TipoDivisaoComponente BuscarTipoDivisaoComponentePorDivisaoComponente(long seqDivisaoComponente)
        {
            var tipo = this.DivisaoComponenteDomainService
                            .SearchProjectionByKey(new SMCSeqSpecification<DivisaoComponente>(seqDivisaoComponente),
                            p => p.TipoDivisaoComponente);
            return tipo;
        }

        /// <summary>
        /// Busca os tipo componente curricular de acordo com o parâmetro de tipos gestão divisão componente
        /// </summary>
        /// <param name="tiposGestaoDivisaoComponente">Tipos de gestão divisão componente informados como parâmetro</param>
        /// <returns>Lista de sequenciais tipos componente curricular</returns>
        public List<long> BuscarTipoComponenteCurricularPorTipoGestaoDivisaoComponente(TipoGestaoDivisaoComponente[] tiposGestaoDivisaoComponente)
        {
            TipoDivisaoComponenteFilterSpecification spec = new TipoDivisaoComponenteFilterSpecification();
            spec.TiposGestaoDivisaoComponente = tiposGestaoDivisaoComponente;

            var tiposComponenteCurricular = this.SearchBySpecification(spec);

            List<long> lista = tiposComponenteCurricular.Select(s => s.SeqTipoComponenteCurricular).ToList();

            return lista;
        }

        /// <summary>
        /// Listar, em ordem alfabética, todos os tipos de divisão de componente que estejam associados às divisões de
        /// configurações de componentes do tipo "atividade complementar" e associados ao grupos curriculares do currículo do
        /// aluno em questão.
        /// </summary>
        public List<SMCDatasourceItem> BuscarTiposDivisaoComponenteAlunoComGestao(long seqAluno, TipoGestaoDivisaoComponente tipoGestao)
        {
            var seqCicloAluno = AlunoDomainService.BuscarCicloLetivoAtual(seqAluno);

            var alunoSpec = new SMCSeqSpecification<Aluno>(seqAluno);
            var itens = AlunoDomainService.SearchProjectionByKey(alunoSpec, x =>
                                x.Historicos.FirstOrDefault(f => f.Atual)
                                    .HistoricosCicloLetivo.FirstOrDefault(h => h.SeqCicloLetivo == seqCicloAluno)
                                        .PlanosEstudo.FirstOrDefault(f => f.Atual).MatrizCurricularOferta.MatrizCurricular
                                            .ConfiguracoesComponente.SelectMany(s => s.DivisoesComponente
                                                .Where(a => a.DivisaoComponente.TipoDivisaoComponente.TipoGestaoDivisaoComponente == tipoGestao)
                                                .Select(g => new SMCDatasourceItem
                                                {
                                                    Seq = g.DivisaoComponente.SeqTipoDivisaoComponente,
                                                    Descricao = g.DivisaoComponente.TipoDivisaoComponente.Descricao,
                                                    DataAttributes = new List<SMCKeyValuePair>()
                                                    {
                                                        new SMCKeyValuePair() { Key = "artigo", Value = g.DivisaoComponente.TipoDivisaoComponente.Artigo.HasValue && g.DivisaoComponente.TipoDivisaoComponente.Artigo.Value ? "true" : "false"}
                                                    }
                                                })));
            return itens.SMCDistinct(f => f.Descricao).OrderBy(o => o.Descricao).ToList();
        }
    }
}