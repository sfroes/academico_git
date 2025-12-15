using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.EstruturaOrganizacional.UI.Mvc.Areas.ESO.Lookups;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.ORG.Models;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Models
{
    public abstract class EntidadeViewModel : SMCDynamicViewModel, ISMCMappable, ISMCStep
    {
        public abstract long SeqTipoEntidade { get; set; }

        [SMCIgnoreProp]
        public virtual int Step { get; set; }

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public virtual long SeqInstituicaoEnsino { get; set; }

        [SMCOrder(1)]
        [SMCStep(1, 0)]
        [SMCSize(SMCSize.Grid6_24)]
        [SMCImage(thumbnailHeight: 100, thumbnailWidth: 0, manualUpload: false, maxFileSize: 225651471, AreaDownload = "", ActionDownload = "DownloadFileGuid", ControllerDownload = "Home")]
        [SMCConditionalDisplay(nameof(LogotipoVisivel), true)]
        [SMCConditionalRequired(nameof(LogotipoObrigatorio), true)]
        public SMCUploadFile ArquivoLogotipo { get; set; }

        [SMCHidden]
        [SMCOrder(3)]
        [SMCStep(1, 0)]
        public long? SeqArquivoLogotipo { get; set; }

        [SMCOrder(4)]
        [SMCStep(1, 0)]
        [SMCDescription]
        [SMCRequired]
        [SMCSize(SMCSize.Grid14_24)]
        [SMCMaxLength(100)]
        public virtual string Nome { get; set; }

        [SMCOrder(5)]
        [SMCStep(1, 0)]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCMaxLength(15)]
        [SMCConditionalDisplay(nameof(SiglaVisivel), true)]
        [SMCConditionalRequired(nameof(SiglaObrigatoria), true)]
        public virtual string Sigla { get; set; }

        [SMCOrder(6)]
        [SMCStep(1, 0)]
        [SMCSize(SMCSize.Grid6_24)]
        [SMCMaxLength(50)]
        [SMCConditionalDisplay(nameof(NomeReduzidoVisivel), true)]
        [SMCConditionalRequired(nameof(NomeReduzidoObrigatorio), true)]
        public virtual string NomeReduzido { get; set; }

        [SMCOrder(7)]
        [SMCStep(1, 0)]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCMaxLength(100)]
        [SMCConditionalDisplay(nameof(NomeComplementarVisivel), true)]
        [SMCConditionalRequired(nameof(NomeComplementarObrigatorio), true)]
        public virtual string NomeComplementar { get; set; }

        [SMCOrder(8)]
        [SMCStep(1, 0)]
        [SMCConditionalDisplay(nameof(UnidadeSeoVisivel), true)]
        [SMCConditionalRequired(nameof(UnidadeSeoObrigatorio), true)]
        [SMCSize(SMCSize.Grid9_24)]
        [SMCInclude(true)] // O Dynamic gera include automático dos lookups, ignorado por ser uma entidade externa
        [UnidadeLookup]
        public virtual UnidadeLookupViewModel CodigoUnidadeSeo { get; set; }

        /// <summary>
        /// Sequenciais das hierarquias entidade itens
        /// Quando informado, serão retornados os ramos dos intens de classificação informados nas entidades responsáveis.
        /// Caso contrário restá retornada a hierarquia inteira.
        /// </summary>
        [SMCHidden]
        public long[] SeqsHierarquiaEntidadeItem { get; set; }

        [SMCHidden]
        public virtual long? SeqEntidadeResponsavel { get; set; }

        public List<EntidadeClassificacoesViewModel> Hierarquias { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        [SMCStep(1, 0)]
        [SMCOrder(9)]
        [SMCSelect(nameof(UnidadesResponsaveisAGD))]
        [SMCConditionalDisplay(nameof(UnidadeAgdVisivel), true)]
        [SMCConditionalRequired(nameof(UnidadeAgdObrigatorio), true)]
        public virtual long? SeqUnidadeResponsavelAgd { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(IEntidadeService), nameof(IEntidadeService.BuscarUnidadesResponsaveisAGDSelect))]
        public List<SMCDatasourceItem> UnidadesResponsaveisAGD { get; set; }

        [SMCSize(SMCSize.Grid12_24)]
        [SMCStep(1, 0)]
        [SMCOrder(10)]
        [SMCSelect(nameof(UnidadesResponsaveisGPI))]
        [SMCConditionalDisplay(nameof(UnidadeGpiVisivel), true)]
        [SMCConditionalRequired(nameof(UnidadeGpiObrigatorio), true)]
        public virtual long? SeqUnidadeResponsavelGpi { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(IEntidadeService), nameof(IEntidadeService.BuscarUnidadesResponsaveisGPISelect))]
        public List<SMCDatasourceItem> UnidadesResponsaveisGPI { get; set; }

        [SMCSize(SMCSize.Grid12_24)]
        [SMCStep(1, 0)]
        [SMCOrder(11)]
        [SMCSelect(nameof(UnidadesResponsaveisNotificacoes))]
        [SMCConditionalDisplay(nameof(UnidadeNotificacaoVisivel), true)]
        [SMCConditionalRequired(nameof(UnidadeNotificacaoObrigatorio), true)]
        public long? SeqUnidadeResponsavelNotificacao { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(IEntidadeService), nameof(IEntidadeService.BuscarUnidadesResponsaveisNotificacaoSelect))]
        public List<SMCDatasourceItem> UnidadesResponsaveisNotificacoes { get; set; }

        [SMCSize(SMCSize.Grid12_24)]
        [SMCStep(1, 0)]
        [SMCOrder(12)]
        [SMCSelect(nameof(UnidadesResponsaveisFormularios))]
        [SMCConditionalDisplay(nameof(UnidadeFormularioVisivel), true)]
        [SMCConditionalRequired(nameof(UnidadeFormularioObrigatorio), true)]
        public virtual long? SeqUnidadeResponsavelFormulario { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(IEntidadeService), nameof(IEntidadeService.BuscarUnidadesResponsaveisFormularioSelect))]
        public List<SMCDatasourceItem> UnidadesResponsaveisFormularios { get; set; }

        #region Configurações de Visibilidade e Obrigatoriedade

        [SMCStep(1, 0)]
        [SMCHidden]
        [SMCOrder(0)]
        public bool LogotipoVisivel { get; set; }

        [SMCStep(1, 0)]
        [SMCHidden]
        [SMCOrder(0)]
        public bool SiglaVisivel { get; set; }

        [SMCStep(1, 0)]
        [SMCHidden]
        [SMCOrder(0)]
        public bool UnidadeSeoVisivel { get; set; }

        [SMCStep(1, 0)]
        [SMCHidden]
        [SMCOrder(0)]
        public bool UnidadeAgdVisivel { get; set; }

        [SMCStep(1, 0)]
        [SMCHidden]
        [SMCOrder(0)]
        public bool NomeReduzidoVisivel { get; set; }

        [SMCStep(1, 0)]
        [SMCHidden]
        [SMCOrder(0)]
        public bool NomeComplementarVisivel { get; set; }

        [SMCStep(1, 0)]
        [SMCHidden]
        [SMCOrder(0)]
        public bool LogotipoObrigatorio { get; set; }

        [SMCStep(1, 0)]
        [SMCOrder(0)]
        [SMCHidden]
        public bool SiglaObrigatoria { get; set; }

        [SMCStep(1, 0)]
        [SMCHidden]
        [SMCOrder(0)]
        public bool UnidadeSeoObrigatorio { get; set; }

        [SMCStep(1, 0)]
        [SMCHidden]
        [SMCOrder(0)]
        public bool UnidadeAgdObrigatorio { get; set; }

        [SMCStep(1, 0)]
        [SMCOrder(0)]
        [SMCHidden]
        public bool NomeReduzidoObrigatorio { get; set; }

        [SMCStep(1, 0)]
        [SMCOrder(0)]
        [SMCHidden]
        public bool NomeComplementarObrigatorio { get; set; }

        [SMCStep(1, 0)]
        [SMCHidden]
        [SMCOrder(0)]
        public bool UnidadeGpiVisivel { get; set; }

        [SMCStep(1, 0)]
        [SMCHidden]
        [SMCOrder(0)]
        public bool? UnidadeGpiObrigatorio { get; set; }

        [SMCStep(1, 0)]
        [SMCHidden]
        [SMCOrder(0)]
        public bool UnidadeNotificacaoVisivel { get; set; }

        [SMCStep(1, 0)]
        [SMCHidden]
        [SMCOrder(0)]
        public bool? UnidadeNotificacaoObrigatorio { get; set; }

        [SMCStep(1, 0)]
        [SMCHidden]
        [SMCOrder(0)]
        public bool UnidadeFormularioVisivel { get; set; }

        [SMCStep(1, 0)]
        [SMCHidden]
        [SMCOrder(0)]
        public bool? UnidadeFormularioObrigatorio { get; set; }

        #endregion Configurações de Visibilidade e Obrigatoriedade
    }
}