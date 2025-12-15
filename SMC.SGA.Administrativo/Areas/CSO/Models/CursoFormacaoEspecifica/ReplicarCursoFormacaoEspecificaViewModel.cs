using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class ReplicarCursoFormacaoEspecificaViewModel : SMCViewModelBase
    {
        #region DataSources

        public List<SMCDatasourceItem> CursosOfertasLocalidades { get; set; }

        #endregion

        [SMCHidden]
        public long SeqCurso { get; set; }

        [SMCHidden]
        public long SeqFormacaoEspecifica { get; set; }

        [SMCRequired]
        [SMCSelect(nameof(CursosOfertasLocalidades), UseCustomSelect = true)]
        [SMCSize(SMCSize.Grid12_24)]
        public List<long> SeqsCursosOfertasLocalidades { get; set; }

        public string Mensagem { get; set; }
    }
}