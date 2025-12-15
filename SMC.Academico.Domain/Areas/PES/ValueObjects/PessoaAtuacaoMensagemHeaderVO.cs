using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class PessoaAtuacaoMensagemHeaderVO : ISMCMappable
    {
        public string Nome { get; set; }

        public string NomeSocial { get; set; }

        public TipoAtuacao TipoAtuacao { get; set; }

        public string DescricaoSituacaoMatricula { get; set; }
    }
}