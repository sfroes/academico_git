using SMC.Academico.Common.Areas.Shared.Helpers;
using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.CUR.Resources;
using SMC.Framework;
using SMC.Framework.Validation;
using System;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CUR.Validators
{
    public class ComponenteCurricularValidator : SMCValidator<ComponenteCurricular>
    {
        protected override void DoValidate(ComponenteCurricular componenteCurricular, SMCValidationResults validationResults)
        {
            base.DoValidate(componenteCurricular, validationResults);

            if (componenteCurricular == null)
                return;

            ////Verifica alteração com divisões de matrizes associadas
            //if (componenteCurricular.Seq > 0)
            //{
            //    var includes = IncludesComponenteCurricular.TipoComponente
            //             | IncludesComponenteCurricular.Ementas
            //             | IncludesComponenteCurricular.NiveisEnsino_NivelEnsino
            //             | IncludesComponenteCurricular.EntidadesResponsaveis_Entidade
            //             | IncludesComponenteCurricular.Configuracoes;

            //    var registro = new ComponenteCurricularDomainService().SearchByKey(new SMCSeqSpecification<ComponenteCurricular>(componenteCurricular.Seq), includes);

            //    if (registro.Configuracoes.Where(w => w.DivisoesComponente.Count > 0).Count() > 0)
            //    {
            //        //RN_CUR_036 - Alteração componente curricular não permitida quando tem divisões
            //        if (!(
            //                   registro.Descricao == componenteCurricular.Descricao
            //                && registro.CargaHoraria == componenteCurricular.CargaHoraria
            //                && registro.Credito == componenteCurricular.Credito
            //                && registro.TipoOrganizacao == componenteCurricular.TipoOrganizacao
            //              )
            //          )
            //        {
            //            this.AddPropertyError(p => p.Descricao, MessagesResource.MSG_ComponenteCurricularConfiguracaoDivisoes);
            //            return;
            //        }
            //    }
            //}

            var seqNivelResponsavel = componenteCurricular.NiveisEnsino.Where(w => w.Responsavel == true).SingleOrDefault().SeqNivelEnsino;
            var configuracao = new InstituicaoNivelTipoComponenteCurricularDomainService().BuscarInstituicaoNivelTipoComponenteCurricularConfiguracao(seqNivelResponsavel, componenteCurricular.SeqTipoComponenteCurricular);

            //Verifica se não intervalos de períodos com conflitos
            if (componenteCurricular.OrgaosReguladores != null)
            {
                if (!ValidacaoData.ValidarSobreposicaoPeriodos<ComponenteCurricularOrgaoRegulador>(componenteCurricular.OrgaosReguladores.ToList(), nameof(ComponenteCurricularOrgaoRegulador.DataInicio), nameof(ComponenteCurricularOrgaoRegulador.DataFim)) ||
                    !ValidacaoData.ValidarContinuidadePeriodos(componenteCurricular.OrgaosReguladores, p => p.DataInicio, p => p.DataFim))
                {
                    this.AddPropertyError(p => p.OrgaosReguladores, MessagesResource.MSG_ComponenteCurricularOrgaoReguladorData);
                    return;
                }
            }

            //Verifica se não ficou período de espaço entre as ementas e o tamanhdo minimo definido na configuração
            if (componenteCurricular.Ementas != null)
            {
                var listEmenta = componenteCurricular.Ementas.OrderBy(o => o.DataInicio).ToList();
                for (int indice = 0; indice < listEmenta.Count; indice++)
                {
                    if (listEmenta[indice].Ementa.Length < configuracao.QuantidadeMinimaCaracteresEmenta)
                    {
                        this.AddPropertyError(p => p.Ementas, string.Format(MessagesResource.MSG_ComponenteCurricularEmentaMinimo, configuracao.QuantidadeMinimaCaracteresEmenta));
                        return;
                    }

                    var dataFim = DateTime.MaxValue.AddDays(-1);

                    if (listEmenta[indice].DataFim.HasValue)
                        dataFim = listEmenta[indice].DataFim.Value;

                    if (indice + 1 < listEmenta.Count && dataFim.AddDays(1) != listEmenta[indice + 1].DataInicio)
                    {
                        this.AddPropertyError(p => p.Ementas, MessagesResource.MSG_ComponenteCurricularEmentaDatas);
                        return;
                    }
                }
            }

            //Verificar se a Entidade Responsável é obrigatória no parâmetro
            if (configuracao.EntidadeResponsavelObrigatoria && componenteCurricular.EntidadesResponsaveis.SMCCount() == 0)
            {
                this.AddPropertyError(p => p.EntidadesResponsaveis, MessagesResource.MSG_ComponenteCurricularEntidadeObrigatorio);
                return;
            }

            ////Verificar número de entidades máximo definido nas configurações
            //if (componenteCurricular.EntidadesResponsaveis.SMCCount() > configuracao.QuantidadeMaximaEntidadeResponsavel)
            //{
            //    this.AddPropertyError(p => p.EntidadesResponsaveis, string.Format(MessagesResource.MSG_ComponenteCurricularEntidadeMaximo, configuracao.QuantidadeMaximaEntidadeResponsavel));
            //    return;
            //}
        }
    }
}