using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class ConsultaDivisaoMatrizCurricularComponenteItemData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public string CodigoConfiguracao { get; set; }

        public string DescricaoConfiguracao { get; set; }

        public string DescricaoComplementarConfiguracao { get; set; }

        public short? CargaHorariaComponente { get; set; }

        public short? CreditosComponente { get; set; }

        public SituacaoConfiguracaoComponenteCurricular SituacaoComponente { get; set; }

        public List<string> SiglasEntidadesResponsaveisComponente { get; set; }
    }
}