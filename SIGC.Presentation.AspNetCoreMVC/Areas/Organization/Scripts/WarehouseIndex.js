$(function () {
    let WarehouseValidate = null;
    const Warehouse = {
        _Init: function () {           
            Warehouse._Validation.fnWarehouseCreateUpdateValidate();
            Warehouse._Other.fnOpenFile();
            Warehouse._Search.fnWarehouseDataTable();
            $('#stxtWarehouseName,#txtWarehouseName').keypress(function (event) {
                return Uti.KeyBoard.LettersAndNumbers(event);
            });
            $('#stxtWarehouseName').on('keyup', Uti.SetTimeout.Debounce((event) => {
                const keyCode = event.keyCode ? event.keyCode : event.which;
                if (!(keyCode == 32 || keyCode == '32')) {
                    Warehouse._Search.fnWarehouseDataTable();
                };
            }));
            $('#scboStateID').on('change', function () {
                Warehouse._Search.fnWarehouseDataTable();
            });
            if ($('#btnWarehouseCreate').length > 0) {
                $('#btnWarehouseCreate').on('click', function () {
                    Warehouse._Operation.fnWarehouseCreateUpdate();
                });
            };
            if ($('#btnWarehouseUpdate').length > 0) {
                $('#btnWarehouseUpdate').hide();
                $('#btnWarehouseUpdate').on('click', function () {
                    Warehouse._Operation.fnWarehouseCreateUpdate();
                });
            };
            $('#btnWarehouseNew').on('click', function () {
                Warehouse._Clear.fnWarehouseGet();
            });
            $('#btnQuitar').hide();
            $('#btnQuitar').on('click', function () {
                Uti.Image.Preview('imgWarehouseLogo');
                $('#profile-img-file-input').val('');
                $(this).hide();
                $('#hdWarehouseLogoBandera').val('DELETE');
            });
            $('#cboGlobalEstablishmentID').on('change', function () {
                Warehouse._Search.fnWarehouseDataTable();
            });
        },
        _Clear: {
            fnWarehouseGet: function () {
                $('#txtWarehouseID').val('GENERADO');
                $('#cboTypeID,#txtWarehouseName,#txtWarehouseCode,#txtWarehouseAddress').val('');
                $('#chkStateID').prop('checked', true);
                if ($('#btnWarehouseUpdate').length > 0) $('#btnWarehouseUpdate').hide();
                if ($('#btnWarehouseCreate').length > 0) $('#btnWarehouseCreate').show();
                Warehouse._Other.fnWarehouseTabs();
                Warehouse._Validation.fnWarehouseCreateUpdateReset();
                Uti.Image.Preview('imgWarehouseLogo');
                $('#txtWarehouseCode').focus();
                $('#hdWarehouseLogo,#hdWarehouseLogoBandera').val('');
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
                        $('#imgWarehouseLogo').fadeIn('fast').attr('src', tmppath);
                        $('#btnQuitar').show();
                        $('#hdWarehouseLogoBandera').val('');
                    }
                });
            },
            fnWarehouseTabs: function () {
                $('#warehouse-card ul li a[href="#tab-search"]').removeClass('disabled');
                $('#warehouse-card ul li a[href="#tab-search"]').attr('data-bs-toggle', 'tab');
            }
        },
        _Validation: {
            fnWarehouseCreateUpdateReset: function () {
                WarehouseValidate.resetForm();
                $('#frmWarehouseCreateUpdate *').removeClass(['invalid-feedback', 'is-invalid']);
            },
            fnWarehouseCreateUpdateValidate: function () {
                WarehouseValidate = $('#frmWarehouseCreateUpdate').validate({
                    rules: {
                        TypeID: { required: true},
                        WarehouseCode: { required: true, minlength: 4, maxlength: 10 },
                        WarehouseName: { required: true, minlength: 3, maxlength: 50 },
                        WarehouseAddress: { required: true, minlength: 10, maxlength: 150 },
                    },
                    messages: {
                        TypeID: { required: '*Campo requerido'},
                        WarehouseCode: { required: '*Campo requerido', minlength: '*Mínimo 4 caracteres', maxlength: '*Máximo 10 caracteres' },
                        WarehouseName: { required: '*Campo requerido', minlength: '*Mínimo 3 caracteres', maxlength: '*Máximo 50 caracteres' },
                        WarehouseAddress: { required: '*Campo requerido', minlength: '*Mínimo 10 caracteres', maxlength: '*Máximo 150 caracteres' }
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
            fnWarehouseDataTable: function () {
                $('#dtWarehouse').dataTable({
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
                        const input = $('#dtWarehouse_filter input');
                        input.removeClass().addClass('form-control');
                        input.attr({ placeholder: 'Buscar almacen...', type: 'text' });
                        input.off();
                        input.on('keyup', Uti.SetTimeout.Debounce((event) => {
                            const valor = event.target.value;
                            const keyCode = event.keyCode ? event.keyCode : event.which;
                            if (!(keyCode == 32 || keyCode == '32')) {
                                $('#dtWarehouse').DataTable().search(valor).draw();
                            };
                        })
                        );
                    },
                    bJQueryUI: false,
                    bAutoWidth: false,
                    bDestroy: true,
                    sServerMethod: "POST",
                    sAjaxSource: Uti.Url.Base + '/Organization/Warehouse/WarehouseDataTable',
                    fnServerParams: function (aoData) {
                        aoData.push(
                            { name: 'RecordStateID', value: $('#scboStateID').val() },
                            { name: 'Search', value: $('#stxtWarehouseName').val().trim() },
                            { name: 'EstablishmentID', value: $('#cboGlobalEstablishmentID').val()   }
                        );
                    },
                    sPaginationType: 'full_numbers',
                    aoColumnDefs: [
                        { bSortable: true, aTargets: [0], sClass: 'text-left' },
                        { bSortable: true, aTargets: [1], sClass: 'text-center' },
                        { bSortable: true, aTargets: [2], sClass: 'text-center' },
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
                            Warehouse._Search.fnWarehouseGet(data[0]);
                        }).tooltip();
                        $(row).find('a[name=slnkInactive]').on('click', function () {
                            Warehouse._Operation.fnWarehouseChangeState(data[0], Uti.Variable.StateType.Inactive);
                        }).tooltip();
                        $(row).find('a[name=slnkActive]').on('click', function () {
                            Warehouse._Operation.fnWarehouseChangeState(data[0], Uti.Variable.StateType.Active);
                        }).tooltip();
                    },
                    drawCallback: function (data) {
                        const response = data.json;
                    }
                });
            },
            fnWarehouseGet: function (WarehouseID) {
                const options = {
                    url: Uti.Url.Base + '/Organization/Warehouse/WarehouseGet/' + WarehouseID,
                    type: Uti.Variable.FetchAjax.Type.Get
                };
                Uti.Ajax.Custom(options, function (response) {
                    Uti.Modal.Message(response.type, response.message, response.function);
                    if (response.type === Uti.Message.Type.Session) {
                        Uti.Modal.Process();
                    }
                    if (response.type === Uti.Message.Type.Query) {
                        const { data: rowData } = response;
                        Warehouse._Clear.fnWarehouseGet();
                        $('#txtWarehouseID').val(rowData.WarehouseID);
                        $('#cboTypeID').val(rowData.typeID);
                        $('#txtWarehouseCode').val(rowData.WarehouseCode.trim());
                        $('#txtWarehouseName').val(rowData.WarehouseName.trim());
                        $('#txtWarehouseAddress').val(rowData.WarehouseAddress.trim());
                        $('#chkStateID').attr('checked', rowData.recordStateID == Uti.Variable.StateType.Active);
                        $('#hdWarehouseLogo').val(rowData.WarehouseLogo.trim());
                        Uti.Image.Preview('imgWarehouseLogo', rowData.WarehouseUrl.trim());
                        if (rowData.WarehouseLogo.trim() != '') $('#btnQuitar').show();
                        $('#warehouse-card ul li a[href="#tab-search"]').addClass('disabled');
                        $('#warehouse-card ul li a[href="#tab-search"]').removeAttr('data-bs-toggle');
                        $('#warehouse-card ul li a[href="#tab-register"]').tab('show');
                        if ($('#btnWarehouseUpdate').length > 0) $('#btnWarehouseUpdate').show();
                        if ($('#btnWarehouseCreate').length > 0) $('#btnWarehouseCreate').hide();
                    };
                });
            }
        },
        _Operation: {
            fnWarehouseChangeState: function (WarehouseId, StateID) {
                const options = {
                    url: Uti.Url.Base + '/Organization/Warehouse/WarehouseChangeState',
                    data: {
                        WarehouseID: WarehouseId,
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
                        Warehouse._Search.fnWarehouseDataTable();
                    }
                });
            },
            fnWarehouseCreateUpdate: function () {
                if ($('#frmWarehouseCreateUpdate').valid()) {
                    const file = document.getElementById('profile-img-file-input').files[0];
                    if (file) {
                        if (!(file.type == 'image/png' || file.type == 'image/jpeg' || file.type == 'image/jpg')) {
                            Uti.Modal.Message(Uti.Message.Type.Warning, 'Solo se admite archivos con extensión: (jpg,png,jpeg)');
                            return;
                        };
                    };
                    const WarehouseID = $('#txtWarehouseID').val() == 'GENERADO' ? 0 : $('#txtWarehouseID').val();

                    var formData = new FormData();
                    formData.append('WarehouseID', WarehouseID);
                    formData.append('TypeID', $('#cboTypeID').val());
                    formData.append('WarehouseCode', $('#txtWarehouseCode').val().trim());
                    formData.append('WarehouseName', $('#txtWarehouseName').val().trim());
                    formData.append('WarehouseAddress', $('#txtWarehouseAddress').val().trim());
                    formData.append('RecordStateId', $('#chkStateID').is(':checked') ? Uti.Variable.StateType.Active : Uti.Variable.StateType.Inactive);
                    formData.append('FormFile', file);
                    formData.append('WarehouseLogo', $('#hdWarehouseLogo').val().trim());
                    formData.append('WarehouseLogoBandera', $('#hdWarehouseLogoBandera').val().trim());

                    const options = {
                        url: Uti.Url.Base + '/Organization/Warehouse/' + (WarehouseID == 0 ? 'WarehouseCreate' : 'WarehouseUpdate') + '',
                        data: formData,
                        type: WarehouseID == 0 ? Uti.Variable.FetchAjax.Type.Post : Uti.Variable.FetchAjax.Type.Put
                    };
                    Uti.Ajax.Custom(options, function (response) {
                        Uti.Modal.Message(response.type, response.message, response.function);
                        if (response.type === Uti.Message.Type.Session) {
                            Uti.Modal.Process();
                        };
                        if (response.type === Uti.Message.Type.Success) {
                            Uti.Modal.Process();
                            Warehouse._Search.fnWarehouseDataTable();
                            Warehouse._Clear.fnWarehouseGet();
                        };
                    });
                }
            },
        }
    }
    Warehouse._Init();
});