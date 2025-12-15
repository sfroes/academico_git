using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Framework.UI.Mvc;
using System;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class SolicitacaoAnaliseEmissaoDocumentoConclusaoCamposModaisViewModel : SMCViewModelBase
    {
        public long SeqSolicitacaoServico { get; set; }

        public TipoIdentidade? TipoIdentidade { get; set; }

        public bool ExibirComandoDocumentacaoAcademica { get; set; }

        public bool ExibirComandoRVDD { get; set; }

        public long SeqCursoOfertaLocalidade { get; set; }

        public long? SeqGrauAcademico { get; set; }

        public DateTime? DataConclusao { get; set; }
    }
}