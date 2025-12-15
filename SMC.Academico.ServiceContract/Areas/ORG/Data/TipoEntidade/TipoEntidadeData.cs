using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class TipoEntidadeData : ISMCMappable
    {
        /// <summary>
        /// Sequencial do tipo de entidade
        /// </summary>
        public long Seq { get; set; }

        /// <summary>
        /// Descrição do tipo de entidade
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Token do tipo de entidade
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Indicador de entidade externada
        /// </summary>
        public bool EntidadeExternada { get; set; }
    }
}