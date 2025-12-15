using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class ConvocacaoImpedimentosDeMatriculaData : ISMCMappable
    {
        public string DescricaoCampanha { get; set; }

        public string DescricaoProcessoSeletivo { get; set; }

        public int NumeroConvocados { get; set; }

        public bool SemImpedimento { get; set; }

        public List<ConvocadoImpedimentoData> Impedimentos { get; set; }
    }
}