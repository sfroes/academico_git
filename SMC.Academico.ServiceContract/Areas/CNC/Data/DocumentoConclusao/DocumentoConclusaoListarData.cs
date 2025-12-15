using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CNC.Data
{
    public class DocumentoConclusaoListarData : ISMCMappable
    {
        public long Seq { get; set; }

        public long NumeroRegistroAcademico { get; set; }

        public string NomeAluno { get; set; }

        public long? SeqEntidadeCursoOfertaLocalidade { get; set; }

        public string NomeEntidadeCursoOfertaLocalidade { get; set; }

        public long SeqTipoDocumentoAcademico { get; set; }

        public string DescricaoTipoDocumentoAcademico { get; set; }

        public short NumeroViaDocumento { get; set; }

        public string DescricaoSituacaoDocumentoAcademicoAtual { get; set; }

        public string DescricaoTitulacao { get; set; }

        public string DescricaoGrauAcademico { get; set; }

        public bool HabilitarBotaoExcluir { get; set; }

        public bool HabilitarBotaoApostilamento { get; set; }
    }
}
