using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ALN.DomainServices
{
    public class InstituicaoNivelTipoTermoIntercambioDomainService : AcademicoContextDomain<InstituicaoNivelTipoTermoIntercambio>
    {
        #region DomainService

        private ParceriaIntercambioTipoTermoDomainService ParceriaIntercambioTipoTermoDomainService => Create<ParceriaIntercambioTipoTermoDomainService>();

        #endregion DomainService

        /// <summary>
        /// Busca instituição nível tipo termo intercambio de acordo com o sequencial do ingressante para validar parâmetros na solicitação de matrícula
        /// </summary>
        /// <param name="filtro">Filtros da instituição nível tipo termo intercambio</param>
        /// <returns>Objeto instituição nível tipo termo intercambio</returns>
        public List<InstituicaoNivelTipoTermoIntercambioVO> BuscarInstituicoesNivelTipoVinculoAluno(InstituicaoNivelTipoTermoIntercambioFilterSpecification filtro)
        {
            var registro = this.SearchProjectionBySpecification(filtro, x => new InstituicaoNivelTipoTermoIntercambioVO
            {
                ConcedeFormacao = x.ConcedeFormacao,
                ExigePeriodoIntercambioTermo = x.ExigePeriodoIntercambioTermo,
                PermiteIngresso = x.PermiteIngresso,
                PermiteSaidaIntercambio = x.PermiteSaidaIntercambio,
                SeqInstituicaoNivelTipoVinculoAluno = x.SeqInstituicaoNivelTipoVinculoAluno,
                Seq = x.Seq,
                SeqNivelEnsino = x.InstituicaoNivelTipoVinculoAluno.InstituicaoNivel.SeqNivelEnsino,
                SeqTipoTermoIntercambio = x.SeqTipoTermoIntercambio
            }).ToList();

            return registro;
        }

        /// <summary>
        /// UC_ALN_004_01_04 - Manter Termo de Intercâmbio
        /// NV08 O agrupamento de campos só deve ser exibido para preenchimento e obrigatório
        /// (ou habilitado para preenchimento) se o tipo do termo selecionado tiver sido parametrizado,
        /// por instituição-nível-vínculo, para exigir período de intercâmbio no ingresso.
        /// </summary>
        /// <param name="seqTermoIntercambio">Sequencial do Termo de Intercâmbio.</param>
        /// <returns></returns>
        public bool ExigirVigenciaTermoIntercambio(long seqParceriaIntercambioTipoTermo, long seqInstituicaoEnsino, long seqNivelEnsino)
        {
            var seqTipoTermoIntercambio = this.ParceriaIntercambioTipoTermoDomainService.SearchByKey(new SMCSeqSpecification<ParceriaIntercambioTipoTermo>(seqParceriaIntercambioTipoTermo)).SeqTipoTermoIntercambio;

            ExigirVigenciasTermoIntercambioFilterSpecification spec = new ExigirVigenciasTermoIntercambioFilterSpecification()
            {
                SeqTipoTermoIntercambio = seqTipoTermoIntercambio,
                SeqInstituicaoEnsino = seqInstituicaoEnsino,
                SeqNivelEnsino = seqNivelEnsino
            };

            return Count(spec) > 0;
        }

        /// <summary>
        /// Verificar se os tipos de termo associados a instituição-nível-vínculo em questão já foram associados anteriormente a alguma instituição-nível-vínculo.
        /// Caso já estiverem sido associados, eles devem ter o mesmo valor no campo "Concede formação" do cadastro anterior.
        /// </summary>
        /// <param name="seqInstituicaoNivelTipoVinculoAluno">Sequencial do Tipo de Vinculo do Aluno</param>
        /// <param name="seqTipoTermoIntercambio">Sequencial do Tipo de Termo de Intercâmbio.</param>
        /// <param name="seqInstituicaoNivel">Sequencial da Instituição Nível</param>
        /// <param name="concedeFormacao">Parâmetro Concede Formação</param>
        /// <returns>Se retornar registro não esta válido para gravar, precisa do retorno para a mensagem de erro</returns>
        public List<string> ValidarTermoIntercambioInstituicaoNivel(long seqInstituicaoNivelTipoVinculoAluno, long seqTipoTermoIntercambio, long seqInstituicaoNivel, bool concedeFormacao)
        {
            InstituicaoNivelTipoTermoIntercambioFilterSpecification spec = new InstituicaoNivelTipoTermoIntercambioFilterSpecification()
            {
                SeqInstituicaoNivelTipoVinculoAlunoDiferente = seqInstituicaoNivelTipoVinculoAluno,
                SeqTipoTermoIntercambio = seqTipoTermoIntercambio,
                SeqInstituicaoNivel = seqInstituicaoNivel,
                ConcedeFormacaoDiferente = concedeFormacao
            };

            var registro = this.SearchProjectionBySpecification(spec, p => p.TipoTermoIntercambio.Descricao + " - " + p.ConcedeFormacao).ToList();

            return registro;
        }

    }
}