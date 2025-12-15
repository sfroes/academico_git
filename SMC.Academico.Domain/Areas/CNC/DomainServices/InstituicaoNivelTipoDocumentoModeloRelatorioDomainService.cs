using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Academico.Domain.Areas.CNC.Specifications;
using SMC.Framework;
using SMC.Framework.Model;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CNC.DomainServices
{
    public class InstituicaoNivelTipoDocumentoModeloRelatorioDomainService : AcademicoContextDomain<InstituicaoNivelTipoDocumentoModeloRelatorio>
    {
        private InstituicaoNivelTipoDocumentoAcademicoDomainService InstituicaoNivelTipoDocumentoAcademicoDomainService => Create<InstituicaoNivelTipoDocumentoAcademicoDomainService>();

        public List<SMCDatasourceItem> BuscarIdiomasDocumentoAcademicoSelect(long seqNivelEnsinoPorGrupoDocumentoAcademico, long seqTipoDocumentoAcademico)
        {
            var specInstituicaoNivelTipoDocumentoAcademico = new InstituicaoNivelTipoDocumentoAcademicoFilterSpecification()
            {
                SeqNivelEnsino = seqNivelEnsinoPorGrupoDocumentoAcademico,
                SeqTipoDocumentoAcademico = seqTipoDocumentoAcademico
            };
            var instituicaoNivelTipoDocumentoAcademico = InstituicaoNivelTipoDocumentoAcademicoDomainService.SearchByKey(specInstituicaoNivelTipoDocumentoAcademico);

            var spec = new InstituicaoNivelTipoDocumentoModeloRelatorioFilterSpecification() { SeqInstituicaoNivelTipoDocumentoAcademico = instituicaoNivelTipoDocumentoAcademico.Seq };
            var instituicaoNivelTipoDocumentoModeloRelatorio = this.SearchBySpecification(spec).ToList();

            var retorno = new List<SMCDatasourceItem>();
            foreach (var item in instituicaoNivelTipoDocumentoModeloRelatorio)
            {
                var idioma = new SMCDatasourceItem()
                {
                    Seq = (long)item.Idioma,
                    Descricao = item.Idioma.GetDescriptionPortuguese(),
                    Selected = SMCLanguage.Portuguese == item.Idioma
                };
                retorno.Add(idioma);
            }
            return retorno;
        }
    }
}
