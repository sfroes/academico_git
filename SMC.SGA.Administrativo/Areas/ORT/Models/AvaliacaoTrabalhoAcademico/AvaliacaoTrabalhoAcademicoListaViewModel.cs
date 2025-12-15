using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
    public class AvaliacaoTrabalhoAcademicoListaViewModel : SMCViewModelBase
    {
        #region [ Hidden ]

        [SMCHidden]
        [SMCKey]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqNivelEnsino { get; set; }

        [SMCHidden]
        public long SeqOrigemAvaliacao { get; set; }

        [SMCHidden]
        public bool PermitirAgendamentoBanca { get; set; }

        #endregion [ Hidden ] 

        [SMCReadOnly]
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid11_24, SMCSize.Grid24_24, SMCSize.Grid11_24, SMCSize.Grid13_24)]
        public string DescricaoComponenteCurricular { get; set; }

        [SMCOrder(1)]
        [SMCMaxLength(15)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid5_24)]
        [SMCReadOnly]
        public string DescricaoSituacaoAprovacao { get { return SituacaoHistoricoEscolar?.SMCGetDescription(); } }

        [SMCHidden]
        public bool PublicacaoBiblioteca { get; set; }

        [SMCHidden]
        public DateTime? DataAutorizacaoSegundoDeposito { get; set; }

        public SituacaoHistoricoEscolar? SituacaoHistoricoEscolar { get; set; }

        public List<AvaliacaoTrabalhoAcademicoAvaliacaoViewModel> Avaliacoes { get; set; }
    }
}