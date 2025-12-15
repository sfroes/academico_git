using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.PES.Includes;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Security.Util;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.PES.Services
{
    public class MotivoBloqueioService : SMCServiceBase, IMotivoBloqueioService
    {
        #region [ DomainServices ]

        private MotivoBloqueioDomainService MotivoBloqueioDomainService
        {
            get { return this.Create<MotivoBloqueioDomainService>(); }
        }

        private InstituicaoMotivoBloqueioDomainService InstituicaoMotivoBloqueioDomainService
        {
            get { return this.Create<InstituicaoMotivoBloqueioDomainService>(); }
        }

        private ServicoMotivoBloqueioParcelaDomainService ServicoMotivoBloqueioParcelaDomainService
        {
            get { return this.Create<ServicoMotivoBloqueioParcelaDomainService>(); }
        }

        #endregion [ DomainServices ]

        public List<SMCDatasourceItem> BuscarMotivosBloqueioSelect(MotivoBloqueioFiltroData filtros)
        {
            var spec = filtros.Transform<MotivoBloqueioFilterSpecification>();

            var result = this.MotivoBloqueioDomainService.SearchBySpecification(spec).TransformList<SMCDatasourceItem>();

            return result;
        }

        public List<SMCDatasourceItem> BuscarMotivosBloqueioInstituicaoSelect(MotivoBloqueioFiltroData filtros)
        {
            var spec = filtros.Transform<InstituicaoMotivoBloqueioFilterSpecification>();
            spec.SetOrderBy(t => t.MotivoBloqueio.Descricao);
            return this.InstituicaoMotivoBloqueioDomainService
                       .SearchProjectionBySpecification(spec, p => new SMCDatasourceItem()
                       {
                           Seq = p.MotivoBloqueio.Seq,
                           Descricao = p.MotivoBloqueio.Descricao
                       }, true).OrderBy(t => t.Descricao).ToList();
        }

        public List<SMCDatasourceItem> BuscarMotivosBloqueioDescricaoCompletaPorInstituicaoSelect(long seqInstituicaoEnsino)
        {
            var spec = new InstituicaoMotivoBloqueioFilterSpecification() { SeqInstituicaoEnsino = seqInstituicaoEnsino };

            return this.InstituicaoMotivoBloqueioDomainService
                       .SearchProjectionBySpecification(spec, p => new SMCDatasourceItem()
                       {
                           Seq = p.MotivoBloqueio.Seq,
                           Descricao = p.MotivoBloqueio.TipoBloqueio.Descricao + " - " + p.MotivoBloqueio.Descricao

                       }, true).OrderBy(t => t.Descricao).ToList();
        }

        public List<SMCDatasourceItem> BuscarMotivosBloqueioFinanceiroSelect()
        {
            var result = this.InstituicaoMotivoBloqueioDomainService
                             .SearchAll(i => i.MotivoBloqueio.Descricao, IncludesInstituicaoMotivoBloqueio.MotivoBloqueio_TipoBloqueio)
                             .Select(i => i.MotivoBloqueio)
                             .Where(i => i.TipoBloqueio.Token == TOKEN_TIPO_BLOQUEIO.BLOQUEIO_FINANCEIRO)
                             .Distinct().ToList();

            return result.TransformList<SMCDatasourceItem>();
        }

        public List<SMCDatasourceItem> BuscarMotivosBloqueioServicoParcelaSelect(long seqServico)
        {
            var spec = new ServicoMotivoBloqueioParcelaFilterSpecification() { SeqServico = seqServico };

            var lista = this.ServicoMotivoBloqueioParcelaDomainService.SearchProjectionBySpecification(spec, x => new SMCDatasourceItem()
            {
                Seq = x.MotivoBloqueio.Seq,
                Descricao = x.MotivoBloqueio.Descricao

            }).OrderBy(o => o.Descricao).SMCDistinct(a => a.Seq).ToList();

            return lista;
        }

        public MotivoBloqueioData BuscarMotivoBloqueio(long seqMotivoBloqueio)
        {
            var result = this.MotivoBloqueioDomainService.SearchByKey(new SMCSeqSpecification<MotivoBloqueio>(seqMotivoBloqueio));

            return result.Transform<MotivoBloqueioData>();
        }

        public List<SMCDatasourceItem> BuscarMotivosBloqueioFormatoManualAmbosSelect(MotivoBloqueioFiltroData filtros)
        {
            var spec = filtros.Transform<MotivoBloqueioFilterSpecification>();
            var formaBloqueio = new List<FormaBloqueio> { FormaBloqueio.Ambos, FormaBloqueio.Manual };

            var result = this.MotivoBloqueioDomainService.SearchBySpecification(spec)
                                                         .Where(w => formaBloqueio.Contains(w.FormaBloqueio))
                                                         .OrderBy(o => o.Descricao)
                                                         .ToList();

            var retorno = new List<SMCDatasourceItem>();
            foreach (var item in result)
                if (!string.IsNullOrEmpty(item.TokenPermissaoCadastro) && SMCSecurityHelper.Authorize(item.TokenPermissaoCadastro))
                    retorno.Add(new SMCDatasourceItem { Seq = item.Seq, Descricao = item.Descricao });

            return retorno;
        }
    }
}