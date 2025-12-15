using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Formularios.ServiceContract.Areas.TMP.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using SMC.Seguranca.ServiceContract.Areas.APL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.SRC.Services
{
    public class TipoServicoService : SMCServiceBase, ITipoServicoService
    {
        #region [ DomainService ]

        private TipoServicoDomainService TipoServicoDomainService
        {
            get { return Create<TipoServicoDomainService>(); }
        }

        private InstituicaoNivelServicoDomainService InstituicaoNivelServicoDomainService
        {
            get { return Create<InstituicaoNivelServicoDomainService>(); }
        }

        private IAplicacaoService AplicacaoService
        {
            get { return Create<IAplicacaoService>(); }
        }

        private IClasseTemplateProcessoService ClasseTemplateProcessoService
        {
            get { return Create<IClasseTemplateProcessoService>(); }
        }

        private IServicoService ServicoService
        {
            get { return Create<IServicoService>(); }
        }

        #endregion [ DomainService ]

        public TipoServicoData BuscarTipoServico(long seqTipoServico)
        {
            return TipoServicoDomainService.BuscarTipoServico(seqTipoServico).Transform<TipoServicoData>();
        }

        public TipoServicoData BuscarTipoServicoPorToken(string token)
        {
            return TipoServicoDomainService.BuscarTipoServicoPorToken(token).Transform<TipoServicoData>();
        }

        public List<SMCDatasourceItem> BuscarTiposServicosSelect()
        {
            var lista = TipoServicoDomainService.SearchAll(i => i.Descricao);

            List<SMCDatasourceItem> retorno = new List<SMCDatasourceItem>();

            foreach (var item in lista)
                retorno.Add(new SMCDatasourceItem(item.Seq, item.Descricao));

            return retorno;           
        }

        public List<SMCDatasourceItem> BuscarTiposServicosPorInstituicaoNivelEnsinoSelect()
        {
            //Busca todos os tipos de serviço respeitando o filtro global
            var seqsTiposServicos = InstituicaoNivelServicoDomainService
                                    .SearchProjectionAll(x => x.Servico.TipoServico.Seq)
                                    .ToArray();

            var spec = new SMCContainsSpecification<TipoServico, long>(t => t.Seq, seqsTiposServicos);

            var result = TipoServicoDomainService.SearchProjectionBySpecification(spec, t => new SMCDatasourceItem()
            {
                Seq = t.Seq,
                Descricao = t.Descricao
            });

            return result.OrderBy(o => o.Descricao).ToList();
        }

        public List<SMCDatasourceItem> BuscarClassesTemplateProcessoSgfSelect()
        {
            var grupoAplicacao = AplicacaoService.BuscarGrupoAplicacaoPelaSigla(SIGLA_APLICACAO.GRUPO_APLICACAO);
            return ClasseTemplateProcessoService.BuscarClassesTemplateProcessoPorGrupoAplicacaoSAS(grupoAplicacao?.Seq ?? 0).OrderBy(a => a.Descricao).ToList();
        }

        public List<SMCDatasourceItem> BuscarTiposServicosPorAlunoSelect(ServicoPorAlunoFiltroData filtro)
        {
            return TipoServicoDomainService.BuscarTiposServicosPorAlunoSelect(filtro.Transform<ServicoPorAlunoFiltroVO>());
        }
    }
}