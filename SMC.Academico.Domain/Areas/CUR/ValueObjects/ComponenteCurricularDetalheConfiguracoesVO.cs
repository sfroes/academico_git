using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class ComponenteCurricularDetalheConfiguracoesVO : ISMCMappable
    {
        public long Seq { get; set; }
           
        public string Codigo { get; set; }

        public string Descricao { get; set; }

        public string DescricaoComplementar { get; set; }    

        public short? CargaHoraria { get; set; }

        public short? Credito { get; set; }

        public FormatoCargaHoraria? FormatoCargaHoraria { get; set; }

        public IEnumerable<string> EntidadesSigla { get; set; }

        public string DescricaoCompleta
        {
            get
            {
                if (CargaHoraria.HasValue && Credito.HasValue)
                    return $"{Codigo} - {Descricao} - {DescricaoComplementar} - {CargaHoraria} {FormatoCargaHoraria} - {Credito} Crédito - {string.Join(" / ", EntidadesSigla ?? new List<string>())}";

                if (CargaHoraria.HasValue)
                    return $"{Codigo} - {Descricao} - {DescricaoComplementar} - {CargaHoraria} {FormatoCargaHoraria} - {string.Join(" / ", EntidadesSigla ?? new List<string>())}";


                if (Credito.HasValue)
                    return $"{Codigo} - {Descricao} - {DescricaoComplementar} - {Credito} Crédito - {string.Join(" / ",EntidadesSigla ?? new List<string>())}";

                if (!string.IsNullOrEmpty(Codigo))
                    return $"{Codigo} - {Descricao} - {DescricaoComplementar} - {string.Join(" / ", EntidadesSigla ?? new List<string>())}";

                return string.Empty;
            }
        }

        public IList<ComponenteCurricularDetalheDivisoesVO> Divisoes { get; set; }
    }
}
