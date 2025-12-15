using SMC.Academico.UI.Mvc.Areas.PES.Controllers;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.PES.Models.SuporteTecnico
{
    public class SuporteTecnicoViewModel : SMCViewModelBase
    {
        #region [ DataSources ]

        public List<SMCDatasourceItem> Tipos { get; set; }

        #endregion [ DataSources ]

        [SMCSelect(nameof(Tipos), NameDescriptionField = nameof(DescricaoCatalogo))]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid8_24)]
        [SMCRequired]
        public long? CatalogoServicoId { get; set; }

        public string DescricaoCatalogo { get; set; }

        [SMCEmail]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid8_24)]
        [SMCRequired]
        public string Email { get; set; }

        [SMCPhone(IncludeDDD = true)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid8_24)]
        [SMCRequired]
        public string Telefone { get; set; }

        [SMCDependency(nameof(CatalogoServicoId), nameof(SuporteTecnicoController.ListaDadosDescricao), "SuporteTecnicoRoute", true, includedProperties: new[] { nameof(DescricaoCatalogo) })]
        [SMCRequired]       
        [SMCMultiline(Rows = 10)]
        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24)]
        public string Descricao { get; set; }


        public int? LoginSolicitante { get; set; }

        public long? NumeroMatricula { get; set; }       

        public bool Professor { get; set; }

        public int? CodigoOrigem { get; set; }

        public string DescricaoOrigem { get; set; }

        public string CodigoEstabelecimento { get; set; }

        public string CodigoPessoa { get; set; }

        public string Nome { get; set; }

        public string UsuarioTeams { get; set; }

        public string Token { get; set; }

        public bool Mobile { get; set; }
    }
}
