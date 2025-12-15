using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Academico.Domain.Helpers;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CUR.DomainServices
{
    public class DivisaoComponenteDomainService : AcademicoContextDomain<DivisaoComponente>
    {
        #region [ DomainService ]

        private InstituicaoNivelTipoComponenteCurricularDomainService InstituicaoNivelTipoComponenteCurricularDomainService
        {
            get { return this.Create<InstituicaoNivelTipoComponenteCurricularDomainService>(); }
        }

        private AlunoDomainService AlunoDomainService => Create<AlunoDomainService>();

        #endregion [ DomainService ]

        /// <summary>
        /// Busca as divisões de uma configuração de componente curricular
        /// </summary>
        /// <param name="seqConfiguracaoCompoente">Sequencial da configuração de componente curricular</param>
        /// <returns>Dados das divisões de componentes curriculares</returns>
        public List<SMCDatasourceItem> BuscarDivisoesCompoentePorConfiguracao(long seqConfiguracaoCompoente)
        {
            FilterHelper.DesativarFiltros(this);
            var spec = new DivisaoComponenteFilterSpecification() { SeqConfiguracaoComponente = seqConfiguracaoCompoente };
            var registro = this.SearchProjectionBySpecification(spec, p => new DivisaoComponenteVO()
            {
                Seq = p.Seq,
                Numero = p.Numero,
                DescricaoTipoComponente = p.TipoDivisaoComponente.Descricao,
                CargaHoraria = p.CargaHoraria,
                SeqTipoComponenteCurricular = p.ConfiguracaoComponente.ComponenteCurricular.SeqTipoComponenteCurricular,
                SeqNivelEnsino = p.ConfiguracaoComponente.ComponenteCurricular.NiveisEnsino.Where(w => w.Responsavel == true).FirstOrDefault().SeqNivelEnsino,
                PermiteGrupo = p.PermiteGrupo,
                Artigo = p.TipoDivisaoComponente.Artigo ?? false,
                DescricaoComponenteCurricularOrganizacao = p.ComponenteCurricularOrganizacao.Descricao,
                CargaHorariaGrade = p.CargaHorariaGrade
            }).ToList();

            List<SMCDatasourceItem> result = new List<SMCDatasourceItem>();
            foreach (var item in registro.ToList())
            {
                var formato = InstituicaoNivelTipoComponenteCurricularDomainService.SearchProjectionByKey(new InstituicaoNivelTipoComponenteCurricularFilterSpecification()
                {
                    SeqNivelEnsino = item.SeqNivelEnsino,
                    SeqTipoComponenteCurricular = item.SeqTipoComponenteCurricular
                }, i => i.FormatoCargaHoraria);

                result.Add(new SMCDatasourceItem()
                {
                    Seq = item.Seq,
                    Descricao = $"{item.Numero} - {(string.IsNullOrEmpty(item.DescricaoComponenteCurricularOrganizacao) ? string.Empty : item.DescricaoComponenteCurricularOrganizacao + " -")} {item.DescricaoTipoComponente} - {item.CargaHoraria} {formato}",
                    DataAttributes = new List<SMCKeyValuePair>() {
                        new SMCKeyValuePair() { Key = "permite-grupo", Value = item.PermiteGrupo.ToString() },
                        new SMCKeyValuePair() { Key = "tipo-artigo", Value = item.Artigo.ToString() },
                        new SMCKeyValuePair() { Key = "carga-horaria-grade", Value = item.CargaHorariaGrade?.ToString() }
                    }
                });
            }
            FilterHelper.AtivarFiltros(this);
            return result;
        }

        /// <summary>
        /// Listar em ordem alfabética todas as configurações de componente associadas à matriz curricular do aluno em questão
        /// e que possuem divisões de tipo cuja gestão é igual a "Entrega de comprovante"
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno utilizado para descobrir a matriz curricular.</param>
        public List<SMCDatasourceItem> BuscarDivisaoComponenteAluno(long seqAluno)
        {
            var seqAlunoCicloLetivo = AlunoDomainService.BuscarCicloLetivoAtual(seqAluno);
            var alunoSpec = new SMCSeqSpecification<Aluno>(seqAluno);
            var items = AlunoDomainService.SearchProjectionByKey(alunoSpec, x =>
                                x.Historicos.FirstOrDefault(f => f.Atual)
                                    .HistoricosCicloLetivo.FirstOrDefault(c => c.SeqCicloLetivo == seqAlunoCicloLetivo)
                                        .PlanosEstudo.FirstOrDefault(f => f.Atual).MatrizCurricularOferta.MatrizCurricular
                                            .ConfiguracoesComponente.SelectMany(s => s.DivisoesComponente
                                                .Where(w => w.DivisaoComponente.TipoDivisaoComponente.TipoGestaoDivisaoComponente == TipoGestaoDivisaoComponente.EntregaComprovante)
                                                .Select(g => new SMCDatasourceItem
                                                {
                                                    DataAttributes = new List<SMCKeyValuePair> {
                                                        new SMCKeyValuePair { Key = "artigo", Value = g.DivisaoComponente.TipoDivisaoComponente.Artigo.ToString() },
                                                        new SMCKeyValuePair { Key = "minimo", Value = ((int)g.ComprovacaoArtigoMinima).ToString() },
                                                        new SMCKeyValuePair { Key = "apuracaonota", Value = g.DivisaoMatrizCurricularComponente.CriterioAprovacao.ApuracaoNota.ToString() },
                                                    },
                                                    Seq = g.SeqDivisaoComponente,
                                                    Descricao = (g.DivisaoComponente.ConfiguracaoComponente.ComponenteCurricular.CargaHoraria != null) ?
                                                                    g.DivisaoComponente.ConfiguracaoComponente.Descricao + " - " + g.DivisaoComponente.ConfiguracaoComponente.ComponenteCurricular.CargaHoraria :
                                                                    g.DivisaoComponente.ConfiguracaoComponente.Descricao,
                                                })));

            return items.SMCDistinct(d => d.Descricao).OrderBy(o => o.Descricao).ToList();
        }

        /// <summary>
        /// Retorna descrição divisão componente (RN_CUR_037)
        /// </summary>
        /// <param name="seqDivisaoCompoente">Sequencial divisão componente</param>
        public string BuscarDescricaoDivisaoCompoente(long seqDivisaoCompoente)
        {
            var spec = new DivisaoComponenteFilterSpecification() { Seq = seqDivisaoCompoente };
            var registro = this.SearchProjectionByKey(spec, p => new DivisaoComponenteVO()
            {
                Seq = p.Seq,
                Numero = p.Numero,
                DescricaoTipoComponente = p.TipoDivisaoComponente.Descricao,
                CargaHoraria = p.CargaHoraria,
                SeqTipoComponenteCurricular = p.ConfiguracaoComponente.ComponenteCurricular.SeqTipoComponenteCurricular,
                SeqNivelEnsino = p.ConfiguracaoComponente.ComponenteCurricular.NiveisEnsino.Where(w => w.Responsavel == true).FirstOrDefault().SeqNivelEnsino,
                DescricaoComponenteCurricularOrganizacao = p.ComponenteCurricularOrganizacao.Descricao,
            });

            var formato = InstituicaoNivelTipoComponenteCurricularDomainService.SearchProjectionByKey(new InstituicaoNivelTipoComponenteCurricularFilterSpecification()
            {
                SeqNivelEnsino = registro.SeqNivelEnsino,
                SeqTipoComponenteCurricular = registro.SeqTipoComponenteCurricular
            }, i => i.FormatoCargaHoraria);

            var descricao = $"{registro.Numero} - {(string.IsNullOrEmpty(registro.DescricaoComponenteCurricularOrganizacao) ? string.Empty : registro.DescricaoComponenteCurricularOrganizacao + " -")} {registro.DescricaoTipoComponente} - {registro.CargaHoraria} {formato}";

            return descricao;
        }
    }
}