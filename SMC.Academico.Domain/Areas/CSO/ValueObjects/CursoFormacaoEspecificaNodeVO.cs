using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CSO.ValueObjects
{
    public class CursoFormacaoEspecificaNodeVO : ISMCMappable, ISMCSeq​​
    {
        /// <summary>
        /// Seq formação específica
        /// </summary>
        public long Seq { get; set; }

        public long? SeqFormacaoEspecificaSuperior { get; set; }

        public long SeqCurso { get; set; }

        /// <summary>
        /// Quando nulo representa uma formação específica não associada ao curso
        /// </summary>
        public long? SeqCursoFormacaoEspecifica { get; set; }

        public string Descricao { get; set; }

        public bool Ativo { get; set; }

        public bool TipoCursoFormacaoEspecificaFolha { get; set; }

        public bool PossuiOfertaCursoLocalidade { get; set; }
    }
}