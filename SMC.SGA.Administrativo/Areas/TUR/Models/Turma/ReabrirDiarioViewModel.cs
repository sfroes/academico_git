using SMC.Academico.Common.Enums;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.TUR.Models
{
    public class ReabrirDiarioViewModel : SMCViewModelBase
    {
        /*Situação Texto/100 x Aberto/fechado
                Data Data x
                Usuário Texto/250 x
                Motivo Texto/500 x NV01
*/
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqTurma { get; set; }

        [SMCReadOnly]
        public string DescricaoSituacao { get { return DiarioFechado ? "Fechado" : "Aberto"; } }

        [SMCReadOnly]
        public DateTime DataInclusao { get; set; }

        [SMCReadOnly]
        public string Usuario { get; set; }

        /// <summary>
        /// Botão de reabertura e campo Motivo somente disponíveis se o diário estiver aberto
        /// Neste caso o campo Motivo é obrigatório.
        /// </summary>
        [SMCRequired]
        [SMCMaxLength(500)]
        [SMCMultiline]
        public string Observacao { get; set; }

        [SMCHidden]
        public bool DiarioFechado { get; set; }
    }
}