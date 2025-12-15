using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.MAT.ValueObjects
{
    public class CondicaoPagamentoSolicitacaoMatriculaVO : ISMCMappable
    {
        public decimal ValorTotal { get; set; }

        public List<SMCDatasourceItem> CondicoesPagamento { get; set; }

        public int? SeqCondicaoPagamento { get; set; }

        public bool TermoAderido { get; set; }
    }
}
