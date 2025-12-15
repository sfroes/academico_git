using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class ItemRelatorioDisciplinasCursadasVO : ISMCMappable
    {
        public long SeqAluno { get; set; }
        
        public long SeqHistoricoEscolar { get; set; }
        
        public long SeqInstituicaoEnsino { get; set; }

        public string Aluno { get; set; }

        public string CursoOferta { get; set; }

        public string Instituicao { get; set; }

        public string Programa { get; set; }

        public string NivelEnsino { get; set; }

        public string TipoVinculo { get; set; }

        public bool ExibirEmentasComponentesCurriculares { get; set; }

        public List<ItemRelatorioDisciplinasCursadasComponenteCurricularVO> ComponentesCurriculares { get; set; }

        public List<ItemRelatorioDisciplinasCursadasEmentaVO> Ementas { get; set; }
    }
}