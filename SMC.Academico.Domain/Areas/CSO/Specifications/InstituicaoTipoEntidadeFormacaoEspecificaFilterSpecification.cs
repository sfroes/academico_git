using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CSO.Specifications
{
    public class InstituicaoTipoEntidadeFormacaoEspecificaFilterSpecification : SMCSpecification<InstituicaoTipoEntidadeFormacaoEspecifica>
    {
        public long? SeqTipoEntidade { get; set; }

        public long? SeqTipoFormacaoEspecificaPai { get; set; }

        public bool? Ativo { get; set; }

        public bool? ObrigatorioAssociacaoIngressante { get; set; }

        public long? SeqInstituicao { get; set; }

        public string TokenEntidade { get; set; }

        public bool? ObrigatorioAssociacaoAluno { get; set; }

        /// <summary>
        /// Quando setado considera o SeqTipoFormacaoEspecificaPai para retornar apenas os filhos do seq informado
        /// ou itens de raiz quando o seq for nulo
        /// </summary>
        public bool ApenasFilhos { get; set; }

        public override Expression<Func<InstituicaoTipoEntidadeFormacaoEspecifica, bool>> SatisfiedBy()
        {
            AddExpression(SeqTipoEntidade, p => p.InstituicaoTipoEntidade.SeqTipoEntidade == SeqTipoEntidade);
            if (ApenasFilhos)
            {
                AddExpression(() => !SeqTipoFormacaoEspecificaPai.HasValue, p => !p.SeqPai.HasValue);
                AddExpression(SeqTipoFormacaoEspecificaPai, p => p.TipoFormacaoEspecificaPai.SeqTipoFormacaoEspecifica == SeqTipoFormacaoEspecificaPai);
            }
            AddExpression(Ativo, p => p.TipoFormacaoEspecifica.Ativo == Ativo);
            AddExpression(ObrigatorioAssociacaoIngressante, p => p.ObrigatorioAssociacaoIngressante == ObrigatorioAssociacaoIngressante);
            AddExpression(SeqInstituicao, p => p.InstituicaoTipoEntidade.SeqInstituicaoEnsino == SeqInstituicao);
            AddExpression(TokenEntidade, p => p.InstituicaoTipoEntidade.TipoEntidade.Token == TokenEntidade);
            AddExpression(ObrigatorioAssociacaoAluno, p => p.ObrigatorioAssociacaoAluno == ObrigatorioAssociacaoAluno);

            return GetExpression();

            //return p => (!SeqTipoEntidade.HasValue || p.InstituicaoTipoEntidade.SeqTipoEntidade == this.SeqTipoEntidade) &&
            //            (!ApenasFilhos ||
            //                !this.SeqTipoFormacaoEspecificaPai.HasValue && !p.SeqPai.HasValue ||
            //                p.TipoFormacaoEspecificaPai.SeqTipoFormacaoEspecifica == this.SeqTipoFormacaoEspecificaPai) &&
            //            (!Ativo.HasValue || p.TipoFormacaoEspecifica.Ativo == this.Ativo) &&
            //            (!ObrigatorioAssociacaoIngressante.HasValue || p.ObrigatorioAssociacaoAluno == ObrigatorioAssociacaoIngressante.Value) &&
            //            (!SeqInstituicao.HasValue || p.InstituicaoTipoEntidade.SeqInstituicaoEnsino == this.SeqInstituicao) &&
            //            (string.IsNullOrEmpty(TokenEntidade) || p.InstituicaoTipoEntidade.TipoEntidade.Token.Equals(TokenEntidade));
        }
    }
}