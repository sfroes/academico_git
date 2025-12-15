using SMC.Academico.Common.Areas.DCT.Constants;
using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.PES.Exceptions;
using SMC.Academico.ServiceContract.Areas.CNC.Interfaces;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.DCT.Data;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Security;
using SMC.Framework.Util;
using SMC.SGA.Administrativo.Areas.DCT.Models;
using SMC.SGA.Administrativo.Areas.DCT.Views.Colaborador.App_LocalResources;
using SMC.SGA.Administrativo.Areas.PES.Models;
using SMC.SGA.Administrativo.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.PES.Controllers
{
    public class FuncionarioController : PessoaAtuacaoBaseController<FuncionarioDynamicModel>
    {
        #region [ Services ]

        private IFuncionarioService FuncionarioService => Create<IFuncionarioService>();
        private ITipoFuncionarioService TipoFuncionarioService => Create<ITipoFuncionarioService>();
        private IFuncionarioVinculoService FuncionarioVinculoService => Create<IFuncionarioVinculoService>();
        private IInstituicaoTipoEntidadeTipoFuncionarioService InstituicaoTipoEntidadeTipoFuncionarioService => Create<IInstituicaoTipoEntidadeTipoFuncionarioService>();
        private IEntidadeService EntidadeService => Create<IEntidadeService>();
        private IInstituicaoTipoEntidadeService InstituicaoTipoEntidadeService => Create<IInstituicaoTipoEntidadeService>();

        #endregion [ Services ]UC_PES_006_02_02

        [SMCAuthorize(UC_PES_006_02_02.MANTER_FUNCIONARIO)]
        public override ActionResult Selecao(FuncionarioDynamicModel model)
        {
            return base.Selecao(model);
        }

        [HttpPost]
        [SMCAuthorize(UC_PES_006_02_02.MANTER_FUNCIONARIO)]
        public override ActionResult BuscarPessoaExistente(FuncionarioDynamicModel model)
        {
            return base.BuscarPessoaExistente(model);
        }

        [SMCAuthorize(UC_PES_006_02_02.MANTER_FUNCIONARIO)]
        public override ActionResult DadosPessoais(FuncionarioDynamicModel model)
        {
            var result = base.DadosPessoais(model);
            var seqPessoaAtuacao = model.SelectedValues.GetValueOrDefault();

            if (seqPessoaAtuacao > 0)
            {
                var pessoasExistentes = this.BuscarPessoasExistentes(model, true);
                if (pessoasExistentes.FirstOrDefault(a => a.Seq == seqPessoaAtuacao).Atuacoes.Any(atuacoes => atuacoes == TipoAtuacao.Funcionario))
                    throw new PessoaAtuacaoDuplicadaException(SMCEnumHelper.GetDescription(TipoAtuacao.Funcionario));
            }

            return result;
        }

        [SMCAuthorize(UC_PES_006_02_02.MANTER_FUNCIONARIO)]
        public ActionResult Vinculos(FuncionarioDynamicModel model)
        {
            this.ConfigureDynamic(model);
            this.SetViewMode(SMCViewMode.Insert);
            return ViewWizard(model);
        }

        [SMCAuthorize(UC_PES_006_02_02.MANTER_FUNCIONARIO)]
        public ActionResult ConfirmacaoCadastroFuncionario(FuncionarioDynamicModel model)
        {
            this.ConfigureDynamic(model);
            ValidarRegistroProfissional(model);
            
            ValidarObrigatorioVinculoUnico(model);

            //Tratamento de campos opcionais
            model.NomeSocialConfirmacao = string.IsNullOrEmpty(model.NomeSocial) ? App_GlobalResources.UIResource.Label_Campo_Vazio : model.NomeSocial;
            model.NumeroPassaporteConfirmacao = string.IsNullOrEmpty(model.NumeroPassaporte) ? App_GlobalResources.UIResource.Label_Campo_Vazio : model.NumeroPassaporte;

            // Descrição do vínculo
            var descricaoTipoFuncionario = this.TipoFuncionarioService.BuscarTiposFuncionarioSelect()
                                                                      .FirstOrDefault(s => s.Seq == model.SeqTipoFuncionario)
                                                                      .Descricao;
            model.DescricaoVinculoConfirmacao = model.DataFimVinculo.HasValue ?
                $"{descricaoTipoFuncionario} - Período: {model.DataInicioVinculo.ToShortDateString()} a {model.DataFimVinculo.Value.ToShortDateString()}" :
                $"{descricaoTipoFuncionario} - Início: {model.DataInicioVinculo.ToShortDateString()}";

            model.DescricaoEntidadeCadastrada = model.ExibirCamposTipoEntidadesEEntidades ? EntidadeService.BuscarEntidadeNome(model.SeqEntidadeVinculo.Value) : string.Empty;

            return ViewWizard(model);
        }

        [SMCAuthorize(UC_PES_006_02_02.MANTER_FUNCIONARIO)]
        public override ActionResult Contatos(FuncionarioDynamicModel model)
        {
            return base.Contatos(model);
        }

        public override FuncionarioDynamicModel BuscarConfiguracao()
        {
            return FuncionarioService.BuscarConfiguracaoFuncionario().Transform<FuncionarioDynamicModel>();
        }

        private void ValidarRegistroProfissional(FuncionarioDynamicModel model)
        {
            FuncionarioVinculoService.ValidarRegistroProfissional(model.SeqTipoFuncionario, model.TipoRegistroProfissional);
        }   
        private void ValidarObrigatorioVinculoUnico(FuncionarioDynamicModel model)
        {
            
            FuncionarioVinculoService.ValidarObrigatorioVinculoUnico(model.Seq,model.SeqFuncionario, model.SeqTipoFuncionario, model.DataInicioVinculo,model.DataFimVinculo);
        }

        [SMCAllowAnonymous]
        public JsonResult BuscarEntidadesPorVinculoFuncionario(long seqTipoFuncionario)
        {
            return Json(FuncionarioVinculoService.BuscarEntidadesPorVinculoFuncionario(seqTipoFuncionario));
        }
        
        [SMCAllowAnonymous]
        public JsonResult ExibirCampos(long seqTipoFuncionario)
        {
            //bug 60367
            //Exibir os campos "Tipo de Entidade" e "Entidade" apenas se o funcionário estiver parametrizado por tipo de entidade(tabela pes.instituicao_tipo_entidade_tipo_funcionario)
            var existeTipoEntidade = InstituicaoTipoEntidadeTipoFuncionarioService.ListaTiposEntidadePorTipoFuncionario(seqTipoFuncionario);

            return Json(existeTipoEntidade);
        }   
        
        [SMCAllowAnonymous]
        public JsonResult BuscarEntidades(long seqTipoEntidade)
        {
            return Json(EntidadeService.BuscarEntidadePorTipoEntidade(seqTipoEntidade));
        }
        
        [SMCAllowAnonymous]
        public JsonResult BuscarTiposEntidades(long seqTipoFuncionario)
        {
            return Json(InstituicaoTipoEntidadeTipoFuncionarioService.BuscarTipoEntidadePorTipoFuncionario(seqTipoFuncionario));
        }

    }
}