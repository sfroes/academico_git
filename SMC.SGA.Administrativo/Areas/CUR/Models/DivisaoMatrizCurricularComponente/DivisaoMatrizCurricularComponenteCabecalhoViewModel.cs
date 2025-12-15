using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.SGA.Administrativo.Areas.CUR.Views.DivisaoMatrizCurricularComponente.App_LocalResources;
using System.Collections.Generic;
using System.Linq;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class DivisaoMatrizCurricularComponenteCabecalhoViewModel : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }
        public string CodigoMatriz { get; set; }
        public string DescricaoMatriz { get; set; }
        public string DescricaoMatrizComplementar { get; set; }
        public string DescricaoMatrizFormatada => string.Join(" - ", new[] { DescricaoMatriz, DescricaoMatrizComplementar }.Where(w => !string.IsNullOrEmpty(w)));
        public string CodigoComponente { get; set; }
        public string DescricaoComponente { get; set; }
        public short? CargaHorariaComponente { get; set; }
        public short? CreditosComponente { get; set; }
        public string Formato { get; set; }
        public string DescricaoComponenteFormatada
        {
            get
            {
                if (CargaHorariaComponente.HasValue && CreditosComponente.HasValue)
                    return $"{CodigoComponente} - {DescricaoComponente} - {CargaHorariaComponente} {Formato} - {CreditosComponente} {UIResource.Label_Credito}";
                else if (CargaHorariaComponente.HasValue)
                    return $"{CodigoComponente} - {DescricaoComponente} - {CargaHorariaComponente} {Formato}";
                else if (CreditosComponente.HasValue)
                    return $"{CodigoComponente} - {DescricaoComponente} - {CreditosComponente} {UIResource.Label_Credito}";
                return $"{CodigoComponente} - {DescricaoComponente}";
            }
        }
        public List<string> GruposPertecentes { get; set; }

    }
}