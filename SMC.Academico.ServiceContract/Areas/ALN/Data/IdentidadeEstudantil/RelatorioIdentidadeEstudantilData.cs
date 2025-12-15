using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class RelatorioIdentidadeEstudantilData : ISMCMappable
    {
        public long SeqAlunoEsquerda { get; set; }

        public long SeqAlunoDireita { get; set; }

        public long SeqColaboradorEsquerda { get; set; }

        public long SeqColaboradorDireita { get; set; }

        public long SeqProgramaEsquerda { get; set; }

        public long SeqProgramaDireita { get; set; }

        public string NomeEsquerda { get; set; }

        public string NomeDireita { get; set; }

        public string RegistroDVEsquerda { get; set; }

        public string RegistroDVDireita { get; set; }

        public string DataValidadeEsquerda { get; set; }

        public string DataValidadeDireita { get; set; }

        public string DescricaoTipoEntidadeResponsavelEsquerda { get; set; }

        public string DescricaoTipoEntidadeResponsavelDireita { get; set; }

        public string DescricaoEntidadeResponsavelEsquerda { get; set; }

        public string DescricaoEntidadeResponsavelDireita { get; set; }

        public string DescricaoUnidadeDireita { get; set; }

        public string DescricaoUnidadeEsquerda { get; set; }

        public string CodigoDireita { get; set; }

        public string CodigoEsquerda { get; set; }

        public string CodigoBarraDireita { get; set; }

        public string CodigoBarraEsquerda { get; set; }

        public bool Frente { get; set; }
    }
}