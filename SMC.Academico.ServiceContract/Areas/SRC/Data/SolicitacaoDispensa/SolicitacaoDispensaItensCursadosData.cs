using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
    public class SolicitacaoDispensaItensCursadosData : ISMCMappable, ISMCLookupData
    {
        public List<SMCDatasourceItem> CiclosLetivos { get; set; }

        public List<SMCDatasourceItem> Titulacoes { get; set; }

        public long SeqSolicitacaoServico { get; set; }

        public long SeqProcessoEtapa { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public List<SolicitacaoDispensaItensCursadosOutrasInstituicoesData> ItensCursadosOutrasInstituicoes { get; set; }

        public List<ComponenteCurricularListaData> ItensCursadosNestaInstituicao { get; set; }

        public decimal CursadosTotalCargaHorariaHoras { get; set; }

        public decimal CursadosTotalCargaHorariaHorasAula { get; set; }

        public decimal CursadosTotalCreditos { get; set; }

        public decimal DispensaTotalCargaHorariaHoras { get; set; }

        public decimal DispensaTotalCargaHorariaHorasAula { get; set; }

        public decimal DispensaTotalCreditos { get; set; }
    }
}