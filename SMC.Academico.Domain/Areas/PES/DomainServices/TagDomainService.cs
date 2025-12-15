using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Domain.Areas.CNC.DomainServices;
using SMC.Academico.Domain.Areas.CNC.Specifications;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Framework.Extensions;
using SMC.Framework.Specification;
using System.Linq;

namespace SMC.Academico.Domain.Areas.PES.DomainServices
{
    public class TagDomainService : AcademicoContextDomain<Tag>
    {
        #region DomainServices

        private TipoDocumentoAcademicoTagDomainService TipoDocumentoAcademicoTagDomainService => Create<TipoDocumentoAcademicoTagDomainService>();

        #endregion

        public TagVO BuscarTag(long seq)
        {
            var tag = this.SearchByKey(new SMCSeqSpecification<Tag>(seq)).Transform<TagVO>();

            tag.HabilitarTipoPreenchimentoTag = tag.TipoTag == TipoTag.Mensagem;
            tag.HabilitarQueryOrigem = tag.TipoTag == TipoTag.DeclaracaoGenerica && tag.TipoPreenchimentoTag == TipoPreenchimentoTag.Automatico;

            return tag;
        }

        public long SalvarTag(TagVO tagVO)
        {
            var tag = tagVO.Transform<Tag>();

            if (tag.Seq != 0 && tag.TipoTag == TipoTag.DeclaracaoGenerica)
            {
                var tiposDocumentoConclusaoTag = TipoDocumentoAcademicoTagDomainService.SearchBySpecification(new TipoDocumentoAcademicoTagFilterSpecification() { SeqTag = tag.Seq }).ToList();
                foreach (var tipoDocumentoAcademicoTag in tiposDocumentoConclusaoTag)
                    if (tipoDocumentoAcademicoTag.PermiteEditarDado.HasValue && !tipoDocumentoAcademicoTag.PermiteEditarDado.Value)
                    {
                        tipoDocumentoAcademicoTag.PermiteEditarDado = true;
                        TipoDocumentoAcademicoTagDomainService.UpdateFields(tipoDocumentoAcademicoTag, p => p.PermiteEditarDado);
                    }
            }

            SaveEntity(tag);
            return tag.Seq;
        }

        public bool ExibirMensagem(TagVO tag)
        {
            var retorno = false;

            if (tag.Seq != 0 && tag.TipoTag == TipoTag.DeclaracaoGenerica)
            {
                var tagBanco = BuscarTag(tag.Seq);

                if (tag.MensagemAutomaticoParaManual &&
                    tagBanco.TipoPreenchimentoTag != tag.TipoPreenchimentoTag &&
                    tag.TipoPreenchimentoTag == TipoPreenchimentoTag.Manual)
                {
                    retorno = true;
                }

                if (tag.MensagemManualParaAutomatico &&
                    tagBanco.TipoPreenchimentoTag != tag.TipoPreenchimentoTag &&
                    tag.TipoPreenchimentoTag == TipoPreenchimentoTag.Automatico)
                {
                    retorno = true;
                }
            }

            return retorno;
        }
    }
}
