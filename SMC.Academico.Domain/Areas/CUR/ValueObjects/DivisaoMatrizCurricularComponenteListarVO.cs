using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class DivisaoMatrizCurricularComponenteListarVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqComponenteCurricular { get; set; }

        public long SeqGrupoCurricularComponente { get; set; }

        public long SeqGrupoCurricular { get; set; }

        public string DescricaoComponente { get; set; }

        public string DescricaoDivisao { get; set; }

        public string Codigo { get; set; }

        public string Descricao { get; set; }

        public string DescricaoComplementar { get; set; }

        public short? CargaHoraria { get; set; }

        public short? Credito { get; set; }

        public long SeqTipoComponenteCurricular { get; set; }

        public long SeqNivelEnsino { get; set; }

        public List<string> EntidadesSigla { get; set; }

        public FormatoCargaHoraria? Formato { get; set; }

        public string DescricaoConfiguracaoComponente { get; set; }

        public bool? ExigeAssuntoComponente { get; set; }
    }
}