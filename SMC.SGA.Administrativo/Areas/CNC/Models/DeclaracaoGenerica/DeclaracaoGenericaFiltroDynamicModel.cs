using SMC.Framework.Model;
using System.Collections.Generic;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Academico.UI.Mvc.Areas.PES.Lookups;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using System;
using SMC.Framework.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CNC.Models
{
    public class DeclaracaoGenericaFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        #region DataSources 

        public List<SMCDatasourceItem> EntidadesResponsaveis { get; set; }

        public List<SMCDatasourceItem> TiposDocumento { get; set; }

        public List<SMCDatasourceItem> Situacoes { get; set; }

        #endregion

        [PessoaLookup(ModalWindowSize = SMCModalWindowSize.Largest)]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid9_24, SMCSize.Grid24_24, SMCSize.Grid9_24, SMCSize.Grid9_24)]
        public PessoaLookupViewModel PessoaLookup { get; set; }

        [SMCHidden]
        public long? SeqPessoa
        {
            get { return PessoaLookup?.Seq; }
        }

        [SMCSelect(nameof(EntidadesResponsaveis))]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public List<long?> SeqsEntidadesResponsaveis { get; set; }

        [CursoOfertaLocalidadeLookup]
        [SMCDependency(nameof(SeqsEntidadesResponsaveis))]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid9_24,   SMCSize.Grid24_24, SMCSize.Grid9_24, SMCSize.Grid9_24)]
        public CursoOfertaLocalidadeLookupViewModel SeqCursoOfertaLocalidade { get; set; }

        [SMCHidden]
        public long? SeqCursoOfertaLocalidadeParam
        {
            get { return SeqCursoOfertaLocalidade?.Seq; }
        }

        [SMCSelect(nameof(TiposDocumento))]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public List<long?> SeqsTiposDocumentos { get; set; }

        [SMCSelect(nameof(Situacoes))]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public long? SeqSituacaoAtual { get; set; }

        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        [SMCDateTimeMode(SMCDateTimeMode.Date)]
        [SMCMaxDateNow]
        [SMCMaxDate(nameof(DataFimInclusao))]
        [SMCConditionalRequired(nameof(DataFimInclusao), SMCConditionalOperation.NotEqual, "")]
        public DateTime? DataInicioInclusao { get; set; }


        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        [SMCDateTimeMode(SMCDateTimeMode.Date)]
        [SMCMinDate(nameof(DataInicioInclusao))]
        [SMCMaxDateNow]
        [SMCConditionalRequired(nameof(DataInicioInclusao), SMCConditionalOperation.NotEqual, "")]
        public DateTime? DataFimInclusao { get; set; }

    }
}