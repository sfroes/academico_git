using SMC.Academico.Common.Constants;
using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.UI.Mvc.Areas.CAM.Lookups
{
    public class SelecaoOfertaCampanhaLookupFiltroViewModel : SMCLookupFilterViewModel
    {
        #region Datasources
        [SMCIgnoreProp]
        public List<SMCSelectListItem> NiveisEnsino { get; set; }

        [SMCIgnoreProp]
        public List<SMCSelectListItem> EntidadesResponsaveis { get; set; }

        [SMCIgnoreProp]
        public List<SMCSelectListItem> Localidades { get; set; }

        [SMCIgnoreProp]
        public List<SMCSelectListItem> TiposOferta { get; set; }
        #endregion

        [SMCHidden]
        public long SeqCampanha { get; set; }

        #region BI_CAM_001        

        [SMCRequired]
        [SMCSelect(nameof(TiposOferta))]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public long SeqTipoOferta { get; set; }

        #region [BI_CSO_002]

        /// <summary>
        /// Sequencial da entidade responsável com o nome esperado pelo lookup (para facilitar o depency)
        /// e mapeado para o nome adequando para os dtos
        /// </summary>        
        //[SMCMapProperty("SeqEntidadesResponsaveis")]
        [SMCSelect(nameof(EntidadesResponsaveis), autoSelectSingleItem: true)]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid7_24)]
        public List<long> SeqsEntidadeResponsavel { get; set; }

        [SMCSelect(nameof(Localidades), autoSelectSingleItem: true)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid11_24)]
        public long? SeqLocalidade { get; set; }

        [SMCSelect(nameof(NiveisEnsino))]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public long? SeqNivelEnsino { get; set; }

        [CursoOfertaLookup]
        [SMCDependency(nameof(SeqsEntidadeResponsavel))]
        [SMCDependency(nameof(SeqLocalidade))]
        [SMCDependency(nameof(SeqNivelEnsino))]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid13_24)]
        public CursoOfertaLookupViewModel SeqCursoOferta { get; set; }

        [SMCConditionalReadonly(nameof(SeqCursoOferta), SMCConditionalOperation.Equals, "", RuleName = "R1")]
        [SMCConditionalReadonly(nameof(SeqLocalidade), SMCConditionalOperation.Equals, "", RuleName = "R2")]
        [SMCConditionalRule("R1 && R2")]
        [SMCDependency(nameof(SeqCursoOferta), "BuscarTurnosPorCursoOfertaOuLocalidadeSelect", "Turno", "CSO", false, includedProperties: new[] { nameof(SeqLocalidade) })]
        [SMCDependency(nameof(SeqLocalidade), "BuscarTurnosPorCursoOfertaOuLocalidadeSelect", "Turno", "CSO", false, includedProperties: new[] { nameof(SeqCursoOferta) })]
        [SMCSelect]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid5_24)]
        public long? SeqTurno { get; set; }

        #endregion [BI_CSO_002]        

        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        [FormacaoEspecificaLookup]
        [SMCConditionalReadonly(nameof(SeqTipoOferta), SMCConditionalOperation.Equals, "false", "", DataAttribute = "formacaoespecifica", RuleName = "R1")]
        [SMCConditionalReadonly(nameof(SeqsEntidadeResponsavel), SMCConditionalOperation.NotEqual, TOKEN_TIPO_ENTIDADE_EXTERNADA.PROGRAMA, DataAttribute = "tipoentidade", RuleName = "R2")]
        [SMCConditionalReadonly(nameof(SeqCursoOferta), "", RuleName = "R3")]
        [SMCConditionalRule("R1 || !(!R1 && (!R2 || !R3))")]
        public SMCLookupViewModel FormacaoEspecifica { get; set; }

        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid7_24)]
        [SMCConditionalReadonly(nameof(SeqsEntidadeResponsavel), SMCConditionalOperation.Equals, null, RuleName = "R1")]
        [SMCConditionalReadonly(nameof(SeqCursoOferta), SMCConditionalOperation.Equals, "", RuleName = "R2")]
        [SMCConditionalRule("R1 && R2")]
        [SMCDependency(nameof(SeqsEntidadeResponsavel), "BuscarColaboradoresOrientacao", "Colaborador", "DCT", false, new[] { nameof(SeqCursoOferta), nameof(SeqLocalidade) })]
        [SMCDependency(nameof(SeqCursoOferta), "BuscarColaboradoresOrientacao", "Colaborador", "DCT", false, new[] { nameof(SeqsEntidadeResponsavel), nameof(SeqLocalidade) })]
        [SMCDependency(nameof(SeqLocalidade), "BuscarColaboradoresOrientacao", "Colaborador", "DCT", false, new[] { nameof(SeqCursoOferta), nameof(SeqsEntidadeResponsavel) })]
        [SMCSelect]
        public long? SeqOrientador { get; set; }

        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid6_24)]
        public string Turma { get; set; }

        #endregion
    }
}
