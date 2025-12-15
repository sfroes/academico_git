using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class DivisaoMatrizCurricularComponenteFiltroVO : SMCPagerFilterData, ISMCMappable
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