using SMC.Academico.Common.Areas.PES.Includes;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.PES.DomainServices
{
    public class TipoMensagemDomainService : AcademicoContextDomain<TipoMensagem>
    {
        public SMCPagerData<TipoMensagemListaVO> ListarTipoMensagem(TipoMensagemFiltroVO filtro)
        {
            var spec = filtro.Transform<TipoMensagemFilterSpecification>(filtro);
            spec.SetOrderBy(t => t.Descricao);

            var tipoMensagens = SearchBySpecification(spec, IncludesTipoMensagem.TiposAtuacao | IncludesTipoMensagem.TiposUso | IncludesTipoMensagem.Tags);

            var lista = tipoMensagens.TransformList<TipoMensagemListaVO>();

            return new SMCPagerData<TipoMensagemListaVO>(lista, lista.Count);
        }

        public long SalvarTipoMensagem(TipoMensagem dominio)
        {
            if (dominio.PermiteCadastroManual)
            {
                dominio.Tags = new List<TipoMensagemTag>();
            }

            if (dominio.CategoriaMensagem != Common.Areas.PES.Enums.CategoriaMensagem.Documento)
            {
                dominio.TiposUso = new List<TipoMensagemTipoUso>();
            }

            SaveEntity(dominio);
            return dominio.Seq;
        }

        /// <summary>
        /// Buscar tipo de mensagem
        /// </summary>
        /// <param name="seq">Seuencial do tipo de mensagem</param>
        /// <returns></returns>
        public TipoMensagem BuscarTipoMensagem(long seq)
        {
            return this.SearchByKey(seq);
        }
    }
}
