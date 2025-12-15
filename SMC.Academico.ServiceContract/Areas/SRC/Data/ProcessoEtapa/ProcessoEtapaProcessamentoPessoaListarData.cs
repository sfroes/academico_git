using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class ProcessoEtapaProcessamentoPessoaListarData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqPessoa { get; set; }
        
        public string NomeFormatado { get; set; }

        public string CPF { get; set; }

        public string Passaporte { get; set; }
    }
}
