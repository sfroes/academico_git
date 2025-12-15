using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CNC.Data
{
    public class DocumentoConclusaoFormacaoListarData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqDocumentoConclusao { get; set; }

        public long SeqAlunoFormacao { get; set; }

        public long? SeqDocumentoConclusaoApostilamento { get; set; }

        public long NumeroRegistroAcademico { get; set; }

        public string DescricaoNivelEnsino { get; set; }

        public string DescricaoCursoOferta { get; set; }

        public string DescricaoDocumentoConclusao { get; set; }

        public DateTime? DataColacaoGrau { get; set; }

        public long SeqAluno { get; set; }

        public List<string> FormacoesEspecificas { get; set; }

        public DateTime? DataConclusao { get; set; }

        public bool FormacaoPossuiApostilamento { get; set; }

        public string DescricaoApostilamento { get; set; }

        public string DescricaoGrauAcademico { get; set; }

        public string TitulacaoDescricaoCompleta { get; set; }
    }
}
