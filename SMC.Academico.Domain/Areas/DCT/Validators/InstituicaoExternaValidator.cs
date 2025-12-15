using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.Domain.Areas.DCT.Models;
using SMC.Academico.Domain.Areas.DCT.Resources;
using SMC.Framework.Validation;
using SMC.Localidades.Common.Constants;

namespace SMC.Academico.Domain.Areas.DCT.Validators
{
    public class InstituicaoExternaValidator : SMCValidator<InstituicaoExterna>
    {
        protected override void DoValidate(InstituicaoExterna item, SMCValidationResults validationResults)
        {
            base.DoValidate(item, validationResults);

            if (item == null)
                return;

            if (item.EhInstituicaoEnsino && item.CodigoPais == LocalidadesDefaultValues.SEQ_PAIS_BRASIL)
            {
                if (!item.SeqCategoriaInstituicaoEnsino.HasValue || item.SeqCategoriaInstituicaoEnsino.Value == 0)
                    this.AddPropertyError(nameof(InstituicaoExterna.SeqCategoriaInstituicaoEnsino), MessagesResource.ERR_InstituicaoExternaCategoriaNaoInformada);

                if (item.TipoInstituicaoEnsino == TipoInstituicaoEnsino.Nenhum)
                    this.AddPropertyError(nameof(InstituicaoExterna.TipoInstituicaoEnsino), MessagesResource.ERR_InstituicaoExternaTipoNaoInformado);
            }
        }
    }
}