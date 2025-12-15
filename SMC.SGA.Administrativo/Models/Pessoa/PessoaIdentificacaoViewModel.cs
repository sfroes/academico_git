using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using System;

namespace SMC.SGA.Administrativo.Models
{
    public class PessoaIdentificacaoViewModel : SMCViewModelBase
    {
        [SMCImage(thumbnailHeight: 100, thumbnailWidth: 0, manualUpload: false, maxFileSize: 225651471, AreaDownload = "", ActionDownload = "DownloadFileGuid", ControllerDownload = "Home")]
        public SMCUploadFile ArquivoFoto { get; set; }

        public long? SeqArquivoFoto { get; set; }
        public DateTime? DataNascimento { get; set; }
        public bool Falecido { get; set; }
        public EstadoCivil? EstadoCivil { get; set; }
        public Sexo Sexo { get; set; }
        public RacaCor? RacaCor { get; set; }

        [SMCCpf]
        public string Cpf { get; set; }

        public TipoNacionalidade TipoNacionalidade { get; set; }

        [SMCHidden]
        public int CodigoPaisNacionalidade { get; set; }

        public string PaisNacionalidade { get; set; }

        [SMCConditionalDisplay("Identificacao_CodigoPaisNacionalidade", SMCConditionalOperation.Equals, 800)]
        public string UfNaturalidade { get; set; }

        public int? CodigoCidadeNaturalidade { get; set; }

        [SMCConditionalDisplay("Identificacao_CodigoPaisNacionalidade", SMCConditionalOperation.Equals, 800)]
        public string CidadeNaturalidade { get; set; }

        [SMCConditionalDisplay("Identificacao_CodigoPaisNacionalidade", SMCConditionalOperation.NotEqual, 800)]
        public string DescricaoNaturalidadeEstrangeira { get; set; }

        public string NumeroPassaporte { get; set; }
        public DateTime? DataValidadePassaporte { get; set; }
        public int? CodigoPaisEmissaoPassaporte { get; set; }
        public string PaisEmissaoPassaporte { get; set; }
        public string NumeroIdentidade { get; set; }
        public string OrgaoEmissorIdentidade { get; set; }
        public string UfIdentidade { get; set; }
        public DateTime? DataExpedicaoIdentidade { get; set; }
        public string NumeroRegistroCnh { get; set; }
        public CategoriaCnh? CategoriaCnh { get; set; }
        public DateTime? DataEmissaoCnh { get; set; }
        public DateTime? DataVencimentoCnh { get; set; }
        public string NumeroTituloEleitor { get; set; }
        public string NumeroZonaTituloEleitor { get; set; }
        public string NumeroSecaoTituloEleitor { get; set; }
        public string UfTituloEleitor { get; set; }
        public TipoPisPasep? TipoPisPasep { get; set; }
        public string NumeroPisPasep { get; set; }
        public DateTime? DataPisPasep { get; set; }
        public string NumeroDocumentoMilitar { get; set; }
        public string CsmDocumentoMilitar { get; set; }
        public TipoDocumentoMilitar? TipoDocumentoMilitar { get; set; }
        public string UfDocumentoMilitar { get; set; }
        public bool NecessidadeEspecial { get; set; }
        public TipoNecessidadeEspecial? TipoNecessidadeEspecial { get; set; }
        public SMCMasterDetailList<PessoaFiliacaoViewModel> Filiacao { get; set; }
    }
}