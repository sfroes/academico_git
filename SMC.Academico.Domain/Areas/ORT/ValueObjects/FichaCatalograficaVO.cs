using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.ORT.ValueObjects
{
    public class FichaCatalograficaVO : ISMCMappable
    {
        public string NomeAlunoABNT { get; set; }
        public string NomeAluno { get; set; }
        public string Cidade { get; set; }
        public string AnoDefesa { get; set; }
        public short? NumeroPaginas { get; set; }
        public string Orientador { get; set; }
        public string Coorientador { get; set; }
        public string TipoTrabalho { get; set; }
        public string NivelEnsino { get; set; }
        public string InstituicaoEnsino { get; set; }
        public string GrupoPrograma { get; set; }
        public string NomeOrientadorABNT { get; set; }
    }
}
