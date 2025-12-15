
(function ($) {
    var btnRetornar = $('#btn_retornar_aluno'),
        btnConferencia = $('#btn_liberar_conferencia'),
        btnConsulta = $('#btn_liberar_consulta');

    $('[data-type="smc-instructions"]', btnRetornar.parent()).hide();
    $('[data-type="smc-instructions"]', btnConferencia.parent()).hide();
    $('[data-type="smc-instructions"]', btnConsulta.parent()).hide();

    $(document).on('smcload', function () {

		$('input, select, textarea, [data-control="masterdetail"], [data-type="smc-uploadFile"]').not('[data-bdp-bind]').on('change.bdp', function () {
            btnRetornar.prop('disabled', true).parent().addClass('smc-btn-disabled');
            $('[data-type="smc-instructions"]', btnRetornar.parent()).show();
            btnConferencia.prop('disabled', true).parent().addClass('smc-btn-disabled');
            $('[data-type="smc-instructions"]', btnConferencia.parent()).show();
            btnConsulta.prop('disabled', true).parent().addClass('smc-btn-disabled');
            $('[data-type="smc-instructions"]', btnConsulta.parent()).show();
        }).attr('data-bpd-bind', 'true');

    });

    //if (smc.core.uicontrol('#BloqueiaAlteracoes').getValue() == 'True') {
    //    $('[data-behavior="smc-behavior-salvar"]').prop('disabled', true);
    //}

})(jQuery);