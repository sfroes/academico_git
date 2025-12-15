using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.TUR.Models
{
    public class TurmaColaboradorDivisaoOrganizacaoViewModel : SMCViewModelBase
    {
        /// <summary>
        /// Seq da Divisão Componente Organizacao (DIVISAO_COMPONENTE_ORGANIZACAO)
        /// </summary>
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqDivisaoTurmaColaborador { get; set; }

        [SMCDependency("SeqDivisao", "RecuperarTipoOrganizacaoComponente", "TurmaColaboradorDivisao", true)]
        [SMCOrder(1)]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid4_24)]
        public string DescricaoTipoOrganizacaoDivisao { get; set; }

        [SMCHidden]
        public long SeqDivisaoTurmaOrganizacao { get; set; }

        [SMCOrder(2)]
        [SMCRequired]
        [SMCSelect("OrganizacoesComponente", AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid13_24)]
        public long SeqDivisaoComponenteOrganizacao { get; set; }

        [SMCMask("999")]
        [SMCMinValue(1)]
        [SMCOrder(3)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid5_24)]
        public short? QuantidadeCargaHoraria { get; set; }

    }
}