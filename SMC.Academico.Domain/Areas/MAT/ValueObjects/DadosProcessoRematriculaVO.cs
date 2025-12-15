using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.MAT.ValueObjects
{
    public class DadosProcessoRematriculaVO : ISMCMappable
    {
        public long SeqProcesso { get; set; }

        public string DescricaoProcesso { get; set; }

        public string TokenServico { get; set; }

        public IEnumerable<GrupoEscalonamento> EscalonamentosFuturos { get; set; }

        public long SeqServico { get; set; }

        public List<long> SeqsRestricaoSolicitacaoSimultanea { get; set; }

        public OrigemSolicitacaoServico OrigemSolicitacaoServico { get; set; }

        public decimal? PercentualServicoAdicional { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public IEnumerable<long> SeqsEntidadesResponsaveis { get; set; }
    }

    public class DadosConfiguracaoProcessoEtapaVO
    {
        public long SeqConfiguracaoProcesso { get; set; }

        public long SeqSolicitacaoServico { get; set; }

        public IEnumerable<DocumentoRequerido> Documentos { get; set; }

        public long SeqPrimeiraEtapaSGF { get; set; }

        public long SeqConfiguracaoPrimeiraEtapa { get; set; }

        public long SeqTemplateProcessoSGF { get; set; }
    }
}