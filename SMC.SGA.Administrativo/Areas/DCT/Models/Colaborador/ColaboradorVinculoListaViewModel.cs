using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.DCT.Views.Colaborador.App_LocalResources;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.DCT.Models
{
    public class ColaboradorVinculoListaViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCIgnoreProp]
        public string NomeEntidadeVinculo { get; set; }

        [SMCIgnoreProp]
        public string DescricaoTipoVinculoColaborador { get; set; }

        [SMCIgnoreProp]
        public DateTime DataInicio { get; set; }

        [SMCIgnoreProp]
        public DateTime? DataFim { get; set; }

        [SMCIgnoreProp]
        public bool InseridoPorCarga { get; set; }

        public List<ColaboradorVinculoCursoListarViewModel> Cursos { get; set; }

        [SMCDescription]
        [SMCSize(Framework.SMCSize.Grid24_24)]
        public string Descricao => DataFim.HasValue ?
            string.Format(UIResource.MascaraVinculoPeriodo, NomeEntidadeVinculo, DescricaoTipoVinculoColaborador, DataInicio, DataFim) :
                   string.Format(UIResource.MascaraVinculo, NomeEntidadeVinculo, DescricaoTipoVinculoColaborador, DataInicio);
    }
}