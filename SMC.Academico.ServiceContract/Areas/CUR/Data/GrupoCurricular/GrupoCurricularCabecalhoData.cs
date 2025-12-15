using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class GrupoCurricularCabecalhoData : ISMCMappable
    {
        public string CodigoCurriculo { get; set; }

        public string DescricaoCurriculo { get; set; }

        public string SiglaCurso { get; set; }

        public string NomeCurso { get; set; }

        public bool CurriculoAtivo { get; set; }
    }
}
