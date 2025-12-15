using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class PessoaAtuacaoTermoIntercambioListarDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCHidden]
        public override long Seq { get; set; }

        [SMCHidden]
        public long SeqPessoaAtuacao { get; set; }

        [SMCMaxLength(100)]
        public string Nome { get; set; }

        public string NomeSocial { get; set; }

        [SMCMaxLength(100)]
        public TipoAtuacao TipoAtuacao { get; set; }

        [SMCMaxLength(100)]
        public string TipoVinculoAlunoDescricao { get; set; }

        public string NomeEntidadeResponsavel { get; set; }

        public string DescricaoNivelEnsino { get; set; }

        public string DescricaoTermoIntercambio { get; set; }
        
        public string DescricaoTipoTermo { get; set; }

        [SMCHidden]
        public long SeqTermoIntercambio { get; set; }

        public string InstituicaoExternaNome { get; set; }
                
        public TipoMobilidade TipoMobilidade { get; set; }

        //Períodos
        public List<PessoaAtuacaoTermoIntercambioPeriodoDynamicModel> Periodos { get; set; }
    }
}