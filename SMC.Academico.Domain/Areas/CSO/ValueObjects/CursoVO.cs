using SMC.Academico.Common.Areas.CSO.Enums;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.ValueObjects;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CSO.ValueObjects
{
    public class CursoVO : EntidadeVO, ISMCMappable
    {
        #region [ Primitive Properties ]

        public long SeqNivelEnsino { get; set; }

        public TipoCurso TipoCurso { get; set; }

        public long[] SeqsHierarquiaEntidadeItem { get; set; }

        #endregion

        #region [ Navigation Properties ]

        public NivelEnsino NivelEnsino { get; set; }

        public List<HierarquiaEntidadeItem> HierarquiasEntidades { get; set; }

        public IList<CursoFormacaoEspecifica> CursosFormacaoEspecifica { get; set; }

        public IList<CursoOferta> CursosOferta { get; set; }

        public IList<CursoUnidade> CursosUnidade { get; set; }

        public IList<Curriculo> Curriculos { get; set; }

        #endregion

    }
}
