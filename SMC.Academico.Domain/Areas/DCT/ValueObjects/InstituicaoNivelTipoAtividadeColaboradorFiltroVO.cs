using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.Domain.Areas.DCT.ValueObjects
{
    public class InstituicaoNivelTipoAtividadeColaboradorFiltroVO : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqCursoOfertaLocalidade { get; set; }

        /// <summary>
        /// Desativa todos os filtros exceto o filtro de instituição
        /// </summary>
        public bool IgnorarFiltros { get; set; }
    }
}