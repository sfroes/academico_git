using SMC.Framework;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.CSO.ValueObjects
{
    public class CursoOfertaVO : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }

        public long SeqTipoOfertaCurso { get; set; }

        public long SeqCurso { get; set; }

        public long? SeqFormacaoEspecifica { get; set; }

        public long? SeqGrauAcademico { get; set; }

        public string Descricao { get; set; }

        public string DescricaoComplementar { get; set; }

        public string DescricaoDocumentoConclusao { get; set; }

        public bool Ativo { get; set; }

        public DateTime? DataLiberacao { get; set; }

        [SMCMapProperty("Curso.NivelEnsino.Descricao")]
        public string DescricaoInstituicaoNivelEnsino { get; set; }

        [SMCMapProperty("Curso.Nome")]
        public string DescricaoCurso { get; set; }

        [SMCMapProperty("Curso.SituacaoAtual.Descricao")]
        public string DescricaoSituacaoAtual { get; set; }
    }
}