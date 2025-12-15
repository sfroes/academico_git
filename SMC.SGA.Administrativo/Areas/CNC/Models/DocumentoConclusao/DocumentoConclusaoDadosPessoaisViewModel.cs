using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.CNC.Models
{
    public class DocumentoConclusaoDadosPessoaisViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqPessoa { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid5_24)]
        public string Nome { get; set; }

        [SMCDisplay]
        [SMCValueEmpty("-")]
        [SMCSize(SMCSize.Grid5_24)]
        public string NomeSocial { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid3_24)]
        public Sexo Sexo { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid3_24)]
        [SMCDateTimeMode(SMCDateTimeMode.Date)]
        public DateTime DataNascimento { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid3_24)]
        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        public DateTime DataInclusao { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid4_24)]
        public string UsuarioInclusao { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid4_24)]
        public string NumeroIdentidade { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid3_24)]
        public string OrgaoEmissorIdentidade { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid2_24)]
        public string UfIdentidade { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid3_24)]
        [SMCDateTimeMode(SMCDateTimeMode.Date)]
        public DateTime? DataExpedicaoIdentidade { get; set; }

        [SMCHidden]
        public TipoNacionalidade TipoNacionalidade { get; set; }

        [SMCHidden]
        public string UfNaturalidade { get; set; }

        [SMCHidden]
        public int? CodigoCidadeNaturalidade { get; set; }

        [SMCHidden]
        public string DescricaoNaturalidadeEstrangeira { get; set; }

        [SMCHidden]
        public int CodigoPaisNacionalidade { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid5_24)]
        public string Nacionalidade { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid5_24)]
        public string Naturalidade { get; set; }
    }
}