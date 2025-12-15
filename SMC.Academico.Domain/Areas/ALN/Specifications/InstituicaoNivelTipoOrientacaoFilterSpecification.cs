using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ALN.Specifications
{
    public class InstituicaoNivelTipoOrientacaoFilterSpecification : SMCSpecification<InstituicaoNivelTipoOrientacao>
    {
        public long? SeqInstituicaoEnsino { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public long? SeqInstituicaoNivelTipoVinculoAluno { get; set; }

        public long? SeqTipoVinculoAluno { get; set; }

        public long? SeqTipoOrientacao { get; set; }

        public long? SeqTipoIntercambio { get; set; }

        public bool? PossuiTipoIntercambio { get; set; }

        public long[] SeqsTipoTermoIntercambio { get; set; }

        public long? SeqInstituicaoNivelTipoTermoIntercambio { get; set; }

        public CadastroOrientacao? CadastroOrientacaoIngressante { get; set; }

        public CadastroOrientacao[] CadastroOrientacoesIngressante { get; set; }

        public CadastroOrientacao? CadastroOrientacaoAluno { get; set; }

        public CadastroOrientacao[] CadastroOrientacoesAluno { get; set; }

        public bool? PermiteManutencaoManual { get; set; }

        /// <summary>
        /// Trazer aqueles aqueles tipos de orientação exeto as que tenham intercambio
        /// </summary>
        public bool? ExcetoParceriaIntercambio { get; set; }

        public override Expression<Func<InstituicaoNivelTipoOrientacao, bool>> SatisfiedBy()
        {
            AddExpression(SeqNivelEnsino, w => w.InstituicaoNivelTipoVinculoAluno.InstituicaoNivel.SeqNivelEnsino == SeqNivelEnsino);
            AddExpression(SeqInstituicaoEnsino, w => w.InstituicaoNivelTipoVinculoAluno.InstituicaoNivel.SeqInstituicaoEnsino == SeqInstituicaoEnsino);
            AddExpression(SeqInstituicaoNivelTipoVinculoAluno, w => w.SeqInstituicaoNivelTipoVinculoAluno == SeqInstituicaoNivelTipoVinculoAluno);
            AddExpression(SeqTipoVinculoAluno, w => w.InstituicaoNivelTipoVinculoAluno.SeqTipoVinculoAluno == SeqTipoVinculoAluno);
            AddExpression(SeqTipoIntercambio, w => w.InstituicaoNivelTipoTermoIntercambio.SeqTipoTermoIntercambio == this.SeqTipoIntercambio);
            AddExpression(PossuiTipoIntercambio, w => w.SeqInstituicaoNivelTipoTermoIntercambio.HasValue == PossuiTipoIntercambio);
            AddExpression(this.SeqInstituicaoNivelTipoTermoIntercambio, w => w.SeqInstituicaoNivelTipoTermoIntercambio == this.SeqInstituicaoNivelTipoTermoIntercambio);
            AddExpression(SeqsTipoTermoIntercambio, w => SeqsTipoTermoIntercambio.Contains(w.InstituicaoNivelTipoTermoIntercambio.SeqTipoTermoIntercambio));
            AddExpression(SeqTipoOrientacao, w => w.SeqTipoOrientacao == SeqTipoOrientacao);
            AddExpression(PermiteManutencaoManual, w => w.TipoOrientacao.PermiteManutencaoManual == PermiteManutencaoManual);
            AddExpression(this.ExcetoParceriaIntercambio, w => w.InstituicaoNivelTipoTermoIntercambio == null);

            AddExpression(CadastroOrientacaoIngressante, w => w.CadastroOrientacaoIngressante == this.CadastroOrientacaoIngressante);
            AddExpression(CadastroOrientacoesIngressante, w => CadastroOrientacoesIngressante.Contains(w.CadastroOrientacaoIngressante));

            AddExpression(CadastroOrientacaoAluno, w => w.CadastroOrientacaoAluno == this.CadastroOrientacaoAluno);
            AddExpression(CadastroOrientacoesAluno, w => CadastroOrientacoesAluno.Contains(w.CadastroOrientacaoAluno));

            return GetExpression();
        }
    }
}