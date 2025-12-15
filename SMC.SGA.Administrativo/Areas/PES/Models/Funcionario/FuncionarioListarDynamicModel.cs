using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.Util;
using System.Collections.Generic;
using SMC.SGA.Administrativo.Areas.DCT.Views.Colaborador.App_LocalResources;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class FuncionarioListarDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        public override long Seq { get; set; }
        public string Nome { get; set; }
        public string NomeSocial { get; set; }
        [SMCDescription]
        /// <summary>
        /// Nome segundo a regra RN_PES_023 - Nome e Nome Social - Visão Administrativo
        /// </summary>
        public string NomeFormatado => string.IsNullOrEmpty(NomeSocial) ? Nome : $"{NomeSocial} ({Nome})";
        public string Cpf { get; set; }
        public string NumeroPassaporte { get; set; }
        public string CpfOuPassaporte { get => string.IsNullOrEmpty(Cpf) ? NumeroPassaporte : SMCMask.ApplyMaskCPF(Cpf); }
        public bool Falecido { get; set; }
        public List<FuncionarioVinculoListaViewModel> VinculosAtivos { get; set; }
    }
}