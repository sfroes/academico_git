using SMC.Academico.ServiceContract.Areas.APR.Data.Aula;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.APR.Interfaces
{
    public interface IAulaService : ISMCService
    {
        SMCPagerData<AulaListaData> BuscarAulasLista(AulaFiltroData filtro);

        AulaData BuscarAula(long seq, bool agruparAluosCurso, List<long> seqsOrientadores);

        long SalvarAula(AulaData aula);

        List<AulaOfertaData> BuscarAlunosNovaApuracao(long seqDivisaoTurma, bool agruparAluosCurso, List<long> seqsOrientadores);

        void Excluir(long seq);
    }
}