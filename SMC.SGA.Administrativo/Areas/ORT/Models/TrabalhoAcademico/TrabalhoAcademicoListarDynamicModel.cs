using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.ORT.Constants;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
    public class TrabalhoAcademicoListarDynamicModel : SMCDynamicViewModel
    {
        [SMCSortable]
        [SMCKey]
        public override long Seq { get; set; }

        public string TipoTrabalho { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.Date)]
        public DateTime? DataInicioAplicacaoAvaliacao { get; set; }

        public int? NumeroDefesa { get; set; }

        public string Titulo { get; set; }

        public DateTime? DataAutorizacaoSegundoDeposito { get; set; }

        public string JustificativaSegundoDeposito { get; set; }

        public string UsuarioInclusaoSegundoDeposito { get; set; }

        public DateTime DataInclusaoSegundoDeposito { get; set; }

        [SMCFile(AreaDownload = "", ActionDownload = "DownloadFileGuid", ControllerDownload = "Home", HideDescription = true)]
        public SMCUploadFile ArquivoAnexadoSegundoDeposito { get; set; }

        public List<string> Autores { get; set; }

		public List<TrabalhoAcademicoAutorListaViewModel> Alunos { get; set; }

		public List<string> NomesAutores { get; set; }

        [SMCHidden]
        public DateTime? Data { get; set; }

        [SMCHidden]
        public bool GeraFinanceiroEntregaTrabalho { get; set; }

        [SMCHidden]
        public bool ApuracaoReprovada { get { return this.SituacaoHistoricoEscolar == Academico.Common.Areas.APR.Enums.SituacaoHistoricoEscolar.Reprovado; } }

        [SMCHidden]
        public bool PublicacaoBibliotecaObrigatoria { get; set; }

        [SMCHidden]
        public SituacaoHistoricoEscolar? SituacaoHistoricoEscolar { get; set; }
        public string ProtocoloSolicitacaoInclusao { get; set; }

    }
}