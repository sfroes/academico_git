using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.APR.Exceptions;
using SMC.Academico.Domain.Areas.APR.DomainServices;
using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Academico.Domain.Areas.APR.Specifications;
using SMC.Academico.Domain.Areas.APR.ValueObjects;
using SMC.Academico.ServiceContract.Areas.APR.Data;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.APR.Services
{
    public class MaterialService : SMCServiceBase, IMaterialService
    {
        #region [ DomainService ]

        private MaterialDomainService MaterialDomainService
        {
            get { return Create<MaterialDomainService>(); }
        }

        private OrigemMaterialDomainService OrigemMaterialDomainService
        {
            get { return Create<OrigemMaterialDomainService>(); }
        }

        #endregion [ DomainService ]

        public MaterialData[] ListarMateriais(MaterialFiltroData filtro)
        {
            //Fiz isto porque sei lá por qual motivo nenhum está vindo nulo, todos não informados estão vindo com valor zerado.
            if (filtro.SeqOrigem.GetValueOrDefault() == 0)
            {
                filtro.SeqOrigem = null;
            }

            if (filtro.SeqOrigemMaterial.GetValueOrDefault() == 0)
            {
                filtro.SeqOrigemMaterial = null;
            }

            if (filtro.TipoOrigemMaterial.GetValueOrDefault() == TipoOrigemMaterial.Nenhum)
            {
                filtro.TipoOrigemMaterial = null;
            }

            if (!filtro.SeqOrigemMaterial.HasValue && (!filtro.SeqOrigem.HasValue || !filtro.TipoOrigemMaterial.HasValue))
            {
                throw new ListarException();
            }

            return MaterialDomainService.ListarMateriais(filtro.Transform<MaterialFilterSpecification>()).OrderBy(a => a.SeqSuperior).ThenBy(a => a.Descricao).ToList().TransformListToArray<MaterialData>();
        }

        public long SalvarMaterial(MaterialData material)
        {
            if (material.SeqSuperior.GetValueOrDefault() == 0)
            {
                material.SeqSuperior = null;
            }
            return MaterialDomainService.SalvarMaterial(material.Transform<MaterialVO>());
        }

        public MaterialData InserirMaterial(MaterialFiltroData filtro)
        {
            if (filtro.SeqOrigemMaterial.GetValueOrDefault() > 0)
            {
                OrigemMaterial om = OrigemMaterialDomainService.SearchByKey(new SMCSeqSpecification<OrigemMaterial>(filtro.SeqOrigemMaterial.Value));
                filtro.TipoOrigemMaterial = om.TipoOrigemMaterial;
            }

            MaterialData material = filtro.Transform<MaterialData>();
            material.ExibeVisualizacoes = (material.TipoOrigemMaterial == TipoOrigemMaterial.Entidade);
            material.SeqSuperior = filtro.SeqPai;

            if (filtro.SeqPai.GetValueOrDefault() > 0)
            {
                material.NomePasta = MaterialDomainService.SearchByKey(new SMCSeqSpecification<Material>(filtro.SeqPai.Value)).Descricao;
            }

            return material;
        }

        public OrigemMaterialData BuscarOrigemMaterial(long seqOrigemMaterial)
        {
            return OrigemMaterialDomainService.SearchByKey(new SMCSeqSpecification<OrigemMaterial>(seqOrigemMaterial)).Transform<OrigemMaterialData>();
        }

        public void ExcluirMaterial(long seq)
        {
            MaterialDomainService.ExcluirMaterial(seq);
        }

        public MaterialData AlterarMaterial(long seq)
        {
            var material = MaterialDomainService.SearchByKey(new SMCSeqSpecification<Material>(seq), a => a.ArquivoAnexado, a => a.Visualizacoes, a => a.OrigemMaterial);
           
            var retorno = material.Transform<MaterialData>();

            if (retorno.ArquivoAnexado != null)
                retorno.ArquivoAnexado.GuidFile = material.ArquivoAnexado.UidArquivo.ToString();

            retorno.ExibeVisualizacoes = (retorno.TipoOrigemMaterial == TipoOrigemMaterial.Entidade);

            if (retorno.SeqSuperior.GetValueOrDefault() > 0)
            {
                retorno.NomePasta = MaterialDomainService.SearchByKey(new SMCSeqSpecification<Material>(retorno.SeqSuperior.Value)).Descricao;
            }

            return retorno;
        }

        public MaterialData[] ListarMateriaisParaDownload(DownloadMaterialParametrosData parametros)
        {
            var dic = MaterialDomainService.ListarMateriaisParaDownload(parametros.Transform<DownloadMaterialVO>());
            List<MaterialData> retorno = new List<MaterialData>();

            Random rnd = new Random();
            foreach (var item in dic)
            {
                MaterialData pai = new MaterialData()
                {
                    Seq = rnd.Next(((int)item.Value.Max(a => a.Seq) + 1), int.MaxValue),
                    TipoMaterial = TipoMaterial.Nenhum, //proposital, para não mostrar check na tela.
                    SeqOrigemMaterial = item.Key //Fazer o cabeçalho na raiz da árvore.
                };

                List<MaterialData> materiais = item.Value.TransformArrayToList<MaterialData>();
                foreach (var material in materiais.Where(a => a.SeqSuperior == null))
                {
                    material.SeqSuperior = pai.Seq;
                }
                materiais.Add(pai);
                retorno.AddRange(materiais);
            }
            return retorno?.OrderBy(x => x.DescricaoOrigem).ThenBy(x => x.Descricao).ToArray();
        }

        /// <summary>
        /// Busca o sequencial de um arquivo anexado com base no sequencial do material.
        /// </summary>
        /// <param name="seqMaterial">Sequencial do materuial.</param>
        /// <returns>Sequencial do arquivo anexado.</returns>
        public long BuscarSequencialArquivoAnexado(long seqMaterial)
        {
            return MaterialDomainService.BuscarSequencialArquivoAnexado(seqMaterial);
        }

        /// <summary>
        /// Busca o uid de um arquivo anexado com base no sequencial do material.
        /// </summary>
        /// <param name="seqMaterial">Sequencial do material.</param>
        /// <returns>Uid do arquivo anexado.</returns>
        public Guid BuscarUidArquivoAnexado(long seqMaterial)
        {
            return MaterialDomainService.BuscarUidArquivoAnexado(seqMaterial);
        }

        /// <summary>
        /// Gera um arquivo .zip para download.
        /// </summary>
        /// <param name="seqsMateriais">Sequenciais dos materiais do tipo arquivo para extrair os arquivos e gerar o arquivo .zip.</param>
        /// <returns>Arquivo .zip dos materiais em questão para download.</returns>
        public SMCFile DownloadMateriais(List<long> seqsMateriais)
        {
            return MaterialDomainService.DownloadMateriais(seqsMateriais);
        }
    }
}
