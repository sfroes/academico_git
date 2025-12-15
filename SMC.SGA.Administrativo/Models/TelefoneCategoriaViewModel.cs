using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Models
{
    /// <summary>
    /// Telefone com categoria no select para casos em que seja necessário inserir, por exemplo, "Comercial - Secretaria" ao select,
    /// que não consegue usar um enum para Tipo e Categoria ao mesmo tempo
    /// </summary>
    public class TelefoneCategoriaViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        public long SeqTelefone { get; set; }

        /// <summary>
        /// Tipo do telefone
        /// </summary>
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid8_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        [SMCRequired]
        [SMCMapForceFromTo]
        [SMCSelect("TiposTelefone", "Descricao", "Descricao", AutoSelectSingleItem = true)]
        public string DescricaoTipoTelefone { get; set; }

        /// <summary>
        /// Numero do DDI
        /// </summary>
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid4_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        [SMCOrder(1)]
        [SMCRequired]
        [SMCMask("0099")]
        [SMCMapForceFromTo]
        public int CodigoPais { get; set; } = 55;

        /// <summary>
        /// Numero do DDD
        /// </summary>
        [SMCOrder(2)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid4_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        [SMCMask("099")]
        [SMCRequired]
        [SMCAttribute(name: "data-parsley-phoneddd", value: "true")]
        [SMCAttribute(name: "data-parsley-phoneddd-message", resourceKey: "Mensagem_Erro_DDD", getValueFromResource: true)]
        public int? CodigoArea { get; set; }

        /// <summary>
        /// Numero do Telefone
        /// </summary>
        [SMCOrder(3)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid6_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        [SMCPhone]
        [SMCRequired]
        [SMCAttribute(name: "data-parsley-phonetel", value: "true")]
        [SMCAttribute(name: "data-parsley-phonetel-message", resourceKey: "Mensagem_Erro_9Digito", getValueFromResource: true)]
        public string Numero { get; set; }
    }
}