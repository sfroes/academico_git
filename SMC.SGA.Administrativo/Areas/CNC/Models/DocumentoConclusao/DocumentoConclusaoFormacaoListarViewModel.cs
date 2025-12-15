using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CNC.Models
{
    public class DocumentoConclusaoFormacaoListarViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqDocumentoConclusao { get; set; }

        [SMCHidden]
        public long SeqAlunoFormacao { get; set; }

        [SMCHidden]
        public long? SeqDocumentoConclusaoApostilamento { get; set; }

        public long NumeroRegistroAcademico { get; set; }

        public string DescricaoNivelEnsino { get; set; }

        public string DescricaoCursoOferta { get; set; }

        [SMCHidden]
        public long SeqAluno { get; set; }

        public List<string> FormacoesEspecificas { get; set; }

        [SMCValueEmpty("-")]
        public DateTime? DataColacaoGrau { get; set; }

        [SMCValueEmpty("-")]
        public DateTime? DataConclusao { get; set; }

        public string DescricaoGrauAcademico { get; set; }

        public string TitulacaoDescricaoCompleta { get; set; }

        public bool FormacaoPossuiApostilamento { get; set; }

        [SMCHidden]
        public string DescricaoApostilamento { get; set; }
    }
}