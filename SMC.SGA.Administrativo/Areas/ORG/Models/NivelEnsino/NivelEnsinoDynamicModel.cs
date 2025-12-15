using SMC.Academico.Common.Areas.ORG.Constants;
using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class NivelEnsinoDynamicModel : SMCDynamicViewModel, ISMCTreeNode
    {
        public NivelEnsinoDynamicModel()
        {
            this.ReconhecidoLDB = true;
        }

        [SMCKey]
        [SMCIgnoreProp(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        [SMCReadOnly]
        [SMCOrder(0)]
        public override long Seq { get; set; }

        [SMCParameter(Name = "Description")] //Para passar a descrição do pai para o no filho (quando for novo)
        [SMCHidden(SMCViewMode.List | SMCViewMode.Filter)]
        [SMCInclude("NivelEnsinoSuperior")]
        [SMCMapProperty("NivelEnsinoSuperior.Descricao")]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid20_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid20_24)]
        [SMCOrder(1)]
        [SMCConditionalDisplay("SeqNivelEnsinoSuperior", SMCConditionalOperation.GreaterThen, 0)]
        public string NivelSuperior { get; set; }

        /// <summary>
        /// Tem que ter a propriedade que relaciona com o Nivel Superior e o SeqPai
        /// </summary>
        [SMCHidden]
        public long? SeqNivelEnsinoSuperior { get; set; }

        /// <summary>
        /// Tem que ter a propriedade que relaciona com o Nivel Superior e o SeqPai
        /// </summary>
        [SMCParameter()]
        [SMCHidden]
        public long? SeqPai
        {
            get { return SeqNivelEnsinoSuperior; }
            set { SeqNivelEnsinoSuperior = value; }
        }

        [SMCIgnoreProp(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCDescription]
        [SMCSize(SMCSize.Grid20_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid20_24)]
        [SMCRequired]
        [SMCMaxLength(100)]
        [SMCOrder(2)]
        public string Descricao { get; set; }

        [SMCIgnoreProp(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        [SMCRequired]
        [SMCMaxLength(5)]
        [SMCOrder(3)]
        public string Sigla { get; set; }

        [SMCIgnoreProp(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCSize(SMCSize.Grid9_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid9_24)]
        [SMCRequired]
        [SMCMaxLength(100)]
        [SMCRegularExpression(REGEX.TOKEN)]
        [SMCOrder(4)]
        public string Token { get; set; }

        [SMCIgnoreProp(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCSize(SMCSize.Grid9_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid9_24)]
        [SMCRequired]
        [SMCRadioButtonList]
        [SMCMapForceFromTo]
        [SMCOrder(5)]
        public bool ReconhecidoLDB { get; set; }

        [SMCSortable()]
        [SMCHidden]
        public short? Ordem { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .IgnoreFilterGeneration()
                   .Tokens(tokenList: UC_ORG_001_03_01.PESQUISAR_NIVEL_ENSINO,
                           tokenInsert: UC_ORG_001_03_02.MANTER_NIVEL_ENSINO,
                           tokenEdit: UC_ORG_001_03_02.MANTER_NIVEL_ENSINO,
                           tokenRemove: UC_ORG_001_03_02.MANTER_NIVEL_ENSINO);
        }
    }
}