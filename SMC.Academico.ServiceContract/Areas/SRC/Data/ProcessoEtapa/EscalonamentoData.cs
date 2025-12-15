using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class EscalonamentoData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqProcessoEtapa { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public DateTime? DataEncerramento { get; set; }

        public List<string> DescricaoGruposEscalonamento { get; set; }

        public SituacaoEtapa SituacaoEtapa { get; set; }

        public bool Vigente { get; set; }

        public bool ExisteSolicitacaoGrupoEscalonamento { get; set; }

        public bool HabilitaBtnComPermissaoManutencaoProcesso { get; set; }
    }
}