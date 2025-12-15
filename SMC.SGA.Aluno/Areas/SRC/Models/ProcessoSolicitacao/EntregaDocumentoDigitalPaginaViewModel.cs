using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.UI.Mvc.Areas.SRC.Models;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.SGA.Aluno.Areas.SRC.Models
{
    public class EntregaDocumentoDigitalPaginaViewModel : SolicitacaoServicoPaginaViewModelBase
    {
        #region Data Sources

        public List<SMCDatasourceItem> TiposDocumento { get; set; }

        #endregion

        [SMCHidden]
        public override string Token => TOKEN_SOLICITACAO_SERVICO.ENTREGA_DOCUMENTO_DIGITAL;

        public override string ChaveTextoBotaoProximo => "Botao_Confirmar";

        [SMCHidden]
        public long SeqSolicitacaoServicoAuxiliar { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public string Cpf { get; set; }

        [SMCSize(SMCSize.Grid7_24)]
        public string NomeAluno { get; set; }

        [SMCSize(SMCSize.Grid7_24)]
        public string NomeSocial { get; set; }

        [SMCSize(SMCSize.Grid3_24)]
        public Sexo Sexo { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        [SMCDateTimeMode(SMCDateTimeMode.Date)]
        public DateTime DataNascimento { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public string NumeroIdentidade { get; set; }

        [SMCSize(SMCSize.Grid2_24)]
        public string OrgaoEmissorIdentidade { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public string UfIdentidade { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public string NumeroPassaporte { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public string NomePaisEmissaoPassaporte { get; set; }

        [SMCSize(SMCSize.Grid7_24)]
        public string DescricaoNacionalidade { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public string Naturalidade { get; set; }

        public List<EntregaDocumentoDigitalFiliacaoPaginaViewModel> Filiacao { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        public List<string> FiliacaoDisplay
        {
            get
            {
                return Filiacao?.Select(f => $"{SMCEnumHelper.GetDescription(f.TipoParentesco)} - {f.Nome}").ToList() ?? new List<string>();
            }
        }

        public List<EntregaDocumentoDigitalDocumentoConclusaoPaginaViewModel> DocumentosConclusao { get; set; }

        [SMCSelect]
        [SMCRequired]
        [SMCSize(SMCSize.Grid6_24)]
        public bool? ConfirmacaoAluno { get; set; }

        [SMCMaxLength(255)]
        [SMCMultiline]
        [SMCConditionalDisplay(nameof(ConfirmacaoAluno), SMCConditionalOperation.Equals, false)]
        [SMCConditionalRequired(nameof(ConfirmacaoAluno), SMCConditionalOperation.Equals, false)]
        [SMCSize(SMCSize.Grid24_24)]
        public string Observacao { get; set; }

        [SMCDetail]
        [SMCConditionalDisplay(nameof(ConfirmacaoAluno), SMCConditionalOperation.Equals, false)]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<EntregaDocumentoDigitalUploadPaginaViewModel> DocumentosUpload { get; set; }
    }
}