using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.ServiceContract.Areas.ALN.Data;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Data;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.ALN.Services
{
    public class FormaIngressoService : SMCServiceBase, IFormaIngressoService
    {
        private FormaIngressoDomainService FormaIngressoDomainService => Create<FormaIngressoDomainService>();

        public List<SMCDatasourceItem> BuscarFormasIngressoSelect(FormaIngressoFiltroData filtro)
        {
            var spec = new FormaIngressoSpecification(filtro.SeqTipoVinculoAluno.Value);
            spec.SetOrderBy(x => x.Descricao);
            return FormaIngressoDomainService.SearchProjectionBySpecification(spec, x => new SMCDatasourceItem
            {
                Seq = x.Seq,
                Descricao = x.Descricao
            }).ToList();
        }

        public FormaIngressoData BuscarFormaIngresso(long seq)
        {
            var obj = FormaIngressoDomainService.SearchByKey(new SMCSeqSpecification<FormaIngresso>(seq));

            return obj.Transform<FormaIngressoData>();
        }

        /// <summary>
        /// Recupera todas formas de ingresso associadas a algum tipo de vínculo de aluno da instituição
        /// </summary>
        /// <param name="filtro">Dados do filtro</param>
        /// <returns>Formas de vínculo associadas ordenadas por descrição</returns>
        public List<SMCDatasourceItem> BuscarFormasIngressoInstituicaoNivelVinculoSelect(FormaIngressoFiltroData filtro)
        {
            return FormaIngressoDomainService.BuscarFormasIngressoInstituicaoNivelVinculoSelect(filtro.Transform<FormaIngressoFiltroVO>());
        }
    }
}