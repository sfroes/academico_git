using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Lookups
{
    public class GrupoEscalonamentoLookupFiltroViewModel : SMCLookupFilterViewModel
    {
        #region [ DataSources ]

        [SMCIgnoreProp]
        public List<SMCDatasourceItem> Processos { get; set; }

        #endregion [ DataSources ]

        [SMCKey]
        [SMCHidden]
        public long? Seq { get; set; }

        [SMCHidden]
        public bool? GrupoEscalonamentoAtivo { get; set; }
       
        [SMCHidden]
        public bool? Ativo { get { return GrupoEscalonamentoAtivo; } }

        [SMCHidden]
        public bool SeqProcessoSomenteLeitura { get; set; }

        [SMCSize(SMCSize.Grid10_24)]
        [SMCSelect(nameof(Processos))]
        [SMCConditionalReadonly(nameof(SeqProcessoSomenteLeitura), true, PersistentValue = true)]
        public long? SeqProcesso { get; set; }

        // Foi necessário fazer essa propriedade pois onde foi utilizado o lookup já existia a propriedade SeqProcesso com um valor diferente do que era necessário mandar para o filtro.
        // Desta maneira, foi criado o SeqProcessoEscalonamentoReabertura na tela e passado como dependência pro lookup
        // SMC.SGA.Administrativo\Areas\SRC\Models\RealizarAtendimento\AtendimentoReaberturaViewModel.cs
        [SMCHidden]
        public long? SeqProcessoEscalonamentoReabertura
        {
            get { return SeqProcesso; }
            set { SeqProcesso = value; }
        }

        [SMCDescription]
        [SMCSize(SMCSize.Grid10_24)]
        public string Descricao { get; set; }

        [SMCHidden]
        public bool DisparaExcecao { get; set; }
    }
}