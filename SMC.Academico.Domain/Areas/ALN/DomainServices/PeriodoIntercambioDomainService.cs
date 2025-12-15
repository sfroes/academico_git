using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Framework.Domain;
using System;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ALN.DomainServices
{
    public class PeriodoIntercambioDomainService : AcademicoContextDomain<PeriodoIntercambio>
    {

        #region DomainService

        private AlunoDomainService AlunoDomainService
        {
            get => Create<AlunoDomainService>();
        }

        #endregion

        /// <summary>
        /// Buscar dados do intercambio do aluno baseado no periodo de intercambio dele
        /// </summary>
        /// <param name="seqPeriodoIntercambio">Sequancial Periodo Intercambio</param>
        /// <returns>Dados do intercambio</returns>
        public PessoaAtuacaoTermoIntercambioVO BuscarDadosIntercambioPorAluno(long seqPeriodoIntercambio)
        {
            var spec = new PeriodoIntercambioFilterSpecification() { Seq = seqPeriodoIntercambio };

            var dados = this.SearchProjectionBySpecification(spec, p => new PessoaAtuacaoTermoIntercambioVO()
            {
                DataInicio = p.DataInicio,
                DataFim = p.DataFim,
                DescricaoTipoIntercambio = p.PessoaAtuacaoTermoIntercambio.TermoIntercambio.ParceriaIntercambioTipoTermo.TipoTermoIntercambio.Descricao,
                DescricaoTermoIntercambio = p.PessoaAtuacaoTermoIntercambio.TermoIntercambio.Descricao,
                InstituicaoExterna = p.PessoaAtuacaoTermoIntercambio.TermoIntercambio.ParceriaIntercambioInstituicaoExterna.InstituicaoExterna.Nome
            }).FirstOrDefault();

            return dados;

        }
    }
}