using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class ServicoPorAlunoFiltroVO : ISMCMappable
    {
        public long SeqAluno { get; set; }

        public long? SeqTipoServico { get; set; }

        public PermissaoServico? PermissaoServico { get; set; }

        public OrigemSolicitacaoServico? OrigemSolicitacaoServico { get; set; }

        public bool Com1EtapaAtiva { get; set; }

        public bool ConsiderarSituacaoAluno { get; set; }

        public TipoUnidadeResponsavel? TipoUnidadeResponsavel { get; set; }

        public TipoAtuacao? TipoAtuacao { get; set; }


    }
}