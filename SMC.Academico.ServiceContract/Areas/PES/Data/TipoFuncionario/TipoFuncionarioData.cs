using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class TipoFuncionarioData : ISMCSeq, ISMCMappable
    {
        public long Seq { get; set; }
        public string Descricao { get; set; }
        public string Token { get; set; }
        public bool ObrigatorioVinculoUnico { get; set; }
        public TipoRegistroProfissional TipoRegistroProfissionalObrigatorio { get; set; }
    }
}