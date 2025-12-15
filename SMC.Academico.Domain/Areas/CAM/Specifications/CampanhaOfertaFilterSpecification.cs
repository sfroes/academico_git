using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CAM.Specifications
{
    public class CampanhaOfertaFilterSpecification : SMCSpecification<CampanhaOferta>
    {
        public long? Seq { get; set; }

        public long? SeqTipoOferta { get; set; }

        public long? SeqCiloLetivo { get; set; }

        public long? SeqEntidadeResponsavel { get; set; }

        public bool? Ativas { get; set; }

        public string Descricao { get; set; }

        public string DescricaoDuplicada { get; set; }

        public long[] Seqs { get; set; }

        public long? SeqCampanha { get; set; }

        public List<long> SeqNivelEnsino { get; set; }

        public bool? VinculoExigeCurso { get; set; }

        public string Oferta { get; set; }

        public string Turma { get; set; }

        public List<long> SeqsEntidadeResponsavel { get; set; }

        public long? SeqLocalidade { get; set; }

        public long? SeqCursoOferta { get; set; }

        public long? SeqTurno { get; set; }

        public long? SeqFormacaoEspecifica { get; set; }

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

        public long? SeqProcessoSeletivo { get; set; }

        public long? SeqCursoOfertaLocalidadeTurno { get; set; }

        public long? SeqTurma { get; set; }

        public long? SeqColaborador { get; set; }

        public override Expression<Func<CampanhaOferta, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p => p.Seq == this.Seq.Value);
            AddExpression(this.SeqTipoOferta, p => p.SeqTipoOferta == this.SeqTipoOferta.Value);
            AddExpression(this.Descricao, p => p.Descricao.Contains(this.Descricao));
            AddExpression(this.DescricaoDuplicada, p => p.Descricao.ToLower().Equals(this.DescricaoDuplicada.ToLower()));
            AddExpression(this.SeqCiloLetivo, p => p.Campanha.CiclosLetivos.Any(a => a.SeqCicloLetivo == this.SeqCiloLetivo.Value));
            AddExpression(this.SeqEntidadeResponsavel, p => p.Campanha.SeqEntidadeResponsavel == this.SeqEntidadeResponsavel.Value);
            AddExpression(this.Seqs, p => Seqs.Contains(p.Seq));
            AddExpression(this.SeqCampanha, p => p.SeqCampanha == this.SeqCampanha.Value);
            AddExpression(this.SeqNivelEnsino, p => p.Itens.Any(a => SeqNivelEnsino.Contains(a.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.SeqNivelEnsino)));
            AddExpression(this.VinculoExigeCurso, p => p.Itens.Any(a => a.SeqCursoOfertaLocalidadeTurno.HasValue == this.VinculoExigeCurso));
            AddExpression(this.Oferta, x => x.Descricao.ToLower().Contains(Oferta.ToLower()));
            AddExpression(this.Turma, x => x.Itens.FirstOrDefault().Turma.ConfiguracoesComponente.Any(t => t.Descricao.ToLower().Contains(Turma.ToLower())));
            AddExpression(this.SeqsEntidadeResponsavel, p => this.SeqsEntidadeResponsavel.Contains(p.Campanha.SeqEntidadeResponsavel));
            AddExpression(this.SeqLocalidade, p => p.Itens.FirstOrDefault().CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Seq == this.SeqLocalidade);
            AddExpression(this.SeqCursoOferta, p => p.Itens.FirstOrDefault().CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.SeqCursoOferta == this.SeqCursoOferta);
            AddExpression(this.SeqTurno, p => p.Itens.FirstOrDefault().CursoOfertaLocalidadeTurno.SeqTurno == this.SeqTurno);
            AddExpression(this.SeqFormacaoEspecifica, p => p.Itens.FirstOrDefault().CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.SeqFormacaoEspecifica == this.SeqFormacaoEspecifica);
            AddExpression(this.SeqProcessoSeletivo, p => p.Campanha.ProcessosSeletivos.Any(a => a.Seq == this.SeqProcessoSeletivo && a.Ofertas.Any(ao => ao.SeqCampanhaOferta == p.Seq)));

            /*Ao associar uma oferta, verificar se já existe uma oferta cadastrada, com um campanha-oferta-item, com o mesmo curso-oferta-localidade-turno, 
             * a mesma turma, o mesmo colaborador e a mesma formação específica. Considerar que os campos podem estar nulos.*/
            AddExpression(this.SeqCursoOfertaLocalidadeTurno, p => p.Itens.Any(a => a.SeqCursoOfertaLocalidadeTurno == this.SeqCursoOfertaLocalidadeTurno));
            AddExpression(this.SeqTurma, p => p.Itens.Any(a => a.SeqTurma == this.SeqTurma));
            AddExpression(this.SeqColaborador, p => p.Itens.Any(a => a.SeqColaborador == this.SeqColaborador));
       
            return GetExpression();
        }
    }
}