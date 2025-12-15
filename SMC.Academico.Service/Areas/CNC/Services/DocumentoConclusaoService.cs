using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Domain.Areas.CNC.DomainServices;
using SMC.Academico.Domain.Areas.CNC.ValueObjects;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.ServiceContract.Areas.CNC.Data;
using SMC.Academico.ServiceContract.Areas.CNC.Data.DocumentoConclusao;
using SMC.Academico.ServiceContract.Areas.CNC.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.CNC.Services
{
    public class DocumentoConclusaoService : SMCServiceBase, IDocumentoConclusaoService
    {
        #region [ DomainService ]

        private DocumentoConclusaoDomainService DocumentoConclusaoDomainService => Create<DocumentoConclusaoDomainService>();

        private PessoaDadosPessoaisDomainService PessoaDadosPessoaisDomainService => Create<PessoaDadosPessoaisDomainService>();

        #endregion [ DomainService ]

        public DocumentoConclusaoData BuscarDocumentoConclusao(long seq)
        {
            return DocumentoConclusaoDomainService.BuscarDocumentoConclusao(seq).Transform<DocumentoConclusaoData>();
        }

        public DocumentoConclusaoData BuscarPessoaCursoGrauDocumentoConclusao(long seq)
        {
            return DocumentoConclusaoDomainService.BuscarPessoaCursoGrauDocumentoConclusao(seq).Transform<DocumentoConclusaoData>();
        }

        public DocumentoConclusaoConsultaData BuscarDocumentoConclusaoConsulta(long seq)
        {
            return DocumentoConclusaoDomainService.BuscarDocumentoConclusaoConsulta(seq).Transform<DocumentoConclusaoConsultaData>();
        }

        public SMCPagerData<DocumentoConclusaoHistoricoDownloadListarData> BuscarHistoricosDownload(DocumentoConclusaoFiltroHistoricoDownloadData filtro)
        {
            return DocumentoConclusaoDomainService.BuscarHistoricosDownload(filtro.Transform<DocumentoConclusaoFiltroHistoricoDownloadVO>()).Transform<SMCPagerData<DocumentoConclusaoHistoricoDownloadListarData>>();
        }

        public void SalvarStatusDocumentoDigital(long seqDocumentoConclusao, string tokenAcao, string observacao, MotivoInvalidadeDocumento motivoInvalidadeDocumento, TipoInvalidade? tipoCancelamento, long? seqClassificacaoInvalidadeDocumento)
        {
            DocumentoConclusaoDomainService.SalvarStatusDocumentoDigital(seqDocumentoConclusao, tokenAcao, observacao, motivoInvalidadeDocumento, tipoCancelamento, seqClassificacaoInvalidadeDocumento);
        }

        public SMCPagerData<DocumentoConclusaoListarData> BuscarDocumentosConclusao(DocumentoConclusaoFiltroData filtro)
        {
            return DocumentoConclusaoDomainService.BuscarDocumentosConclusao(filtro.Transform<DocumentoConclusaoFiltroVO>()).Transform<SMCPagerData<DocumentoConclusaoListarData>>();
        }

        public DocumentoConclusaoCabecalhoData BuscarDocumentoConclusaoCabecalho(long seq)
        {
            return DocumentoConclusaoDomainService.BuscarDocumentoConclusaoCabecalho(seq).Transform<DocumentoConclusaoCabecalhoData>();
        }

        public void Excluir(long seq)
        {
            DocumentoConclusaoDomainService.Excluir(seq);
        }

        public DocumentoConclusaoDadosPessoaisData VisualizarDadosPessoais(long seqPessoaDadosPessoais)
        {
            return PessoaDadosPessoaisDomainService.VisualizarDadosPessoais(seqPessoaDadosPessoais).Transform<DocumentoConclusaoDadosPessoaisData>();
        }

        public void AtualizarDocumentoAcademicoDiplomaOuHistoricoDigital(long seqDocumentoAcademicoGAD)
        {
            DocumentoConclusaoDomainService.AtualizarDocumentoAcademicoDiplomaOuHistoricoDigital(seqDocumentoAcademicoGAD);
        }

        public EntregaDocumentoDigitalPaginaData BuscarDadosEntregaDocumentoDigital(long seqSolicitacaoServico, bool agrupar = false)
        {
            return DocumentoConclusaoDomainService.BuscarDadosEntregaDocumentoDigital(seqSolicitacaoServico, agrupar).Transform<EntregaDocumentoDigitalPaginaData>();
        }

        public List<SMCDatasourceItem> BuscarTiposDocumentoEntregaDigital()
        {
            return DocumentoConclusaoDomainService.BuscarTiposDocumentoEntregaDigital();
        }

        public void SalvarEntregaDocumentoDigital(EntregaDocumentoDigitalPaginaData modelo)
        {
            DocumentoConclusaoDomainService.SalvarEntregaDocumentoDigital(modelo.Transform<EntregaDocumentoDigitalPaginaVO>());
        }

        public DownloadDocumentoDigitalPaginaData BuscarDadosDownloadDocumentoDigital(long seqSolicitacaoServico)
        {
            return DocumentoConclusaoDomainService.BuscarDadosDownloadDocumentoDigital(seqSolicitacaoServico).Transform<DownloadDocumentoDigitalPaginaData>();
        }

        public ConsultaPublicaData ValidarCodigoVerificacaoDiploma(string codigoVerificacao, string nomeCompletoDiplomado)
        {
            return DocumentoConclusaoDomainService.ValidarCodigoVerificacaoDiploma(codigoVerificacao, nomeCompletoDiplomado).Transform<ConsultaPublicaData>();
        }

        public (List<long> listaSeqsDocumentosAssociados, string listaDocumentosAssociados) ValidarDocumentoConclusaoParaAlterarStatus(long seqDocumentoConclusao, string tokenAcao)
        {
            return DocumentoConclusaoDomainService.ValidarDocumentoConclusaoParaAlterarStatus(seqDocumentoConclusao, tokenAcao);
        }

        public bool DocumentoInvalidoTemporario(long seqDocumentoConclusao)
        {
            return DocumentoConclusaoDomainService.DocumentoInvalidoTemporario(seqDocumentoConclusao);
        }

        public string BuscarDadosPessoaisDocumentoConclusao(long seqPessoaAtuacao, string descricaoCursoDocumento, long? seqGrauAcademico, long? seqTitulacao, long seqPessoaDadosPessoais, TipoIdentidade? tipoIdentidade)
        {
            return DocumentoConclusaoDomainService.BuscarDadosPessoaisDocumentoConclusao(seqPessoaAtuacao, descricaoCursoDocumento, seqGrauAcademico, seqTitulacao, seqPessoaDadosPessoais, tipoIdentidade);
        }


    }
}
