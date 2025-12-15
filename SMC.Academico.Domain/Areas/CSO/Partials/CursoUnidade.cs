using System.Linq;

namespace SMC.Academico.Domain.Areas.CSO.Models
{
    public partial class CursoUnidade
    {
        /// <summary>
        /// Recupera o nome da unidade responsável pelo curso unidade
        /// </summary>
        /// <returns>Nome da unidade</returns>
        public string RecuperarNomeUnidade()
        {
            return this.HierarquiasEntidades?.FirstOrDefault()?.ItemSuperior?.Entidade?.Nome;
        }
    }
}