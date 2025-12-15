using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.Util;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class GrupoConfiguracaoComponenteListarItemViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        [SMCKey]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqConfiguracaoComponente { get; set; }

        [SMCHidden]
        public string ConfiguracaoComponenteCodigo { get; set; }

        [SMCHidden]
        public string ConfiguracaoComponenteDescricao { get; set; }

        [SMCHidden]
        public string ConfiguracaoComponenteDescricaoComplementar { get; set; }

        [SMCHidden]
        public short? ComponenteCurricularCargaHoraria { get; set; }

        [SMCHidden]
        public short? ComponenteCurricularCredito { get; set; }

        [SMCHidden]
        public FormatoCargaHoraria? FormatoCargaHoraria { get; set; }

        [SMCOrder(1)]
        [SMCSize(SMCSize.Grid24_24)]
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

                if (FormatoCargaHoraria.HasValue)
                    result += $" {SMCEnumHelper.GetDescription(FormatoCargaHoraria.Value)}";

                if (ComponenteCurricularCredito.HasValue)
                    result += $" - {ComponenteCurricularCredito.Value} Crédito";

                return result;
            }
        }
    }
}