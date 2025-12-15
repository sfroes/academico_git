using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Service.Areas.PES.Services
{
    public class ConfiguracaoAvaliacaoPpaTurmaService : SMCServiceBase, IConfiguracaoAvaliacaoPpaTurmaService
    {
        ConfiguracaoAvaliacaoPpaTurmaDomainService ConfiguracaoAvaliacaoPpaTurmaDomainService => Create<ConfiguracaoAvaliacaoPpaTurmaDomainService>();

        ConfiguracaoAvaliacaoPpaDomainService ConfiguracaoAvaliacaoPpaDomainService => Create<ConfiguracaoAvaliacaoPpaDomainService>();

        public ConfiguracaoAvaliacaoPpaTurmaCabecalhoData BuscarCabecalhoConfiguracaoAvaliacaoPpaTurma(long seqConfiguracaoAvaliacaoPpa)
        {
            var result = this.ConfiguracaoAvaliacaoPpaDomainService.SearchProjectionByKey(
                new SMCSeqSpecification<ConfiguracaoAvaliacaoPpa>(seqConfiguracaoAvaliacaoPpa), i =>
                new ConfiguracaoAvaliacaoPpaTurmaCabecalhoData
            {
                DescricaoConfiguracaoAvaliacaoPpa = i.Descricao,
                EntidadeResponsavel = i.EntidadeResponsavel.Nome,
                TipoAvaliacao = i.TipoAvaliacaoPpa.ToString(),
                Seq = i.Seq
            });

            return result;
        }

        public SMCPagerData<ConfiguracaoAvaliacaoPpaTurmaListarData> ListarTurmas(ConfiguracaoAvaliacaoPpaTurmaFiltroData filtro)
        {
            var list = ConfiguracaoAvaliacaoPpaTurmaDomainService.ListarTurmas(filtro.Transform<ConfiguracaoAvaliacaoPpaTurmaFiltroVO>());
            return new SMCPagerData<ConfiguracaoAvaliacaoPpaTurmaListarData>(list.TransformList<ConfiguracaoAvaliacaoPpaTurmaListarData>(), list.Total);
        }

    }
}
