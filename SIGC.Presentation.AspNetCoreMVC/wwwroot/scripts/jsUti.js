var Uti = function () {
    return {
        Url: {
            Base: $('#UrlBase').val()
        },
        Fetch: {
            Custom: async function (options, successCallback) {
                if (options.url == null) options.url = "";

                if (options.headers == null)
                    options.headers = { "content-type": "application/json; charset=UTF-8" };

                if (options.method == null) options.method = "POST";

                let config = {
                    method: options.method,
                    headers: options.headers,
                };
                if (options.body != null) {
                    config = {
                        method: options.method,
                        headers: options.headers,
                        body: options.body,
                    };
                }

                if (
                    options.method == "POST" ||
                    options.method == "PUT" ||
                    options.method == "DELETE"
                )
                    Uti.Modal.Process("open");
                else
                    Uti.Modal.Process("open", Uti.Message.Description.LoadingInformation);

                await fetch(options.url, {
                    ...config,
                })
                    .then((responseType) => {
                        const contentType = responseType.headers.get("content-type");
                        if (contentType && contentType.indexOf("application/json") !== -1) {
                            return response.json();
                        } else {
                            return responseType.text();
                        }
                    })
                    .then((responseData) => {
                        if (successCallback != null && typeof successCallback == "function")
                            successCallback(responseData);
                    })
                    .catch((error) => {
                        //Uti.Modal.Message(Uti.Message.Title.AssistantOperation, Uti.Message.Description.ErrorAjax, Uti.Message.Type.Error);
                        console.log("error", error);
                    })
                    .finally(() => {
                        Uti.Modal.Process();
                        console.log("finally");
                    });
            },
            Post: async function (url, body, successCallback) {
                Uti.Modal.Process("open");
                await fetch(url, {
                    method: "POST",
                    body: JSON.stringify(body),
                    headers: {
                        "content-type": "application/json; charset=UTF-8",
                    },
                })
                    .then((responseType) => {
                        const contentType = responseType.headers.get("content-type");
                        if (contentType && contentType.indexOf("application/json") !== -1) {
                            return response.json();
                        } else {
                            return responseType.text();
                        }
                    })
                    .then((responseData) => {
                        if (successCallback != null && typeof successCallback == "function")
                            successCallback(responseData);
                    })
                    .catch((error) => {
                        //Uti.Modal.Message(Uti.Message.Title.AssistantOperation, Uti.Message.Description.ErrorAjax, Uti.Message.Type.Error);
                        console.log("error", error);
                    })
                    .finally(() => {
                        Uti.Modal.Process();
                        console.log("finally");
                    });
            },
            Put: async function (url, body, successCallback) {
                Uti.Modal.Process("open");
                await fetch(url, {
                    method: "PUT",
                    body: JSON.stringify(body),
                    headers: {
                        "content-type": "application/json; charset=UTF-8",
                    },
                })
                    .then((responseType) => {
                        const contentType = responseType.headers.get("content-type");
                        if (contentType && contentType.indexOf("application/json") !== -1) {
                            return response.json();
                        } else {
                            return responseType.text();
                        }
                    })
                    .then((responseData) => {
                        if (successCallback != null && typeof successCallback == "function")
                            successCallback(responseData);
                    })
                    .catch((error) => {
                        //Uti.Modal.Message(Uti.Message.Title.AssistantOperation, Uti.Message.Description.ErrorAjax, Uti.Message.Type.Error);
                        console.log("error", error);
                    })
                    .finally(() => {
                        Uti.Modal.Process();
                        console.log("finally");
                    });
            },
            Delete: async function (url, body, successCallback) {
                Uti.Modal.Process("open");
                await fetch(url, {
                    method: "DELETE",
                    headers: {
                        "content-type": "application/json; charset=UTF-8",
                    },
                })
                    .then((responseType) => {
                        const contentType = responseType.headers.get("content-type");
                        if (contentType && contentType.indexOf("application/json") !== -1) {
                            return response.json();
                        } else {
                            return responseType.text();
                        }
                    })
                    .then((responseData) => {
                        if (successCallback != null && typeof successCallback == "function")
                            successCallback(responseData);
                    })
                    .catch((error) => {
                        //Uti.Modal.Message(Uti.Message.Title.AssistantOperation, Uti.Message.Description.ErrorAjax, Uti.Message.Type.Error);
                        console.log("error", error);
                    })
                    .finally(() => {
                        Uti.Modal.Process();
                        console.log("finally");
                    });
            },
            Get: async function (url, successCallback) {
                Uti.Modal.Process("open", Uti.Message.Description.LoadingInformation);
                await fetch(url, {
                    method: "GET",
                })
                    .then((responseType) => {
                        const contentType = responseType.headers.get("content-type");
                        if (contentType && contentType.indexOf("application/json") !== -1) {
                            return response.json();
                        } else {
                            return responseType.text();
                        }
                    })
                    .then((responseData) => {
                        if (successCallback != null && typeof successCallback == "function")
                            successCallback(responseData);
                    })
                    .catch((error) => {
                        //Uti.Modal.Message(Uti.Message.Title.AssistantOperation, Uti.Message.Description.ErrorAjax, Uti.Message.Type.Error);
                        console.log("error", error);
                    })
                    .finally(() => {
                        Uti.Modal.Process();
                        console.log("finally");
                    });
            },
        },
        Ajax: {
            Custom: function (options, successCallback) {
                if (options.type == null) options.type = Uti.Variable.FetchAjax.Type.Post; 
                if (options.url == null) options.url = "";
                if (options.async == null) options.async = true; //true peticion asincrona | false no asincrona =ejecuta una funcion despues de haberse terminado la otra
                if (options.dataType == null) options.dataType = Uti.Variable.FetchAjax.DataType.Json; // Tipo de respuesta que retornada del controlador
                if (options.data == null) options.data = {};
                if (options.cache == null) options.cache = false; // true Borrar la cache
                if (options.contentType == null) options.contentType = Uti.Variable.FetchAjax.ContentType.ApplicationJsonCharset //options.contentType = > El tipo de contenido que se enviara al controlador
            
                if (options.preload == null || options.preload == undefined) options.preload = true;
                if (options.msgload == null || options.msgload == undefined) options.msgload = 'default';

                let config = {
                    type: options.type,
                    url: options.url,
                    async: options.async,
                    dataType: options.dataType, 
                    data: options.data,
                    cache: options.cache
                }       
                if (options.type != Uti.Variable.FetchAjax.Type.Get) {
                    const isFormData = options.data instanceof FormData;
                    const isJson = options.contentType?.includes(Uti.Variable.FetchAjax.ContentType.ApplicationJson) || (options.data && typeof options.data === "object");
                    config.processData = true;
                    if (isFormData) {
                        config.contentType = false;
                        config.processData = false;
                    } else if (isJson) {
                        config.contentType = Uti.Variable.FetchAjax.ContentType.ApplicationJsonCharset;                 
                        config.data = JSON.stringify(options.data);
                    } else {
                        config.contentType = Uti.Variable.FetchAjax.ContentType.ApplicationFormUrlencodedCharset;
                    }
                } 

                $.ajax({
                    ...config,
                    beforeSend: function () {
                        if (options.preload) {
                            if (options.type == "GET")
                                Uti.Modal.Process("open", Uti.Message.Description.LoadingInformation);
                            else
                                if (options.msgload == 'default') Uti.Modal.Process("open");
                                else Uti.Modal.Process("open", Uti.Message.Description.LoadingInformation);
                        };
                    },
                    success: function (response) {
                        if (successCallback != null && typeof successCallback == "function")
                            successCallback(response);
                    },
                    error: function (xhr, status, error) {                    
                        Uti.Modal.Message(Uti.Message.Type.Error,Uti.Message.Description.ErrorAjax);
                    },
                    complete: function (complete) { Uti.Modal.Process(); }
                }); 
            },
        },
        DataTable: {
            sUrl: $('#UrlBase').val() + '/scripts/spanish-config.txt',
            iDisplayLength: {
                NumRows5: 5,
                NumRows10: 10,
                NumRows15: 15,
                NumRows20: 20,
                NumRows25: 25,
                NumRows30: 30,
                NumRows35: 35,
                NumRows40: 40,
                NumRows45: 45,
                NumRows50: 50
            },
            _fnAjaxUpdate: function (IdTabla) {
                $('#' + IdTabla).dataTable()._fnAjaxUpdate();
            }
        },
        Convert: {
            String: {

            },
            Numeric: function (valor) {
                vari = String($.trim(valor) == '' ? 0 : valor).split(',').join('');
                return parseFloat(vari);
            },
            Integer: function (valor) {
                return parseInt(valor);
            },
            Base64ToArrayByte: function (cadenaBase64) {
                var binaryString = window.atob(cadenaBase64);
                var binaryLen = binaryString.length;
                var bytes = new Uint8Array(binaryLen);
                for (var i = 0; i < binaryLen; i++) {
                    var ascii = binaryString.charCodeAt(i);
                    bytes[i] = ascii;
                }
                return bytes;
            }
        },
        Download: {
            /*
            Blob: function (ByteArray, FileName) {
                var blob = new Blob([ByteArray]);
                var link = document.createElement('a');
                link.href = window.URL.createObjectURL(blob);               
                link.download = FileName;
                link.click();
            },
            */
            Bytes: function (ByteArray, FileName) {
                var bytes = new Uint8Array(ByteArray);
                var blob = new Blob([bytes]);
                var link = document.createElement('a');
                link.href = window.URL.createObjectURL(blob);
                link.download = FileName;
                link.click();
            },
            Base64: function (cadenaBase64, FileName) {
                var ArrayByte = Uti.Convert.Base64ToArrayByte(cadenaBase64);
                var blob = new Blob([ArrayByte]);
                var link = document.createElement('a');
                link.href = window.URL.createObjectURL(blob);
                link.download = FileName;
                link.click();
            }            
        },
        Format: {
            Numeric2: function (valor) {
                const num = Uti.Convert.Numeric(valor);
                var m = Number((Math.abs(num) * 100).toPrecision(15));
                return (Math.round(m) / 100 * Math.sign(num)).toFixed(2);
            },
            Numeric: function (valor, decimales = null) {
                const num = Uti.Convert.Numeric(valor);
                if (decimales == null) {
                    return num;
                }
                var m = Number((Math.abs(num) * Math.pow(10, decimales)).toPrecision(15));
                return (Math.round(m) / Math.pow(10, decimales) * Math.sign(num)).toFixed(decimales);
            },
            NumericSeparator: function (valor, decimales = null) {
                const num = Uti.Convert.Numeric(valor);
                if (decimales == null) {
                    return num;
                }
                const newValor = num.toLocaleString('es-PE', { minimumFractionDigits: decimales, maximumFractionDigits: decimales });
                return newValor;
            }
        },
        Modal: {
            Message: function (type, message=null, funcion = null) {              
                $('#message-modal-generic #btn-modal-not').text('Cerrar');
                $('#message-modal-generic #btn-modal-yes').hide().text('Aceptar');
                $('#message-modal-generic #message-error-generic').hide().text('');
                switch (type) {
                    case Uti.Message.Type.Success: // 'Success':
                        $('#message-modal-generic').modal('show');
                        $('#message-title-generic').html('Proceso!');
                        $('#message-description-generic').html(message);
                        $('#message-modal-generic #btn-modal-not').prop({ 'class': 'btn btn-success' });
                        $('#message-loard-icon-generic').prop({
                            'src': 'https://cdn.lordicon.com/uvofdfal.json',
                            'trigger': 'loop',
                            'colors': 'primary:#30e849,secondary:#30e849',
                            'style':'width:110px;height:110px'
                        });
                        break;
                    case Uti.Message.Type.Warning://'Warning':
                        $('#message-modal-generic').modal('show');
                        $('#message-title-generic').html('Advertencia!');
                        $('#message-description-generic').html(message);
                        $('#message-modal-generic #btn-modal-not').prop({ 'class': 'btn btn-warning' });
                        $('#message-loard-icon-generic').prop({
                            'src': 'https://cdn.lordicon.com/tdrtiskw.json',
                            'trigger': 'loop',
                            'colors': 'primary:#f7b84b,secondary:#f7b84b',
                            'style': 'width:120px;height:120px'
                        });
                        break;                  
                    case Uti.Message.Type.Error:// 'Error':
                        let messageError = message;
                        if (message.includes(',')) {
                            messageError = message.split(',')[0];
                            $('#message-error-generic').html(message.split(',')[1]).show();
                        }                       
                        $('#message-modal-generic').modal('show');
                        $('#message-title-generic').html('Error!');
                        $('#message-description-generic').html(messageError);
                        $('#message-modal-generic #btn-modal-not').prop({ 'class': 'btn btn-danger' });
                        $('#message-loard-icon-generic').prop({
                            'src': 'https://cdn.lordicon.com/ebyacdql.json',
                            'trigger': 'hover',
                            'state':'hover-cross-2',
                            'colors':'primary:#e83a30',
                            'style':'width:100px;height:100px'
                        });
                        break;          
                    case Uti.Message.Type.ConfirmDelete:// 'Confirm Delete':
                        $('#message-modal-generic').modal('show');
                        $('#message-title-generic').html('¿Estas seguro de eliminar?');
                        $('#message-description-generic').html(message ?? 'Realmente desea eliminar estos registros,Este proceso no se puede deshacer');
                        $('#message-modal-generic #btn-modal-not').show().prop({ 'class': 'btn w-sm btn-light' });
                        $('#message-modal-generic #btn-modal-yes').show().prop({ 'class': 'btn w-sm btn-danger' });
                        $('#message-loard-icon-generic').prop({
                            'src': 'https://cdn.lordicon.com/gsqxdxog.json',                           
                            'trigger': 'loop',                             
                            'colors': 'primary:#f7b84b,secondary:#f06548',
                            'style': 'width:110px;height:110px'
                        });
            
                        if (funcion) {
                            $('#btn-modal-yes').attr('onClick', '' + funcion + '');
                        }
                        break;
                    case Uti.Message.Type.ConfirmProcess:// 'Confirm Process':
                        $('#message-modal-generic').modal('show');
                        $('#message-title-generic').html('¿Confirmar Proceso?');
                        $('#message-description-generic').html(message ?? '');
                        $('#message-modal-generic #btn-modal-not').show().prop({ 'class': 'btn w-sm btn-light' });
                        $('#message-modal-generic #btn-modal-yes').show().prop({ 'class': 'btn w-sm btn-success' });
                        $('#message-loard-icon-generic').prop({
                            'src': 'https://cdn.lordicon.com/biqqsrac.json',
                            'trigger': 'loop',  
                            'colors': 'primary:#66a1ee,secondary:#66a1ee',
                            'style': 'width:110px;height:110px'
                        });
                        if (funcion) {
                            $('#btn-modal-yes').attr('onClick', '' + funcion + '');
                        }
                        break;
                    case Uti.Message.Type.Session: //'Session':
                        $('#message-modal-generic').modal('show');
                        $('#message-title-generic').html('Su sesión se ha terminado');
                        $('#message-description-generic').html(message ?? 'Vuelva a iniciar sessíon');
                        $('#message-modal-generic #btn-modal-not').prop({ 'class': 'btn w-sm btn-light' });
                        $('#message-modal-generic #btn-modal-yes').prop({ 'class': 'btn w-sm btn-success' });
                        $('#message-loard-icon-generic').prop({
                            'src': 'https://cdn.lordicon.com/bushiqea.json',
                            'trigger': 'loop',
                            'colors': 'primary:#e83a30',
                            'style': 'width:110px;height:110px'
                        });
                        $('#btn-modal-not').hide();
                        $('#btn-modal-yes').attr('onClick', '' + funcion + '').text('Iniciar Sesión').show();
                        break;
                }         
            },
            Process: function (modo, msg) {
                if (modo == 'open') {
                    if (!msg) msg = 'Procesando..';
                    $('body').prepend("<div id='ajax-overlay'><div id='ajax-overlay-body' class='center'><i class='fal fa-spinner fa-pulse fa-3x fa-fw'></i><span class='sr-only' style='position:relative'><br/>" + msg + "</span></div></div>");
                    $('#ajax-overlay').css({
                        position: 'absolute',
                        color: '#FFFFFF',
                        top: '0',
                        left: '0',
                        width: '100%',
                        height: '100%',
                        position: 'fixed',
                        background: 'rgba(39, 38, 46, 0.67)',
                        'text-align': 'center',
                        'z-index': '9999'
                    });
                    $('#ajax-overlay-body').css({
                        position: 'absolute',
                        top: '40%',
                        left: '50%',
                        width: '120px',
                        height: '48px',
                        'margin-top': '-12px',
                        'margin-left': '-60px',
                        //background: 'rgba(39, 38, 46, 0.1)',
                        '-webkit-border-radius': '10px',
                        '-moz-border-radius': '10px',
                        'border-radius': '10px'
                    });
                    $('#ajax-overlay').fadeIn(50);
                }
                else {
                    $('#ajax-overlay').fadeOut(100, function () {
                        $('#ajax-overlay').remove();
                    });
                }
            },
            Session: function () {
                $('#modal-message').modal('hide');
                $('#ajaxModal').remove();
                $('.modal-backdrop').remove();
                $remote = $('#lock-screen').attr('data-url'); 
                $modal = $('<div class="modal fade" style="z-index:9999999" id=ajaxModal data-bs-backdrop="static" data-bs-keyboard="false"><div class="modal-dialog modal-dialog-centered"></div></div>');
                $('body').append($modal);                
                $modal.modal('show');
                $modal.load($remote); 
                $('body').removeClass('modal-open');
                $('body').removeAttr('style'); 
            },
            Form: function (title, formlulario) {
                $('button[name="btn-modal-comun"]').hide();
                $('button[name="btn-modal-comun"]').removeAttr('click');
                $('#btn-modal-comun').show();
                $('#modal-comun .title').html(title);
                $('#modal-comun .formulario').html(formlulario);
                //  $('#modal-comun .modal-footer').html(button);
                $("#modal-comun").modal({
                    backdrop: 'static',
                    keyboard: false
                });
            },
            Toastify: function (message, type) {

                switch (type) {
                    case Uti.Message.Type.Warning:
                        type = 'bg-warning'
                        break;
                    case Uti.Message.Type.Success:
                        type = 'bg-primary'
                        break;
                    case Uti.Message.Type.Error:
                        type = 'bg-danger'
                        break;
                    case Uti.Message.Type.Alert:
                        type = 'bg-success'
                        break;
                    case Uti.Message.Type.Session:
                        type = 'bg-primary'
                        break;
                }

                Toastify({
                    text: message,
                    className: type,
                    duration: 3000,                   
                    newWindow: true,
                    close: true,
                    gravity: "top", 
                    position: "right",
                    stopOnFocus: true,                   
                    onClick: function () { }  
                }).showToast();
 
            }
        },
        Message: {
            Title: {
                AssistantOperation: $('#AssistantOperation').text(),
                AssistantVerify: $('#AssistantVerify').text(),               
                AssistantError: $('#AssistantError').text(),    
                AssistantSearch: $('#AssistantSearch').text(),
                AssistantDetalleLinea: $('#AssistantDetalleLinea').text(),
                AssistantSession: $('#AssistantSession').text(),
                AssistantConnectionWWW: $('#AssistantConnectionWWW').text()
            },
            Type: {
                Success: 'Success',
                Warning: 'Warning',
                Error: 'Error',                          
                ConfirmDelete: 'ConfirmDelete',
                ConfirmProcess: 'ConfirmProcess',
                Session: 'Session',
                Query: 'Query'
            },
            Description: { 
                VerifyExpiredSession: $('#VerifyExpiredSession').text(),
                ErrorAjax: $('#ErrorAjax').text(),
                ProcessInformation: $('#ProcessInformation').text(),
                LoadingInformation: $('#LoadingInformation').text(),
                NotFoundInformation: $('#NotFoundInformation').text(),
                QuestionDelete: $('#QuestionDelete').text(),
                RequiredDetalleLinea: $('#RequiredDetalleLinea').text(),
                RequiredSeleccionarDetalleLinea: $('#RequiredSeleccionarDetalleLinea').text(),
                ErrorException: $('#ErrorException').text()
            }
        },
        Image: {        
            Preview: function (idImg, fileUrl = '') {
                if (fileUrl == '') {
                    fileUrl = Uti.Url.Base + '/assets/images/image-not-available.jpg';                
                };               
                $('#' + idImg).fadeIn('fast').attr('src', fileUrl);    
            },
            PersonBycPerCodigo: function (idimg, cPerCodigo) {
                $.ajax({
                    url: Uti.Url.Base + '/Imagen/FotoPersonaBase64BycPerCodigo',
                    type: 'POST',
                    async: true,
                    data: { cPerCodigo: cPerCodigo },
                    beforeSend: function () { $('#' + idimg).attr('src', '' + Uti.Url.Base + '/Content/img/loader/general/preload_cuadro_azul_32x32_2.gif'); },
                    success: function (result) {
                        if (result == 'sin_foto') {
                            Uti.Image.Default(idimg);
                        }
                        else {
                            $('#' + idimg).attr('src', '' + result + '');
                        }
                    },
                    error: function () { },
                    complete: function () { }
                })
            }
        },
        KeyBoard: {
            Letters: function (e) {
                tecla = (document.all) ? e.keyCode : e.which; // 2
                if (tecla == 8) return true; // 3
                patron = /[A-Za-zñÑáéíóúÁÉÍÓÚ\s]/; // 4   
                te = String.fromCharCode(tecla); // 5
                return patron.test(te); // 6
            },
            Numbers: function (e) {
                var key = window.Event ? e.which : e.keyCode;
                return ((key >= 48 && key <= 57) || (key == 8));
            },
            LettersAndNumbers: function (e) {
                tecla = (document.all) ? e.keyCode : e.which; // 2
                if (tecla == 8) return true; // 3
                patron = /[A-Za-z0-9ñÑáéíóúÁÉÍÓÚ\s]/; // 4   
                te = String.fromCharCode(tecla); // 5
                return patron.test(te); // 6
            },
            Decimal: function (evt, control, num_decimales) {
                var charCode = (evt.which) ? evt.which : event.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46)
                    return false;
                else {
                    var len = $(control).val().length;
                    var index = $(control).val().indexOf('.');
                    if (index > 0 && charCode == 46) {
                        return false;
                    }
                    if (index > 0) {
                        var CharAfterdot = (len + 1) - index;
                        if (CharAfterdot > num_decimales + 1) {
                            return false;
                        }
                    }
                }
                return true;
            }
        },
        Variable: {
            FetchAjax: {
                ContentType: {
                    ApplicationFormUrlencodedCharset: "application/x-www-form-urlencoded; charset=UTF-8",
                    ApplicationJsonCharset: "application/json;charset=utf-8",
                    ApplicationJson: "application/json",
                },
                DataType: {
                    Json: "json",
                    Text: "text",
                    TextHtmlCharset: "text/html; charset=UTF-8",
                    Html: "html",
                    TextCharset: "text; charset=UTF-8",
                },
                Type: {
                    Post: "POST",
                    Get: "GET",
                    Put: "PUT",
                    Delete: "DELETE",
                },
            },
            StateType: {
                Inactive: 0,
                Active: 1,
                Delete:2
            }
        },
        HttpsServices: {

        },
        Date: {
            Today: function () {
                var date = new Date();
                var month = date.getMonth() + 1;
                var day = date.getDate();
                var current_date = (day < 10 ? '0' : '') + day + '/' + (month < 10 ? '0' : '') + month + '/' + date.getFullYear();
                return current_date;
            },
            NewByDateDays(fecha, dias) {
                var Fecha = new Date();
                var sFecha = fecha || (Fecha.getDate() + "/" + (Fecha.getMonth() + 1) + "/" + Fecha.getFullYear());
                var sep = sFecha.indexOf('/') != -1 ? '/' : '-';
                var aFecha = sFecha.split(sep);
                var fecha = aFecha[2] + '/' + aFecha[1] + '/' + aFecha[0];
                fecha = new Date(fecha);
                fecha.setDate(fecha.getDate() + parseInt(dias));
                var anno = fecha.getFullYear();
                var mes = fecha.getMonth() + 1;
                var dia = fecha.getDate();
                mes = (mes < 10) ? ("0" + mes) : mes;
                dia = (dia < 10) ? ("0" + dia) : dia;
                var fechaFinal = dia + sep + mes + sep + anno;
                return fechaFinal;
            },
            DaysByMonthYear(month, year) {
                return new Date(year || new Date().getFullYear(), month, 0).getDate();
            },
            Time: function () {
                var dt = new Date();
                var hours = dt.getHours(); // da el valor en formato de 24 horas
                var minutes = dt.getMinutes();
                var finalTime = (hours < 9 ? '0' + hours : hours) + ":" + (minutes < 9 ? '0' + minutes : minutes);
                return finalTime  // final time Time - 22:10
            }
        },
        Autocomplete: {
            Completar: function (ctrl, len) {
                var numero = ctrl.value;
                if (numero.length == len || numero.length == 0) return true;
                for (var i = 1; numero.length < len; i++) {
                    numero = '0' + numero;
                }
                ctrl.value = numero;
                return true;
            }
        },
        SetTimeout: {
            Debounce: function (fn, delay = 500) {
                let timeoutID;
                return function (...args) {
                    if (timeoutID) clearTimeout(timeoutID);
                    timeoutID = setTimeout(() => {
                        fn(...args)
                    }, delay);
                }
            }
        },
        Screen: {
            Height: document.documentElement.clientHeight,
            Scroll: {
                BodyContent: function () {
                    const headerRightHeight = $('.header-right').height();
                    const headerContentHeight = $('.header-content').height()
                    const footerContentHeight = $('.footer-content').height();
                    const bodyContentHeight = Uti.Screen.Height - (headerRightHeight + headerContentHeight + footerContentHeight + 5);
                    $('#body-content').css({ 'overflow-y': 'scroll', 'height': bodyContentHeight + "px" });
                },
                FormModalComun: function () {
                    $('#body-comun').css({ 'overflow-y': 'scroll', 'height': (Uti.Screen.Height) + "px" });
                }
            }
        }
    }
}();
$(function () {
    $('#span_mensajes').hide();
    $('#message-error-generic').hide();
});