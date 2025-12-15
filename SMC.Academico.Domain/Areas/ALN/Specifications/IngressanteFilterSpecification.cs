using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Framework;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Permissions;

namespace SMC.Academico.Domain.Areas.ALN.Specifications
{
    public class IngressanteFilterSpecification : SMCSpecification<Ingressante>
    {
        public long? SeqPessoa { get; set; }

        public long? SeqUsuarioSAS { get; set; }

        public bool? ApenasAptosParaMatricula { get; set; }

        public long? Seq { get; set; }

        public string Nome { get; set; }

        public string Cpf { get; set; }

        public long? SeqCampanhaCicloLetivo { get; set; }

        public string NumeroPassaporte { get; set; }

        public long? SeqTipoVinculoAluno { get; set; }

        public long? SeqFormaIngresso { get; set; }

        public long? SeqConvocado { get; set; }

        public long? SeqCampanhaOferta { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public long[] SeqsConvocados { get; set; }

        public long? SeqChamada { get; set; }

        public long? SeqEntidadeResponsavel { get; set; }

        public long? SeqCampanha { get; set; }

        public long? SeqProcessoSeletivo { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public SituacaoIngressante? SituacaoIngressanteAtual { get; set; }

        public override Expression<Func<Ingressante, bool>> SatisfiedBy()
        {
            if (!string.IsNullOrEmpty(Cpf))
                Cpf = Cpf.SMCRemoveNonDigits(); 

            AddExpression(SeqPessoa, w => SeqPessoa.Value == w.SeqPessoa);
            AddExpression(ApenasAptosParaMatricula, w => w.HistoricosSituacao.OrderByDescending(h => h.Seq).FirstOrDefault().SituacaoIngressante == SituacaoIngressante.AptoMatricula || w.HistoricosSituacao.OrderByDescending(h => h.Seq).FirstOrDefault().SituacaoIngressante == SituacaoIngressante.Matriculado);
            AddExpression(Seq, w => Seq.Value == w.Seq);
            AddExpression(Nome, w => (w.DadosPessoais.Nome.Contains(Nome) || w.DadosPessoais.NomeSocial.Contains(Nome)));
            AddExpression(Cpf, w => w.Pessoa.Cpf == Cpf);
            AddExpression(SeqCampanhaCicloLetivo, w => SeqCampanhaCicloLetivo.Value == w.SeqCampanhaCicloLetivo);
            AddExpression(NumeroPassaporte, w => w.Pessoa.NumeroPassaporte.Contains(NumeroPassaporte));
            AddExpression(SeqTipoVinculoAluno, w => SeqTipoVinculoAluno.Value == w.SeqTipoVinculoAluno);
            AddExpression(SeqFormaIngresso, w => SeqFormaIngresso.Value == w.SeqFormaIngresso);
            AddExpression(SeqCampanhaOferta, w => w.Ofertas.Any(a => a.SeqCampanhaOferta == SeqCampanhaOferta));
            AddExpression(SeqCicloLetivo, w => w.CampanhaCicloLetivo.SeqCicloLetivo == SeqCicloLetivo);
            AddExpression(SeqsConvocados, w => SeqsConvocados.Contains(w.SeqConvocado.Value));
            AddExpression(SeqConvocado, w => w.SeqConvocado == SeqConvocado);
            AddExpression(SeqChamada, w => w.Convocado.SeqChamada == SeqChamada.Value);
            AddExpression(SeqEntidadeResponsavel, w => w.SeqEntidadeResponsavel == SeqEntidadeResponsavel);
            AddExpression(SeqCampanha, w => w.CampanhaCicloLetivo.SeqCampanha == SeqCampanha);
            AddExpression(SeqProcessoSeletivo, w => w.SeqProcessoSeletivo == SeqProcessoSeletivo);
            AddExpression(SeqNivelEnsino, w => w.SeqNivelEnsino == SeqNivelEnsino);
            AddExpression(SeqUsuarioSAS, w => w.Pessoa.SeqUsuarioSAS == SeqUsuarioSAS);
            AddExpression(SituacaoIngressanteAtual, w => w.HistoricosSituacao.OrderByDescending(h => h.Seq).FirstOrDefault().SituacaoIngressante == SituacaoIngressanteAtual);

            return GetExpression();
        }
    }
}