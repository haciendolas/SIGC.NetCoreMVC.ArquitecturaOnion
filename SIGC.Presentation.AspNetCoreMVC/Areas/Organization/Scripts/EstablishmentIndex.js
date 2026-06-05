$(function () {
    let EstablishmentValidate = null;
    const Establishment = {
        _Init: function () {           
            Establishment._Validation.fnEstablishmentCreateUpdateValidate();
            Establishment._Other.fnOpenFile();
            Establishment._Search.fnEstablishmentDataTable();
            $('#stxtEstablishmentName,#txtEstablishmentName').keypress(function (event) {
                return Uti.KeyBoard.LettersAndNumbers(event);
            });
            $('#stxtEstablishmentName').on('keyup', Uti.SetTimeout.Debounce((event) => {
                const keyCode = event.keyCode ? event.keyCode : event.which;
                if (!(keyCode == 32 || keyCode == '32')) {
                    Establishment._Search.fnEstablishmentDataTable();
                };
            }));
            $('#scboStateID').on('change', function () {
                Establishment._Search.fnEstablishmentDataTable();
            });
            if ($('#btnEstablishmentCreate').length > 0) {
                $('#btnEstablishmentCreate').on('click', function () {
                    Establishment._Operation.fnEstablishmentCreateUpdate();
                });
            };
            if ($('#btnEstablishmentUpdate').length > 0) {
                $('#btnEstablishmentUpdate').hide();
                $('#btnEstablishmentUpdate').on('click', function () {
                    Establishment._Operation.fnEstablishmentCreateUpdate();
                });
            };
            $('#btnEstablishmentNew').on('click', function () {
                Establishment._Clear.fnEstablishmentGet();
            });
            $('#btnQuitar').hide();
            $('#btnQuitar').on('click', function () {
                Uti.Image.Preview('imgEstablishmentLogo');
                $('#profile-img-file-input').val('');
                $(this).hide();
                $('#hdEstablishmentLogoBandera').val('DELETE');
            });
        },
        _Clear: {
            fnEstablishmentGet: function () {
                $('#txtEstablishmentID').val('GENERADO');
                $('#cboTypeID,#txtEstablishmentName,#txtEstablishmentCode,#txtEstablishmentAddress').val('');
                $('#chkStateID').prop('checked', true);
                if ($('#btnEstablishmentUpdate').length > 0) $('#btnEstablishmentUpdate').hide();
                if ($('#btnEstablishmentCreate').length > 0) $('#btnEstablishmentCreate').show();
                Establishment._Other.fnEstablishmentTabs();
                Establishment._Validation.fnEstablishmentCreateUpdateReset();
                Uti.Image.Preview('imgEstablishmentLogo');
                $('#txtEstablishmentCode').focus();
                $('#hdEstablishmentLogo,#hdEstablishmentLogoBandera').val('');
                $('#profile-img-file-input').val('');
                $('#btnQuitar').hide();
            }
        },
        _Other: {
            fnOpenFile: function () {
                $('#profile-img-file-input').on('change', function (event) {
                    const _URL = window.URL || window.webkitURL;  //window.URL para firefox  webkitURL para chrome y otros navegadores
                    const file = event.target.files[0];
                    if (file) {
                        const tmppath = _URL.createObjectURL(file);
                        if (!(file.type == 'image/png' || file.type == 'image/jpeg' || file.type == 'image/jpg')) {
                            Uti.Modal.Message(Uti.Message.Type.Warning, 'Solo se admite archivos con extensión: (jpg,png,jpeg)');
                            $('#profile-img-file-input').val('');
                            return;
                        };
                        $('#imgEstablishmentLogo').fadeIn('fast').attr('src', tmppath);
                        $('#btnQuitar').show();
                        $('#hdEstablishmentLogoBandera').val('');
                    }
                });
            },
            fnEstablishmentTabs: function () {
                $('#establishment-card ul li a[href="#tab-search"]').removeClass('disabled');
                $('#establishment-card ul li a[href="#tab-search"]').attr('data-bs-toggle', 'tab');
            }
        },
        _Validation: {
            fnEstablishmentCreateUpdateReset: function () {
                EstablishmentValidate.resetForm();
                $('#frmEstablishmentCreateUpdate *').removeClass(['invalid-feedback', 'is-invalid']);
            },
            fnEstablishmentCreateUpdateValidate: function () {
                EstablishmentValidate = $('#frmEstablishmentCreateUpdate').validate({
                    rules: {
                        TypeID: { required: true},
                        EstablishmentCode: { required: true, minlength: 4, maxlength: 10 },
                        EstablishmentName: { required: true, minlength: 3, maxlength: 50 },
                        EstablishmentAddress: { required: true, minlength: 10, maxlength: 150 },
                    },
                    messages: {
                        TypeID: { required: '*Campo requerido'},
                        EstablishmentCode: { required: '*Campo requerido', minlength: '*Mínimo 4 caracteres', maxlength: '*Máximo 10 caracteres' },
                        EstablishmentName: { required: '*Campo requerido', minlength: '*Mínimo 3 caracteres', maxlength: '*Máximo 50 caracteres' },
                        EstablishmentAddress: { required: '*Campo requerido', minlength: '*Mínimo 10 caracteres', maxlength: '*Máximo 150 caracteres' }
                    },
                    highlight: function (element) {
                        $(element).addClass('is-invalid');
                    },
                    unhighlight: function (element) {
                        $(element).removeClass('is-invalid');
                    },
                    errorPlacement: function (error, element) {
                        const $parent = $(element).closest('.error-placeholder');
                        error.addClass('invalid-feedback');

                        if ($parent.length) {
                            $parent.append(error);
                        } else {
                            error.insertAfter(element);
                        }
                    },
                    submitHandler: function (form) {
                    }
                });
            }
        },
        _Search: {
            fnEstablishmentDataTable: function () {
                $('#dtEstablishment').dataTable({
                    oLanguage: {
                        sUrl: Uti.DataTable.sUrl,
                    },
                    bProcessing: true,
                    bServerSide: true,
                    iDisplayLength: Uti.DataTable.iDisplayLength.NumRows10,
                    //'<"row p-1 align-items-center"<"col-auto"B><"col-sm-4 col-auto m-0"f>>'
                    sDom: '<"row p-1 align-items-center"<"col-auto"B>>' +
                        'rt' +
                        '<"row"<"col-auto"l><"col text-center mt-2"i><"col-auto text-end"p>>',
                    buttons: [
                        { extend: 'copy', text: 'Copiar' },
                        { extend: 'excel', text: 'Excel' },
                        { extend: 'pdf', text: 'PDF' },
                        { extend: 'print', text: 'Imprimir' }
                    ],
                    lengthMenu: [[5, 10, 25, 50, 100], [5, 10, 25, 50, 100]],
                    initComplete: function () {
                        const input = $('#dtEstablishment_filter input');
                        input.removeClass().addClass('form-control');
                        input.attr({ placeholder: 'Buscar establecimiento...', type: 'text' });
                        input.off();
                        input.on('keyup', Uti.SetTimeout.Debounce((event) => {
                            const valor = event.target.value;
                            const keyCode = event.keyCode ? event.keyCode : event.which;
                            if (!(keyCode == 32 || keyCode == '32')) {
                                $('#dtEstablishment').DataTable().search(valor).draw();
                            };
                        })
                        );
                    },
                    bJQueryUI: false,
                    bAutoWidth: false,
                    bDestroy: true,
                    sServerMethod: "POST",
                    sAjaxSource: Uti.Url.Base + '/Organization/Establishment/EstablishmentDataTable',
                    fnServerParams: function (aoData) {
                        aoData.push(
                            { name: 'RecordStateID', value: $('#scboStateID').val() },
                            { name: 'Search', value: $('#stxtEstablishmentName').val().trim() }
                        );
                    },
                    sPaginationType: 'full_numbers',
                    aoColumnDefs: [
                        { bSortable: true, aTargets: [0], sClass: 'text-center' },
                        { bSortable: true, aTargets: [1], sClass: 'text-left' },
                        { bSortable: true, aTargets: [2], sClass: 'text-left' },
                        { bSortable: false, aTargets: [3], sClass: 'text-left' },
                        { bSortable: true, aTargets: [4], sClass: 'text-center' },
                        { bSortable: true, aTargets: [5], sClass: 'text-center' },
                        { bSortable: false, aTargets: [6], sClass: 'text-center' },
                        { bSortable: false, aTargets: [7], sClass: 'text-center' },
                        { bSortable: false, aTargets: [8], sClass: 'text-center' }
                    ],
                    order: [[0, 'desc']],
                    bSort: false,
                    rowCallback: function (row, data, dataIndex) {
                        $(row).find('a[name=slnkEdit]').on('click', function () {
                            Establishment._Search.fnEstablishmentGet(data[0]);
                        }).tooltip();
                        $(row).find('a[name=slnkInactive]').on('click', function () {
                            Establishment._Operation.fnEstablishmentChangeState(data[0], Uti.Variable.StateType.Inactive);
                        }).tooltip();
                        $(row).find('a[name=slnkActive]').on('click', function () {
                            Establishment._Operation.fnEstablishmentChangeState(data[0], Uti.Variable.StateType.Active);
                        }).tooltip();
                    },
                    drawCallback: function (data) {
                        const response = data.json;
                    }
                });
            },
            fnEstablishmentGet: function (EstablishmentID) {
                const options = {
                    url: Uti.Url.Base + '/Organization/Establishment/EstablishmentGet/' + EstablishmentID,
                    type: Uti.Variable.FetchAjax.Type.Get
                };
                Uti.Ajax.Custom(options, function (response) {
                    Uti.Modal.Message(response.type, response.message, response.function);
                    if (response.type === Uti.Message.Type.Session) {
                        Uti.Modal.Process();
                    }
                    if (response.type === Uti.Message.Type.Query) {
                        const { data: rowData } = response;
                        Establishment._Clear.fnEstablishmentGet();
                        $('#txtEstablishmentID').val(rowData.establishmentID);
                        $('#cboTypeID').val(rowData.typeID);
                        $('#txtEstablishmentCode').val(rowData.establishmentCode.trim());
                        $('#txtEstablishmentName').val(rowData.establishmentName.trim());
                        $('#txtEstablishmentAddress').val(rowData.establishmentAddress.trim());
                        $('#chkStateID').attr('checked', rowData.recordStateID == Uti.Variable.StateType.Active);
                        $('#hdEstablishmentLogo').val(rowData.establishmentLogo.trim());
                        Uti.Image.Preview('imgEstablishmentLogo', rowData.establishmentUrl.trim());
                        if (rowData.establishmentLogo.trim() != '') $('#btnQuitar').show();
                        $('#establishment-card ul li a[href="#tab-search"]').addClass('disabled');
                        $('#establishment-card ul li a[href="#tab-search"]').removeAttr('data-bs-toggle');
                        $('#establishment-card ul li a[href="#tab-register"]').tab('show');
                        if ($('#btnEstablishmentUpdate').length > 0) $('#btnEstablishmentUpdate').show();
                        if ($('#btnEstablishmentCreate').length > 0) $('#btnEstablishmentCreate').hide();
                    };
                });
            }
        },
        _Operation: {
            fnEstablishmentChangeState: function (EstablishmentId, StateID) {
                const options = {
                    url: Uti.Url.Base + '/Organization/Establishment/EstablishmentChangeState',
                    data: {
                        EstablishmentID: EstablishmentId,
                        RecordStateID: StateID
                    },
                    type: Uti.Variable.FetchAjax.Type.Put
                };
                Uti.Ajax.Custom(options, function (response) {
                    Uti.Modal.Message(response.type, response.message, response.function);
                    if (response.type === Uti.Message.Type.Session) {
                        Uti.Modal.Process();
                    }
                    if (response.type === Uti.Message.Type.Success) {
                        Establishment._Search.fnEstablishmentDataTable();
                    }
                });
            },
            fnEstablishmentCreateUpdate: function () {
                if ($('#frmEstablishmentCreateUpdate').valid()) {
                    const file = document.getElementById('profile-img-file-input').files[0];
                    if (file) {
                        if (!(file.type == 'image/png' || file.type == 'image/jpeg' || file.type == 'image/jpg')) {
                            Uti.Modal.Message(Uti.Message.Type.Warning, 'Solo se admite archivos con extensión: (jpg,png,jpeg)');
                            return;
                        };
                    };
                    const EstablishmentID = $('#txtEstablishmentID').val() == 'GENERADO' ? 0 : $('#txtEstablishmentID').val();

                    var formData = new FormData();
                    formData.append('EstablishmentID', EstablishmentID);
                    formData.append('TypeID', $('#cboTypeID').val());
                    formData.append('EstablishmentCode', $('#txtEstablishmentCode').val().trim());
                    formData.append('EstablishmentName', $('#txtEstablishmentName').val().trim());
                    formData.append('EstablishmentAddress', $('#txtEstablishmentAddress').val().trim());
                    formData.append('RecordStateId', $('#chkStateID').is(':checked') ? Uti.Variable.StateType.Active : Uti.Variable.StateType.Inactive);
                    formData.append('FormFile', file);
                    formData.append('EstablishmentLogo', $('#hdEstablishmentLogo').val().trim());
                    formData.append('EstablishmentLogoBandera', $('#hdEstablishmentLogoBandera').val().trim());

                    const options = {
                        url: Uti.Url.Base + '/Organization/Establishment/' + (EstablishmentID == 0 ? 'EstablishmentCreate' : 'EstablishmentUpdate') + '',
                        data: formData,
                        type: EstablishmentID == 0 ? Uti.Variable.FetchAjax.Type.Post : Uti.Variable.FetchAjax.Type.Put
                    };
                    Uti.Ajax.Custom(options, function (response) {
                        Uti.Modal.Message(response.type, response.message, response.function);
                        if (response.type === Uti.Message.Type.Session) {
                            Uti.Modal.Process();
                        };
                        if (response.type === Uti.Message.Type.Success) {
                            Uti.Modal.Process();
                            Establishment._Search.fnEstablishmentDataTable();
                            Establishment._Clear.fnEstablishmentGet();
                        };
                    });
                }
            },
        }
    }
    Establishment._Init();
});