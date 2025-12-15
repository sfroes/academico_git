using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Domain;

namespace SMC.Academico.Domain.Areas.CUR.DomainServices
{
    public class DivisaoCurricularItemDomainService : AcademicoContextDomain<DivisaoCurricularItem>
    {
        /// <summary>
        /// Descrição de uma divisão de matriz curricular segundo a regra RN_CUR_031
        /// </summary>
        /// <param name="numero">Número da divisão</param>
        /// <param name="descricaoTipoDivisaoCurricular">Descrição do tipo da divisão curricular</param>
        /// <returns>Descrição da divisão formatada conforme a regra RN_CUR_031 [Número do Item da Divisão + Descrição do Tipo de Divisão]</returns>
        public static string GerarDescricaoDivisaoMatrizCurricularItem(short numero, string descricaoTipoDivisaoCurricular)
        {
            return $"{numero} - {descricaoTipoDivisaoCurricular}";
        }
    }
}