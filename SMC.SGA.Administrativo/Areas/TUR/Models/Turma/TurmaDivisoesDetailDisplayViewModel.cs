using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.TUR.Models
{
    public class TurmaDivisoesDetailDisplayViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqTurma { get; set; }

        [SMCHidden]
        public string Turma { get; set; }

        [SMCHidden]
        public string DivisaoDescricao { get; set; }

        [SMCHidden]
        public string GrupoNumero { get; set; }

        [SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        public string CodigoCompleto { get { return $"{Turma}.{DivisaoDescricao}.{GrupoNumero}"; } }

        [SMCHidden]
        [SMCSelect(nameof(TurmaDynamicModel.DivisoesLocalidades), StorageType = SMCStorageType.TempData, NameDescriptionField = nameof(DescricaoLocalidade))]
        public long SeqLocalidade { get; set; }

        [SMCCssClass("smc-size-md-7 smc-size-xs-7 smc-size-sm-7 smc-size-lg-7")]
        [SMCValueEmpty("-")]
        public string DescricaoLocalidade { get; set; }

        [SMCCssClass("smc-size-md-3 smc-size-xs-3 smc-size-sm-3 smc-size-lg-3")]
        [SMCValueEmpty("-")]
        public short? DivisaoVagas { get; set; }

        [SMCCssClass("smc-size-md-3 smc-size-xs-3 smc-size-sm-3 smc-size-lg-3")]
        [SMCValueEmpty("0")]
        public short? QuantidadeVagasOcupadas { get; set; }

        [SMCCssClass("smc-size-md-10 smc-size-xs-10 smc-size-sm-10 smc-size-lg-10")]
        [SMCValueEmpty("-")]
        public string InformacoesAdicionais { get; set; }

        [SMCHidden]
        public bool HabilitaBtnConfiguracaoGrade { get; set; }

        [SMCHidden]
        public string InstructionConfiguracaoGrade { get; set; }

        [SMCHidden]
        public bool HabilitaBtnListaFrequencia { get; set; }

        [SMCHidden]
        public string InstructionListaFrequencia { get; set; }

        [SMCHidden]
        public bool HabilitaBtnLancarFrequencia { get; set; }

        [SMCHidden]
        public string InstructionLancarFrequencia { get; set; }

        [SMCHidden]
        public bool HabilitaBtnOrientacaoTurma { get; set; }

        [SMCHidden]
        public string InstructionOrientacaoTurma { get; set; }

        [SMCHidden]
        public long SeqOrigemAvaliacao { get; set; }
    }
}