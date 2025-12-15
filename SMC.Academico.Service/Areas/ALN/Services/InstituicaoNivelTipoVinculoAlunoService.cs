using SMC.Academico.Common.Areas.ALN.Includes;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.ServiceContract.Areas.ALN.Data;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.ALN.Services
{
    public class InstituicaoNivelTipoVinculoAlunoService : SMCServiceBase, IInstituicaoNivelTipoVinculoAlunoService
    {
        #region [ DomainService ]

        private InstituicaoNivelTipoOrientacaoDomainService InstituicaoNivelTipoOrientacaoDomainService
        {
            get { return this.Create<InstituicaoNivelTipoOrientacaoDomainService>(); }
        }

        private InstituicaoNivelTipoTermoIntercambioDomainService InstituicaoNivelTipoTermoIntercambioDomainService
        {
            get { return this.Create<InstituicaoNivelTipoTermoIntercambioDomainService>(); }
        }

        private InstituicaoNivelTipoVinculoAlunoDomainService InstituicaoNivelTipoVinculoAlunoDomainService
        {
            get { return this.Create<InstituicaoNivelTipoVinculoAlunoDomainService>(); }
        }

        #endregion [ DomainService ]

        public InstituicaoNivelTipoVinculoAlunoData AlterarInstituicaoNivelTipoVinculoAluno(long seq)
        {
            var result = this.InstituicaoNivelTipoVinculoAlunoDomainService.SearchByKey(new SMCSeqSpecification<InstituicaoNivelTipoVinculoAluno>(seq),
                IncludesInstituicaoNivelTipoVinculoAluno.InstituicaoNivel
                | IncludesInstituicaoNivelTipoVinculoAluno.InstituicaoNivel_NivelEnsino
                | IncludesInstituicaoNivelTipoVinculoAluno.TipoVinculoAluno
                | IncludesInstituicaoNivelTipoVinculoAluno.FormasIngresso
                | IncludesInstituicaoNivelTipoVinculoAluno.FormasIngresso_FormaIngresso
                | IncludesInstituicaoNivelTipoVinculoAluno.FormasIngresso_TiposProcessoSeletivo_TipoProcessoSeletivo
                | IncludesInstituicaoNivelTipoVinculoAluno.SituacoesMatricula
                | IncludesInstituicaoNivelTipoVinculoAluno.SituacoesMatricula_SituacaoMatricula
                | IncludesInstituicaoNivelTipoVinculoAluno.TiposTermoIntercambio
                | IncludesInstituicaoNivelTipoVinculoAluno.TiposTermoIntercambio_TipoTermoIntercambio);

            var data = result.Transform<InstituicaoNivelTipoVinculoAlunoData>();

            foreach (var item in data.FormasIngresso)
                item.TipoFormaIngressoDescricao = item.TipoFormaIngresso.SMCGetDescription();

            return data;
        }

        public SMCPagerData<InstituicaoNivelTipoVinculoAlunoListarData> ListarInstituicaoNivelTipoVinculoAluno(InstituicaoNivelTipoVinculoAlunoFiltroData filtros)
        {
            var specPaginacao = filtros.Transform<InstituicaoNivelTipoVinculoAlunoFilterSpecification>();

            var lista = InstituicaoNivelTipoVinculoAlunoDomainService.SearchBySpecification(specPaginacao, out int total,
                IncludesInstituicaoNivelTipoVinculoAluno.InstituicaoNivel
                | IncludesInstituicaoNivelTipoVinculoAluno.InstituicaoNivel_NivelEnsino
                | IncludesInstituicaoNivelTipoVinculoAluno.TipoVinculoAluno
                | IncludesInstituicaoNivelTipoVinculoAluno.TiposOrientacao
                | IncludesInstituicaoNivelTipoVinculoAluno.TiposOrientacao_TipoOrientacao
                | IncludesInstituicaoNivelTipoVinculoAluno.TiposOrientacao_TiposParticipacao
                | IncludesInstituicaoNivelTipoVinculoAluno.TiposTermoIntercambio
                | IncludesInstituicaoNivelTipoVinculoAluno.TiposTermoIntercambio_TipoTermoIntercambio);

            var retorno = new SMCPagerData<InstituicaoNivelTipoVinculoAluno>(lista, total);
            var list = retorno.Transform<SMCPagerData<InstituicaoNivelTipoVinculoAlunoListarData>>();

            list.SMCForEach(f =>
            {
                f.TiposOrientacao.SMCForEach(w =>
                {
                    w.TipoTermoIntercambio = InstituicaoNivelTipoTermoIntercambioDomainService.SearchByKey(new SMCSeqSpecification<InstituicaoNivelTipoTermoIntercambio>(w.SeqInstituicaoNivelTipoTermoIntercambio.GetValueOrDefault()), IncludesInstituicaoNivelTipoTermoIntercambio.TipoTermoIntercambio)?.TipoTermoIntercambio?.Descricao;
                    w.TipoOrientacao = InstituicaoNivelTipoOrientacaoDomainService.SearchByKey(new SMCSeqSpecification<InstituicaoNivelTipoOrientacao>(w.Seq), IncludesInstituicaoNivelTipoOrientacao.TipoOrientacao).TipoOrientacao.Descricao;
                });
            });

            return list;
        }

        public long SalvarInstituicaoNivelTipoVinculoAluno(InstituicaoNivelTipoVinculoAlunoData modelo)
        {
            var dominio = modelo.Transform<InstituicaoNivelTipoVinculoAluno>();

            return this.InstituicaoNivelTipoVinculoAlunoDomainService.SalvarInstituicaoNivelTipoVinculoAluno(dominio);
        }

        /// <summary>
        /// Busca instituição nível tipo vínculo aluno de acordo com o sequencial do ingressante para validar parâmetros na solicitação de matrícula
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial do ingressante</param>
        /// <returns>Parâmetros de instituição nível tipo vínculo aluno</returns>
        public InstituicaoNivelTipoVinculoAlunoData BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(long seqPessoaAtuacao)
        {
            var registro = this.InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(seqPessoaAtuacao);

            return registro.Transform<InstituicaoNivelTipoVinculoAlunoData>();
        }

        /// <summary>
        /// Busca o vínculo de aluno pelo tipo e nível de ensino
        /// </summary>
        /// <param name="seqNivelEnsino">Nível de ensino do vínculo</param>
        /// <param name="seqTipoVinculoAluno">Sequencial do tipo de vínculo</param>
        /// <returns>Configuração do vínculo par o tipo e nível informada na instituição</returns>
        public InstituicaoNivelTipoVinculoAlunoData BuscarInstituicaoNivelTipoVinculoAlunoPorNivelTipo(long seqNivelEnsino, long seqTipoVinculoAluno)
        {
            return this.InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorNivelTipo(seqNivelEnsino, seqTipoVinculoAluno)?.Transform<InstituicaoNivelTipoVinculoAlunoData>();
        }

        /// <summary>
        /// Busca os tipos de vínculos das forma de ingresso associadas ao tipo do processo seletivo informado
        /// </summary>
        /// <param name="seqProcessoSeletivo">Sequencial do processo seletivo</param>
        /// <returns>Dados dos vínculos</returns>
        public List<SMCDatasourceItem> BuscarTipoVinculoAlunoPorProcesso(long seqProcessoSeletivo)
        {
            return this.InstituicaoNivelTipoVinculoAlunoDomainService.BuscarTipoVinculoAlunoPorProcesso(seqProcessoSeletivo);
        }

        public List<SMCDatasourceItem> BuscarTipoVinculoPorNivelEnsino(long seqNivelEnsino)
        {
            return this.InstituicaoNivelTipoVinculoAlunoDomainService.BuscarTipoVinculoPorNivelEnsino(seqNivelEnsino);
        }

        public List<SMCDatasourceItem> BuscarTipoVinculoPorNivelEnsinoPermiteManutencaoManual(long seqNivelEnsino)
        {
            return this.InstituicaoNivelTipoVinculoAlunoDomainService.BuscarTipoVinculoPorNivelEnsinoPermiteManutencaoManual(seqNivelEnsino);
        }

        public List<SMCDatasourceItem> BuscarTermoIntercambioPorNivelEnsinoPermiteManutencaoManual(long seqNivelEnsino, long seqTipoVinculo)
        {
            return this.InstituicaoNivelTipoVinculoAlunoDomainService.BuscarTermoIntercambioPorNivelEnsinoPermiteManutencaoManual(seqNivelEnsino, seqTipoVinculo);
        }

        public List<SMCDatasourceItem> BuscarTipoOperacaoPorNivelEnsinoPermiteManutencaoManual(long seqNivelEnsino, long seqTipoVinculo, long? SeqTipoTermoIntercambio)
        {
            return this.InstituicaoNivelTipoVinculoAlunoDomainService.BuscarTipoOperacaoPorNivelEnsinoPermiteManutencaoManual(seqNivelEnsino, seqTipoVinculo, SeqTipoTermoIntercambio);
        }

        /// <summary>
        /// Buscar os tipos de orientação no qual exista tipo de orientação que esteja configurado para permitir inclusão manual da orientação
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do nivel de ensino</param>
        /// <param name="seqTipoVinculo">Sequencial tipo vinculo</param>
        /// <param name="SeqTipoTermoIntercambio">Sequencial tipo de intercambio</param>
        /// <returns></returns>
        public List<SMCDatasourceItem> BuscarTipoOrientacaoPorNivelEnsinoPermiteInclusaoManual(long seqNivelEnsino, long seqTipoVinculo, long? SeqTipoTermoIntercambio)
        {
            return this.InstituicaoNivelTipoVinculoAlunoDomainService.BuscarTipoOrientacaoPorNivelEnsinoPermiteInclusaoManual(seqNivelEnsino, seqTipoVinculo, SeqTipoTermoIntercambio);
        }
        }
}