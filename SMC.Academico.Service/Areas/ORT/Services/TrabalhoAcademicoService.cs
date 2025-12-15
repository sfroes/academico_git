using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Common.Areas.ORT.Constants;
using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.Domain.Areas.ORT.DomainServices;
using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Academico.Domain.Areas.ORT.Specifications;
using SMC.Academico.Domain.Areas.ORT.ValueObjects;
using SMC.Academico.Domain.Models;
using SMC.Academico.ServiceContract.Areas.ORT.Data;
using SMC.Academico.ServiceContract.Areas.ORT.Interfaces;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Security.Util;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.ORT.Services
{
    public class TrabalhoAcademicoService : SMCServiceBase, ITrabalhoAcademicoService
    {
        #region DomainServices
        private TrabalhoAcademicoDomainService TrabalhoAcademicoDomainService { get => Create<TrabalhoAcademicoDomainService>(); }

        private PublicacaoBdpDomainService PublicacaoBdpDomainService { get => Create<PublicacaoBdpDomainService>(); }

        private InstituicaoNivelDomainService InstituicaoNivelDomainService { get => Create<InstituicaoNivelDomainService>(); }

        private PublicacaoBdpAutorizacaoDomainService PublicacaoBdpAutorizacaoDomainService => this.Create<PublicacaoBdpAutorizacaoDomainService>();

        #endregion DomainServices

        public SMCPagerData<TrabalhoAcademicoListaData> BuscarTrabalhosAcademicos(TrabalhoAcademicoFiltroData filtro)
        {
            return TrabalhoAcademicoDomainService.BuscarTrabalhosAcademicos(filtro.Transform<TrabalhoAcademicoFilterSpecification>())
                                                    .Transform<SMCPagerData<TrabalhoAcademicoListaData>>();
        }

        public SMCPagerData<TrabalhoAcademicoListaData> BuscarTrabalhosAcademicosLiberacaoConsulta(TrabalhoAcademicoFiltroData filtro)
        {
            // Monta o specification para pesquisar as publicações BDP
            var spec = filtro.Transform<PublicacaoBdpFilterSpecification>();
            spec.SetOrderBy(o => o.TrabalhoAcademico.Autores.FirstOrDefault().Aluno.Historicos.OrderByDescending(f => f.DataInclusao).FirstOrDefault().EntidadeVinculo.Nome);
            spec.SetOrderBy(o => o.TrabalhoAcademico.Autores.FirstOrDefault().Aluno.Historicos.OrderByDescending(f => f.DataInclusao).FirstOrDefault().CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Nome);
            spec.SetOrderBy(o => o.TrabalhoAcademico.Autores.FirstOrDefault().Aluno.DadosPessoais.Nome);

            // Busca as publicações BDP
            return PublicacaoBdpDomainService.BuscarPublicacoesBdp(spec).Transform<SMCPagerData<TrabalhoAcademicoListaData>>(); ;
        }

        /// <summary>
        /// Consulta avançada
        /// </summary>
        /// <param name="filtro">Filtro</param>
        /// <returns></returns>
		public SMCPagerData<TrabalhoAcademicoListaData> BuscarTrabalhoAcademicosBDP(TrabalhoAcademicoFiltroData filtro)
        {
            return TrabalhoAcademicoDomainService.ConsultaAvancadaTrabalhosAcademicos(filtro.Transform<TrabalhoAcademicoFiltroVO>())
                                                    .Transform<SMCPagerData<TrabalhoAcademicoListaData>>();
        }

        /// <summary>
        /// Buscar Trabalhos com futuras defesas
        /// </summary>
        /// <param name="filtro">Filtros da pesquisa</param>
        /// <returns>Lista paginada do trabalho</returns>
        public SMCPagerData<TrabalhoAcademicoListaData> BuscarTrabalhoFuturasDefesasAcademicosBDP(TrabalhoAcademicoFiltroData filtro)
        {
            return TrabalhoAcademicoDomainService.ConsultaFuturasDefesasTrabalhosAcademicos(filtro.Transform<TrabalhoAcademicoFiltroVO>())
                                        .Transform<SMCPagerData<TrabalhoAcademicoListaData>>();
        }

        /*
        private SMCPagerData<TrabalhoAcademicoListaData> BuscarTrabalhoAcademicos(SMCSpecification<TrabalhoAcademico> spec)
        {
            var lista = TrabalhoAcademicoDomainService.SearchProjectionBySpecification(spec, x => new TrabalhoAcademicoListaData
            {
                Seq = x.Seq,
                Data = x.DivisoesComponente.Select(f => f.OrigemAvaliacao.AplicacoesAvaliacao
                                                                    .Where(g => g.Avaliacao.TipoAvaliacao == TipoAvaliacao.Banca && g.DataCancelamento == null)
                                                                    .OrderByDescending(o => o.Seq)
                                                                    .FirstOrDefault().DataInicioAplicacaoAvaliacao).FirstOrDefault(),
                SeqTipoTrabalho = x.SeqTipoTrabalho,
                DescricaoTipoTrabalho = x.TipoTrabalho.Descricao,
                Titulo = x.Titulo,
                TituloPortugues = x.PublicacaoBdp.FirstOrDefault().InformacoesIdioma.FirstOrDefault(f => f.Idioma == SMCLanguage.Portuguese).Titulo,
                Autores = x.Autores.Select(f => f.NomeAutor + " (" + f.NomeAutorFormatado + ")").ToList(),
                Orientadores = x.DivisoesComponente.SelectMany(f => f.OrigemAvaliacao.AplicacoesAvaliacao.SelectMany(g => g.MembrosBancaExaminadora.Where(w => w.TipoMembroBanca == TipoMembroBanca.Orientador).Select(h => h.SeqColaborador.HasValue ? h.Colaborador.DadosPessoais.Nome : h.NomeColaborador))).OrderBy(o => o).ToList(),
                Coorientadores = x.DivisoesComponente.SelectMany(f => f.OrigemAvaliacao.AplicacoesAvaliacao.SelectMany(g => g.MembrosBancaExaminadora.Where(w => w.TipoMembroBanca == TipoMembroBanca.Coorientador && w.Participou == true).Select(h => h.SeqColaborador.HasValue ? h.Colaborador.DadosPessoais.Nome : h.NomeColaborador))).OrderBy(o => o).ToList(),
                OrdenacaoAutor = x.Autores.OrderBy(o => o.NomeAutor).FirstOrDefault().NomeAutor,
                OrdnacaoOrientador = x.DivisoesComponente.SelectMany(f => f.OrigemAvaliacao.AplicacoesAvaliacao.SelectMany(g => g.MembrosBancaExaminadora.Where(w => w.TipoMembroBanca == TipoMembroBanca.Orientador).Select(h => h.SeqColaborador.HasValue ? h.Colaborador.DadosPessoais.Nome : h.NomeColaborador))).OrderBy(o => o).FirstOrDefault(),
            }, out int total);

            return new SMCPagerData<TrabalhoAcademicoListaData>(lista, total);
        }
        */

        public VisualizarTrabalhoAcademicoData VisualizarTrabalhoAcademico(long seq)
        {
            return TrabalhoAcademicoDomainService.VisualizarTrabalhoAcademico(seq).Transform<VisualizarTrabalhoAcademicoData>();
        }

        public string FormatarNome(long seqAutor)
        {
            var ret = TrabalhoAcademicoDomainService.FormatarNome(seqAutor);
            return ret;
        }

        public long SalvarTrabalhoAcademico(TrabalhoAcademicoData trabalhoAcademico)
        {
            return TrabalhoAcademicoDomainService.SalvarTrabalhoAcademico(trabalhoAcademico.Transform<TrabalhoAcademicoVO>());
        }

        public void IncluirSegundoDeposito(long seqTrabalhoAcademico, string justificativa, DateTime dataAutorizacao, ArquivoAnexado arquivo)
        {
            TrabalhoAcademicoDomainService.IncluirSegundoDeposito(seqTrabalhoAcademico, justificativa, dataAutorizacao, arquivo);
        }

        public bool ValidarSituacaoTrabalho(TrabalhoAcademicoData trabalho) 
        {

            var situacaoTrabalho = TrabalhoAcademicoDomainService.SearchProjectionByKey(new SMCSeqSpecification<TrabalhoAcademico>(trabalho.Seq),
                  x => x.PublicacaoBdp.FirstOrDefault().HistoricoSituacoes.OrderByDescending(o => o.DataInclusao)
                  .FirstOrDefault())?.SituacaoTrabalhoAcademico;

            if (situacaoTrabalho == SituacaoTrabalhoAcademico.AutorizadaLiberadaSecretaria || situacaoTrabalho == SituacaoTrabalhoAcademico.CadastradaAluno)
            {
                return true;
            }

            return false;
        }

        public long SalvarAlteracoesLiberacaoConsultaBdp(TrabalhoAcademicoPublicacaoBdpData trabalhoAcademico)
        {
            return PublicacaoBdpDomainService.SalvarAlteracoesLiberacaoConsultaBdp(trabalhoAcademico.Transform<LiberacaoPublicacaoBdpVO>());
        }

        public TrabalhoAcademicoData AlterarTrabalhoAcademico(long seq)
        {
            var entity = this.TrabalhoAcademicoDomainService.AlterarTrabalhoAcademico(seq);               

            return entity.Transform<TrabalhoAcademicoData>();
        }

        public void ExcluirTrabalhoAcademico(long seq)
        {
            this.TrabalhoAcademicoDomainService.ExcluirTrabalhoAcademico(seq);
        }

        public TrabalhoAcademicoPublicacaoBdpData AlterarTrabalhoAcademicoPublicacaoBdp(long seq)
        {
            var ret = TrabalhoAcademicoDomainService.SearchProjectionByKey(new SMCSeqSpecification<TrabalhoAcademico>(seq), x => new TrabalhoAcademicoPublicacaoBdpData
            {
                Seq = x.Seq,
                SeqPublicacaoBdp = x.PublicacaoBdp.FirstOrDefault().Seq,
                Titulo = x.Titulo,
                Alunos = x.Autores.Select(f => new TrabalhoAcademicoAutoriaData()
                {
                    SeqTrabalhoAcademicoAutoria = f.Seq,
                    NomeAutor = f.NomeAutor,
                    NomeAutorFormatado = f.NomeAutorFormatado,
                    EmailAutor = f.EmailAutor
                }).ToList(),
                TipoTrabalho = x.TipoTrabalho.Descricao,
                DataDefesa = x.DivisoesComponente.Select(f => f.OrigemAvaliacao.AplicacoesAvaliacao
                                                                    .Where(g => g.Avaliacao.TipoAvaliacao == TipoAvaliacao.Banca && g.DataCancelamento == null)
                                                                    .OrderByDescending(o => o.Seq)
                                                                    .FirstOrDefault().DataInicioAplicacaoAvaliacao).FirstOrDefault(),

                DataEntrega = x.DataDepositoSecretaria,
                QuantidadeVolumes = x.PublicacaoBdp.FirstOrDefault().QuantidadeVolumes,
                QuantidadePaginas = x.PublicacaoBdp.FirstOrDefault().QuantidadePaginas,
                CodigoAcervo = x.PublicacaoBdp.FirstOrDefault().CodigoAcervo,
                Arquivos = x.PublicacaoBdp.SelectMany(m => m.Arquivos).Select(f => new PublicacaoBdpArquivoData()
                {
                    Seq = f.Seq,
                    TipoAutorizacao = f.TipoAutorizacao,
                    NomeArquivo = f.NomeArquivo,
                    TamanhoArquivo = f.TamanhoArquivo,
                    UrlArquivo = f.UrlArquivo,
                    Arquivo = new SMCUploadFile
                    {
                        // Adiciona um valor qualquer ao GuidFile para o componente saber se o arquivo já existia ou não
                        GuidFile = x.Seq.ToString(),
                        Name = f.NomeArquivo,
                        Size = f.TamanhoArquivo ?? 0
                    }
                }).ToList(),
                ArquivoAnexadoAtaDefesa = x.DivisoesComponente.SelectMany(f => f.OrigemAvaliacao
                                                                                .AplicacoesAvaliacao
                                                                                .OrderByDescending(a => a.DataInicioAplicacaoAvaliacao)
                                                                                .FirstOrDefault(a => !a.DataCancelamento.HasValue && a.ApuracoesAvaliacao.Any(ap => ap.SeqArquivoAnexadoAtaDefesa.HasValue))
                                                                                .ApuracoesAvaliacao.Select(aq => new SMCUploadFile
                                                                                {
                                                                                    GuidFile = aq.ArquivoAnexadoAtaDefesa.UidArquivo.ToString(),
                                                                                    Name = aq.ArquivoAnexadoAtaDefesa.Nome,
                                                                                    Size = aq.ArquivoAnexadoAtaDefesa.Tamanho
                                                                                })).FirstOrDefault(),
                Banca = x.DivisoesComponente.SelectMany(f => f.OrigemAvaliacao.AplicacoesAvaliacao.Where(g => g.Avaliacao.TipoAvaliacao == TipoAvaliacao.Banca && g.DataCancelamento == null)
                                                            .OrderByDescending(g => g.Seq).FirstOrDefault()
                                                            .MembrosBancaExaminadora.Where(w => w.Participou == true)
                                                            .Select(h => new TrabalhoAcademicoMembroBancaData
                                                            {
                                                                Nome = h.Colaborador.DadosPessoais.Nome,
                                                                TipoMembroBanca = h.TipoMembroBanca,
                                                                Instituicao = h.InstituicaoExterna.Nome,
                                                                NomeInstituicaoExterna = h.NomeInstituicaoExterna,
                                                                NomeColaborador = h.NomeColaborador,
                                                                ComplementoInstituicao = h.ComplementoInstituicao
                                                            })).OrderBy(m => m.Nome).ThenBy(m => m.NomeColaborador).ToList(),
                Idiomas = x.PublicacaoBdp.FirstOrDefault().InformacoesIdioma.Select(f => new PublicacaoBdpIdiomaData
                {
                    Seq = f.Seq,
                    Idioma = f.Idioma,
                    IdiomaTrabalho = f.IdiomaTrabalho,
                    Titulo = f.Titulo,
                    Resumo = f.Resumo,
                    PalavrasChave = f.PalavrasChave.Select(g => new PublicacaoBdpPalavraChaveData
                    {
                        Seq = g.Seq,
                        PalavraChave = g.PalavraChave
                    }).ToList()
                }).ToList(),
            });

            var spec = new PublicacaoBdpAutorizacaoFilterSpecification() { SeqPublicacaoBdp = ret.SeqPublicacaoBdp };

            ret.Autorizacoes = PublicacaoBdpAutorizacaoDomainService.SearchProjectionBySpecification(spec,
                x => new PublicacaoBdpAutorizacaoData
                {
                    CodigoAutorizacao = x.CodigoAutorizacao.ToString(),
                    DataAutorizacao = x.DataAutorizacao.ToString(),
                    TipoAutorizacao = x.TipoAutorizacao.ToString()
                }).ToList();

            //Monta a descrição do Membro da banca
            ret.Banca.ForEach(m =>
            {
                string nomeInstituicao = m.Instituicao ?? m.NomeInstituicaoExterna;
                string nomeColaborador = m.Nome ?? m.NomeColaborador;
                string complementoInstituicao = !string.IsNullOrEmpty(m.ComplementoInstituicao) ? $" - {m.ComplementoInstituicao}" : string.Empty;

                string tipoMembro = string.Empty;
                string instituicao = string.Empty;
                if (m.TipoMembroBanca == TipoMembroBanca.Coorientador || m.TipoMembroBanca == TipoMembroBanca.Orientador)
                    tipoMembro = string.IsNullOrEmpty(m.TipoMembroBanca.SMCGetDescription()) ? "" : $" ({m.TipoMembroBanca.SMCGetDescription()})";

                instituicao = string.IsNullOrEmpty(nomeInstituicao) ? "" : $" ({nomeInstituicao}{complementoInstituicao})";

                m.DescricaoMembro = $"{nomeColaborador}{tipoMembro}{instituicao}";
            });
            ret.Banca = ret?.Banca.OrderBy(b => b.DescricaoMembro).ToList();

            // Ordena o idioma do trabalho. 1o o idioma do trabalho, 2o por ordem alfabética do idioma
            ret.Idiomas = ret?.Idiomas.OrderByDescending(i => i.IdiomaTrabalho).ThenBy(i => i.Idioma.SMCGetDescription()).ToList();
            
            return ret;
        }

        public CabecalhoPublicacaoBdpData BuscarCabecalhoPublicacaoBdp(long seq)
        {
            return TrabalhoAcademicoDomainService.BuscarCabecalhoPublicacaoBdp(seq).Transform<CabecalhoPublicacaoBdpData>();
        }

        public string UrlPublicacao(long seqTrabalhoAcademico, string nomeArquivo)
        {
            return TrabalhoAcademicoDomainService.SearchProjectionByKey(new SMCSeqSpecification<TrabalhoAcademico>(seqTrabalhoAcademico),
                                                    x => x.PublicacaoBdp.FirstOrDefault().Arquivos.FirstOrDefault(f=> f.NomeArquivo.Contains(nomeArquivo)).UrlArquivo);
        }

        public AvaliacaoTrabalhoAcademicoCabecalhoData BuscarTrabalhoAcademicoCabecalho(long seq)
        {
            var entity = this.TrabalhoAcademicoDomainService.BuscarTrabalhoAcademicoCabecalho(seq);
            return entity.Transform<AvaliacaoTrabalhoAcademicoCabecalhoData>();
        }

        /// <summary>
        /// Busca o comprovante de entrega para cada autoria do trabalho academico
        /// </summary>
        /// UC_ORT_002_02_06 - Emitir de Comprovante de Entrega
        ///
        /// <param name="seq"></param>
        /// <returns></returns>
        public List<ComprovanteEntregaTrabalhoAcademicoData> BuscarComprovantesEntregaTrabalhoAcademico(long seq)
        {
            return TrabalhoAcademicoDomainService.BuscarComprovantesEntregaTrabalhoAcademico(seq).TransformList<ComprovanteEntregaTrabalhoAcademicoData>();
        }

        /// <summary>
        /// Busca o relatório de comprovante de entrega para cada autoria do trabalho academico
        /// </summary>
        /// UC_ORT_002_02_06 - Emitir de Comprovante de Entrega
        ///
        /// <param name="seq"></param>
        /// <returns></returns>
        public byte[] BuscarRelatorioEntregaTrabalhoAcademico(long seq)
        {
            return TrabalhoAcademicoDomainService.BuscarRelatorioEntregaTrabalhoAcademico(seq);
        }

        public long BuscarSeqAlunoTrabalhoAcademico(long seqTrabalhoAcademico)
        {
            return TrabalhoAcademicoDomainService.BuscarSeqAlunoTrabalhoAcademico(seqTrabalhoAcademico);
        }

        public DateTime? DataPublicacaoBdpTrabalhoAcademico(long seqTrabalhoAcademico)
        {
            return TrabalhoAcademicoDomainService.DataPublicacaoBdpTrabalhoAcademico(seqTrabalhoAcademico);
        }

        public SituacaoAlunoFormacao? BuscarSituacaoAlunoFormacaoHistorico(long seqTrabalhoAcademico)
        {
            return TrabalhoAcademicoDomainService.BuscarSituacaoAlunoFormacaoHistorico(seqTrabalhoAcademico);
        }

        /// <summary>
        /// Método que verifica se existe alguma avaliação, cadastrada para o trabalho acadêmico
        /// </summary>
        /// <param name="seqTrabalhoAcademico"></param>
        /// <returns>true, false</returns>
        public bool ExisteAvaliacao(long seqTrabalhoAcademico) { return TrabalhoAcademicoDomainService.ExisteAvaliacao(seqTrabalhoAcademico); }

        public bool ValidarDataMinimaDepositoSecretaria(DateTime dataDepositoSecretaria)
        {
            return TrabalhoAcademicoDomainService.ValidarDataMinimaDepositoSecretaria(dataDepositoSecretaria);
        }

        public bool VerificaPublicacaoBdpIdioma(long seqTrabalhoAcademico)
        {
            return TrabalhoAcademicoDomainService.SearchProjectionByKey(seqTrabalhoAcademico, x => x.PublicacaoBdp.Any(p => p.InformacoesIdioma.Any()));
        }

        public (long SeqTipoTrabalho, long? SeqDivisaoComponente) RecuperarDadosTipoTrabalhoAcademico(long seqAluno)
        {
            return TrabalhoAcademicoDomainService.RecuperarDadosTipoTrabalhoAcademico(seqAluno);
        }

        public bool AtendeRegraHabilitarAgendamentoBanca(long seqTrabalhoAcademico)
        {
            return TrabalhoAcademicoDomainService.AtendeRegraHabilitarAgendamentoBanca(seqTrabalhoAcademico);
        }
    }
}