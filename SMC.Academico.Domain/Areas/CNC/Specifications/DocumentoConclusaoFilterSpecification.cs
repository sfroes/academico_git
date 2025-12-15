using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CNC.Specifications
{
    public class DocumentoConclusaoFilterSpecification : SMCSpecification<DocumentoConclusao>
    {
        public long? Seq { get; set; }

        public long? SeqDocumentoConclusaoDiferente { get; set; }

        public List<long> SeqsPessoasAtuacoes { get; set; }

        public List<long?> SeqsEntidadesResponsaveis { get; set; }

        public long? SeqCursoOfertaLocalidade { get; set; }

        public long? SeqCurso { get; set; }

        public long? SeqCursoDocumentoFormacao { get; set; }

        public long? SeqGrauAcademico { get; set; }

        public long? SeqPessoaAtuacao { get; set; }

        public long? SeqTipoDocumentoAcademico { get; set; }

        public List<string> TokensTipoDocumentoAcademico { get; set; }

        public long? SeqSituacaoDocumentoAcademico { get; set; }

        public long? SeqSolicitacaoServico { get; set; }

        public long? NumeroSerie { get; set; }

        public List<string> TokensSituacaoAtual { get; set; }

        public string TokenSituacaoAtual { get; set; }

        public List<long> SeqsFormacoesEspecificas { get; set; }

        public List<long> SeqsTiposFormacoesEspecificas { get; set; }

        public string DescricaoDocumento { get; set; }

        public long? SeqTitulacao { get; set; }

        public long? SeqDocumentoDiplomaGAD { get; set; }

        public ClasseSituacaoDocumento? ClasseSituacaoDocumento { get; set; }

        public List<ClasseSituacaoDocumento> ListaClasseSituacaoDocumento { get; set; }

        public long? SeqGrupoRegistro { get; set; }

        public string NumeroRegistro { get; set; }

        public string Nome { get; set; }

        public GrupoDocumentoAcademico? GrupoDocumentoAcademico { get; set; }

        public List<MotivoInvalidadeDocumento?> ListaMotivoInvalidadeDocumento { get; set; }

        public TipoInvalidade? TipoInvalidade { get; set; }

        public override Expression<Func<DocumentoConclusao, bool>> SatisfiedBy()
        {
            AddExpression(Seq, x => x.Seq == Seq);
            AddExpression(SeqPessoaAtuacao, x => x.SeqAtuacaoAluno == SeqPessoaAtuacao);
            AddExpression(SeqsPessoasAtuacoes, x => SeqsPessoasAtuacoes.Contains(x.SeqAtuacaoAluno));
            AddExpression(SeqsEntidadesResponsaveis, x => x.Aluno.Historicos.Any(w => w.Atual && SeqsEntidadesResponsaveis.Contains(w.SeqEntidadeVinculo)));
            AddExpression(SeqCursoOfertaLocalidade, x => x.Aluno.Historicos.Where(w => w.Atual).FirstOrDefault().CursoOfertaLocalidadeTurno.SeqCursoOfertaLocalidade == this.SeqCursoOfertaLocalidade);
            AddExpression(SeqTipoDocumentoAcademico, x => x.SeqTipoDocumentoAcademico == this.SeqTipoDocumentoAcademico);
            AddExpression(TokensTipoDocumentoAcademico, x => TokensTipoDocumentoAcademico.Contains(x.TipoDocumentoAcademico.Token));
            AddExpression(GrupoDocumentoAcademico, x => x.TipoDocumentoAcademico.GrupoDocumentoAcademico == GrupoDocumentoAcademico);
            AddExpression(SeqSituacaoDocumentoAcademico, x => x.SituacaoAtual.SeqSituacaoDocumentoAcademico == this.SeqSituacaoDocumentoAcademico);
            AddExpression(SeqSolicitacaoServico, x => x.SeqSolicitacaoServico == this.SeqSolicitacaoServico);
            AddExpression(NumeroSerie, x => x.NumeroSerie == this.NumeroSerie);
            AddExpression(SeqCurso, x => x.Aluno.Historicos.FirstOrDefault(h => h.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.SeqCurso == SeqCurso);
            AddExpression(SeqCursoDocumentoFormacao, x => x.FormacoesEspecificas.OrderBy(o => o.DataInclusao).FirstOrDefault().AlunoFormacao.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.SeqCurso == SeqCursoDocumentoFormacao);
            AddExpression(TokenSituacaoAtual, x => x.SituacaoAtual.SituacaoDocumentoAcademico.Token == TokenSituacaoAtual);
            AddExpression(TokensSituacaoAtual, x => TokensSituacaoAtual.Contains(x.SituacaoAtual.SituacaoDocumentoAcademico.Token));
            AddExpression(SeqsFormacoesEspecificas, x => x.FormacoesEspecificas.Any(f => SeqsFormacoesEspecificas.Contains(f.AlunoFormacao.SeqFormacaoEspecifica)));
            AddExpression(SeqsTiposFormacoesEspecificas, x => x.FormacoesEspecificas.Any(f => SeqsTiposFormacoesEspecificas.Contains(f.AlunoFormacao.FormacaoEspecifica.SeqTipoFormacaoEspecifica)));
            AddExpression(SeqDocumentoConclusaoDiferente, x => x.Seq != SeqDocumentoConclusaoDiferente);
            AddExpression(SeqGrauAcademico, x => x.FormacoesEspecificas.OrderBy(o => o.DataInclusao).FirstOrDefault().AlunoFormacao.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.CursosFormacaoEspecifica.FirstOrDefault(f => f.SeqFormacaoEspecifica == x.FormacoesEspecificas.OrderBy(o => o.DataInclusao).FirstOrDefault().AlunoFormacao.SeqFormacaoEspecifica).SeqGrauAcademico == SeqGrauAcademico);
            AddExpression(DescricaoDocumento, x => x.FormacoesEspecificas.OrderBy(o => o.DataInclusao).FirstOrDefault().AlunoFormacao.DescricaoDocumentoConclusao.Trim() == DescricaoDocumento.Trim());
            AddExpression(SeqTitulacao, x => x.FormacoesEspecificas.OrderBy(o => o.DataInclusao).FirstOrDefault().AlunoFormacao.SeqTitulacao == SeqTitulacao);
            AddExpression(SeqDocumentoDiplomaGAD, x => x.SeqDocumentoGAD == SeqDocumentoDiplomaGAD);
            AddExpression(ClasseSituacaoDocumento, x => x.SituacaoAtual.SituacaoDocumentoAcademico.ClasseSituacaoDocumento == ClasseSituacaoDocumento);
            AddExpression(ListaClasseSituacaoDocumento, x => ListaClasseSituacaoDocumento.Contains(x.SituacaoAtual.SituacaoDocumentoAcademico.ClasseSituacaoDocumento));
            AddExpression(ListaMotivoInvalidadeDocumento, x => ListaMotivoInvalidadeDocumento.Contains(x.SituacaoAtual.MotivoInvalidadeDocumento));
            AddExpression(SeqGrupoRegistro, x => x.SeqGrupoRegistro == SeqGrupoRegistro);
            AddExpression(NumeroRegistro, x => x.NumeroRegistro == NumeroRegistro);
            AddExpression(TipoInvalidade, x => x.SituacaoAtual.ClassificacaoInvalidadeDocumento.TipoInvalidade == TipoInvalidade);

            AddExpression(Nome, p => p.Aluno.DadosPessoais.Nome.Contains(Nome) || p.Aluno.DadosPessoais.NomeSocial.Contains(Nome));

            return GetExpression();
        }
    }
}