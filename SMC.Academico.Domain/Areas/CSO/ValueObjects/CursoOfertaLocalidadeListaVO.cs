using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CSO.ValueObjects
{
    public class CursoOfertaLocalidadeListaVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqCursoUnidade { get; set; }

        public long SeqTipoEntidade { get; set; }

        [SMCMapProperty("CursoUnidade.Curso.NivelEnsino.Descricao")]
        public string DescricaoGrauAcademico { get; set; }

        [SMCMapProperty("CursoUnidade.Curso.Nome")]
        public string NomeCurso { get; set; }

        public string Nome { get; set; }

        [SMCMapMethod("RecuperarNomeUnidade")]
        public string NomeUnidade { get; set; }

        [SMCMapMethod("VerificarSituacaoAtualAtiva")]
        public bool Ativo { get; set; }

        public long SeqEntidadeLocalidade { get; set; }

        public List<CursoOfertaLocalidadeTurnoVO> Turnos { get; set; }
    }
}