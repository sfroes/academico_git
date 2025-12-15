using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class DocumentacaoComprobatoriaVO : ISMCMappable
    {
        public string Tipo { get; set; } //enum DocumentoIdentidadeDoAluno, ProvaConclusaoEnsinoMedio, ProvaColacao, ComprovacaoEstagioCurricular, CertidaoNascimento, CertidaoCasamento, TituloEleitor, AtoNaturalizacao, Outros, TermoResponsabilidade, HistoricoEscolar
        public string Observacoes { get; set; }
        public string PdfA { get; set; } //Conteúdo do arquivo em formato Base 64.
    }
}
