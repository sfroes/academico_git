using SMC.Academico.Common.Areas.FIN.Exceptions;
using SMC.Academico.Common.Areas.FIN.Includes;
using SMC.Academico.Domain.Areas.FIN.Models;
using SMC.Academico.Domain.Areas.FIN.Specifications;
using SMC.Academico.Domain.Areas.FIN.ValueObjects;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using System.Linq;

namespace SMC.Academico.Domain.Areas.FIN.DomainServices
{
    public class TermoAdesaoDomainService : AcademicoContextDomain<TermoAdesao>
    {
        #region DomainServices

        private ContratoDomainService ContratoDomainService { get => Create<ContratoDomainService>(); }

        #endregion DomainServices

        public SMCPagerData<TermoAdesaoListarVO> ListarTermosAdesao(TermoAdesaoFiltroVO filtroVO)
        {
            var spec = filtroVO.Transform<TermoAdesaoFilterSpecification>(filtroVO);

            int total = 0;
            var lista = this.SearchBySpecification(spec, out total, IncludesTermoAdesao.TipoVinculoAluno | IncludesTermoAdesao.Servico);

            var result = lista.TransformList<TermoAdesaoListarVO>();

            result.SMCForEach(f => f.Ativo = (f.Ativo == "True") ? "Sim" : "Não");

            return new SMCPagerData<TermoAdesaoListarVO>(result, total);
        }

        /// <summary>
        /// Busca o termo de adesão para o contrato do curso ou nível de ensino informados
        /// </summary>
        /// <param name="vo">Dados do curso ou nível de ensino</param>
        /// <returns>Dados do termo de adesão</returns>
        /// <exception cref="TermoAdesaoSemContratoVigenteException">Caso não seja encontrado um contrato vigente vinculado ao curso ou ao nível de ensino</exception>
        public long BuscarTermoAdesao(TermoAdesaoSolicitacaoMatriculaVO vo)
        {
            Contrato contrato = null;
            if (vo.TipoOfertaExigeCurso)
            {
                // Retorna o contrato vigente vinculado ao curso
                contrato = ContratoDomainService.SearchByKey(new ContratoVigenteSpecification() { SeqCurso = vo.SeqCurso });
            }
            if (contrato == null)
            {
                // Se não encontrar um contrato vigente vinculado ao curso, busca o contrato associado à Instituição e ao nível de ensino
                contrato = ContratoDomainService.SearchByKey(new ContratoVigenteSpecification()
                {
                    SeqInstituicaoEnsino = vo.SeqInstituicaoEnsino,
                    SeqNivelEnsino = vo.SeqNivelEnsino
                });
            }

            if (contrato == null)
                throw new TermoAdesaoSemContratoVigenteException();

            return SearchProjectionByKey(new TermoAdesaoFilterSpecification()
            {
                Ativo = true,
                SeqContrato = contrato.Seq,
                SeqTipoVinculoAluno = vo.SeqTipoVinculoAluno,
                SeqServico = vo.SeqServico
            }, x => x.Seq);
        }

        public long SalvarTermoAdesao(TermoAdesaoVO vo)
        {
            var termo = vo.Transform<TermoAdesao>();

            var seqsTermosAtivos = this.SearchProjectionBySpecification(
                new TermoAdesaoAtivoSpecification()
                {
                    SeqContrato = vo.SeqContrato,
                    SeqServico = vo.SeqServico,
                    SeqTipoVinculoAluno = vo.SeqTipoVinculoAluno
                }, t => new { Seq = termo.Seq }).ToList();

            if (seqsTermosAtivos.Count >= 1 && vo.Ativo.Value && vo.Seq != seqsTermosAtivos.First().Seq)
                throw new ContratoComTermoAtivoException();

            this.SaveEntity(termo);

            return termo.Seq;
        }
    }
}