using System.Linq;

namespace SMC.Academico.Domain.Areas.CSO.Models
{
    public partial class CursoOfertaLocalidade
    {
        /// <summary>
        /// Recupera a o nome da localidade amarrada à oferta de localidade pela hierarquia de entidades
        /// </summary>
        /// <returns>O nome da localidade da oferta de curso por localidade</returns>
        public string RecuperarNomeLocalidade()
        {
            return this.HierarquiasEntidades?.FirstOrDefault()?.ItemSuperior?.Entidade?.Nome;
        }

        /// <summary>
        /// Recupera o seq da localidade amarrada à oferta de localidade pela hierarquia de entidades
        /// </summary>
        /// <returns>O seq da localidade da oferta de curso por localidade</returns>
        public long? RecuperarSeqLocalidade()
        {
            return this.HierarquiasEntidades?.FirstOrDefault()?.ItemSuperior?.Entidade?.Seq;
        }

        /// <summary>
        /// Recupera o nome da unidade responsável vinculada pelo curso unidade desta oferta
        /// </summary>
        /// <returns>Nome da unidade responsável</returns>
        public string RecuperarNomeUnidade()
        {
            return this?.CursoUnidade.RecuperarNomeUnidade();
        }

    }
}