using SMC.Academico.Common.Enums;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Models
{
    public class EnderecoEletronicoCategoriaViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public TipoEnderecoEletronico TipoEnderecoEletronico { get; set; }

        [SMCHidden]
        public CategoriaEnderecoEletronico CategoriaEnderecoEletronico { get; set; }

        [SMCRequired]
        [SMCSelect("TiposEnderecoEletronico", "Descricao", "Descricao")]
        [SMCSize(Framework.SMCSize.Grid9_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid12_24, Framework.SMCSize.Grid9_24)]
        public string DescricaoTipoEnderecoEletronico { get; set; }

        [SMCDescription]
        [SMCRequired]
        [SMCSize(Framework.SMCSize.Grid13_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid12_24, Framework.SMCSize.Grid13_24)]
        [SMCMaxLength(100)]
        public string Descricao { get; set; }        
    }
}