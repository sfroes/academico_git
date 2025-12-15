using SMC.Academico.Common.Areas.PES.Enums;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Models
{
    /// <summary>
    /// Representa um cadastro de pessoa utilizado na listagem de pessoas existentes dos cadastros de atuação
    /// </summary>
    public class PessoaExistenteListaViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public TipoNacionalidade TipoNacionalidade { get; set; }

        [SMCHidden]
        public int CodigoPaisNacionalidade { get; set; }

        [SMCHidden]
        public Sexo Sexo { get; set; }

        [SMCHidden]
        public bool Selecionado { get; set; }

        [SMCCssClass("smc-size-md-7 smc-size-xs-7 smc-size-sm-7 smc-size-lg-7")]
        public string Nome { get; set; }

        [SMCCssClass("smc-size-md-3 smc-size-xs-3 smc-size-sm-3 smc-size-lg-3")]
        public DateTime? DataNascimento { get; set; }

        [SMCCpf]
        [SMCCssClass("smc-size-md-3 smc-size-xs-3 smc-size-sm-3 smc-size-lg-3")]
        public string Cpf { get; set; }

        [SMCCssClass("smc-size-md-3 smc-size-xs-3 smc-size-sm-3 smc-size-lg-3")]
        public string NumeroPassaporte { get; set; }

        [SMCCssClass("smc-size-md-8 smc-size-xs-8 smc-size-sm-8 smc-size-lg-8")]
        public List<string> Filiacao { get; set; }

        [SMCHidden]
        [SMCIgnoreProp]
        public List<TipoAtuacao> Atuacoes { get; set; }
    }
}