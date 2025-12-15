using SMC.Academico.Common.Areas.MAT.Constants;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.MAT.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORT.Interfaces;
using SMC.Academico.UI.Mvc.Areas.ALN.Lookups;
using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.Academico.UI.Mvc.Areas.DCT.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.CAM.Controllers;
using SMC.SGA.Administrativo.Areas.CSO.Controllers;
using SMC.SGA.Administrativo.Areas.ORT.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.TUR.Models
{
    public class TurmaOrientacaoFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        #region Corpo do Filtro        

        [AlunoLookup]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid8_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        [SMCFilter]
        public AlunoLookupViewModel SeqPessoaAtuacao { get; set; }

        [ColaboradorLookup]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid8_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        [SMCFilter]
        public ColaboradorLookupViewModel SeqColaborador { get; set; }

        //[SMCSelect]
        //[SMCSize(SMCSize.Grid12_24, SMCSize.Grid8_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        //public bool? VinculoAtivo { get; set; }

       // [SMCHidden]
       //// [SMCParameter]
       // public long SeqDivisaoTurma { get; set; }

       // [SMCHidden]
       //// [SMCParameter]
       // public long SeqTurma { get; set; }

        //Listar os alunos, cujo token do tipo de situação da situação de matrícula atual no ciclo letivo da turma é:
        //- MATRICULADO
        //- FORMADO
        //- NÃO MATRICULADO
        //- APTO PARA MATRICULA
        [SMCHidden]
        [SMCParameter]
        //String de tokens separados por virgula, uma vez que se passado uma lista de string ocorre exception ao mudar o numero de páginas na tela de pesquisa.
        public string TokensTipoSituacaoMaticula { get; set; } = $"{TOKENS_TIPO_SITUACAO_MATRICULA.MATRICULADO},{TOKENS_TIPO_SITUACAO_MATRICULA.FORMADO },{TOKENS_TIPO_SITUACAO_MATRICULA.APTO_MATRICULA},{TOKENS_TIPO_SITUACAO_MATRICULA.NAO_MATRICULADO}";

        #endregion

    }
}