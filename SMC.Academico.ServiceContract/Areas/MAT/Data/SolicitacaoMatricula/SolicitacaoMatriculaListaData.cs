using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.MAT.Data
{
    public class SolicitacaoMatriculaListaData : ISMCMappable
    {
        public long SeqSolicitacaoServico { get; set; }

        public string NomeInstituicaoEnsino { get; set; }

        public string DescricaoProcesso { get; set; }

        public string DescricaoVinculo { get; set; }

        public string DescricaoFormaIngresso { get; set; }

        public string DescricaoOferta { get; set; }

        public string CicloLetivo { get; set; }

        public string DescricaoSituacao { get; set; }

        public bool ExibirVinculo { get; set; }

        public bool ExibirCicloLetivo { get; set; }
    }
}