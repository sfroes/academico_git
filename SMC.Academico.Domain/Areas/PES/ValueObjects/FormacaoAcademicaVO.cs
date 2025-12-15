using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.DCT.Models;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class FormacaoAcademicaVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public long SeqTitulacao { get; set; }

        public string Orientador { get; set; }

        public string Descricao { get; set; }

        public int? AnoInicio { get; set; }

        public int? AnoObtencaoTitulo { get; set; }

        public bool? TitulacaoMaxima { get; set; }

        public string Curso { get; set; }

        public short? QuantidadeMinima { get; set; }

        public short? QuantidadeMaxima { get; set; }

        public long? SeqInstituicaoExterna { get; set; }

        public long? SeqHierarquiaClassificacao { get; set; }

        public List<long> SeqsDocumentosApresentados { get; set; }
        
        public Sexo Sexo { get; set; }

        public long? SeqClassificacao { get; set; }
    }
}

