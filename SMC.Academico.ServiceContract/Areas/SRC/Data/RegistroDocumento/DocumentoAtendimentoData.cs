using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class DocumentoAtendimentoData : ISMCMappable
    {
        public long Seq { get; set; }

        public List<SMCDatasourceItem> SolicitacoesEntregaDocumento { get; set; }
        public long SeqSolicitacaoServico { get; set; }
        public long SeqGrupoDocumento { get; set; }

        public long SeqTipoDocumento { get; set; }

        public long? SeqArquivoAnexado { get; set; }
        public string DescricaoTipoDocumento { get; set; }

        public DateTime? DataEntrega { get; set; }

        public FormaEntregaDocumento? FormaEntregaDocumento { get; set; }

        public VersaoDocumento? VersaoDocumento { get; set; }

        public VersaoDocumento? VersaoExigida { get; set; }

        public SMCUploadFile ArquivoAnexado { get; set; }

        public SituacaoEntregaDocumento SituacaoEntregaDocumento { get; set; }

        public SituacaoEntregaDocumento SituacaoEntregaDocumentoInicial { get; set; }

        public bool BloquearTodosOsCampos { get; set; }

        public DateTime? DataPrazoEntrega { get; set; }

        public string Observacao { get; set; }

        public string DescricaoInconformidade { get; set; }

        public string ObservacaoSecretaria { get; set; }

        public DateTime? DataLimiteEntrega { get; set; }

        public bool? EntregaPosterior { get; set; }

        public bool TemAnexoAnterior { get; set; }

        public bool EntregueAnteriormente { get; set; }

        public bool PermiteVarios { get; set; }

        public long SeqConfiguracaoEtapa { get; set; }

        public Sexo Sexo { get; set; }

        public long SeqDocumentoRequerido { get; set; }

        public List<GrupoDocumentoData> Grupos { get; set; }

        public long? NumeroMinimoDocumentosGrupoRequerido { get; set; }

        public bool PermiteEntregaPosterior { get; set; }

        public bool Obrigatorio { get; set; }

        public bool ObrigatorioUpload { get; set; }

        public bool ValidacaoOutroSetor { get; set; }

        public bool PermiteUploadArquivo { get; set; }

    }
}