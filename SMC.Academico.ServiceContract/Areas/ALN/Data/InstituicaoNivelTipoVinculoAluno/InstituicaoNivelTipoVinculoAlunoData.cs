using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class InstituicaoNivelTipoVinculoAlunoData : ISMCMappable
    {
        public long Seq { get; set; }
         
        public long SeqInstituicaoNivel { get; set; }
         
        public long SeqTipoVinculoAluno { get; set; } 
         
        public bool? ExigeParceriaIntercambioIngresso { get; set; }
         
        public bool? ExigeCurso { get; set; }
         
        public bool? ConcedeFormacao { get; set; }
         
        public bool? ExigeOfertaMatrizCurricular { get; set; }
         
        public short? QuantidadeOfertaCampanhaIngresso { get; set; }
         
        public TipoCobranca TipoCobranca { get; set; }
         
        public bool? PossuiValorFixoMatricula { get; set; }

        public List<FormaIngressoData> FormasIngresso { get; set; }

        public List<InstituicaoNivelTipoTermoIntercambioData> TiposTermoIntercambio { get; set; }

        public List<SituacaoMatriculaData> SituacoesMatricula { get; set; }
    }
}
