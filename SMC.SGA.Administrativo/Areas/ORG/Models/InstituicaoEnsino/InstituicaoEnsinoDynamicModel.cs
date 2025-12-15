using SMC.Academico.Common.Areas.APR.Constants;
using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.ORG.Constants;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.UI.Mvc.Areas.ORG.InterfaceBlocks;
using SMC.Academico.UI.Mvc.Areas.ORG.Models;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.EstruturaOrganizacional.UI.Mvc.Areas.ESO.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.Localidades.Common.Areas.LOC.Enums;
using SMC.Localidades.UI.Mvc.DataAnnotation;
using SMC.Localidades.UI.Mvc.Models;
using SMC.SGA.Administrativo.Models;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    [SMCStepConfiguration]
    [SMCStepConfiguration]
    [SMCStepConfiguration(Partial = "_AtoNormativo")]
    public class InstituicaoEnsinoDynamicModel : EntidadeViewModel, IAtoNormativoBI
    {
        public InstituicaoEnsinoDynamicModel()
        {
            this.Ativo = true;

            // Configura as visibilidades e obrigatoriedades
            this.LogotipoVisivel = true;
            this.LogotipoObrigatorio = true;
            this.SiglaVisivel = true;
            this.SiglaObrigatoria = true;
            this.NomeReduzidoVisivel = true;
            this.NomeReduzidoObrigatorio = true;
            this.UnidadeAgdVisivel = true;
            this.UnidadeAgdObrigatorio = true;

            // Inclui um endereço comercial
            Enderecos = new AddressList();
            Enderecos.Add(new InformacoesEnderecoViewModel()
            {
                TipoEndereco = (short)TipoEndereco.Comercial,
                Correspondencia = true
            });

            // Inclui um telefone comercial
            Telefones = new SMCMasterDetailList<TelefoneCategoriaViewModel>();
            Telefones.Add(new TelefoneCategoriaViewModel() { DescricaoTipoTelefone = TipoTelefone.Comercial.SMCGetDescription() });

            // Incluir dois endereços eletronicos: email e website
            EnderecosEletronicos = new SMCMasterDetailList<EnderecoEletronicoCategoriaViewModel>();
            EnderecosEletronicos.Add(new EnderecoEletronicoCategoriaViewModel() { DescricaoTipoEnderecoEletronico = TipoEnderecoEletronico.Email.SMCGetDescription() });
            EnderecosEletronicos.Add(new EnderecoEletronicoCategoriaViewModel() { DescricaoTipoEnderecoEletronico = TipoEnderecoEletronico.Website.SMCGetDescription() });
        }

        /// <summary>
        /// Apenas na instituição de ensino o valor da instituição de ensino não pode ser preenchido com o dataFilter, pois este deve ser o sequencial do próprio registro
        /// </summary>
        [SMCHidden]
        [SMCStep(0)]
        public override long SeqInstituicaoEnsino { get; set; }

        [SMCHidden]
        [SMCStep(0)]
        public override long SeqTipoEntidade { get; set; }

        [SMCKey]
        [SMCOrder(2)]
        [SMCReadOnly]
        [SMCRequired]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid3_24, SMCSize.Grid3_24)]
        [SMCStep(0)]
        public override long Seq { get; set; }

        //Propriedade idêntica a da classe pai. Duplicada apenas para alterar o size no componente na tela
        [SMCOrder(8)]
        [SMCStep(1, 0)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid18_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid18_24)]
        [SMCInclude(true)]
        [UnidadeLookup]
        public override UnidadeLookupViewModel CodigoUnidadeSeo { get; set; }

        //O conditional display não estava ocultando o campo, remoção forçada na herança
        [SMCIgnoreProp]
        public override string NomeComplementar { get; set; }

        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid13_24, SMCSize.Grid6_24)]
        [SMCStep(1, 0)]
        [SMCOrder(9)]
        [SMCSelect(nameof(UnidadesResponsaveisAGD))]
        [SMCConditionalDisplay(nameof(UnidadeAgdVisivel), true)]
        [SMCConditionalRequired(nameof(UnidadeAgdObrigatorio), true)]
        public override long? SeqUnidadeResponsavelAgd { get; set; }

        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        [SMCStep(1, 0)]
        [SMCOrder(10)]
        [SMCSelect(nameof(UnidadesResponsaveisGPI))]
        [SMCConditionalDisplay(nameof(UnidadeGpiVisivel), true)]
        [SMCConditionalRequired(nameof(UnidadeGpiObrigatorio), true)]
        public override long? SeqUnidadeResponsavelGpi { get; set; }

        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        [SMCStep(1, 0)]
        [SMCOrder(11)]
        [SMCSelect(nameof(UnidadesResponsaveisFormularios))]
        [SMCConditionalDisplay(nameof(UnidadeFormularioVisivel), true)]
        [SMCConditionalRequired(nameof(UnidadeFormularioObrigatorio), true)]
        public override long? SeqUnidadeResponsavelFormulario { get; set; }

        [SMCOrder(3)]
        [SMCStep(1, 0)]
        [SMCDescription]
        [SMCRequired]
        [SMCSize(SMCSize.Grid15_24, SMCSize.Grid24_24, SMCSize.Grid13_24, SMCSize.Grid15_24)]
        [SMCMaxLength(100)]
        public override string Nome { get; set; }


        [SMCOrder(5)]
        [SMCStep(1, 0)]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid3_24, SMCSize.Grid3_24)]
        [SMCMaxLength(15)]
        [SMCConditionalDisplay(nameof(SiglaVisivel), true)]
        [SMCConditionalRequired(nameof(SiglaObrigatoria), true)]
        public override string Sigla { get; set; }

        [SMCOrder(6)]
        [SMCStep(1, 0)]
        [SMCSize(SMCSize.Grid15_24, SMCSize.Grid24_24, SMCSize.Grid13_24, SMCSize.Grid15_24)]
        [SMCMaxLength(50)]
        [SMCConditionalDisplay(nameof(NomeReduzidoVisivel), true)]
        [SMCConditionalRequired(nameof(NomeReduzidoObrigatorio), true)]
        public override string NomeReduzido { get; set; }

        [SMCOrder(9)]
        [SMCRequired]
        [SMCSelect("Mantenedoras", "Seq", "Nome")]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid6_24)]
        [SMCStep(0)]
        public long SeqMantenedora { get; set; }

        [SMCDataSource("Mantenedora")]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IORGDynamicService))]
        public List<SMCDatasourceItem> Mantenedoras { get; set; }

        [SMCOrder(10)]
        [SMCRequired]
        [SMCSelect("CategoriasInstituicao", "Seq", "Descricao")]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        [SMCStep(0)]
        public long SeqCategoriaInstituicao { get; set; }

        [SMCDataSource("CategoriaInstituicaoEnsino")]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IDCTDynamicService))]
        public List<SMCDatasourceItem> CategoriasInstituicao { get; set; }

        [SMCDataSource("HierarquiaClassificacao")]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ICSODynamicService))]
        public List<SMCDatasourceItem> HierarquiasClassificacao { get; set; }

        [SMCMapForceFromTo]
        [SMCOrder(11)]
        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid4_24)]
        [SMCStep(0)]
        public bool? Ativo { get; set; } = true;

        [SMCConditionalDisplay("Ativo", false)]
        [SMCConditionalRequired("Ativo", false)]
        [SMCOrder(12)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        [SMCMaxDateNow]
        [SMCStep(0)]
        public DateTime? DataDesativacao { get; set; }

        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid10_24)]
        [SMCSelect]
        [SMCStep(0)]
        [SMCOrder(13)]
        [SMCRequired]
        public IntegracaoBiblioteca IntegracaoBiblioteca { get; set; }

        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid14_24)]
        [SMCStep(0)]
        [SMCOrder(14)]
        [SMCEmail]
        [SMCRequired]
        public string EmailContatoBdp { get; set; }

        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid10_24)]
        [SMCStep(0)]
        [SMCOrder(15)]
        [SMCRequired]
        [SMCSelect("HierarquiasClassificacao", "Seq", "Descricao")]
        public long SeqHierarquiaClassificacaoFormacaoAcademica { get; set; }

        [Address(min: 1, Correspondence = true, TiposEnderecos = new TipoEndereco[] { TipoEndereco.Comercial, TipoEndereco.Outro })]
        [SMCMapForceFromTo]
        [SMCCssClass("smc-sga-detalhe-editavel-blocos-endereco-responsivo")]
        [SMCOrder(16)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCStep(1)]
        public AddressList Enderecos { get; set; }

        [SMCDetail]
        [SMCMapForceFromTo]
        [SMCOrder(17)]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCStep(1)]
        public SMCMasterDetailList<TelefoneCategoriaViewModel> Telefones { get; set; }

        [SMCDetail]
        [SMCMapForceFromTo]
        [SMCOrder(18)]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCStep(1)]
        public SMCMasterDetailList<EnderecoEletronicoCategoriaViewModel> EnderecosEletronicos { get; set; }

        [SMCDataSource(SMCStorageType.TempData)]
        [SMCMapForceFromTo]
        public List<SMCDatasourceItem<string>> TiposTelefone { get; set; }

        [SMCDataSource(SMCStorageType.TempData)]
        [SMCMapForceFromTo]
        public List<SMCDatasourceItem<string>> TiposEnderecoEletronico { get; set; }

        #region [BI_ORG_002 - Atos Normativos da Entidade] 

        [SMCHidden]
        [SMCStep(0)]
        public bool AtivaAbaAtoNormativo { get; set; }

        [SMCHidden]
        [SMCStep(0)]
        public bool HabilitaColunaGrauAcademico { get; set; }

        [SMCStep(2)]
        [SMCIgnoreProp(SMCViewMode.Insert)]
        [SMCConditional(SMCConditionalBehavior.Visibility, nameof(AtivaAbaAtoNormativo), SMCConditionalOperation.Equals, true)]
        public List<AtoNormativoVisualizarViewModel> AtoNormativo { get; set; }

        #endregion [BI_ORG_002 - Atos Normativos da Entidade]

        public override void ConfigureDataSources()
        {
            // Incluir todos os tipos de endereço eletrônico
            if (TiposEnderecoEletronico == null)
                TiposEnderecoEletronico = new List<SMCDatasourceItem<string>>();
            TipoEnderecoEletronico[] tiposEE = new TipoEnderecoEletronico[] { TipoEnderecoEletronico.Email, TipoEnderecoEletronico.Facebook, TipoEnderecoEletronico.Instagram, TipoEnderecoEletronico.Twitter, TipoEnderecoEletronico.Website, TipoEnderecoEletronico.Youtube };
            foreach (TipoEnderecoEletronico endEletronico in tiposEE)
            {
                TiposEnderecoEletronico.Add(new SMCDatasourceItem<string>()
                {
                    Descricao = endEletronico.SMCGetDescription(),
                    Seq = endEletronico.SMCGetDescription()
                });
            }

            // Incluir todos os tipos de telefone com exceção do tipo Residencial
            if (TiposTelefone == null)
                TiposTelefone = new List<SMCDatasourceItem<string>>();
            TipoTelefone[] tipos = new TipoTelefone[] { TipoTelefone.Celular, TipoTelefone.CelularComercial, TipoTelefone.Comercial, TipoTelefone.Fax };
            foreach (TipoTelefone tipoTel in tipos)
            {
                TiposTelefone.Add(new SMCDatasourceItem<string>()
                {
                    Descricao = tipoTel.SMCGetDescription(),
                    Seq = tipoTel.SMCGetDescription()
                });
            }
        }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Service<IInstituicaoEnsinoService>(index: nameof(IInstituicaoEnsinoService.BuscarInstituicoesEnsino),
                                                        save: nameof(IInstituicaoEnsinoService.SalvarInstituicaoEnsino),
                                                        edit: nameof(IInstituicaoEnsinoService.BuscarInstituicaoEnsino),
                                                      delete: nameof(IInstituicaoEnsinoService.ExcluirInstituicaoEnsino))
                   .Tab()
                   .Tokens(tokenEdit: UC_ORG_001_02_02.MANTER_INSTITUICAO_ENSINO,
                           tokenInsert: UC_ORG_001_02_02.MANTER_INSTITUICAO_ENSINO,
                           tokenRemove: UC_ORG_001_02_02.MANTER_INSTITUICAO_ENSINO,
                           tokenList: UC_ORG_001_02_01.PESQUISAR_INSTITUICAO_ENSINO)
                   .Button("AssociacaoImagem", "Index", "EntidadeImagem",
                                UC_ORG_001_12_01.PESQUISAR_ASSOCIACAO_IMAGEM,
                                i => new
                                {
                                    SeqEntidade = SMCDESCrypto.EncryptNumberForURL(((ISMCSeq)i).Seq)
                                })
                   .Button("PostagemMaterial", "Index", "Material",
                                UC_APR_004_01_01.PESQUISAR_MATERIAL,
                                i => new
                                {
                                    seqOrigem = SMCDESCrypto.EncryptNumberForURL(((ISMCSeq)i).Seq),
                                    tipoOrigemMaterial = TipoOrigemMaterial.Entidade,
                                    Area = "APR"
                                });
                   
        }

        
    }
}