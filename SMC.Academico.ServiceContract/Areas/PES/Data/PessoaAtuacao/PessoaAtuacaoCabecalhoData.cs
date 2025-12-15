using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class PessoaAtuacaoCabecalhoData : ISMCMappable
    {
        public string Nome { get; set; }

        public string NomeSocial { get; set; }

        public string Cpf { get; set; }

        public string NumeroPassaporte { get; set; }

        public bool Falecido { get; set; }

        public TipoAtuacao TipoAtuacao { get; set; }

        public long? CodigoMigracaoAluno { get; set; }

        public long? RA { get; set; }

        public long? SeqIngressante { get; set; }
    }
}