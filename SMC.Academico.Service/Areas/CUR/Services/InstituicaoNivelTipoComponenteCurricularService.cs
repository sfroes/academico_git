using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.CUR.Services
{
    public class InstituicaoNivelTipoComponenteCurricularService : SMCServiceBase, IInstituicaoNivelTipoComponenteCurricularService
    {
        #region [ DomainService ]

        private InstituicaoNivelTipoComponenteCurricularDomainService InstituicaoNivelTipoComponenteCurricularDomainService => this.Create<InstituicaoNivelTipoComponenteCurricularDomainService>();

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => this.Create<PessoaAtuacaoDomainService>();

        #endregion [ DomainService ]

        /// <summary>
        /// Busca a instituição nível tipo componente curricular pelo sequencia para popular o datasource de TiposDivisão
        /// </summary>
        /// <param name="seq">Sequencial do Instituição Nivel Tipo Componente Curricular selecionado</param>
        /// <returns>Instituicao nivel tipo componente curricular</returns>
        public InstituicaoNivelTipoComponenteCurricularData BuscarInstituicaoNivelTipoComponenteCurricular(long seq)
        {
            return this.InstituicaoNivelTipoComponenteCurricularDomainService
                       .BuscarInstituicaoNivelTipoComponenteCurricular(seq)
                       .Transform<InstituicaoNivelTipoComponenteCurricularData>();
        }

        /// <summary>
        /// Busca a lista de Tipo Componente Curricular de acordo com a Instituição e Nivel Ensino para popular um Select
        /// </summary>
        /// <param name="seqInstituicaoNivel">Sequencial Instituição Nível Ensino</param>
        /// <returns>Lista de Tipo Componente Curricular</returns>
        public List<SMCDatasourceItem> BuscarTipoComponenteCurricularSelect(long seqInstituicaoNivelResponsavel)
        {
            return this.InstituicaoNivelTipoComponenteCurricularDomainService.BuscarTipoComponenteCurricularSelect(seqInstituicaoNivelResponsavel);
        }

        /// <summary>
        /// Busca a lista dos tipos de componente curriculares associados ao tipo do grupo curricular informado
        /// </summary>
        /// <param name="seqGrupoCurricular">Sequencial do grupo currícular</param>
        /// <returns>Tipos de componentes associados ao tipo do grupo curricular informado</returns>
        public List<SMCDatasourceItem> BuscarTipoComponenteCurricularPorGrupoSelect(long seqGrupoCurricular)
        {
            return this.InstituicaoNivelTipoComponenteCurricularDomainService.BuscarTipoComponenteCurricularPorGrupoSelect(seqGrupoCurricular);
        }

        /// <summary>
        /// Busca a configuração do tipo componente curricular de acordo com a instituição nível ensino e  do tipo componente
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do Nivel Ensino selecionado</param>
        /// <param name="seqTipoComponenteCurricular">Sequencial do Tipo Componente Curricular selecionado</param>
        /// <returns>Instituicao nivel tipo componente curricular</returns>
        public InstituicaoNivelTipoComponenteCurricularData BuscarInstituicaoNivelTipoComponenteCurricularConfiguracao(long seqNivelEnsino, long seqTipoComponenteCurricular)
        {
            return this.InstituicaoNivelTipoComponenteCurricularDomainService
                       .BuscarInstituicaoNivelTipoComponenteCurricularConfiguracao(seqNivelEnsino, seqTipoComponenteCurricular)
                       .Transform<InstituicaoNivelTipoComponenteCurricularData>();
        }

        /// <summary>
        /// Busca um componente curricular de um nível de ensino e com uma divisão com a gestão do tipo informado
        /// </summary>
        /// <param name="seqInstituicaoNivel">Sequencial do Insituição Nivel Ensino selecionado</param>
        /// <param name="tipoGestaoDivisaoComponente">Tipo de gestão de uma das divisões do tipo de componente</param>
        /// <returns>Dados do tipo do compomente</returns>
        public InstituicaoNivelTipoComponenteCurricularData BuscarInstituicaoNivelTipoComponenteCurricularGestaoDivisao(long seqInstituicaoNivel, TipoGestaoDivisaoComponente tipoGestaoDivisaoComponente)
        {
            return this.InstituicaoNivelTipoComponenteCurricularDomainService
                       .BuscarInstituicaoNivelTipoComponenteCurricularGestaoDivisao(seqInstituicaoNivel, tipoGestaoDivisaoComponente)
                       .Transform<InstituicaoNivelTipoComponenteCurricularData>();
        }

        /// <summary>
        /// Busca a lista de Entidades de acordo com a Instituição e Nivel Ensino para popular um Select
        /// </summary>
        /// <param name="seqInstituicaoNivelResponsavel">Sequencial Instituição Nível Ensino</param>
        /// <param name="seqTipoComponenteCurricular">Sequencial Tipo Componente</param>
        /// <returns>Lista de Entidades do mesmo tipo</returns>
        public List<SMCDatasourceItem> BuscarEntidadesPorTipoComponenteSelect(long seqInstituicaoNivelResponsavel, long seqTipoComponenteCurricular)
        {
            return this.InstituicaoNivelTipoComponenteCurricularDomainService.BuscarEntidadesPorTipoComponenteSelect(seqInstituicaoNivelResponsavel, seqTipoComponenteCurricular);
        }

        /// <summary>
        /// Busca os tipo componente curricular de acordo com o parâmetro de aceita dispensa
        /// </summary>
        /// <returns>Lista de sequenciais tipos componente curricular que permitem dispensa</returns>
        public List<long> BuscarTipoComponenteCurricularDispensa()
        {
            // Busca os tipos de componente que aceitam dispensa
            // FIX: Não tem como informar a instituição x Nivel conforme está descrito em regra do LK_CUR_001 - Componente Curricular
            // pois quando essa regra é necessária (cadastro de dispensa/equivalencia) não temos a informação da instituição x nível
            return this.InstituicaoNivelTipoComponenteCurricularDomainService.BuscarTipoComponenteCurricularDispensa(null, null);
        }

        /// <summary>
        /// Salva um componente curricular de um nível de ensino e com suas divisões com a gestão dos tipos informados
        /// </summary>
        /// <param name="model">Modelo a ser persistido</param>
        public long SalvarInstituicaoNivelTipoComponenteCurricular(InstituicaoNivelTipoComponenteCurricularData model)
        {
            return this.InstituicaoNivelTipoComponenteCurricularDomainService.SalvarInstituicaoNivelTipoComponenteCurricular(model.Transform<InstituicaoNivelTipoComponenteCurricularVO>());
        }

        /// <summary>
        /// Busca tipo componente curricular configurado na instituição nivel pela matriz
        /// </summary>
        /// <param name="seqMatrizCurricular">Sequencial da Matriz Curricular</param>
        /// <returns>Tipos compontente configurados instituição nivel pela matriz</returns>
        public List<SMCDatasourceItem> BuscarInstituicaoNivelTipoComponenteMatrizCurricularSelect(long seqMatrizCurricular)
        {
            return InstituicaoNivelTipoComponenteCurricularDomainService.BuscarInstituicaoNivelTipoComponenteMatrizCurricularSelect(seqMatrizCurricular);
        }
    }
}