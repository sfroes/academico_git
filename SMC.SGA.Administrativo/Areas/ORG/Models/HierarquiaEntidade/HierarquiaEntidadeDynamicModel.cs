using SMC.Academico.Common.Areas.ORG.Constants;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class HierarquiaEntidadeDynamicModel : SMCDynamicViewModel
    {
        /// <summary>
        /// Construtor padrão, define um valor padrão para a data de início da vigência
        /// </summary>
        public HierarquiaEntidadeDynamicModel()
        {
            this.DataInicioVigencia = DateTime.Now;
        }

        [SMCKey]
        [SMCOrder(0)]
        [SMCReadOnly(SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
        public override long Seq { get; set; }

        [SMCDescription]
        [SMCFocus]
        [SMCMaxLength(100)]
        [SMCOrder(1)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid11_24)]
        public string Descricao { get; set; }

        [SMCMapForceFromTo]
        [SMCMaxDate(nameof(DataFimVigencia))]
        [SMCOrder(2)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        public DateTime DataInicioVigencia { get; set; }

        [SMCMinDate(nameof(DataInicioVigencia))]
        [SMCOrder(3)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
        public DateTime? DataFimVigencia { get; set; }

        [SMCHidden(SMCViewMode.Filter)]
        [SMCOrder(4)]
        [SMCRequired]
        [SMCSelect("TiposHierarquiaEntidade", "Seq", "Descricao")]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid3_24)]
        public long SeqTipoHierarquiaEntidade { get; set; }

        [SMCDataSource("TipoHierarquiaEntidade")]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IORGDynamicService))]
        public List<SMCDatasourceItem> TiposHierarquiaEntidade { get; set; }

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }

        #region configuracoes

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options
                   .Button("Montagem da hierarquia", "VisaoArvore", "HierarquiaEntidade",
                                UC_ORG_001_07_03.MONTAR_HIERARQUIA_ENTIDADE,
                                i => new { seqHierarquiaEntidade = SMCDESCrypto.EncryptNumberForURL(((ISMCSeq)i).Seq), seqTipoHierarquiaEntidade = SMCDESCrypto.EncryptNumberForURL(((HierarquiaEntidadeListarDynamicModel)i).SeqTipoHierarquiaEntidade) })

                   .Service<IHierarquiaEntidadeService>(index: "BuscarHierarquiasEntidade",
                                                        save: "SalvarHierarquiaEntidade")

                   .Tokens(tokenInsert: UC_ORG_001_07_02.MANTER_HIERARQUIA_ENTIDADE,
                           tokenEdit: UC_ORG_001_07_02.MANTER_HIERARQUIA_ENTIDADE,
                           tokenRemove: UC_ORG_001_07_02.MANTER_HIERARQUIA_ENTIDADE,
                           tokenList: UC_ORG_001_07_01.PESQUISAR_HIERARQUIA_ENTIDADE);
        }

        public override void ConfigureNavigation(ref SMCNavigationGroup navigationGroup)
        {
            navigationGroup = new HierarquiaEntidadeNavigationGroup(this);
        }

        #endregion configuracoes
    }
}