using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.Domain.Areas.DCT.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.DCT.Specifications
{
    public class ColaboradorVinculoFilterSpecification : SMCSpecification<ColaboradorVinculo>
    {
        public long? Seq { get; set; }

        public long[] Seqs { get; set; }

        public long? SeqEntidadeVinculo { get; set; }

        public long? SeqTipoVinculoColaborador { get; set; }

        public DateTime? DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public long? SeqCursoOfertaLocalidade { get; set; }

        public TipoAtividadeColaborador? TipoAtividade { get; set; }

        public long? SeqColaborador { get; set; }

        public long[] SeqsColaboradorVinculoCurso { get; set; }

        public string TokenEntidadeVinculo { get; set; }

        public List<string> TokensEntidadeVinculo { get; set; }

        public long[] SeqsTiposEntidadesVinculo { get; set; }

        public bool? VinculoAtivo { get; set; }

        public bool? PermiteInclusaoManualVinculo { get; set; }

        public bool? CriaVinculoInstitucional { get; set; }

		public long[] SeqsEntidadesVinculo { get; set; }

        public bool? ExigeFormacao { get; set; }

        public string TipoEntidadeToken { get; set; }

        public string[] TiposEntidadesTokens { get; set; }

        public string TokenTipoVinculoColaborador { get; set; }
        public long[] SeqsEntidadesResponsaveis { get; set; }

        public override Expression<Func<ColaboradorVinculo, bool>> SatisfiedBy()
        {
            var dataAtual = DateTime.Today;
                        
            if (DataInicio.HasValue ^ DataFim.HasValue)
                throw new ArgumentNullException("Ao informar o intervalo ativo as duas datas são obrigatórias");

            var dataInicio = DataInicio.GetValueOrDefault().Date;
            var dataFim = DataFim.GetValueOrDefault().Date;

            AddExpression(Seq, a => a.Seq == Seq);
            AddExpression(Seqs, a => Seqs.Contains(a.Seq));
			AddExpression(SeqEntidadeVinculo, a => a.SeqEntidadeVinculo == SeqEntidadeVinculo);

			if (SeqsEntidadesVinculo != null)
				AddExpression(a => SeqsEntidadesVinculo.Contains(a.SeqEntidadeVinculo));

			AddExpression(SeqTipoVinculoColaborador, a => a.SeqTipoVinculoColaborador == SeqTipoVinculoColaborador);
            AddExpression(DataInicio, a =>
                (a.DataInicio >= dataInicio && a.DataInicio <= dataFim) ||
                (a.DataInicio < dataInicio && (!a.DataFim.HasValue || a.DataFim >= dataInicio)));
            AddExpression(SeqCursoOfertaLocalidade, a => a.Cursos.Any(ac => ac.SeqCursoOfertaLocalidade == SeqCursoOfertaLocalidade));
            AddExpression(TipoAtividade, a => a.Cursos.Any(ac => ac.Atividades.Any(at => at.TipoAtividadeColaborador == TipoAtividade)));
            AddExpression(SeqColaborador, a => a.SeqColaborador == SeqColaborador);
            // Considera também uma lista vazia
            if (SeqsColaboradorVinculoCurso != null)
                AddExpression(a => a.Cursos.Any(ac => SeqsColaboradorVinculoCurso.Contains(ac.Seq)));
            AddExpression(TokenEntidadeVinculo, a => a.EntidadeVinculo.TipoEntidade.Token == TokenEntidadeVinculo);
            AddExpression(TokensEntidadeVinculo, a => TokensEntidadeVinculo.Contains(a.EntidadeVinculo.TipoEntidade.Token));
            AddExpression(SeqsTiposEntidadesVinculo, a => SeqsTiposEntidadesVinculo.Contains(a.EntidadeVinculo.SeqTipoEntidade));

            if (VinculoAtivo.HasValue)
            {
                if (VinculoAtivo.Value)
                {
                    AddExpression(a => a.DataInicio <= dataAtual && (!a.DataFim.HasValue || a.DataFim >= dataAtual));
                }
                else
                {
                    AddExpression(a => a.DataInicio < dataAtual && a.DataFim.HasValue && a.DataFim < dataAtual);
                }
            }
            
            AddExpression(PermiteInclusaoManualVinculo, a => a.TipoVinculoColaborador.PermiteInclusaoManualVinculo == PermiteInclusaoManualVinculo);
            AddExpression(CriaVinculoInstitucional, a => a.TipoVinculoColaborador.CriaVinculoInstitucional == CriaVinculoInstitucional);
            AddExpression(ExigeFormacao, a => a.TipoVinculoColaborador.ExigeFormacaoAcademica == ExigeFormacao);
            AddExpression(TipoEntidadeToken, x => x.EntidadeVinculo.TipoEntidade.Token == TipoEntidadeToken);
            AddExpression(TiposEntidadesTokens, x => TiposEntidadesTokens.Contains(x.EntidadeVinculo.TipoEntidade.Token));
            AddExpression(TokenTipoVinculoColaborador, x => x.TipoVinculoColaborador.Token == TokenTipoVinculoColaborador);

            if (SeqsEntidadesResponsaveis != null && SeqTipoVinculoColaborador.HasValue)
                AddExpression(SeqsEntidadesResponsaveis, w => this.SeqsEntidadesResponsaveis.Contains(w.SeqEntidadeVinculo) && w.SeqTipoVinculoColaborador == this.SeqTipoVinculoColaborador);

         

            return GetExpression();
        }
    }
}