using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class DivisaoMatrizCurricularComponenteFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? Seq { get; set; }
        public long? SeqMatrizCurricular { get; set; }
        public long? SeqMatrizCurricularOferta { get; set; }
        public long? SeqGrupoCurricularComponente { get; set; }
        public long? SeqDivisaoMatrizCurricular { get; set; }
        public long? SeqCurriculoCursoOferta { get; set; }
        public long? SeqComponenteCurricular { get; set; }
        public long? SeqConfiguracaoComponente { get; set; }
        public long? SeqAluno { get; set; }
        public long? SeqNivelEnsino { get; set; }
        public long? SeqTipoTrabalho { get; set; }
        public long? SeqInstituicaoEnsino { get; set; }
        public TipoGestaoDivisaoComponente? TipoGestaoDivisaoComponente { get; set; }
        public string DescricaoGrupoCurricular { get; set; }
        public ComponentesConfiguracaoCadastrada? SomenteComponentesSemConfiguracao { get; set; }
        public long? SeqTipoComponente { get; set; }
    }
}


