using SMC.Academico.Common.Areas.ALN.Includes;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.ServiceContract.Areas.ALN.Data;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.ALN.Services
{
    public class InstituicaoNivelTipoTermoIntercambioService : SMCServiceBase, IInstituicaoNivelTipoTermoIntercambioService
    {
        private InstituicaoNivelTipoTermoIntercambioDomainService InstituicaoNivelTipoTermoIntercambioDomainService
        {
            get { return Create<InstituicaoNivelTipoTermoIntercambioDomainService>(); }
        }

        public List<SMCDatasourceItem> BuscarInstituicaoNivelTipoTermoIntercambioSelect(long seqInstituicaoNivelTipoVinculoAluno)
        {
            var lista = InstituicaoNivelTipoTermoIntercambioDomainService.SearchAll(i => i.TipoTermoIntercambio.Descricao, IncludesInstituicaoNivelTipoTermoIntercambio.TipoTermoIntercambio).Where(w => w.SeqInstituicaoNivelTipoVinculoAluno == seqInstituicaoNivelTipoVinculoAluno).OrderBy(w => w.TipoTermoIntercambio.Descricao);
            List<SMCDatasourceItem> retorno = new List<SMCDatasourceItem>();
            foreach (var item in lista)
                retorno.Add(new SMCDatasourceItem(item.Seq, item.TipoTermoIntercambio.Descricao));
            return retorno;
        }

        public InstituicaoNivelTipoTermoIntercambioData BuscarInstituicoesNivelTipoVinculoAluno(InstituicaoNivelTipoTermoIntercambioFiltroData filtro)
        {
            return InstituicaoNivelTipoTermoIntercambioDomainService.BuscarInstituicoesNivelTipoVinculoAluno(new InstituicaoNivelTipoTermoIntercambioFilterSpecification
            {
                SeqInstituicaoNivelTipoVinculoAluno = filtro.SeqInstituicaoNivelTipoVinculoAluno,
                SeqTipoTermoIntercambio = filtro.SeqTipoTermoIntercambio
            }).FirstOrDefault().Transform<InstituicaoNivelTipoTermoIntercambioData>();
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
            return InstituicaoNivelTipoTermoIntercambioDomainService.ExigirVigenciaTermoIntercambio(seqParceriaIntercambioTipoTermo, seqInstituicaoEnsino, seqNivelEnsino);
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
            return InstituicaoNivelTipoTermoIntercambioDomainService.ValidarTermoIntercambioInstituicaoNivel(seqInstituicaoNivelTipoVinculoAluno, seqTipoTermoIntercambio, seqInstituicaoNivel, concedeFormacao);
        }
    }
}