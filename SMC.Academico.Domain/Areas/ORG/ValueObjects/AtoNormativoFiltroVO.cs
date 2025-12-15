using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;

namespace SMC.Academico.Domain.Areas.ORG.ValueObjects
{
    public class AtoNormativoFiltroVO : SMCPagerFilterData, ISMCMappable
    {   
        public long? Seq { get; set; }

        public long? SeqAssuntoNormativo { get; set; }

        public long? SeqTipoAtoNormativo { get; set; }

        public string NumeroDocumento { get; set; }

        public DateTime? DataDocumento { get; set; }

        public long? SeqEntidade { get; set; }

        public long? SeqTipoEntidade { get; set; }

        public string NomeEntidade { get; set; }

        public long? SeqGrauAcademico { get; set; }

        public TipoOrgaoRegulador? TipoOrgaoRegulador { get; set; }

        public int? CodigoCursoOferta { get; set; }

        public int? CodigoOrgaoRegulador { get; set; }
    }
}
