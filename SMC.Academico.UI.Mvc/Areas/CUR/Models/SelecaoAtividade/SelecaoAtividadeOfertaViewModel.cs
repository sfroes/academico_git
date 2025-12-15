using SMC.Academico.Common.Areas.MAT.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.CUR.Models
{
    public class SelecaoAtividadeOfertaViewModel : SMCViewModelBase, ISMCSeq
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqProcessoEtapa { get; set; }

        [SMCHidden]
        public long? SeqSolicitacaoMatriculaItem { get; set; }

        [SMCHidden]
        public long SeqConfiguracaoComponente { get; set; }

        [SMCHidden]
        public string Codigo { get; set; }

        public string DescricaoFormatada { get; set; }

        [SMCHidden]
        public bool PreRequisito { get; set; }

        [SMCHidden]
        public bool ObrigatorioOrientador { get; set; }

        [SMCHidden]
        public List<long> SeqsAtividadeCoRequisitos { get; set; }

        //Colocado a legenda por questão de layout porque a atividade sempre pertence a matriz curricular
        public TurmaOfertaMatricula Pertence { get { return TurmaOfertaMatricula.ComponentePertence; } }
    }
}
