using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class ConsultaDivisaoMatrizCurricularComponenteItemVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqGrupoCurricular { get; set; }

        public long SeqGrupoCurricularComponente { get; set; }

        public string CodigoConfiguracao { get; set; }

        public string DescricaoConfiguracao { get; set; }

        public string DescricaoComplementarConfiguracao { get; set; }

        public short? CargaHorariaComponente { get; set; }

        public short? CreditosComponente { get; set; }

        public bool? ExigeAssuntoComponente { get; set; }

        public bool ContemComponenteSubstituto { get; set; }

        public bool ContemRequisitos { get; set; }

        public SituacaoConfiguracaoComponenteCurricular SituacaoComponente { get; set; }

        public IEnumerable<string> SiglasEntidadesResponsaveisComponente { get; set; }
    }
}