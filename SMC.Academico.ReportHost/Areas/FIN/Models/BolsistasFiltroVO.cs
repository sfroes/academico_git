using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ReportHost.Areas.MAT.Models
{
    public class BolsistasFiltroVO : ISMCMappable
    {
        public long? SeqInstituicaoLogada { get; set; }

        public DateTime? DataInicioReferencia { get; set; }

        public DateTime? DataFimReferencia { get; set; }

        public List<TipoAtuacao> TipoAtuacoes { get; set; }

        public List<long> SeqsEntidadesResponsaveis { get; set; }

        public List<long> SeqsBeneficios { get; set; }

        public SituacaoChancelaBeneficio SituacaoBeneficio { get; set; }

        public bool ExibirParcelasEmAberto { get; set; }

        public bool ExibirReferenciaContrato { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public long? SeqCicloLetivoIngresso { get; set; }
        public List<SMCDatasourceItem> NiveisEnsino { get; set; }
        public long? SeqNivelEnsino { get; set; }

    }
}