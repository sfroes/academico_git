using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Academico.Common.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.TUR.Models
{
    public class TurmaOrientacaoColaboradorViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }

        public TipoParticipacaoOrientacao TipoParticipacaoOrientacao { get; set; }

        public string DadosColaboradorCompleto { get; set; }

        public DateTime? DataInicioOrientacao { get; set; }

        public DateTime? DataFimOrientacao { get; set; }

        public bool VinculoAtivo { get; set; }

        public SituacaoVinculoOrietador SituacaoVinculoOrietador { get; set; } = SituacaoVinculoOrietador.VinculoNaoAtivo;

    }
}