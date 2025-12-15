using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;

namespace SMC.SGA.BDP.Models
{
    public class PesquisaViewModel : SMCPagerViewModel
    {
        #region Datasources
        public List<SMCDatasourceItem> TiposTrabalho { get; set; }

        public List<SMCDatasourceItem> AreasConhecimento { get; set; }

        public List<SMCDatasourceItem> Programas { get; set; }

        public List<TipoPesquisaTrabalhoAcademico> TiposPesquisaTrabalho { get; set; }

        #endregion

        public long SeqInstituicaoLogada { get; set; }

        public string DescricaoInstituicaoLogada { get; set; }

        public string PalavraChave { get; set; }

        public bool? PesquisaPalavraChave { get; set; }

        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid7_24)]
        public string Nome { get; set; }

        [SMCConditionalReadonly(nameof(Nome), SMCConditionalOperation.Equals, "")]
        [SMCFilter(true, true)]
        [SMCSelect(nameof(TiposPesquisaTrabalho))]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
        public List<TipoPesquisaTrabalhoAcademico> TipoPesquisaTrabalho { get; set; }

        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid8_24)]
        public string TituloResumo { get; set; }

        [SMCFilter(true, true)]
        [SMCSelect(nameof(TiposTrabalho), NameDescriptionField = nameof(TipoTrabalho))]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        public List<long> SeqsTipoTrabalho { get; set; }

        public List<string> TipoTrabalho { get; set; }

        [SMCFilter(true, true)]
        [SMCSelect(nameof(AreasConhecimento), NameDescriptionField = nameof(AreaConhecimento))]
        [SMCSize(SMCSize.Grid11_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid10_24)]
        public long? SeqAreaConhecimento { get; set; }

        public string AreaConhecimento { get; set; }

        [SMCFilter(true, true)]
        [SMCSelect(nameof(Programas), NameDescriptionField = nameof(Programa))]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        public long? SeqPrograma { get; set; }

        public string Programa { get; set; }

        [SMCConditionalRequired(nameof(DataFim), SMCConditionalOperation.NotEqual, "")]
        [SMCFilter(true, true)]
        [SMCMaxDate(nameof(DataFim))]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
        public DateTime? DataInicio { get; set; }

        [SMCConditionalRequired(nameof(DataInicio), SMCConditionalOperation.NotEqual, "")]
        [SMCFilter(true, true)]
        [SMCMinDate(nameof(DataInicio))]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
        public DateTime? DataFim { get; set; }

        public string Letra { get; set; }

        public bool? PesquisaDetalhada { get; set; }

        public bool? EmPublicacao { get; set; }

        public bool? EmFuturasDefesas { get; set; }

        public bool? UltimasPublicacoes { get; set; }

        public List<string> FiltrosDescricao { get; set; }

        public bool ExibirCoorientador { get; set; }

        #region campos hidden para ordenação

        [SMCHidden]
        [SMCSortable(true, true, SMCSortDirection.Ascending)]
        public string Titulo { get; set; }

        [SMCHidden]
        [SMCSortable(true, true, SMCSortDirection.Ascending)]
        public string Autor { get; set; }

        [SMCHidden]
        [SMCSortable(true, true, SMCSortDirection.Ascending)]
        public string Orientador { get; set; }

        [SMCHidden]
        [SMCSortable(true, true, SMCSortDirection.Ascending)]
        public string Data { get; set; }

        #endregion
    }
}