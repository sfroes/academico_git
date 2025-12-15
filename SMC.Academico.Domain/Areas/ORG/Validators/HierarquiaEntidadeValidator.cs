using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Resources;
using SMC.Framework.Validation;
using System;

namespace SMC.Academico.Domain.Areas.ORG.Validators
{
    public class HierarquiaEntidadeValidator : SMCValidator<HierarquiaEntidade>
    {
        protected override void DoValidate(HierarquiaEntidade item, SMCValidationResults validationResults)
        {
            base.DoValidate(item, validationResults);

            if (item == null) return;

            if ((item.DataInicioVigencia != null && item.DataFimVigencia.HasValue) && (item.DataInicioVigencia > item.DataFimVigencia))
                this.AddPropertyError(p => p.DataInicioVigencia, String.Format(MessagesResource.MSG_DataVigenciaInicialMaiorQueFinal, item.DataInicioVigencia.ToShortDateString(), item.DataFimVigencia.Value.ToShortDateString()));

        }

        public override void Configure()
        {
            this.Property(p => p.Descricao).HasMaxLength(100);
            base.Configure();
        }
    }
}
