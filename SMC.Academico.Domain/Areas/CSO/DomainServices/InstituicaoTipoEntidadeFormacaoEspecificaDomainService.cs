using SMC.Academico.Common.Areas.CSO.Includes;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Academico.Domain.Areas.CSO.ValueObjects;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CSO.DomainServices
{
    public class InstituicaoTipoEntidadeFormacaoEspecificaDomainService : AcademicoContextDomain<InstituicaoTipoEntidadeFormacaoEspecifica>
    {
        #region [ DomainService ]

        private TipoEntidadeDomainService TipoEntidadeDomainService => Create<TipoEntidadeDomainService>();

        private FormacaoEspecificaDomainService FormacaoEspecificaDomainService => Create<FormacaoEspecificaDomainService>();

        private CursoDomainService CursoDomainService => Create<CursoDomainService>();

        private TipoFormacaoEspecificaDomainService TipoFormacaoEspecificaDomainService => Create<TipoFormacaoEspecificaDomainService>();

        #endregion [ DomainService ]

        /// <summary>
        /// Busca todos os tipos de entidades com associação de ingressante obrigatório por instituição e token de tipo programa
        /// </summary>
        /// <param name="seqInstituicao">Sequencial da instituição</param>
        /// <returns>Lista de objeto com os respectivos filhos</returns>
        public List<InstituicaoTipoEntidadeFormacaoEspecificaVO> BuscarTiposObrigatorioComFilhos(long seqInstituicao)
        {
            var specInstituicao = new InstituicaoTipoEntidadeFormacaoEspecificaFilterSpecification() { SeqInstituicao = seqInstituicao, TokenEntidade = TOKEN_TIPO_ENTIDADE_EXTERNADA.PROGRAMA };
            specInstituicao.MaxResults = int.MaxValue;

            var tipoFormacaoInstituicao = this.SearchBySpecification(specInstituicao,
                IncludesInstituicaoTipoEntidadeFormacaoEspecifica.TipoFormacaoEspecifica |
                IncludesInstituicaoTipoEntidadeFormacaoEspecifica.TipoFormacaoEspecificaPai |
                IncludesInstituicaoTipoEntidadeFormacaoEspecifica.TiposFormacaoEspecificasFilhas).ToList();

            var tipoFormacaoObrigatorio = tipoFormacaoInstituicao.Where(w => w.ObrigatorioAssociacaoIngressante
                                                                 && (w.TipoFormacaoEspecificaPai == null || !w.TipoFormacaoEspecificaPai.ObrigatorioAssociacaoIngressante))
                                                                 .TransformList<InstituicaoTipoEntidadeFormacaoEspecificaVO>();

            Func<InstituicaoTipoEntidadeFormacaoEspecificaVO, List<long>> funcFilhos = null;
            funcFilhos = (node) =>
            {
                var retFunc = new List<long>();
                if (node != null)
                {
                    retFunc.Add(node.SeqTipoFormacaoEspecifica);

                    var formacoesFilhas = tipoFormacaoInstituicao.Where(w => w.Seq == node.Seq).First();

                    if (formacoesFilhas != null && formacoesFilhas.TiposFormacaoEspecificasFilhas.Count() > 0)
                        foreach (var formacaoFilha in formacoesFilhas.TiposFormacaoEspecificasFilhas)
                            retFunc.AddRange(funcFilhos(formacaoFilha.Transform<InstituicaoTipoEntidadeFormacaoEspecificaVO>()));
                }
                return retFunc;
            };

            Func<InstituicaoTipoEntidadeFormacaoEspecificaVO, List<InstituicaoTipoEntidadeFormacaoEspecificaVO>> funcPais = null;
            funcPais = (node) =>
            {
                var retFunc = new List<InstituicaoTipoEntidadeFormacaoEspecificaVO>();
                if (node != null)
                {
                    var formacoesFilhas = tipoFormacaoInstituicao.Where(w => w.Seq == node.Seq).First();

                    node.SeqsFilhos = new List<long>();

                    if (formacoesFilhas != null && formacoesFilhas.TiposFormacaoEspecificasFilhas.Count() > 0)
                        foreach (var formacaoFilha in formacoesFilhas.TiposFormacaoEspecificasFilhas)
                            node.SeqsFilhos.AddRange(funcFilhos(formacaoFilha.Transform<InstituicaoTipoEntidadeFormacaoEspecificaVO>()));

                    node.SeqsFilhos.Add(node.SeqTipoFormacaoEspecifica);
                    retFunc.Add(node);
                }
                return retFunc;
            };

            List<InstituicaoTipoEntidadeFormacaoEspecificaVO> tiposObrigatoriosCompletos = new List<InstituicaoTipoEntidadeFormacaoEspecificaVO>();
            // Func para buscar os filhos de todas os tipos de formação específica obrigatórias
            foreach (var item in tipoFormacaoObrigatorio)
            {
                tiposObrigatoriosCompletos.AddRange(funcPais(item));
            }

            return tiposObrigatoriosCompletos;
        }

        /// <summary>
        /// Recupera os tipos de formação específica para um tipo de entidade com o nível de cada tipo na hierarquia
        /// </summary>
        /// <param name="seqTipoEntidade">Sequncial do tipo da entidade</param>
        /// <returns>Tipos de formação ordenados por nível na hierarquia</returns>
        public List<InstituicaoTipoEntidadeFormacaoEspecificaVO> BuscarTiposComNivelHierarquia(long seqTipoEntidade)
        {
            var specTipos = new InstituicaoTipoEntidadeFormacaoEspecificaFilterSpecification() { SeqTipoEntidade = seqTipoEntidade };
            var tipos = this.SearchBySpecification(specTipos, IncludesInstituicaoTipoEntidadeFormacaoEspecifica.TipoFormacaoEspecifica)
                .TransformList<InstituicaoTipoEntidadeFormacaoEspecificaVO>();
            tipos.SMCForEach(f => f.NivelHierarquia = CalcularNivelTipoFormacao(tipos, f.Seq));
            return tipos.OrderBy(o => o.NivelHierarquia).ToList();
        }

        private int CalcularNivelTipoFormacao(IEnumerable<InstituicaoTipoEntidadeFormacaoEspecificaVO> hierarquia, long seqTipoFormacaoEspecifica)
        {
            var formacao = hierarquia.FirstOrDefault(f => f.Seq == seqTipoFormacaoEspecifica);
            return formacao == null ? 0 : CalcularNivelTipoFormacao(hierarquia, formacao.SeqPai.GetValueOrDefault()) + 1;
        }

        /// <summary>
        /// Busca as hierarquia de formação específica por tipo de entidade na instiuição
        /// </summary>
        /// <param name="seqTipoEntidade">Sequencial do tipo da entidade</param>
        /// <returns>Hierarquias de tipo de formação específica configuradas para o tipo de entidade informado de forma aninhada</returns>
        public List<InstituicaoTipoEntidadeFormacaoEspecificaVO> BuscarHierarquiaTipoFormacaoPorTipoEntidade(long seqTipoEntidade)
        {
            var spec = new InstituicaoTipoEntidadeFormacaoEspecificaFilterSpecification() { SeqTipoEntidade = seqTipoEntidade };
            spec.SetOrderBy(o => o.TipoFormacaoEspecifica.Descricao);
            var tiposFormacao = SearchProjectionBySpecification(spec, p => new InstituicaoTipoEntidadeFormacaoEspecificaVO()
            {
                Seq = p.Seq,
                SeqPai = p.TipoFormacaoEspecificaPai.Seq,
                SeqTipoFormacaoEspecifica = p.SeqTipoFormacaoEspecifica,
                DescricaoTipoFormacaoEspecifica = p.TipoFormacaoEspecifica.Descricao
            }).ToList();

            var raizes = new List<InstituicaoTipoEntidadeFormacaoEspecificaVO>(tiposFormacao.Where(w => !w.SeqPai.HasValue));
            raizes.SMCForEach(f => PopularArvore(tiposFormacao, ref f));

            return raizes;
        }

        /// <summary>
        /// Busca as hierarquia de formação específica por token
        /// </summary>
        /// <param name="token">Token da entidade</param>
        /// <returns>Hierarquias de tipo de formação específica configuradas para o token informado</returns>
        public List<InstituicaoTipoEntidadeFormacaoEspecificaVO> BuscarHierarquiaTipoFormacaoPorTipoEntidade(string token)
        {
            var spec = new InstituicaoTipoEntidadeFormacaoEspecificaFilterSpecification() { TokenEntidade = token };
            spec.SetOrderBy(o => o.TipoFormacaoEspecifica.Descricao);
            var tiposFormacao = SearchProjectionBySpecification(spec, p => new InstituicaoTipoEntidadeFormacaoEspecificaVO()
            {
                Seq = p.Seq,
                SeqPai = p.TipoFormacaoEspecificaPai.Seq,
                SeqTipoFormacaoEspecifica = p.SeqTipoFormacaoEspecifica,
                DescricaoTipoFormacaoEspecifica = p.TipoFormacaoEspecifica.Descricao
            }).ToList();

            return tiposFormacao;
        }

        /// <summary>
        /// Retorna a lista plana de tipos de formação específica por entidade ordenadas pela hierarquia
        /// </summary>
        /// <param name="seqTipoEntidade">Sequencial do tipo da entidade</param>
        /// <returns>Lista com os tipos de formação específicas planas</returns>
        public List<InstituicaoTipoEntidadeFormacaoEspecificaVO> BuscarTipoFormacaoPorTipoEntidade(long seqTipoEntidade)
        {
            var retorno = new List<InstituicaoTipoEntidadeFormacaoEspecificaVO>();
            BuscarHierarquiaTipoFormacaoPorTipoEntidade(seqTipoEntidade).SMCForEach(f => TransformarHierarquiaEmListaOrdenada(f, ref retorno));
            return retorno;
        }

        /// <summary>
        /// Valida todas árvores de formações específicas obrigatórias foram informadas
        /// Ex:
        /// Formações obrigatorias
        /// ├─ Área de concentração (obrigatória)
        /// ├── Linha de pesquisa (obrigatória)
        /// └─── Eixo temático (obrigatório)
        /// └─ Área temática (obrigatória)
        /// Informado um eixo temático e uma área temática as duas árvores seriam atendidas.
        /// </summary>
        /// <param name="seqCurso">Sequencial do curso da pessoa atuação</param>
        /// <param name="seqsFormacoes">Sequenciais das formações específicas associadas à pessoa atuação</param>
        /// <param name="aluno">True para aluno False para ingressante</param>
        /// <returns>True caso todas orientações obri</returns>
        public bool ValidarObrigatoriedadeFormacoesPorCurso(long seqCurso, IEnumerable<long> seqsFormacoes, bool aluno = true)
        {
            return !BuscarObrigatoriedadeFormacoesNaoAtendidasPorCurso(seqCurso, seqsFormacoes, false, aluno).SMCAny();
        }

        /// <summary>
        /// Valida todas árvores de formações específicas obrigatórias foram informadas
        /// Ex:
        /// Formações obrigatorias
        /// ├─ Área de concentração (obrigatória)
        /// ├── Linha de pesquisa (obrigatória)
        /// └─── Eixo temático (obrigatório)
        /// └─ Área temática (obrigatória)
        /// Informado um eixo temático e uma área temática as duas árvores seriam atendidas.
        /// </summary>
        /// <param name="seqCurso">Sequencial do curso da pessoa atuação</param>
        /// <param name="seqsFormacoes">Sequenciais das formações específicas associadas à pessoa atuação</param>
        /// <param name="validarQuantidades">Define se serão validadas as quantidades ou somentes se todos tipo obrigatórios foram informados</param>
        /// <param name="aluno">True para aluno False para ingressante</param>
        /// <returns>True caso todas orientações obri</returns>
        public IEnumerable<InstituicaoTipoEntidadeFormacaoEspecifica> BuscarObrigatoriedadeFormacoesNaoAtendidasPorCurso(long seqCurso, IEnumerable<long> seqsFormacoes, bool validarQuantidades, bool aluno = true)
        {
            if (!seqsFormacoes.SMCAny())
            {
                seqsFormacoes = new List<long>();
            }
            // Recupera todos tipos de formações obrigatórias na instiuição
            var seqTipoPrograma = TipoEntidadeDomainService.BuscarTipoEntidadeNaInstituicao(TOKEN_TIPO_ENTIDADE_EXTERNADA.PROGRAMA).Seq;
            var specTiposFormacoesObrigatorios = new InstituicaoTipoEntidadeFormacaoEspecificaFilterSpecification()
            {
                SeqTipoEntidade = seqTipoPrograma
            };
            var configuracoesTipoFormacaoInstituicao = SearchBySpecification(specTiposFormacoesObrigatorios,
                                                                             IncludesInstituicaoTipoEntidadeFormacaoEspecifica.TipoFormacaoEspecifica).ToList();
            var tiposObrigatoriosInstituicao = configuracoesTipoFormacaoInstituicao
                .Where(w => aluno ? w.ObrigatorioAssociacaoAluno : w.ObrigatorioAssociacaoIngressante)
                .Select(s => s.SeqTipoFormacaoEspecifica);

            // Recupera as entidades responsáveis pelo curso e as formações do curso
            var configCurso = CursoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Curso>(seqCurso), p => new
            {
                SeqsEntidadesResponsaveis = p.HierarquiasEntidades.Select(s => s.ItemSuperior.SeqEntidade).ToList(),
                SeqsFormacoesCurso = p.CursosFormacaoEspecifica.Select(s => s.SeqFormacaoEspecifica)
            });

            // Recupera as formações das entidades responsáveis pelo curso
            var formacoes = new List<FormacaoEspecificaNodeVO>();
            foreach (var seqEntidade in configCurso.SeqsEntidadesResponsaveis)
            {
                formacoes.AddRange(FormacaoEspecificaDomainService.BuscarFormacoesEspecificasHierarquia(seqEntidade));
            }

            // Filtra apenas formações do curso
            var formacoesCurso = new List<FormacaoEspecificaNodeVO>();
            foreach (var seqFormacao in configCurso.SeqsFormacoesCurso)
            {
                formacoesCurso.AddRange(FormacaoEspecificaDomainService.RecuperarFormacaoEspecificaComItensSuperiores(seqFormacao, formacoes));
            }
            // Remove os registros as areas superiores duplicadas retornadas pelas várias chamadas a RecuperarFormacaoEspecificaComItensSuperiores
            formacoesCurso = formacoesCurso.SMCDistinct(p => p.Seq).ToList();

            // Considera apenas os tipos obrigatórios que estejam associados nas entidades responsáveis do curso
            var tiposFormacoesObrigatoriasCurso = tiposObrigatoriosInstituicao.Intersect(TipoFormacaoEspecificaDomainService.RecuperarTiposFormacoesEspecificas(formacoesCurso));

            // Recupera os tipos informados com suas hierarquias
            var tiposInformados = seqsFormacoes.SelectMany(s =>
                FormacaoEspecificaDomainService.RecuperarFormacaoEspecificaComItensSuperiores(s, formacoes).Select(sf => new
                {
                    SeqFormacaoEspecifica = sf.Seq,
                    sf.SeqTipoFormacaoEspecifica
                }));

            // Verficia se algum tipo obrigatório não foi informado
            var tiposNaoInformados = tiposFormacoesObrigatoriasCurso.Where(a => !tiposInformados.Select(s => s.SeqTipoFormacaoEspecifica).Contains(a));

            // Retorna os tipos obrigatórios segundo o curso e que não foram informados
            var tiposFalhasReportadas = new List<long>();
            foreach (var tipoNaoInformado in configuracoesTipoFormacaoInstituicao.Where(w => tiposObrigatoriosInstituicao.Contains(w.SeqTipoFormacaoEspecifica)
                                                                                          && tiposNaoInformados.Contains(w.SeqTipoFormacaoEspecifica)))
            {
                yield return tipoNaoInformado;
                tiposFalhasReportadas.Add(tipoNaoInformado.SeqTipoFormacaoEspecifica);
            }

            if (!validarQuantidades)
            {
                yield break;
            }

            // Retorna os tipos de formação com a quantidade informada maior que a permitida.
            foreach (var tipoInformado in tiposInformados.GroupBy(g => g.SeqTipoFormacaoEspecifica))
            {
                if (tiposFalhasReportadas.Contains(tipoInformado.Key))
                    continue;
                var configuracao = configuracoesTipoFormacaoInstituicao.FirstOrDefault(f => f.SeqTipoFormacaoEspecifica == tipoInformado.Key);
                if (configuracao == null)
                    break;
                var quantidadeInformada = tipoInformado.Distinct().Count();
                if (aluno && configuracao.QuantidadePermitidaAssociacaoAluno < quantidadeInformada ||
                   !aluno && configuracao.QuantidadePermitidaAssociacaoIngressante < quantidadeInformada)
                {
                    yield return configuracao;
                }
            }
        }

        /// <summary>
        /// Popula uma árvore com um node raiz
        /// </summary>
        /// <param name="lista">Lista plana com os itens da hierarquia</param>
        /// <param name="item">Nó que deve ser populado</param>
        private void PopularArvore(List<InstituicaoTipoEntidadeFormacaoEspecificaVO> lista, ref InstituicaoTipoEntidadeFormacaoEspecificaVO item)
        {
            var seqPai = item.Seq;
            item.filhos = lista.Where(w => w.SeqPai == seqPai).ToList();
            item.filhos.SMCForEach(f => PopularArvore(lista, ref f));
        }

        /// <summary>
        /// Alimenta a lista com o item recebido e seus sub itens
        /// </summary>
        /// <param name="lista">Lista plana com os itens da hierarquia</param>
        /// <param name="item">Nó que deve ser usado para popular a lista</param>
        private void TransformarHierarquiaEmListaOrdenada(InstituicaoTipoEntidadeFormacaoEspecificaVO item, ref List<InstituicaoTipoEntidadeFormacaoEspecificaVO> lista)
        {
            lista.Add(item);
            if (item.SeqsFilhos == null)
                item.SeqsFilhos = new List<long>();
            if (item.filhos.SMCCount() > 0)
            {
                foreach (var itemFilho in item.filhos)
                {
                    item.SeqsFilhos.Add(itemFilho.Seq);
                    TransformarHierarquiaEmListaOrdenada(itemFilho, ref lista);
                }
            }
        }
    }
}