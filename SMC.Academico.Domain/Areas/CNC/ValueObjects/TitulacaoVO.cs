using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class TitulacaoVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public string DescricaoFeminino { get; set; }

        public string DescricaoMasculino { get; set; }

        public string DescricaoAbreviada { get; set; }

        public string DescricaoXSD { get; set; }

        public bool Ativo { get; set; }

        public long? SeqCurso { get; set; }

        [SMCMapProperty("Curso.Nome")]
        public string DescricaoCurso { get; set; }

        [SMCMapProperty("GrauAcademico.Descricao")]
        public string DescricaoGrauAcademico { get; set; }

        public long? SeqGrauAcademico { get; set; }

        public string DescricaoCompleta
        {
            get
            {
                var result = string.Empty;

                if (!string.IsNullOrEmpty(DescricaoMasculino))
                    result += $"{DescricaoMasculino}";

                if (!string.IsNullOrEmpty(result))
                {
                    if (!string.IsNullOrEmpty(DescricaoFeminino))
                        result += $" / {DescricaoFeminino}";
                }
                else
                {
                    if (!string.IsNullOrEmpty(DescricaoFeminino))
                        result += $"{DescricaoFeminino}";
                }

                return result;
            }
        }

        public string Descricao
        {
            get
            {
                return string.Format("{0} / {1}", this.DescricaoMasculino, this.DescricaoFeminino);
            }
        }

        public List<TitulacaoDocumentoComprobatorioVO> DocumentosComprobatorios { get; set; }
    }
}