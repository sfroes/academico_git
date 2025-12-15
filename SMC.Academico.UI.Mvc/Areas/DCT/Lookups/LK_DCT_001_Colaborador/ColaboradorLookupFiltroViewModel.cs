using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.DCT.Lookups
{
    [SMCGroupedPropertyConfiguration(GroupId = "FiltroColaborador", Size = SMCSize.Grid24_24)]
    [SMCGroupedPropertyConfiguration(GroupId = "FiltroVinculo", Size = SMCSize.Grid24_24)]
    [SMCGroupedPropertyConfiguration(GroupId = "FiltroAtividade", Size = SMCSize.Grid12_24)]
    [SMCGroupedPropertyConfiguration(GroupId = "FiltroInstituicao", Size = SMCSize.Grid12_24)]
    public class ColaboradorLookupFiltroViewModel : SMCLookupFilterViewModel
    {
        #region [ DataSources ]

        [SMCDataSource(SMCStorageType.None)]
        public List<SMCDatasourceItem> EntidadesColaborador { get; set; }

        [SMCDataSource(SMCStorageType.None)]
        public List<SMCDatasourceItem> TiposVinculoColaborador { get; set; }

        [SMCDataSource(SMCStorageType.None)]
        public List<SMCDatasourceItem> TiposAtividadeColaborador { get; set; }

        #endregion [ DataSources ]

        [SMCHidden]
        public string TokeEntidadeVinculo { get; set; }

        [SMCHidden]
        public bool? VinculoAtivo { get; set; }

        [SMCGroupedProperty("FiltroColaborador")]
        [SMCOrder(1)]
        [SMCSize(SMCSize.Grid2_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid2_24)]
        [SMCKey]
        public long? Seq { get; set; }

        [SMCGroupedProperty("FiltroColaborador")]
        [SMCOrder(2)]
        [SMCSize(SMCSize.Grid11_24, SMCSize.Grid24_24, SMCSize.Grid20_24, SMCSize.Grid11_24)]
        [SMCDescription]
        public string Nome { get; set; }

        [SMCGroupedProperty("FiltroColaborador")]
        [SMCOrder(3)]
        [SMCSize(SMCSize.Grid11_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid11_24)]
        public string NomeSocial { get; set; }

        [SMCConditionalReadonly(nameof(SeqEntidadeVinculoReadOnly), true, PersistentValue = true)]
        [SMCGroupedProperty("FiltroVinculo")]
        [SMCOrder(4)]
        [SMCSelect(nameof(EntidadesColaborador))]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid13_24)]
        public long? SeqEntidadeVinculo { get; set; }

        [SMCGroupedProperty("FiltroVinculo")]
        [SMCOrder(5)]
        [SMCDependency(nameof(SeqEntidadeVinculo), "BuscarTiposVinculoColaboradorLookupSelect", "ColaboradorVinculo", "DCT", false, includedProperties: new[] { nameof(CriaVinculoInstitucional) })]
        [SMCSelect(nameof(TiposVinculoColaborador))]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid14_24, SMCSize.Grid5_24)]
        public long? SeqTipoVinculoColaborador { get; set; }

        [SMCGroupedProperty("FiltroVinculo")]
        [SMCConditionalRequired(nameof(DataFim), SMCConditionalOperation.NotEqual, "")]
		[SMCConditionalReadonly(nameof(DataInicioReadOnly), true, PersistentValue = true)]
		[SMCOrder(6)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid5_24, SMCSize.Grid3_24)]
        public DateTime? DataInicio { get; set; }

        [SMCGroupedProperty("FiltroVinculo")]
        [SMCConditionalRequired(nameof(DataInicio), SMCConditionalOperation.NotEqual, "")]
		[SMCConditionalReadonly(nameof(DataFimReadOnly), true, PersistentValue = true)]
		[SMCMinDate(nameof(DataInicio))]
        [SMCOrder(7)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid5_24, SMCSize.Grid3_24)]
        public DateTime? DataFim { get; set; }

        [SMCDependency(nameof(SeqEntidadeVinculo))]
        [SMCHidden]
        public long? SeqsEntidadesResponsaveis => SeqEntidadeVinculo;

        [CursoOfertaLocalidadeLookup]
        [SMCDependency(nameof(SeqsEntidadesResponsaveis))]
        [SMCGroupedProperty("FiltroAtividade")]
        [SMCOrder(8)]
        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid16_24)]
        public CursoOfertaLocalidadeLookupViewModel SeqCursoOfertaLocalidade { get; set; }

        [SMCGroupedProperty("FiltroAtividade")]
        [SMCOrder(9)]
        [SMCSelect(nameof(TiposAtividadeColaborador))]
        [SMCConditionalReadonly(nameof(TipoAtividadeReadOnly), true, PersistentValue = true)]
        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid8_24)]
        public TipoAtividadeColaborador? TipoAtividade { get; set; }

        [SMCHidden]
        public bool RetornarInstituicaoEnsinoLogada { get; set; } = true;

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long? SeqInstituicaoEnsino { get; set; }

        [InstituicaoExternaLookup]
        [SMCDependency(nameof(RetornarInstituicaoEnsinoLogada))]
        [SMCDependency(nameof(SeqInstituicaoEnsino))]
        [SMCGroupedProperty("FiltroInstituicao")]
        [SMCOrder(10)]
        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid16_24)]
        public InstituicaoExternaViewModel SeqInstituicaoExterna { get; set; }

        [SMCDependency(nameof(SeqInstituicaoExterna), "BuscarSituacoesColaborador", "Colaborador", "DCT", true)]
        [SMCGroupedProperty("FiltroInstituicao")]
        [SMCOrder(11)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid8_24)]
        public SituacaoColaborador? SituacaoColaboradorNaInstituicao { get; set; }

        [SMCHidden]
        public OrigemColaborador? OrigemColaborador { get; set; }

        [SMCHidden]
        public bool? Oritentador { get; set; }

        [SMCHidden]
        public bool? TipoEntidadePermiteVinculo { get; set; }

        /// <summary>
        /// Caso o tipo de atividade já esteja preenchido
        /// </summary>
        [SMCHidden]
        public bool TipoAtividadeReadOnly { get; set; }

        /// <summary>
        /// Caso o entidade vinculo já esteja preenchido
        /// </summary>
        [SMCHidden]
        public bool SeqEntidadeVinculoReadOnly { get; set; }

        [SMCKeyModel]
        [SMCHidden]
        public long?[] Seqs { get; set; }

        [SMCHidden]
        public bool? CriaVinculoInstitucional { get; set; }

        [SMCHidden]
        public long? SeqTurma { get; set; }

        [SMCHidden]
        public bool DataInicioReadOnly { get; set; }

        [SMCHidden]
        public bool DataFimReadOnly { get; set; }

        [SMCHidden]
        public DateTime? DataInicioVinculo { get; set; }

        [SMCHidden]
        public DateTime? DataFimVinculo { get; set; }
    }
}