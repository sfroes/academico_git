using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.Academico.UI.Mvc.Areas.PES.Lookups;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.CNC.Controllers;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CNC.Models
{
    public class DocumentoConclusaoFiltroViewModel : SMCPagerViewModel, ISMCMappable
    {
        #region DataSources 

        public List<SMCDatasourceItem> EntidadesResponsaveis { get; set; }

        public List<SMCDatasourceItem> TiposDocumento { get; set; }

        public List<SMCDatasourceItem> Situacoes { get; set; }

        #endregion

        [PessoaLookup(ModalWindowSize = SMCModalWindowSize.Largest)]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid9_24, SMCSize.Grid24_24, SMCSize.Grid9_24, SMCSize.Grid9_24)]
        public PessoaLookupViewModel SeqPessoa { get; set; }

        [SMCSelect(nameof(EntidadesResponsaveis))]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public List<long?> SeqsEntidadesResponsaveis { get; set; }

        [CursoOfertaLocalidadeLookup]
        [SMCDependency(nameof(SeqsEntidadesResponsaveis))]
        [SMCDependency(nameof(ListarDepartamentosGruposProgramas))]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid9_24, SMCSize.Grid24_24, SMCSize.Grid9_24, SMCSize.Grid9_24)]
        public CursoOfertaLocalidadeLookupViewModel SeqCursoOfertaLocalidade { get; set; }

        [SMCSelect(nameof(TiposDocumento))]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public long? SeqTipoDocumentoAcademico { get; set; }

        [SMCSelect(nameof(Situacoes))]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public long? SeqSituacaoDocumentoAcademico { get; set; }

        [SMCHidden]
        [SMCDependency(nameof(SeqTipoDocumentoAcademico), nameof(DocumentoConclusaoController.HabilitaTipoInvalidade), "DocumentoConclusao", true, includedProperties: new[] { nameof(SeqSituacaoDocumentoAcademico) })]
        [SMCDependency(nameof(SeqSituacaoDocumentoAcademico), nameof(DocumentoConclusaoController.HabilitaTipoInvalidade), "DocumentoConclusao", true, includedProperties: new[] { nameof(SeqTipoDocumentoAcademico) })]
        public bool HabilitaTipoInvalidade { get; set; }

        [SMCSelect()]
        [SMCConditionalDisplay(nameof(HabilitaTipoInvalidade), SMCConditionalOperation.Equals, true)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public TipoInvalidade? TipoInvalidade { get; set; }

        [SMCHidden]
        public long? SeqTipoServico { get; set; }

        [SMCHidden]
        public bool ListarDepartamentosGruposProgramas { get; set; } = true;
    }
}