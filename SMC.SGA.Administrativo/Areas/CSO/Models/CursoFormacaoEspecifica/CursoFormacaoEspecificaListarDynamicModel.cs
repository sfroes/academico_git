using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class CursoFormacaoEspecificaListarDynamicModel : SMCDynamicViewModel, ISMCTreeNode
    {
        /// <summary>
        /// Seq formação específica
        /// </summary>
        [SMCKey]
        public override long Seq { get; set; }

        [SMCHidden]
        [SMCMapProperty("SeqFormacaoEspecificaSuperior")]
        public long? SeqPai { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqCurso { get; set; }

        [SMCDescription]
        [SMCSize(SMCSize.Grid24_24)]
        public string Descricao { get; set; }

        /// <summary>
        /// Quando nulo representa uma formação específica não associada ao curso
        /// </summary>
        [SMCHidden]
        public long? SeqCursoFormacaoEspecifica { get; set; }

        [SMCIgnoreProp]
        public bool Ativo { get; set; }

        [SMCHidden]
        public bool TipoCursoFormacaoEspecificaFolha { get; set; }

        [SMCHidden]
        public bool PossuiOfertaCursoLocalidade { get; set; }
    }
}