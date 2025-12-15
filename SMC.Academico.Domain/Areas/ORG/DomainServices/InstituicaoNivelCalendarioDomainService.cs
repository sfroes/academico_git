using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Common.Areas.ORG.Includes;
using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.Domain.Areas.ORT.DomainServices;
using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Calendarios.ServiceContract.Areas.CLD.Data;
using SMC.Calendarios.ServiceContract.Areas.CLD.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ORG.DomainServices
{
    public class InstituicaoNivelCalendarioDomainService : AcademicoContextDomain<InstituicaoNivelCalendario>
    {

        #region [ DomainServices ]

        private InstituicaoNivelDomainService InstituicaoNivelDomainService { get => Create<InstituicaoNivelDomainService>(); }

        private TrabalhoAcademicoDomainService TrabalhoAcademicoDomainService { get => Create<TrabalhoAcademicoDomainService>(); }

        private InstituicaoNivelTipoDivisaoComponenteDomainService InstituicaoNivelTipoDivisaoComponenteDomainService { get => Create<InstituicaoNivelTipoDivisaoComponenteDomainService>(); }

        #endregion [ DomainServices ]

        #region [ Services ]

        private ITipoEventoService TipoEventoService { get => Create<ITipoEventoService>(); }

        #endregion [ Services ]

        public InstituicaoNivelCalendario BuscarInstituicaoNivelCalendario(long seqInstituicaoNivelCalendario, IncludesInstituicaoNivelCalendario includes)
        {
            return this.SearchByKey(new SMCSeqSpecification<InstituicaoNivelCalendario>(seqInstituicaoNivelCalendario), includes);
        }

        public SMCPagerData<InstituicaoNivelCalendario> BuscarListaInstituicaoNivelCalendario(InstituicaoNivelCalendarioSpecification filtros, IncludesInstituicaoNivelCalendario includes)
        {
            var lista = this.SearchBySpecification(filtros, includes).OrderBy(c => c.InstituicaoNivel.NivelEnsino.Descricao);
            return new SMCPagerData<InstituicaoNivelCalendario>(lista, lista.Count());
        }

        public List<SMCDatasourceItem> BuscarTiposEventosCalendario(List<long> seqsNivelEnsino)
        {

            List<SMCDatasourceItem> retorno = new List<SMCDatasourceItem>();
            if (!seqsNivelEnsino.SMCAny()) { return retorno; }

            var spec = new InstituicaoNivelCalendarioSpecification()
            {
                UsoCalendario = UsoCalendario.BancaExaminadora,
                SeqsNivelEnsino = seqsNivelEnsino,
                TipoAvaliacao = TipoAvaliacao.Banca
            };

            var lista = SearchProjectionByKey(spec, x => x.TiposEvento.Select(s => new
            {
                s.Seq,
                s.SeqTipoEventoAgd
            }));

            if (lista == null)
                return retorno;

            var tiposEventos = TipoEventoService.BuscarTiposEventos(new TipoEventoFiltroData()
            {
                Seqs = lista.Select(f => f.SeqTipoEventoAgd).ToList()
            });

            foreach (var item in lista)
            {
                retorno.Add(new SMCDatasourceItem()
                {
                    Seq = item.SeqTipoEventoAgd,
                    Descricao = tiposEventos.FirstOrDefault(f => f.Seq == item.SeqTipoEventoAgd).Descricao
                });
            }

            return retorno;
        }

        public List<SMCDatasourceItem> BuscarTiposEventosTrabalhoAcademico(long? seqTrabalhoAcademico, long? seqOrigemAvaliacao)
        {

            List<SMCDatasourceItem> retorno = new List<SMCDatasourceItem>();

            if (!seqTrabalhoAcademico.HasValue) { return retorno; }

            var specTrabalhoAcademico = new SMCSeqSpecification<TrabalhoAcademico>(seqTrabalhoAcademico.Value);

            var dadosTrabalhioAcademico = TrabalhoAcademicoDomainService.SearchProjectionByKey(specTrabalhoAcademico, t => new
            {
                DivisaoComponente = t.DivisoesComponente
                                     .Where(d => d.SeqOrigemAvaliacao == seqOrigemAvaliacao)
                                     .Select(d => new
                                     {
                                         d.DivisaoComponente.SeqTipoDivisaoComponente,
                                         d.DivisaoComponente.ConfiguracaoComponente.ComponenteCurricular.SeqTipoComponenteCurricular
                                     }).FirstOrDefault(),
                t.SeqTipoTrabalho,
                t.SeqNivelEnsino
            });

            var specInstituicaoNivelTipoDivisaoComponente = new InstituicaoNivelTipoDivisaoComponenteFilterSpecification()
            {
                SeqTipoTrabalho = dadosTrabalhioAcademico.SeqTipoTrabalho,
                SeqTipoDivisaoComponente = dadosTrabalhioAcademico.DivisaoComponente.SeqTipoDivisaoComponente,
                SeqTipoComponenteCurricular = dadosTrabalhioAcademico.DivisaoComponente.SeqTipoComponenteCurricular,
                SeqNivelEnsino = dadosTrabalhioAcademico.SeqNivelEnsino
            };

            var seqsTiposEventosAgd = InstituicaoNivelTipoDivisaoComponenteDomainService.SearchProjectionBySpecification(specInstituicaoNivelTipoDivisaoComponente, i => i.SeqTipoEventoAgd).Where(i => i.HasValue).Select(i=>(long)i).ToList();
            
            if (!seqsTiposEventosAgd.SMCAny()) { return retorno; }

            var tiposEventos = TipoEventoService.BuscarTiposEventos(new TipoEventoFiltroData()
            {
                Seqs = seqsTiposEventosAgd
            });

            foreach (var seqTipoEventoAgd in seqsTiposEventosAgd)
            {
                retorno.Add(new SMCDatasourceItem()
                {
                    Seq = seqTipoEventoAgd,
                    Descricao = tiposEventos.FirstOrDefault(f => f.Seq == seqTipoEventoAgd).Descricao
                });
            }

            return retorno;
        }

        /// <summary>
        /// Buscar tipos de envento por intituicao nivel
        /// </summary>
        /// <param name="seqInstituicaoNivel">Sequencial instituição nivel</param>
        /// <returns>Lista baseado na instituicao nivel para select</returns>
        public List<SMCDatasourceItem> BuscarTiposEventosCalendarioInstituicaoNivel(long seqInstituicaoNivel)
        {
            long seqNivelEnsino = this.InstituicaoNivelDomainService.BuscarSequencialNivelEnsino(seqInstituicaoNivel);

            var retorno = this.BuscarTiposEventosCalendario(new List<long>() { seqNivelEnsino });

            return retorno;

        }

        #region [ Método para Teste, Temporário ]

        /// <summary>
        /// Método para Testes
        /// </summary>
        /// <param name="seqNivelEnsino"></param>
        /// <returns></returns>
        public List<SMCDatasourceItem> BuscarTiposEventosCalendarioTeste(long seqNivelEnsino)
        {
            List<SMCDatasourceItem> retorno = new List<SMCDatasourceItem>();
            if (seqNivelEnsino <= 0) { return retorno; }

            var spec = new InstituicaoNivelCalendarioSpecification() { };

            var lista = SearchProjectionByKey(spec, x => x.TiposEvento.Select(s => new
            {
                s.Seq,
                s.SeqTipoEventoAgd
            }));

            if (lista == null)
                return retorno;

            var tiposEventos = TipoEventoService.BuscarTiposEventos(new TipoEventoFiltroData()
            {
                Seqs = lista.Select(f => f.SeqTipoEventoAgd).ToList()
            });

            ///Temporário testes
            for (int i = 0; i < tiposEventos.Itens.Count(); i++)
            {
                retorno.Add(new SMCDatasourceItem()
                {
                    Seq = tiposEventos.ElementAt(i).Seq,
                    Descricao = tiposEventos.ElementAt(i).Descricao
                });
            }

            return retorno;
        }

        #endregion [ Método para Teste, Temporário ]
    }
}