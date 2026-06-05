$(function () {
    let CategoryValidate = null;
    const Category = {
        _Init: function () {
            $("#txtCategoryName").stringToSlug({
                setEvents: 'keyup keydown blur',
                getPut: '#txtCategorySlug',
                space: '-'
            });
            Category._Validation.fnCategoryCreateUpdateValidate();
            Category._Other.fnOpenFile();
            Category._Search.fnCategoryDataTable();
            $('#stxtCategoryName,#txtCategoryName').keypress(function (event) {
                return Uti.KeyBoard.LettersAndNumbers(event);
            });
            $('#stxtCategoryName').on('keyup', Uti.SetTimeout.Debounce((event) => {
                const keyCode = event.keyCode ? event.keyCode : event.which;
                if (!(keyCode == 32 || keyCode == '32')) {
                    Category._Search.fnCategoryDataTable();
                };
            }));
            $('#scboStateID').on('change', function () {
                Category._Search.fnCategoryDataTable();
            });
            if ($('#btnCategoryCreate').length>0) {
                $('#btnCategoryCreate').on('click', function () {
                    Category._Operation.fnCategoryCreateUpdate();
                });
            };
            if ($('#btnCategoryUpdate').length>0) {
                $('#btnCategoryUpdate').hide();
                $('#btnCategoryUpdate').on('click', function () {
                    Category._Operation.fnCategoryCreateUpdate();
                });
            };
            $('#btnCategoryNew').on('click', function () {
                Category._Clear.fnCategoryGet();
            });
            $('#btnQuitar').hide();
            $('#btnQuitar').on('click', function () {
                Uti.Image.Preview('imgCategoryImage');
                $('#profile-img-file-input').val('');
                $(this).hide();
                $('#hdCategoryImageBandera').val('DELETE');
            });
        },
        _Clear: {
            fnCategoryGet: function () {
                $('#txtCategoryID').val('GENERADO');
                $('#txtCategoryName,#txtCategorySlug').val('');   
                $('#chkStateID').prop('checked', true); 
                if ($('#btnCategoryUpdate').length > 0) $('#btnCategoryUpdate').hide();
                if ($('#btnCategoryCreate').length > 0) $('#btnCategoryCreate').show();
                Category._Other.fnCategoryTabs();
                Category._Validation.fnCategoryCreateUpdateReset();
                Uti.Image.Preview('imgCategoryImage');
                $('#txtCategoryName').focus();
                $('#hdCategoryImage,#hdCategoryImageBandera').val('');
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
                        $('#imgCategoryImage').fadeIn('fast').attr('src', tmppath);                       
                        $('#btnQuitar').show();
                        $('#hdCategoryImageBandera').val('');
                    }
                });
            },
            fnCategoryTabs: function () {
                $('#category-card ul li a[href="#tab-search"]').removeClass('disabled');
                $('#category-card ul li a[href="#tab-search"]').attr('data-bs-toggle', 'tab');      
            }
        },
        _Validation: {
            fnCategoryCreateUpdateReset: function () {
                CategoryValidate.resetForm();
                $('#frmCategoryCreateUpdate *').removeClass(['invalid-feedback', 'is-invalid']);
            },
            fnCategoryCreateUpdateValidate: function () {
                CategoryValidate = $('#frmCategoryCreateUpdate').validate({
                    rules: { 
                        CategorySlug: { required: true, minlength: 3, maxlength: 100 },   
                        CategoryName: { required: true, minlength: 3, maxlength: 100 },    
                    },
                    messages: {                        
                        CategorySlug: { required: '*Campo requerido', minlength: '*Mínimo 3 caracteres', maxlength: '*Máximo 100 caracteres' } ,
                        CategoryName: { required: '*Campo requerido', minlength: '*Mínimo 3 caracteres', maxlength: '*Máximo 100 caracteres' }    
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
            fnCategoryDataTable: function () {
                $('#dtCategory').dataTable({
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
                        const input = $('#dtCategory_filter input');
                        input.removeClass().addClass('form-control');
                        input.attr({ placeholder: 'Buscar categoría...', type: 'text' });
                        input.off();
                        input.on('keyup', Uti.SetTimeout.Debounce((event) => {
                            const valor = event.target.value;
                            const keyCode = event.keyCode ? event.keyCode : event.which;
                            if (!(keyCode == 32 || keyCode == '32')) {
                                $('#dtCategory').DataTable().search(valor).draw();
                            };
                        })
                        );
                    },
                    bJQueryUI: false,
                    bAutoWidth: false,
                    bDestroy: true,
                    sServerMethod: "POST",
                    sAjaxSource: Uti.Url.Base + '/Product/Category/CategoryDataTable',
                    fnServerParams: function (aoData) {
                        aoData.push(
                            { name: 'RecordStateID', value: $('#scboStateID').val() },
                            { name: 'Search', value: $('#stxtCategoryName').val().trim() }                             
                        );
                    },
                    sPaginationType: 'full_numbers',
                    aoColumnDefs: [
                        { bSortable: true, aTargets: [0], sClass: 'text-center' },
                        { bSortable: true, aTargets: [1], sClass: 'text-left' },
                        { bSortable: true, aTargets: [2], sClass: 'text-left' },
                        { bSortable: false, aTargets: [3], sClass: 'text-center' },
                        { bSortable: true, aTargets: [4], sClass: 'text-center' },
                        { bSortable: true, aTargets: [5], sClass: 'text-center' },
                        { bSortable: false, aTargets: [6], sClass: 'text-center' },
                        { bSortable: false, aTargets: [7], sClass: 'text-center' }             
                    ],
                    order: [[0, 'desc']],
                    bSort: false,
                    rowCallback: function (row, data, dataIndex) {
                        $(row).find('a[name=slnkEdit]').on('click', function () {
                            Category._Search.fnCategoryGet(data[0]);
                        }).tooltip();
                        $(row).find('a[name=slnkInactive]').on('click', function () {
                            Category._Operation.fnCategoryChangeState(data[0], Uti.Variable.StateType.Inactive);
                        }).tooltip();
                        $(row).find('a[name=slnkActive]').on('click', function () {
                            Category._Operation.fnCategoryChangeState(data[0], Uti.Variable.StateType.Active);
                        }).tooltip();
                    },
                    drawCallback: function (data) {
                        const response = data.json;
                    }
                });
            },
            fnCategoryGet: function (CategoryID) {
                const options = {
                    url: Uti.Url.Base + '/Product/Category/CategoryGet/' + CategoryID,
                    type: Uti.Variable.FetchAjax.Type.Get
                };
                Uti.Ajax.Custom(options, function (response) {
                    Uti.Modal.Message(response.type, response.message, response.function);
                    if (response.type === Uti.Message.Type.Session) {
                        Uti.Modal.Process();
                    }
                    if (response.type === Uti.Message.Type.Query) {
                        const { data: rowData } = response;
                        Category._Clear.fnCategoryGet();
                        $('#txtCategoryID').val(rowData.categoryId);                      
                        $('#txtCategoryName').val(rowData.categoryName.trim());
                        $('#txtCategorySlug').val(rowData.categorySlug.trim());                      
                        $('#chkStateID').attr('checked', rowData.recordStateID == Uti.Variable.StateType.Active); 
                        $('#hdCategoryImage').val(rowData.categoryImage.trim());
                        Uti.Image.Preview('imgCategoryImage', rowData.categoryUrl.trim());
                        if (rowData.categoryImage.trim() != '') $('#btnQuitar').show();                         
                        $('#category-card ul li a[href="#tab-search"]').addClass('disabled');
                        $('#category-card ul li a[href="#tab-search"]').removeAttr('data-bs-toggle'); 
                        $('#category-card ul li a[href="#tab-register"]').tab('show');
                        if ($('#btnCategoryUpdate').length > 0) $('#btnCategoryUpdate').show();
                        if ($('#btnCategoryCreate').length > 0) $('#btnCategoryCreate').hide();
                    };
                });
            }
        },
        _Operation: {
            fnCategoryChangeState: function (CategoryId,StateID) {
                const options = {
                    url: Uti.Url.Base + '/Product/Category/CategoryChangeState',
                    data: {
                        CategoryId: CategoryId,
                        RecordStateId: StateID
                    },
                    type: Uti.Variable.FetchAjax.Type.Put
                };
                Uti.Ajax.Custom(options, function (response) {
                    Uti.Modal.Message(response.type, response.message, response.function);
                    if (response.type === Uti.Message.Type.Session) {
                        Uti.Modal.Process();
                    }
                    if (response.type === Uti.Message.Type.Success) {
                        Category._Search.fnCategoryDataTable();
                    }
                });
            },
            fnCategoryCreateUpdate: function () {
                if ($('#frmCategoryCreateUpdate').valid()) {
                    const file = document.getElementById('profile-img-file-input').files[0];
                    if (file) {
                        if (!(file.type == 'image/png' || file.type == 'image/jpeg' || file.type == 'image/jpg')) {
                            Uti.Modal.Message(Uti.Message.Type.Warning, 'Solo se admite archivos con extensión: (jpg,png,jpeg)');
                            return;
                        };
                    };
                    const CategoryId = $('#txtCategoryID').val() == 'GENERADO' ? 0 : $('#txtCategoryID').val();                 

                    var formData = new FormData();
                    formData.append('CategoryId', CategoryId);
                    formData.append('CategoryName', $('#txtCategoryName').val().trim());
                    formData.append('CategorySlug', $('#txtCategorySlug').val().trim());                 
                    formData.append('RecordStateId', $('#chkStateID').is(':checked') ? Uti.Variable.StateType.Active : Uti.Variable.StateType.Inactive);
                    formData.append('FormFile', file);
                    formData.append('CategoryImage', $('#hdCategoryImage').val().trim());
                    formData.append('CategoryImageBandera', $('#hdCategoryImageBandera').val().trim());

                    const options = {
                        url: Uti.Url.Base + '/Product/Category/' + (CategoryId == 0 ? 'CategoryCreate' : 'CategoryUpdate') + '',
                        data: formData,
                        type: CategoryId == 0 ? Uti.Variable.FetchAjax.Type.Post : Uti.Variable.FetchAjax.Type.Put
                    };
                    Uti.Ajax.Custom(options, function (response) {
                        Uti.Modal.Message(response.type, response.message, response.function);
                        if (response.type === Uti.Message.Type.Session) {
                            Uti.Modal.Process();
                        };
                        if (response.type === Uti.Message.Type.Success) {
                            Uti.Modal.Process();
                            Category._Search.fnCategoryDataTable();
                            Category._Clear.fnCategoryGet();
                        };
                    });
                }
            },
        }
    }
    Category._Init();
});