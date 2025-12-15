using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CAM.ValueObjects
{
    public class ConvocacaoImpedimentosDeMatriculaVO : ISMCMappable
    {
        public ConvocacaoImpedimentosDeMatriculaVO()
        {
            this.Impedimentos = new List<ConvocadoImpedimentoVO>();
        }

        public string DescricaoCampanha { get; set; }

        public string DescricaoProcessoSeletivo { get; set; }

        public int NumeroConvocados { get; set; }

        public bool SemImpedimento { get; set; }

        public List<ConvocadoImpedimentoVO> Impedimentos { get; set; }
    }
}