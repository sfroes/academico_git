using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Interfaces
{
    public interface IRegistroDocumentoService : ISMCService
    {
        RegistroDocumentoCabecalhoData BuscarCabecalhoRegistroDocumentos(long seqSolicitacaoServico);

        List<DocumentoData> BuscarDocumentosRegistro(long seqSolicitacaoServico, long? seqConfiguracaoEtapa = null, bool? permiteUploadArquivo = null, bool exibirDocumentoPermiteUpload = true, bool exibirDocumentoNaoPermiteUpload = false, bool? atendimento = null);

        long SalvarRegistroDocumentos(RegistroDocumentoData modelo);

        void RegistrarDocumentosSolicitacao(RegistroDocumentoData modelo);

        void AnexarDocumentosSolicitacao(RegistroDocumentoData modelo, bool enviarNotificacao = false, bool ehPrimeiraEtapa = true);

        /// <summary>
        /// Envia notificação para nova entrega de documentação
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de servico</param>
        void EnviarNotificacaoUploadAluno(long seqSolicitacaoServico);

        List<DocumentoData> BuscarDocumentosRegistroPorStatus(long seqSolicitacaoServico, long? seqConfiguracaoEtapa = null, List<SituacaoEntregaDocumento> situacoes = null, bool? permiteUploadArquivo = null);

        void SalvarRegistrarEntregaDocumentacaoAtendimento(RegistroDocumentoAtendimentoData model, long seqSolicitacaoServico);
        void FinalizarSolicitacaoCRA(long seqSolicitacaoServico);
    }
}