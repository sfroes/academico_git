using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class EntidadeFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        [SMCFilter(true, true)]
        [SMCOrder(0)]
        [SMCSize(SMCSize.Grid2_24)]
        public long? Seq { get; set; }

        [SMCFilter]
        [SMCOrder(1)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid9_24)]
        [SMCMaxLength(100)]
        public string Nome { get; set; }

        [SMCFilter]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid6_24)]
        [SMCOrder(2)]
        [SMCSelect("Instituicoes")]
        public long? SeqTipoEntidade { get; set; }

        [SMCIgnoreMetadata]
        [SMCServiceReference(typeof(IInstituicaoTipoEntidadeService), "BuscarTipoEntidadesNaoExternadaDaInstituicaoSelect")]
        [SMCDataSource("InstituicaoTipoEntidade")]
        public List<SMCDatasourceItem> Instituicoes { get; set; }

        [SMCHidden]
        public bool Externada { get; set; } = false;
    }
}