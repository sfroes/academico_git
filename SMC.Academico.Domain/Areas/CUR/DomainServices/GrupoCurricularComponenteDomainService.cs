using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Common.Areas.CUR.Exceptions;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Academico.Domain.Areas.TUR.Specifications;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CUR.DomainServices
{
    public class GrupoCurricularComponenteDomainService : AcademicoContextDomain<GrupoCurricularComponente>
    {
        #region DomainService

        private FormacaoEspecificaDomainService FormacaoEspecificaDomainService => Create<FormacaoEspecificaDomainService>();
        private MatrizCurricularDomainService MatrizCurricularDomainService => Create<MatrizCurricularDomainService>();
        private ComponenteCurricularDomainService ComponenteCurricularDomainService => Create<ComponenteCurricularDomainService>();
        private AlunoDomainService AlunoDomainService { get { return this.Create<AlunoDomainService>(); } }
        private GrupoCurricularDomainService GrupoCurricularDomainService { get { return this.Create<GrupoCurricularDomainService>(); } }
        private DivisaoMatrizCurricularComponenteDomainService DivisaoMatrizCurricularComponenteDomainService => this.Create<DivisaoMatrizCurricularComponenteDomainService>();
        private TurmaConfiguracaoComponenteDomainService TurmaConfiguracaoComponenteDomainService => this.Create<TurmaConfiguracaoComponenteDomainService>();

        #endregion DomainService

        public IEnumerable<GrupoCurricularComponenteListaVO> BuscarGruposCurricularesComponentesLookup(GrupoCurricularComponenteFiltroVO filtro)
        {
            var specAluno = new SMCSeqSpecification<Aluno>(filtro.SeqPessoaAtuacao);

            long seqCurriculoCursoOferta = 0;
            if (filtro.SeqMatrizCurricular.HasValue)
            {
                seqCurriculoCursoOferta = MatrizCurricularDomainService.SearchProjectionByKey(filtro.SeqMatrizCurricular.Value, x => x.SeqCurriculoCursoOferta);
            }
            else if (filtro.SeqCicloLetivo.HasValue)
            {
                seqCurriculoCursoOferta = this.AlunoDomainService.SearchProjectionByKey(specAluno, a => a
                                                .Historicos.FirstOrDefault(h => h.Atual)
                                                .HistoricosCicloLetivo.Where(h => h.SeqCicloLetivo == filtro.SeqCicloLetivo).FirstOrDefault(s => !s.DataExclusao.HasValue)
                                                .PlanosEstudo.FirstOrDefault(p => p.Atual)
                                                .MatrizCurricularOferta.MatrizCurricular.SeqCurriculoCursoOferta);
            }
            else
                throw new SMCApplicationException("É obrigatório informar o sequencial do ciclo letivo ou o sequencial da matriz curricular.");

            /* NV02
               Não exibir ou não permitir seleção de componente cujo tipo foi parametrizado em "Tipo de Componente por Instituição e Nível de Ensino" para não permitir cadastro de dispensa.

               NV03
               Não permitir seleção itens já cursados com aprovação pelo aluno ou dispensados para o aluno em questão*/

            var gruposCurricularesListaVO = this.GrupoCurricularDomainService.BuscarGruposCurricularesLookup(new GrupoCurricularFiltroVO()
            {
                SeqCurriculoCursoOferta = seqCurriculoCursoOferta,
                SelecionarComponente = true,
                DesconsiderarItensQueNaoPermitemCadastroDispensa = true,
                DesconsiderarItensCursadosAprovacaoOuDispensadosAluno = true,
                SeqPessoaAtuacao = filtro.SeqPessoaAtuacao,
                FiltrarFormacoesEspecificasAluno = filtro.FiltrarFormacoesEspecificasAluno
            });

            return gruposCurricularesListaVO.TransformList<GrupoCurricularComponenteListaVO>();
        }

        public GrupoCurricularComponenteVO[] BuscarGruposCurricularesComponentesLookupSelecionado(long[] seqsGruposCurricularesComponentes)
        {
            var specGrupoCurricularComponente = new SMCContainsSpecification<GrupoCurricularComponente, long>(g => g.Seq, seqsGruposCurricularesComponentes);

            var gruposCurricularesComponentes = this.SearchProjectionBySpecification(specGrupoCurricularComponente, g => new
            {
                Seq = g.Seq,
                SeqNivelEnsino = g.ComponenteCurricular.TipoComponente.InstituicoesNivelTipoComponenteCurricular.FirstOrDefault(i => i.SeqTipoComponenteCurricular == g.ComponenteCurricular.SeqTipoComponenteCurricular).InstituicaoNivel.SeqNivelEnsino,
                Codigo = g.ComponenteCurricular.Codigo,
                CargaHoraria = g.ComponenteCurricular.CargaHoraria,
                Credito = g.ComponenteCurricular.Credito,
                Descricao = g.ComponenteCurricular.Descricao,
                SeqTipoComponenteCurricular = g.ComponenteCurricular.SeqTipoComponenteCurricular,
                Formato = g.ComponenteCurricular.TipoComponente.InstituicoesNivelTipoComponenteCurricular.FirstOrDefault(i => i.SeqTipoComponenteCurricular == g.ComponenteCurricular.SeqTipoComponenteCurricular).FormatoCargaHoraria,

                DescricaoGrupoCurricular = g.GrupoCurricular.Descricao,
                TipoConfiguracaoDescricao = g.GrupoCurricular.TipoConfiguracaoGrupoCurricular.Descricao,
                QuantidadeCreditos = g.GrupoCurricular.QuantidadeCreditos,
                QuantidadeHoraAula = g.GrupoCurricular.QuantidadeHoraAula,
                QuantidadeHoraRelogio = g.GrupoCurricular.QuantidadeHoraRelogio,
                QuantidadeItens = g.GrupoCurricular.QuantidadeItens,
                FormatoConfiguracaoGrupo = g.GrupoCurricular.FormatoConfiguracaoGrupo,
            }).ToArray();

            var ret = gruposCurricularesComponentes.Select(g =>
            {
                var item = SMCMapperHelper.Create<GrupoCurricularComponenteVO>(g);
                item.DescricaoGrupoCurricular = GrupoCurricularDomainService.GerarDescricaoGrupoCurricular(
                    g.DescricaoGrupoCurricular,
                    g.TipoConfiguracaoDescricao,
                    g.FormatoConfiguracaoGrupo,
                    g.QuantidadeHoraRelogio,
                    g.QuantidadeHoraAula,
                    g.QuantidadeCreditos,
                    g.QuantidadeItens);

                return item;
            }).ToArray();

            return ret;
        }

        /// <summary>
        /// Calcular os valores totais dos grupos de componentes informados
        /// </summary>
        /// <param name="seqsGruposCurricularesComponentes">Lista de sequenciais dos grupos de componente</param>
        /// <returns>Objeto com o total de hora, hora-aula e créditos</returns>
        public TotalHoraCreditoVO CalculaHoraCreditoGrupoCurricularComponente(List<long> seqsGruposCurricularesComponentes)
        {
            float totalHoras = 0;
            float totalHorasAula = 0;
            float totalCreditos = 0;

            var specGrupoCurricularComponente = new SMCContainsSpecification<GrupoCurricularComponente, long>(g => g.Seq, seqsGruposCurricularesComponentes.ToArray());

            var componentesCurriculares = this.SearchProjectionBySpecification(specGrupoCurricularComponente, g => new
            {
                g.ComponenteCurricular.Seq,
                g.ComponenteCurricular.CargaHoraria,
                g.ComponenteCurricular.Credito,
                g.ComponenteCurricular.TipoComponente.InstituicoesNivelTipoComponenteCurricular.FirstOrDefault(i => i.SeqTipoComponenteCurricular == g.ComponenteCurricular.SeqTipoComponenteCurricular).FormatoCargaHoraria,
            }).ToList();

            foreach (var componenteCurricular in componentesCurriculares)
            {
                totalCreditos += componenteCurricular.Credito.GetValueOrDefault();

                switch (componenteCurricular.FormatoCargaHoraria)
                {
                    case FormatoCargaHoraria.Hora:
                        totalHorasAula += (componenteCurricular.CargaHoraria.GetValueOrDefault() * 60F) / 50F;
                        totalHoras += componenteCurricular.CargaHoraria.GetValueOrDefault();
                        break;

                    case FormatoCargaHoraria.HoraAula:
                        totalHorasAula += componenteCurricular.CargaHoraria.GetValueOrDefault();
                        totalHoras += (componenteCurricular.CargaHoraria.GetValueOrDefault() * 50F) / 60F;
                        break;
                }
            }

            return new TotalHoraCreditoVO() { TotalHoras = Convert.ToDecimal(totalHoras), TotalHorasAula = Convert.ToDecimal(totalHorasAula), TotalCreditos = Convert.ToDecimal(totalCreditos) };
        }

        public List<SMCDatasourceItem> BuscarComponentesCurricularesPadrao(long seqCurriculoCursoOferta)
        {

            var spec = new GrupoCurricularComponenteFilterSpecification()
            {
                SeqCurriculoCursoOferta = seqCurriculoCursoOferta,
                SiglaComponente = "ATA",
                ComponenteAtivo = true
            };

            var retorno = SearchProjectionBySpecification(spec, c => new SMCDatasourceItem
            {
                Seq = c.ComponenteCurricular.Seq,
                Descricao = c.ComponenteCurricular.Descricao
            }, true).ToList();


            return retorno;
        }

        public void Excluir(long seq)
        {
            var seqDivisaoMatrizCurricularComponente = ValidarAoExcluir(seq);

            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                try
                {
                    if (seqDivisaoMatrizCurricularComponente != 0)
                        DivisaoMatrizCurricularComponenteDomainService.ExcluirConfiguracaoComponente(seqDivisaoMatrizCurricularComponente);

                    var grupoCurricularComponente = this.SearchByKey(new SMCSeqSpecification<GrupoCurricularComponente>(seq));
                    this.DeleteEntity(grupoCurricularComponente);

                    unitOfWork.Commit();
                }
                catch (Exception)
                {
                    unitOfWork.Rollback();
                    throw;
                }
            }
        }

        public long ValidarAoExcluir(long seq)
        {
            var spec = new DivisaoMatrizCurricularComponenteFilterSpecification() { SeqGrupoCurricularComponente = seq };
            var divisaoMatrizCurricularComponente = DivisaoMatrizCurricularComponenteDomainService.SearchByKey(spec, x => x.ConfiguracaoComponente, x => x.MatrizCurricular);
            if (divisaoMatrizCurricularComponente != null)
            {
                var specTurmaConfiguracaoComponente = new TurmaConfiguracaoComponenteFilterSpecification()
                {
                    SeqConfiguracaoComponente = divisaoMatrizCurricularComponente.SeqConfiguracaoComponente,
                    SeqMatrizCurricular = divisaoMatrizCurricularComponente.SeqMatrizCurricular,
                    SituacaoTurmaAtual = Common.Areas.TUR.Enums.SituacaoTurma.Ofertada
                };

                var dadosTurma = TurmaConfiguracaoComponenteDomainService.SearchProjectionBySpecification(specTurmaConfiguracaoComponente, x => new
                {
                    CodigoTurma = x.Turma.Codigo,
                    NumeroTurma = x.Turma.Numero,
                    x.Turma.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoTurma
                }).ToList();

                if (dadosTurma.Any())
                {
                    var turma = string.Empty;
                    foreach (var turmaOfertada in dadosTurma)
                        turma += $"<br /> {turmaOfertada.CodigoTurma}.{turmaOfertada.NumeroTurma}";
                    throw new GrupoConfiguracaoComponenteExclusaoNaoPermitidaException(divisaoMatrizCurricularComponente.MatrizCurricular.Codigo, turma);
                }
            }

            return divisaoMatrizCurricularComponente != null ? divisaoMatrizCurricularComponente.Seq : 0;
        }
    }
}