using SMC.Academico.Common.Areas.GRD.Enums;
using SMC.Academico.Common.Areas.GRD.Exceptions;
using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.Domain.Areas.FIN.Specifications;
using SMC.Academico.Domain.Areas.GRD.Models;
using SMC.Academico.Domain.Areas.GRD.Specifications;
using SMC.Academico.Domain.Areas.GRD.ValueObjects;
using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Academico.Domain.Areas.TUR.Models;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.Framework.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.GRD.DomainServices
{
    public class HistoricoDivisaoTurmaConfiguracaoGradeDomainService : AcademicoContextDomain<HistoricoDivisaoTurmaConfiguracaoGrade>
    {
        #region DomainServices

        private EventoAulaDomainService EventoAulaDomainService
        {
            get { return this.Create<EventoAulaDomainService>(); }
        }

        private DivisaoTurmaDomainService DivisaoTurmaDomainService
        {
            get { return this.Create<DivisaoTurmaDomainService>(); }
        }

        private InstituicaoNivelTipoComponenteCurricularDomainService InstituicaoNivelTipoComponenteCurricularDomainService
        {
            get { return this.Create<InstituicaoNivelTipoComponenteCurricularDomainService>(); }
        }

        private ComponenteCurricularDomainService ComponenteCurricularDomainService
        {
            get { return this.Create<ComponenteCurricularDomainService>(); }
        }

        private TurmaDomainService TurmaDomainService
        {
            get { return this.Create<TurmaDomainService>(); }
        }

        private MatrizCurricularDivisaoComponenteDomainService MatrizCurricularDivisaoComponenteDomainService
        {
            get { return this.Create<MatrizCurricularDivisaoComponenteDomainService>(); }
        }

        #endregion DomainServices

        public string BuscarCabecalhoConfiguracaoGrade(long seqDivisaoTurma)
        {
            var dadosCabecalho = this.DivisaoTurmaDomainService.SearchProjectionByKey(new SMCSeqSpecification<DivisaoTurma>(seqDivisaoTurma), x => new
            {
                CodigoTurma = x.Turma.Codigo,
                NumeroTurma = x.Turma.Numero,
                NumeroDivisao = x.DivisaoComponente.Numero,
                NumeroGrupo = x.NumeroGrupo,
                CodigoConfiguracaoComponente = x.DivisaoComponente.ConfiguracaoComponente.Codigo,
                DescricaoConfiguracaoComponente = x.DivisaoComponente.ConfiguracaoComponente.Descricao,
                DescricaoComplementarConfiguracaoComponente = x.DivisaoComponente.ConfiguracaoComponente.DescricaoComplementar,
                ComponenteCurricularCredito = x.DivisaoComponente.ConfiguracaoComponente.ComponenteCurricular.Credito,
                ComponenteCurricularCargaHoraria = x.DivisaoComponente.ConfiguracaoComponente.ComponenteCurricular.CargaHoraria,
                SeqComponenteCurricular = x.DivisaoComponente.ConfiguracaoComponente.SeqComponenteCurricular,
                SeqTipoComponenteCurricular = x.DivisaoComponente.ConfiguracaoComponente.ComponenteCurricular.SeqTipoComponenteCurricular,
                EntidadesResponsaveis = x.DivisaoComponente.ConfiguracaoComponente.ComponenteCurricular.EntidadesResponsaveis.Select(s => s.Entidade.Sigla).ToList()
            });

            var componenteCurricular = this.ComponenteCurricularDomainService.SearchByKey(new SMCSeqSpecification<ComponenteCurricular>(dadosCabecalho.SeqComponenteCurricular), x => x.NiveisEnsino);

            var seqNivelEnsino = componenteCurricular.RecuperarSeqNivelEnsinoResponsavel();

            var tiposComponenteNivel = this.InstituicaoNivelTipoComponenteCurricularDomainService
                .SearchProjectionAll(p => new
                {
                    p.InstituicaoNivel.SeqNivelEnsino,
                    p.SeqTipoComponenteCurricular,
                    p.FormatoCargaHoraria
                }).ToList();

            var formatoCargaHoraria = tiposComponenteNivel.FirstOrDefault(
                   f => f.SeqNivelEnsino == seqNivelEnsino
                   && f.SeqTipoComponenteCurricular == dadosCabecalho.SeqTipoComponenteCurricular)?.FormatoCargaHoraria;

            var codificacaoTurma = $"{dadosCabecalho.CodigoTurma}.{dadosCabecalho.NumeroTurma}.{dadosCabecalho.NumeroDivisao.ToString()}.{dadosCabecalho.NumeroGrupo.ToString().PadLeft(3, '0')}";

            var descricaoConfiguracaoComponente = ConfiguracaoComponenteDomainService.GerarDescricaoConfiguracaoComponenteCurricular(
                      dadosCabecalho.CodigoConfiguracaoComponente,
                      dadosCabecalho.DescricaoConfiguracaoComponente,
                      dadosCabecalho.DescricaoComplementarConfiguracaoComponente,
                      dadosCabecalho.ComponenteCurricularCredito,
                      dadosCabecalho.ComponenteCurricularCargaHoraria,
                      formatoCargaHoraria,
                      dadosCabecalho.EntidadesResponsaveis);

            return $"{codificacaoTurma} - {descricaoConfiguracaoComponente}";
        }

        public HistoricoDivisaoTurmaConfiguracaoGradeVO BuscarNovaConfiguracaoGrade(long seqDivisaoTurma)
        {
            var modelo = new HistoricoDivisaoTurmaConfiguracaoGradeVO() { SeqDivisaoTurma = seqDivisaoTurma };

            var qtdeConfiguracoesGrade = this.Count(new HistoricoDivisaoTurmaConfiguracaoGradeFilterSpecification() { SeqDivisaoTurma = seqDivisaoTurma });

            if (qtdeConfiguracoesGrade > 0)
            {
                modelo.DataInicio = DateTime.Now.Date;
            }
            else
            {
                var divisaoTurma = this.DivisaoTurmaDomainService.SearchByKey(new SMCSeqSpecification<DivisaoTurma>(seqDivisaoTurma));

                var dadosTurma = this.TurmaDomainService.SearchProjectionByKey(new SMCSeqSpecification<Turma>(divisaoTurma.SeqTurma), x => new
                {
                    x.DataInicioPeriodoLetivo,
                    x.ConfiguracoesComponente.SelectMany(c => c.RestricoesTurmaMatriz).FirstOrDefault(a => a.OfertaMatrizPrincipal).SeqMatrizCurricularOferta
                });

                modelo.DataInicio = dadosTurma.DataInicioPeriodoLetivo.Date;

                if (dadosTurma.SeqMatrizCurricularOferta.HasValue)
                {
                    var matrizCurricularDivisaoComponente = this.MatrizCurricularDivisaoComponenteDomainService.SearchBySpecification(new MatrizCurricularDivisaoComponenteFilterSpecification() { SeqMatrizCurricularOferta = dadosTurma.SeqMatrizCurricularOferta, SeqDivisaoComponente = divisaoTurma.SeqDivisaoComponente }).FirstOrDefault();

                    if (matrizCurricularDivisaoComponente != null)
                    {
                        modelo.AulaSabado = matrizCurricularDivisaoComponente.AulaSabado.GetValueOrDefault();
                        modelo.TipoDistribuicaoAula = matrizCurricularDivisaoComponente.TipoDistribuicaoAula.GetValueOrDefault();
                        modelo.TipoPagamentoAula = matrizCurricularDivisaoComponente.TipoPagamentoAula.GetValueOrDefault();
                        modelo.TipoPulaFeriado = matrizCurricularDivisaoComponente.TipoPulaFeriado.GetValueOrDefault();
                    }
                }
            }

            return modelo;
        }

        public HistoricoDivisaoTurmaConfiguracaoGradeVO BuscarHistoricoDivisaoConfiguracaoGrade(long seq)
        {
            var spec = new SMCSeqSpecification<HistoricoDivisaoTurmaConfiguracaoGrade>(seq);

            return this.SearchProjectionByKey(spec, x => new HistoricoDivisaoTurmaConfiguracaoGradeVO
            {
                Seq = x.Seq,
                SeqDivisaoTurma = x.SeqDivisaoTurma,
                DataInicio = x.DataInicio,
                TipoDistribuicaoAula = x.TipoDistribuicaoAula,
                TipoPulaFeriado = x.TipoPulaFeriado,
                AulaSabado = x.AulaSabado,
                TipoPagamentoAula = x.TipoPagamentoAula
            });
        }

        public SMCPagerData<HistoricoDivisaoTurmaConfiguracaoGradeListarVO> BuscarHistoricosDivisaoConfiguracaoGrade(HistoricoDivisaoTurmaConfiguracaoGradeFiltroVO filtros)
        {
            int total = 0;
            var spec = filtros.Transform<HistoricoDivisaoTurmaConfiguracaoGradeFilterSpecification>();
            spec.SetOrderBy(o => o.DataInclusao);

            var lista = SearchProjectionBySpecification(spec, x => new HistoricoDivisaoTurmaConfiguracaoGradeListarVO
            {
                Seq = x.Seq,
                SeqDivisaoTurma = x.SeqDivisaoTurma,
                DataInicio = x.DataInicio,
                TipoDistribuicaoAula = x.TipoDistribuicaoAula,
                TipoPulaFeriado = x.TipoPulaFeriado,
                AulaSabado = x.AulaSabado,
                TipoPagamentoAula = x.TipoPagamentoAula

            }, out total).ToList();

            return new SMCPagerData<HistoricoDivisaoTurmaConfiguracaoGradeListarVO>(lista, total);
        }

        public List<SMCDatasourceItem> BuscarTiposPagamentoSelect(TipoDistribuicaoAula tipoDistribuicaoAula)
        {
            var retorno = new List<SMCDatasourceItem>();

            if (tipoDistribuicaoAula == TipoDistribuicaoAula.Quinzenal || tipoDistribuicaoAula == TipoDistribuicaoAula.Concentrada)
            {
                retorno.Add(new SMCDatasourceItem() { Seq = (long)TipoPagamentoAula.AulaSemanal, Descricao = SMCEnumHelper.GetDescription(TipoPagamentoAula.AulaSemanal) });
                retorno.Add(new SMCDatasourceItem() { Seq = (long)TipoPagamentoAula.AulaFracionada, Descricao = SMCEnumHelper.GetDescription(TipoPagamentoAula.AulaFracionada) });
            }
            else if (tipoDistribuicaoAula == TipoDistribuicaoAula.Livre)
            {
                retorno.Add(new SMCDatasourceItem() { Seq = (long)TipoPagamentoAula.AulaExecutada, Descricao = SMCEnumHelper.GetDescription(TipoPagamentoAula.AulaExecutada) });
            }
            else if (tipoDistribuicaoAula == TipoDistribuicaoAula.Semanal)
            {
                foreach (var item in Enum.GetValues(typeof(TipoPagamentoAula)).Cast<TipoPagamentoAula>())
                {
                    if ((long)item != 0)
                    {
                        retorno.Add(new SMCDatasourceItem() { Seq = (long)item, Descricao = SMCEnumHelper.GetDescription(item) });
                    }
                }
            }

            return retorno;
        }

        public List<SMCDatasourceItem> BuscarTiposPulaFeriadoSelect(TipoDistribuicaoAula tipoDistribuicaoAula)
        {
            var retorno = new List<SMCDatasourceItem>();

            if (tipoDistribuicaoAula == TipoDistribuicaoAula.Quinzenal)
            {
                foreach (var item in Enum.GetValues(typeof(TipoPulaFeriado)).Cast<TipoPulaFeriado>())
                {
                    if ((long)item != 0)
                    {
                        retorno.Add(new SMCDatasourceItem() { Seq = (long)item, Descricao = SMCEnumHelper.GetDescription(item) });
                    }
                }
            }
            else
            {
                foreach (var item in Enum.GetValues(typeof(TipoPulaFeriado)).Cast<TipoPulaFeriado>())
                {
                    if ((long)item != 0 && item != TipoPulaFeriado.PulaConjugado)
                    {
                        retorno.Add(new SMCDatasourceItem() { Seq = (long)item, Descricao = SMCEnumHelper.GetDescription(item) });
                    }
                }
            }

            return retorno;
        }

        public long SalvarHistoricoDivisaoConfiguracaoGrade(HistoricoDivisaoTurmaConfiguracaoGradeVO modelo)
        {
            ValidarModeloSalvar(modelo);

            var dominio = modelo.Transform<HistoricoDivisaoTurmaConfiguracaoGrade>();

            this.SaveEntity(dominio);

            return dominio.Seq;
        }

        public void ValidarModeloSalvar(HistoricoDivisaoTurmaConfiguracaoGradeVO modelo)
        {
            if (modelo.Seq == 0)
            {
                var configuracaoGradePorData = this.SearchBySpecification(new HistoricoDivisaoTurmaConfiguracaoGradeFilterSpecification() { SeqDivisaoTurma = modelo.SeqDivisaoTurma, DataInicio = modelo.DataInicio }).ToList();

                if (configuracaoGradePorData.Any(a => a.Seq != modelo.Seq))
                    throw new ConfiguracaoGradeJaCadastradaComMesmaDataException();
            }

            var eventosAula = this.EventoAulaDomainService.SearchBySpecification(new EventoAulaFilterSpecification() { SeqDivisaoTurma = modelo.SeqDivisaoTurma }).ToList();

            foreach (var eventoAula in eventosAula)
            {
                if (eventoAula.Data >= modelo.DataInicio)
                    throw new ConfiguracaoGradeAcoesNaoPermitidasEventoAulaException();
            }
        }

        public void ExcluirHistoricoDivisaoConfiguracaoGrade(long seq)
        {
            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                try
                {
                    ValidarModeloExcluir(seq);

                    var configToDelete = this.SearchByKey(new SMCSeqSpecification<HistoricoDivisaoTurmaConfiguracaoGrade>(seq));

                    var divisaoTurma = this.DivisaoTurmaDomainService.SearchByKey(new SMCSeqSpecification<DivisaoTurma>(configToDelete.SeqDivisaoTurma));

                    if (divisaoTurma.SeqHistoricoConfiguracaoGradeAtual == seq)
                    {
                        divisaoTurma.SeqHistoricoConfiguracaoGradeAtual = null;
                        this.DivisaoTurmaDomainService.SaveEntity(divisaoTurma);
                    }

                    this.DeleteEntity(configToDelete);

                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    throw;
                }
            }
        }

        public void ValidarModeloExcluir(long seq)
        {
            var configuracaoGrade = this.SearchByKey(new SMCSeqSpecification<HistoricoDivisaoTurmaConfiguracaoGrade>(seq));
            var quantidadeConfiguracoesGrade = this.Count(new HistoricoDivisaoTurmaConfiguracaoGradeFilterSpecification() { SeqDivisaoTurma = configuracaoGrade.SeqDivisaoTurma });

            if (quantidadeConfiguracoesGrade == 1)
                throw new ConfiguracaoGradeExclusaoNaoPermitidaException();

            var eventosAula = this.EventoAulaDomainService.SearchBySpecification(new EventoAulaFilterSpecification() { SeqDivisaoTurma = configuracaoGrade.SeqDivisaoTurma }).ToList();

            foreach (var eventoAula in eventosAula)
            {
                if (eventoAula.Data >= configuracaoGrade.DataInicio)
                    throw new ConfiguracaoGradeAcoesNaoPermitidasEventoAulaException();
            }
        }
    }
}
