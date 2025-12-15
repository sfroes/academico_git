using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class TipoAtoNormativoData : ISMCMappable
    {
        /// <summary>
        /// Sequencial do tipo de ato normativo
        /// </summary>
        public long Seq { get; set; }

        /// <summary>
        /// Descrição do tipo de ato normativo
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Ativo tipo de ato normativo
        /// </summary>
        public bool Ativo { get; set; }
    }
}