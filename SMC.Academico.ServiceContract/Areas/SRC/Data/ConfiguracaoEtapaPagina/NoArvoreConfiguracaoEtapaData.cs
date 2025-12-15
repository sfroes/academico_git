using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class NoArvoreConfiguracaoEtapaData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqItem { get; set; }

        public long SeqConfiguracaoEtapa { get; set; }

        public long SeqEtapaProcesso { get; set; }

        public long SeqProcesso { get; set; }

        public long? SeqPai { get; set; }

        public string Descricao { get; set; }

        public TipoItemPaginaEtapa Tipo { get; set; }

        public bool PaginaPermiteExibicaoOutrasPaginas { get; set; }

        public bool PaginaObrigatoria { get; set; }

        public bool PaginaExibeFormulario { get; set; }

        public bool PaginaPermiteDuplicar { get; set; }
       
        public SituacaoEtapa SituacaoEtapa { get; set; }      
    }
}
