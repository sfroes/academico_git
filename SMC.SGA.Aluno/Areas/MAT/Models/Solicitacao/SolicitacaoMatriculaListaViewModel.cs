using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Aluno.Areas.MAT.Models
{
    public class SolicitacaoMatriculaListaViewModel : SMCViewModelBase
    {
        public long SeqSolicitacaoServico { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public string NomeInstituicaoEnsino { get; set; }

        [SMCSize(Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid24_24)]
        public string DescricaoProcesso { get; set; }

        [SMCSize(Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid24_24)] 
        public string DescricaoVinculo { get; set; }

        [SMCSize(Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid24_24)]
        public string DescricaoFormaIngresso { get; set; }

        [SMCSize(Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid24_24)] 
        public string DescricaoOferta { get; set; }

        [SMCSize(Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid24_24)]
        public string CicloLetivo { get; set; }

        [SMCSize(Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid24_24)] 
        public string DescricaoSituacao { get; set; }

        public bool ExibirVinculo { get; set; }

        public bool ExibirCicloLetivo { get; set; }
    }
}