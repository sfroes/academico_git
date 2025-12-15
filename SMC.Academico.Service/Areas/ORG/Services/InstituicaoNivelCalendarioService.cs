using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Common.Areas.ORG.Includes;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.ORG.Services
{
    public class InstituicaoNivelCalendarioService : SMCServiceBase, IInstituicaoNivelCalendarioService
    {
        private ICalendarioService CalendarioService
        {
            get { return this.Create<ICalendarioService>(); }
        }

        private IEntidadeService EntidadeService
        {
            get { return this.Create<IEntidadeService>(); }
        }

        private InstituicaoNivelCalendarioDomainService InstituicaoNivelCalendarioDomainService
        {
            get { return this.Create<InstituicaoNivelCalendarioDomainService>(); }
        }

        public InstituicaoNivelCalendarioData BuscarInstituicaoNivelCalendario(long seq)
        {
            var instituicaoNivelCalendario = InstituicaoNivelCalendarioDomainService.BuscarInstituicaoNivelCalendario(seq,
                IncludesInstituicaoNivelCalendario.InstituicaoNivel_InstituicaoEnsino | IncludesInstituicaoNivelCalendario.TiposEvento);
            return instituicaoNivelCalendario.Transform<InstituicaoNivelCalendarioData>();
        }

        public SMCPagerData<InstituicaoNivelCalendarioListaData> BuscarListaInstituicaoNivelCalendario(InstituicaoNivelCalendarioFiltroData filtros)
        {
            var lista = InstituicaoNivelCalendarioDomainService.BuscarListaInstituicaoNivelCalendario(filtros.Transform<InstituicaoNivelCalendarioSpecification>(),
                IncludesInstituicaoNivelCalendario.InstituicaoNivel_InstituicaoEnsino | IncludesInstituicaoNivelCalendario.InstituicaoNivel_NivelEnsino);
            var listaData = lista.Transform<SMCPagerData<InstituicaoNivelCalendarioListaData>>();

            foreach (var item in listaData)
            {
                item.NomeCalendario = CalendarioService.BuscarNomeCalendario(item.SeqCalendarioAgd);
            }

            return listaData;
        }

        public List<SMCDatasourceItem> BuscarTiposAvaliacao(UsoCalendario usoCalendario)
        {
            List<SMCDatasourceItem> select = new List<SMCDatasourceItem>();

            if (usoCalendario == UsoCalendario.BancaExaminadora)
            {
                select.Add(new SMCDatasourceItem() { Seq = (short)TipoAvaliacao.Banca, Descricao = TipoAvaliacao.Banca.SMCGetDescription() });
            }
            else
            {
                select.Add(new SMCDatasourceItem() { Seq = (short)TipoAvaliacao.Prova, Descricao = TipoAvaliacao.Prova.SMCGetDescription() });
                select.Add(new SMCDatasourceItem() { Seq = (short)TipoAvaliacao.Reavaliacao, Descricao = TipoAvaliacao.Reavaliacao.SMCGetDescription() });
                select.Add(new SMCDatasourceItem() { Seq = (short)TipoAvaliacao.Trabalho, Descricao = TipoAvaliacao.Trabalho.SMCGetDescription() });
            }

            return select;
        }

        public List<SMCDatasourceItem> BuscarTiposEventosCalendario(List<long> seqsNivelEnsino)
        {
            return InstituicaoNivelCalendarioDomainService.BuscarTiposEventosCalendario(seqsNivelEnsino);
        }

        /// <summary>
        /// Buscar tipos de envento por intituicao nivel
        /// </summary>
        /// <param name="seqInstituicaoNivel">Sequencial instituição nivel</param>
        /// <returns>Lista baseado na instituicao nivel para select</returns>
        public List<SMCDatasourceItem> BuscarTiposEventosCalendarioInstituicaoNivel(long seqInstituicaoNivel)
        {
            return this.InstituicaoNivelCalendarioDomainService.BuscarTiposEventosCalendarioInstituicaoNivel(seqInstituicaoNivel);
        }

        public List<SMCDatasourceItem> BuscarTiposEventosTrabalhoAcademico(long? seqTrabalhoAcademico, long? seqOriemAvaliacao)
        {
            return InstituicaoNivelCalendarioDomainService.BuscarTiposEventosTrabalhoAcademico(seqTrabalhoAcademico, seqOriemAvaliacao);
        }

    }
}
