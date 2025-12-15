using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Util;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class GrupoConfiguracaoComponenteItemVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqConfiguracaoComponente { get; set; }

        public string ConfiguracaoComponenteCodigo { get; set; }

        public string ConfiguracaoComponenteDescricao { get; set; }

        public string ConfiguracaoComponenteDescricaoComplementar { get; set; }

        public long? SeqComponenteCurricular { get; set; }

        public short? ComponenteCurricularCargaHoraria { get; set; }

        public short? ComponenteCurricularCredito { get; set; }

        public FormatoCargaHoraria? FormatoCargaHoraria { get; set; }

        public IEnumerable<string> ComponenteCurricularEntidadesSigla { get; set; }


        /// <summary>
        /// [Código da configuração] + "-" + [Descrição] + "-" + [Descrição complementar] + "-" + [Carga horária do componente curricular] 
        /// + [label parametrizado*] + "-" + [Créditos do componente curricular] + "Créditos" + "-"  
        /// + [Lista de siglas das entidades responsáveis do componente separadas por "/", ordenadas alfabeticamente]. 
        /// </summary>
        public string ConfiguracaoComponenteDescricaoCompleta
        {
            get
            {
                var result = $"{ConfiguracaoComponenteCodigo}";

                if (!string.IsNullOrEmpty(ConfiguracaoComponenteDescricao))
                    result += $" - {ConfiguracaoComponenteDescricao}";

                if (!string.IsNullOrEmpty(ConfiguracaoComponenteDescricaoComplementar))
                    result += $" - {ConfiguracaoComponenteDescricaoComplementar}";

                if (ComponenteCurricularCargaHoraria.HasValue)
                    result += $" - {ComponenteCurricularCargaHoraria.Value}";

                if (FormatoCargaHoraria.HasValue && FormatoCargaHoraria.Value != Common.Areas.CUR.Enums.FormatoCargaHoraria.Nenhum)
                    result += $" {SMCEnumHelper.GetDescription(FormatoCargaHoraria)}";

                if (ComponenteCurricularCredito.HasValue)
                {
                    var credito = ComponenteCurricularCredito.Value > 1 ? "Créditos" : "Crédito";
                    result += $" - {ComponenteCurricularCredito.Value} {credito}";
                }
                if (ComponenteCurricularEntidadesSigla != null && ComponenteCurricularEntidadesSigla.Any(x => x != null))
                    result += $" - {string.Join(" / ", ComponenteCurricularEntidadesSigla.OrderBy(x => x))}";

                return result;
            }
        }
    }
}
