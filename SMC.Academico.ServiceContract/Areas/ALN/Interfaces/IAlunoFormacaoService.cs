using SMC.Academico.Common.Areas.ALN.Exceptions;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.ALN.Data.Aluno;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;
using System.ServiceModel;

namespace SMC.Academico.ServiceContract.Areas.ALN.Interfaces
{
    [ServiceContract(Namespace = NAMESPACES.SERVICE)]
    public interface IAlunoFormacaoService : ISMCService
    {
        /// <summary>
        /// Busca as associações de formação específica do aluno
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <returns>Dados das assoociações de formação específica do aluno</returns>
        AssociacaoFormacaoEspecificaAlunoData BuscarAssociacaoFormacaoEspecifica(long seqAluno);

        /// <summary>
        /// Grava as associações com foramação específica do aluno
        /// </summary>
        /// <param name="associacaoFormacaoEspecificaAlunoData">Dados das associações de formações específicas do aluno</param>
        /// <exception cref="AssociacaoFormacaoEspecificaIngressanteException">Caso não sejam informadas as quantidades de formações configuradas por tipo</exception>
        void SalvarAssociacaoFormacoesEspecificasAluno(AssociacaoFormacaoEspecificaAlunoData associacaoFormacaoEspecificaAlunoData);

        /// <summary>
        /// Buscar as formações do aluno cujo tipo de formação exige grau acadêmico
        /// </summary>
        /// <param name="seqPessoa">Pessoa do aluno</param>
        /// <param name="seqCurso">Curso</param>
        /// <param name="seqGrauAcademico">Grau academico</param>
        /// <param name="seqDocumentoConclusao">Documento conclusão</param>
        /// <returns>Lista com as formações aluno</returns>
        List<SMCDatasourceItem> BuscarFormacoesAlunoTipoExigeGrauAcademico(long seqDocumentoConclusao, long? seqPessoa, long? seqCurso, long? seqGrauAcademico);

        /// <summary>
        /// Buscar as formações do aluno cujo tipo de formação não exige grau acadêmico
        /// </summary>
        /// <param name="seqPessoa">Pessoa do aluno</param>
        /// <param name="seqCurso">Curso</param>
        /// <param name="seqTipoFormacaoEspecifica">Tipo da formação específica</param>
        /// <param name="seqDocumentoConclusao">Documento conclusão</param>
        /// <returns>Lista com as formações aluno</returns>
        List<SMCDatasourceItem> BuscarFormacoesAlunoTipoNaoExigeGrauAcademico(long seqDocumentoConclusao, long? seqPessoa, long? seqCurso, long? seqTipoFormacaoEspecifica);
    }
}