using SMC.Academico.ServiceContract.Areas.CNC.Interfaces;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class FormacaoAcademicaListarDynamicModel : SMCDynamicViewModel
    {
        [SMCInclude("PessoaAtuacao.DadosPessoais")]
        [SMCMapProperty("PessoaAtuacao.DadosPessoais.Sexo")]
        [SMCHidden]
        public Sexo Sexo { get; set; }

        [SMCIgnoreProp]
        public bool SemCurso => true;

        [SMCKey]
        [SMCOrder(0)]
        [SMCHidden]
        public override long Seq { get; set; }

        public string DescricaoTitulacao => Sexo == Sexo.Masculino ? DescricaoMasculino : DescricaoFeminino;

        [SMCIgnoreProp]
        [SMCInclude("Titulacao")]
        [SMCMapProperty("Titulacao.DescricaoFeminino")]
        public string DescricaoFeminino { get; set; }

        [SMCIgnoreProp]
        [SMCInclude("Titulacao")]
        [SMCMapProperty("Titulacao.DescricaoMasculino")]
        public string DescricaoMasculino { get; set; }

        public string Descricao { get; set; }

        [SMCRadioButtonList]
        public bool? TitulacaoMaxima { get; set; }
    }
}