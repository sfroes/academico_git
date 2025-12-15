using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.APR.ValueObjects
{
    public class AulaOfertaVO : ISMCMappable
    {
        public long SeqCursoOfertaLocalidadeTurno { get; set; }

        public string DescricaoCursoOfertaLocalidadeTurno { get; set; }

        public List<ApuracaoFrequenciaVO> ApuracoesFrequencia { get; set; }
    }
}