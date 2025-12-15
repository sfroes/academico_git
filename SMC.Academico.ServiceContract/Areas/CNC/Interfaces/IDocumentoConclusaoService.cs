using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.ServiceContract.Areas.CNC.Data;
using SMC.Academico.ServiceContract.Areas.CNC.Data.DocumentoConclusao;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CNC.Interfaces
{
    public interface IDocumentoConclusaoService : ISMCService
    {
        DocumentoConclusaoData BuscarDocumentoConclusao(long seq);

        DocumentoConclusaoData BuscarPessoaCursoGrauDocumentoConclusao(long seq);

        DocumentoConclusaoConsultaData BuscarDocumentoConclusaoConsulta(long seq);

        SMCPagerData<DocumentoConclusaoHistoricoDownloadListarData> BuscarHistoricosDownload(DocumentoConclusaoFiltroHistoricoDownloadData filtro);

        void SalvarStatusDocumentoDigital(long seqDocumentoConclusao, string tokenAcao, string observacao, MotivoInvalidadeDocumento motivoInvalidadeDocumento, TipoInvalidade? tipoCancelamento, long? seqClassificacaoInvalidadeDocumento);

        SMCPagerData<DocumentoConclusaoListarData> BuscarDocumentosConclusao(DocumentoConclusaoFiltroData filtro);

        DocumentoConclusaoCabecalhoData BuscarDocumentoConclusaoCabecalho(long seq);

        void Excluir(long seq);

        DocumentoConclusaoDadosPessoaisData VisualizarDadosPessoais(long seqPessoaDadosPessoais);

        void AtualizarDocumentoAcademicoDiplomaOuHistoricoDigital(long seqDocumentoAcademicoGAD);

        EntregaDocumentoDigitalPaginaData BuscarDadosEntregaDocumentoDigital(long seqSolicitacaoServico, bool agrupar = false);

        List<SMCDatasourceItem> BuscarTiposDocumentoEntregaDigital();

        void SalvarEntregaDocumentoDigital(EntregaDocumentoDigitalPaginaData modelo);

        DownloadDocumentoDigitalPaginaData BuscarDadosDownloadDocumentoDigital(long seqSolicitacaoServico);

        ConsultaPublicaData ValidarCodigoVerificacaoDiploma(string codigoVerificacao, string nomeCompletoDiplomado);

        (List<long> listaSeqsDocumentosAssociados, string listaDocumentosAssociados) ValidarDocumentoConclusaoParaAlterarStatus(long seqDocumentoConclusao, string tokenAcao);

        bool DocumentoInvalidoTemporario(long seqDocumentoConclusao);

        string BuscarDadosPessoaisDocumentoConclusao(long seqPessoaAtuacao, string descricaoCursoDocumento, long? seqGrauAcademico, long? seqTitulacao, long seqPessoaDadosPessoais, TipoIdentidade? tipoIdentidade);
    }
}
