using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.ServiceContract.Areas.DCT.Data;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Academico.UI.Mvc.Areas.DCT.Models;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.Util;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SMC.Academico.UI.Mvc.Areas.DCT.Controllers
{
    public class LookupColaboradorController : SMCControllerBase
    {
        #region [ Services ]

        private ITurmaService TurmaService => this.Create<ITurmaService>();

        private IEntidadeService EntidadeService => this.Create<IEntidadeService>();

        private IColaboradorService ColaboradorService => this.Create<IColaboradorService>();

        private ITipoVinculoColaboradorService TipoVinculoColaboradorService => this.Create<ITipoVinculoColaboradorService>();

        private IInstituicaoNivelTipoAtividadeColaboradorService InstituicaoNivelTipoAtividadeColaboradorService => this.Create<IInstituicaoNivelTipoAtividadeColaboradorService>();

        #endregion [ Services ]

        [HttpPost]
        [SMCAllowAnonymous]
        public ContentResult BuscarSelectColaboradores(LookupColaboradorFiltroViewModel filtro)
        {

            SMCPagerData<ColaboradorListaData> result = ColaboradorService.BuscarColaboradores(filtro.Transform<ColaboradorFiltroData>());

            var retorno = result.Select(s => new
            {
                Key = s.Seq,
                Value = s.Nome
            });

            return SMCJsonResultAngular(retorno);
        }

        [HttpPost]
        [SMCAllowAnonymous]
        public ContentResult BuscarColaboradores(LookupColaboradorFiltroViewModel filtro)
        {
            SMCPagerData<ColaboradorListaData> result = ColaboradorService.BuscarColaboradores(filtro.Transform<ColaboradorFiltroData>());

            var retorno = new
            {
                itens = result.Select(s => new
                {
                    s.Seq,
                    Cpf = SMCMask.ApplyMaskCPF(s.Cpf),
                    s.NumeroPassaporte,
                    DataNascimento = s.DataNascimento.SMCDataAbreviada(),
                    s.Nome,
                    s.NomeSocial,
                    Sexo = s.Sexo.ToString(),
                }),
                total = result.Total
            };

            return SMCJsonResultAngular(retorno);
        }

        [SMCAllowAnonymous]
        public ContentResult BuscarDataSourceEntidadesResponsaveis(long? seqTurma)
        {
            List<SMCDatasourceItem> result = new List<SMCDatasourceItem>();

            if (seqTurma.HasValue)
            {
                result = ColaboradorService.BuscarEntidadeVinculoColaboradorPorTurmaSelect(seqTurma.Value);
            }
            else
            {
                result = EntidadeService.BuscarEntidadesVinculoColaboradorSelect(false);
            }

            return SMCDataSourceAngular(result, keyValue: true);
        }

        [SMCAllowAnonymous]
        public ContentResult BuscarDataSourceTiposViculoColaborador(bool? criaVinculoInstitucional = null)
        {
            List<SMCDatasourceItem> result = TipoVinculoColaboradorService.BuscarTipoVinculoColaboradorDeEntidadesVinculoSelect(criaVinculoInstitucional: criaVinculoInstitucional);

            return SMCDataSourceAngular(result, keyValue: true);
        }

        [SMCAllowAnonymous]
        public ContentResult BuscarDataSourceTiposAtividadeColaborador()
        {
            List<SMCDatasourceItem> result = InstituicaoNivelTipoAtividadeColaboradorService.BuscarTiposAtividadeColaboradorSelect(new InstituicaoNivelTipoAtividadeColaboradorFiltroData());

            return SMCDataSourceAngular(result, keyValue: true, encryptSeq: false);
        }

        [SMCAllowAnonymous]
        public ContentResult BuscarDataSourceSituacaoColaboradorInstituicao()
        {
            return SMCDataSourceAngular<SituacaoColaborador>(keyValue: true);
        }
    }
}

