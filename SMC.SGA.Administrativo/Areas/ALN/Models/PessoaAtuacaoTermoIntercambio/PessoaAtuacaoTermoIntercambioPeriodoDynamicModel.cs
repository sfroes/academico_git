using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.CAM.Data;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class PessoaAtuacaoTermoIntercambioPeriodoDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCHidden]        
        public override long Seq { get; set; }
        
        [SMCHidden]
        public long SeqPessoaAtuacao { get; set; }

        [SMCHidden]
        public long SeqTermoIntercambio { get; set; }

        [SMCHidden]
        public long SeqCicloLetivo { get; set; }

        [SMCHidden]
        public long? SeqCursoOfertaLocalidadeTurno { get; set; }

        [SMCHidden]
        public string Nome { get; set; }

        [SMCHidden]
        public string NomeSocial { get; set; }

        [SMCHidden]
        public string TipoAtuacao { get; set; }

        [SMCHidden]
        public string TipoVinculoAlunoDescricao { get; set; }

        [SMCHidden]
        public string NomeEntidadeResponsavel { get; set; }

        [SMCHidden]
        public string DescricaoNivelEnsino { get; set; }

        [SMCHidden]
        public string DescricaoTermoIntercambio { get; set; }

        [SMCHidden]
        public string DescricaoTipoTermo { get; set; }
        
        [SMCHidden]        
        public string InstituicaoExternaNome { get; set; }

        [SMCHidden]
        public TipoMobilidade TipoMobilidade { get; set; }

        [SMCHidden]
        public long SeqParceriaIntercambio { get; set; }

        [SMCHidden]
        public long SeqPessoaAtuacaoTermoIntercambio { get; set; }

        [SMCHidden]
        public long SeqAluno { get; set; }

        [SMCHidden]
        public bool PermiteEdicao { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid8_24)]
        public DateTime DataInicio { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid8_24)]        
        [SMCMinDate(nameof(DataInicio))]
        public DateTime DataFim { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Service<IPessoaAtuacaoTermoIntercambioService>
                   (edit: nameof(IPessoaAtuacaoTermoIntercambioService.BuscarPeriodoIntercambio),
                    save: nameof(IPessoaAtuacaoTermoIntercambioService.SalvarPeriodoIntercambio))
                   .EditInModal()
                   .Header("CabecalhoPessoaAtuacaoTermoIntercambioPeriodo")
                   .IgnoreFilterGeneration()
                   .Tokens(tokenList: UC_ALN_004_03_01.PESQUISAR_INTERCAMBIO,
                           tokenEdit: UC_ALN_004_03_02.MANTER_INTERCAMBIO);
        }

    }
}