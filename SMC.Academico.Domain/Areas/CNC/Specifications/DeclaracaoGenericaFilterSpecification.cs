using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Framework.Specification;

namespace SMC.Academico.Domain.Areas.CNC.Specifications
{
    public class DeclaracaoGenericaFilterSpecification : SMCSpecification<DeclaracaoGenerica>
    {
        public long? Seq { get; set; }
        public long? SeqDocumentoGAD { get; set; }
        public List<long?> SeqsTiposDocumentos { get; set; }
        public long? SeqSituacaoAtual { get; set; }
        public long? SeqCursoOfertaLocalidadeParam { get; set; }
        public long? SeqPessoa { get; set; }
        public List<long?> SeqsEntidadesResponsaveis { get; set; }
        public DateTime? DataInicioInclusao { get; set; }
        public DateTime? DataFimInclusao { get; set; }
        public ClasseSituacaoDocumento? ClasseSituacaoDocumento { get; set; }
        public List<ClasseSituacaoDocumento> ListaClasseSituacaoDocumento { get; set; }

        public override Expression<Func<DeclaracaoGenerica, bool>> SatisfiedBy()
        {
            AddExpression(Seq, x => x.Seq == Seq);
            AddExpression(SeqDocumentoGAD, x => x.SeqDocumentoGAD == SeqDocumentoGAD);
            AddExpression(ClasseSituacaoDocumento, x => x.SituacaoAtual.SituacaoDocumentoAcademico.ClasseSituacaoDocumento == ClasseSituacaoDocumento);
            AddExpression(ListaClasseSituacaoDocumento, x => ListaClasseSituacaoDocumento.Contains(x.SituacaoAtual.SituacaoDocumentoAcademico.ClasseSituacaoDocumento));
            AddExpression(SeqsTiposDocumentos, x => SeqsTiposDocumentos.Contains(x.SeqTipoDocumentoAcademico));
            AddExpression(SeqSituacaoAtual, x => x.SituacaoAtual.SeqSituacaoDocumentoAcademico == SeqSituacaoAtual);
            AddExpression(SeqPessoa, x => (x.PessoaAtuacao.Pessoa.Seq == SeqPessoa));
            AddExpression(SeqsEntidadesResponsaveis, x => (x.PessoaAtuacao as Aluno).Historicos.Any(w => w.Atual && SeqsEntidadesResponsaveis.Contains(w.SeqEntidadeVinculo)));
            AddExpression(SeqCursoOfertaLocalidadeParam, x => (x.PessoaAtuacao as Aluno).Historicos.Where(w => w.Atual).FirstOrDefault().CursoOfertaLocalidadeTurno.SeqCursoOfertaLocalidade == this.SeqCursoOfertaLocalidadeParam);

            if (DataInicioInclusao.HasValue && DataFimInclusao.HasValue)
            {
                DateTime dateTimeInicio = DataInicioInclusao.Value.Date; 
                DateTime dateTimeFim = DataFimInclusao.Value.Date.AddDays(1).AddTicks(-1);
                AddExpression(x =>
                    x.DataInclusao >= dateTimeInicio &&
                    x.DataInclusao <= dateTimeFim
                );
            }

            return GetExpression();
        }
    }
}
