using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CAM.ValueObjects
{
    public class SelecaoOfertaCampanhaLookupListaVO : ISMCMappable
    {
        /// <summary>
        /// Tipo Object necessário para implementação do lookup receber seq duplo, 
        /// da linha selecionada
        /// </summary>
        public object Seq { get; set; }

        public long? SeqCampanha { get; set; }

        public string Descricao { get; set; }

        public long? SeqTipoOferta { get; set; }

        public string TipoOferta { get; set; }

        public string TipoOfertaToken { get; set; }

        public string CursoOferta { get; set; }

        public string Localidade { get; set; }

        public string Turno { get; set; }

        public string AreaConcentracao { get; set; }

        public string LinhaPesquisa { get; set; }

        public string EixoTematico { get; set; }

        public string AreaTematica { get; set; }

        public long? SeqOrientador { get; set; }

        public string Orientador { get; set; }

        public string Turma { get; set; }

        public string Vagas { get; set; }

        public long? SeqFormacaoEspecifica { get; set; }

        public long? SeqTurma { get; set; }

        public long? SeqCursoOferta { get; set; }

        public long? SeqCursoOfertaLocalidadeTurno { get; set; }

        public long? SeqCursoOfertaLocalidade { get; set; }

        public List<SMCDatasourceItem> Turnos { get; set; }

        public List<SMCDatasourceItem> Orientadores { get; set; }

        public long? SeqTurno { get; set; }
    }
}
