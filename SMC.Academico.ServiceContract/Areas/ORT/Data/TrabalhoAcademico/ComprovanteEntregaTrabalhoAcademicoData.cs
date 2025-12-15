using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.ORT.Data
{
    public class ComprovanteEntregaTrabalhoAcademicoData : ISMCMappable
    {
            public long SeqTrabalhoAcademicos { get; set; }
            public long SeqInstituicaoNivel { get; set; }
            public long NumeroRegistroAcademico { get; set; }
            public string NomeAutor { get; set; }
            public string NomeEntidadeResponsavel { get; set; }
            public string NomeCurso { get; set; }
            public string DescricaoNivelEnsino { get; set; }
            public string DescricaoTurno { get; set; }
            public string DescricaoCicloLetivoIngresso { get; set; }
            public string DescricaoTipoTrabalho { get; set; }
            public string DescricaoTitulo { get; set; }
            public DateTime? DataDepositoSecretaria { get; set; }
            public string NomeCidadeLocalidade { get; set; }

            public string DataDepositoString
            {
                get
                {
                    return DataDepositoSecretaria?.ToShortDateString();
                }
            }

            public string NomeUsuarioLogado { get; set; }
        
    }
}
