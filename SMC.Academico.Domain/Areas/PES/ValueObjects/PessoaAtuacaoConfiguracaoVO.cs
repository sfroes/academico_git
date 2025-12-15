using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    /// <summary>
    /// Configurações comuns da pessoa atuação como nível de ensino e matriz curricular
    /// (O modelo original não têm esses campos por não serem aplicaveis à colaborador)
    /// </summary>
    public class PessoaAtuacaoConfiguracaoVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long? SeqAluno { get; set; }

        public long? SeqIngressante { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public long? SeqMatrizCurricularOferta { get; set; }
    }
}