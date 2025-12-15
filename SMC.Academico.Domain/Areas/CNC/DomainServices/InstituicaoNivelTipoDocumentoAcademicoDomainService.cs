using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Common.Areas.CNC.Exceptions;
using SMC.Academico.Common.Areas.CNC.Exceptions.InstituicaoNivelTipoDocumentoConclusao;
using SMC.Academico.Common.Areas.CNC.Exceptions.SolicitacaoDocumentoConclusao;
using SMC.Academico.Common.Areas.CNC.Includes;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Enums;
using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Academico.Domain.Areas.CNC.Specifications;
using SMC.Academico.Domain.Areas.CNC.ValueObjects;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.AssinaturaDigital.ServiceContract.Areas.DOC.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Security.Util;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CNC.DomainServices
{
    public class InstituicaoNivelTipoDocumentoAcademicoDomainService : AcademicoContextDomain<InstituicaoNivelTipoDocumentoAcademico>
    {
        #region DomainServices

        private InstituicaoNivelTipoFormacaoEspecificaDomainService InstituicaoNivelTipoFormacaoEspecificaDomainService => this.Create<InstituicaoNivelTipoFormacaoEspecificaDomainService>();
        private InstituicaoNivelTipoDocumentoFormacaoEspecificaDomainService InstituicaoNivelTipoDocumentoFormacaoEspecificaDomainService => this.Create<InstituicaoNivelTipoDocumentoFormacaoEspecificaDomainService>();
        private InstituicaoNivelSistemaOrigemDomainService InstituicaoNivelSistemaOrigemDomainService => this.Create<InstituicaoNivelSistemaOrigemDomainService>();
        private TipoFuncionarioDomainService TipoFuncionarioDomainService => this.Create<TipoFuncionarioDomainService>();

        private IConfiguracaoDocumentoService ConfiguracaoDocumentoService { get => Create<IConfiguracaoDocumentoService>(); }

        #endregion

        public InstituicaoNivelTipoDocumentoAcademicoVO BuscarInstituicaoNivelTipoDocumentoAcademico(long seq)
        {
            var instituicaoNivelTipoDocumentoAcademico = this.SearchByKey(new SMCSeqSpecification<InstituicaoNivelTipoDocumentoAcademico>(seq),
                                                                          IncludesInstituicaoNivelTipoDocumentoAcademico.FormacoesEspecificas_TipoFormacaoEspecifica |
                                                                          IncludesInstituicaoNivelTipoDocumentoAcademico.TipoDocumentoAcademico |
                                                                          IncludesInstituicaoNivelTipoDocumentoAcademico.ModelosRelatorio |
                                                                          IncludesInstituicaoNivelTipoDocumentoAcademico.ModelosRelatorio_ArquivoModelo);

            var retorno = instituicaoNivelTipoDocumentoAcademico.Transform<InstituicaoNivelTipoDocumentoAcademicoVO>();
            retorno.TiposFormacaoEspecifica = this.InstituicaoNivelTipoFormacaoEspecificaDomainService.BuscarTiposFormacaoEspecificaPorInstituicaoNivelSelect(retorno.SeqInstituicaoNivel);
            retorno.ConfiguracaoDiplomaGAD = InstituicaoNivelSistemaOrigemDomainService.BuscarConfiguracaoDiplomaPorNivelEnsinoGADSelect(retorno.SeqInstituicaoNivel, retorno.UsoSistemaOrigem);

            retorno.ExibeTiposFormacao = instituicaoNivelTipoDocumentoAcademico.TipoDocumentoAcademico.GrupoDocumentoAcademico != GrupoDocumentoAcademico.DeclaracoesGenericasAluno &&
                                         instituicaoNivelTipoDocumentoAcademico.TipoDocumentoAcademico.GrupoDocumentoAcademico != GrupoDocumentoAcademico.DeclaracoesGenericasProfessor &&
                                         instituicaoNivelTipoDocumentoAcademico.TipoDocumentoAcademico.GrupoDocumentoAcademico != GrupoDocumentoAcademico.CurriculoEscolar;


            if (instituicaoNivelTipoDocumentoAcademico.ModelosRelatorio.Any())
            {
                retorno.ModelosRelatorio.SMCForEach(f => f.ArquivoModelo.GuidFile = instituicaoNivelTipoDocumentoAcademico.ModelosRelatorio.Where(w => w.SeqArquivoModelo == f.SeqArquivoModelo).Select(s => s.ArquivoModelo.UidArquivo).FirstOrDefault().ToString());
            }

            foreach (var formacaoEspecifica in retorno.FormacoesEspecificas)
            {
                var auxiliarFormacao = this.InstituicaoNivelTipoDocumentoFormacaoEspecificaDomainService.SearchByKey(new SMCSeqSpecification<InstituicaoNivelTipoDocumentoFormacaoEspecifica>(formacaoEspecifica.Seq), x => x.TipoFormacaoEspecifica);
                formacaoEspecifica.DescricaoFormacaoEspecifica = auxiliarFormacao.TipoFormacaoEspecifica.Descricao;
            }

            return retorno;
        }

        public long SalvarInstituicaoNivelTipoDocumentoAcademico(InstituicaoNivelTipoDocumentoAcademicoVO modelo)
        {
            var instituicaoNivelTipoDocumentoAcademico = modelo.Transform<InstituicaoNivelTipoDocumentoAcademico>();

            ValidarModelo(modelo);

            if (instituicaoNivelTipoDocumentoAcademico.ModelosRelatorio != null)
                foreach (var modeloRelatorio in instituicaoNivelTipoDocumentoAcademico.ModelosRelatorio)
                    this.EnsureFileIntegrity(modeloRelatorio, x => x.SeqArquivoModelo, x => x.ArquivoModelo);

            this.SaveEntity(instituicaoNivelTipoDocumentoAcademico);

            return instituicaoNivelTipoDocumentoAcademico.Seq;
        }

        private void ValidarModelo(InstituicaoNivelTipoDocumentoAcademicoVO modelo)
        {
            // Não pode haver mais de um template por idioma
            var possuiMais1Idioma = modelo.ModelosRelatorio?.GroupBy(x => x.Idioma).Any(g => g.Count() > 1);

            // Não pode haver mais de uma parametrização para o mesmo tipo documento acadêmico
            var possuiFormacaoAMais = modelo.FormacoesEspecificas?.GroupBy(x => x.DescricaoFormacaoEspecifica).Any(g => g.Count() > 1);

            // Tem que haver ao menos um template portugues informado e tem que haver o arquivo de template associado
            var possuiPortuguesInformado = modelo.ModelosRelatorio?.Any(x => x.Idioma == Linguagem.Portuguese && !string.IsNullOrEmpty(x.ArquivoModelo.Name));

            if (possuiMais1Idioma == true)
                throw new InstituicaoNivelTipoDocumentoTemplateException();

            if (possuiFormacaoAMais == true)
                throw new InstituicaoNivelTipoDocumentoFormacaoException();

            if (possuiPortuguesInformado == false)
                throw new InstituicaoNivelTipoDocumentoArquivoSemTemplatePadraoException();

            if (modelo.UsoSistemaOrigem == UsoSistemaOrigem.ArquivoPDF)
            {
                if (modelo.SeqConfiguracaoGad.HasValue)
                {
                    var tipoVinculoFuncionario = this.TipoFuncionarioDomainService.SearchProjectionAll(x => x.Token).ToList();

                    var retornoTagsVinculoGAD = this.ConfiguracaoDocumentoService.BuscarTagsConfiguracaoDocumento(modelo.SeqConfiguracaoGad);
                    var listaTagsVinculoGAD = retornoTagsVinculoGAD.Where(w => !w.TemParticipante).Select(s => s.TagPosicaoAssinatura).ToList();

                    if (retornoTagsVinculoGAD == null || !retornoTagsVinculoGAD.Any())
                        throw new InstituicaoNivelTipoDocumentoSemTagTipoVinculoException();

                    var contemVinculoAtivo = tipoVinculoFuncionario.Any(tipo => listaTagsVinculoGAD.Any(x => x == tipo));

                    if (!contemVinculoAtivo && listaTagsVinculoGAD.Any())
                    {
                        string lista = string.Join(", ", listaTagsVinculoGAD);
                        throw new InstituicaoNivelTipoDocumentoNenhumTipoVinculoException(lista);
                    }
                }
            }
        }

        public List<SMCDatasourceItem> BuscarNiveisEnsinoPorGrupoDocumentoAcademicoSelect(GrupoDocumentoAcademico grupoDocumento)
        {
            var spec = new InstituicaoNivelTipoDocumentoAcademicoFilterSpecification() { GrupoDocumentoAcademico = grupoDocumento };
            var lista = this.SearchProjectionBySpecification(spec, x => new
            {
                x.InstituicaoNivel.NivelEnsino.Seq,
                x.InstituicaoNivel.NivelEnsino.Descricao,
                x.TokenPermissaoEmissaoDocumento
            }).Distinct().OrderBy(o => o.Descricao).ToList();

            var retorno = new List<SMCDatasourceItem>();
            foreach (var nivelEnsino in lista)
            {
                if (!retorno.Any(a => a.Seq == nivelEnsino.Seq) &&
                   ((!string.IsNullOrEmpty(nivelEnsino.TokenPermissaoEmissaoDocumento) &&
                     SMCSecurityHelper.Authorize(nivelEnsino.TokenPermissaoEmissaoDocumento.Trim())) ||
                     SMCSecurityHelper.Authorize(UC_SRC_002_01_01.MANTER_PROCESSO_RESTRITO_ADMINISTRADOR)))
                {
                    var item = new SMCDatasourceItem() { Seq = nivelEnsino.Seq, Descricao = nivelEnsino.Descricao };
                    retorno.Add(item);
                }
            }

            return retorno;
        }

        public List<SMCDatasourceItem> BuscarTiposDocumentoAcademicoPorNivelEnsinoSelect(long seqNivelEnsinoPorGrupoDocumentoAcademico, GrupoDocumentoAcademico grupoDocumento)
        {
            var spec = new InstituicaoNivelTipoDocumentoAcademicoFilterSpecification()
            {
                SeqNivelEnsino = seqNivelEnsinoPorGrupoDocumentoAcademico,
                GrupoDocumentoAcademico = grupoDocumento
            };

            var lista = this.SearchProjectionBySpecification(spec, x => new
            {
                x.TipoDocumentoAcademico.Seq,
                x.TipoDocumentoAcademico.Descricao,
                x.TokenPermissaoEmissaoDocumento
            }).OrderBy(o => o.Descricao).ToList();

            var retorno = new List<SMCDatasourceItem>();
            foreach (var tipoDocumentoAcademico in lista)
            {
                if ((!string.IsNullOrEmpty(tipoDocumentoAcademico.TokenPermissaoEmissaoDocumento) &&
                     SMCSecurityHelper.Authorize(tipoDocumentoAcademico.TokenPermissaoEmissaoDocumento.Trim())) ||
                     SMCSecurityHelper.Authorize(UC_SRC_002_01_01.MANTER_PROCESSO_RESTRITO_ADMINISTRADOR))
                {
                    var item = new SMCDatasourceItem() { Seq = tipoDocumentoAcademico.Seq, Descricao = tipoDocumentoAcademico.Descricao };
                    retorno.Add(item);
                }
            }

            return retorno;
        }

        public InstituicaoNivelTipoDocumentoAcademicoConfigVO BuscarConfiguracoesInstituicaoNivelTipoDocumentoAcademico(long? seqNivelEnsinoPorGrupoDocumentoAcademico, long? seqTipoDocumentoAcademico, SMCLanguage? idiomaDocumentoAcademico)
        {
            var spec = new InstituicaoNivelTipoDocumentoAcademicoFilterSpecification()
            {
                SeqNivelEnsino = seqNivelEnsinoPorGrupoDocumentoAcademico,
                SeqTipoDocumentoAcademico = seqTipoDocumentoAcademico,
            };
            var retorno = this.SearchProjectionByKey(spec, x => new InstituicaoNivelTipoDocumentoAcademicoConfigVO()
            {
                Seq = x.Seq,
                SeqInstituicaoNivel = x.SeqInstituicaoNivel,
                UsoSistemaOrigem = x.UsoSistemaOrigem,
                DescricaoTipoDocumentoAcademico = x.TipoDocumentoAcademico.Descricao,
                Tags = x.TipoDocumentoAcademico.Tags.Select(t => new TipoDocumentoAcademicoTagVO()
                {
                    Seq = t.Seq,
                    SeqTag = t.SeqTag,
                    SeqTipoDocumentoAcademico = t.SeqTipoDocumentoAcademico,
                    DescricaoTag = t.Tag.Descricao,
                    PermiteEditarDado = t.PermiteEditarDado,
                    TipoPreenchimentoTag = t.Tag.TipoPreenchimentoTag,
                    QueryOrigem = t.Tag.QueryOrigem,
                    InformacaoTag = t.InformacaoTag
                }).OrderBy(o => o.DescricaoTag).ToList(),
                SeqConfiguracaoGad = x.SeqConfiguracaoGad,
                DescricaoNivelEnsino = x.InstituicaoNivel.NivelEnsino.Descricao,
                TokenNivelEnsino = x.InstituicaoNivel.NivelEnsino.Token,
                ModelosRelatorio = x.ModelosRelatorio.Where(w => w.Idioma == idiomaDocumentoAcademico).Select(s => new InstituicaoNivelTipoDocumentoModelosRelatorioVO()
                {
                    ArquivoModelo = new SMCUploadFile
                    {
                        Type = s.ArquivoModelo.Tipo,
                        Name = s.ArquivoModelo.Nome,
                        FileData = s.ArquivoModelo.Conteudo,
                        Size = s.ArquivoModelo.Tamanho
                    }
                }).FirstOrDefault()
            });

            retorno.TokenSistemaOrigemGAD = InstituicaoNivelSistemaOrigemDomainService.BuscarTokenSistemaOrigemGAD(retorno.SeqInstituicaoNivel, retorno.UsoSistemaOrigem);
            return retorno;
        }

        public bool ValidarTipoDocumentoAcademicoArquivoXml(long seqTipoDocumentoAcademico)
        {
            var spec = new InstituicaoNivelTipoDocumentoAcademicoFilterSpecification()
            {
                SeqTipoDocumentoAcademico = seqTipoDocumentoAcademico,
                UsoSistemaOrigem = UsoSistemaOrigem.ArquivoXML
            };
            var retorno = this.SearchBySpecification(spec);

            return retorno.Any();
        }

        public void ValidarPermissaoConfigurarRelatorio(long seqNivelEnsinoPorGrupoDocumentoAcademico, long seqTipoDocumentoAcademico)
        {
            try
            {
                var spec = new InstituicaoNivelTipoDocumentoAcademicoFilterSpecification()
                {
                    SeqNivelEnsino = seqNivelEnsinoPorGrupoDocumentoAcademico,
                    SeqTipoDocumentoAcademico = seqTipoDocumentoAcademico
                };

                var retorno = this.SearchProjectionByKey(spec, x => new
                {
                    SeqNivelEnsino = x.InstituicaoNivel.NivelEnsino.Seq,
                    SeqTipoDocumentoAcademico = x.TipoDocumentoAcademico.Seq,
                    x.TokenPermissaoEmissaoDocumento
                });

                var temAcesso = (retorno != null && !string.IsNullOrEmpty(retorno.TokenPermissaoEmissaoDocumento) &&
                                 SMCSecurityHelper.Authorize(retorno.TokenPermissaoEmissaoDocumento.Trim())) ||
                                 SMCSecurityHelper.Authorize(UC_SRC_002_01_01.MANTER_PROCESSO_RESTRITO_ADMINISTRADOR);

                if (!temAcesso)
                {
                    var specNivelEnsino = new InstituicaoNivelTipoDocumentoAcademicoFilterSpecification() { SeqNivelEnsino = seqNivelEnsinoPorGrupoDocumentoAcademico };
                    var nivelEnsino = this.SearchProjectionByKey(specNivelEnsino, x => new
                    { x.InstituicaoNivel.NivelEnsino.Descricao });
                    var descricaoNivelEnsino = nivelEnsino != null ? nivelEnsino.Descricao : "Descrição do nível de ensino não encontrada";

                    var specTipoDocumentoAcademico = new InstituicaoNivelTipoDocumentoAcademicoFilterSpecification() { SeqTipoDocumentoAcademico = seqTipoDocumentoAcademico };
                    var tipoDocumentoAcademico = this.SearchProjectionByKey(specTipoDocumentoAcademico, x => new
                    { x.TipoDocumentoAcademico.Descricao });
                    var descricaoTipoDocumentoAcademico = tipoDocumentoAcademico != null ? tipoDocumentoAcademico.Descricao : "Descrição do tipo de documento acadêmico não encontrada";

                    throw new PermissaoConfigurarRelatorioException(descricaoTipoDocumentoAcademico, descricaoNivelEnsino);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Busca dados Parâmetros GAD (Template e Fluxo) de acordo com o tipo documento - RN_CNC_087
        /// </summary>
        public DadosInstituicaoNivelTipoDocumentoAcademicoVO BuscarConfiguracaoInstituicaoNivelTipoDocumentoAcademico(long? seqInstituicaoEnsino, long? seqTipoDocumentoAcademico, long? seqInstituicaoNivel)
        {
            var retorno = this.SearchProjectionByKey(new InstituicaoNivelTipoDocumentoAcademicoFilterSpecification
            {
                SeqInstituicaoEnsino = seqInstituicaoEnsino,
                SeqTipoDocumentoAcademico = seqTipoDocumentoAcademico,
                SeqInstituicaoNivel = seqInstituicaoNivel
            }, x => new DadosInstituicaoNivelTipoDocumentoAcademicoVO
            {
                Seq = x.Seq,
                HabilitaRegistroDocumento = x.HabilitaRegistroDocumento,
                SeqGrupoRegistro = x.SeqGrupoRegistro,
                SeqConfiguracaoGad = x.SeqConfiguracaoGad
            });

            if (retorno == null)
                throw new SolicitacaoDocumentoConclusaoParametrizacaoTipoDocumentoNaoEncontradaException();

            return retorno;
        }
    }
}
