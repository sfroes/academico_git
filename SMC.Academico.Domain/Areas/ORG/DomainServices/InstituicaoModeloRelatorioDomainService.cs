using System;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Framework.Domain;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.Common.Areas.ORG.Includes;
using System.Linq;
using SMC.Academico.Domain.Areas.ORG.ValueObjects;
using SMC.Framework.Specification;
using SMC.Framework.Model;
using SMC.Framework.Extensions;
using System.Collections.Generic;
using SMC.Framework;

namespace SMC.Academico.Domain.Areas.ORG.DomainServices
{
    public class InstituicaoModeloRelatorioDomainService : AcademicoContextDomain<InstituicaoModeloRelatorio>
    {
        public InstituicaoModeloRelatorioVO BuscarInstituicaoModeloRelatorio(long seq)
        {
            return this.SearchProjectionByKey(new SMCSeqSpecification<InstituicaoModeloRelatorio>(seq), x => new InstituicaoModeloRelatorioVO
            {
                Seq = x.Seq,
                SeqInstituicaoEnsino = x.SeqInstituicaoEnsino,
                ModeloRelatorio = x.ModeloRelatorio,
                Idioma = x.Idioma,
                ArquivoModelo = new SMCUploadFile
                {
                    GuidFile = x.ArquivoModelo.UidArquivo.ToString(),
                    Name = x.ArquivoModelo.Nome,
                    Size = x.ArquivoModelo.Tamanho,
                    Type = x.ArquivoModelo.Tipo
                },
                SeqArquivoModelo = x.SeqArquivoModelo
            });
        }

        public InstituicaoModeloRelatorio BuscarTemplateModeloRelatorio(long seqInstituicaoEnsino, ModeloRelatorio modeloRelatorio)
        {
            var spec = new InstituicaoModeloRelatorioFilterSpecification() { SeqInstituicaoEnsino = seqInstituicaoEnsino, ModeloRelatorio = modeloRelatorio };

            var obj = SearchBySpecification(spec, IncludesModeloRelatorio.ArquivoModelo).FirstOrDefault();

            return obj;
        }

        public SMCPagerData<InstituicaoModeloRelatorioListarVO> BuscarInstituicaoModeloRelatorios(InstituicaoModeloRelatorioFiltroVO filtros)
        {
            /*O FILTRO, ORDENAÇÃO E PAGINAÇÃO DESTE MÉTODO FORAM FEITAS MANUALMENTE PARA ORDENAR
            PELO CAMPO MODELO, COMO É UM ENUM E AO MONTAR A QUERY LEVA EM CONSIDERAÇÃO SEU SEQ DO DOMÍNIO E NÃO SEU VALOR*/

            var spec = filtros.Transform<InstituicaoModeloRelatorioFilterSpecification>();

            //LIMPANDO A ORDENAÇÃO QUE SERÁ FEITA MANUALMENTE E SETANDO O MAXRESULTS PARA NÃO BUSCAR TODOS OS REGISTROS DA TABELA
            int qtdeRegistros = this.Count();
            spec.MaxResults = qtdeRegistros > 0 ? qtdeRegistros : int.MaxValue;
            spec.ClearOrderBy();

            var listaVO = SearchProjectionBySpecification(spec, x => new InstituicaoModeloRelatorioListarVO
            {
                Seq = x.Seq,
                DescricaoInstituicaoEnsino = x.InstituicaoEnsino.Nome,
                ModeloRelatorio = x.ModeloRelatorio,
                Idioma = x.Idioma,
                ArquivoModelo = new SMCUploadFile
                {
                    GuidFile = x.ArquivoModelo.UidArquivo.ToString(),
                    Name = x.ArquivoModelo.Nome,
                    Size = x.ArquivoModelo.Tamanho,
                    Type = x.ArquivoModelo.Tipo
                }
            }).ToList();

            int total = listaVO.Count();

            //ORDENAÇÃO MANUAL EM TODA A LISTA, NÃO SOMENTE NA PÁGINA ATUAL
            List<SMCSortInfo> listaOrdenacao = filtros.PageSettings.SortInfo;

            foreach (var sort in listaOrdenacao)
            {
                if (sort.FieldName == nameof(InstituicaoModeloRelatorioListarVO.DescricaoInstituicaoEnsino))
                {
                    if (sort.Direction == SMCSortDirection.Ascending)
                        listaVO = listaVO.OrderBy(o => o.DescricaoInstituicaoEnsino).ToList();
                    else
                        listaVO = listaVO.OrderByDescending(o => o.DescricaoInstituicaoEnsino).ToList();
                }
                if (sort.FieldName == nameof(InstituicaoModeloRelatorioListarVO.ModeloRelatorio))
                {
                    if (sort.Direction == SMCSortDirection.Ascending)
                        listaVO = listaVO.OrderBy(o => o.ModeloRelatorio.SMCGetDescription()).ToList();
                    else
                        listaVO = listaVO.OrderByDescending(o => o.ModeloRelatorio.SMCGetDescription()).ToList();
                }            
            }

            //CONFIGURAÇÃO DE PAGINAÇÃO, RECUPERANDO OS REGISTROS DA PÁGINA ATUAL
            listaVO = listaVO.Skip((filtros.PageSettings.PageIndex - 1) * filtros.PageSettings.PageSize).Take(filtros.PageSettings.PageSize).ToList();

            return new SMCPagerData<InstituicaoModeloRelatorioListarVO>(listaVO, total);

            #region Listagem padrão 

            //int total = 0;

            //var lista = SearchProjectionBySpecification(filtros.Transform<InstituicaoModeloRelatorioFilterSpecification>(), x => new InstituicaoModeloRelatorioListarVO
            //{
            //    Seq = x.Seq,
            //    DescricaoInstituicaoEnsino = x.InstituicaoEnsino.Nome,
            //    ModeloRelatorio = x.ModeloRelatorio,
            //    ArquivoModelo = new SMCUploadFile
            //    {
            //        GuidFile = x.ArquivoModelo.UidArquivo.ToString(),
            //        Name = x.ArquivoModelo.Nome,
            //        Size = x.ArquivoModelo.Tamanho,
            //        Type = x.ArquivoModelo.Tipo
            //    }
            //}, out total).ToList();

            //return new SMCPagerData<InstituicaoModeloRelatorioListarVO>(lista, total);

            #endregion
        }

        /// <summary>
        /// Salva um modelo de relatório
        /// </summary>
        /// <param name="modelo">Modelo de relatorio de Instituição a ser salvo</param>
        /// <returns>Sequencial do modelo de relatorio de intituição salvo</returns>
        public long SalvarModeloRelatorio(InstituicaoModeloRelatorio modelo)
        {
            this.EnsureFileIntegrity(modelo, x => x.SeqArquivoModelo, x => x.ArquivoModelo);

            // Salva o modelo
            this.SaveEntity(modelo);

            // Retorna o sequencial salvo
            return modelo.Seq;
        }

    }
}

