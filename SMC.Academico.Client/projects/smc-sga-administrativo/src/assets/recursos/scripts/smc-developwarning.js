$(document).ready(function () {

    var local;// = "Produção";
    if (window.location.href.indexOf('localhost') > 0 || window.location.href.indexOf('desenvolvimento') > 0) {
        local = 'Desenvolvimento';
    }
    else if (window.location.href.indexOf('qualidade') > 0) {
        local = 'Qualidade';
    }
    else if (window.location.href.indexOf('homologacao') > 0) {
        local = 'Homologação';
    }

    if (local != null) {
		$("body").prepend(
      '<div style="width:100%; background-color:#bd0944; height:20px; position:fixed; text-align:center; z-index:2147483640; font-style:italic; color: #fff; font-weight:bold;top: 0px;"> Ambiente: ' +
        local +
        " </div>"
    );
    }

});
