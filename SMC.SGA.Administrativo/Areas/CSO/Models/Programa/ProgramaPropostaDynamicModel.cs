using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Formularios.UI.Mvc.Attributes;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Models;
using System.Collections.Generic;
using System.Linq;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class ProgramaPropostaDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCHidden]
        public override long Seq { get; set; }

        /// <summary>
        /// Sequencial do programa utilizado na navegação
        /// </summary>
        [SMCHidden]
        [SMCParameter]
        public long SeqEntidade { get; set; }

        /// <summary>
        /// Sequencial do programa utilizado para filtrar o resultado da lista
        /// </summary>
        [SMCHidden]
        [SMCParameter("SeqEntidade")]
        public long SeqPrograma { get; set; }

        [SMCOrder(1)]
        [SMCHidden]
        public long SeqDadoFormulario { get; set; }

        [CicloLetivoLookup]
        [SMCOrder(0)]
        [SMCHidden(SMCViewMode.Filter)]
        [SMCRequired]
        [SMCInclude(Name = "CicloLetivo")]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCDependency(nameof(SeqsNiveisEnsinoStr))]
        public CicloLetivoLookupViewModel SeqCicloLetivo { get; set; }

        /// <summary>
        /// Sequenciais dos níveis de ensino stricto sensu para filtro do lookup
        /// </summary>
        [SMCIgnoreProp]
        public List<long> SeqsNiveisEnsino { get; set; }

        /// <summary>
        /// Concatena os sequenciais para enviar como dependency do lookup
        /// </summary>
        [SMCHidden]
        public string SeqsNiveisEnsinoStr { get { return SeqsNiveisEnsino != null ? string.Join(",", SeqsNiveisEnsino.ToArray()) : null; } set { SeqsNiveisEnsino = value != null ? value?.Split(',').Select(x => long.Parse(x)).ToList() : null; } }

        [SMCHidden]
        [SMCDescription]
        [SMCMapProperty("CicloLetivo.Descricao")]
        public string Descricao { get; set; }

        [SMCOrder(2)]
        [SMCSGF]
        [SMCIgnoreProp(SMCViewMode.Filter | SMCViewMode.List)]
        public SGADadoFormularioViewModel DadoFormulario { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqHierarquiaEntidadeItem { get; set; }

        #region [ Configuração ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options
                .Header("ProgramaPropostaCabecalho")
                .HeaderIndex("ProgramaPropostaCabecalho")
                .ButtonBackIndex("Index", "Programa")
                .RequiredIncomingParameters(nameof(SeqEntidade))
                .IgnoreFilterGeneration()
                .Service<IProgramaPropostaService>(insert: nameof(IProgramaPropostaService.BuscarConfiguracoesFormularioProposta),
                                                     save: nameof(IProgramaPropostaService.SalvarProgramaProposta),
                                                     edit: nameof(IProgramaPropostaService.BuscarProgramaProposta));
        }

        public override void ConfigureNavigation(ref SMCNavigationGroup navigationGroup)
        {
            navigationGroup = new ProgramaNavigationGroup(this);
        }

        #endregion [ Configuração ]
    }
}