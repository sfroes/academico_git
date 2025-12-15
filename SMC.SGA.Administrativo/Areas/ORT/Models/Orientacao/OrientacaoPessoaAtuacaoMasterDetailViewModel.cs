using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.UI.Mvc.Areas.ALN.Lookups;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
    public class OrientacaoPessoaAtuacaoMasterDetailViewModel : SMCViewModelBase
    {
        //FIX: Remover está classe passando os paramentros para a class OrientacaoPessoaAtuacaoViewModel, quando for corrigido SMCHidden no SMCMasterDetails

        #region Hidden

        [SMCIgnoreProp]
        public string NomeAlunoConfirmacao { get; set; }

        [SMCIgnoreProp]
        public long RAConfirmacao { get; set; }

        #endregion

        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqOrientacao { get; set; }

        [AlunoLookup]
        [SMCDependency(nameof(OrientacaoDynamicModel.SeqNivelEnsino))]
        [SMCDependency(nameof(OrientacaoDynamicModel.SeqTipoVinculoAluno))]
        [SMCDependency(nameof(OrientacaoDynamicModel.SeqTipoTermoIntercambio))]
        [SMCRequired]
        [SMCUnique]
        [SMCSize(SMCSize.Grid24_24)]
        public AlunoLookupViewModel SeqPessoaAtuacao { get; set; }
    }
}