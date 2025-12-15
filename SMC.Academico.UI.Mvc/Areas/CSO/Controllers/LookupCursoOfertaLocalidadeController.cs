using SMC.Academico.ServiceContract.Areas.CSO.Data;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CSO.Models;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SMC.Academico.UI.Mvc.Areas.CSO.Controllers
{
#if !DEBUG
    [SessionState(System.Web.SessionState.SessionStateBehavior.ReadOnly)]
#endif
    public class LookupCursoOfertaLocalidadeController : SMCControllerBase
    {
        #region [ Services ]

        private ICursoOfertaLocalidadeService CursoOfertaLocalidadeService => Create<ICursoOfertaLocalidadeService>();

        private ICursoService CursoService => Create<ICursoService>();

        private ITipoFormacaoEspecificaService TipoFormacaoEspecificaService => Create<ITipoFormacaoEspecificaService>();

        private IInstituicaoNivelService InstituicaoNivelService => Create<IInstituicaoNivelService>();

        private IGrauAcademicoService GrauAcademicoService => Create<IGrauAcademicoService>();

        private IInstituicaoNivelModalidadeService InstituicaoNivelModalidadeService => Create<IInstituicaoNivelModalidadeService>();

        private IEntidadeService EntidadeService => Create<IEntidadeService>();

        #endregion [ Services ]

        [HttpPost]
        [SMCAllowAnonymous]
        public ContentResult BuscarCursoOfertasLocalidadesLookupSelect(LookupCursoOfertaLocalidadeFiltroViewModel filtro)
        {
            SMCPagerData<CursoOfertaLocalidadeListaData> data = CursoOfertaLocalidadeService.BuscarCursoOfertasLocalidade(filtro.Transform<CursoOfertaLocalidadeFiltroData>());
            var retorno = data.Select(s => new KeyValuePair<string, string>(new SMCEncryptedLong(s.Seq).ToString(), s.Nome));
            return SMCJsonResultAngular(retorno);
        }

        [HttpPost]
        [SMCAllowAnonymous]
        public ContentResult BuscarCursoOfertasLocalidadesLookup(LookupCursoOfertaLocalidadeFiltroViewModel filtro)
        {
            SMCPagerData<CursoOfertaLocalidadeListaData> data = CursoOfertaLocalidadeService.BuscarCursoOfertasLocalidade(filtro.Transform<CursoOfertaLocalidadeFiltroData>());
            return SMCPagerDataAngular(data, s => new
            {
                Seq = SMCEncryptedLong.GetStringValue(s.Seq),
                s.DescricaoNivelEnsino,
                s.DescricaoCurso,
                s.DescricaoOfertaCurso,
                s.Ativo,
                s.NomeLocalidade,
                s.DescricaoModalidade
            });
        }

        [SMCAllowAnonymous]
        public ContentResult BuscarDataSourceEntidadesResponsaveis(bool? listarDepartamentosGruposProgramas)
        {
            List<SMCDatasourceItem> data;

            if (listarDepartamentosGruposProgramas.HasValue && listarDepartamentosGruposProgramas.Value)
            {
                data = EntidadeService.BuscarDepartamentosGruposProgramasSelect(true);
            }
            else
            {
                data = CursoService.BuscarHierarquiaSuperiorCursoSelect(false, true);

            }
            return SMCDataSourceAngular(data);
        }

        [SMCAllowAnonymous]
        public ContentResult BuscarDataSourceNivelEnsino()
        {
            List<SMCDatasourceItem> data = InstituicaoNivelService.BuscarNiveisEnsinoReconhecidoLDBSelect();
            return SMCDataSourceAngular(data, keyValue: true);
        }

        [SMCAllowAnonymous]
        public ContentResult BuscarDataSourceSituacao()
        {
            List<SMCDatasourceItem> data = CursoService.BuscarSituacoesCursoSelect();
            return SMCDataSourceAngular(data, keyValue: true);
        }

        [SMCAllowAnonymous]
        [HttpPost]
        public ContentResult BuscarDataSourceTiposFormacaoEspecifica(long? seqNivelEnsino)
        {
            List<SMCDatasourceItem> niveisEnsino = new List<SMCDatasourceItem>();
            if (!seqNivelEnsino.HasValue)
            {
                niveisEnsino = InstituicaoNivelService.BuscarNiveisEnsinoReconhecidoLDBSelect();
            }
            List<SMCDatasourceItem> data = TipoFormacaoEspecificaService
                                            .BuscarTipoFormacaoEspecificaPorNivelEnsinoSelect(new TipoFormacaoEspecificaPorNivelEnsinoFiltroData { 
                                                                                                    SeqNivelEnsino = (seqNivelEnsino.HasValue) ? 
                                                                                                    new List<long>() { seqNivelEnsino.GetValueOrDefault() } : 
                                                                                                    niveisEnsino.Select(a => a.Seq).ToList(), Ativo = true });

            return SMCDataSourceAngular(data, keyValue: true);
        }

        [SMCAllowAnonymous]
        [HttpPost]
        public ContentResult BuscarDataSourceGrauAcademico(long? seqNivelEnsino)
        {
            List<SMCDatasourceItem> niveisEnsino = new List<SMCDatasourceItem>();
            if (!seqNivelEnsino.HasValue)
            {
                niveisEnsino = InstituicaoNivelService.BuscarNiveisEnsinoReconhecidoLDBSelect();
            }
            List<SMCDatasourceItem> data = GrauAcademicoService
                                            .BuscarGrauAcademicoLookupSelect(new GrauAcademicoFiltroData
                                            {
                                                SeqNivelEnsino = (seqNivelEnsino.HasValue) ?
                                                new List<long>() { seqNivelEnsino.GetValueOrDefault() } :
                                                niveisEnsino.Select(a => a.Seq).ToList(),
                                                Ativo = true,
                                            });
            return SMCDataSourceAngular(data, keyValue: true);
        }

        [SMCAllowAnonymous]
        [HttpPost]
        public ContentResult BuscarDataSourceLocalidade()
        {
            List<SMCDatasourceItem> data = CursoOfertaLocalidadeService.BuscarEntidadesSuperioresSelect(false);
            return SMCDataSourceAngular(data, keyValue: true);
        }

        [SMCAllowAnonymous]
        [HttpPost]
        public ContentResult BuscarDataSourceModalidade(long? seqNivelEnsino)
        {
            List<SMCDatasourceItem> data;
            if (seqNivelEnsino.HasValue)
            {
                data = InstituicaoNivelModalidadeService.BuscarModalidadesPorNivelEnsinoSelect(seqNivelEnsino.Value);
            }
            else
            {
                data = InstituicaoNivelModalidadeService.BuscarModalidadesPorInstituicaoSelect();
            }
            return SMCDataSourceAngular(data, keyValue: true);
        }
    }
}
