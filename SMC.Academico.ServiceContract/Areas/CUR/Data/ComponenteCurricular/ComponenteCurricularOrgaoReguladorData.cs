using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Util;
using System;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class ComponenteCurricularOrgaoReguladorData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqComponenteCurricular { get; set; }

        public TipoOrgaoRegulador TipoOrgaoRegulador { get; set; }

        public string TipoOrgaoReguladorDescricao { get { return SMCEnumHelper.GetDescription(TipoOrgaoRegulador); } }

        public string Codigo { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }        
    }
}
