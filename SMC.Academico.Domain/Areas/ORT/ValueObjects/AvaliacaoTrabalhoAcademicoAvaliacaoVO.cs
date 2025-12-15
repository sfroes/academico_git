using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Domain.Areas.APR.ValueObjects;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ORT.ValueObjects
{
    public class AvaliacaoTrabalhoAcademicoAvaliacaoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqTrabalhoAcademico { get; set; }

        public bool AlunoFormado { get; set; }

        public long? SeqArquivoAnexadoAtaDefesa { get; set; }

        public string GuidArquivoAnexadoAtaDefesa { get; set; }

        public long SeqOrigemAvaliacao { get; set; }

        public DateTime? DataInicioAplicacaoAvaliacao { get; set; }

        public DateTime? Hora { get; set; }

        public DateTime? DataCancelamento { get; set; }

        public string Local { get; set; }

        public string NotaConceito { get; set; }

        public short? Nota { get; set; }

        public long? SeqEscalaApuracaoItem { get; set; }

        public virtual TipoAvaliacao TipoAvaliacao { get; set; }

        public SituacaoHistoricoEscolar SituacaoHistoricoEscolar { get; set; }

        public List<MembroBancaExaminadoraVO> MembrosBancaExaminadora { get; set; }
    }
}
