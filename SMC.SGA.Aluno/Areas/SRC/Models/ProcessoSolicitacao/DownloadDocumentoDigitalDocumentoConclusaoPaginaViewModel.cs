using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Aluno.Areas.SRC.Views.ProcessoSolicitacao.App_LocalResources;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Aluno.Areas.SRC.Models
{
    public class DownloadDocumentoDigitalDocumentoConclusaoPaginaViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public bool DiplomaDigital { get; set; }

        [SMCHidden]
        [SMCKey]
        public long SeqDocumentoConclusao { get; set; }

        [SMCHidden]
        public long SeqSolicitacaoServico { get; set; }

        [SMCHidden]
        public long SeqConfiguracaoEtapa { get; set; }

        [SMCHidden]
        public ClasseSituacaoDocumento CategoriaSituacaoDocumentoAcademicoAtual { get; set; }

        [SMCHidden]
        public string TituloSecao { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        public string CodigoValidacaoDiploma { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        public string CodigoValidacaoHistorico { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        public string DescricaoCurso { get; set; }

        [SMCHidden]
        public long? SeqGrauAcademico { get; set; }

        [SMCValueEmpty("-")]
        [SMCSize(SMCSize.Grid6_24)]
        [SMCConditionalDisplay(nameof(ExibirGrauTitulacao), SMCConditionalOperation.Equals, true)]
        public string DescricaoGrauAcademico { get; set; }

        [SMCValueEmpty("-")]
        [SMCSize(SMCSize.Grid6_24)]
        [SMCConditionalDisplay(nameof(ExibirGrauTitulacao), SMCConditionalOperation.Equals, true)]
        public string DescricaoTitulacao { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        public string NumeroRegistro { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        [SMCDateTimeMode(SMCDateTimeMode.Date)]
        public DateTime? DataRegistro { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        public string NumeroProcesso { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        [SMCDateTimeMode(SMCDateTimeMode.Date)]
        public DateTime? DataColacao { get; set; }

        [SMCValueEmpty("-")]
        [SMCSize(SMCSize.Grid6_24)]
        [SMCConditionalDisplay(nameof(ExibirFormacao), SMCConditionalOperation.Equals, true)]
        public string FormacaoEspecifica { get; set; }

        [SMCHidden]
        public bool ExibirGrauTitulacao { get; set; }

        [SMCHidden]
        public bool ExibirFormacao { get; set; }

        [SMCHidden]
        public string NomeDiplomado { get; set; }

        [SMCHidden]
        public string UrlConsultaPublicaDiploma { get; set; }

        [SMCHidden]
        public string UrlConsultaPublicaHistorico { get; set; }
    }
}