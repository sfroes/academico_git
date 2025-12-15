$(document).ready(function () {
    $(".smc-sga-info-grupo-escalonamento").hover(function () {
        var info_content_id = $(this).attr("id");
        var info_content = $("#"+info_content_id+" input").val();

        $(this).append("<span class='info-grupo-escalonamento-show'>" + info_content + "</span>");
    }, function () {
        $(".info-grupo-escalonamento-show").remove();
    });
});