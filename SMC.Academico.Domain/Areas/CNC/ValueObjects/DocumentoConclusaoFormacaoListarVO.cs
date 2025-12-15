using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class DocumentoConclusaoFormacaoListarVO : ISMCMappable, ISMCSeq
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

        public string DescricaoTitulacao { get; set; }

        public string DescricaoTitulacaoXSD { get; set; }

        public string DescricaoTitulacaoMasculino { get; set; }

        public string DescricaoTitulacaoFeminino { get; set; }

        public string TitulacaoDescricaoCompleta
        {
            get
            {
                var result = string.Empty;

                if (!string.IsNullOrEmpty(DescricaoTitulacaoMasculino))
                    result += $"{DescricaoTitulacaoMasculino}";

                if (!string.IsNullOrEmpty(result))
                {
                    if (!string.IsNullOrEmpty(DescricaoTitulacaoFeminino))
                        result += $" / {DescricaoTitulacaoFeminino}";
                }
                else
                {
                    if (!string.IsNullOrEmpty(DescricaoTitulacaoFeminino))
                        result += $"{DescricaoTitulacaoFeminino}";
                }

                return result;
            }
        }

        public string ObservacaoFormacao { get; set; }

        public long? SeqEntidadeResponsavel { get; set; }

        public long? SeqFormacaoEspecifica { get; set; }

        public string DescricaoTipoFormacaoEspecifica { get; set; }
        public string DescricaoFormacaoEspecifica { get; set; }
    }
}
