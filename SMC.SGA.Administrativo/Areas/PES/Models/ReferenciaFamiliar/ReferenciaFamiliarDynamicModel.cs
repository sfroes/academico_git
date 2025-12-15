using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Academico.UI.Mvc.Areas.PES.Models;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.Localidades.UI.Mvc.DataAnnotation;
using SMC.Localidades.UI.Mvc.Models;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class ReferenciaFamiliarDynamicModel : SMCDynamicViewModel
    {
        #region [ DataSource ]

        [SMCDataSource(SMCStorageType.TempData)]
        [SMCMapForceFromTo]
        public List<SMCDatasourceItem> TiposTelefones { get; set; }

        [SMCDataSource(SMCStorageType.TempData)]
        [SMCMapForceFromTo]        
        public List<SMCDatasourceItem> TiposEnderecos { get; set; }

        [SMCDataSource(SMCStorageType.TempData)]
        [SMCMapForceFromTo]
        public List<SMCDatasourceItem> Paises { get; set; }

        #endregion

        [SMCHidden]
        [SMCKey]
        public override long Seq { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqPessoaAtuacao { get; set; }

        [SMCRegularExpression(REGEX.NOME)]
        [SMCMaxLength(255)]
        [SMCOrder(0)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24)]
        public string NomeParente { get; set; }

        [SMCOrder(1)]
        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid6_24)]
        public TipoParentesco TipoParentesco { get; set; }

        [SMCDetail(min:1)]
        [SMCMapForceFromTo]
        [SMCOrder(2)]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<EnderecoEletronicoViewModel> EnderecosEletronicos { get; set; }

        [Address(max: 1, Correspondence = true)]
        [SMCMapForceFromTo]
        [SMCOrder(3)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCCssClass("smc-sga-detalhe-editavel-blocos-endereco-responsivo")]
        public AddressList Enderecos { get; set; }
        
        [Phone]
        [SMCMapForceFromTo]
        [SMCOrder(4)]
        [SMCSize(SMCSize.Grid24_24)]
        public PhoneList Telefones { get; set; }
        
        #region [ Configurações ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {           
            options.Detail<ReferenciaFamiliarDynamicModel>("_DetailList")
                   .Service<IReferenciaFamiliarService>( delete: nameof(IReferenciaFamiliarService.ExcluirReferenciaFamiliar),
                                                           edit: nameof(IReferenciaFamiliarService.BuscarReferenciaFamiliar),
                                                          index: nameof(IReferenciaFamiliarService.BuscarReferenciasFamiliares),
                                                           save: nameof(IReferenciaFamiliarService.SalvarReferenciaFamiliar))
                   .Tokens(tokenEdit: UC_PES_001_03_02.MANTER_REFERENCIA_FAMILIAR,
                           tokenInsert: UC_PES_001_03_02.MANTER_REFERENCIA_FAMILIAR,
                           tokenRemove: UC_PES_001_03_02.MANTER_REFERENCIA_FAMILIAR,
                           tokenList: UC_PES_001_03_01.PESQUISAR_REFERENCIA_FAMILIAR);
        }

        public override void ConfigureDataSources()
        {
            // Preenche a lista de tipos de tipos de telefone
            TiposTelefones = new List<SMCDatasourceItem>();
            TipoTelefone[] tiposTE = new TipoTelefone[] { TipoTelefone.Celular, TipoTelefone.Residencial };
            foreach (TipoTelefone item in tiposTE)
            {
                TiposTelefones.Add(new SMCDatasourceItem()
                {
                    Descricao = item.SMCGetDescription(),
                    Seq = item.SMCTo<long>()
                });
            }
           
        }

        #endregion
    }
}