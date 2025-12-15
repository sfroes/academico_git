using SMC.Academico.Common.Areas.FIN.Constants;
using SMC.Academico.Service.Areas.FIN.Services;
using SMC.Academico.ServiceContract.Areas.FIN.Interfaces;
using SMC.Academico.UI.Mvc.Areas.PES.Models;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.Util;
using SMC.Localidades.Common.Areas.LOC.Enums;
using SMC.Localidades.UI.Mvc.DataAnnotation;
using SMC.Localidades.UI.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.FIN.Models
{
    public class PessoaJuridicaDynamicModel : SMCDynamicViewModel
    {

        #region DataSources

        [SMCDataSource(SMCStorageType.TempData)]
        [SMCMapForceFromTo]
        [SMCIgnoreProp]
        public List<SMCDatasourceItem> TiposEnderecos { get; set; }

        [SMCDataSource(SMCStorageType.TempData)]
        [SMCMapForceFromTo]
        [SMCIgnoreProp]
        public List<SMCDatasourceItem> TiposTelefones { get; set; }

        #endregion DataSources

        [SMCKey]
        [SMCSortable(true)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.Insert | SMCViewMode.Edit)]
        public override long Seq { get; set; }

        [SMCSize(SMCSize.Grid11_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid11_24)]
        [SMCSortable(true, true)]
        [SMCMaxLength(100)]
        [SMCDescription]
        [SMCRequired]
        public string RazaoSocial { get; set; }

        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        [SMCCnpj]
        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCRequired]
        public string Cnpj { get; set; }

        [SMCSize(SMCSize.Grid9_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid9_24)]
        [SMCMaxLength(100)]
        public string NomeFantasia { get; set; }

        [Address(min: 1, Correspondence = true)]
        [SMCMapForceFromTo]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCRequired]
        [SMCIgnoreProp(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCCssClass("smc-sga-detalhe-editavel-blocos-endereco-responsivo")]
        public AddressList Enderecos { get; set; }

        //[Phone(max: 4, min: 1, ExibeNomeContato = true, ExibePreferencial = true)]
        [Phone(max: 4, ExibeNomeContato = true, ExibePreferencial = true)]
        [SMCMapForceFromTo]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid12_24)]
        [SMCRequired]
        [SMCIgnoreProp(SMCViewMode.Filter | SMCViewMode.List)]
        public PhoneList Telefones { get; set; }

        [SMCMapForceFromTo]
        [SMCDetail]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid12_24)]
        [SMCIgnoreProp(SMCViewMode.Filter | SMCViewMode.List)]
        public SMCMasterDetailList<EnderecoEletronicoViewModel> EnderecosEletronicos { get; set; }

        #region Configurações

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options
                .Tokens(tokenInsert: UC_FIN_001_05_02.MANTER_PESSOA_JURIDICA,
                        tokenEdit: UC_FIN_001_05_02.MANTER_PESSOA_JURIDICA,
                        tokenRemove: UC_FIN_001_05_02.MANTER_PESSOA_JURIDICA,
                        tokenList: UC_FIN_001_05_01.PESQUISAR_PESSOA_JURIDICA)
                .Service<IPessoaJuridicaService>(save: nameof(IPessoaJuridicaService.SalvarPessoaJuridia));
        }

        public override void ConfigureDataSources()
        {
            base.ConfigureDataSources();
            /// Sobrescrevemos o datasource utilizado pelo componente, pelo TempData.
            var enumEnderecoFiltrado = SMCEnumHelper.GenerateKeyValuePair<TipoEndereco>()
                                                    .Where(w => w.Key != TipoEndereco.Residencial)
                                                    .Select(s => new SMCDatasourceItem
                                                    {
                                                        Seq = (int)s.Key,
                                                        Descricao = s.Value
                                                    }).OrderBy(o => o.Descricao);
            this.TiposEnderecos = enumEnderecoFiltrado.ToList().Where(X => X.Seq != (int)TipoEndereco.Nenhum).ToList();

            var enumTelefonFiltrado = SMCEnumHelper.GenerateKeyValuePair<TipoTelefone>()
                                        .Where(w => w.Key != TipoTelefone.Residencial)
                                        .Select(s => new SMCDatasourceItem
                                        {
                                            Seq = (int)s.Key,
                                            Descricao = s.Value
                                        }).OrderBy(o => o.Descricao);
            this.TiposTelefones = enumTelefonFiltrado.ToList();
        }

        public override void InitializeModel(SMCViewMode viewMode)
        {
            if (Telefones == null)
            {
                Telefones = new PhoneList();
            }

            Telefones.DefaultModel = new InformacoesTelefoneViewModel() { Preferencial = true };
        }

        #endregion Configurações

    }
}