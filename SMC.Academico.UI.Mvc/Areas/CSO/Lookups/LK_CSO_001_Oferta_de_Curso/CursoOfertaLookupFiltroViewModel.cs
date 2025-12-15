using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.UI.Mvc.Areas.CSO.Lookups
{
    [SMCGroupedPropertyConfiguration(GroupId = "LookupCursoOfertaFiltroCurso", Size = SMCSize.Grid24_24)]
    [SMCGroupedPropertyConfiguration(GroupId = "LookupCursoOfertaFiltroCursoOferta", Size = SMCSize.Grid24_24)]

    public class CursoOfertaLookupFiltroViewModel : SMCLookupFilterViewModel
    {

        #region [ DataSources ]

        [SMCDataSource(SMCStorageType.None)]
        public List<SMCDatasourceItem> NiveisEnsino { get; set; }

        [SMCDataSource(SMCStorageType.None)]
        public List<SMCDatasourceItem> EntidadesResponsaveis { get; set; }

        [SMCDataSource(SMCStorageType.None)]
        public List<SMCDatasourceItem> Situacoes { get; set; }

        [SMCDataSource(SMCStorageType.None)]
        public List<SMCDatasourceItem> TiposFormacaoEspecifica { get; set; }

        #endregion [ DataSources ]

        [SMCKey]
        [SMCHidden]
        public long? Seq { get; set; }

        [SMCHidden]
        public long? SeqEntidadeResponsavelFormacao { get; set; }

        [SMCHidden]
        public long? SeqLocalidade { get; set; }

        [SMCConditionalReadonly(nameof(SeqCursoIDLeitura), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCGroupedProperty("LookupCursoOfertaFiltroCurso")]
        [SMCMapProperty("SeqCurso")]
        [SMCOrder(1)]
        [SMCSize(SMCSize.Grid2_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid2_24)]
        public long? SeqCursoID { get; set; }

        [SMCConditionalReadonly(nameof(NomeLeitura), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCGroupedProperty("LookupCursoOfertaFiltroCurso")]
        [SMCMaxLength(100)]
        [SMCOrder(2)]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid20_24, SMCSize.Grid7_24)]
        public string Nome { get; set; }
        
        /// <summary>
        /// Sequenciais dos grupos de programas pais dos programas responsáveis pelos cursos
        /// </summary>
        [SMCIgnoreProp]
        public List<long> SeqsGruposProgramasResponsaveis { get; set; }

        /// <summary>
        /// Sequencial do HierarquiaItem que representa a entidade responsável pelo curso
        /// Modificar na documentação para ser combo no lugar de lookup
        /// </summary>
        [SMCConditionalReadonly(nameof(SeqEntidadeResponsavelLeitura), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCGroupedProperty("LookupCursoOfertaFiltroCurso")]
        [SMCOrder(3)]
        [SMCSelect(nameof(EntidadesResponsaveis), UseCustomSelect = true)]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid7_24)]
        public List<long> SeqsEntidadesResponsaveis { get; set; }

        [SMCConditionalReadonly(nameof(SeqNivelEnsinoLeitura), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCGroupedProperty("LookupCursoOfertaFiltroCurso")]
        [SMCOrder(4)]
        [SMCSelect(nameof(NiveisEnsino))]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid4_24)]
        public List<long> SeqNivelEnsino { get; set; }

        [SMCConditionalReadonly(nameof(SeqNivelEnsino), SMCConditionalOperation.Equals, null)]
        [SMCGroupedProperty("LookupCursoOfertaFiltroCurso")]
        [SMCOrder(5)]
        [SMCSelect(nameof(Situacoes))]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid4_24)]
        public long? SeqSituacaoAtual { get; set; }

        [SMCConditionalReadonly(nameof(SeqTipoFormacaoEspecificaLeitura), SMCConditionalOperation.Equals, true)]
        [SMCDependency(nameof(SeqNivelEnsino), "BuscarTipoFormacaoEspecificaPorNivelEnsinoSelect", "TipoFormacaoEspecifica", "CSO", false)]
        [SMCGroupedProperty("LookupCursoOfertaFiltroCursoOferta")]
        [SMCOrder(6)]
        [SMCSelect(nameof(TiposFormacaoEspecifica))]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        public long? SeqTipoFormacaoEspecifica { get; set; }

        [SMCDescription]
        [SMCGroupedProperty("LookupCursoOfertaFiltroCursoOferta")]
        [SMCOrder(7)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        public string Descricao { get; set; }

        [SMCGroupedProperty("LookupCursoOfertaFiltroCursoOferta")]
        [SMCOrder(8)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        public bool? Ativo { get; set; }

        #region [ Readonly ]

        [SMCHidden]
        public bool SeqCursoIDLeitura { get; set; }

        [SMCHidden]
        public bool NomeLeitura { get; set; }

        [SMCHidden]
        public bool SeqEntidadeResponsavelLeitura { get; set; }

        [SMCHidden]
        public bool SeqNivelEnsinoLeitura { get; set; }

        [SMCHidden]
        public bool SeqSituacaoAtualLeitura { get; set; }

        [SMCDependency(nameof(SeqNivelEnsino))]
        [SMCHidden]
        public bool SeqSituacaoLeitura { get { return SeqSituacaoAtualLeitura || !(SeqNivelEnsino != null && SeqNivelEnsino.Any()); } }

        [SMCHidden]
        public bool SeqTipoFormacaoEspecificaLeitura { get; set; }

        [SMCHidden]
        public bool RetornarTodos { get; set; }

        /// <summary>
        /// Valida se ira buscar curso oferta de entidades ativas, caso queira procucar todas as entidades ativas e inativas somente passar false
        /// </summary>
        [SMCHidden]
        public bool EntidadesAtivas { get; set; } = true;

        #endregion [ Readonly ]
    }
}