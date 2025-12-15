using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class CopiarProcessoDetalheGrupoEscalonamentoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public bool? CriarGrupo { get; set; }

        public bool? CopiarNotificacoes { get; set; }
    }
}
