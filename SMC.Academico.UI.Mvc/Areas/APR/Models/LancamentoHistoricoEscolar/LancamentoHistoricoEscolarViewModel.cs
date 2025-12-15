using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.UI.Mvc.Areas.APR.Models
{
    public class LancamentoHistoricoEscolarViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public string BackUrl { get; set; }

        [SMCHidden]
        public long SeqTurma { get; set; }

        [SMCHidden]
        public long SeqOrigemAvaliacao { get; set; }

        [SMCHidden]
        public bool PermiteEmitirRelatorio { get; set; }

        [SMCHidden]
        public bool HabilitaSalvar { get; set; }

        [SMCHidden]
        public bool HabilitaFecharDiario { get; set; }

        [SMCHidden]
        public bool AlunosPendentes { get; set; }

        [SMCHidden]
        public bool SomenteEscalaApuracao { get; set; }

        [SMCHidden]
        [SMCDataSource]
        public List<SMCDatasourceItem> ListaEscalaApuracaoItens { get; internal set; }

        [SMCHidden]
        public bool MostrarLancamentosNota { get; set; }

        [SMCHidden]
        public bool MostrarLancamentosNotaDescricao { get; set; }

        [SMCHidden]
        public bool MostrarLancamentosSeqEscalaApuracaoItem { get; set; }

        [SMCHidden]
        public bool MostrarLacamentosSemNota { get; set; }

        [SMCHidden]
        public bool MateriaLecionadaObrigatoria { get; set; }

        /// <summary>
        /// Necessário para tratar a view.
        /// </summary>
        public bool TurmaSemAluno { get; set; }

        [SMCMultiline(Rows = 5)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCConditionalReadonly(nameof(HabilitaSalvar), SMCConditionalOperation.Equals, false, PersistentValue = true)]
        public string MateriaLecionada { get; set; }

        [SMCHidden]
        public bool ExibirCampoCargaHorariaRealizada => Lancamentos?.Any(l => l.CargaHorariaRealizada.HasValue && l.CargaHoraria != l.CargaHorariaRealizada) ?? false;

        [SMCSize(SMCSize.Grid24_24)]
        public List<HistoricoEscolarNotaViewModel> Lancamentos { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        public string NotaMaxima
        {
            get
            {
                if (Lancamentos != null && Lancamentos.Count > 0)
                    return Lancamentos.First().NotaMaxima?.ToString();
                return null;
            }
        }

        [SMCSize(SMCSize.Grid24_24)]
        public bool SomenteLeitura
        {
            get
            {
                if (Lancamentos != null && Lancamentos.Count > 0)
                    return Lancamentos.Any(a => a.SomenteLeitura);

                return false;
            }
        }

        #region Suporte dicionários javascript

        [SMCHidden]
        public string EscalaApuracao { get; set; }

        [SMCHidden]
        public string SituacoesFinais { get; set; }

        #endregion Suporte dicionários javascript
    }
}