using Newtonsoft.Json;
using SMC.Academico.Common.Areas.APR.Constants;
using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.APR.Data;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Academico.ServiceContract.Interfaces;
using SMC.Academico.UI.Mvc.Areas.APR.Models;
using SMC.Academico.UI.Mvc.Areas.APR.Models.DownloadMaterial;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web.Mvc;

namespace SMC.Academico.UI.Mvc.Areas.APR.Controllers
{
    public class DownloadMaterialController : SMCControllerBase
    {
        /*
         1) Coloque a View como EmbeddedResource
         2) Crie um PATH (nome do projeto mais o caminho ate chegar na View) e concatene com a view
        */

        #region [ Services ]

        private IMaterialService MaterialService
        {
            get { return Create<IMaterialService>(); }
        }

        private IArquivoAnexadoService ArquivoAnexadoService
        {
            get { return Create<IArquivoAnexadoService>(); }
        }

        #endregion [ Services ]

        #region [ Actions ]

        [SMCAuthorize(UC_APR_004_02_01.DOWNLOAD_MATERIAL)]
        public ActionResult Index(DownloadMaterialParametrosData filtro)
        {
            switch (filtro.TipoAtuacao.GetValueOrDefault())
            {
                case TipoAtuacao.Aluno:
                    filtro.SeqAluno = HttpContext.GetEntityLogOn(FILTER.ALUNO).Value;
                    break;

                case TipoAtuacao.Colaborador:
                    filtro.SeqColaborador = HttpContext.GetEntityLogOn(FILTER.PROFESSOR).Value;
                    break;

                default:
                    filtro.SeqInstituicaoEnsino = HttpContext.GetEntityLogOn(FILTER.INSTITUICAO_ENSINO).Value;
                    break;
            }

            DownloadMaterialViewModel modelo = filtro.Transform<DownloadMaterialViewModel>();
            MaterialData[] material = MaterialService.ListarMateriaisParaDownload(filtro);

            var arvore = SMCTreeHelper.CreateTree(material.OrderBy(a => a.Descricao).ToList().TransformList<MaterialListarDynamicModel>(), allowCheck: (e) =>
            {
                return (e.TipoMaterial != TipoMaterial.Nenhum);
            });

            // Ordena a árvore...
            arvore?.ForEach(a =>
            {
                if (a.Level == 0)
                    a.Value.DescricaoOrigem = MaterialService.BuscarOrigemMaterial(a.Value.SeqOrigemMaterial).Descricao;
            });
            arvore = arvore?.OrderBy(a => a.Level).ThenBy(a => a.Value.DescricaoOrigem).ThenBy(a => a.Description).ToList();

            modelo.Materiais = arvore;
            modelo.TipoOrigemMaterial = filtro.TipoOrigemMaterial;

            if (filtro.TipoOrigemMaterial == TipoOrigemMaterial.Entidade)
                ViewBag.Title = "Download de Documentos";
            else
                ViewBag.Title = "Download de Material Didático";

            var view = GetExternalView(AcademicoExternalViews.DOWNLOAD_PATH + "TreeView");
            return View(view, modelo);
        }

        [SMCAuthorize(UC_APR_004_02_01.DOWNLOAD_MATERIAL)]
        public ActionResult BuscarCabecalho(SMCEncryptedLong seqOrigemMaterial)
        {
            DownloadMaterialCabecalhoViewModel model = new DownloadMaterialCabecalhoViewModel();

            OrigemMaterialData data = MaterialService.BuscarOrigemMaterial(seqOrigemMaterial);
            model.Descricao = data.Descricao;

            var view = GetExternalView(AcademicoExternalViews.DOWNLOAD_PATH + "_Cabecalho");
            return PartialView(view, model);
        }

        [HttpPost]
        [SMCAuthorize(UC_APR_004_02_01.DOWNLOAD_MATERIAL)]
        public ActionResult Download()
        {
            try
            {
                var json = Request.Form["DownloadMateriais"];
                RootObject obj = JsonConvert.DeserializeObject<RootObject>(json);

                var marcados = obj.checkedNodes.Where(a => a.@checked).ToList();

                if (marcados == null || marcados.Count == 0)
                {
                    throw new Exception("Favor marcar ao menos um arquivo para efetuar o download.");
                }
                else if (marcados.Count == 1)
                {
                    //Baixando apenas um arquivo.
                    return DownloadFile(MaterialService.BuscarUidArquivoAnexado(Convert.ToInt64(marcados.FirstOrDefault().value)));
                }
                else
                {
                    //Gerando um aquivo .zip, pois é mais de um arquivo.
                    List<long> listaMateriaisMarcados = new List<long>();
                    var listaStringMarcados = marcados.Select(a => a.value).ToList();
                    foreach (var seq in listaStringMarcados)
                    {
                        listaMateriaisMarcados.Add(Convert.ToInt64(seq));
                    }
                    SMCFile zipMateriais = MaterialService.DownloadMateriais(listaMateriaisMarcados);
                    return File(zipMateriais.Conteudo, MediaTypeNames.Application.Zip, zipMateriais.Nome);
                }
            }
            catch (Exception ex)
            {
                var tipoOrigemMaterial = Request.Form["TipoOrigemMaterial"];
                var seqsOrigemMaterial = Request.Form["SeqsOrigemMaterial"];
                var tipoAtuacao = Request.Form["TipoAtuacao"];

                SetErrorMessage(ex.Message, target: SMCMessagePlaceholders.Centro);
                return RedirectToAction("Index", new { tipoOrigemMaterial = tipoOrigemMaterial, tipoAtuacao = tipoAtuacao, seqsOrigemMaterial = seqsOrigemMaterial });
            }
        }

        [HttpPost]
        [SMCAuthorize(UC_APR_004_02_01.DOWNLOAD_MATERIAL)]
        public FileResult DownloadFile(Guid guidFile)
        {
            var arq = ArquivoAnexadoService.BuscarArquivoAnexadoPorGuid(guidFile);
            return File(arq.FileData, arq.Type, arq.Name);
        }

        #endregion [ Actions ]
    }
}