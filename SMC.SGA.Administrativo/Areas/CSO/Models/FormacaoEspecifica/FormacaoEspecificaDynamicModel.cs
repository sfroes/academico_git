using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class FormacaoEspecificaDynamicModel : SMCDynamicViewModel, ISMCTreeNode
    {
        #region [ Hidden ]

        [SMCHidden]
        public long? SeqFormacaoEspecificaSuperior { get; set; }

        [SMCHidden]
        //TODO: Verificar por que o map property não funcionou
        //[SMCInclude("FormacaoEspecificaSuperior")]
        //[SMCMapProperty("FormacaoEspecificaSuperior.Seq")]
        public long? SeqPai { get { return SeqFormacaoEspecificaSuperior; } set { SeqFormacaoEspecificaSuperior = value; } }

        [SMCHidden]
        [SMCParameter]
        public long SeqEntidadeResponsavel { get; set; }

        //Utilizado pelo navigation group
        [SMCHidden]
        [SMCParameter("SeqEntidadeResponsavel")]
        public long SeqEntidade { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqHierarquiaEntidadeItem { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqTipoEntidade { get; set; }

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }


        [SMCHidden]
        public bool TipoFormacaoEspecificaFolha { get; set; }

        [SMCHidden]
        public bool PossuiCursoAssociado { get; set; }

        #endregion [ Hidden ]

        #region [ DataSources ]

        [SMCDataSource("TipoFormacaoEspecifica")]
        [SMCServiceReference(typeof(ITipoFormacaoEspecificaService), "BuscarTipoFormacaoEspecificaEntidadeSelect",
            values: new string[] { nameof(SeqTipoEntidade), nameof(SeqFormacaoEspecificaSuperior) })]
        public List<SMCDatasourceItem> TiposFormacaoEspecifica { get; set; }

        #endregion [ DataSources ]

        [SMCKey]
        [SMCOrder(2)]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid2_24)]
        public override long Seq { get; set; }

        [SMCConditionalDisplay("SeqPai", SMCConditionalOperation.GreaterThen, new object[] { 0 })]
        [SMCInclude("FormacaoEspecificaSuperior")]
        [SMCMapProperty("FormacaoEspecificaSuperior.Descricao")]
        [SMCOrder(1)]
        [SMCParameter("Description")]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid24_24)]
        public string NomeFormacaoEspecificaSuperior { get; set; }

        [SMCOrder(3)]
        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCRequired]
        [SMCSelect(nameof(TiposFormacaoEspecifica), AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid5_24)]
        public long SeqTipoFormacaoEspecifica { get; set; }

        [SMCHidden]
        public string DescricaoTipoFormacaoEspecifica { get; set; }

        [SMCDescription]
        [SMCHidden]
        public string DescricaoFormatada { get => $"[{DescricaoTipoFormacaoEspecifica}] {Descricao}"; }

        [SMCMaxLength(250)]
        [SMCOrder(4)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid11_24)]
        public string Descricao { get; set; }

        [SMCOrder(5)]
        [SMCSize(SMCSize.Grid6_24)]
        [SMCRadioButtonList]
        [SMCRequired]
        [SMCMapForceFromTo]
        public bool? Ativo { get; set; } = true;

        #region [ Configurações ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            Func<SMCTreeViewNode<FormacaoEspecificaDynamicModel>, string> retCss = (model) =>
            {
                List<string> ret = new List<string>();

                if (!model.Value.Ativo.GetValueOrDefault())
                    ret.Add("smc-sga-item-inativo");

                if (!model.Value.PossuiCursoAssociado)
                    ret.Add("smc-sga-item-nao-associado");

                return string.Join(" ", ret);
            };

            options
                .EditInModal()
                .ModalSize(SMCModalWindowSize.Large)
                .IgnoreFilterGeneration()
                .HeaderIndex("CabecalhoFormacaoEspecifica")
                .HeaderIndexList("MensagemFormacaoEspecifica")
                .ButtonBackIndex("Index", "Programa")
                //.ConfigureContextMenuItem((button, model, action) =>
                //{
                //    if (action == SMCDynamicButtonAction.Insert)
                //    {
                //        button.Hide((model as SMCTreeViewNode<FormacaoEspecificaDynamicModel>).Value.TipoFormacaoEspecificaFolha);
                //    }
                //})
                .Service<IFormacaoEspecificaService>(index: nameof(IFormacaoEspecificaService.BuscarFormacoesEspecificas),
                                                     save:  nameof(IFormacaoEspecificaService.SalvarFormacaoEspecifica))
                .TreeView(configureNode: x => (x as SMCTreeViewNode<FormacaoEspecificaDynamicModel>).Options.CssClass = retCss(x as SMCTreeViewNode<FormacaoEspecificaDynamicModel>))
                .Button(idResource: "ReplicarFormacaoEspecificaPrograma",
                        action: "ReplicarFormacaoEspecificaPrograma",
                        controller: "Programa",
                        securityToken: UC_CSO_002_01_07.REPLICAR_FORMACAO_ESPECIFICA_PROGRAMA,
                        displayButton: x => (x as SMCTreeViewNode<FormacaoEspecificaDynamicModel>).Value.Ativo.GetValueOrDefault() && (x as SMCTreeViewNode<FormacaoEspecificaDynamicModel>).Value.TipoFormacaoEspecificaFolha,
                        routes: x => new { SeqFormacaoEspecifica = new SMCEncryptedLong((x as SMCTreeViewNode<FormacaoEspecificaDynamicModel>).Value.Seq), SeqEntidadeResponsavel = new SMCEncryptedLong((x as SMCTreeViewNode<FormacaoEspecificaDynamicModel>).Value.SeqEntidadeResponsavel) })
                .Tokens(tokenInsert: UC_CSO_002_01_04.MANTER_FORMACAO_ESPECIFICA_PROGRAMA,
                        tokenEdit: UC_CSO_002_01_04.MANTER_FORMACAO_ESPECIFICA_PROGRAMA,
                        tokenRemove: UC_CSO_002_01_04.MANTER_FORMACAO_ESPECIFICA_PROGRAMA,
                        tokenList: UC_CSO_002_01_03.PESQUISAR_FORMACAO_ESPECIFICA_PROGRAMA);
        }

        public override void ConfigureNavigation(ref SMCNavigationGroup navigationGroup)
        {
            navigationGroup = new ProgramaNavigationGroup(this);
        }

        #endregion [ Configurações ]
    }
}