using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class SolicitacaoDispensaItensCursadosVO : ISMCMappable
    {
        public List<SMCDatasourceItem> Titulacoes { get; set; }

        public List<SMCDatasourceItem> CiclosLetivos { get; set; }
                
        public long SeqSolicitacaoServico { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public List<SolicitacaoDispensaItensCursadosOutrasInstituicoesVO> ItensCursadosOutrasInstituicoes { get; set; }

        public List<ComponenteCurricularListaVO> ItensCursadosNestaInstituicao { get; set; }

        public decimal CursadosTotalCargaHorariaHoras { get; set; }

        public decimal CursadosTotalCargaHorariaHorasAula { get; set; }

        public decimal CursadosTotalCreditos { get; set; }

        public decimal DispensaTotalCargaHorariaHoras { get; set; }

        public decimal DispensaTotalCargaHorariaHorasAula { get; set; }

        public decimal DispensaTotalCreditos { get; set; }

        public bool ExibirGrupoComponente { get; set; }
    }
}