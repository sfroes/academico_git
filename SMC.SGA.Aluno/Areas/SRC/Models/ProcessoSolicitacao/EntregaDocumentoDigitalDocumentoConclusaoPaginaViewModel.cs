using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Aluno.Areas.SRC.Models
{
    public class EntregaDocumentoDigitalDocumentoConclusaoPaginaViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public bool DiplomaDigital { get; set; }

        [SMCHidden]
        [SMCKey]
        public long SeqDocumentoConclusao { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public string DescricaoCurso { get; set; }

        [SMCValueEmpty("-")]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCConditionalDisplay(nameof(ExibirGrauTitulacao), SMCConditionalOperation.Equals, true)]
        public string DescricaoGrauAcademico { get; set; }

        [SMCValueEmpty("-")]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCConditionalDisplay(nameof(ExibirGrauTitulacao), SMCConditionalOperation.Equals, true)]
        public string DescricaoTitulacao { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        [SMCDateTimeMode(SMCDateTimeMode.Date)]
        [SMCConditionalDisplay(nameof(DiplomaDigital), SMCConditionalOperation.Equals, true)]
        public DateTime? DataColacaoGrau { get; set; }

        [SMCSize(SMCSize.Grid2_24)]
        [SMCValueEmpty("-")]
        [SMCConditionalDisplay(nameof(DiplomaDigital), SMCConditionalOperation.Equals, true)]
        public string NumeroVia { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        public string CodigoValidacaoDiploma { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        public string CodigoValidacaoHistorico { get; set; }

        [SMCHidden]
        public string NomeDiplomado { get; set; }

        [SMCHidden]
        public string UrlConsultaPublicaDiploma { get; set; }

        [SMCHidden]
        public string UrlConsultaPublicaHistorico { get; set; }

        [SMCHidden]
        public bool ExibirGrauTitulacao { get; set; }

        public List<EntregaDocumentoDigitalFormacaoPaginaViewModel> FormacoesEspecificas { get; set; }
    }
}