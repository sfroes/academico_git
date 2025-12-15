using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
    public class MembroBancaExaminadoraViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public string Nome { get; set; }

        [SMCHidden]
        public virtual bool? Participou { get; set; }

        [SMCReadOnly]
        [SMCSize(SMCSize.Grid8_24)]
        public string DescricaoMembro { get; set; }

        public TipoMembroBanca? TipoMembroBanca { get; set; }

        [SMCHidden]
        [SMCSize(SMCSize.Grid8_24)]
        public string DescricaoTipoMembro { get => TipoMembroBanca.SMCGetDescription(); }

        [SMCHidden]
        [SMCSize(SMCSize.Grid8_24)]
        public string Instituicao { get; set; }

        [SMCHidden]
        public string NomeColaborador { get; set; }

        [SMCHidden]
        public string NomeInstituicaoExterna { get; set; }

        [SMCHidden]
        public string ComplementoInstituicao { get; set; }

    }
}