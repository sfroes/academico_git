using SMC.Academico.Domain.Areas.APR.DomainServices;
using SMC.Academico.Domain.Areas.APR.Specifications;
using SMC.Academico.Domain.Areas.APR.ValueObjects;
using SMC.Academico.ServiceContract.Areas.APR.Data.Aula;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.APR.Services
{
    public class AulaService : SMCServiceBase, IAulaService
    {
        public AulaDomainService AulaDomainService => this.Create<AulaDomainService>();

        public List<AulaOfertaData> BuscarAlunosNovaApuracao(long seqDivisaoTurma, bool agruparAluosCurso, List<long> seqsOrientadores)
        {
            return AulaDomainService.BuscarAlunosNovaApuracao(seqDivisaoTurma, agruparAluosCurso, seqsOrientadores).TransformList<AulaOfertaData>();
        }

        public AulaData BuscarAula(long seq, bool agruparAluosCurso, List<long> seqsOrientadores)
        {
            return AulaDomainService.BuscarAula(seq, agruparAluosCurso, seqsOrientadores).Transform<AulaData>();
        }

        public SMCPagerData<AulaListaData> BuscarAulasLista(AulaFiltroData filtro)
        {
            var spec = filtro.Transform<AulaFilterSpecification>();
            spec.SetOrderBy(a => a.DataAula);

            var lista = AulaDomainService.SearchProjectionBySpecification(spec, x => new AulaListaData
            {
                Seq = x.Seq,
                DataAula = x.DataAula,
                SeqDivisaoTurma = x.SeqDivisaoTurma
            }, out int total);
            return new SMCPagerData<AulaListaData>(lista, total);
        }

        public void Excluir(long seq)
        {
            AulaDomainService.Excluir(seq);
        }

        public long SalvarAula(AulaData aula)
        {
            var aulaDomain = aula.Transform<AulaVO>();
            return AulaDomainService.SalvarAula(aulaDomain);
        }
    }
}