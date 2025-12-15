//using Newtonsoft.Json;
//using SMC.Academico.Domain.Repositories;
//using SMC.Framework.Data;
//using SMC.Framework.Exceptions;
//using SMC.IntegracaoAcademico.ServiceContract.Areas.IAC.Data;
//using System;
//using System.Data;

//namespace SMC.IntegracaoAcademico.DataRepository
//{
//    public class IntegracaoAcademicoRepository : SMCSqlDbRepositoryProvider, IIntegracaoAcademicoRepository
//    {
//        public IntegracaoAcademicoRepository()
//            : base("IntegracaoAcademicoRepository")
//        {
//        }

//        public long CriarAlunoSGP(AlunoSGPData model)
//        {
//            // Retorno da procedure
//            string descImpedimento = string.Empty;

//            // Cria o command apontando para procedure de verificar impedimentos
//            var command = CreateCommand("st_matricula_ingressante_SGP_ACADEMICO", CommandType.StoredProcedure);

//            var jsonTurmas = JsonConvert.SerializeObject(model.Turmas);

//            AddParameter(command, "@seq_ingressante", model.SeqIngressante, SqlDbType.BigInt, ParameterDirection.Input);
//            AddParameter(command, "@seq_condicao_pagamento", model.SeqCondicaoPagamento, SqlDbType.BigInt, ParameterDirection.Input);
//            AddParameter(command, "@seq_formacao_especifica", model.SeqFormacaoEspecifica, SqlDbType.BigInt, ParameterDirection.Input);
//            AddParameter(command, "@seq_curso_oferta_localidade_turno", model.SeqCursoOfertaLocalidadeTurno, SqlDbType.BigInt, ParameterDirection.Input);
//            AddParameter(command, "@ind_disciplina_isolada", model.DisciplinaIsolada, SqlDbType.Bit, ParameterDirection.Input);
//            AddParameter(command, "@ano_ciclo_letivo", model.AnoCicloLetivo, SqlDbType.SmallInt, ParameterDirection.Input);
//            AddParameter(command, "@num_ciclo_letivo", model.NumeroCicloLetivo, SqlDbType.SmallInt, ParameterDirection.Input);
//            AddParameter(command, "@cod_pessoa_CAD", model.CodigoPessoaCAD, SqlDbType.Int, ParameterDirection.Input);
//            AddParameter(command, "@ind_pre_matricula", model.PreMatricula, SqlDbType.Bit, ParameterDirection.Input);
//            AddParameter(command, "@cod_professor_SGP", model.CodigoProfessorSGP, SqlDbType.Int, ParameterDirection.Input);
//            AddParameter(command, "@usuario", model.Usuario, SqlDbType.VarChar, ParameterDirection.Input);
//            AddParameter(command, "@lista_turma", jsonTurmas, SqlDbType.VarChar, jsonTurmas.Length, ParameterDirection.Input);
//            AddParameter(command, "@dat_adesao_contrato", model.DataAdesaoContrato, SqlDbType.SmallDateTime, ParameterDirection.Input);
//            AddParameter(command, "@cod_adesao_contrato", model.CodigoAdesaoContrato, SqlDbType.UniqueIdentifier, ParameterDirection.Input);
//            AddParameter(command, "@dat_previsao_conclusao", model.DataPrevisaoConclusao, SqlDbType.DateTime, ParameterDirection.Input);
//            AddParameter(command, "@cod_aluno_SGP", string.Empty, SqlDbType.Int, ParameterDirection.Output);
//            AddParameter(command, "@dsc_erro", string.Empty, SqlDbType.VarChar, 8000, ParameterDirection.Output);

//            try
//            {
//                this.ExecuteNonQuery(command);
//            }
//            finally
//            {
//                if (!string.IsNullOrWhiteSpace(command.Parameters["@dsc_erro"].SqlValue?.ToString()) &&
//                    !string.IsNullOrEmpty(command.Parameters["@dsc_erro"].SqlValue?.ToString()))
//                    throw new SMCApplicationException(command.Parameters["@dsc_erro"].SqlValue.ToString());
//            }

//            // Retorna
//            return Convert.ToInt64(command.Parameters["@cod_aluno_SGP"].SqlValue.ToString());
//        }
//    }
//}