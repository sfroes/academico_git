using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.Util;
using System;

namespace SMC.SGA.Aluno.Areas.SRC.Models
{
    public class SolicitacaoServicoItemListaViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long SeqSolicitacao { get; set; }

        [SMCHidden]
        public long SeqConfiguracaoEtapa { get; set; }

        [SMCHidden]
        public DateTime DataInicio { get; set; }

        [SMCHidden]
        public DateTime? DataFim { get; set; }

        [SMCHidden]
        public bool ExibirVisualizarPlanoEstudos { get; set; }

        public string DescricaoEtapa { get; set; }

        public string Instrucoes { get; set; }

        public string SituacaoEtapa { get; set; }

        public string DataVigencia
        {
            get
            {
                if (DataInicio == default(DateTime))
                    return "-";
                else if (DataFim.HasValue)
                    return DataInicio.ToString("dd/MM/yyyy HH:mm") + " a " + DataFim.Value.ToString("dd/MM/yyyy HH:mm");
                else
                    return "a partir de " + DataInicio.ToString("dd/MM/yyyy HH:mm");
            }
            set
            {
            }
        }


    }
}