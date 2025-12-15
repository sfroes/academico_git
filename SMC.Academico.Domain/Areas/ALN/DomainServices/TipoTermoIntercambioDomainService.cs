using SMC.Academico.Common.Areas.ALN.Includes;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ALN.DomainServices
{
    public class TipoTermoIntercambioDomainService : AcademicoContextDomain<TipoTermoIntercambio>
    {
        #region [ Domain Services ]

        private InstituicaoNivelTipoTermoIntercambioDomainService InstituicaoNivelTipoTermoIntercambioDomainService
        {
            get { return Create<InstituicaoNivelTipoTermoIntercambioDomainService>(); }
        }

        private ParceriaIntercambioDomainService ParceriaIntercambioDomainService
        {
            get { return Create<ParceriaIntercambioDomainService>(); }
        }

        private ParceriaIntercambioTipoTermoDomainService ParceriaIntercambioTipoTermoDomainService => Create<ParceriaIntercambioTipoTermoDomainService>();

        #endregion [ Domain Services ]

        private InstituicaoNivelTipoVinculoAlunoDomainService InstituicaoNivelTipoVinculoAlunoDomainService
        {
            get { return Create<InstituicaoNivelTipoVinculoAlunoDomainService>(); }
        }

        public List<SMCDatasourceItem> BuscarTiposTermosIntercambiosInstituicaoNivelSelect()
        {
            return InstituicaoNivelTipoVinculoAlunoDomainService.SearchProjectionAll(x => new
            {
                Itens = x.TiposTermoIntercambio.Select(t => new SMCDatasourceItem
                {
                    Seq = t.TipoTermoIntercambio.Seq,
                    Descricao = t.TipoTermoIntercambio.Descricao
                })
            })?.SelectMany(x => x.Itens).SMCDistinct(i => i.Seq).ToList();
        }

        public List<SMCDatasourceItem> BuscarTiposTermosIntercambiosInstituicaoNivelPermiteAssociarAlunoSelect()
        {
            var retorno = new List<SMCDatasourceItem>();

            var lista = InstituicaoNivelTipoVinculoAlunoDomainService.SearchProjectionAll(x => new
            {
                Itens = x.TiposTermoIntercambio.Select(t => new
                {
                    Seq = t.TipoTermoIntercambio.Seq,
                    Descricao = t.TipoTermoIntercambio.Descricao,
                    PermiteAssociarAluno = t.TipoTermoIntercambio.PermiteAssociarAluno
                })
            }).SelectMany(x => x.Itens).SMCDistinct(i => i.Seq).ToList();

            foreach (var item in lista)
            {
                if (item.PermiteAssociarAluno)
                {
                    retorno.Add(new SMCDatasourceItem()
                    {
                        Seq = item.Seq,
                        Descricao = item.Descricao
                    });
                }
            }

            return retorno.OrderBy(c => c.Descricao).ToList();
        }

        /// <summary>
        /// Busca os tipos de termo de intercâmbio desconsiderando os filtros de instituição e nível de ensino
        /// </summary>
        /// <returns>Lista com todos os tipos de termos de intercâmbio</returns>
        public List<SMCDatasourceItem> BuscarTodosTiposTermosIntercambioSelect()
        {
            try
            {
                DisableFilter(FILTER.INSTITUICAO_ENSINO);
                DisableFilter(FILTER.NIVEL_ENSINO);
                return SearchProjectionAll(p => new SMCDatasourceItem()
                {
                    Seq = p.Seq,
                    Descricao = p.Descricao
                }, o => o.Descricao).ToList();
            }
            finally
            {
                EnableFilter(FILTER.INSTITUICAO_ENSINO);
                EnableFilter(FILTER.NIVEL_ENSINO);
            }
        }

        /// <summary>
        /// Busca os tipos de termo de intercâmbio considerando os filtros de instituição e nível de ensino
        /// </summary>
        /// <returns>Lista com todos os tipos de termos de intercâmbio</returns>
        public List<SMCDatasourceItem> BuscarTiposTermosIntercambioSelect()
        {
            return SearchProjectionAll(p => new SMCDatasourceItem()
            {
                Seq = p.Seq,
                Descricao = p.Descricao
            }, o => o.Descricao).ToList();
        }

        public List<SMCDatasourceItem> BuscarTiposTermosIntercambiosDaParceriaSelect(long seqParceriaIntercambio, long seqNivelEnsino)
        {
            var seqsTiposTermoParceria = ParceriaIntercambioDomainService.SearchProjectionByKey(new SMCSeqSpecification<ParceriaIntercambio>(seqParceriaIntercambio),
                p => p.TiposTermo.Select(s => s.SeqTipoTermoIntercambio)).ToList();

            var seqsTiposTermoNivel = InstituicaoNivelTipoVinculoAlunoDomainService.SearchProjectionBySpecification(new InstituicaoNivelTipoVinculoAlunoFilterSpecification() { SeqNivelEnsino = (seqNivelEnsino != 0 ? (long?)seqNivelEnsino : null) },
                p => p.TiposTermoIntercambio.Select(s => s.SeqTipoTermoIntercambio)).SelectMany(s => s).ToList();

            var seqsTiposTermoParceriaNivel = seqsTiposTermoParceria.Intersect(seqsTiposTermoNivel).ToArray();

            var specParcerias = new SMCContainsSpecification<TipoTermoIntercambio, long>(p => p.Seq, (seqNivelEnsino == default(long)) ? seqsTiposTermoParceria.ToArray() : seqsTiposTermoParceriaNivel);
            specParcerias.SetOrderBy(o => o.Descricao);
            return SearchProjectionBySpecification(specParcerias, p => new SMCDatasourceItem()
            {
                Seq = p.Seq,
                Descricao = p.Descricao
            }).ToList();
        }

        /// <summary>
        /// Verifica se o tipo de intercambio concede formação de acordo com os paraêtros de vinculo do aluno
        /// </summary>
        /// <param name="seqTipoTermoIntercambio">Sequencial do tipo de termo de intercambio</param>
        /// <param name="seqTipoVinculoAluno">Sequencial do tipo de vinculo do aluno</param>
        /// <param name="seqNivelEnsino">Sequencial de nivel de ensino</param>
        /// <param name="seqInstituicao">Sequencial da instituicao</param>
        /// <returns>Retorna se o tipo termo de intercambio concede formação</returns>
        public bool TipoTermoIntercambioConcedeFormacao(long seqTipoTermoIntercambio, long seqTipoVinculoAluno, long seqNivelEnsino, long seqInstituicao)
        {
            var paramSpec = new InstituicaoNivelTipoVinculoAlunoFilterSpecification()
            {
                SeqTipoVinculoAluno = seqTipoVinculoAluno,
                SeqNivelEnsino = seqNivelEnsino,
                SeqInstituicao = seqInstituicao
            };
            var instituicaoNivelTipoVinculoAluno = InstituicaoNivelTipoVinculoAlunoDomainService.SearchProjectionByKey(paramSpec,
                                                        x => new { x.Seq, x.ConcedeFormacao });
            var intercambioSpec = new InstituicaoNivelTipoTermoIntercambioFilterSpecification()
            {
                SeqTipoTermoIntercambio = seqTipoTermoIntercambio,
                SeqInstituicaoNivelTipoVinculoAluno = instituicaoNivelTipoVinculoAluno.Seq
            };
            bool? termoIntercambioConcedeFormacao = InstituicaoNivelTipoTermoIntercambioDomainService.SearchProjectionByKey(intercambioSpec, x => x.ConcedeFormacao);
            return termoIntercambioConcedeFormacao.GetValueOrDefault();
        }

        /// <summary>
        /// Verifica se o tipo de intercambio concede formação de acordo com os paraêtros de vinculo do aluno
        /// </summary>
        /// <param name="seqTipoTermoIntercambio">Sequencial do tipo de termo de intercambio</param>
        /// <param name="seqInstituicaoNivelTipoVinculoAluno">Sequencial da instituicao nivel tipo vinculo do aluno</param>
        /// <returns>Retorna se tipo termo de intercambio concede formação</returns>
        public bool TipoTermoIntercambioConcedeFormacao(long seqTipoTermoIntercambio, long seqInstituicaoNivelTipoVinculoAluno)
        {
            var intercambioSpec = new InstituicaoNivelTipoTermoIntercambioFilterSpecification()
            {
                SeqTipoTermoIntercambio = seqTipoTermoIntercambio,
                SeqInstituicaoNivelTipoVinculoAluno = seqInstituicaoNivelTipoVinculoAluno
            };
            bool? termoIntercambioConcedeFormacao = InstituicaoNivelTipoTermoIntercambioDomainService.SearchProjectionByKey(intercambioSpec, x => x.ConcedeFormacao);
            return termoIntercambioConcedeFormacao.GetValueOrDefault();
        }

        /// <summary>
        /// Buscar tipos termo intercambio por nivel de ensino e parceria de intercambio
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial nivel de ensino</param>
        /// <param name="seqParceriaIntercambio">Sequencial parceria intercambio</param>
        /// /// <param name="ativo">Indicativo situacao</param>
        /// <returns>Tipos de termo de intercambio</returns>
        public List<SMCDatasourceItem> BuscaTiposTermoIntercabioPorParceriaIntercambioTipoTermoSelect(long? seqNivelEnsino, long seqParceriaIntercambio, bool? ativo)
        {
            ///Recupera todas as parcerias tipo termo referente a parceria
            var specParceriaIntercambioTipoTermo = new ParceriaIntercambioTipoTermoFilterSpecification() { SeqParceriaIntercambio = seqParceriaIntercambio, Ativo = ativo };

            var parceriaIntercambioTiposTermos = this.ParceriaIntercambioTipoTermoDomainService.SearchProjectionBySpecification(specParceriaIntercambioTipoTermo, p => new
            {
                p.Seq,
                p.SeqParceriaIntercambio,
                p.SeqTipoTermoIntercambio,
                p.TipoTermoIntercambio
            }).ToList();

            ///Recupera todos os tipos de termo do nivel de ensino
            var seqsTiposTermoNivelEnsino = this.InstituicaoNivelTipoVinculoAlunoDomainService.SearchProjectionBySpecification(new InstituicaoNivelTipoVinculoAlunoFilterSpecification() { SeqNivelEnsino = seqNivelEnsino },
                                        p => p.TiposTermoIntercambio.Select(s => s.SeqTipoTermoIntercambio)).SelectMany(s => s).ToList();

            List<SMCDatasourceItem> retorno = new List<SMCDatasourceItem>();

            ///Verifica se o tipo de ensino esta salvo na parceria tipo termo e gera o select
            foreach (var item in parceriaIntercambioTiposTermos)
            {
                ///Faz o filtro da seleção levando em consideração se o nivel de ensino foi selecionado ou não
                if (seqsTiposTermoNivelEnsino.Contains(item.SeqTipoTermoIntercambio))
                {
                    retorno.Add(new SMCDatasourceItem()
                    {
                        Seq = item.Seq,
                        Descricao = item.TipoTermoIntercambio.Descricao
                    });
                }
            }

            return retorno;
        }

        /// <summary>
        /// Valida se o tipo de termo intercambio é Cotutela
        /// </summary>
        /// <param name="seq">Sequencial tipo termo intercambio</param>
        /// <returns>Boleano caso</returns>
        public bool ValidarTipoTermoIntercambioCoutela(long seq)
        {
            //FIX: Conforme foi combinado será tratado neste momento como o texto da descrição até ter uma parametrização
            var cotutela = "cotutela";

            var tipoTermoIntercambio = this.SearchByKey(new SMCSeqSpecification<TipoTermoIntercambio>(seq));

            return cotutela == tipoTermoIntercambio.Descricao.ToLower();
        }
    }
}