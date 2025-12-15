using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.SRC.Controllers;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemViewModel : SMCViewModelBase
    {
        #region Datasource    

        public List<SMCDatasourceItem> TiposMensagem { get; set; }

        #endregion

        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCRequired]
        [SMCSelect(nameof(TiposMensagem))]
        [SMCSize(SMCSize.Grid12_24)]
        public long SeqTipoMensagem { get; set; }

        [SMCRequired]
        [SMCMultiline]
        [SMCHtml]
        [SMCDependency(nameof(SeqTipoMensagem), nameof(RealizarAtendimentoController.PreencherDescricaoMensagem), "RealizarAtendimento", true, includedProperties: new string[] { nameof(SeqInstituicaoEnsino), nameof(SeqInstituicaoNivel) })]
        [SMCSize(SMCSize.Grid24_24)]
        public string Descricao { get; set; }

        [SMCHidden]
        public DateTime DataInicioVigencia { get; set; }

        [SMCHidden]
        public long SeqSolicitacaoServico { get; set; }

        [SMCHidden]
        public bool ExisteDocumentoConclusao { get; set; }

        [SMCHidden]
        public string TokenTipoDocumentoAcademico { get; set; }

        [SMCHidden]
        public long? SeqTipoDocumentoSolicitado { get; set; }

        [SMCHidden]
        public int? NumeroVia { get; set; }

        [SMCHidden]
        public long SeqPessoaAtuacao { get; set; }

        [SMCHidden]
        public long SeqInstituicaoEnsino { get; set; }

        [SMCHidden]
        public long SeqInstituicaoNivel { get; set; }

        [SMCHidden]
        public bool? ReutilizarDados { get; set; }

        [SMCHidden]
        public string NomePais { get; set; }

        [SMCHidden]
        public string DescricaoViaAnterior { get; set; }

        [SMCHidden]
        public string DescricaoViaAtual { get; set; }

        [SMCHidden]
        public int? CodigoUnidadeSeo { get; set; }

        [SMCHidden]
        public string DescricaoGrauAcademico { get; set; }

        [SMCHidden]
        public string DocumentoAcademico { get; set; }
    }
}