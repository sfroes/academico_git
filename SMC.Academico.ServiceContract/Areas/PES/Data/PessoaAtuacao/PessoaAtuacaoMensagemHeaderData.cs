using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class PessoaAtuacaoMensagemHeaderData : ISMCMappable
    {
        public string Nome { get; set; }

        public string NomeSocial { get; set; }

        public TipoAtuacao TipoAtuacao { get; set; }

        public string DescricaoSituacaoMatricula { get; set; }
    }
}