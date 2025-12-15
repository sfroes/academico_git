using SMC.Academico.UI.Mvc.Models;
using SMC.Framework.UI.Mvc;
using System.Collections.Generic;

namespace SMC.SGA.Aluno.Areas.MAT.Models
{
    public class SolicitacaoMatriculaSelecionadaViewModel : SMCViewModelBase
    {
        public long SeqSolicitacaoServico { get; set; }

        public string DescricaoProcesso { get; set; }

        public List<EtapaListaViewModel> Etapas { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public bool ExibirEntregaDocumentos { get; set; }
    }
}