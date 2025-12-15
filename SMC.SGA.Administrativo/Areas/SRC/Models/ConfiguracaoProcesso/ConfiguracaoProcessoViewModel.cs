using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ConfiguracaoProcessoViewModel : SMCWizardViewModel, ISMCStatefulView
    {
        #region Datasource    

        public List<SMCDatasourceItem> NiveisEnsinoDataSource { get; set; }

        public List<SMCDatasourceItem> TiposVinculoDataSource { get; set; }

        public List<SMCDatasourceItem> TurnosDataSource { get; set; }

        public List<SMCDatasourceItem> TiposConfiguracaoDataSource { get; set; }

        #endregion

        #region Processo

        [SMCHidden]
        public long SeqProcesso { get; set; }

        [SMCHidden]
        public string DescricaoProcesso { get; set; }

        [SMCHidden]
        public bool ProcessoEncerrado { get; set; }

        #endregion       

        [SMCRequired]
        [SMCSelect(nameof(TiposConfiguracaoDataSource))]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid10_24)]
        public long SeqTipoConfiguracao { get; set; }

        [SMCReadOnly]
        [SMCKey]
        [SMCRequired]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid3_24, SMCSize.Grid3_24)]
        public long Seq { get; set; }

        [SMCDescription]
        [SMCRequired]
        [SMCMaxLength(255)]
        [SMCConditionalReadonly(nameof(ProcessoEncerrado), true, PersistentValue = true)]
        public string Descricao { get; set; }

        [SMCHidden]
        public bool ExibirSecaoNivelEnsino { get; set; }

        [SMCDetail]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCConditionalDisplay(nameof(ExibirSecaoNivelEnsino), SMCConditionalOperation.Equals, true)]
        [SMCConditionalReadonly(nameof(ProcessoEncerrado), true, PersistentValue = true)]
        public SMCMasterDetailList<ConfiguracaoProcessoNivelEnsinoViewModel> NiveisEnsino { get; set; }

        [SMCDetail]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCConditionalReadonly(nameof(ProcessoEncerrado), true, PersistentValue = true)]
        public SMCMasterDetailList<ConfiguracaoProcessoTipoVinculoAlunoViewModel> TiposVinculoAluno { get; set; }

        [SMCHidden]
        public bool ListarDepartamentosGruposProgramas { get; set; } = true;

        [SMCHidden]
        public List<long> SeqsEntidadesResponsaveis { get; set; }

        [SMCHidden]
        public List<long> SeqsNiveisEnsino { get; set; }

        [SMCHidden]
        public bool ExibirSecaoCursos { get; set; }

        [SMCDetail]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCConditionalDisplay(nameof(ExibirSecaoCursos), SMCConditionalOperation.Equals, true)]
        [SMCConditionalReadonly(nameof(ProcessoEncerrado), true, PersistentValue = true)]
        public SMCMasterDetailList<ConfiguracaoProcessoCursoViewModel> Cursos { get; set; }

        public string Mensagem { get; set; }
    }
}