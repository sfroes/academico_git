using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using SMC.SGA.BDP.Views.Home.App_LocalResources;

namespace SMC.SGA.BDP.Models
{
    public class ResultadoViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }

        [SMCSortable(true, true, SMCSortDirection.Ascending)]
        public DateTime? Data { get; set; }

        [SMCSortable(true, true, SMCSortDirection.Ascending)]
        public string Titulo { get; set; }

        public string TituloPortugues { get; set; }

        public List<string> Autores { get; set; }

        public string AutoresDisplay => Autores.SMCAny() ? string.Join("; ", Autores) : "-";

        public List<string> Orientadores { get; set; }

        public string OrientadoresDisplay => Orientadores.SMCAny() ? string.Join("; ", Orientadores) : "-";

        public List<string> Coorientadores { get; set; }

        public string CoorientadoresDisplay => Coorientadores.SMCAny() ? string.Join("; ", Coorientadores) : "-";

        public long SeqTipoTrabalho { get; set; }
        
        public string DescricaoTipoTrabalho { get; set; }

        public bool ExibirCoorientador { get; set; }

        public string DataDefesa => Data.GetValueOrDefault().ToShortDateString();

        public string DataPrevistaDefesa => Data.GetValueOrDefault().ToShortDateString();

        public string DescricaoNivelEnsino { get; set; }

        public string TituloModal => $"{DescricaoTipoTrabalho}  de  {DescricaoNivelEnsino}";

        public string ResumoPortugues { get; set; }

        #region campos hidden para ordenação

        [SMCHidden]
        [SMCSortable(true, true, SMCSortDirection.Ascending)]
        public string Autor { get; set; }

        [SMCHidden]
        [SMCSortable(true, true, SMCSortDirection.Ascending)]
        public string Orientador { get; set; }

        #endregion

    }
}
