using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class CabecalhoDocumentoRequeridoViewModel : ISMCMappable
    {
        public long SeqProcesso { get; set; }

        public string DescricaoProcesso { get; set; }

        public long SeqEtapaSgf { get; set; }

        public string DescricaoEtapaSgf { get; set; }

        public SituacaoEtapa SituacaoEtapa { get; set; }

        public string DescricaoConfiguracaoEtapa { get; set; }
    }
}