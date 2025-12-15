using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class CopiarProcessoDetalheGrupoEscalonamentoData : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public bool? CriarGrupo { get; set; }

        public bool? CopiarNotificacoes { get; set; }
    }
}
