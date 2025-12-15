using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using SMC.SGA.Administrativo.Areas.CNC.Views.ConsultaPublica.App_LocalResources;
using System.Collections.Generic;
using SMC.Academico.Common.Areas.CNC.Enums;

namespace SMC.SGA.Administrativo.Areas.CNC.Models
{
    public class ConsultaPublicaViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCDisplay]
        [SMCSize(SMCSize.Grid12_24)]
        public string CodigoVerificacao { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCCssClass("smc-cpd-data")]
        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        public DateTime DataConsulta { get; set; }

        public long Seq { get; set; }

        public bool Valido { get; set; }

        [SMCDisplay]
        public string SituacaoDiploma { get; set; }

        [SMCHidden]
        public ClasseSituacaoDocumento ClasseSituacaoDocumento { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCCssClass("smc-cpd-cabecalho-conteudo-nome")]
        public string Nome { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCCssClass("smc-cpd-cabecalho-conteudo")]
        public string Cpf { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid6_24)]
        [SMCCssClass("smc-cpd-cabecalho-conteudo")]
        public int? CodigoCursoEMEC { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid18_24)]
        [SMCCssClass("smc-cpd-cabecalho-conteudo")]
        public string NomeCurso { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid12_24)]
        public string NomeCivil { get; set; }

        public string DescricaoTitulacao { get; set; }

        public string DescricaoTitulacaoXSD { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid12_24)]
        public string Titulacao { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid12_24)]
        public string DescricaoGrauAcademico { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCDateTimeMode(SMCDateTimeMode.Date)]
        public DateTime DataIngresso { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCDateTimeMode(SMCDateTimeMode.Date)]
        public DateTime? DataConclusao { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid12_24)]
        public string NumeroProcesso { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid12_24)]
        public string NumeroRegistro { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCDateTimeMode(SMCDateTimeMode.Date)]
        public DateTime? DataRegistro { get; set; }

        [SMCHidden]
        public DateTime? DataRegistroDOU { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCDateTimeMode(SMCDateTimeMode.Date)]
        public string DataRegistroDOUFormatado { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid4_24)]
        public int CodigoMEC { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid10_24)]
        [SMCCssClass("smc-cpd-nomeinstituicaoensino")]
        public string NomeInstituicaoEnsino { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid24_24)]
        public string Mantenedora { get; set; }

        public string Mensagem { get; set; }

        public string MensagemInformativaDiploma { get; set; }

        public string MensagemInformativaDiplomaAnuladoParteUm { get => UIResource.Mensagem_Informativa_Diploma_Anulado_ParteUm; }
        public string MensagemInformativaDiplomaAnuladoParteDois { get => UIResource.Mensagem_Informativa_Diploma_Anulado_ParteDois; }
        public string MensagemInformativaDiplomaAnuladoParteTres { get => UIResource.Mensagem_Informativa_Diploma_Anulado_ParteTres; }
        public string MensagemInformativaDiplomaAnuladoParteQuatro { get => UIResource.Mensagem_Informativa_Diploma_Anulado_ParteQuatro; }

        [SMCHidden]
        public bool ExibirApenasConsulta { get; set; }

        [SMCHidden]
        public bool HistoricoInvalido { get; set; }

        public List<ConsultaPublicaHistoricoViewModel> Historicos { get; set; }
    }
}