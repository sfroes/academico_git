using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Models
{
    public class EtapaListaViewModel : SMCViewModelBase
    {
        [SMCGridLegend]
        [SMCOrder(0)]
        public SituacaoEtapaSolicitacaoMatricula SituacaoEtapaIngressante { get; set; }

        [SMCOrder(1)]
        public string DescricaoEtapa { get; set; }

        [SMCOrder(3)]
        public string DataVigencia
        {
            get
            {
                if (DataFim.HasValue)
                    return DataInicio.ToString("dd/MM/yyyy HH:mm") + " a " + DataFim.Value.ToString("dd/MM/yyyy HH:mm");
                else
                    return "a partir de " + DataInicio.ToString("dd/MM/yyyy HH:mm");
            }
            set
            {
            }
        }

        [SMCHidden]
        public DateTime DataInicio { get; set; }

        [SMCHidden]
        public DateTime? DataFim { get; set; }

        [SMCHidden]
        public long SeqEtapaSGF { get; set; }

        [SMCHidden]
        public long SeqProcessoEtapa { get; set; }

        [SMCHidden]
        public long SeqConfiguracaoProcesso { get; set; }

        [SMCHidden]
        public long? SeqGrupoEscalonamento { get; set; }

        [SMCHidden]
        public long? SeqEscalonamento { get; set; }

        [SMCHidden]
        public bool Ativo { get; set; }

        [SMCHidden]
        public bool ExibirVisualizarPlanoEstudos { get; set; }

        [SMCHidden]
        public bool PossuiFluxoNaAplicacaoSGAAluno { get; set; }

        public string Instrucoes { get; set; }

        [SMCHidden]
        public long SeqSolicitacaoServico { get; set; }

        public long SeqConfiguracaoEtapa { get; set; }

        public long SeqTemplateProcessoSGF { get; set; }

        public List<EtapaSituacaoViewModel> Situacoes { get; set; }

        public List<EtapaPaginaViewModel> Paginas { get; set; }

        public int OrdemEtapaSGF { get; set; }
    }
}