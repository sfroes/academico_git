using SMC.Academico.Common.Areas.CSO.Enums;
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
    public class LookupCursoOfertaController : SMCControllerBase
    {
        #region [ Services ]

        private ICursoOfertaService CursoOfertaService => Create<ICursoOfertaService>();
        
        private ICursoService CursoService => Create<ICursoService>();
        
        private ITipoFormacaoEspecificaService TipoFormacaoEspecificaService => Create<ITipoFormacaoEspecificaService>();
        
        private IInstituicaoNivelService InstituicaoNivelService => Create<IInstituicaoNivelService>();
        
        private IGrauAcademicoService GrauAcademicoService => Create<IGrauAcademicoService>();

        #endregion [ Services ]

        [HttpPost]
        [SMCAllowAnonymous]
        public ContentResult BuscarCursoOfertasLookupSelect(LookupCursoOfertaFiltroViewModel filtro)
        {
            SMCPagerData<CursoOfertaData> data = CursoOfertaService.BuscarCursoOfertasLookup(filtro.Transform<CursoOfertaFiltroData>());
            var retorno = data.Select(s => new KeyValuePair<string, string>(new SMCEncryptedLong(s.Seq).ToString(), s.Descricao));
            return SMCJsonResultAngular(retorno);
        }

        [HttpPost]
        [SMCAllowAnonymous]
        public ContentResult BuscarCursoOfertasLookup(LookupCursoOfertaFiltroViewModel filtro)
        {
            SMCPagerData<CursoOfertaData> data = CursoOfertaService.BuscarCursoOfertasLookup(filtro.Transform<CursoOfertaFiltroData>());
            return SMCPagerDataAngular(data, s => new
            {
                Seq = SMCEncryptedLong.GetStringValue(s.Seq),
                SeqFormacaoEspecifica = SMCEncryptedLong.GetStringValue(s.SeqFormacaoEspecifica),
                s.DescricaoInstituicaoNivelEnsino,
                s.DescricaoCurso,
                s.DescricaoOfertaCurso,
                s.Ativo,
            });
        }

        [SMCAllowAnonymous]
        public ContentResult BuscarDataSourceEntidadesResponsaveis()
        {
            List<SMCDatasourceItem> data = CursoService.BuscarHierarquiaSuperiorCursoSelect(true, true);
            return SMCDataSourceAngular(data);
        }

        [SMCAllowAnonymous]
        public ContentResult BuscarDataSourceNivelEnsino()
        {
            List<SMCDatasourceItem> data = InstituicaoNivelService.BuscarNiveisEnsinoReconhecidoLDBSelect();
            return SMCDataSourceAngular(data);
        }

        [SMCAllowAnonymous]
        public ContentResult BuscarDataSourceSituacao()
        {
            List<SMCDatasourceItem> data = CursoService.BuscarSituacoesCursoSelect();
            return SMCDataSourceAngular(data, keyValue: true);
        }

        [SMCAllowAnonymous]
        [HttpPost]
        public ContentResult BuscarDataSourceTiposFormacaoEspecifica(List<long> seqsNivelEnsino)
        {
            List<SMCDatasourceItem> data = seqsNivelEnsino.SMCAny() ?
                TipoFormacaoEspecificaService.BuscarTipoFormacaoEspecificaPorNivelEnsinoSelect(new TipoFormacaoEspecificaPorNivelEnsinoFiltroData()
                {
                    SeqNivelEnsino = seqsNivelEnsino,
                    Ativo = true
                }) :
                TipoFormacaoEspecificaService.BuscarTipoFormacaoEspecificaSelect(new TipoFormacaoEspecificaFiltroData
                {
                    ClasseTipoFormacao = ClasseTipoFormacao.Curso,
                    Ativo = true
                });
            return SMCDataSourceAngular(data, keyValue: true);
        }

        [SMCAllowAnonymous]
        [HttpPost]
        public ContentResult BuscarDataSourceGrauAcademico(List<long> seqsNivelEnsino)
        {
            List<SMCDatasourceItem> data = GrauAcademicoService.BuscarGrauAcademicoLookupSelect(new GrauAcademicoFiltroData
            {
                SeqNivelEnsino = seqsNivelEnsino,
                GrauAcademicoAtivo = true,
                RetornarTodos = false
            });
            return SMCDataSourceAngular(data, keyValue: true);
        }
    }
}

