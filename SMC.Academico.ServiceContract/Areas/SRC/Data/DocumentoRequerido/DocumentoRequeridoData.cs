using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class DocumentoRequeridoData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqConfiguracaoEtapa { get; set; }

        public string DescricaoConfiguracaoEtapa { get; set; }

        public long SeqProcessoEtapa { get; set; }

        public long SeqTipoDocumento { get; set; }

        public bool Obrigatorio { get; set; }

        public bool PermiteUploadArquivo { get; set; }

        public bool ObrigatorioUpload { get; set; }

        public VersaoDocumento VersaoDocumento { get; set; }

        public Sexo? Sexo { get; set; }

        public bool PermiteEntregaPosterior { get; set; }

        
        public DateTime? DataLimiteEntrega { get; set; }

        public bool ValidacaoOutroSetor { get; set; }

        public bool PermiteVarios { get; set; }

        public bool CamposReadyOnly { get; set; }

        public string DescricaoTipoDocumento { get; set; }
    }
}
