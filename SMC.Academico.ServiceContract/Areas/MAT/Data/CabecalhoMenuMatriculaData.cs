using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.MAT.Data
{
    public class CabecalhoMenuMatriculaData : ISMCMappable
    {
        public string DescricaoProcesso { get; set; }

        public string DescricaoUnidadeResponsavel { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public string LocalidadeUnidadeResponsavel { get; set; }

        public string DescricaoNivelEnsino { get; set; }

        public string DescricaoVinculo { get; set; }

        public string DescricaoVinculoInstitucional { get; set; }

        public string DescricaoCicloLetivo { get; set; }

        public string NomeIngressante { get; set; }

        public string Nome { get; set; }

        public long NumeroRegistroAcademico { get; set; }

        public long SeqPessoa { get; set; }

        public bool ExibeEntidadeResponsavel { get; set; }

        public bool ExibeNivelEnsino { get; set; }

        public bool ExibeVinculo { get; set; }

        public string DescricaoCurso { get; set; }

        public bool ExibeDescricaoCurso { get; set; }
    }
}