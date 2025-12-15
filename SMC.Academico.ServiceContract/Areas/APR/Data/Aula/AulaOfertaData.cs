using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.APR.Data.Aula
{
    public class AulaOfertaData : ISMCMappable
    {
        public string DescricaoCursoOfertaLocalidadeTurno { get; set; }

        public List<ApuracaoFrequenciaData> ApuracoesFrequencia { get; set; }
    }
}