using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class ProcessoListaVO : ISMCMappable
    {
        public long Seq { get; set; }
        public string Descricao { get; set; }

        [SMCMapProperty("Servico.TipoServico.Descricao")]
        public string DescricaoTipoServico { get; set; }

        [SMCMapProperty("Servico.Descricao")]
        public string DescricaoServico { get; set; }

        public List<ProcessoUnidadeResponsavelVO> UnidadesResponsaveis { get; set; }

        [SMCMapProperty("CicloLetivo.Descricao")]
        public string DescricaoCicloLetivo { get; set; }

        public DateTime? DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public DateTime? DataEncerramento { get; set; }

        [SMCMapProperty("Servico.Token")]
        public string TokenServico { get; set; }

        public List<ProcessoEtapaDetalheVO> Etapas { get; set; }

        public bool HabilitaBtnComPermissaoManutencaoProcesso { get; set; }

        public bool ExibeBtnEscalonamentosEtapa { get; set; }

        public bool ExibeBtnGrupoEscalonamento { get; set; }

        public bool HabilitaBtnGrupoEscalonamento { get;  set; }

        public bool ExibeBtnEncerrarProcesso { get; set; }

        public bool HabilitaBtnEncerrarProcesso { get; set; }

        public string InstructionEncerrarProcesso { get; set; }

        public bool HabilitaBtnExcluirProcesso { get; set; }

        public string InstructionExcluirProcesso { get; set; }

        public bool HabilitaBtnConfigurarEtapa { get; set; }

        public string InstructionConfigurarEtapa { get; set; }

        public bool HabilitaBtnCopiarProcesso { get; set; }

        public string InstructionCopiarProcesso { get; set; }
        public bool HabilitaBtnReabrirProcesso { get; set; }
    }
}