using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Common.Areas.CUR.Exceptions;
using SMC.Academico.Common.Areas.CUR.Includes;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Calendarios.ServiceContract.Areas.CLD.Interfaces;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CUR.DomainServices
{
    public class InstituicaoNivelTipoComponenteCurricularDomainService : AcademicoContextDomain<InstituicaoNivelTipoComponenteCurricular>
    {
        #region [ DomainService ]

        private EntidadeDomainService EntidadeDomainService
        {
            get { return this.Create<EntidadeDomainService>(); }
        }

        private TipoDivisaoComponenteDomainService TipoDivisaoComponenteDomainService
        {
            get { return this.Create<TipoDivisaoComponenteDomainService>(); }
        }

        private GrupoCurricularDomainService GrupoCurricularDomainService
        {
            get { return this.Create<GrupoCurricularDomainService>(); }
        }
        private MatrizCurricularDomainService MatrizCurricularDomainService => Create<MatrizCurricularDomainService>();

        #endregion [ DomainService ]

        #region Service

        private ITipoEventoService TipoEventoService { get => this.Create<ITipoEventoService>(); }

        #endregion

        /// <summary>
        /// Busca a instituição nível tipo componente curricular pelo sequencia para popular o datasource de TiposDivisão
        /// </summary>
        /// <param name="seq">Sequencial do Instituição Nivel Tipo Componente Curricular selecionado</param>
        /// <returns>Instituicao nivel tipo componente curricular</returns>
        public InstituicaoNivelTipoComponenteCurricularVO BuscarInstituicaoNivelTipoComponenteCurricular(long seq)
        {
            var includes = IncludesInstituicaoNivelTipoComponenteCurricular.TiposDivisaoComponente |
                            IncludesInstituicaoNivelTipoComponenteCurricular.InstituicaoNivel |
                            IncludesInstituicaoNivelTipoComponenteCurricular.TiposDivisaoComponente_TipoDivisaoComponente;

            var spec = new SMCSeqSpecification<InstituicaoNivelTipoComponenteCurricular>(seq);
            InstituicaoNivelTipoComponenteCurricularVO registro = this.SearchByKey(spec, includes)
                                                                      .Transform<InstituicaoNivelTipoComponenteCurricularVO>();

            // Busca os tipos de divisão do tipo de componente
            registro.TiposDivisao = TipoDivisaoComponenteDomainService.BuscarTipoDivisaoComponenteSelect(registro.SeqTipoComponenteCurricular);

            foreach (var item in registro.TiposDivisaoComponente)
            {
                if (item.SeqTipoEventoAgd.HasValue)
                {
                    item.DescricaoTipoEventoAgd = this.TipoEventoService.BuscarTipoEvento(item.SeqTipoEventoAgd.GetValueOrDefault()).Descricao;
                }
            }

            return registro;
        }

        /// <summary>
        /// Busca os tipo componente curricular de acordo com a instituição nível ensino
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencia do Instituição Nivel Ensino selecionado</param>
        /// <returns>Lista de tipos componente curricular</returns>
        public List<SMCDatasourceItem> BuscarTipoComponenteCurricularSelect(long seqNivelEnsino)
        {
            if (seqNivelEnsino == 0)
                return new List<SMCDatasourceItem>();

            InstituicaoNivelTipoComponenteCurricularFilterSpecification spec = new InstituicaoNivelTipoComponenteCurricularFilterSpecification();
            spec.SeqNivelEnsino = seqNivelEnsino;
            spec.SetOrderBy(o => o.TipoComponenteCurricular.Descricao);
            var tiposComponenteCurricular = this.SearchBySpecification(spec, IncludesInstituicaoNivelTipoComponenteCurricular.TipoComponenteCurricular | IncludesInstituicaoNivelTipoComponenteCurricular.InstituicaoNivel);

            // Monta o retorno
            List<SMCDatasourceItem> lista = new List<SMCDatasourceItem>();
            foreach (var tipo in tiposComponenteCurricular)
            {
                lista.Add(new SMCDatasourceItem(tipo.SeqTipoComponenteCurricular, tipo.TipoComponenteCurricular.Descricao));
            }
            return lista;
        }

        /// <summary>
        /// Busca a lista dos tipos de componente curriculares associados ao tipo do grupo curricular informado
        /// </summary>
        /// <param name="seqGrupoCurricular">Sequencial do grupo currícular</param>
        /// <returns>Tipos de componentes associados ao tipo do grupo curricular informado</returns>
        public List<SMCDatasourceItem> BuscarTipoComponenteCurricularPorGrupoSelect(long seqGrupoCurricular)
        {
            var spec = new SMCSeqSpecification<GrupoCurricular>(seqGrupoCurricular);
            return this.GrupoCurricularDomainService
                .SearchProjectionByKey(spec, p => p.TipoGrupoCurricular.TiposComponenteCurricular)
                .OrderBy(o => o.Descricao)
                .Select(s => new SMCDatasourceItem() { Seq = s.Seq, Descricao = s.Descricao })
                .ToList();
        }

        /// <summary>
        /// Busca a configuração do tipo componente curricular de acordo com a instituição nível ensino e  do tipo componente
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do Nivel Ensino selecionado</param>
        /// <param name="seqTipoComponenteCurricular">Sequencial do Tipo Componente Curricular selecionado</param>
        /// <returns>Instituicao nivel tipo componente curricular</returns>
        public InstituicaoNivelTipoComponenteCurricular BuscarInstituicaoNivelTipoComponenteCurricularConfiguracao(long seqNivelEnsino, long seqTipoComponenteCurricular)
        {
            InstituicaoNivelTipoComponenteCurricularFilterSpecification spec = new InstituicaoNivelTipoComponenteCurricularFilterSpecification();
            spec.SeqNivelEnsino = seqNivelEnsino == 0 ? (long?)null : seqNivelEnsino;
            spec.SeqTipoComponenteCurricular = seqTipoComponenteCurricular == 0 ? (long?)null : seqTipoComponenteCurricular;

            var instituicaoNivelTiposComponenteCurricular = this.SearchByKey(spec, IncludesInstituicaoNivelTipoComponenteCurricular.InstituicaoNivel);

            return instituicaoNivelTiposComponenteCurricular;
        }

        /// <summary>
        /// Busca um componente curricular de um nível de ensino e com uma divisão com a gestão do tipo informado
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do Nivel Ensino selecionado</param>
        /// <param name="tipoGestaoDivisaoComponente">Tipo de gestão de uma das divisões do tipo de componente</param>
        /// <returns>Dados do tipo do compomente</returns>
        public InstituicaoNivelTipoComponenteCurricular BuscarInstituicaoNivelTipoComponenteCurricularGestaoDivisao(long seqNivelEnsino, TipoGestaoDivisaoComponente tipoGestaoDivisaoComponente)
        {
            var spec = new InstituicaoNivelTipoComponenteCurricularFilterSpecification()
            {
                SeqNivelEnsino = seqNivelEnsino,
                TipoGestaoDivisaoComponente = tipoGestaoDivisaoComponente
            };

            return this.SearchByKey(spec, IncludesInstituicaoNivelTipoComponenteCurricular.InstituicaoNivel);
        }

        /// <summary>
        /// Busca a lista de Entidades de acordo com a Instituição e Nivel Ensino para popular um Select
        /// </summary>
        /// <param name="seqInstituicaoNivel">Sequencial Instituição Nível Ensino</param>
        /// <param name="seqTipoComponenteCurricular">Sequencial Tipo Componente</param>
        /// <returns>Lista de Entidades do mesmo tipo</returns>
        public List<SMCDatasourceItem> BuscarEntidadesPorTipoComponenteSelect(long seqInstituicaoNivel, long seqTipoComponenteCurricular)
        {
            if (seqInstituicaoNivel == 0 && seqTipoComponenteCurricular == 0)
                return new List<SMCDatasourceItem>();

            InstituicaoNivelTipoComponenteCurricular registro = this.BuscarInstituicaoNivelTipoComponenteCurricularConfiguracao(seqInstituicaoNivel, seqTipoComponenteCurricular);
            if (registro == null)
                return new List<SMCDatasourceItem>();

            var specEntidade = new EntidadeFilterSpecification() { SeqTipoEntidade = registro.TipoEntidadeResponsavel };
            specEntidade.SetOrderBy(o => o.Nome);

            var listEntidades = this.EntidadeDomainService
                .SearchProjectionBySpecification(specEntidade, p => new SMCDatasourceItem() { Seq = p.Seq, Descricao = p.Nome })
                .ToList();

            return listEntidades;
        }

        /// <summary>
        /// Busca os tipo componente curricular de acordo com o parâmetro de aceita dispensa
        /// </summary>
        /// <param name="seqInstituicaoEnsino">Sequencial da instituição de ensino a ser pesquisada</param>
        /// <param name="seqNivelEnsino">Sequencial do nível de ensino a ser pesquisado</param>
        /// <returns>Lista de sequenciais tipos componente curricular que permitem dispensa de acordo com a institução e nível de ensino</returns>
        public List<long> BuscarTipoComponenteCurricularDispensa(long? seqInstituicaoEnsino, long? seqNivelEnsino)
        {
            var spec = new InstituicaoNivelTipoComponenteCurricularFilterSpecification()
            {
                SeqInstituicaoEnsino = seqInstituicaoEnsino,
                SeqNivelEnsino = seqNivelEnsino,
                PermiteCadastroDispensa = true
            };
            return this.SearchProjectionBySpecification(spec, t => t.SeqTipoComponenteCurricular).ToList();
        }

        /// <summary>
        /// Busca a quantidade de horas por crédito para o tipo de componente Disciplina
        /// </summary>
        /// <param name="seqInstituicaoEnsino">Sequencial da Instituição de ensino</param>
        /// <returns>Quantidade de horas parametrizado para o tipo Disciplina</returns>
        public short? BuscarQuantidadeHorasPorCreditoDisciplina(long seqInstituicaoEnsino)
        {
            // Quantidade de carga horária para cálculo caso não encontre no componente curricular específico
            // Quando não encontrar componente curricular com parametrização, deve usar a parametrização do tipo Disciplina (Regra da Janice)
            // FIX: Precisa de um token nessa tabela. Não é certo pesquisar pela sigla/descrição
            var quantidadeHorasPorCreditoDisciplina = this.SearchProjectionByKey(new InstituicaoNivelTipoComponenteCurricularFilterSpecification
            {
                SeqInstituicaoEnsino = seqInstituicaoEnsino,
                TipoGestaoDivisaoComponente = TipoGestaoDivisaoComponente.Turma
            }, x => x.QuantidadeHorasPorCredito);

            return quantidadeHorasPorCreditoDisciplina;
        }

        public long SalvarInstituicaoNivelTipoComponenteCurricular(InstituicaoNivelTipoComponenteCurricularVO model)
        {

            //RN_ORG_052 - Consistência ao Salvar parâmetro tipo de componente por NE
            //Se existir algum tipo de divisão com o parâmetro Permite carga horária grade = SIM então o parâmetro
            //Exibe carga horária ? do componente também deve ser sim. Caso contrário não permitir salvar e exibir mensagem.
            if (model.TiposDivisaoComponente.Any(t => t.PermiteCargaHorariaGrade) && !model.ExibeCargaHoraria)
                throw new InstituicaoNivelTipoComponenteCurricularExigeCargaHorariaException();

            var InstituicaoNivelTipoComponenteCurricular = model.Transform<InstituicaoNivelTipoComponenteCurricular>();

            this.SaveEntity(InstituicaoNivelTipoComponenteCurricular);

            return InstituicaoNivelTipoComponenteCurricular.Seq;
        }

        /// <summary>
        /// Busca tipo componente curricular configurado na instituição nivel pela matriz
        /// </summary>
        /// <param name="seqMatrizCurricular">Sequencial da Matriz Curricular</param>
        /// <returns>Tipos compontente configurados instituição nivel pela matriz</returns>
        public List<SMCDatasourceItem> BuscarInstituicaoNivelTipoComponenteMatrizCurricularSelect(long seqMatrizCurricular)
        {
            long seqNivelEnsino = MatrizCurricularDomainService.SearchProjectionByKey(seqMatrizCurricular, p => p.DivisaoCurricular.SeqNivelEnsino);

            List<SMCDatasourceItem> retorno = BuscarTipoComponenteCurricularSelect(seqNivelEnsino);

            return retorno;
        }
    }
}