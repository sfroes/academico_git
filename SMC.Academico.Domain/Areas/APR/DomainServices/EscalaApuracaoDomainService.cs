using SMC.Academico.Common.Areas.APR.Includes;
using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Academico.Domain.Areas.APR.Specifications;
using SMC.Academico.Domain.Areas.APR.Validators;
using SMC.Academico.Domain.Areas.APR.ValueObjects;
using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.APR.DomainServices
{
    public class EscalaApuracaoDomainService : AcademicoContextDomain<EscalaApuracao>
    {
        #region [ Serviços ]

        private ConfiguracaoComponenteDomainService ConfiguracaoComponenteDomainService
        {
            get { return this.Create<ConfiguracaoComponenteDomainService>(); }
        }

        private CriterioAprovacaoDomainService CriterioAprovacaoDomainService
        {
            get { return this.Create<CriterioAprovacaoDomainService>(); }
        }

        private CurriculoCursoOfertaDomainService CurriculoCursoOfertaDomainService
        {
            get { return this.Create<CurriculoCursoOfertaDomainService>(); }
        }

        private InstituicaoNivelEscalaApuracaoDomainService InstituicaoNivelEscalaApuracaoDomainService
        {
            get { return this.Create<InstituicaoNivelEscalaApuracaoDomainService>(); }
        }

        #endregion [ Serviços ]

        /// <summary>
        /// Cria uma Escala Apuração com um item aprovado e um item reprovado
        /// </summary>
        /// <returns>Escala de Apuração com dois itens</returns>
        public EscalaApuracao BuscarConfiguracaoEscalaApuracao()
        {
            var escalaApurcao = new EscalaApuracao();
            escalaApurcao.Itens = new List<EscalaApuracaoItem>();
            escalaApurcao.Itens.Add(new EscalaApuracaoItem() { Aprovado = true });
            escalaApurcao.Itens.Add(new EscalaApuracaoItem() { Aprovado = false });
            return escalaApurcao;
        }

        /// <summary>
        /// Busca uma escala de Apuração com seus Itens, Critérios de Aprovação e Flag de Utilização por Crietério de Aprovação
        /// </summary>
        /// <param name="seq">Sequencial da Escala de Apuração a ser recuperada</param>
        /// <returns>Retorna a escala</returns>
        public EscalaApuracaoVO BuscarEscalaApuracao(long seq)
        {
            var includes = IncludesEscalaApuracao.CriteriosAprovacao
                         | IncludesEscalaApuracao.Itens;
            var escalaApuracao = this.SearchByKey(new SMCSeqSpecification<EscalaApuracao>(seq), includes);

            var escalaApuracaoDto = escalaApuracao.Transform<EscalaApuracaoVO>();
            return escalaApuracaoDto;
        }

        /// <summary>
        /// Busca as escalas de Apuração marcadas apra Apuração Final
        /// </summary>
        /// <returns>Dados das escação de Apuração com ApuracaoFinal setado</returns>
        public List<SMCDatasourceItem> BuscarEscalaApuracaoFinalSelect()
        {
            // Escalas de Appuração final
            var specFinal = new EscalaApuracaoFilterSpecification() { ApuracaoFinal = true };
            specFinal.SetOrderBy(o => o.Descricao);

            var selectItens = this.SearchProjectionBySpecification(specFinal,
                p => new SMCDatasourceItem() { Seq = p.Seq, Descricao = p.Descricao });

            return selectItens
                .ToList();
        }

        /// <summary>
        /// Busca as escalas de Apuração que não sejam do tipo conceito e sejam do nível de ensino do curso do curriculo curso oferta informado
        /// </summary>
        /// <param name="seqCurriculoCursoOferta">Sequencial do currículo curso oferta do curso com o nível de ensino em questão</param>
        /// <returns>Dados das escação de Apuração</returns>
        public List<SMCDatasourceItem> BuscarEscalasApuracaoNaoConceitoNivelEnsinoSelect(long seqCurriculoCursoOferta)
        {
            // Recupera nível ensino do curso associado ao curriculo curso oferta
            long seqNivelEnsino = this.CurriculoCursoOfertaDomainService
                .SearchProjectionByKey(new SMCSeqSpecification<CurriculoCursoOferta>(seqCurriculoCursoOferta), p => p.CursoOferta.Curso.SeqNivelEnsino);

            // Recupera todas as escalas do nível de ensino do curso
            var specEscalasNivel = new InstituicaoNivelEscalaApuracaoFilterSpecification() { SeqNivelEnsino = seqNivelEnsino };
            var seqsEscalasNivel = this.InstituicaoNivelEscalaApuracaoDomainService
                .SearchProjectionBySpecification(specEscalasNivel, p => p.SeqEscalaApuracao);

            var specInNivel = new SMCContainsSpecification<EscalaApuracao, long>(p => p.Seq, seqsEscalasNivel.ToArray());

            var specNaoConceito = new EscalaApuracaoFilterSpecification() { TipoDiferenteConceito = true };

            var specNaoConceitoNivel = new SMCAndSpecification<EscalaApuracao>(specInNivel, specNaoConceito)
                .SetOrderBy(o => o.Descricao);

            var selectItens = this.SearchProjectionBySpecification(specNaoConceitoNivel,
                p => new SMCDatasourceItem() { Seq = p.Seq, Descricao = p.Descricao });

            return selectItens
                .ToList();
        }

        /// <summary>
        /// Busca as escalas de Apuração que não sejam do tipo conceito e sejam do nível de ensino do configuração de componente
        /// </summary>
        /// <param name="seqConfiguracaoComponente">Sequencial da configuração do componente com o nível de ensino em questão</param>
        /// <returns>Dados das escação de Apuração</returns>
        public List<SMCDatasourceItem> BuscarEscalasApuracaoNivelEnsinoPorConfiguracaoComponenteSelect(long seqConfiguracaoComponente)
        {
            // Recupera nível ensino do curso associado ao curriculo curso oferta
            List<long> seqsNivelEnsino = this.ConfiguracaoComponenteDomainService.BuscarConfiguracaoComponenteNiveisEnsino(seqConfiguracaoComponente);

            // Recupera todas as escalas do nível de ensino do curso
            var specEscalasNivel = new InstituicaoNivelEscalaApuracaoFilterSpecification() { SeqsNivelEnsino = seqsNivelEnsino };
            var seqsEscalasNivel = this.InstituicaoNivelEscalaApuracaoDomainService
                .SearchProjectionBySpecification(specEscalasNivel, p => p.SeqEscalaApuracao);

            var specInNivel = new SMCContainsSpecification<EscalaApuracao, long>(p => p.Seq, seqsEscalasNivel.ToArray());

            var specNaoConceito = new EscalaApuracaoFilterSpecification() { TipoDiferenteConceito = true };

            var specNaoConceitoNivel = new SMCAndSpecification<EscalaApuracao>(specInNivel, specNaoConceito)
                .SetOrderBy(o => o.Descricao);

            var selectItens = this.SearchProjectionBySpecification(specNaoConceitoNivel,
                p => new SMCDatasourceItem() { Seq = p.Seq, Descricao = p.Descricao } , isDistinct:true);

            return selectItens
                .ToList();
        }

        /// <summary>
        /// Grava uma Escala de Apuração aplicando as validações
        /// </summary>
        /// <param name="escalaApuracao">Escala de Validação a ser gravada</param>
        /// <returns>Sequencial da Escala de Apuração gravada</returns>
        public long SalvarEscalaApuracao(EscalaApuracao escalaApuracao)
        {
            var specCriterios = new CriterioAprovacaoFilterSpecification() { SeqEscalaApuracao = escalaApuracao.Seq };

            escalaApuracao.CriteriosAprovacao = this.CriterioAprovacaoDomainService
                .SearchBySpecification(specCriterios)
                .ToList();

            this.SaveEntity(escalaApuracao, new EscalaApuracaoValidator());

            return escalaApuracao.Seq;
        }
    }
}