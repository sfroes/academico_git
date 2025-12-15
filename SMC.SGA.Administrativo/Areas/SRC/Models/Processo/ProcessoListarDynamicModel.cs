using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ProcessoListarDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCParameter]
        public override long Seq { get; set; }

        [SMCDescription]
        public string Descricao { get; set; }

        public string DescricaoTipoServico { get; set; }

        public string DescricaoServico { get; set; }

        [SMCValueEmpty("-")]
        public string DescricaoCicloLetivo { get; set; }

        public List<ProcessoUnidadeResponsavelViewModel> UnidadesResponsaveis { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        public DateTime? DataInicio { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        [SMCValueEmpty("-")]
        public DateTime? DataFim { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        [SMCValueEmpty("-")]
        public DateTime? DataEncerramento { get; set; }

        [SMCIgnoreProp]
        public string TokenServico { get; set; }

        public List<ProcessoEtapaDetalheViewModel> Etapas { get; set; }

        [SMCHidden]
        public bool HabilitaBtnComPermissaoManutencaoProcesso { get; set; }

        [SMCHidden]
        public bool ExibeBtnEscalonamentosEtapa { get; set; }

        [SMCHidden]
        public bool ExibeBtnGrupoEscalonamento { get; set; }

        [SMCHidden]
        public bool HabilitaBtnGrupoEscalonamento { get; set; }

        [SMCHidden]
        public bool ExibeBtnEncerrarProcesso { get; set; }

        [SMCHidden]
        public bool HabilitaBtnEncerrarProcesso { get; set; }

        [SMCHidden]
        public string InstructionEncerrarProcesso { get; set; }

        [SMCHidden]
        public bool HabilitaBtnExcluirProcesso { get; set; }

        [SMCHidden]
        public string InstructionExcluirProcesso { get; set; }

        [SMCHidden]
        public bool HabilitaBtnConfigurarEtapa { get; set; }

        [SMCHidden]
        public string InstructionConfigurarEtapa { get; set; }

        [SMCHidden]
        public bool HabilitaBtnCopiarProcesso { get; set; }
        [SMCHidden]
        public string InstructionCopiarProcesso { get; set; }

        [SMCHidden]
        public bool HabilitaBtnReabrirProcesso { get; set; }
    }
}