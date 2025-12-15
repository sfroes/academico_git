using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Framework;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ALN.Specifications
{
    public class TermoIntercambioFilterSpecification : SMCSpecification<TermoIntercambio>
    {
        public long? Seq { get; set; }
        public long[] SeqsTermosIntercambio { get; set; }

        public long? SeqParceriaIntercambio { get; set; }

        public string Descricao { get; set; }

        public long? SeqTipoTermoIntercambio { get; set; }

        public long? SeqParceriaIntercambioTipoTermo { get; set; }

        public long? SeqParceriaIntercambioInstituicaoExterna { get; set; }

        public bool? Ativo { get; set; }

        public string DescricaoParceria { get; set; }

        public TipoParceriaIntercambio? TipoParceriaIntercambio { get; set; }

        public long? SeqInstituicaoExterna { get; set; }

        public string Cpf { get; set; }

        public string NumeroPassaporte { get; set; }

        public TipoMobilidade? TipoMobilidade { get; set; }

        public long[] SeqsTiposTermoIntercambio { get; set; }

        public long? SeqPessoaAtuacao { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public override Expression<Func<TermoIntercambio, bool>> SatisfiedBy()
        {
            if (!string.IsNullOrEmpty(Cpf))
            {
                Cpf = Cpf.SMCRemoveNonDigits();
            }

            AddExpression(Seq, w => w.Seq == this.Seq.Value);
            AddExpression(SeqPessoaAtuacao, w => w.PessoasAtuacao.Any(a => a.SeqPessoaAtuacao == SeqPessoaAtuacao.Value));
            AddExpression(Descricao, w => w.Descricao.Contains(Descricao));
            AddExpression(SeqTipoTermoIntercambio, w => w.ParceriaIntercambioTipoTermo.SeqTipoTermoIntercambio == SeqTipoTermoIntercambio);
            AddExpression(SeqParceriaIntercambioInstituicaoExterna, w => w.SeqParceriaIntercambioInstituicaoExterna == SeqParceriaIntercambioInstituicaoExterna);
            AddExpression(SeqParceriaIntercambio, w => w.ParceriaIntercambioInstituicaoExterna.SeqParceriaIntercambio == SeqParceriaIntercambio);
            AddExpression(DescricaoParceria, w => w.ParceriaIntercambioInstituicaoExterna.ParceriaIntercambio.Descricao.Contains(DescricaoParceria));
            AddExpression(TipoParceriaIntercambio, w => w.ParceriaIntercambioInstituicaoExterna.ParceriaIntercambio.TipoParceriaIntercambio == TipoParceriaIntercambio);
            AddExpression(SeqInstituicaoExterna, w => w.ParceriaIntercambioInstituicaoExterna.SeqInstituicaoExterna == SeqInstituicaoExterna.Value);
            AddExpression(this.SeqParceriaIntercambioTipoTermo, a => a.SeqParceriaIntercambioTipoTermo == this.SeqParceriaIntercambioTipoTermo);
            AddExpression(SeqNivelEnsino, w => w.SeqNivelEnsino == SeqNivelEnsino.Value);
            if (TipoMobilidade.HasValue)
            {
                // Caso tenha tipo de mobilidade e seja para filtrar por documento,
                // Recupera todos os termos que tenham o documento informado ou tenham vagas e não tenham nenhum documento informado.
                // São consideradas como vagas ocupadas os relacionamentos ativos dos termos de intercâmbio com as pessoas atuação.
                AddExpression(Cpf, w =>
                    w.TiposMobilidade.FirstOrDefault(t => t.TipoMobilidade == TipoMobilidade).Pessoas.Any(p => p.Cpf == Cpf) ||
                    w.PessoasAtuacao.Count(c => c.Ativo && c.PessoaAtuacao.Pessoa.Cpf != Cpf) < w.TiposMobilidade.FirstOrDefault(t => t.TipoMobilidade == TipoMobilidade && !t.Pessoas.Any()).QuantidadeVagas);
                AddExpression(NumeroPassaporte, w =>
                    w.TiposMobilidade.FirstOrDefault(t => t.TipoMobilidade == TipoMobilidade).Pessoas.Any(p => p.Passaporte == NumeroPassaporte) ||
                    w.PessoasAtuacao.Count(c => c.Ativo && c.PessoaAtuacao.Pessoa.NumeroPassaporte != NumeroPassaporte) < w.TiposMobilidade.FirstOrDefault(t => t.TipoMobilidade == TipoMobilidade && !t.Pessoas.Any()).QuantidadeVagas);
            }
            else
            {
                // Caso contrário, considera os documentos independente do tipo de mobilidade
                AddExpression(Cpf, w =>
                    w.TiposMobilidade.Any(a => a.Pessoas.Any(p => p.Cpf == Cpf)));
                AddExpression(NumeroPassaporte, w =>
                    w.TiposMobilidade.Any(a => a.Pessoas.Any(p => p.Passaporte == NumeroPassaporte)));
            }
            AddExpression(TipoMobilidade, w => w.TiposMobilidade.Count(c => c.TipoMobilidade == TipoMobilidade.Value) > 0);
            AddExpression(SeqsTiposTermoIntercambio, w => SeqsTiposTermoIntercambio.Contains(w.ParceriaIntercambioTipoTermo.SeqTipoTermoIntercambio));
            AddExpression(SeqsTermosIntercambio, w => SeqsTermosIntercambio.Contains(w.Seq));

            return GetExpression();
        }
    }
}