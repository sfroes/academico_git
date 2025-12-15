using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Framework.Domain;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.PES.DomainServices
{
    public class PessoaAtuacaoCondicaoObrigatoriedadeDomainService : AcademicoContextDomain<PessoaAtuacaoCondicaoObrigatoriedade>
    {
        #region [ Domain Services ]

        private CondicaoObrigatoriedadeDomainService CondicaoObrigatoriedadeDomainService { get => Create<CondicaoObrigatoriedadeDomainService>(); }

        private IngressanteDomainService IngressanteDomainService { get => Create<IngressanteDomainService>(); }

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService { get => Create<PessoaAtuacaoDomainService>(); }

        private AlunoDomainService AlunoDomainService { get => Create<AlunoDomainService>(); }

        #endregion [ Domain Services ]

        public PessoaAtuacaoCondicaoObrigatoriedadeVO BuscarPessoaAtuacaoCondicaoObrigatoriedade(long seqPessoaAtuacao)
        {
            var data = new PessoaAtuacaoCondicaoObrigatoriedadeVO();
            data.CondicoesObrigatoriedade = new List<CondicoesObrigatoriedadeVO>();

            var pessoaAtuacao = PessoaAtuacaoDomainService.BuscarPessoaAtuacaoConfiguracao(seqPessoaAtuacao);

            if (pessoaAtuacao.SeqMatrizCurricularOferta == null)
                return data;

            var condicoes = CondicaoObrigatoriedadeDomainService.BuscarCondicoesObrigatoriedadePorMatrizCurricularOferta(pessoaAtuacao.SeqMatrizCurricularOferta.Value);

            PessoaAtuacaoCondicaoObrigatoriedadeFilterSpecification spec = new PessoaAtuacaoCondicaoObrigatoriedadeFilterSpecification();
            spec.SeqPessoaAtuacao = seqPessoaAtuacao;
            List<PessoaAtuacaoCondicaoObrigatoriedade> registros = SearchBySpecification(spec, a => a.CondicaoObrigatoriedade).ToList();

            if (registros != null && registros.Count > 0)
            {
                foreach (var item in registros)
                {
                    data.CondicoesObrigatoriedade.Add(new CondicoesObrigatoriedadeVO()
                    {
                        Ativo = item.Ativo,
                        DescricaoCondicaoObrigatoriedade = item.CondicaoObrigatoriedade.Descricao,
                        Seq = item.Seq,
                        SeqCondicaoObrigatoriedade = item.SeqCondicaoObrigatoriedade,
                        SeqPessoaAtuacao = item.SeqPessoaAtuacao
                    });

                    //Removendo a condição que já existe na lista
                    if (condicoes.Any(a => a.Seq == item.SeqCondicaoObrigatoriedade))
                    {
                        condicoes.Remove(condicoes.Where(a => a.Seq == item.SeqCondicaoObrigatoriedade).FirstOrDefault());
                    }
                }
            }
            foreach (var item in condicoes)
            {
                data.CondicoesObrigatoriedade.Add(new CondicoesObrigatoriedadeVO()
                {
                    DescricaoCondicaoObrigatoriedade = item.Descricao,
                    SeqCondicaoObrigatoriedade = item.Seq,
                    SeqPessoaAtuacao = seqPessoaAtuacao
                });
            }

            data.PossuiSituacaoImpeditivaIngressante = pessoaAtuacao.SeqIngressante.HasValue &&
                IngressanteDomainService.ValidarSituacaoImpeditivaIngressante(seqPessoaAtuacao);

            return data;
        }

        /// <summary>
        /// Buscar os sequenciais de condições de obrigatoriedade ativos para a integralização curricular
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <returns>Lista de condições de obrigatoriedade ativos</returns>
        public List<long> BuscarSequenciaisCondicaoObrigatoriedadePessoaAtuacao(long seqPessoaAtuacao)
        {
            var spec = new PessoaAtuacaoCondicaoObrigatoriedadeFilterSpecification() { SeqPessoaAtuacao = seqPessoaAtuacao, Ativo = true };

            var seqsCondicoes = SearchProjectionBySpecification(spec, p => p.SeqCondicaoObrigatoriedade).ToList();

            return seqsCondicoes;
        }
    }
}