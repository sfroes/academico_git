using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class DocumentoVO : ISMCMappable
    {
        public List<SMCDatasourceItem> SolicitacoesEntregaDocumento { get; set; }

        public bool PermiteVarios { get; set; }

        public string DescricaoTipoDocumento { get; set; }

        public long SeqTipoDocumento { get; set; }

        public long SeqConfiguracaoEtapa { get; set; }

        public Sexo Sexo { get; set; }

        public List<DocumentoItemVO> Documentos { get; set; }

        public long SeqDocumentoRequerido { get; set; }

        public List<GrupoDocumentoVO> Grupos { get; set; }

        public long? NumeroMinimoDocumentosGrupoRequerido { get; set; }

        public bool PermiteEntregaPosterior { get; set; }

        public bool Obrigatorio { get; set; }

        public bool ObrigatorioUpload { get; set; }

        public bool ValidacaoOutroSetor { get; set; }

        public bool PermiteUploadArquivo { get; set; }

        public  DateTime? DataLimiteEntrega { get; set; }
        
        public bool EntregueAnteriormente { get; set; }
    }
}