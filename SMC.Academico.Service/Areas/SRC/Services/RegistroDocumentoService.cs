using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Extensions;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.SRC.Services
{
    public class RegistroDocumentoService : SMCServiceBase, IRegistroDocumentoService
    {
        #region [ DomainService ]

        private RegistroDocumentoDomainService RegistroDocumentoDomainService
        {
            get { return Create<RegistroDocumentoDomainService>(); }
        }

        #endregion [ DomainService ]

        public RegistroDocumentoCabecalhoData BuscarCabecalhoRegistroDocumentos(long seqSolicitacaoServico)
        {
            return this.RegistroDocumentoDomainService.BuscarCabecalhoRegistroDocumentos(seqSolicitacaoServico).Transform<RegistroDocumentoCabecalhoData>();
        }

        public List<DocumentoData> BuscarDocumentosRegistro(long seqSolicitacaoServico, long? seqConfiguracaoEtapa = null, bool? permiteUploadArquivo = null, bool exibirDocumentoPermiteUpload = true, bool exibirDocumentoNaoPermiteUpload = false, bool? atendimento = null)
        {
            var ret = this.RegistroDocumentoDomainService.BuscarDocumentosRegistro(seqSolicitacaoServico, seqConfiguracaoEtapa, permiteUploadArquivo, exibirDocumentoPermiteUpload, exibirDocumentoNaoPermiteUpload, atendimento);
            return ret.TransformList<DocumentoData>();
        }

        public long SalvarRegistroDocumentos(RegistroDocumentoData modelo)
        {
            return RegistroDocumentoDomainService.SalvarRegistroDocumentos(modelo.Transform<RegistroDocumentoVO>());
        }

        public void RegistrarDocumentosSolicitacao(RegistroDocumentoData modelo)
        {
            RegistroDocumentoDomainService.RegistrarDocumentosSolicitacao(modelo.Transform<RegistroDocumentoVO>());
        }

        public void AnexarDocumentosSolicitacao(RegistroDocumentoData modelo, bool enviarNotificacao = false, bool ehPrimeiraEtapa = true)
        {
            RegistroDocumentoDomainService.AnexarDocumentosSolicitacao(modelo.Transform<RegistroDocumentoVO>(), enviarNotificacao, ehPrimeiraEtapa);
        }

        /// <summary>
        /// Envia notificação para nova entrega de documentação
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de servico</param>
        public void EnviarNotificacaoUploadAluno(long seqSolicitacaoServico)
        {
            RegistroDocumentoDomainService.EnviarNotificacaoUploadAluno(seqSolicitacaoServico);
        }

        public List<DocumentoData> BuscarDocumentosRegistroPorStatus(long seqSolicitacaoServico, long? seqConfiguracaoEtapa = null, List<SituacaoEntregaDocumento> situacoes = null, bool? permiteUploadArquivo = null)
        {
            var ret = this.RegistroDocumentoDomainService.BuscarDocumentosRegistroPorStatus(seqSolicitacaoServico, seqConfiguracaoEtapa, situacoes, permiteUploadArquivo);
            return ret.TransformList<DocumentoData>();
        }

        public void SalvarRegistrarEntregaDocumentacaoAtendimento(RegistroDocumentoAtendimentoData model, long seqSolicitacaoServico)
        {
            RegistroDocumentoDomainService.SalvarRegistrarEntregaDocumentacaoAtendimento(model.Transform<RegistroDocumentoAtendimentoVO>(), seqSolicitacaoServico);
        }

        public void FinalizarSolicitacaoCRA(long seqSolicitacaoServico)
        {
            RegistroDocumentoDomainService.FinalizarSolicitacaoCRA(seqSolicitacaoServico);
        }
    }
}