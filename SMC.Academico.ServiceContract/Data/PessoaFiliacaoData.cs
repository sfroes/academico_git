using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Data
{
    public class PessoaFiliacaoData : ISMCSeq, ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqPessoa { get; set; }

        public TipoParentesco TipoParentesco { get; set; }

        public string Nome { get; set; }
    }
}