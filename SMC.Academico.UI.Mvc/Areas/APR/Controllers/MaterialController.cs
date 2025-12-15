using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.ServiceContract.Areas.APR.Data;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORT.Interfaces;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Academico.UI.Mvc.Areas.APR.Models;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Web.Mvc;

namespace SMC.Academico.UI.Mvc.Areas.APR.Controllers
{
    public class MaterialController : SMCControllerBase, ISMCDynamicController
    {
        #region [ Services ]

        private IMaterialService MaterialService
        {
            get { return Create<IMaterialService>(); }
        }

        private IDivisaoTurmaService DivisaoTurmaService
        {
            get { return Create<IDivisaoTurmaService>(); }
        }

        private IEntidadeService EntidadeService
        {
            get { return Create<IEntidadeService>(); }
        }

        private IOrientacaoService OrientacaoService
        {
            get { return Create<IOrientacaoService>(); }
        }

        #endregion [ Services ]

        [SMCAllowAnonymous]
        public ActionResult BuscarCabecalho(MaterialFiltroData dados)
        {
            MaterialCabecalhoViewModel model = new MaterialCabecalhoViewModel();

            if (dados.SeqOrigemMaterial.GetValueOrDefault() > 0)
            {
                OrigemMaterialData data = MaterialService.BuscarOrigemMaterial(dados.SeqOrigemMaterial.Value);
                model.Descricao = data.Descricao;
            }
            else if (!string.IsNullOrEmpty(dados.DescricaoOrigem))
            {
                model.Descricao = dados.DescricaoOrigem;
            }
            else
            {
                switch (dados.TipoOrigemMaterial.Value)
                {
                    case TipoOrigemMaterial.DivisaoTurma:
                        //var divisao = DivisaoTurmaService.BuscarDivisaoTurmaDetalhes(dados.SeqOrigem.Value);
                        //model.Descricao = divisao.DescricaoFormatada;
                        var cabecalho = DivisaoTurmaService.BuscarDivisaoTurmaCabecalho(dados.SeqOrigem.Value);
                        model.Descricao = cabecalho.TurmaDescricaoFormatado;// + " - " + cabecalho.DescricaoFormatada + " - " + cabecalho.DescricaoConfiguracaoComponente + " - " + cabecalho.TipoDivisaoDescricao;
                        break;

                    case TipoOrigemMaterial.Entidade:
                        var entidade = EntidadeService.BuscarEntidade(dados.SeqOrigem.Value);
                        model.Descricao = entidade.Nome;
                        break;

                    case TipoOrigemMaterial.Orientacao:
                        var orientacao = OrientacaoService.AlterarOrientacao(dados.SeqOrigem.Value);
                        model.Descricao = "Orientação: " + orientacao.Seq.ToString();
                        break;

                    default:
                        break;
                }
            }

            var view = GetExternalView(AcademicoExternalViews.MATERIAL_PATH + "_Cabecalho");
            return PartialView(view, model);
        }
    }
}
