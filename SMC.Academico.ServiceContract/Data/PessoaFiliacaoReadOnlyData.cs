using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Data
{
    public class PessoaFiliacaoReadOnlyData : ISMCSeq, ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqPessoa { get; set; }

        public TipoParentesco TipoParentescoReadOnly { get; set; }

        public string NomeFiliacaoReadOnly { get; set; }
    }
}