using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class MatrizCurricularRelatorioCabecalhoVO : ISMCMappable
    {
        public long SeqCurriculoCursoOferta { get; set; }

        public long SeqMatrizCurricular { get; set; }
                
        public string DescricaoMatrizCurricular { get; set; }

        public string DescricaoSituacao { get; set; }

        public long SeqNivelEnsino { get; set; }

        public long SeqTipoComponenteCurricular { get; set; }

        public string DescricaoNivelEnsino { get; set; }

        public string DescricaoLocalidade { get; set; }

        public string DescricaoTurno { get; set; }
    }
}
