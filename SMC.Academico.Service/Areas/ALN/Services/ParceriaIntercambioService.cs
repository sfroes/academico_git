using SMC.Academico.Common.Areas.ALN.Includes;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.ServiceContract.Areas.ALN.Data;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using System.Linq;

namespace SMC.Academico.Service.Areas.ALN.Services
{
    public class ParceriaIntercambioService : SMCServiceBase, IParceriaIntercambioService
    {
        #region DomainService

        private ParceriaIntercambioDomainService ParceriaIntercambioDomainService
        {
            get { return this.Create<ParceriaIntercambioDomainService>(); }
        }

        private TermoIntercambioDomainService TermoIntercambioDomainService
        {
            get { return this.Create<TermoIntercambioDomainService>(); }
        }

        private ParceriaIntercambioInstituicaoExternaDomainService ParceriaIntercambioInstituicaoExternaDomainService
        {
            get { return this.Create<ParceriaIntercambioInstituicaoExternaDomainService>(); }
        }

        #endregion DomainService

        public ParceriaIntercambioData AlterarParceriaIntercambio(long seq)
        {
            var result = ParceriaIntercambioDomainService.SearchByKey(new SMCSeqSpecification<ParceriaIntercambio>(seq),
                                IncludesParceriaIntercambio.Arquivos
                                | IncludesParceriaIntercambio.TiposTermo
                                | IncludesParceriaIntercambio.Vigencias
                                | IncludesParceriaIntercambio.Arquivos_ArquivoAnexado
                                | IncludesParceriaIntercambio.InstituicoesExternas
                                | IncludesParceriaIntercambio.InstituicoesExternas_InstituicaoExterna);

            var data = result.Transform<ParceriaIntercambioData>();

            if (result.Arquivos.Count() > 0)
            {
                data.Arquivos.ForEach(f => f.ArquivoAnexado.GuidFile = result.Arquivos
                                                                             .Where(w => w.SeqArquivoAnexado == f.SeqArquivoAnexado)
                                                                             .Select(s => s.ArquivoAnexado.UidArquivo).First().ToString());
            }

            //NV06 - Só é possível alterar o registro de vigência mais atual, exibir os demais registros desabilitados e sem possibilidade de exclusão.
            //Apenas o registro atual pode ser alterado e excluído.
            //if (data.Vigencias != null && data.Vigencias.Count > 0)
            //{
            //    var listaVigencias = data.Vigencias.OrderByDescending(a => a.Seq).ToList();
            //    data.Vigencias.Clear();
            //    data.Vigencias.AddRange(listaVigencias);
            //}

            return data;
        }

        public long SalvarParceriaIntercambio(ParceriaIntercambioData modelo)
        {
            ParceriaIntercambio obj = modelo.Transform<ParceriaIntercambio>();
            if (modelo.Seq > default(long))
            {
                obj.Vigencias = modelo.Vigencias.TransformList<ParceriaIntercambioVigencia>();
            }

            //O lookup retorna o seq da instituição externa no campo seq, ao invés do seqInstituicaoExterna.
            foreach (var instituicaoExterna in obj.InstituicoesExternas)
            {
                if (instituicaoExterna.SeqInstituicaoExterna == default(long))
                {
                    instituicaoExterna.SeqInstituicaoExterna = instituicaoExterna.Seq;
                }
            }
            return ParceriaIntercambioDomainService.SalvarParceriaIntercambio(obj);
        }

        /// <summary>
        /// Exlcuir parceria de intercambio
        /// </summary>
        /// <param name="seq">Sequencial da parceria de intercambio</param>
        public void ExcluirParceria(long seq)
        {
            this.ParceriaIntercambioDomainService.ExcluirParceria(seq);
        }

        public SMCPagerData<ParceriaIntercambioListarData> ListarParceriaIntercambio(ParceriaIntercambioFiltroData filtro)
        {
            var list = ParceriaIntercambioDomainService.ListarParceriaIntercambio(filtro.Transform<ParceriaIntercambioFiltroVO>());
            var result = list.TransformList<ParceriaIntercambioListarData>();
            return new SMCPagerData<ParceriaIntercambioListarData>(result, list.Total);
        }

        public ParceriaIntercambioData BuscarParceriaIntercambio(long seqParceriaIntercambio)
        {
            return ParceriaIntercambioDomainService.SearchByKey(new SMCSeqSpecification<ParceriaIntercambio>(seqParceriaIntercambio)).Transform<ParceriaIntercambioData>();
        }
    }
}