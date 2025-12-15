using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.DCT.Data
{
    public class InstituicaoNivelTipoAtividadeColaboradorFiltroData : ISMCMappable
    {
        public long? SeqCursoOfertaLocalidade { get; set; }

        /// <summary>
        /// Desativa todos os filtros exceto o filtro de instituição
        /// </summary>
        public bool IgnorarFiltros { get; set; }
    }
}