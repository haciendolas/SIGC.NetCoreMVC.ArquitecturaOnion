$(function () {
    var Sign = function () {
        return {
            init: function () {
                $('#alert-danger,#alert-success,#icon-eye-on').hide();
                Sign.signValidation(); Sign.signIcon();
                
            },
            signIcon: function () {
                $('#btnPassword').attr('disabled', false).on('click', function () {                    
                    if ($('#icon-class').hasClass('ri-eye-fill')) {
                        $('#icon-class').removeClass('ri-eye-fill');  
                        $('#icon-class').addClass('ri-eye-off-fill');
                        $('#UserPassword').attr('type', 'password').focus();
                        $('#icon-eye-off').show(); $('#icon-eye-on').hide();
                    }
                    else {                       
                        $('#icon-class').removeClass('ri-eye-off-fill');
                        $('#icon-class').addClass('ri-eye-fill');
                        $('#UserPassword').attr('type', 'text').focus();
                        $('#icon-eye-off').hide(); $('#icon-eye-on').show();
                    }
                });
            },
            signBaseURL: function () {
                return $('#UrlBase').val();
            },
            signValidation: function () {
                $('#frmLogin').validate({
                    invalidHandler:
                        function () {
                        },
                    rules: {
                        CompanyDocumentNumber: { required: true },
                        UserName: { required: true },
                        UserPassword: { required: true }
                    },
                    messages: {
                        CompanyDocumentNumber: { required: "Ingrese usuario" },
                        UserName: { required: "Ingrese usuario" },
                        UserPassword: { required: "Ingrese contrase&ntilde;a" }
                    },
                    errorPlacement: function errorPlacement(error, element) {
                        var $parent = $(element).parents(".error-placeholder");
                        if ($parent.find(".jquery-validation-error").length) {
                            return;
                        }
                        $parent.append(
                            error.addClass("invalid-feedback")
                        );
                        if (element.parents('.invalid-feedback').length) {
                            error.insertAfter(element.parent());
                        } else {
                            error.insertAfter(element);
                        }
                    },
                    highlight: function (element) {
                        const $el = $(element);
                        $el.parents(".error-placeholder");
                        $el.addClass("is-invalid");
                    },
                    unhighlight: function (element) {
                        $(element).parents('.mb-3').find(".is-invalid").removeClass("is-invalid");
                    },
                    submitHandler: function (form) {
                        $('#alert-danger,#alert-success').hide();
                        var btn = $('#login-btn');
                        btn.html('Conectandose ...');
                        btn.attr('disabled', 'disabled');
                        $.ajax({
                            url: Sign.signBaseURL() + '/Authenticate/Authenticate',
                            method: "POST",
                            data: $(form).serialize(),
                            cache: false,
                            statusCode: {
                                401: function () {
                                    $(".lblRespuesta").html("Las credenciales son incorrectas.");
                                    $('#alert-danger').show();
                                },
                                404: function () {
                                    $(".lblRespuesta").html("El recurso no esta disponible, contactese con el administrador del sistema.");
                                    $('#alert-danger').show();
                                },
                                200: function () { },
                                201: function () { },
                                202: function () { },
                                500: function () {
                                    $(".lblRespuesta").html("Internal Server Error, contactese con el administrador del sistema.");
                                    $('#alert-danger').show();
                                }
                            },
                            beforeSend: function (xhr) {
                                setTimeout(function () { btn.text('Espere por favor ...'); }, 1400);
                            },
                            success: function (response, callback) {
                                setTimeout(function () {
                                    $(".lblRespuesta").html(response.message);
                                    if (response.url == null || response.url == "") {
                                        $('#alert-danger').show();
                                    }
                                    else {
                                        $('#alert-success').show();
                                    }
                                }, 2000);
                                if (response.url == null || response.url == "") {
                                    setTimeout(function () { btn.removeAttr('disabled').html('Iniciar sesi&oacute;n'); }, 2500);
                                }
                                else {
                                    setTimeout(function () { location.href = Sign.signBaseURL() + "/" + response.url; }, 2500);
                                }
                            },
                            error: function (jqXHR, textStatus, errorThrown) {
                                btn.removeAttr('disabled').html('Iniciar sesi&oacute;n');
                                setTimeout(function () { $(".lblRespuesta").html(''); $('#alert-danger').hide(); }, 20000);
                            },
                            failure: function (response, status) { alert(response); }
                        });
                    }
                });
            }
        };
    }();
    Sign.init();
});