using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.CNC.Models
{
    public class IntegralizacaoMatrizGrupoFormacaoViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCHidden]
        public long SeqFormacaoEspecifica { get; set; }

        [SMCHidden]
        public long? SeqFormacaoEspecificaSuperior { get; set; }

        [SMCHidden]
        public string DescricaoFormacaoEspecifica { get; set; }

        [SMCHidden]
        public long SeqTipoFormacaoEspecifica { get; set; }

        [SMCHidden]
        public string DescricaoTipoFormacaoEspecifica { get; set; }

        [SMCDescription]
        public string DescricaoCompleta { get { return $"[{DescricaoTipoFormacaoEspecifica}] - {DescricaoFormacaoEspecifica}"; } }
    }
}
