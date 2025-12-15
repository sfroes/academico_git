using SMC.Academico.Common.Areas.CNC.Constants;
using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Common.Areas.CNC.Exceptions;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Academico.Domain.Areas.CNC.Specifications;
using SMC.Academico.Domain.Areas.CNC.ValueObjects;
using SMC.Academico.ReportHost.Areas.ALN.Models;
using SMC.AssinaturaDigital.Common.Areas.DOC.Enums;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CNC.DomainServices
{
    public class DeclaracaoGenericaDomainService : AcademicoContextDomain<DeclaracaoGenerica>
    {
        #region DomainServices

        private SituacaoDocumentoAcademicoDomainService SituacaoDocumentoAcademicoDomainService => Create<SituacaoDocumentoAcademicoDomainService>();
        private DocumentoAcademicoHistoricoSituacaoDomainService DocumentoAcademicoHistoricoSituacaoDomainService => Create<DocumentoAcademicoHistoricoSituacaoDomainService>();

        #endregion DomainServices

        public long SalvarDeclaracaoGenerica(DeclaracaoGenericaVO modelo)
        {
            var dominio = modelo.Transform<DeclaracaoGenerica>();
            this.SaveEntity(dominio);

            return dominio.Seq;
        }

        public void AtualizarSituacaoDocumento(long seqDocumentoGad, StatusDocumento status)
        {
            var spec = new DeclaracaoGenericaFilterSpecification
            {
                SeqDocumentoGAD = seqDocumentoGad,
                ClasseSituacaoDocumento = ClasseSituacaoDocumento.EmissaoEmAndamento
            };

            var declaracaoGenerica = this.SearchProjectionBySpecification(spec, s => new
            {
                s.Seq,
                s.NumeroViaDocumento,
                s.TipoDocumentoAcademico.Token
            }).FirstOrDefault();

            if (declaracaoGenerica != null)
            {
                long seqSituacaoDocumentoAcademico = 0;
                switch (status)
                {
                    case StatusDocumento.Cancelado:
                        var specSituacaoCancelado = new SituacaoDocumentoAcademicoFilterSpecification() { Token = TOKEN_SITUACAO_DOCUMENTO_ACADEMICO.CANCELADO };
                        seqSituacaoDocumentoAcademico = SituacaoDocumentoAcademicoDomainService.SearchProjectionBySpecification(specSituacaoCancelado, s => s.Seq).FirstOrDefault();

                        if (seqSituacaoDocumentoAcademico == 0)
                            throw new TokenSituacaoDocumentoAcademicoNaoEncontradoException(TOKEN_SITUACAO_DOCUMENTO_ACADEMICO.CANCELADO);
                        break;
                    case StatusDocumento.Concluido:
                        var specSituacaoConcluido = new SituacaoDocumentoAcademicoFilterSpecification() { Token = TOKEN_SITUACAO_DOCUMENTO_ACADEMICO.ASSINADO };
                        seqSituacaoDocumentoAcademico = SituacaoDocumentoAcademicoDomainService.SearchProjectionBySpecification(specSituacaoConcluido, s => s.Seq).FirstOrDefault();

                        if (seqSituacaoDocumentoAcademico == 0)
                            throw new TokenSituacaoDocumentoAcademicoNaoEncontradoException(TOKEN_SITUACAO_DOCUMENTO_ACADEMICO.ASSINADO);
                        break;
                    case StatusDocumento.Recusado:
                        var specSituacaoRecusado = new SituacaoDocumentoAcademicoFilterSpecification() { Token = TOKEN_SITUACAO_DOCUMENTO_ACADEMICO.RECUSADO };
                        seqSituacaoDocumentoAcademico = SituacaoDocumentoAcademicoDomainService.SearchProjectionBySpecification(specSituacaoRecusado, s => s.Seq).FirstOrDefault();

                        if (seqSituacaoDocumentoAcademico == 0)
                            throw new TokenSituacaoDocumentoAcademicoNaoEncontradoException(TOKEN_SITUACAO_DOCUMENTO_ACADEMICO.RECUSADO);
                        break;
                }

                using (var transacao = SMCUnitOfWork.Begin())
                {
                    var documentoAcademicoHistoricoSituacao = new DocumentoAcademicoHistoricoSituacao()
                    {
                        SeqDocumentoAcademico = declaracaoGenerica.Seq,
                        SeqSituacaoDocumentoAcademico = seqSituacaoDocumentoAcademico
                    };

                    DocumentoAcademicoHistoricoSituacaoDomainService.SaveEntity(documentoAcademicoHistoricoSituacao);

                    transacao.Commit();
                }
            }
        }

        public SMCPagerData<DeclaracaoGenericaListarVO> BuscarDeclaracoesGenericas(DeclaracaoGenericaFiltroVO filtro)
        {
            var spec = filtro.Transform<DeclaracaoGenericaFilterSpecification>();

            if (!spec.OrderByClauses.SMCAny())
            {
                spec.SetOrderByDescending(dg => dg.DataInclusao);
                spec.SetOrderBy(dg => dg.PessoaDadosPessoais.Nome);
            }

            var declaracoesGenericas = SearchProjectionBySpecification(spec, d => new DeclaracaoGenericaListarVO
            {
                Seq = d.Seq,
                SituacaoAtual = d.SituacaoAtual != null ?
                                    d.SituacaoAtual.SituacaoDocumentoAcademico.Descricao : string.Empty,
                TipoDocumento = d.TipoDocumentoAcademico.Descricao,
                NumeroRegistroAcademico = d.SeqPessoaAtuacao,
                DataEmissao = d.DataInclusao,
                NomeAluno = (d.PessoaAtuacao as Aluno).DadosPessoais.Nome,
                NomeSocialAluno = (d.PessoaAtuacao as Aluno).DadosPessoais.NomeSocial,
                DescricaoCursoOfertaLocalidade = (d.PessoaAtuacao as Aluno)
                                                    .Historicos
                                                    .Where(h => h.Atual)
                                                    .FirstOrDefault().CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Nome,
                CodigoAlunoMigracao = (d.PessoaAtuacao as Aluno).CodigoAlunoMigracao

            }, out int total).ToList();


            return new SMCPagerData<DeclaracaoGenericaListarVO>(declaracoesGenericas, total);
        }

        public DeclaracaoGenericaDadosGeraisVO BuscarDeclaracaoGenerica(long seqDocumento)
        {
            var declaracaoGenerica = SearchProjectionByKey(new SMCSeqSpecification<DeclaracaoGenerica>(seqDocumento),
                                                                d => new DeclaracaoGenericaDadosGeraisVO()
                                                                {
                                                                    //Aluno
                                                                    NumeroRegistroAcademico = d.SeqPessoaAtuacao,
                                                                    CodigoAlunoMigracao = (d.PessoaAtuacao as Aluno).CodigoAlunoMigracao,
                                                                    Nome = (d.PessoaAtuacao as Aluno).DadosPessoais.Nome,                                                                    
                                                                    NomeSocial = (d.PessoaAtuacao as Aluno).DadosPessoais.NomeSocial,
                                                                    Cpf = d.PessoaAtuacao.Pessoa.Cpf,
                                                                    NumeroPassaporte = d.PessoaAtuacao.Pessoa.NumeroPassaporte,
                                                                    SeqPessoaDadosPessoais = d.PessoaAtuacao.DadosPessoais.Seq,
                                                                    NivelEnsino = (d.PessoaAtuacao as Aluno)
                                                                                  .Historicos
                                                                                  .Where(h => h.Atual)
                                                                                  .FirstOrDefault()
                                                                                  .CursoOfertaLocalidadeTurno
                                                                                  .CursoOfertaLocalidade
                                                                                  .CursoOferta
                                                                                  .Curso
                                                                                  .NivelEnsino.Descricao,
                                                                    Vinculo = d.PessoaAtuacao.Descricao,

                                                                    //Documento
                                                                    Seq = seqDocumento,
                                                                    TipoDocumento = d.TipoDocumentoAcademico.Descricao,
                                                                    SeqDocumentoGAD = d.SeqDocumentoGAD,
                                                                    TokenTipoDocumento = d.TipoDocumentoAcademico.Token,

                                                                    //Situações
                                                                    Situacoes = d.Situacoes.OrderBy(o => o.DataInclusao).Select(x => new DeclaracaoGenericaHistoricoListarVO()
                                                                    {   
                                                                        SeqDocumento = x.SeqDocumentoAcademico,                                                                        
                                                                        DescSituacao = x.SituacaoDocumentoAcademico.Descricao,                                                                        
                                                                        DataEmissao = x.DataInclusao,
                                                                        UsuarioResponsavel = x.UsuarioInclusao,
                                                                        Observacoes = x.Observacao                                                                        
                                                                    }).ToList()


                                                                });

            return declaracaoGenerica;
        }
    }
}
