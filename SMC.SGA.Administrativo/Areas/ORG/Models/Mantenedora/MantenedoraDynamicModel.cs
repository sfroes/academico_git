using SMC.Academico.Common.Areas.ORG.Constants;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.EstruturaOrganizacional.UI.Mvc.Areas.ESO.Lookups;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.Localidades.Common.Areas.LOC.Enums;
using SMC.Localidades.UI.Mvc.DataAnnotation;
using SMC.Localidades.UI.Mvc.Models;
using SMC.SGA.Administrativo.Models;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    [SMCStepConfiguration]
    [SMCStepConfiguration]
    public class MantenedoraDynamicModel : SMCDynamicViewModel, ISMCStep, ISMCMappable
    {
        public MantenedoraDynamicModel()
        {
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

        [SMCOrder(0)]
        [SMCStep(0)]
        [SMCSize(SMCSize.Grid6_24)]
        [SMCImage(thumbnailHeight: 100, thumbnailWidth: 0, manualUpload: false, maxFileSize: 225651471,
                  AreaDownload = "", ActionDownload = "DownloadFileGuid", ControllerDownload = "Home")]
        public SMCUploadFile ArquivoLogotipo { get; set; }

        [SMCStep(0)]
        [SMCHidden]
        public long? SeqArquivoLogotipo { get; set; }

        [SMCOrder(1)]
        [SMCStep(0)]
        [SMCKey]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid4_24)]
        public override long Seq { get; set; }

        [SMCOrder(2)]
        [SMCStep(0)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid14_24)]
        [SMCMaxLength(100)]
        public string Nome { get; set; }

        [SMCOrder(3)]
        [SMCStep(0)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCMaxLength(15)]
        public string Sigla { get; set; }

        [SMCOrder(4)]
        [SMCStep(0)]
        [SMCSize(SMCSize.Grid14_24)]
        [SMCInclude(true)] // O Dynamic gera include automático dos lookups, ignorado por ser uma entidade externa
        [UnidadeLookup]
        public UnidadeLookupViewModel CodigoUnidadeSeo { get; set; }

        [SMCOrder(5)]
        [SMCStep(1)]
        [Address(min: 1, Correspondence = true, TiposEnderecos = new TipoEndereco[] { TipoEndereco.Comercial, TipoEndereco.Outro })]
        [SMCMapForceFromTo]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCCssClass("smc-sga-detalhe-editavel-blocos-endereco-responsivo")]
        public AddressList Enderecos { get; set; }

        [SMCStep(1)]
        [SMCDetail]
        [SMCOrder(6)]
        [SMCMapForceFromTo]
        [SMCSize(SMCSize.Grid12_24)]
        public SMCMasterDetailList<TelefoneCategoriaViewModel> Telefones { get; set; }

        [SMCOrder(7)]
        [SMCStep(1)]
        [SMCMapForceFromTo]
        [SMCDetail]
        [SMCSize(SMCSize.Grid12_24)]
        public SMCMasterDetailList<EnderecoEletronicoCategoriaViewModel> EnderecosEletronicos { get; set; }

        [SMCDataSource(SMCStorageType.TempData)]
        [SMCMapForceFromTo]
        public List<SMCDatasourceItem<string>> TiposEnderecoEletronico { get; set; }

        [SMCDataSource(SMCStorageType.TempData)]
        [SMCMapForceFromTo]
        public List<SMCDatasourceItem<string>> TiposTelefone { get; set; }

        [SMCIgnoreProp]
        public int Step { get; set; }

        public override void ConfigureDataSources()
        {
            // Incluir todos os tipos de endereço eletrônico
            if (TiposEnderecoEletronico == null)
            {
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
            }

            // Incluir todos os tipos de telefone com exceção do tipo Residencial
            if (TiposTelefone == null)
            {
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
        }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options
                .Tab()
                .Service<IMantenedoraService>(edit: nameof(IMantenedoraService.BuscarMantenedora),
                                              save: nameof(IMantenedoraService.SalvarMantenedora))
                .Tokens(tokenList: UC_ORG_001_01_01.PESQUISAR_MANTENEDORA,
                           tokenInsert: UC_ORG_001_01_02.MANTER_MANTENEDORA,
                           tokenEdit: UC_ORG_001_01_02.MANTER_MANTENEDORA,
                           tokenRemove: UC_ORG_001_01_02.MANTER_MANTENEDORA);
        }
    }
}