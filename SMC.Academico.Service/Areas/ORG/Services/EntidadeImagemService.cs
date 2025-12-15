using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;

namespace SMC.Academico.Service.Areas.ORG.Services
{
    public class EntidadeImagemService : SMCServiceBase, IEntidadeImagemService
    {

        #region DomainServices

        private EntidadeImagemDomainService EntidadeImagemDomainService
        {
            get { return this.Create<EntidadeImagemDomainService>(); }
        }

        #endregion

        public EntidadeImagemData BuscarEntidadeImagem(long seq)
        {
            return EntidadeImagemDomainService.BuscarEntidadeImagem(seq).Transform<EntidadeImagemData>();
        }

        public SMCPagerData<EntidadeImagemData> BuscarEntidadeImagens(EntidadeImagemFiltroData filtro)
        {
            var spec = filtro.Transform<EntidadeImagemFilterSpecification>();
            var lista = EntidadeImagemDomainService.BuscarEntidadeImagens(spec);
            return lista.Transform<SMCPagerData<EntidadeImagemData>>();
        }

        public SMCUploadFile BuscarImagemEntidade(long seqEntidade, TipoImagem tipoImagem)
        {
            return EntidadeImagemDomainService.BuscarImagemEntidade(seqEntidade, tipoImagem);
        }

        public long SalvarEntidadeImagem(EntidadeImagemData entidadeImagemData)
        {
            var entidadeImagem = entidadeImagemData.Transform<EntidadeImagem>();

            return EntidadeImagemDomainService.SalvarEntidadeImagem(entidadeImagem);
        }

        public void ExcluirEntidadeImagem(long seq)
        {
            try
            {
                EntidadeImagemDomainService.DisableFilter(FILTER.INSTITUICAO_ENSINO);
                EntidadeImagemDomainService.DeleteEntity(seq);
            }
            finally
            {
                EntidadeImagemDomainService.EnableFilter(FILTER.INSTITUICAO_ENSINO);
            }
        }
    }
}
