using SMC.Academico.Common.Enums;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.TUR.Data;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Academico.UI.Mvc.Areas.TUR.Models;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace SMC.Academico.UI.Mvc.Areas.TUR.Controllers
{
    public class LookupTurmaController : SMCControllerBase
    {
        #region [ Services ]

        private ITurmaService TurmaService => this.Create<ITurmaService>();

        private IEntidadeService EntidadeService => this.Create<IEntidadeService>();

        #endregion [ Services ]

        [HttpPost]
        [SMCAllowAnonymous]
        public ContentResult BuscarSelectTurmas(LookupTurmaFiltroViewModel filtro)
        {
            var regexCodigo = new Regex(@"^\d+\.?\d*$");
            if (regexCodigo.IsMatch(filtro.DescricaoConfiguracao))
            {
                filtro.CodigoFormatado = filtro.DescricaoConfiguracao;
                filtro.DescricaoConfiguracao = null;
            }
            SMCPagerData<TurmaListarData> result = TurmaService.BuscarTurmas(filtro.Transform<TurmaFiltroData>());

            var retorno = result.Select(s => new
            {
                Key = SMCEncryptedLong.GetStringValue(s.Seq),
                Value = s.TurmaConfiguracoesCabecalho.First(f => f.ConfiguracaoPrincipal == LegendaPrincipal.Principal).DescricaoConfiguracaoComponente
            });

            return SMCJsonResultAngular(retorno);
        }

        [HttpPost]
        [SMCAllowAnonymous]
        public ContentResult BuscarTurmas(LookupTurmaFiltroViewModel filtro)
        {
            SMCPagerData<TurmaListarData> result = TurmaService.BuscarTurmas(filtro.Transform<TurmaFiltroData>());

            var retorno = new
            {
                itens = result.Select(s => new
                {
                    Seq = new SMCEncryptedLong(s.Seq).ToString(),
                    s.DescricaoCicloLetivoInicio,
                    CodigoFormatado = $"{s.Codigo}.{s.Numero}",
                    s.TurmaConfiguracoesCabecalho.First(f => f.ConfiguracaoPrincipal == LegendaPrincipal.Principal).DescricaoConfiguracaoComponente,
                    SituacaoTurmaAtual = s.SituacaoTurmaAtual.SMCGetDescription(),
                    InicioPeriodoLetivo = s.DataInicioPeriodoLetivo,
                    FimPeriodoLetivo = s.DataFimPeriodoLetivo,
                }),
                total = result.Total
            };

            return SMCJsonResultAngular(retorno);
        }

        [SMCAllowAnonymous]
        public ContentResult BuscarDataSourceEntidadesResponsaveis()
        {
            var result = EntidadeService.BuscarUnidadesResponsaveisGPILocalSelect();

            var retorno = result.Select(s => new
            {
                Value = s.Seq,
                Label = s.Descricao
            });

            return SMCJsonResultAngular(retorno);
        }
    }
}
