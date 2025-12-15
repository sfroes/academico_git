using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Framework.Mapper;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CSO.Specifications
{
    public class CursoOfertaLocalidadeTurnoFilterSpecification : SMCSpecification<CursoOfertaLocalidadeTurno>, ISMCMappable
    {
        public long? Seq { get; set; }

        public long[] Seqs { get; set; }

        public long? SeqCursoOfertaLocalidade { get; set; }

        public long? SeqTurno { get; set; }

        public long? SeqLocalidade { get; set; }

        public long? SeqCursoOferta { get; set; }

        public long? SeqCurso { get; set; }
        
        public long[] SeqsCurso { get; set; }

        public bool? Ativo { get; set; }

        public long? SeqEntidadeResponsavel { get; set; }

        public List<long> SeqsEntidadeResponsavel { get; set; }

        public long? SeqFormacaoEspecifica { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public DateTime? DataInicioPeriodoLetivo { get; set; }

        /// <summary>
        ///  Não retornar a oferta se: O curso do curso-oferta-localidade-turno da oferta
        ///  estiver com a categoria da situação "Em desativação" ou "Inativa"
        ///  na data início do período letivo
        /// </summary>
        public bool? CursoAtivoDataInicioCicloLetivo { get; set; }

        /// <summary>
        /// Não reotrnar a oferta se: O curso-unidade do curso-oferta-localidade-turno estiver
        /// com a categoria da situação "Em desativação" ou· "Inativa" na data início do período letivo
        /// </summary>
        public bool? CursoUnidadeAtivoDataInicioCicloLetivo { get; set; }

        /// <summary>
        /// Não reotrnar a oferta se: O curso-oferta do curso-oferta-localidade-turno estiver desativado.
        /// </summary>
        public bool? CursoOfertaAtivo { get; set; }

        /// <summary>
        /// Não reotrnar a oferta se: O curso-oferta-localidade do curso-oferta-localidade-turno estiver com
        /// a categoria da situação “Em desativação”· ou “Inativa” na data início do período letivo
        /// </summary>
        public bool? CursoOfertaLocalidadeAtivoDataInicioCicloLetivo { get; set; }

        /// <summary>
        /// Não reotrnar a oferta se: A formação específica estiver desativada ou a formação
        /// específica por curso não estiver mais vigente na data· início do período letivo
        /// </summary>
        public bool? FormacaoEspecificaAtivoDataInicioCicloLetivo { get; set; }

        /// <summary>
        /// Não reotrnar a oferta se: O turno do curso-oferta-localidade-turno estiver
        /// desativado para o curso-oferta-localidade do· curso-oferta-localidade-turno.
        /// </summary>
        public bool? TurnoAtivo { get; set; }

        public bool? CursoOfertaLocalidadeAtivo { get; set; }

        public override Expression<Func<CursoOfertaLocalidadeTurno, bool>> SatisfiedBy()
        {
            AddExpression(Seq, w => w.Seq == this.Seq);
            AddExpression(Seqs, w => Seqs.Contains(w.Seq));
            AddExpression(SeqCursoOfertaLocalidade, w => w.SeqCursoOfertaLocalidade == this.SeqCursoOfertaLocalidade);
            AddExpression(SeqTurno, w => w.SeqTurno == this.SeqTurno);
            AddExpression(SeqCursoOferta, w => w.CursoOfertaLocalidade.SeqCursoOferta == this.SeqCursoOferta);
            AddExpression(SeqCurso, w => w.CursoOfertaLocalidade.CursoUnidade.SeqCurso == this.SeqCurso);
            AddExpression(SeqsCurso, w => this.SeqsCurso.Contains(w.CursoOfertaLocalidade.CursoUnidade.SeqCurso));
            AddExpression(SeqLocalidade, w => w.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.SeqEntidade == this.SeqLocalidade);
            AddExpression(Ativo, w => w.Ativo == this.Ativo);

            AddExpression(SeqEntidadeResponsavel, w => w.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.ItemSuperior.SeqEntidade == this.SeqEntidadeResponsavel);
            AddExpression(SeqsEntidadeResponsavel, w => SeqsEntidadeResponsavel.Contains(w.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.ItemSuperior.SeqEntidade));
            AddExpression(SeqFormacaoEspecifica, w => w.CursoOfertaLocalidade.CursoOferta.SeqFormacaoEspecifica == this.SeqFormacaoEspecifica);
            AddExpression(SeqNivelEnsino, w => w.CursoOfertaLocalidade.CursoOferta.Curso.SeqNivelEnsino == this.SeqNivelEnsino);

            /// Complementos das validações NV06
            /*NV06 Se o tipo de oferta exigir curso-oferta-localidade-turno, não reotrnar a oferta se:
             * O curso do curso-oferta-localidade-turno da oferta estiver com a categoria da situação
             * "Em desativação" ou· "Inativa" na data início do período letivo* do menor ciclo letivo da campanha informada por parâmetro.
             * O curso-unidade do curso-oferta-localidade-turno estiver com a categoria da situação "Em desativação" ou· "Inativa"
             * na data início do período letivo* do menor ciclo letivo da campanha informada por parâmetro.
             * O curso-oferta do curso-oferta-localidade-turno estiver desativado.
             * O curso-oferta-localidade do curso-oferta-localidade-turno estiver com a categoria
             * da situação “Em desativação”· ou “Inativa” na data início do período letivo* do menor
             * ciclo letivo da campanha informada por parâmetro.
             * A formação específica estiver desativada ou a formação específica por curso não
             * estiver mais vigente na data· início do período
             * letivo* do menor ciclo letivo da campanha informada por parâmetro.
             * O turno do curso-oferta-localidade-turno estiver desativado para o curso-oferta-localidade do·
             * curso-oferta-localidade-turno.
             *
             * Se o tipo de oferta não exigir curso-oferta-localidade-turno e for do tipo TURMA, não retornar a oferta se:
             *      A turma estiver cancelada.
             *      A turma tiver divisão de componente cujo tipo gere orientação*/
            if (DataInicioPeriodoLetivo.HasValue)
            {
                if (CursoAtivoDataInicioCicloLetivo.HasValue && CursoAtivoDataInicioCicloLetivo.Value)
                {
                    //CursoAtivoDataInicioCicloLetivo Apenas ativos
                    AddExpression(this.CursoAtivoDataInicioCicloLetivo, p => p.CursoOfertaLocalidade.CursoOferta.Curso
                                      .HistoricoSituacoes.Any(c => (c.DataInicio <= DataInicioPeriodoLetivo && (!c.DataFim.HasValue || c.DataFim > DataInicioPeriodoLetivo))
                                                     && c.SituacaoEntidade.CategoriaAtividade != Common.Areas.ORG.Enums.CategoriaAtividade.EmDesativacao
                                                     && c.SituacaoEntidade.CategoriaAtividade != Common.Areas.ORG.Enums.CategoriaAtividade.Inativa));
                }
                else
                {
                    //CursoAtivoDataInicioCicloLetivo Apenas EmDesativação ou Inativas
                    AddExpression(this.CursoAtivoDataInicioCicloLetivo, p => p.CursoOfertaLocalidade.CursoOferta.Curso
                                     .HistoricoSituacoes.Any(c => (c.DataInicio <= DataInicioPeriodoLetivo && (!c.DataFim.HasValue || c.DataFim > DataInicioPeriodoLetivo))
                                                    && c.SituacaoEntidade.CategoriaAtividade == Common.Areas.ORG.Enums.CategoriaAtividade.EmDesativacao
                                                    || c.SituacaoEntidade.CategoriaAtividade == Common.Areas.ORG.Enums.CategoriaAtividade.Inativa));
                }

                if (CursoUnidadeAtivoDataInicioCicloLetivo.HasValue && CursoUnidadeAtivoDataInicioCicloLetivo.Value)
                {
                    //CursoUnidadeAtivoDataInicioCicloLetivo Apenas ativos
                    AddExpression(this.CursoUnidadeAtivoDataInicioCicloLetivo, p => p.CursoOfertaLocalidade.CursoUnidade
                                  .HistoricoSituacoes.Any(c => (c.DataInicio <= DataInicioPeriodoLetivo && (!c.DataFim.HasValue || c.DataFim > DataInicioPeriodoLetivo))
                                                && c.SituacaoEntidade.CategoriaAtividade != Common.Areas.ORG.Enums.CategoriaAtividade.EmDesativacao
                                                && c.SituacaoEntidade.CategoriaAtividade != Common.Areas.ORG.Enums.CategoriaAtividade.Inativa));
                }
                else
                {
                    //CursoUnidadeAtivoDataInicioCicloLetivo Apenas EmDesativação ou Inativos
                    AddExpression(this.CursoUnidadeAtivoDataInicioCicloLetivo, p => p.CursoOfertaLocalidade.CursoUnidade
                                  .HistoricoSituacoes.Any(c => (c.DataInicio <= DataInicioPeriodoLetivo && (!c.DataFim.HasValue || c.DataFim > DataInicioPeriodoLetivo))
                                                && c.SituacaoEntidade.CategoriaAtividade == Common.Areas.ORG.Enums.CategoriaAtividade.EmDesativacao
                                                || c.SituacaoEntidade.CategoriaAtividade == Common.Areas.ORG.Enums.CategoriaAtividade.Inativa));
                }

                if (CursoOfertaLocalidadeAtivoDataInicioCicloLetivo.HasValue && CursoOfertaLocalidadeAtivoDataInicioCicloLetivo.Value)
                {
                    //CursoOfertaLocalidadeAtivoDataInicioCicloLetivo Apenas ativos
                    AddExpression(this.CursoOfertaLocalidadeAtivoDataInicioCicloLetivo, p => p.CursoOfertaLocalidade
                                  .HistoricoSituacoes.Any(c => (c.DataInicio <= DataInicioPeriodoLetivo && (!c.DataFim.HasValue || c.DataFim > DataInicioPeriodoLetivo))
                                               && c.SituacaoEntidade.CategoriaAtividade != Common.Areas.ORG.Enums.CategoriaAtividade.EmDesativacao
                                               && c.SituacaoEntidade.CategoriaAtividade != Common.Areas.ORG.Enums.CategoriaAtividade.Inativa));
                }
                else
                {
                    //CursoOfertaLocalidadeAtivoDataInicioCicloLetivo  Apenas EmDesativação ou Inativos
                    AddExpression(this.CursoOfertaLocalidadeAtivoDataInicioCicloLetivo, p => p.CursoOfertaLocalidade
                                  .HistoricoSituacoes.Any(c => (c.DataInicio <= DataInicioPeriodoLetivo && (!c.DataFim.HasValue || c.DataFim > DataInicioPeriodoLetivo))
                                               && c.SituacaoEntidade.CategoriaAtividade == Common.Areas.ORG.Enums.CategoriaAtividade.EmDesativacao
                                               || c.SituacaoEntidade.CategoriaAtividade == Common.Areas.ORG.Enums.CategoriaAtividade.Inativa));
                }

                if (this.FormacaoEspecificaAtivoDataInicioCicloLetivo.HasValue && this.FormacaoEspecificaAtivoDataInicioCicloLetivo.Value)
                {
                    // A formação específica estiver Ativada e a formação específica por curso estiver vigente na data início do período letivo
                    // Formações vigentes no curso na DataInicioPeriodoLetivo
                    AddExpression(this.FormacaoEspecificaAtivoDataInicioCicloLetivo, p =>
                                  p.CursoOfertaLocalidade.CursoOferta.Curso.CursosFormacaoEspecifica.Any(
                                      c => c.DataInicioVigencia <= DataInicioPeriodoLetivo
                                   && (!c.DataFimVigencia.HasValue || c.DataFimVigencia >= DataInicioPeriodoLetivo)
                                   && c.FormacaoEspecifica.Ativo.HasValue
                                   && c.FormacaoEspecifica.Ativo.Value));
                }
            }

            AddExpression(this.CursoOfertaAtivo, p => p.CursoOfertaLocalidade.CursoOferta.Ativo == this.CursoOfertaAtivo);
            AddExpression(this.TurnoAtivo, p => p.Ativo == TurnoAtivo);
            AddExpression(CursoOfertaLocalidadeAtivo, w => w
                .CursoOfertaLocalidade
                .HistoricoSituacoes
                .FirstOrDefault(f => f.DataInicio <= DateTime.Today && (!f.DataFim.HasValue || f.DataFim >= DateTime.Today))
                .SituacaoEntidade.CategoriaAtividade == CategoriaAtividade.Ativa);

            return GetExpression();
        }
    }
}