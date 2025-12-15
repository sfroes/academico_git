using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.TUR.ValueObjects
{
    public class ListarTurmaVO : ISMCMappable
    {
        public long? SeqCicloLetivo { get; set; }

        public SMCUploadFile ArquivoLogotipo { get; set; }

        public string Instituicao { get; set; }

        public string Titulo { get; set; }

        public List<TurmaCicloLetivoVO> Turmas { get; set; }
    }
}