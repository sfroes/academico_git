using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    /// <summary>
    /// Representa um Nível de Ensino
    /// </summary>
    public class NivelEnsinoData : ISMCMappable
    {
        /// <summary>
        /// Sequencial do Nível de ensino
        /// </summary>
        public long Seq { get; set; }

        /// <summary>
        /// Descrição do nível de ensino
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Sigla do nível de ensino
        /// </summary>
        public string Sigla { get; set; }

        /// <summary>
        /// Token do nível de ensino
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Sequencial do nível de ensino superior, caso não seja um item raiz
        /// </summary>
        [SMCMapProperty("SeqPai")]
        public long? SeqNivelEnsinoSuperior { get; set; }

        /// <summary>
        /// Descrição do nível de ensino superior, caso exista
        /// </summary>
        [SMCMapProperty("NivelEnsinoSuperior.Descricao")]
        public string NivelSuperior { get; set; }

        /// <summary>
        /// Ordem do nível de ensino
        /// </summary>
        public short? Ordem { get; set; }
    }
}