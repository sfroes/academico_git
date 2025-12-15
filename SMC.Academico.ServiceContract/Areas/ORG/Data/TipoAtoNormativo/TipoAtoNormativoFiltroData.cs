using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class TipoAtoNormativoFiltroData : ISMCMappable
    {
        /// <summary>
        /// Ativo tipo de ato normativo
        /// </summary>
        public bool? Ativo { get; set; }
    }
}