using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class TipoApostilamentoVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public string Descricao { get; set; }

        public string Token { get; set; }

        public bool Ativo { get; set; }
    }
}
