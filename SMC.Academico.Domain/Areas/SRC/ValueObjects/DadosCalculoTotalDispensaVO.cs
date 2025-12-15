using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class DadosCalculoTotalDispensaVO : ISMCMappable
    {
        public List<long> SeqsComponentesCurricularesDispensa { get; set; }
        public List<DadosCalculoCurriculoCursoOfertaGrupo> DadosGruposCurricularesDispensa { get; set; }

        public List<long> SeqsComponentesCurricularesOrigemInterna { get; set; }

        public List<DadosCalculoTotalDispensaOrigemExternaVO> OrigensExternas { get; set; }

        public long SeqInstituicaoEnsino { get; set; }
    }

    public class DadosCalculoTotalDispensaOrigemExternaVO
    {
        public FormatoCargaHoraria? FormatoCargaHoraria { get; set; }
        public short? Credito { get; set; }

        public short? CargaHoraria { get; set; }
    }

    public class DadosCalculoCurriculoCursoOfertaGrupo
    {
        public long SeqCurriculoCursoOfertaGrupo { get; set; }
        public short? QuantidadeDispensaGrupo { get; set; }
    }
}