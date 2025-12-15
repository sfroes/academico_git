using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class DocumentoConclusaoListarVO : ISMCMappable
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

        public Sexo Sexo { get; set; }

        public string TipoDocumentoToken { get; set; }

        public string Token { get; set; }

        public TipoInvalidade? TipoInvalidade { get; set; }
    }
}
