using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.PES.DomainServices
{
    public class ConfiguracaoAvaliacaoPpaTurmaDomainService : AcademicoContextDomain<ConfiguracaoAvaliacaoPpaTurma>
    {
        public SMCPagerData<ConfiguracaoAvaliacaoPpaTurmaListarVO> ListarTurmas(ConfiguracaoAvaliacaoPpaTurmaFiltroVO filtro)
        {
            var spec = filtro.Transform<ConfiguracaoAvaliacaoPpaTurmaFilterSpecification>();
            spec.SetOrderBy( x=> x.Turma.ConfiguracoesComponente.FirstOrDefault().Descricao);

            int total = 0;
            spec.SetPageSetting(filtro.PageSettings);

            var lista = this.SearchProjectionBySpecification(spec, x => new ConfiguracaoAvaliacaoPpaTurmaListarVO {
                SeqConfiguracaoAvaliacaoPpaTurma = x.Seq,
                SeqConfiguracaoAvaliacaoPpa = x.SeqConfiguracaoAvaliacaoPpa,
                SeqTurma = x.SeqTurma,
                CodigoTurma = x.Turma.Codigo ,
                NumeroTurma = x.Turma.Numero,
                CodigoInstrumento = x.CodigoInstrumento,
                DescricaoTurma = x.Turma.ConfiguracoesComponente.FirstOrDefault().Descricao
            },
            out total).ToList();

            foreach (var item in lista)
            {
                item.Turma = $"{item.CodigoTurma}.{item.NumeroTurma}";
            }
           
            return new SMCPagerData<ConfiguracaoAvaliacaoPpaTurmaListarVO>(lista, total);
        }
    }
}
