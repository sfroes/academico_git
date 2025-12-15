using SMC.Academico.Domain.Areas.CNC.ValueObjects;
using SMC.Academico.Domain.Areas.CUR.Models;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CUR.DomainServices
{
    public class TipoComponenteCurricularDomainService : AcademicoContextDomain<TipoComponenteCurricular>
    {
        /// <summary>
        /// Busca todos os tipos de componente para legenda da integralização
        /// </summary>
        /// <returns>Lista com os tipos de componentes curriculares</returns>
        public List<IntegralizacaoTipoComponenteCurricularVO> BuscarTiposComponentesCurricularesIntegralizacao()
        {
            var registros = this.SearchProjectionAll(s => new IntegralizacaoTipoComponenteCurricularVO()
            {
                SeqTipoComponenteCurricular = s.Seq,
                Descricao = s.Descricao,
                Sigla = s.Sigla
            }).ToList();

            return registros;
        }
    }
}
