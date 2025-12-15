using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class ConsultaDivisaoMatrizCurricularComponenteItemViewModel : SMCViewModelBase, ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public string CodigoConfiguracao { get; set; }

        public string DescricaoConfiguracao { get; set; }

        public string DescricaoComplementarConfiguracao { get; set; }

        public short? CargaHorariaComponente { get; set; }

        public short? CreditosComponente { get; set; }

        public SituacaoConfiguracaoComponenteCurricular SituacaoComponente { get; set; }

        public List<string> SiglasEntidadesResponsaveisComponente { get; set; }

        [SMCDescription]
        [SMCSize(SMCSize.Grid22_24)]
        public string DescricaoFormatada
        {
            get {
                string retorno = string.Empty;

                if (!string.IsNullOrEmpty(CodigoConfiguracao))
                    retorno = $"{CodigoConfiguracao}";

                if (!string.IsNullOrEmpty(DescricaoConfiguracao))
                    if(retorno.Length == 0)
                        retorno = $"{DescricaoConfiguracao}";
                    else
                        retorno += $" - {DescricaoConfiguracao}";

                if (!string.IsNullOrEmpty(DescricaoComplementarConfiguracao))
                    if (retorno.Length == 0)
                        retorno = $"{DescricaoComplementarConfiguracao}";
                    else
                        retorno += $" - {DescricaoComplementarConfiguracao}";

                if (CargaHorariaComponente != null)
                    if (retorno.Length == 0)
                        retorno = $"{CargaHorariaComponente}";
                    else
                        retorno += $" - {CargaHorariaComponente}";

                if (CreditosComponente != null)
                    if (retorno.Length == 0)
                        retorno = $"{CreditosComponente}";
                    else
                        retorno += $" - {CreditosComponente}";

                if (SiglasEntidadesResponsaveisComponente.SMCCount() > 0)
                    if (retorno.Length == 0)
                        retorno = $"{string.Join("/", SiglasEntidadesResponsaveisComponente)}";
                    else
                        retorno += $" - {string.Join("/", SiglasEntidadesResponsaveisComponente)}";

                return retorno;
            }
        }
    }
}