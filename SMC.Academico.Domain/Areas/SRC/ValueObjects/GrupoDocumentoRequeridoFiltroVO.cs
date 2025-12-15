using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class GrupoDocumentoRequeridoFiltroVO : SMCPagerFilterData, ISMCMappable
    {
        public long SeqProcesso { get; set; }

        public long SeqProcessoEtapa { get; set; }
        
        public long SeqConfiguracaoEtapa { get; set; }

        public SituacaoEtapa SituacaoEtapa { get; set; }

        public bool PossuiPaginaAnexarDocumentos { get; set; }      
    }
}
