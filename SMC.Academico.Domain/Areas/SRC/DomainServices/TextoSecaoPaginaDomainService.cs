using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Common.Areas.SRC.Exceptions;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Framework.Extensions;
using SMC.Framework.Specification;

namespace SMC.Academico.Domain.Areas.SRC.DomainServices
{
    public class TextoSecaoPaginaDomainService : AcademicoContextDomain<TextoSecaoPagina>
    {
        public TextoSecaoPaginaVO BuscarTextoSecaoPagina(long seqTextoSecaoPagina)
        {
            var textoSecaoPagina = this.SearchByKey(new SMCSeqSpecification<TextoSecaoPagina>(seqTextoSecaoPagina), x => x.ConfiguracaoEtapaPagina.ConfiguracaoEtapa.ProcessoEtapa);

            var retorno = textoSecaoPagina.Transform<TextoSecaoPaginaVO>();

            var situacao = textoSecaoPagina.ConfiguracaoEtapaPagina.ConfiguracaoEtapa.ProcessoEtapa.SituacaoEtapa;
            if (situacao == SituacaoEtapa.Liberada || situacao == SituacaoEtapa.Encerrada)
                retorno.CamposReadyOnly = true;

            return retorno;
        }

        public long Salvar(TextoSecaoPaginaVO modelo)
        {
            ValidarModelo(modelo);

            var dominio = modelo.Transform<TextoSecaoPagina>();

            this.SaveEntity(dominio);

            return dominio.Seq;
        }

        private void ValidarModelo(TextoSecaoPaginaVO modelo)
        {
            var situacao = this.SearchProjectionByKey(new SMCSeqSpecification<TextoSecaoPagina>(modelo.Seq), x => x.ConfiguracaoEtapaPagina.ConfiguracaoEtapa.ProcessoEtapa.SituacaoEtapa);

            if (situacao == SituacaoEtapa.Liberada || situacao == SituacaoEtapa.Encerrada)
                throw new ConfiguracaoEtapaOperacaoNaoPermitidaException();
        }
    }
}