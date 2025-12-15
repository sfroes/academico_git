using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.MAT.ValueObjects
{
    public class FormacaoCurriculoVO : ISMCMappable
    {
        public int CodCurriculo { get; set; }
        public string TipoFormacaoCurriculo { get; set; }
        public string DescricaoTituloFormacaoCurricular { get; set; }
        public int? CodGrupoProposicoes { get; set; }
        public int SeqFormacaoCurriculo { get; set; }
        public int SeqFormacaoEspecifica { get; set; }
        public int FormacaoSelecionada { get; set; }
    }
}