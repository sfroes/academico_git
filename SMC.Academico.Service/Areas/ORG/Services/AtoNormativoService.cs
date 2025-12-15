using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.ValueObjects;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;

namespace SMC.Academico.Service.Areas.ORG.Services
{
    public class AtoNormativoService : SMCServiceBase, IAtoNormativoService
    {
        #region [Dominio]

        private AtoNormativoDomainService AtoNormativoDomainService => this.Create<AtoNormativoDomainService>();
        private AtoNormativoEntidadeDomainService AtoNormativoEntidadeDomainService => this.Create<AtoNormativoEntidadeDomainService>();

        #endregion

        public SMCPagerData<AtoNormativoListarData> BuscarAtosNormativos(AtoNormativoFiltroData filtros)
        {
            var result = AtoNormativoDomainService.BuscarAtosNormativos(filtros.Transform<AtoNormativoFiltroVO>());
            return new SMCPagerData<AtoNormativoListarData>(result.TransformList<AtoNormativoListarData>(), result.Total);
        }

        public AtoNormativoData BuscarAtoNormativo(long seq)
        {
            return AtoNormativoDomainService.BuscarAtoNormativo(seq).Transform<AtoNormativoData>();
        }

        public long SalvarAtoNormativo(AtoNormativoData modelo)
        {
            return AtoNormativoDomainService.SalvarAtoNormativo(modelo.Transform<AtoNormativoVO>());
        }

        public SMCPagerData<AssociacaoEntidadeListarData> BuscarAssociacoesEntidades(AssociacaoEntidadeFiltroData filtros)
        {
            var result = AtoNormativoEntidadeDomainService.BuscarAtoNormativoEntidades(filtros.Transform<AssociacaoEntidadeFiltroVO>());
            return new SMCPagerData<AssociacaoEntidadeListarData>(result.TransformList<AssociacaoEntidadeListarData>(), result.Total);
        }

        public AssociacaoEntidadeData BuscarAssociacaoEntidades(long seq)
        {
            return AtoNormativoEntidadeDomainService.BuscarAtoNormativoEntidade(seq).Transform<AssociacaoEntidadeData>();
        }

        public long SalvarAssociacaoEntidades(AssociacaoEntidadeData modelo)
        {
            return AtoNormativoEntidadeDomainService.SalvarAtoNormativoEntidade(modelo.Transform<AssociacaoEntidadeVO>());
        }

        public void ExcluirAssociacaoEntidade(long seq)
        {
            AtoNormativoEntidadeDomainService.ExcluirAtoNormativoEntidade(seq);
        }

        public DadosAtoNormativoData BuscarUltimoAtoNormativoVigente(long seqEntidade)
        {
            return AtoNormativoEntidadeDomainService.BuscarUltimoAtoNormativoVigente(seqEntidade).Transform<DadosAtoNormativoData>();
        }
    }
}
