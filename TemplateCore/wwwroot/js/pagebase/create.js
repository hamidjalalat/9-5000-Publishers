
$(".custom-file-input").on("change", function () {
    var fileName = $(this).val().split("\\").pop();
    $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
});

$(document).ready(function () {
    $('#spanGoldenTipsDisplay').click(function () {
        let id = $("#Id").val();
        $.ajax({

            type: "POST",

            dataType: "json",

            data: { id: id },

            url: "/PageBases/GoldenTipsDisplay",

            error: function (response) {
                alert("error");
            },

            success: function (response) {
                $('#tempIframe')
                    .find('embed')
                    .remove()
                    .end();
                console.log(response);
                $.each(response, function (index, value) {
                    $("#tempIframe").append('<embed type="text/html" id="showIframe" src="' + value['serverPath'] + ' "style=height:250px;width:100%">');
                });
                $('#PublicembedShow').modal();
            },

            complete: function (response) {

            },
        });

    });
    $('#spanConceptualPointsDisplay').click(function () {
        let id = $("#Id").val();
        $.ajax({

            type: "POST",

            dataType: "json",

            data: { id: id },

            url: "/PageBases/ConceptualPointsDisplay",

            error: function (response) {
                alert("error");
            },

            success: function (response) {
                $('#tempIframe')
                    .find('embed')
                    .remove()
                    .end();
                console.log(response);
                $.each(response, function (index, value) {
                    $("#tempIframe").append('<embed type="text/html" id="showIframe" src="' + value['serverPath'] + ' "style=height:250px;width:100%">');
                });
                $('#PublicembedShow').modal();
            },

            complete: function (response) {

            },
        });

    });
    $('#spanLetterChartDisplay').click(function () {
        let id = $("#Id").val();
        $.ajax({

            type: "POST",

            dataType: "json",

            data: { id: id },

            url: "/PageBases/LetterChartDisplay",

            error: function (response) {
                alert("error");
            },

            success: function (response) {
                $('#tempIframe')
                    .find('embed')
                    .remove()
                    .end();
                console.log(response);
                $.each(response, function (index, value) {
                    $("#tempIframe").append('<embed type="text/html" id="showIframe" src="' + value['serverPath'] + ' "style=height:250px;width:100%">');
                });
                $('#PublicembedShow').modal();
            },

            complete: function (response) {

            },
        });

    });
    $('#spanLetterTableDisplay').click(function () {
        let id = $("#Id").val();
        $.ajax({

            type: "POST",

            dataType: "json",

            data: { id: id },

            url: "/PageBases/LetterTableDisplay",

            error: function (response) {
                alert("error");
            },

            success: function (response) {
                $('#tempIframe')
                    .find('embed')
                    .remove()
                    .end();
                console.log(response);
                $.each(response, function (index, value) {
                    $("#tempIframe").append('<embed type="text/html" id="showIframe" src="' + value['serverPath'] + ' "style=height:250px;width:100%">');
                });
                $('#PublicembedShow').modal();
            },

            complete: function (response) {

            },
        });
    })
    $('#spanMovieDisplay').click(function () {
        let id = $("#Id").val();
        $.ajax({

            type: "POST",

            dataType: "json",

            data: { id: id },

            url: "/PageBases/MovieDisplay",

            error: function (response) {
                alert("error");
            },

            success: function (response) {
                $('#temVideo')
                    .find('video')
                    .remove()
                    .end();

                console.log(response);
                $.each(response, function (index, value) {

                    //$("#temVideo").append('<source src="' + value['serverPath'] + '"type="video/mp4"/>');
                    $("#temVideo").append('<video controls="controls" id="showIframe" ><source src="' + value['serverPath'] + '" type="video/mp4"/></video>');
                });
                $('#moveiTagShow').modal();
            },

            complete: function (response) {

            },
        });

    });

    $('.btnembedShow').click(function () {
        $("#showIframe").attr("src", this.id);
        $("div#embedShow").modal();
    });
    $('#BookId').change(function () {

        var id = $("#BookId").val();


        $.ajax({

            type: "POST",

            dataType: "json",

            data: { id: id },

            url: "/PageBases/CountPage",

            error: function (response) {
                alert("error");
            },

            success: function (response) {
                $('#PageNumber').val(response.lastPage);
            },

            complete: function (response) {

            },
        });


    });
    $('#nextPage').click(function () {
        document.location.reload();
    });
    //$("#upload").click(function (evt) {
    //    var fileUpload = $("#conceptualpointsfiles").get(0);
    //    var files = fileUpload.files;
    //    var data = new FormData();
    //    for (var i = 0; i < files.length; i++) {
    //        data.append(files[i].name, files[i]);
    //    }
    //    data.append('id', $('#Id').val());
    //    data.append('conceptualpointsPageNumber', $('#conceptualpointsPageNumber').val());
    //    $.ajax({
    //        type: "POST",
    //        url: "/PageBases/UploadFilesAjax",
    //        contentType: false,
    //        processData: false,
    //        data: data,
    //        success: function (message) {
    //            alert(message);
    //        },
    //        error: function () {
    //            alert("خطا!");
    //        }
    //    });
    //});

    $('#saveGoldenTips').click(function () {
        var goldenTipsPageNumber = $("#goldenTipsPageNumber").val();
        var fileGoldenTips = $("#fileGoldenTips").val();
        debugger;
        if (goldenTipsPageNumber == "") {

            $('#validationgoldenTips').html('وارد کردن شماره صفحه الزامی می باشد');
            return;
        }
        if (fileGoldenTips == "") {
            $('#validationgoldenTips').html('انتخاب فایل محتوا الزامی می باشد');
            return;
        }
        //if (fileGoldenTips == fileGoldenTips.) {
        //    alert(fileGoldenTips);
        //    $('#validationgoldenTips').html('انتخاب فایل محتوا الزامی می باشد');
        //    return;
        //}
        var fileUpload = $("#fileGoldenTips").get(0);
        var files = fileUpload.files;
        var data = new FormData();
        for (var i = 0; i < files.length; i++) {
            data.append(files[i].name, files[i]);
        }
        data.append('id', $('#Id').val());
        data.append('goldenTipsPageNumber', $('#goldenTipsPageNumber').val());
        $.ajax({
            type: "POST",
            url: "/PageBases/UploadFilessGoldenTips",
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {
                if (result.result == true) {
                    $("#countbtngoldentips").text(parseInt($("#countbtngoldentips").text()) + 1);
                    $("div#messageSuccess").modal();
                    $(`#btngoldentips`).css('background', '#28a745');
                }
                else {
                    $("div#publicMessage p").html(result.message);
                    $("div#publicMessage").modal();
                    $(`#btngoldentips`).css('background', 'red');
                }

            },
            error: function () {
                $("div#messageError").modal();
                $(`#btngoldentips`).css('background', 'red');
            }
        });

        $(`div#goldentips`).modal('hide');


    });
    $('#btngoldentips').click(function () {
        $('#validationgoldenTips').html("");

        $("div#goldentips").modal();
    });

    $('#saveconceptualPoints').click(function () {
        var conceptualPointsPageNumber = $("#conceptualPointsPageNumber").val();
        var conceptualpointsfiles = $("#conceptualpointsfiles").val();
        debugger;
        if (conceptualPointsPageNumber == "") {

            $('#validationconceptualPoints').html('وارد کردن شماره صفحه الزامی می باشد');
            return;
        }
        if (conceptualpointsfiles == "") {
            $('#validationconceptualPoints').html('انتخاب فایل محتوا الزامی می باشد');
            return;
        }

        var fileUpload = $("#conceptualpointsfiles").get(0);
        var files = fileUpload.files;
        var data = new FormData();
        for (var i = 0; i < files.length; i++) {
            data.append(files[i].name, files[i]);
        }
        data.append('id', $('#Id').val());
        data.append('conceptualPointsPageNumber', $('#conceptualPointsPageNumber').val());
        $.ajax({
            type: "POST",
            url: "/PageBases/UploadFilessConceptualPoints",
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {
                if (result.result == true) {
                    $("#countconceptualpoints").text(parseInt($("#countconceptualpoints").text()) + 1);
                    $("div#messageSuccess").modal();
                    $(`#btnconceptualpoints`).css('background', '#28a745');
                }
                else {
                    $("div#publicMessage p").html(result.message);
                    $("div#publicMessage").modal();
                    $(`#btnconceptualpoints`).css('background', 'red');
                }
            },
            error: function () {
                $("div#messageError").modal();
                $(`#btnconceptualpoints`).css('background', 'red');
            }
        });

        $(`div#conceptualpoints`).modal('hide');


    });
    $('#btnconceptualpoints').click(function () {
        $('#validationconceptualPoints').html("");
        $("div#conceptualpoints").modal();
    });

    $('#saveLetterChart').click(function () {
        var LetterChartPageNumber = $("#letterChartPageNumber").val();
        var fileLetterChart = $("#fileLetterChart").val();
        debugger;
        if (LetterChartPageNumber == "") {

            $('#validationletterChart').html('وارد کردن شماره صفحه الزامی می باشد');
            return;
        }
        if (fileLetterChart == "") {
            $('#validationletterChart').html('انتخاب فایل محتوا الزامی می باشد');
            return;
        }
        var fileUpload = $("#fileLetterChart").get(0);
        var files = fileUpload.files;
        var data = new FormData();
        for (var i = 0; i < files.length; i++) {
            data.append(files[i].name, files[i]);
        }
        data.append('id', $('#Id').val());
        data.append('letterChartPageNumber', $('#letterChartPageNumber').val());
        $.ajax({
            type: "POST",
            url: "/PageBases/UploadFilessLetterChart",
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {
                if (result.result == true) {
                    $("#countletterchart").text(parseInt($("#countletterchart").text()) + 1);
                    $("div#messageSuccess").modal();
                    $(`#btnletterchart`).css('background', '#28a745');
                }
                else {
                    $("div#publicMessage p").html(result.message);
                    $("div#publicMessage").modal();
                    $(`#btnletterchart`).css('background', 'red');
                }

            },
            error: function () {
                $("div#messageError").modal();
                $(`#btnletterchart`).css('background', 'red');
            }
        });

        $(`div#letterchart`).modal('hide');


    });
    $('#btnletterchart').click(function () {
        $('#validationletterChart').html("");
        $("div#letterchart").modal();
    });

    $('#saveLetterTable').click(function () {
        var letterTablePageNumber = $("#letterTablePageNumber").val();
        var fileLetterTable = $("#fileLetterTable").val();
        debugger;
        if (letterTablePageNumber == "") {

            $('#validationletterTable').html('وارد کردن شماره صفحه الزامی می باشد');
            return;
        }
        if (fileLetterTable == "") {
            $('#validationletterTable').html('انتخاب فایل محتوا الزامی می باشد');
            return;
        }
        var fileUpload = $("#fileLetterTable").get(0);
        var files = fileUpload.files;
        var data = new FormData();
        for (var i = 0; i < files.length; i++) {
            data.append(files[i].name, files[i]);
        }
        data.append('id', $('#Id').val());
        data.append('letterTablePageNumber', $('#letterTablePageNumber').val());
        $.ajax({
            type: "POST",
            url: "/PageBases/UploadFilessLetterTable",
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {
                if (result.result == true) {
                    $("#countlettertable").text(parseInt($("#countlettertable").text()) + 1);
                    $("div#messageSuccess").modal();
                    $(`#blettertable`).css('background', '#28a745');
                }
                else {
                    $("div#publicMessage p").html(result.message);
                    $("div#publicMessage").modal();
                    $(`#blettertable`).css('background', 'red');
                }

            },
            error: function () {
                $("div#messageError").modal();
                $(`#btnlettertable`).css('background', 'red');
            }
        });

        $(`div#lettertable`).modal('hide');


    });
    $('#blettertable').click(function () {
        $('#validationlettertable').html("");
        $("div#lettertable").modal();
    });


    $('#saveMovie').click(function () {
        var moviePageNumber = $("#moviePageNumber").val();
        var fileMovie = $("#fileMovie").val();
        debugger;
        if (moviePageNumber == "") {

            $('#validationmovie').html('وارد کردن شماره صفحه الزامی می باشد');
            return;
        }
        if (fileMovie == "") {
            $('#validationmovie').html('انتخاب فایل محتوا الزامی می باشد');
            return;
        }
        var fileUpload = $("#fileMovie").get(0);
        var files = fileUpload.files;
        var data = new FormData();
        for (var i = 0; i < files.length; i++) {
            data.append(files[i].name, files[i]);
        }
        data.append('id', $('#Id').val());
        data.append('moviePageNumber', $('#moviePageNumber').val());
        $.ajax({
            type: "POST",
            url: "/PageBases/UploadFilesMovie",
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {
                if (result.result == true) {
                    $("#countmovie").text(parseInt($("#countmovie").text()) + 1);
                    $("div#messageSuccess").modal();
                    $(`#btnmovie`).css('background', '#28a745');
                }
                else {
                    $("div#publicMessage p").html(result.message);
                    $("div#publicMessage").modal();
                    $(`#btnmovie`).css('background', 'red');
                }

            },
            error: function () {
                $("div#messageError").modal();
                $(`#btnmovie`).css('background', 'red');
            }
        });

        $(`div#movie`).modal('hide');


    });
    $('#btnmovie').click(function () {
        $('#validationmovie').html("");
        $("div#movie").modal();
    });
});