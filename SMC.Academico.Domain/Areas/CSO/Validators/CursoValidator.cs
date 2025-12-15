using SMC.Academico.Common.Areas.CSO.Enums;
using SMC.Academico.Common.Areas.ORG.Includes;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CSO.Resources;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Framework.Specification;
using SMC.Framework.Util;
using SMC.Framework.Validation;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CSO.Validators
{
    public class CursoValidator : SMCValidator<Curso>
    {
        //FIX: Mover as valicdações para o domain service. Da forma como está, ocorre acesso a banco de dados do validator.
        protected override void DoValidate(Curso curso, SMCValidationResults validationResults)
        {
            base.DoValidate(curso, validationResults);

            if (curso == null)
                return;

            HierarquiaEntidadeItemFilterSpecification specCurso;
            List<HierarquiaEntidadeItem> itemCurso = new List<HierarquiaEntidadeItem>();
            CursoFormacaoEspecificaFilterSpecification specFormacao;
            List<CursoFormacaoEspecifica> itemCursoFormacao = new List<CursoFormacaoEspecifica>();

            if (curso.Seq > 0)
            {
                specCurso = new HierarquiaEntidadeItemFilterSpecification() { SeqEntidade = curso.Seq };
                itemCurso = new HierarquiaEntidadeItemDomainService().SearchBySpecification(specCurso, IncludesHierarquiaEntidadeItem.Entidade).ToList();

                specFormacao = new CursoFormacaoEspecificaFilterSpecification() { SeqCurso = curso.Seq };
                itemCursoFormacao = new CursoFormacaoEspecificaDomainService().SearchBySpecification(specFormacao).ToList();
            }

            foreach (var itemHierarquia in curso.HierarquiasEntidades)
            {
                var specSuperior = new HierarquiaEntidadeItemFilterSpecification() { Seq = itemHierarquia.SeqItemSuperior };
                var itemHierarquiaSuperior = new HierarquiaEntidadeItemDomainService().SearchByKey(specSuperior, IncludesHierarquiaEntidadeItem.Entidade);

                if (curso.Seq > 0
                 && itemCurso.Count(c => c.SeqItemSuperior == itemHierarquia.SeqItemSuperior) == 0
                 && itemCursoFormacao.Count > 0)
                {
                    this.AddPropertyError(p => p.HierarquiasEntidades, MessagesResource.MSG_AlterarEntidadeCursoFormacaoErro);
                    return;
                }

                var valorTipo = itemHierarquiaSuperior.Entidade.SeqTipoEntidade;
                var tipoPrograma = (itemHierarquiaSuperior.Entidade as Programa)?.TipoPrograma;
                if (tipoPrograma != null)
                    curso.NivelEnsino = new NivelEnsinoDomainService().SearchByKey(new SMCSeqSpecification<NivelEnsino>(curso.SeqNivelEnsino));

                if (valorTipo != itemHierarquiaSuperior.Entidade.SeqTipoEntidade || tipoPrograma != (itemHierarquiaSuperior.Entidade as Programa)?.TipoPrograma)
                {
                    this.AddPropertyError(p => p.HierarquiasEntidades, MessagesResource.MSG_EntidadeResponsavelTipoDiferente);
                    return;
                }

                switch (tipoPrograma)
                {
                    case TipoPrograma.Academico:

                        if (!(curso.NivelEnsino.Token == TOKEN_NIVEL_ENSINO.MESTRADO_ACADEMICO || curso.NivelEnsino.Token == TOKEN_NIVEL_ENSINO.DOUTORADO_ACADEMICO))
                        {
                            this.AddPropertyError(p => p.NivelEnsino,
                                                  string.Format(MessagesResource.MSG_ImpossivelAssociarCusroEntidade,
                                                  curso.NivelEnsino.Descricao, curso.Nome, itemHierarquiaSuperior.Entidade.Nome, SMCEnumHelper.GetDescription(tipoPrograma)));
                            return;
                        }
                        break;

                    case TipoPrograma.Profissional:

                        if (!(curso.NivelEnsino.Token == TOKEN_NIVEL_ENSINO.MESTRADO_PROFISSIONAL || curso.NivelEnsino.Token == TOKEN_NIVEL_ENSINO.DOUTORADO_PROFISSIONAL))
                        {
                            this.AddPropertyError(p => p.NivelEnsino,
                                                  string.Format(MessagesResource.MSG_ImpossivelAssociarCusroEntidade,
                                                  curso.NivelEnsino.Descricao, curso.Nome, itemHierarquiaSuperior.Entidade.Nome, SMCEnumHelper.GetDescription(tipoPrograma)));
                            return;
                        }

                        break;
                }
            }
        }
    }
}