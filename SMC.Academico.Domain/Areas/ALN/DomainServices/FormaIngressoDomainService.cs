using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ALN.DomainServices
{
    public class FormaIngressoDomainService : AcademicoContextDomain<FormaIngresso>
    {
        #region [ DomainService ]

        private InstituicaoNivelFormaIngressoDomainService InstituicaoNivelFormaIngressoDomainService { get => Create<InstituicaoNivelFormaIngressoDomainService>(); }

        private ProcessoSeletivoDomainService ProcessoSeletivoDomainService { get => Create<ProcessoSeletivoDomainService>(); }

        #endregion [ DomainService ]

        /// <summary>
        /// Recupera todas formas de ingresso associadas a algum tipo de vínculo de aluno da instituição
        /// </summary>
        /// <returns>Formas de vínculo associadas ordenadas por descrição</returns>
        public List<SMCDatasourceItem> BuscarFormasIngressoInstituicaoNivelVinculoSelect(FormaIngressoFiltroVO filtroVO)
        {
            var specFormaIngresso = filtroVO.Transform<InstituicaoNivelFormaIngressoFilterSpecification>();
            specFormaIngresso.SetOrderBy(o => o.FormaIngresso.Descricao);

            if (filtroVO.SeqProcessoSeletivo.HasValue)
            {
                var specProcesso = new SMCSeqSpecification<ProcessoSeletivo>(filtroVO.SeqProcessoSeletivo.Value);
                specFormaIngresso.SeqTipoProcessoSeletivo = ProcessoSeletivoDomainService.SearchProjectionByKey(specProcesso, p => p.SeqTipoProcessoSeletivo);
            }

            return InstituicaoNivelFormaIngressoDomainService.SearchProjectionBySpecification(specFormaIngresso, p => new
            {
                p.SeqFormaIngresso,
                p.FormaIngresso.Descricao,
                p.FormaIngresso.Token
            }, isDistinct: true)
                // O distinct estava conflitando com o sub select de DataAttributes
                .Select(s => new SMCDatasourceItem()
                {
                    Seq = s.SeqFormaIngresso,
                    Descricao = s.Descricao,
                    DataAttributes = new List<SMCKeyValuePair>()
                    {
                        new SMCKeyValuePair() {Key = "data-transferencia", Value = s.Token == FormaIngressoTokens.TRANSFERENCIA_EXTERNA ? "true" : "false" }
                    }
                }).OrderBy(o => o.Descricao).ToList();
        }
    }
}