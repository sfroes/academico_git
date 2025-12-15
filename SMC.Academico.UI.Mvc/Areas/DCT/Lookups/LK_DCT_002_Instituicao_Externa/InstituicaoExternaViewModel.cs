using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.DCT.Lookups
{
    public class InstituicaoExternaViewModel : SMCViewModelBase, ISMCLookupData
    {
        [SMCKey]
        [SMCHidden]
        public long? Seq { get; set; }

        public string Sigla { get; set; }
        
        [SMCSortable(true)]
        public string Nome { get; set; }

        public string NomeSigla { get; set; }

        public string DescricaoPais { get; set; }

        public TipoInstituicaoEnsino? TipoInstituicaoEnsino { get; set; }

        public string DescricaoCategoria { get; set; }

        public bool? Ativo { get; set; }

        [SMCDescription]
        public string Descricao
        {
            get
            {
                if (Nome != null || Sigla != null)
                {
                    return $"{Nome} - {Sigla}";
                }
                return null;
            }
        }
    }
}
