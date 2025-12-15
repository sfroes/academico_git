using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Academico.ServiceContract.Areas.CUR.Data.Periodico;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.CUR.Services
{
    public class PeriodicoService : SMCServiceBase, IPeriodicoService
    {
        #region Seriços

        private PeriodicoDomainService PeriodicoDomainService
        {
            get { return this.Create<PeriodicoDomainService>(); }
        }

        private ClassificacaoPeriodicoDomainService ClassificacaoPeriodicoDomainService => Create<ClassificacaoPeriodicoDomainService>();

        private QualisPeriodicoDomainService QualisPeriodicoDomainService => Create<QualisPeriodicoDomainService>();

        #endregion Seriços

        public List<SMCDatasourceItem> BuscarAreaAvaliacaoSelect(long seqClassificacaoPeriodico)
        {
            return PeriodicoDomainService.BuscarAreaAvaliacaoSelect(seqClassificacaoPeriodico);
        }

        public List<SMCDatasourceItem> BuscarQualiCapesSelect(long seqClassificacaoPeriodico)
        {
            return PeriodicoDomainService.BuscarQualiCapesSelect(seqClassificacaoPeriodico);
        }

        public void SalvarPeriodo(PeriodicoData periodico)
        {
            PeriodicoDomainService.SalvarPeriodo(periodico.Transform<PeriodicoVO>());
        }

        public SMCPagerData<PeriodicoListarData> BuscarPeriodicosCapes(PeriodicoFiltroData filtro)
        {
            var spec = filtro.Transform<PeriodicoFilterSpecification>();

            return PeriodicoDomainService.BuscarPeriodicosCapes(spec).Transform<SMCPagerData<PeriodicoListarData>>();
        }

        public SMCPagerData<PeriodicoListarLookupData> BuscarPeriodicosLookup(PeriodicoFiltroData filtro)
        {
            var spec = filtro.Transform<QualisPeriodicoFilterSpecification>();
            var lista = QualisPeriodicoDomainService.SearchProjectionBySpecification(spec, x => new PeriodicoListarLookupData
            {
                Seq = x.Seq,
                CodigoISSN = x.CodigoISSN,
                Descricao = x.Periodico.Descricao,
                DescricaoAreaAvaliacao = x.DescricaoAreaAvaliacao,
                QualisCapes = x.QualisCapes
            }, out int total);
            return new SMCPagerData<PeriodicoListarLookupData>(lista, total);
        }

        public PeriodicoListarLookupData BuscarPeriodicoLookup(long seq)
        {
            return QualisPeriodicoDomainService.SearchProjectionByKey(new SMCSeqSpecification<QualisPeriodico>(seq),
                                                    x => new PeriodicoListarLookupData
                                                    {
                                                        Seq = x.Seq,
                                                        Descricao = x.Periodico.Descricao,
                                                        DescricaoAreaAvaliacao = x.DescricaoAreaAvaliacao,
                                                        QualisCapes = x.QualisCapes
                                                    });
        }

        public PeriodicoData PrepararModeloPeriodico(PeriodicoFiltroData filtro)
        {
            var classificacaoPeriodico = this.ClassificacaoPeriodicoDomainService.BuscarClassificacaoPeriodicoAtual();

            return new PeriodicoData()
            {
                SeqClassificacaoPeriodico = classificacaoPeriodico.Seq,
                DescricaoClassificacaoPeriodico = classificacaoPeriodico.Descricao
            };
        }
    }
}