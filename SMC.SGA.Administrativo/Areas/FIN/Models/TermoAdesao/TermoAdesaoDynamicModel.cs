using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework;
using SMC.Academico.Common.Areas.FIN.Constants;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.FIN.Interfaces;
using SMC.SGA.Administrativo.Areas.FIN.Views.TermoAdesao.App_LocalResources;

namespace SMC.SGA.Administrativo.Areas.FIN.Models
{
    public class TermoAdesaoDynamicModel : SMCDynamicViewModel
    {
        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IServicoService), nameof(IServicoService.BuscarServicosPorInstituicaoNivelDoContratoSelect), values: new[] { nameof(SeqContrato) })]
        public List<SMCDatasourceItem> Servicos { get; set; } = new List<SMCDatasourceItem>();

        [SMCDataSource]
        [SMCServiceReference(typeof(ITipoVinculoAlunoService), nameof(ITipoVinculoAlunoService.BuscarTiposVinculoAlunoPorServicoSelect), values: new[] { nameof(SeqServico) })]
        public List<SMCDatasourceItem> TiposVinculoAluno { get; set; }

        [SMCKey]
        [SMCHidden]
        public override long Seq { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqContrato { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        [SMCRequired]
        [SMCMaxLength(100)]
        public string Titulo { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        [SMCRequired]
        [SMCMultiline(Rows = 6)]
        public string Descricao { get; set; }

        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid12_24)]
        [SMCSelect(nameof(Servicos))]
        [SMCRequired]
        public long SeqServico { get; set; }

        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        [SMCSelect(nameof(TiposVinculoAluno))]
        [SMCRequired]
        [SMCDependency(nameof(SeqServico), "BuscarTiposVinculoAlunoPorServicoSelect", "TermoAdesao", true)]
        public long SeqTipoVinculoAluno { get; set; }

        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid4_24)]
        [SMCRequired]
        [SMCRadioButtonList]
        public bool? Ativo { get; set; }

        #region Configurações

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options
                .Service<ITermoAdesaoService>(
                      index: nameof(ITermoAdesaoService.ListarTermosAdesao),
                      insert: nameof(ITermoAdesaoService.BuscarTermoAdesao),
                      save: nameof(ITermoAdesaoService.SalvarTermoAdesao)
                 )
                .HeaderIndex("CabecalhoTermoAdesao")
                .Header("CabecalhoTermoAdesao")
                .ButtonBackIndex("Index", "Contrato")
                .Messages(x => string.Format(UIResource.Listar_Excluir_Confirmacao,
                              ((TermoAdesaoListarDynamicModel)x).Titulo))
                .Tokens(tokenList: UC_FIN_002_02_03.PESQUISAR_TERMO_ADESAO,
                        tokenEdit: UC_FIN_002_02_04.MANTER_TERMO_ADESAO,
                        tokenRemove: UC_FIN_002_02_04.MANTER_TERMO_ADESAO,
                        tokenInsert: UC_FIN_002_02_04.MANTER_TERMO_ADESAO);
        }

        #endregion
    }
}