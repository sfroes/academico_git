using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CNC.Data
{
    public class TitulacaoData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public string DescricaoFeminino { get; set; }

        public string DescricaoMasculino { get; set; }

        public string DescricaoAbreviada { get; set; }

        public string DescricaoXSD { get; set; }

        public bool Ativo { get; set; }

        public long? SeqCurso { get; set; }

        public string DescricaoCurso { get; set; }

        public string DescricaoGrauAcademico { get; set; }

        public long? SeqGrauAcademico { get; set; }

        public string DescricaoCompleta { get; set; }

        public string Descricao { get; set; } 

        public List<TitulacaoDocumentoComprobatorioData> DocumentosComprobatorios { get; set; }
    }
}