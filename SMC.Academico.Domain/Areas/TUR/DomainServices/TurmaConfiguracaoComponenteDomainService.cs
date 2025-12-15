using SMC.Academico.Domain.Areas.TUR.Models;
using SMC.Academico.Domain.Areas.TUR.Specifications;
using SMC.Academico.Domain.Areas.TUR.ValueObjects;
using SMC.Framework.Domain;
using System.Linq;

namespace SMC.Academico.Domain.Areas.TUR.DomainServices
{
    public class TurmaConfiguracaoComponenteDomainService : AcademicoContextDomain<TurmaConfiguracaoComponente>
    {
        /// <summary>
        /// Buscar a configuração componente principal da turma para edição de registro
        /// </summary>
        /// <param name="seqTurma">Sequencial da turma</param>
        /// <param name="seqConfiguracaoPrincipal">Sequencial da configuração de componente principal</param>
        /// <returns>Objeto TurmaGrupoConfiguracaoVO com sequencial principal preenchido</returns>
        public TurmaGrupoConfiguracaoVO BuscarConfiguracaoPrincipalTurma(long seqTurma, long seqConfiguracaoPrincipal)
        {
            TurmaConfiguracaoComponenteFilterSpecification spec = new TurmaConfiguracaoComponenteFilterSpecification();
            if (seqTurma > 0) { spec.SeqTurma = seqTurma; }
            spec.SeqConfiguracaoComponente = seqConfiguracaoPrincipal;

            var turmaConfiguracao = this.SearchProjectionBySpecification(spec,
                                                   p => new TurmaGrupoConfiguracaoVO()
                                                   {
                                                       Seq = p.Seq,
                                                       SeqConfiguracaoComponente = p.SeqConfiguracaoComponente,
                                                       SeqTurma = p.SeqTurma,
                                                       Descricao = p.Descricao,
                                                       Principal = p.Principal
                                                   }).FirstOrDefault();

            return turmaConfiguracao;
        }

        /// <summary>
        /// Busca a descricao formatada da turma de acordo com componente curricular e ciclo letivo
        /// Inicialmente utilizado para acertar o retorno de erro em validações de requisito
        /// </summary>        
        /// <param name="seqComponenteCurricular">Sequencial do componente curricular</param>
        /// <param name="seqCicloLetivo">Sequencial do ciclo letivo</param>
        /// <returns>Retorna verdadeiro e a descrição se for uma única turma para o componente curricular do ciclo letivo</returns>
        public (long registroTurma, string descricaoTurma) BuscarDescricaoTurmaPorComponenteCurricularCiclo(long? seqComponenteCurricular, long? seqCicloLetivo)
        {
            TurmaConfiguracaoComponenteFilterSpecification filtro = new TurmaConfiguracaoComponenteFilterSpecification()
            {
                SeqComponenteCurricular = seqComponenteCurricular,
                SeqCicloLetivo = seqCicloLetivo,
            };

            var registro = this.SearchProjectionBySpecification(filtro, p => new
            {
                p.Turma.Codigo,
                p.Turma.Numero,
                p.Descricao
            }).ToList();

            if (registro == null || registro.Count == 0)
                return (0,string.Empty);

            if (registro.Count == 1)
            {
                var registroUnico = registro.First();
                return (1, $"{registroUnico.Codigo}.{registroUnico.Numero} - {registroUnico.Descricao}");
            }

            return (2,string.Empty);
        }
    }
}