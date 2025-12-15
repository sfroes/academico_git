using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Models
{
    /// <summary>
    /// Modelo do dynamic de EntidadeHistoricoSituação.
    /// Para configurar o link de retorno e o NavigationGroup
    /// deverão ser criadas DynamicModels que herdem desta classe
    /// e sobrescrevam ConfigureDynamic com o ButtonBackIndex
    /// e ConfigureNavigation adequados
    /// </summary>
    public class EntidadeHistoricoSituacaoViewModel : SMCDynamicViewModel, ISMCMappable
    {
        [SMCHidden]
        [SMCKey]
        [SMCOrder(0)]
        [SMCRequired]
        public override long Seq { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqEntidade { get; set; }

        [SMCHidden]
        [SMCInclude("Entidade")]
        [SMCMapProperty("Entidade.SeqTipoEntidade")]
        [SMCParameter]
        public long SeqTipoEntidade { get; set; }

        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCInclude("SituacaoEntidade")]
        [SMCOrder(1)]
        [SMCRequired]
        [SMCSelect("SituacoesEntidade")]
        [SMCSize(SMCSize.Grid19_24)]
        public long SeqSituacaoEntidade { get; set; }

        [SMCDataSource()]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoTipoEntidadeService), "BuscarSituacoesTipoEntidadeDaInstituicaoSelect",
            values: new string[] { nameof(SeqTipoEntidade) })]
        public List<SMCDatasourceItem> SituacoesEntidade { get; set; }

        [SMCDescription]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCInclude("SituacaoEntidade")]
        [SMCMapProperty("SituacaoEntidade.Descricao")]
        [SMCOrder(2)]
        public string DescricaoStatus { get; set; }

        [SMCHidden(SMCViewMode.Filter)]
        [SMCOrder(3)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid5_24)]
        public DateTime DataInicio { get; set; }

        [SMCHidden(SMCViewMode.Filter | SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCOrder(4)]
        public DateTime? DataFim { get; set; }

        [SMCHidden]
        public bool Ultimo { get { return !this.DataFim.HasValue; } }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options
                .HeaderIndex("CabecalhoEntidadeHistoricoSituacao")
                .EditInModal()
                .IgnoreFilterGeneration()
                .ConfigureButton((button, model, action) =>
                {
                    var entidadeHistorico = (EntidadeHistoricoSituacaoViewModel)model;
                    button.Hide(!entidadeHistorico.Ultimo);
                })
                .Service<IEntidadeHistoricoSituacaoService>(index: nameof(IEntidadeHistoricoSituacaoService.BuscarSituacoes),
                                                            save: nameof(IEntidadeHistoricoSituacaoService.SalvarEntidadeHistoricoSituacao),
                                                            edit: nameof(IEntidadeHistoricoSituacaoService.BuscarEntidadeHistoricoSituacao),
                                                            delete: nameof(IEntidadeHistoricoSituacaoService.ExcluirEntidadeHistoricoSituacao));
        }
    }
}