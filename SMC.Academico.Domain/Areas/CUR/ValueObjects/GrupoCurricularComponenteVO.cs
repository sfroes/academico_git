using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class GrupoCurricularComponenteVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public string Codigo { get; set; }

        public string Descricao { get; set; }

        public string DescricaoComplementar { get; set; }

        public string DescricaoGrupoCurricular { get; set; }

        public short? CargaHoraria { get; set; }

        public short? Credito { get; set; }

        public long SeqTipoComponenteCurricular { get; set; }

        public long SeqNivelEnsino { get; set; }

        public List<string> EntidadesSigla { get; set; }

        public FormatoCargaHoraria? Formato { get; set; }

        public string DescricaoFormatada
        {
            get
            {
                string comum = string.Empty;

                if (!string.IsNullOrEmpty(Codigo))
                    comum = $"{Codigo}";

                if (!string.IsNullOrEmpty(Descricao))
                    comum += $" - {Descricao}";

                if (!string.IsNullOrEmpty(DescricaoComplementar))
                    comum += $" - {DescricaoComplementar}";

                if (CargaHoraria.HasValue)
                    comum += $" - {CargaHoraria} {Formato.SMCGetDescription()}";

                if (Credito.HasValue)
                    comum += $" - {Credito} Crédito";

                return comum;
            }
        }
    }
}