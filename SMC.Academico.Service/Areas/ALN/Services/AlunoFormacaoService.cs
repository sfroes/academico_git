using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.ServiceContract.Areas.ALN.Data.Aluno;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.ALN.Services
{
    public class AlunoFormacaoService : SMCServiceBase, IAlunoFormacaoService
    {
        #region [ DomainServices ]

        private AlunoFormacaoDomainService AlunoFormacaoDomainService => Create<AlunoFormacaoDomainService>();

        #endregion [ DomainServices ]

        /// <summary>
        /// Busca as associações de formação específica do aluno
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <returns>Dados das assoociações de formação específica do aluno</returns>
        public AssociacaoFormacaoEspecificaAlunoData BuscarAssociacaoFormacaoEspecifica(long seqAluno)
        {
            return AlunoFormacaoDomainService.BuscarAssociacaoFormacaoEspecifica(seqAluno).Transform<AssociacaoFormacaoEspecificaAlunoData>();
        }

        /// <summary>
        /// Grava as associações com foramação específica do aluno
        /// </summary>
        /// <param name="associacaoFormacaoEspecificaAlunoData">Dados das associações de formações específicas do aluno</param>
        /// <exception cref="AssociacaoFormacaoEspecificaIngressanteException">Caso não sejam informadas as quantidades de formações configuradas por tipo</exception>
        public void SalvarAssociacaoFormacoesEspecificasAluno(AssociacaoFormacaoEspecificaAlunoData associacaoFormacaoEspecificaAlunoData)
        {
            AlunoFormacaoDomainService.SalvarAssociacaoFormacaoEspecifica(associacaoFormacaoEspecificaAlunoData.Transform<AssociacaoFormacaoEspecificaAlunoVO>());
        }

        /// <summary>
        /// Buscar as formações do aluno cujo tipo de formação exige grau acadêmico
        /// </summary>
        /// <param name="seqPessoa">Pessoa do aluno</param>
        /// <param name="seqCurso">Curso</param>
        /// <param name="seqGrauAcademico">Grau academico</param>
        /// <returns>Lista com as formações aluno</returns>
        public List<SMCDatasourceItem> BuscarFormacoesAlunoTipoExigeGrauAcademico(long seqDocumentoConclusao, long? seqPessoa, long? seqCurso, long? seqGrauAcademico)
        {
            return AlunoFormacaoDomainService.BuscarFormacoesAlunoTipoExigeGrauAcademico(seqDocumentoConclusao, seqPessoa, seqCurso, seqGrauAcademico);
        }

        /// <summary>
        /// Buscar as formações do aluno cujo tipo de formação não exige grau acadêmico
        /// </summary>
        /// <param name="seqPessoa">Pessoa do aluno</param>
        /// <param name="seqCurso">Curso</param>
        /// <param name="seqTipoFormacaoEspecifica">Tipo da formação específica</param>
        /// <returns>Lista com as formações aluno</returns>
        public List<SMCDatasourceItem> BuscarFormacoesAlunoTipoNaoExigeGrauAcademico(long seqDocumentoConclusao, long? seqPessoa, long? seqCurso, long? seqTipoFormacaoEspecifica)
        {
            return AlunoFormacaoDomainService.BuscarFormacoesAlunoTipoNaoExigeGrauAcademico(seqDocumentoConclusao, seqPessoa, seqCurso, seqTipoFormacaoEspecifica);
        }
    }
}