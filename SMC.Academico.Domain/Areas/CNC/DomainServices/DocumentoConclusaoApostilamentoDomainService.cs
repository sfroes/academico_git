using SMC.Academico.Common.Areas.CNC.Constants;
using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Common.Areas.CNC.Exceptions;
using SMC.Academico.Common.Areas.Shared.Constants;
using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Academico.Domain.Areas.CNC.Specifications;
using SMC.Academico.Domain.Areas.CNC.ValueObjects;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CNC.DomainServices
{
    public class DocumentoConclusaoApostilamentoDomainService : AcademicoContextDomain<DocumentoConclusaoApostilamento>
    {
        #region DomainServices

        private DocumentoConclusaoDomainService DocumentoConclusaoDomainService
        {
            get { return this.Create<DocumentoConclusaoDomainService>(); }
        }

        private DocumentoAcademicoHistoricoSituacaoDomainService DocumentoAcademicoHistoricoSituacaoDomainService
        {
            get { return this.Create<DocumentoAcademicoHistoricoSituacaoDomainService>(); }
        }

        private TipoApostilamentoDomainService TipoApostilamentoDomainService
        {
            get { return this.Create<TipoApostilamentoDomainService>(); }
        }

        private DocumentoConclusaoFormacaoDomainService DocumentoConclusaoFormacaoDomainService
        {
            get { return this.Create<DocumentoConclusaoFormacaoDomainService>(); }
        }

        private FormacaoEspecificaDomainService FormacaoEspecificaDomainService
        {
            get { return this.Create<FormacaoEspecificaDomainService>(); }
        }

        #endregion DomainServices

        public DocumentoConclusaoApostilamentoVO BuscarDocumentoConclusaoApostilamento(long seq)
        {
            var documentoConclusaoApostilamento = this.SearchByKey(new SMCSeqSpecification<DocumentoConclusaoApostilamento>(seq), x => x.ArquivoAnexado);

            var retorno = documentoConclusaoApostilamento.Transform<DocumentoConclusaoApostilamentoVO>();

            if (documentoConclusaoApostilamento.ArquivoAnexado != null)
                retorno.ArquivoAnexado.GuidFile = documentoConclusaoApostilamento.ArquivoAnexado.UidArquivo.ToString();

            var documentoConclusao = this.DocumentoConclusaoDomainService.SearchByKey(new SMCSeqSpecification<DocumentoConclusao>(retorno.SeqDocumentoConclusao));

            if (documentoConclusao.SeqDocumentoAcademicoHistoricoSituacaoAtual.HasValue)
            {
                var classeSituacaoDocumentoAcademicoAtual = this.DocumentoAcademicoHistoricoSituacaoDomainService.SearchProjectionByKey(new SMCSeqSpecification<DocumentoAcademicoHistoricoSituacao>(documentoConclusao.SeqDocumentoAcademicoHistoricoSituacaoAtual.Value), x => x.SituacaoDocumentoAcademico.ClasseSituacaoDocumento);
                retorno.CamposReadyOnly = classeSituacaoDocumentoAcademicoAtual != ClasseSituacaoDocumento.Valido;
            }

            var tipoApostilamento = this.TipoApostilamentoDomainService.SearchByKey(new SMCSeqSpecification<TipoApostilamento>(retorno.SeqTipoApostilamento));

            if (tipoApostilamento.Token == TOKEN_TIPO_APOSTILAMENTO.NOVA_FORMACAO_ALUNO)
            {
                var documentoConclusaoFormacao = this.DocumentoConclusaoFormacaoDomainService.SearchBySpecification(new DocumentoConclusaoFormacaoFilterSpecification() { SeqDocumentoConclusao = retorno.SeqDocumentoConclusao, SeqDocumentoConclusaoApostilamento = seq }, x => x.AlunoFormacao.FormacaoEspecifica, x => x.AlunoFormacao.AlunoHistorico.Aluno).FirstOrDefault();

                if (documentoConclusaoFormacao != null)
                {
                    retorno.SeqAlunoFormacao = documentoConclusaoFormacao.SeqAlunoFormacao;

                    var descricoesFormacoesEspecificas = FormacaoEspecificaDomainService.BuscarDescricaoFormacaoEspecifica(documentoConclusaoFormacao.AlunoFormacao.SeqFormacaoEspecifica, documentoConclusaoFormacao.AlunoFormacao.FormacaoEspecifica.SeqEntidadeResponsavel).Select(x => x.DescricaoFormacaoEspecifica).ToList();

                    if (descricoesFormacoesEspecificas != null && descricoesFormacoesEspecificas.Any())
                    {
                        retorno.SeqAlunoFormacaoDescription = string.Join(Environment.NewLine, descricoesFormacoesEspecificas);
                    }
                    else
                    {
                        var descricaoFormacaoEspecifica = FormacaoEspecificaDomainService.BuscarFormacoesEspecificasHierarquia(new long[] { documentoConclusaoFormacao.AlunoFormacao.SeqFormacaoEspecifica });
                        var hierarquiasFormacao = descricaoFormacaoEspecifica.SelectMany(a => a.Hierarquia).ToList();
                        retorno.SeqAlunoFormacaoDescription = string.Join(Environment.NewLine, hierarquiasFormacao.Select(a => $"[{a.DescricaoTipoFormacaoEspecifica}] {a.Descricao}").ToList());
                    }
                }
            }

            return retorno;
        }

        public SMCPagerData<DocumentoConclusaoApostilamentoListarVO> BuscarDocumentosConclusaoApostilamento(DocumentoConclusaoApostilamentoFiltroVO filtros)
        {
            var spec = filtros.Transform<DocumentoConclusaoApostilamentoFilterSpecification>();

            var lista = this.SearchProjectionBySpecification(spec, x => new DocumentoConclusaoApostilamentoListarVO()
            {
                Seq = x.Seq,
                SeqDocumentoConclusao = x.SeqDocumentoConclusao,
                DescricaoTipoApostilamento = x.TipoApostilamento.Descricao,
                Descricao = x.Descricao,
                DataInclusao = x.DataInclusao

            }, out int total).ToList();

            return new SMCPagerData<DocumentoConclusaoApostilamentoListarVO>(lista, total);
        }

        public long SalvarDocumentoConclusaoApostilamento(DocumentoConclusaoApostilamentoVO modelo)
        {
            ValidarModelo(modelo);

            var dominio = modelo.Transform<DocumentoConclusaoApostilamento>();

            var tipoApostilamento = this.TipoApostilamentoDomainService.SearchByKey(new SMCSeqSpecification<TipoApostilamento>(dominio.SeqTipoApostilamento));

            if (tipoApostilamento.Token == TOKEN_TIPO_APOSTILAMENTO.NOVA_FORMACAO_ALUNO)
            {
                dominio.DocumentoConclusaoFormacao = new List<DocumentoConclusaoFormacao>() { new DocumentoConclusaoFormacao()
                {
                    SeqDocumentoConclusao = modelo.SeqDocumentoConclusao,
                    SeqAlunoFormacao = modelo.SeqAlunoFormacao.Value
                }};
            }
            else
                dominio.DocumentoConclusaoFormacao = new List<DocumentoConclusaoFormacao>();

            this.SaveEntity(dominio);

            return dominio.Seq;
        }

        private void ValidarModelo(DocumentoConclusaoApostilamentoVO modelo)
        {
            string extensoesPermitidasApostilamento = ".doc,.docx,.xls,.xlsx,.jpg,.jpeg,.png,.pdf,.rar,.zip,.ps";

            if (modelo.ArquivoAnexado != null)
            {
                string extensao = Path.GetExtension(modelo.ArquivoAnexado.Name);

                if (modelo.ArquivoAnexado.Size > VALIDACAO_ARQUIVO_ANEXADO.TAMANHO_MAXIMO_ARQUIVO_ANEXADO)
                {
                    throw new TamanhoArquivoExcedidoException();
                }

                if (string.IsNullOrEmpty(extensao) || !extensoesPermitidasApostilamento.Contains(extensao))
                {
                    throw new ExtensaoNaoPermitidaException(modelo.ArquivoAnexado.Name);
                }
            }
        }

        public void ExcluirDocumentoConclusaoApostilamento(long seq)
        {
            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                try
                {
                    var configToDelete = this.SearchByKey(new SMCSeqSpecification<DocumentoConclusaoApostilamento>(seq));
                    this.DeleteEntity(configToDelete);

                    unitOfWork.Commit();
                }
                catch (Exception)
                {
                    unitOfWork.Rollback();
                    throw;
                }
            }
        }
    }
}
