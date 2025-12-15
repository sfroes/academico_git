using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.CSO.Lookups
{
    public class FormacaoEspecificaLookupGridItemViewModel
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public string Descricao { get; set; }

        [SMCHidden]
        public string DescricaoTipoFormacaoEspecifica { get; set; }

        [SMCHidden]
        public long SeqTipoFormacaoEspecifica { get; set; }

        [SMCHidden]
        public string DescricaoGrau { get; set; }

        public string DescricaoCompleta
        {
            get
            {
                if (string.IsNullOrEmpty(DescricaoGrau))
                    return $"[{DescricaoTipoFormacaoEspecifica}] {Descricao}";
                else
                    return $"[{DescricaoTipoFormacaoEspecifica}] {Descricao} ({DescricaoGrau})";
            }
        }
    }
}