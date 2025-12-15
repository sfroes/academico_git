using SMC.Academico.Common.Areas.PES.Includes;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Common.Areas.SRC.Exceptions;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.DadosMestres.ServiceContract.Areas.GED.Interfaces;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.SRC.DomainServices
{
    public class GrupoDocumentoRequeridoDomainService : AcademicoContextDomain<GrupoDocumentoRequerido>
    {
        #region Services

        private ITipoDocumentoService TipoDocumentoService
        {
            get { return this.Create<ITipoDocumentoService>(); }
        }

        #endregion

        #region DomainServices

        private DocumentoRequeridoDomainService DocumentoRequeridoDomainService
        {
            get { return this.Create<DocumentoRequeridoDomainService>(); }
        }

        private ConfiguracaoEtapaDomainService ConfiguracaoEtapaDomainService
        {
            get { return this.Create<ConfiguracaoEtapaDomainService>(); }
        }

        private SolicitacaoServicoDomainService SolicitacaoServicoDomainService
        {
            get { return this.Create<SolicitacaoServicoDomainService>(); }
        }

        private SolicitacaoDocumentoRequeridoDomainService SolicitacaoDocumentoRequeridoDomainService
        {
            get { return this.Create<SolicitacaoDocumentoRequeridoDomainService>(); }
        }

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService
        {
            get { return this.Create<PessoaAtuacaoDomainService>(); }
        }

        #endregion DomainServices

        public GrupoDocumentoRequeridoVO BuscarGrupoDocumentoRequerido(long seqGrupoDocumentoRequerido)
        {
            var grupoDocumentoRequerido = this.SearchByKey(new SMCSeqSpecification<GrupoDocumentoRequerido>(seqGrupoDocumentoRequerido), x => x.ConfiguracaoEtapa.ProcessoEtapa, x => x.Itens);
            var situacaoEtapa = grupoDocumentoRequerido.ConfiguracaoEtapa.ProcessoEtapa.SituacaoEtapa;

            var retorno = grupoDocumentoRequerido.Transform<GrupoDocumentoRequeridoVO>();
            retorno.DescricaoConfiguracaoEtapa = grupoDocumentoRequerido.ConfiguracaoEtapa.Descricao;
            retorno.SeqProcessoEtapa = grupoDocumentoRequerido.ConfiguracaoEtapa.SeqProcessoEtapa;

            return retorno;
        }

        public SMCPagerData<GrupoDocumentoRequeridoListarVO> BuscarGruposDocumentosRequeridos(GrupoDocumentoRequeridoFiltroVO filtro)
        {
            var spec = filtro.Transform<GrupoDocumentoRequeridoFilterSpecification>();

            var lista = this.SearchProjectionBySpecification(spec, a => new GrupoDocumentoRequeridoListarVO()
            {
                Seq = a.Seq,
                SeqConfiguracaoEtapa = a.SeqConfiguracaoEtapa,
                SeqProcessoEtapa = a.ConfiguracaoEtapa.SeqProcessoEtapa,
                SituacaoEtapa = a.ConfiguracaoEtapa.ProcessoEtapa.SituacaoEtapa,
                Descricao = a.Descricao,
                MinimoObrigatorio = a.MinimoObrigatorio,
                UploadObrigatorio = a.UploadObrigatorio,
                Itens = a.Itens.Select(b => new GrupoDocumentoRequeridoItemListarVO() { SeqTipoDocumento = b.DocumentoRequerido.SeqTipoDocumento }).ToList()

            }, out int total).ToList();

            foreach (var grupoDocumento in lista)
                grupoDocumento.Itens.ForEach(a => a.DescricaoTipoDocumento = this.TipoDocumentoService.BuscarTipoDocumento(a.SeqTipoDocumento).Descricao);

            return new SMCPagerData<GrupoDocumentoRequeridoListarVO>(lista, total);
        }

        public List<SMCDatasourceItem> BuscarDocumentosRequeridosSelect(bool uploadObrigatorio, long seqConfiguracaoEtapa)
        {
            List<SMCDatasourceItem> listaDocumentos = new List<SMCDatasourceItem>();

            var spec = new DocumentoRequeridoFilterSpecification()
            {
                SeqConfiguracaoEtapa = seqConfiguracaoEtapa,
                Obrigatorio = false,
                ObrigatorioUpload = false
            };

            if (uploadObrigatorio)
                spec.PermiteUploadArquivo = true;

            var documentosRequeridos = this.DocumentoRequeridoDomainService.SearchBySpecification(spec).ToList();

            foreach (var documento in documentosRequeridos)
            {
                var descricaoTipoDocumento = this.TipoDocumentoService.BuscarTipoDocumento(documento.SeqTipoDocumento).Descricao;
                listaDocumentos.Add(new SMCDatasourceItem() { Seq = documento.Seq, Descricao = descricaoTipoDocumento });
            }

            return listaDocumentos.OrderBy(a => a.Descricao).ToList();
        }

        public long Salvar(GrupoDocumentoRequeridoVO modelo)
        {
            ValidarModeloSalvar(modelo);

            var dominio = modelo.Transform<GrupoDocumentoRequerido>();
            this.SaveEntity(dominio);

            var configuracaoEtapa = this.ConfiguracaoEtapaDomainService.SearchByKey(new SMCSeqSpecification<ConfiguracaoEtapa>(modelo.SeqConfiguracaoEtapa));

            var listaSolicitacoesEmAberto = this.SolicitacaoServicoDomainService.SearchBySpecification(new SolicitacaoServicoFilterSpecification()
            {
                SeqConfiguracaoProcesso = configuracaoEtapa.SeqConfiguracaoProcesso,
                CategoriasSituacoes = new List<CategoriaSituacao> { CategoriaSituacao.Novo, CategoriaSituacao.EmAndamento, CategoriaSituacao.Concluido }

            }).ToList();

            foreach (var solicitacaoServico in listaSolicitacoesEmAberto)
            {
                foreach (var item in modelo.Itens)
                {
                    var solicitacoesDocumentoRequerido = this.SolicitacaoDocumentoRequeridoDomainService.SearchBySpecification(new SolicitacaoDocumentoRequeridoFilterSpecification() { SeqDocumentoRequerido = item.SeqDocumentoRequerido, SeqSolicitacaoServico = solicitacaoServico.Seq }).ToList();

                    if (!solicitacoesDocumentoRequerido.Any())
                    {
                        var documentoRequerido = this.DocumentoRequeridoDomainService.SearchByKey(new SMCSeqSpecification<DocumentoRequerido>(item.SeqDocumentoRequerido));

                        SolicitacaoDocumentoRequerido solicitacaoDocumentoRequerido = new SolicitacaoDocumentoRequerido()
                        {
                            SeqDocumentoRequerido = item.SeqDocumentoRequerido,
                            SeqSolicitacaoServico = solicitacaoServico.Seq,
                            SituacaoEntregaDocumento = SituacaoEntregaDocumento.AguardandoEntrega,
                            FormaEntregaDocumento = null,
                            VersaoDocumento = null,
                            DataEntrega = null,
                            Observacao = null,
                            SeqArquivoAnexado = null,
                            DataPrazoEntrega = null
                        };

                        if (documentoRequerido.Sexo.HasValue)
                        {
                            var pessoaAtuacao = this.PessoaAtuacaoDomainService.SearchByKey(new SMCSeqSpecification<PessoaAtuacao>(solicitacaoServico.SeqPessoaAtuacao), IncludesPessoaAtuacao.DadosPessoais);

                            if (documentoRequerido.Sexo.Value == pessoaAtuacao.DadosPessoais?.Sexo)
                                this.SolicitacaoDocumentoRequeridoDomainService.SaveEntity(solicitacaoDocumentoRequerido);
                        }
                        else
                            this.SolicitacaoDocumentoRequeridoDomainService.SaveEntity(solicitacaoDocumentoRequerido);
                    }
                }
            }

            return dominio.Seq;
        }

        public void ValidarModeloSalvar(GrupoDocumentoRequeridoVO modelo)
        {
            var situacaoEtapa = this.ConfiguracaoEtapaDomainService.SearchProjectionByKey(new SMCSeqSpecification<ConfiguracaoEtapa>(modelo.SeqConfiguracaoEtapa), x => x.ProcessoEtapa.SituacaoEtapa);
            var documentosAssociados = this.DocumentoRequeridoDomainService.SearchBySpecification(new DocumentoRequeridoFilterSpecification() { Seqs = modelo.Itens.Select(a => a.SeqDocumentoRequerido).ToArray() }).ToList();

            if (situacaoEtapa == SituacaoEtapa.Liberada || situacaoEtapa == SituacaoEtapa.Encerrada)
                throw new ConfiguracaoEtapaOperacaoNaoPermitidaException();

            if (modelo.UploadObrigatorio.Value && documentosAssociados.Any(a => a.Obrigatorio || !a.PermiteUploadArquivo || a.ObrigatorioUpload))
                throw new SalvarGrupoUploadObrigatorioException();

            if (!modelo.UploadObrigatorio.Value && documentosAssociados.Any(a => a.Obrigatorio || a.ObrigatorioUpload))
                throw new SalvarGrupoUploadNaoObrigatorioException();

            if (modelo.Itens.Count() < modelo.MinimoObrigatorio)
                throw new SalvarGrupoQuantidadeDocumentosAssociadosException();

            if (modelo.Seq != 0)
            {
                var grupoDocumentoRequeridoAntigo = this.SearchByKey(new SMCSeqSpecification<GrupoDocumentoRequerido>(modelo.Seq), x => x.ConfiguracaoEtapa, x => x.Itens);
                var solicitacaoServicoAssociadaConfiguracaoProcesso = this.SolicitacaoServicoDomainService.Count(new SolicitacaoServicoFilterSpecification() { SeqConfiguracaoProcesso = grupoDocumentoRequeridoAntigo.ConfiguracaoEtapa.SeqConfiguracaoProcesso }) > 0;

                var listaSequenciaisItensGrupoAntigo = grupoDocumentoRequeridoAntigo.Itens.Select(a => a.SeqDocumentoRequerido).ToList();
                var listaSequenciaisItensGrupoNovo = modelo.Itens.Select(a => a.SeqDocumentoRequerido).ToList();

                var listaDiferencas1 = listaSequenciaisItensGrupoAntigo.Except(listaSequenciaisItensGrupoNovo).ToList();
                var listaDiferencas2 = listaSequenciaisItensGrupoNovo.Except(listaSequenciaisItensGrupoAntigo).ToList();
                var listaFinalDiferencas = listaDiferencas1.Union(listaDiferencas2).ToList();

                if (listaFinalDiferencas.Any() && solicitacaoServicoAssociadaConfiguracaoProcesso)
                    throw new ExistemSolicitacoesAssociadasTrocaDocumentoGrupoException();
            }
        }

        public void Excluir(long seq)
        {
            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                try
                {
                    ValidarModeloExcluir(seq);

                    var documentoRequerido = this.SearchByKey(new SMCSeqSpecification<GrupoDocumentoRequerido>(seq));
                    this.DeleteEntity(documentoRequerido);

                    unitOfWork.Commit();
                }
                catch (Exception)
                {
                    unitOfWork.Rollback();
                    throw;
                }
            }
        }

        private void ValidarModeloExcluir(long seq)
        {
            var grupoDocumentoRequerido = this.SearchByKey(new SMCSeqSpecification<GrupoDocumentoRequerido>(seq), x => x.ConfiguracaoEtapa.ProcessoEtapa, x => x.Itens);
            var solicitacoesDocumentoRequerido = this.SolicitacaoDocumentoRequeridoDomainService.SearchBySpecification(new SolicitacaoDocumentoRequeridoFilterSpecification() { SeqsDocumentosRequeridos = grupoDocumentoRequerido.Itens.Select(a => a.SeqDocumentoRequerido).ToArray() }).ToList();

            if (solicitacoesDocumentoRequerido.Any())
                throw new ExclusaoGrupoDocumentoAssociadoSolicitacaoException();
        }
    }
}