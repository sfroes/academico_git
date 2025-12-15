using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class TranscricaoAcademicaVO : ISMCMappable
    {
        public bool IsNsf { get; set; }
        public DiplomadoVO Aluno { get; set; }
        public DadosMinimosCursoVO DadosCurso { get; set; }
        public DadosMinimosIesEmissoraVO IesEmissora { get; set; }
        public HistoricoVO HistoricoEscolar { get; set; }
        public string InformacoesAdicionais { get; set; }
    }
}
