using SMC.Academico.Common.Areas.TUR.Enums;
using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.Domain.Areas.TUR.Models;
using SMC.Academico.Domain.Areas.TUR.Specifications;
using SMC.Framework.Domain;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.TUR.DomainServices
{
    public class TipoTurmaDomainService : AcademicoContextDomain<TipoTurma>
    {
        #region [ DomainService ]

        private ComponenteCurricularDomainService ComponenteCurricularDomainService => Create<ComponenteCurricularDomainService>();
        private MatrizCurricularOfertaDomainService MatrizCurricularOfertaDomainService => Create<MatrizCurricularOfertaDomainService>();

        private ConfiguracaoComponenteDomainService ConfiguracaoComponenteDomainService
        {
            get { return this.Create<ConfiguracaoComponenteDomainService>(); }
        }

        private InstituicaoNivelTipoTurmaDomainService InstituicaoNivelTipoTurmaDomainService
        {
            get { return this.Create<InstituicaoNivelTipoTurmaDomainService>(); }
        }

        #endregion [ DomainService ]

        /// <summary>
        /// Busca o tipo de turma pelo sequencial
        /// </summary>
        /// <param name="seq"></param>
        /// <returns>Registro tipo de turma</returns>
        public TipoTurma BuscarTipoTurma(long seq)
        {
            var tipo = this.SearchByKey(new SMCSeqSpecification<TipoTurma>(seq));
            return tipo;
        }

        /// <summary>
        /// Busca o select de tipo de turma
        /// </summary>
        /// <returns>Lista de tipos de turma</returns>
        public List<SMCDatasourceItem> BuscarTiposTurmasSelect()
        {
            var tipos = InstituicaoNivelTipoTurmaDomainService.SearchProjectionAll(p => new SMCDatasourceItem()
            {
                Seq = p.SeqTipoTurma,
                Descricao = p.TipoTurma.Descricao
            }, isDistinct: true).OrderBy(o => o.Descricao).ToList();

            return tipos;
        }

        /// <summary>
        /// Busca o select de tipo de turma curricular de acordo com o seqConfiguracaoComponente para obter o instituição nível
        /// </summary>
        /// <param name="seqConfiguracaoComponente">Sequencial da configuração do componente selecionado</param>
        /// <returns>Lista de tipos de turma com instituição nível associado</returns>
        public List<SMCDatasourceItem> BuscarTiposTurmasPorConfiguracaoComponenteSelect(long seqConfiguracaoComponente)
        {
            try
            {
                if (seqConfiguracaoComponente == 0)
                    return new List<SMCDatasourceItem>();

                var seqInstituicaoNivel = ConfiguracaoComponenteDomainService.BuscarConfiguracaoComponenteInstituicaoNivel(seqConfiguracaoComponente);

                var spec = new InstituicaoNivelTipoTurmaFilterSpecification() { SeqInstituicaoNivel = seqInstituicaoNivel };

                var tipos = InstituicaoNivelTipoTurmaDomainService.SearchProjectionBySpecification(spec, p => new SMCDatasourceItem()
                {
                    Seq = p.SeqTipoTurma,
                    Descricao = p.TipoTurma.Descricao
                }).OrderBy(o => o.Descricao).ToList();

                return tipos;
            }
            catch (Exception)
            {
                return new List<SMCDatasourceItem>();
            }
        }

        /// <summary>
        /// Verifica se existe associação do tipo de turma com a configuração de componente selecionada como principal
        /// </summary>
        /// <param name="seqTipoTurma"></param>
        /// <param name="seqConfiguracaoComponente"></param>
        /// <returns>Retorna se exite associação</returns>
        public bool VerificarTipoTurmaInstituicaoNivel(long seqTipoTurma, long seqConfiguracaoComponente)
        {
            var seqsNivelEnsino = ConfiguracaoComponenteDomainService.BuscarConfiguracaoComponenteNiveisEnsino(seqConfiguracaoComponente);

            var spec = new InstituicaoNivelTipoTurmaFilterSpecification() { SeqTipoTurma = seqTipoTurma, SeqsNivelEnsino = seqsNivelEnsino.ToArray() };

            var tipos = InstituicaoNivelTipoTurmaDomainService.SearchProjectionBySpecification(spec, p => p.Seq).Any();

            return tipos;
        }

        public List<TipoTurma> BuscarTiposTurmasPorComponenteCurricular(long seqComponenteCurricular, List<long> seqsMatrizesCurricularesOferta)
        {
            /*  O campo “Tipo de turma” no cabeçalho do passo 6 deverá ser calculado todas as vezes que a interface for exibida, conforme:
                Verificar se a turma possui mais de uma oferta de matriz associada.
                - Caso existir, associar o tipo de turma que exige mais de uma oferta de matriz e está parametrizado por instituição-nível,
                  de acordo com a instituição do componente da configuração principal e em comum em todos os níveis de ensino dos cursos dos currículos das ofertadas de matriz associadas na turma.
                - Caso não existir, associar o tipo de turma que exige apenas uma oferta de matriz e está parametrizado por instituição-nível, de acordo com a instituição do componente da 
                  configuração principal e em comum em todos os níveis de ensino dos cursos dos currículos das ofertadas de matriz associadas na turma.
            */

            // Recupera a institui
            var seqInstituicaoEnsino = ComponenteCurricularDomainService.SearchProjectionByKey(seqComponenteCurricular, x => x.SeqInstituicaoEnsino);

            var seqsNiveisEnsino = MatrizCurricularOfertaDomainService.SearchProjectionBySpecification(new MatrizCurricularOfertaFilterSpecification
            {
                Seqs = seqsMatrizesCurricularesOferta
            }, x => x.MatrizCurricular.CurriculoCursoOferta.CursoOferta.Curso.SeqNivelEnsino).Distinct().ToArray();

            return InstituicaoNivelTipoTurmaDomainService.SearchProjectionBySpecification(new InstituicaoNivelTipoTurmaFilterSpecification
            {
                SeqInstituicaoEnsino = seqInstituicaoEnsino,
                SeqsNivelEnsino = seqsNiveisEnsino
            }, x => x.TipoTurma).Distinct().ToList();

        }

        /// <summary>
        /// RN 39 - Se o tipo de turma estiver parametrizado para não permitir associação de oferta de matriz, verirficar se a
        /// configuração principal é de um componente que exige associação de assunto de componete.Caso seja, abortar a
        /// operação e enviar a seguinte mensagem: "Não é possível prosseguir. Para o tipo de turma em questão, é necessário selecionar uma configuração de
        /// componente que seja de um componente que não exige associação de assunto de componente."
        /// </summary>
        /// <param name="seqTipoTurma"></param>
        /// <param name="seqConfiguracaoComponente"></param>
        /// <returns>Retorna se permite este tipo de turma com a configuração principal</returns>
        public bool VerificarTipoTurmaConfiguracaoAssunto(long seqTipoTurma, long seqConfiguracaoComponente)
        {
            var tipoAssociacao = this.SearchProjectionByKey(new SMCSeqSpecification<TipoTurma>(seqTipoTurma), p => p.AssociacaoOfertaMatriz);

            if (tipoAssociacao == AssociacaoOfertaMatriz.NaoPermite && ConfiguracaoComponenteDomainService.VerificaConfiguracaoComponenteExigeAssunto(seqConfiguracaoComponente))
                return false;
            else
                return true;
        }

        /// <summary>
        /// Busca tipos de turma, conforme o parâmetro
        /// </summary>
        /// <returns>Lista de tipos de turma</returns>
        public List<SMCDatasourceItem> BuscarTiposTurmasSelectPorNivelEnsino(long seqNivelEnsino)
        {
            var spec = new InstituicaoNivelTipoTurmaFilterSpecification() { SeqNivelEnsino = seqNivelEnsino };

            var tipos = InstituicaoNivelTipoTurmaDomainService.SearchProjectionBySpecification(spec, p => new SMCDatasourceItem()
            {
                Seq = p.SeqTipoTurma,
                Descricao = p.TipoTurma.Descricao
            }, isDistinct: true).OrderBy(o => o.Descricao).ToList();

            return tipos;
        }
    }
}