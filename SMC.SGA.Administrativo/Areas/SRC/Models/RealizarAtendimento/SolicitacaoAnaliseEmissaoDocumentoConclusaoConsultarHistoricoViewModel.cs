using SMC.Academico.ServiceContract.Areas.CNC.Data.SolicitacaoDocumentoConclusao;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarHistoricoViewModel : SMCViewModelBase
    {
        #region Campos cabeçalho

        [SMCHidden]
        public bool EmissaoDiplomaDigital1Via { get; set; }

        [SMCValueEmpty("-")]
        public string DescricaoSituacaoAtualMatricula { get; set; }

        [SMCValueEmpty("-")]
        public string DescricaoFormaIngresso { get; set; }

        [SMCConditionalDisplay(nameof(EmissaoDiplomaDigital1Via), SMCConditionalOperation.Equals, false)]
        [SMCValueEmpty("-")]
        public string DescricaoTipoHistorico { get; set; }

        [SMCValueEmpty("-")]
        [SMCDateTimeMode(SMCDateTimeMode.Date)]
        public DateTime? DataIngresso { get; set; }

        [SMCValueEmpty("-")]
        [SMCDateTimeMode(SMCDateTimeMode.Date)]
        public DateTime? DataConclusao { get; set; }

        [SMCValueEmpty("-")]
        [SMCDateTimeMode(SMCDateTimeMode.Date)]
        public DateTime? DataColacao { get; set; }

        [SMCValueEmpty("-")]
        public string CodigoCurriculo { get; set; }

        [SMCValueEmpty("-")]
        public double CargaHorariaCurso { get; set; }

        [SMCValueEmpty("-")]
        public string CargaHorariaCursoFormatada
        {
            get
            {
                return string.Format("{0:n2}", CargaHorariaCurso);
            }
        }

        [SMCValueEmpty("-")]
        public double CargaHorariaCursoIntegralizada { get; set; }

        [SMCValueEmpty("-")]
        public string CargaHorariaCursoIntegralizadaFormatada
        {
            get
            {
                return string.Format("{0:n2}", CargaHorariaCursoIntegralizada);
            }
        }

        #endregion

        [SMCHidden]
        public long SeqSolicitacaoServico { get; set; }

        [SMCHidden]
        public int? NumeroVia { get; set; }

        public string MensagemInformativa { get; set; }

        [SMCHidden]
        public int? CodigoCursoOfertaLocalidade { get; set; }

        public List<SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarEnadeViewModel> Enade { get; set; }
    }
}