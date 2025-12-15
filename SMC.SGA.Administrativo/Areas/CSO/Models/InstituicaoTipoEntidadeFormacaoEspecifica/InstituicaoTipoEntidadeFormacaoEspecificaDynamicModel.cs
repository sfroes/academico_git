using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.Common.Areas.CSO.Enums;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.ORG.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    [SMCGroupedPropertyConfiguration(GroupId = "associacaoIngressante", Size = SMCSize.Grid24_24)]
    [SMCGroupedPropertyConfiguration(GroupId = "associacaoAlunoFormacao", Size = SMCSize.Grid24_24)]
    public class InstituicaoTipoEntidadeFormacaoEspecificaDynamicModel : SMCDynamicViewModel, ISMCTreeNode
    {
        [SMCHidden]
        [SMCKey]
        [SMCOrder(0)]
        public override long Seq { get; set; }

        [SMCHidden]
        [SMCRequired]
        public long SeqInstituicaoTipoEntidade { get; set; }

        [SMCReadOnly]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid12_24)]
        public string DescricaoInstituicaoTipoEntidade { get; set; }

        [SMCHidden]
        public long? SeqPai { get; set; }

        [SMCConditionalDisplay("SeqPai", SMCConditionalOperation.GreaterThen, 0)]
        [SMCInclude("TipoFormacaoEspecificaPai.TipoFormacaoEspecifica")]
        [SMCMapProperty("TipoFormacaoEspecificaPai.TipoFormacaoEspecifica.Descricao")]
        [SMCParameter(Name = "Description")] //Para passar a descrição do pai para o no filho (quando for novo)
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid12_24)]
        public string TipoFormacaoSuperior { get; set; }

        [SMCFilter(true, true)]
        [SMCOrder(0)]
        [SMCRequired]
        [SMCSelect("TiposFormacaoEspecifica", "Seq", "Descricao")]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid12_24)]
        public long SeqTipoFormacaoEspecifica { get; set; }

        [SMCDataSource("TipoFormacaoEspecifica")]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoFormacaoEspecificaService), nameof(ITipoFormacaoEspecificaService.BuscarTipoFormacaoEspecificaSelect),
            values: new string[] { nameof(ClasseTipoFormacao) })]
        public List<SMCDatasourceItem> TiposFormacaoEspecifica { get; set; }

        [SMCHidden]
        [SMCIgnoreMetadata]
        public ClasseTipoFormacao ClasseTipoFormacao { get; set; } = ClasseTipoFormacao.Programa;

        [SMCGroupedProperty("associacaoIngressante")]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid12_24)]
        [SMCSelect]
        public bool ObrigatorioAssociacaoIngressante { get; set; }

        [SMCGroupedProperty("associacaoIngressante")]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid12_24)]
        [SMCMinValue(1)]
        [SMCMapForceFromTo]
        public short QuantidadePermitidaAssociacaoIngressante { get; set; } = 1;

        [SMCGroupedProperty("associacaoAlunoFormacao")]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid12_24)]
        [SMCSelect]
        public bool ObrigatorioAssociacaoAluno { get; set; }

        [SMCGroupedProperty("associacaoAlunoFormacao")]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid12_24)]
        [SMCMinValue(1)]
        [SMCMapForceFromTo]
        public short QuantidadePermitidaAssociacaoAluno { get; set; } = 1;

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .Tokens(tokenInsert: UC_CSO_003_08_01.MANTER_TIPO_FORMACAO_TIPO_ENTIDADE,
                           tokenEdit: UC_CSO_003_08_01.MANTER_TIPO_FORMACAO_TIPO_ENTIDADE,
                           tokenRemove: UC_CSO_003_08_01.MANTER_TIPO_FORMACAO_TIPO_ENTIDADE,
                           tokenList: UC_CSO_003_08_01.MANTER_TIPO_FORMACAO_TIPO_ENTIDADE)
                   .ModalSize(SMCModalWindowSize.Large, SMCModalWindowSize.Large, SMCModalWindowSize.Large);

            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("seqInstituicaoTipoEntidade"))
                options.ButtonBackIndex("Index", "InstituicaoTipoEntidade", x => new { area = "ORG" });
        }

    }
}