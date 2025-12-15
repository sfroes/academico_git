using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class PlanoEstudoItemData : ISMCMappable
    {
        public long Seq { get; set; }

        public long? SeqDivisaoTurma { get; set; }

        public long? SeqConfiguracaoComponente { get; set; }

        public string DescricaoFormatada { get; set; }

        public string DescricaoOrdenacao { get; set; }

        public long? SeqTurma { get; set; }

        public List<long> SeqsConfiguracoesComponentes { get; set; }

        public short? Credito { get; set; }

        public string CicloLetivo { get; set; }
    }
}