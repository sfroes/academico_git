using System.Collections.Generic;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class RelatorioDisciplinasCursadasData : ISMCMappable
    {
        public long SeqAluno { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public long SeqHistoricoEscolar { get; set; }

        public string Aluno { get; set; }

        public string CursoOferta { get; set; }

        public string Instituicao { get; set; }

        public string Programa { get; set; }

        public string NivelEnsino { get; set; }

        public string TipoVinculo { get; set; }

        public bool ExibirEmentasComponentesCurriculares { get; set; }

        public List<ItemRelatorioDisciplinasCursadasComponenteCurricularData> ComponentesCurriculares { get; set; }

        public List<ItemRelatorioDisciplinasCursadasEmentaData> Ementas { get; set; }
    }
}