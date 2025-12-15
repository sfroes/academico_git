using SMC.Academico.Common.Areas.CSO.Exceptions;
using SMC.Academico.Common.Areas.CSO.Includes;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.Common.Enums;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CSO.Resources;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Academico.Domain.Areas.CSO.ValueObjects;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.Domain.Areas.ORG.Validators;
using SMC.Academico.Domain.Helpers;
using SMC.Framework;
using SMC.Framework.Domain.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CSO.DomainServices
{
    public class CursoUnidadeDomainService : AcademicoContextDomain<CursoUnidade>
    {
        #region [ Serviços ]

        private CursoDomainService CursoDomainService
        {
            get { return this.Create<CursoDomainService>(); }
        }

        private EntidadeDomainService EntidadeDomainService
        {
            get { return this.Create<EntidadeDomainService>(); }
        }

        private HierarquiaEntidadeItemDomainService HierarquiaEntidadeItemDomainService
        {
            get { return this.Create<HierarquiaEntidadeItemDomainService>(); }
        }

        private HierarquiaEntidadeDomainService HierarquiaEntidadeDomainService
        {
            get { return this.Create<HierarquiaEntidadeDomainService>(); }
        }

        private TipoEntidadeDomainService TipoEntidadeDomainService
        {
            get { return this.Create<TipoEntidadeDomainService>(); }
        }

        private InstituicaoTipoEntidadeDomainService InstituicaoTipoEntidadeDomainService => Create<InstituicaoTipoEntidadeDomainService>();

        #endregion [ Serviços ]

        /// <summary>
        /// Recupera um CursoUnidade com suas dependências e configurações
        /// </summary>
        /// <param name="seq">Sequencial do CursoUnidade a ser recuperado</param>
        /// <returns>Dados do CursoUnidade, dependências e configurações</returns>
        public CursoUnidadeVO BuscarCursoUnidade(long seq, bool desativarfiltrosHierarquia = false)
        {
            var includesCursoUnidade = IncludesCursoUnidade.ArquivoLogotipo
                                     | IncludesCursoUnidade.Classificacoes_Classificacao
                                     | IncludesCursoUnidade.Enderecos
                                     | IncludesCursoUnidade.EnderecosEletronicos
                                     | IncludesCursoUnidade.HierarquiasEntidades
                                     | IncludesCursoUnidade.Telefones;

            try
            {
                if (desativarfiltrosHierarquia)
                {
                    HierarquiaEntidadeItemDomainService.DesativarFiltrosHierarquiaItem(this);
                    HierarquiaEntidadeItemDomainService.AtivarFiltroHierarquiaItem(TipoVisao.VisaoLocalidades, this);
                }

                var cursoUnidadeDominio = this.SearchByKey(new SMCSeqSpecification<CursoUnidade>(seq), includesCursoUnidade);
                var cursoUnidadeVo = cursoUnidadeDominio.Transform<CursoUnidadeVO>();
                cursoUnidadeVo.SeqUnidade = cursoUnidadeDominio.HierarquiasEntidades.First().SeqItemSuperior.Value;
                cursoUnidadeVo.HierarquiasClassificacoes = this.EntidadeDomainService.GerarEntidadeClassificacoes(cursoUnidadeDominio.SeqTipoEntidade, cursoUnidadeDominio.Classificacoes);

                var tipoEntidadeSituacao = InstituicaoTipoEntidadeDomainService.BuscarTipoEntidadeDaInstituicao(cursoUnidadeVo.SeqTipoEntidade);

                if (tipoEntidadeSituacao?.TiposTelefone?.Count > 0)
                {
                    if (cursoUnidadeVo.TiposTelefone == null)
                        cursoUnidadeVo.TiposTelefone = new List<SMCDatasourceItem<string>>();

                    foreach (var tipoTelefone in tipoEntidadeSituacao.TiposTelefone)
                    {
                        var descricaoTelefone = (tipoTelefone.CategoriaTelefone == null || tipoTelefone.CategoriaTelefone == CategoriaTelefone.Nenhum)
                                              ? tipoTelefone.TipoTelefone.SMCGetDescription()
                                              : $"{tipoTelefone.TipoTelefone.SMCGetDescription()} - {tipoTelefone.CategoriaTelefone.SMCGetDescription()}";

                        SMCDatasourceItem<string> novoTipoTelefone = new SMCDatasourceItem<string>()
                        {
                            Descricao = descricaoTelefone,
                            Seq = tipoTelefone.TipoTelefone.SMCGetDescription()
                        };

                        cursoUnidadeVo.TiposTelefone.Add(novoTipoTelefone);
                    }
                }

                if (tipoEntidadeSituacao?.TiposEnderecoEletronico?.Count > 0)
                {
                    if (cursoUnidadeVo.TiposEnderecoEletronico == null)
                        cursoUnidadeVo.TiposEnderecoEletronico = new List<SMCDatasourceItem<string>>();

                    foreach (var tipoEE in tipoEntidadeSituacao.TiposEnderecoEletronico)
                    {
                        var descricao = (tipoEE.CategoriaEnderecoEletronico == null || tipoEE.CategoriaEnderecoEletronico == CategoriaEnderecoEletronico.Nenhum)
                                      ? tipoEE.TipoEnderecoEletronico.SMCGetDescription()
                                      : $"{tipoEE.TipoEnderecoEletronico.SMCGetDescription()} - {tipoEE.CategoriaEnderecoEletronico.SMCGetDescription()}";

                        SMCDatasourceItem<string> novoTipo = new SMCDatasourceItem<string>()
                        {
                            Descricao = descricao,
                            Seq = descricao
                        };

                        cursoUnidadeVo.TiposEnderecoEletronico.Add(novoTipo);
                    }
                }

                return cursoUnidadeVo;
            }
            finally
            {
                FilterHelper.AtivarFiltros(this);
            }
        }

        /// <summary>
        /// Recuperar os CursoUnidade com seus níveis de ensino, ofertas e turnos
        /// </summary>
        /// <param name="filtros">Filtros para os CursoUnidade</param>
        /// <returns>Lista páginada com os dados dos CursoUnidade com seus níveis de ensino, ofertas e turnos</returns>
        public SMCPagerData<CursoUnidadeListaVO> BuscarCursosUnidade(CursoUnidadeFilterSpecification filtros)
        {
            int total;

            filtros.SetOrderBy(x => x.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome);

            var agora = DateTime.Today;

            var cursosUnidade = this.SearchProjectionBySpecification(filtros, c => new CursoUnidadeListaVO()
            {
                Seq = c.Seq,
                SeqTipoEntidade = c.SeqTipoEntidade,
                Nome = c.Nome,
                NomeUnidade = c.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome,
                CursosOfertaLocalidade = c.CursosOfertaLocalidade.Select(col => new CursoOfertaLocalidadeListaVO()
                {
                    Seq = col.Seq,
                    SeqCursoUnidade = col.SeqCursoUnidade,
                    SeqTipoEntidade = col.SeqTipoEntidade,
                    DescricaoGrauAcademico = col.CursoUnidade.Curso.NivelEnsino.Descricao,
                    NomeCurso = col.CursoUnidade.Curso.Nome,
                    Nome = col.Nome,
                    NomeUnidade = col.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome,
                    SeqEntidadeLocalidade = col.HierarquiasEntidades.FirstOrDefault().ItemSuperior.SeqEntidade,
                    Ativo = col.HistoricoSituacoes.FirstOrDefault(h => agora >= h.DataInicio && (!h.DataFim.HasValue || agora <= h.DataFim.Value))
                               .SituacaoEntidade
                               .CategoriaAtividade != CategoriaAtividade.Inativa,
                    Turnos = col.Turnos.Select(t => new CursoOfertaLocalidadeTurnoVO()
                    {
                        Seq = t.Seq,
                        SeqCursoOfertaLocalidade = t.SeqCursoOfertaLocalidade,
                        SeqTurno = t.SeqTurno,
                        Descricao = t.Turno.Descricao,
                        Ativo = t.Ativo
                    }).ToList()
                }).ToList()
            }, out total).ToList();

            if (filtros.SeqLocalidade.HasValue)
                cursosUnidade.SMCForEach(s => s.CursosOfertaLocalidade = s.CursosOfertaLocalidade.Where(w => w.SeqEntidadeLocalidade == filtros.SeqLocalidade).ToList());

            return new SMCPagerData<CursoUnidadeListaVO>(cursosUnidade, total);
        }

        /// <summary>
        /// Busca as possíveis entidades superiores de Curso Unidade na visão Unidade
        /// </summary>
        /// <returns>SelectItem dos HierarquiaItem que representam as entidades encontradas</returns>
        public List<SMCDatasourceItem> BuscarUnidadesSelect()
        {
            var voTipoEntidade = this.TipoEntidadeDomainService.BuscarTipoEntidadeNaInstituicao(TOKEN_TIPO_ENTIDADE_EXTERNADA.CURSO_UNIDADE);
            return this.HierarquiaEntidadeDomainService.BuscarEntidadeSuperiorSelect(voTipoEntidade.Seq, TipoVisao.VisaoUnidade);
        }

        public List<SMCDatasourceItem> BuscarUnidadesSelect(bool removeEntidadePai = false)
        {
            var voTipoEntidade = this.TipoEntidadeDomainService.BuscarTipoEntidadeNaInstituicao(TOKEN_TIPO_ENTIDADE_EXTERNADA.CURSO_UNIDADE);
            return this.HierarquiaEntidadeDomainService.BuscarEntidadeSuperiorSelect(voTipoEntidade.Seq, TipoVisao.VisaoUnidade, RemoveEntidadePai: removeEntidadePai);
        }

        /// <summary>
        /// Recupera a mascara de curso unidade segundo a regra RN_CSO_026
        /// </summary>
        /// <param name="seqCurso">Sequencial do curso</param>
        /// <param name="seqUnidade">Sequencial do item de hierarquia da unidade responsável</param>
        /// <returns>Mascara segundo a regra RN_CSO_026</returns>
        public string RecuperarMascaraCursoUnidade(long seqCurso, long seqUnidade)
        {
            var specCurso = new SMCSeqSpecification<Curso>(seqCurso);
            var nomeCurso = this.CursoDomainService.SearchProjectionByKey(specCurso, p => p.Nome);

            var specUnidade = new SMCSeqSpecification<HierarquiaEntidadeItem>(seqUnidade);
            var nomeUnidade = this.HierarquiaEntidadeItemDomainService.SearchProjectionByKey(specUnidade, p => p.Entidade.Nome);

            return string.Format(MessagesResource.MascaraCursoCompletaSemGrau, nomeCurso, nomeUnidade);
        }

        /// <summary>
        /// Grava um CursoUnidade e suas dependências
        /// </summary>
        /// <param name="cursoUnidadeVo">Dados do CursoUnidade</param>
        /// <returns>Sequencial do CursoUnidade gravado</returns>
        public long SalvarCursoUnidade(CursoUnidadeVO cursoUnidadeVo)
        {
            var cursoUnidade = cursoUnidadeVo.Transform<CursoUnidade>();

            // Recupera ou cria o HierarquiaEntidadeItem que relaciona este curso à unidade
            var unidadeDominio = this.HierarquiaEntidadeItemDomainService
                .SearchByKey(new HierarquiaEntidadeItemFilterSpecification() { SeqEntidade = cursoUnidadeVo.Seq }) ??
                             new HierarquiaEntidadeItem() { SeqItemSuperior = cursoUnidadeVo.SeqUnidade };

            // Atualiza o HierarquiaEntidadeItem e vincula este ao curso
            unidadeDominio.SeqItemSuperior = cursoUnidadeVo.SeqUnidade;
            cursoUnidade.HierarquiasEntidades = new List<HierarquiaEntidadeItem>(1);
            cursoUnidade.HierarquiasEntidades.Add(unidadeDominio);

            // Converte as Hierarquias de Classificações em Classificações
            cursoUnidade.Classificacoes = EntidadeDomainService.GerarEntidadeClassificacoes(cursoUnidadeVo);

            //Validações
            this.Validar(cursoUnidade, new EntidadeValidator());
            this.EntidadeDomainService.ValidarDadosContatoObrigatorios(cursoUnidade);
            ValidarMesmoCursoMesmaUnidade(cursoUnidadeVo);
            ValidarMesmoNomeCursoUnidade(cursoUnidadeVo);

            var entidadeDomainService = this.EntidadeDomainService;

            if (cursoUnidade.Seq == 0)
            {
                entidadeDomainService.IncluirSituacao(cursoUnidade);
            }

            entidadeDomainService.AtualizarHierarquiaEntidadeExternada(cursoUnidade, TOKEN_TIPO_ENTIDADE_EXTERNADA.CURSO_UNIDADE, true);

            this.EnsureFileIntegrity(cursoUnidade, m => m.SeqArquivoLogotipo, m => m.ArquivoLogotipo);

            this.SaveEntity(cursoUnidade);

            return cursoUnidade.Seq;
        }

        private void ValidarMesmoCursoMesmaUnidade(CursoUnidadeVO cursoUnidadeVO)
        {
            var result = this.Count(new CursoUnidadeExistenteFilterSpecification() { SeqDiferente = cursoUnidadeVO.Seq, SeqCurso = cursoUnidadeVO.SeqCurso, SeqUnidade = cursoUnidadeVO.SeqUnidade });
            if (result > 0)
                throw new CursoUnidadeMesmoCursoMesmaUnidadeException();
        }

        private void ValidarMesmoNomeCursoUnidade(CursoUnidadeVO cursoUnidadeVO)
        {
            var result = this.Count(new CursoUnidadeExistenteFilterSpecification() { SeqDiferente = cursoUnidadeVO.Seq, SeqInstituicaoEnsino = cursoUnidadeVO.SeqInstituicaoEnsino, Nome = cursoUnidadeVO.Nome });
            if (result > 0)
                throw new CursoUnidadeMesmoNomeException();
        }

        private void Validar(CursoUnidade programa, params SMCValidator[] validatores)
        {
            var results = new List<SMCValidationResults>();
            foreach (var validador in validatores)
            {
                results.Add(validador.Validate(programa));
            }
            if (results.Count(c => !c.IsValid) > 0)
            {
                List<SMCValidationResults> errorList = results.Where(w => !w.IsValid).ToList();
                throw new SMCInvalidEntityException(errorList);
            }
        }
    }
}