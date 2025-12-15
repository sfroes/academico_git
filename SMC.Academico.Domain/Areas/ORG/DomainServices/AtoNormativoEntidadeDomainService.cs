using SMC.Academico.Common.Areas.CNC.Exceptions.SolicitacaoDocumentoConclusao;
using SMC.Academico.Common.Areas.CSO.Includes;
using SMC.Academico.Common.Areas.ORG.Constants;
using SMC.Academico.Common.Areas.ORG.Exceptions;
using SMC.Academico.Common.Areas.ORG.Includes;
using SMC.Academico.Common.Areas.Shared.Helpers;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.Domain.Areas.ORG.Specifications.AtoNormativoEntidade;
using SMC.Academico.Domain.Areas.ORG.ValueObjects;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.Framework.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ORG.DomainServices
{
    public class AtoNormativoEntidadeDomainService : AcademicoContextDomain<AtoNormativoEntidade>
    {
        #region [ DomainService ]

        private CursoOfertaLocalidadeDomainService CursoOfertaLocalidadeDomainService => Create<CursoOfertaLocalidadeDomainService>();

        private InstituicaoNivelDomainService InstituicaoNivelDomainService => Create<InstituicaoNivelDomainService>();

        private CursoOfertaDomainService CursoOfertaDomainService => Create<CursoOfertaDomainService>();

        #endregion [ DomainService ]

        public SMCPagerData<AssociacaoEntidadeListarVO> BuscarAtoNormativoEntidades(AssociacaoEntidadeFiltroVO filtros)
        {
            if (filtros.PageSettings != null)
                filtros.PageSettings.PageSize = 50;

            var specAtoNormativo = filtros.Transform<AtoNormativoEntidadeFilterSpecification>();

            if (filtros.TipoOrgaoRegulador.HasValue || filtros.CodigoOrgaoRegulador.HasValue || filtros.CodigoCursoOferta.HasValue)
            {
                var specCursoOfertaLocalidade = new CursoOfertaLocalidadeFilterSpecification() { AssociadaAtoNormativo = true };

                if (filtros.TipoOrgaoRegulador.HasValue)
                {
                    var specInstituicaoNivel = new InstituicaoNivelFilterSpecification { TipoOrgaoRegulador = filtros.TipoOrgaoRegulador };
                    var seqsNiveisEnsino = InstituicaoNivelDomainService.SearchBySpecification(specInstituicaoNivel).Select(s => s.SeqNivelEnsino).ToList();
                    specCursoOfertaLocalidade.SeqsNiveisEnsino = seqsNiveisEnsino;
                }

                if (filtros.CodigoCursoOferta.HasValue)
                {
                    specCursoOfertaLocalidade.Codigo = filtros.CodigoCursoOferta;
                }

                if (filtros.CodigoOrgaoRegulador.HasValue)
                {
                    specCursoOfertaLocalidade.CodigoOrgaoRegulador = filtros.CodigoOrgaoRegulador;
                }

                var seqsCursoOfertaLocalidade = this.CursoOfertaLocalidadeDomainService.SearchProjectionBySpecification(specCursoOfertaLocalidade, x => x.Seq).ToList();

                if (!specAtoNormativo.SeqEntidade.HasValue)
                {
                    specAtoNormativo.SeqsEntidades = seqsCursoOfertaLocalidade;
                }
                else
                {
                    if (!seqsCursoOfertaLocalidade.Contains(specAtoNormativo.SeqEntidade.Value))
                    {
                        var listaVazia = new List<AssociacaoEntidadeListarVO>();
                        return new SMCPagerData<AssociacaoEntidadeListarVO>(listaVazia, 0);
                    }
                }

                if (!seqsCursoOfertaLocalidade.Any())
                {
                    var listaVazia = new List<AssociacaoEntidadeListarVO>();
                    return new SMCPagerData<AssociacaoEntidadeListarVO>(listaVazia, 0);
                }
            }

            var lista = SearchBySpecification(specAtoNormativo, out int total, IncludesAtoNormativoEntidade.Entidade | IncludesAtoNormativoEntidade.GrauAcademico | IncludesAtoNormativoEntidade.Entidade_TipoEntidade).ToList();

            var retorno = new List<AssociacaoEntidadeListarVO>();
            foreach (var item in lista)
            {
                var seqCursoOferta = item.Entidade.TipoEntidade.Token == TOKEN_TIPO_ENTIDADE.CURSO_OFERTA_LOCALIDADE ? ((CursoOfertaLocalidade)item.Entidade).SeqCursoOferta : (long?)null;
                var codigoCursoOferta = item.Entidade.TipoEntidade.Token == TOKEN_TIPO_ENTIDADE.CURSO_OFERTA_LOCALIDADE ? ((CursoOfertaLocalidade)item.Entidade).Codigo : (int?)null;
                var codigoOrgaoRegulador = item.Entidade.TipoEntidade.Token == TOKEN_TIPO_ENTIDADE.CURSO_OFERTA_LOCALIDADE ? ((CursoOfertaLocalidade)item.Entidade).CodigoOrgaoRegulador : null;

                var descricaoOrgaoRegulador = string.Empty;
                if (seqCursoOferta.HasValue && seqCursoOferta != 0)
                {
                    var dados = CursoOfertaDomainService.SearchByKey(new SMCSeqSpecification<CursoOferta>(seqCursoOferta.Value), IncludesCursoOferta.Curso);

                    var instituicaoNivelEnsino = InstituicaoNivelDomainService.BuscarInstituicaoNivelEnsino(dados.Curso.SeqNivelEnsino, dados.Curso.SeqInstituicaoEnsino.Value);
                    descricaoOrgaoRegulador = SMCEnumHelper.GetDescription(instituicaoNivelEnsino.TipoOrgaoRegulador);
                }

                var atoNormativoEntidade = new AssociacaoEntidadeListarVO
                {
                    Seq = item.Seq,
                    SeqEntidade = item.SeqEntidade,
                    NomeEntidade = item.Entidade.Nome,
                    DescricaoGrauAcademico = item.SeqGrauAcademico.HasValue ? item.GrauAcademico.Descricao : string.Empty,
                    DescricaoOrgaoRegulador = codigoOrgaoRegulador.HasValue ? string.Format("{0}: {1}", descricaoOrgaoRegulador, codigoOrgaoRegulador) : descricaoOrgaoRegulador,
                    SeqCursoOferta = codigoCursoOferta
                };
                retorno.Add(atoNormativoEntidade);
            }

            return new SMCPagerData<AssociacaoEntidadeListarVO>(retorno, total);
        }

        public AssociacaoEntidadeVO BuscarAtoNormativoEntidade(long seq)
        {
            var atoNormativoEntidade = this.SearchByKey(new SMCSeqSpecification<AtoNormativoEntidade>(seq), IncludesAtoNormativoEntidade.Entidade);

            var retorno = atoNormativoEntidade.Transform<AssociacaoEntidadeVO>();

            retorno.Nome = atoNormativoEntidade.Entidade.Nome;
            retorno.HabilitaCampo = CursoOfertaLocalidadeDomainService.CursoOfertaLocalidadeExigeGrau(atoNormativoEntidade.SeqEntidade);

            return retorno;
        }

        public long SalvarAtoNormativoEntidade(AssociacaoEntidadeVO modelo)
        {
            var retorno = modelo.Transform<AtoNormativoEntidade>();

            this.SaveEntity(retorno);

            return retorno.Seq;
        }

        public void ExcluirAtoNormativoEntidade(long seq)
        {
            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                try
                {
                    var retorno = this.SearchByKey(new SMCSeqSpecification<AtoNormativoEntidade>(seq));
                    this.DeleteEntity(retorno);

                    unitOfWork.Commit();
                }
                catch (Exception)
                {
                    unitOfWork.Rollback();
                    throw;
                }
            }
        }

        public long? BuscarSeqGrauAcademicoAtoNormativoEntidade(long seq, long? seqEntidade, long? seqAtoNormativo)
        {
            var spec = new AtoNormativoEntidadeFilterSpecification
            {
                Seq = seq,
                SeqEntidade = seqEntidade,
                SeqAtoNormativo = seqAtoNormativo
            };

            var retorno = SearchByKey(spec);

            if (retorno == null)
                return null;

            return retorno.SeqGrauAcademico;
        }

        /// <summary>
        /// Busca dados instituição - Ato Normativo (Credenciamento/ Recredenciamento/ Renovação recredenciamento) - RN_CNC_053
        /// </summary>
        public DadosAtoNormativoEntidadeVO BuscarAtoNormativoEntidadeInstituicao(long seqInstituicaoEnsino, DateTime? dataConclusao, bool considerarApenasAtosVigente = false)
        {
            var atosNormativosEntidades = this.SearchBySpecification(new AtoNormativoEntidadeFilterSpecification
            {
                SeqEntidade = seqInstituicaoEnsino,
                ConsiderarApenasAtosVigente = considerarApenasAtosVigente
            }, x => x.AtoNormativo.AssuntoNormativo, y => y.AtoNormativo.TipoAtoNormativo);

            if (dataConclusao.HasValue)
                atosNormativosEntidades = atosNormativosEntidades.Where(a => a.AtoNormativo.DataDocumento <= dataConclusao);

            var atosNormativos = atosNormativosEntidades.Select(a => a.AtoNormativo).ToList();
            if (!atosNormativos.Any())
                throw new SolicitacaoDocumentoConclusaoInstituicaoSemCredenciamentoException();

            var retorno = new DadosAtoNormativoEntidadeVO()
            {
                Credenciamento = new DadosAtoNormativoVO(),
                Recredenciamento = new DadosAtoNormativoVO(),
                RenovacaoDeRecredenciamento = new DadosAtoNormativoVO()
            };

            PreencherDadosAtoNormativo(atosNormativos, TOKEN_ASSUNTO_NORMATIVO.CREDENCIAMENTO, retorno.Credenciamento, true);
            PreencherDadosAtoNormativo(atosNormativos, TOKEN_ASSUNTO_NORMATIVO.RECREDENCIAMENTO, retorno.Recredenciamento, true);
            PreencherDadosAtoNormativo(atosNormativos, TOKEN_ASSUNTO_NORMATIVO.RENOVACAO_RECREDENCIAMENTO, retorno.RenovacaoDeRecredenciamento, true);

            return retorno;
        }

        public void PreencherDadosAtoNormativo(List<AtoNormativo> listaAtosNormativos, string token, DadosAtoNormativoVO retorno, bool exibirErro = false)
        {
            var atosNormativos = listaAtosNormativos.Where(a => a.AssuntoNormativo.Token == token).ToList();
            if (atosNormativos.Any())
            {
                var maiorDataDocumento = atosNormativos.Max(a => a.DataDocumento);
                var atoNormativo = atosNormativos.FirstOrDefault(a => a.DataDocumento == maiorDataDocumento);

                var auxNumero = FormatarString.ObterNumerosComVirgula(atoNormativo.NumeroDocumento);
                var numerico = int.TryParse(auxNumero, out int numeroDocumento);

                retorno.Descricao = atoNormativo.Descricao;

                if ((!atoNormativo.NumeroSecaoPublicacao.HasValue || (atoNormativo.NumeroSecaoPublicacao.HasValue && atoNormativo.NumeroSecaoPublicacao > 0)) &&
                    (!atoNormativo.NumeroPaginaPublicacao.HasValue || (atoNormativo.NumeroPaginaPublicacao.HasValue && atoNormativo.NumeroPaginaPublicacao > 0)) &&
                    (!atoNormativo.NumeroPublicacao.HasValue || (atoNormativo.NumeroPublicacao.HasValue && atoNormativo.NumeroPublicacao > 0)) && numeroDocumento > 0)
                {
                    retorno.Numero = auxNumero;
                    retorno.Data = atoNormativo.DataDocumento.Date;

                    if (!string.IsNullOrEmpty(atoNormativo.TipoAtoNormativo.DescricaoXSD))
                        retorno.Tipo = atoNormativo.TipoAtoNormativo.DescricaoXSD;
                    else if (exibirErro)
                        throw new AtoNormativoEntidadeInformacaoNaoEncontradaException($"descrição XSD do tipo de ato normativo '{atoNormativo.TipoAtoNormativo.Descricao}'");

                    if (atoNormativo.VeiculoPublicacao.HasValue)
                    {
                        var auxVeiculoPublicacao = atoNormativo.VeiculoPublicacao.SMCGetDescription();
                        retorno.VeiculoPublicacao = FormatarString.Truncate(auxVeiculoPublicacao, 255);
                    }

                    retorno.DataPublicacao = atoNormativo.DataPublicacao;
                    retorno.SecaoPublicacao = atoNormativo.NumeroSecaoPublicacao;
                    retorno.PaginaPublicacao = atoNormativo.NumeroPaginaPublicacao;
                    retorno.NumeroDOU = atoNormativo.NumeroPublicacao;
                }
            }
            else if (token == TOKEN_ASSUNTO_NORMATIVO.CREDENCIAMENTO && exibirErro)
                throw new AtoNormativoEntidadeInformacaoNaoEncontradaException("Credenciamento");
            else if (token == TOKEN_ASSUNTO_NORMATIVO.AUTORIZACAO && exibirErro)
                throw new AtoNormativoEntidadeInformacaoNaoEncontradaException("Autorização");
            else if (token == TOKEN_ASSUNTO_NORMATIVO.RECONHECIMENTO && exibirErro)
                throw new AtoNormativoEntidadeInformacaoNaoEncontradaException("Reconhecimento");
        }

        public DadosAtoNormativoVO BuscarUltimoAtoNormativoVigente(long seqEntidade)
        {
            var spec = new AtoNormativoEntidadeFilterSpecification { SeqEntidade = seqEntidade, ConsiderarApenasAtosVigente = true };
            spec.SetOrderByDescending(o => o.AtoNormativo.DataDocumento);

            var atosNormativosEntidades = this.SearchBySpecification(spec, y => y.AtoNormativo.TipoAtoNormativo);
            var atosNormativos = atosNormativosEntidades.Where(a => a.AtoNormativo.DataDocumento <= DateTime.Now && !a.AtoNormativo.DataPrazoValidade.HasValue).ToList();

            if (!atosNormativos.Any() || atosNormativos == null)
                return null;

            var atoNormativo = new DadosAtoNormativoVO()
            {
                Descricao = atosNormativos.FirstOrDefault().AtoNormativo.Descricao,
                Numero = atosNormativos.FirstOrDefault().AtoNormativo.NumeroDocumento,
                Data = atosNormativos.FirstOrDefault().AtoNormativo.DataDocumento.Date,
                Tipo = atosNormativos.FirstOrDefault().AtoNormativo.TipoAtoNormativo.DescricaoXSD,
                VeiculoPublicacao = atosNormativos.FirstOrDefault().AtoNormativo.VeiculoPublicacao.SMCGetDescription(),
                DataPublicacao = atosNormativos.FirstOrDefault().AtoNormativo.DataPublicacao,
                SecaoPublicacao = atosNormativos.FirstOrDefault().AtoNormativo.NumeroSecaoPublicacao,
                PaginaPublicacao = atosNormativos.FirstOrDefault().AtoNormativo.NumeroPaginaPublicacao,
                NumeroDOU = atosNormativos.FirstOrDefault().AtoNormativo.NumeroPublicacao
            };

            return atoNormativo;
        }
    }
}