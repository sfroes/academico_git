using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data
{
    public class CursoOfertaLocalidadeListaData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqCursoUnidade { get; set; }

        public long SeqTipoEntidade { get; set; }

        public string DescricaoGrauAcademico { get; set; }

        public string NomeCurso { get; set; }

        public string NomeUnidade { get; set; }

        public string Nome { get; set; }

        public List<CursoOfertaLocalidadeTurnoData> Turnos { get; set; }

        [SMCMapProperty("CursoOferta.Curso.NivelEnsino.Descricao")]
        public string DescricaoNivelEnsino { get; set; }

        [SMCMapProperty("CursoOferta.Curso.SeqNivelEnsino")]
        public long? SeqNivelEnsino { get; set; }

        [SMCMapProperty("CursoOferta.Curso.Nome")]
        public string DescricaoCurso { get; set; }

        [SMCMapProperty("CursoOferta.Descricao")]
        public string DescricaoOfertaCurso { get; set; }

        public long? SeqCursoOferta { get; set; }

        [SMCMapMethod("VerificarSituacaoAtualAtiva")]
        public bool Ativo { get; set; }

        [SMCMapMethod("RecuperarNomeLocalidade")]
        public string NomeLocalidade { get; set; }

        [SMCMapMethod("RecuperarSeqLocalidade")]
        public long? SeqLocalidade { get; set; }

        [SMCMapProperty("Modalidade.Descricao")]
        public string DescricaoModalidade { get; set; }
    }
}