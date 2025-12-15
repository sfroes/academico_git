using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class GrupoEscalonamentoItemViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqGrupoEscalonamento { get; set; }

        [SMCHidden]
        public long SeqProcessoEtapa { get; set; }

        /// <summary>
        /// Esta propriedade foi cliada para bloquear os campos de descricao e situação da etapa
        /// independente da condição de bloqueio ou não do mestre detalhe (fora dessa classe)
        /// Quando tempos um contitional read only para um mestre detalhe que não o bloqueia, ele fica ativo
        /// e tambem ativa os campos dentro dele. Mesmo que o campo esteja anotado com SMCReadOnly.
        /// Como existe uma regra de negócio que diz que a descrição e a situação devem ser bloqueados sempre,
        /// foi necessário implementar dessa forma.
        /// </summary>
        [SMCHidden]
        public bool BloquearDescricaoESituacao { get { return true; } }

        [SMCSize(SMCSize.Grid8_24)]
        [SMCConditionalReadonly(nameof(BloquearDescricaoESituacao), true, PersistentValue = true)]
        public string DescricaoEtapa { get; set; }

        [SMCReadOnly]
        [SMCSize(SMCSize.Grid6_24)]
        [SMCSelect]
        [SMCConditionalReadonly(nameof(BloquearDescricaoESituacao), true, PersistentValue = true)]
        public SituacaoEtapa SituacaoEtapa { get; set; }

        [SMCHidden]
        public bool ExibeEscalonamentoDesabilitado { get; set; }

        [SMCSize(SMCSize.Grid8_24)]
        [SMCSelect(nameof(Escalonamentos), NameDescriptionField = nameof(DescricaoEscalonamento))]
        [SMCConditionalReadonly(nameof(ExibeEscalonamentoDesabilitado), true, PersistentValue = true)]
        [SMCRequired]
        public long SeqEscalonamento { get; set; }

        [SMCSize(SMCSize.Grid1_24)]
        [SMCLegendItemDisplay]
        public GrupoEscalonamentoParametros Legenda { get; set; }

        [SMCHidden]
        public bool DesabilitarEscalonamento
        {
            get
            {
                if (this.SituacaoEtapa == SituacaoEtapa.Encerrada)
                {
                    return true;
                }

                if (this.ExisteSolicitacoes && this.SituacaoEtapa != SituacaoEtapa.EmManutencao)
                {
                    return true;
                }

                return false;
            }
        }

        [SMCHidden]
        public string DescricaoEscalonamento { get; set; }

        [SMCIgnoreProp]
        [SMCHidden]
        public List<SMCDatasourceItem> Escalonamentos { get; set; }

        [SMCHidden]
        public bool ExisteSolicitacoes { get; set; }
    }
}