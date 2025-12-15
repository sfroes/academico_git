using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class GrupoEscalonamentoListarItemViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqProcessoEtapa { get; set; }

        [SMCHidden]
        public long SeqGrupoEscalonamento { get; set; }

        [SMCHidden]
        public long SeqServico { get; set; }

        [SMCHidden]
        public string TokenServico { get; set; }

        [SMCHidden]
        public long SeqProcesso { get; set; }

        [SMCHidden]
        public long SeqEscalonamento { get; set; }

        [SMCHidden]
        public DateTime DataInicio { get; set; }

        [SMCHidden]
        public DateTime? DataFim { get; set; }

        [SMCHidden]
        public DateTime? DataEncerramento { get; set; }

        [SMCCssClass("smc-size-md-8 smc-size-xs-8 smc-size-sm-8 smc-size-lg-8")]
        public string DescricaoEtapa { get; set; }

        [SMCCssClass("smc-size-md-3 smc-size-xs-3 smc-size-sm-3 smc-size-lg-3")]
        public SituacaoEtapa SituacaoEtapa { get; set; }

        [SMCCssClass("smc-size-md-6 smc-size-xs-6 smc-size-sm-6 smc-size-lg-6")]
        public List<string> DescricaoEscalonamento
        {
            get
            {
                var retorno = new List<string>() { this.DataInicio.ToString() };
                if (this.DataFim.HasValue)
                {
                    ///Valida se o escalonamento está encerrado
                    if (this.DataEncerramento.HasValue)
                    {
                        retorno.Add($"{this.DataFim} - Encerrado");
                    }
                    else
                    {
                        retorno.Add(this.DataFim.ToString());
                    }
                }
                return retorno;
            }
        }

        [SMCCssClass("smc-size-md-3 smc-size-xs-3 smc-size-sm-3 smc-size-lg-3")]
        public int QuantidadeParcelas { get; set; }

        [SMCHidden]
        public bool SolicitacaoFinalizadaComSucesso { get; set; }

        [SMCHidden]
        public bool HabilitaBtnComPermissaoManutencaoProcesso { get; set; }
    }
}