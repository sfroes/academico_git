using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class ConfiguracaoAvaliacaoPpaTurmaDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCHidden]
        public override long Seq { get; set; }

        [SMCHidden]
        public long SeqTurma { get; set; }

        public string Turma { get; set; }

        public string DescricaoTurma { get; set; }

        public int? CodigoInstrumento { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {

            options.Service<IConfiguracaoAvaliacaoPpaTurmaService>(
                      index: nameof(IConfiguracaoAvaliacaoPpaTurmaService.ListarTurmas)
                )
            .IgnoreFilterGeneration()
            .IgnoreInsert()
            .HeaderIndex("CabecalhoConfiguracaoAvaliacaoPpaTurma")
            .ButtonBackIndex("Index", "ConfiguracaoAvaliacaoPpa")
            .Tokens(tokenEdit: SMCSecurityConsts.SMC_DENY_AUTHORIZATION,
                    tokenRemove: SMCSecurityConsts.SMC_DENY_AUTHORIZATION);


        }
    }
}