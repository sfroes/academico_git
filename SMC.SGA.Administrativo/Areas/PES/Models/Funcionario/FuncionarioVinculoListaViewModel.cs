using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.PES.Views.Funcionario.App_LocalResources;
using System;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class FuncionarioVinculoListaViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }
        [SMCIgnoreProp]
        public long SeqFuncionario { get; set; }
        [SMCIgnoreProp]
        public long SeqTipoFuncionario { get; set; }
        [SMCIgnoreProp]
        public string DescricaoTipoFuncionario { get; set; }

        [SMCDescription]
        [SMCSize(SMCSize.Grid12_24)]
        public string DescricaoEntidadeCadastrada { get; set; }

        [SMCIgnoreProp]
        public DateTime DataInicio { get; set; }
        [SMCIgnoreProp]
        public DateTime? DataFim { get; set; }

        [SMCDescription]
        [SMCSize(SMCSize.Grid24_24)]
        public string Descricao => DataFim.HasValue ?
            string.Format(UIResource.MascaraVinculoPeriodo, DescricaoTipoFuncionario, DataInicio, DataFim) :
                   string.Format(UIResource.MascaraVinculo, DescricaoTipoFuncionario, DataInicio);

    }
}