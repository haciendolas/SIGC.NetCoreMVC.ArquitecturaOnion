$(function () {
    let RoleValidate = null;
    const Role = {     
        _Init: function () {
            Role._Validation.fnRoleCreateUpdateValidate();
            Role._Other.fnRoleTabs();
            Role._Search.fnRoleDataTable();
            Role._Search.fnPageTreeViewWithAction();
            $('#scboStateID').on('change', function () {
                Role._Search.fnRoleDataTable();
            });
            $('#stxtRoleName').on('keyup', Uti.SetTimeout.Debounce((event) => {
                const keyCode = event.keyCode ? event.keyCode : event.which;
                if (!(keyCode == 32 || keyCode == '32')) {
                    Role._Search.fnRoleDataTable();
                };
               })
            ); 
            $('#btn-modal-yes').on('click', function () {
                Role._Operation.fnRoleChangeState($('#message-modal-generic #hd-modal-id').val(), Uti.Variable.StateType.Delete);
            });
            if ($('#btnRoleCreate').length) { 
                $('#btnRoleCreate').on('click', function () {
                    Role._Operation.fnRoleCreateUpdate();
                });
            };
            if ($('#btnRoleUpdate').length) {
                $('#btnRoleUpdate').hide();
                $('#btnRoleUpdate').on('click', function () {
                    Role._Operation.fnRoleCreateUpdate();
                });
            };
            $('#btnRoleNew').on('click', function () {
                Role._Clear.fnRoleGet();               
            });
            if ($('#cboCompanyID').length) {
                $('#cboCompanyID').on('change', function () {
                    Role._Search.fnRoleDataTable();
                });
            };
        },
        _Clear: {
            fnRoleGet: function () {
                $('#txtRoleID').val('GENERADO');
                $('#txtRoleCode,#txtRoleName,#txtRoleDescription').val('');
                $('#chkStateID').prop('checked', true);
                $('#div-treeview-page input:checkbox').prop('checked', false);
                if ($('#btnRoleUpdate').length) $('#btnRoleUpdate').hide();
                if ($('#btnRoleCreate').length) $('#btnRoleCreate').show();              
                Role._Other.fnRoleTabs();
                Role._Validation.fnRoleCreateUpdateReset();
                $('#txtRoleCode').focus();
                if ($('#cboCompanyID').length) $('#cboCompanyID').removeAttr('disabled');
            }
        },
        _Other: {
            fnRoleTabs: function () {
                $('#role-card ul li a[href="#tab-search"]').removeClass('disabled');
                $('#role-card ul li a[href="#tab-search"]').attr('data-bs-toggle', 'tab');
              //$('#role-card ul li a[href="#tab-register"]').tab('show');
            }            
        },
        _Validation: {
            fnRoleCreateUpdateReset: function () {
                RoleValidate.resetForm();
                $('#frmRoleCreateUpdate *').removeClass(['invalid-feedback', 'is-invalid']);
            },
            fnRoleCreateUpdateValidate: function () {
                RoleValidate = $('#frmRoleCreateUpdate').validate({
                    rules: {
                        RoleCode: { required: true, minlength: 2, maxlength: 5 },
                        RoleName: { required: true, minlength: 5, maxlength: 50 }                       
                    },
                    messages: {
                        RoleCode: { required: '*Campo requerido', minlength: '*Mínimo 2 caracteres', maxlength: '*Máximo 5 caracteres' },
                        RoleName: { required: '*Campo requerido', minlength: '*Mínimo 5 caracteres', maxlength: '*Máximo 50 caracteres' }                        
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
            fnRoleDataTable: function () {
                $('#dtRol').dataTable({
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
                        const input = $('#dtRol_filter input');
                        input.removeClass().addClass('form-control');
                        input.attr({ placeholder: 'Buscar rol...', type: 'text' });
                        input.off();
                        input.on('keyup', Uti.SetTimeout.Debounce((event) => {
                                    const valor = event.target.value;
                                    const keyCode = event.keyCode ? event.keyCode : event.which;
                                    if (!(keyCode == 32 || keyCode == '32')) {
                                        $('#dtRol').DataTable().search(valor).draw();
                                    };
                                 })
                         );
                    },
                    bJQueryUI: false,
                    bAutoWidth: false,
                    bDestroy: true,
                    sServerMethod: "POST",
                    sAjaxSource: Uti.Url.Base + '/Security/Role/RoleDataTable',
                    fnServerParams: function (aoData) {
                        aoData.push(
                            { name: 'sStateID', value: $('#scboStateID').val() },
                            { name: 'sCompanyID', value: $('#cboCompanyID').val() },
                            { name: 'sSearch', value: $('#stxtRoleName').val().trim() }
                        );
                    },
                    sPaginationType: 'full_numbers',
                    aoColumnDefs: [
                        { bSortable: true, aTargets: [0], sClass: 'text-center' },
                        { bSortable: true, aTargets: [1], sClass: 'text-center' },
                        { bSortable: true, aTargets: [2], sClass: 'text-left' },
                        { bSortable: false, aTargets: [3], sClass: 'text-center' },
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
                            Role._Search.fnRoleGet(data[0]);
                        }).tooltip();
                        $(row).find('a[name=slnkInactive]').on('click',function () {
                            Role._Operation.fnRoleChangeState(data[0], Uti.Variable.StateType.Inactive);                           
                        }).tooltip();
                        $(row).find('a[name=slnkActive]').on('click',function () {                           
                            Role._Operation.fnRoleChangeState(data[0], Uti.Variable.StateType.Active);
                        }).tooltip();
                        $(row).find('a[name=slnkDelete]').on('click',function () {
                            Uti.Modal.Message(Uti.Message.Type.ConfirmDelete);
                            $('#message-modal-generic #hd-modal-id').val(data[0]);
                        }).tooltip();
                    },
                    drawCallback: function (data) {
                        const response = data.json;                         
                    }
                });
            },
            fnPageTreeViewWithAction: function () {
                const CompanyID = $('#cboCompanyID').val();              
                const options = {
                    url: Uti.Url.Base + '/Security/Page/PageTreeViewWithAction/' + CompanyID,               
                    type: Uti.Variable.FetchAjax.Type.Get
                };
                Uti.Ajax.Custom(options, function (response) { 
                    Uti.Modal.Message(response.type, response.message, response.function);
                    if (response.type === Uti.Message.Type.Session) {
                        Uti.Modal.Process();                 
                    }
                    if (response.type === Uti.Message.Type.Query) {
                        $('#div-treeview-page').html(response.data).treeview({
                            collapsed: false,
                            animated: 'medium',
                            control: '#sidetreecontrol',
                            persist: 'location'
                        });
                    }
                });      
            },
            fnRoleGet: function (RoleID) {              
                const options = {
                    url: Uti.Url.Base + '/Security/Role/RoleGet/' + RoleID,
                    type: Uti.Variable.FetchAjax.Type.Get
                };
                Uti.Ajax.Custom(options, function (response) {
                    Uti.Modal.Message(response.type, response.message, response.function);
                    if (response.type === Uti.Message.Type.Session) {
                        Uti.Modal.Process();
                    }
                    if (response.type === Uti.Message.Type.Query) {
                        const { data: rowData } = response;                      
                        Role._Clear.fnRoleGet();
                        $('#txtRoleID').val(rowData.roleID);
                        $('#txtRoleCode').val(rowData.roleCode.trim());
                        $('#txtRoleName').val(rowData.roleName.trim());
                        $('#txtRoleDescription').val(rowData.roleDescription.trim());
                        $('#chkStateID').prop('checked', rowData.stateID == Uti.Variable.StateType.Active);
                        if ($('#cboCompanyID').length)  $('#cboCompanyID').attr('disabled', 'disabled');
                        rowData.pages.forEach(page => {
                            $('#div-treeview-page input:checkbox[name=chkPageID]').each(function (pageIndex, pageElement) {
                                if (page.pageID == $(pageElement).val()) {
                                    $(pageElement).prop('checked', true);
                                    return false;
                                }
                            });
                            page.actions.forEach(action => {
                                $('#' + page.pageID + ' input:checkbox[name=chkPageActionID]').each(function (actionIndex, actionElement) {
                                    if (action.pageActionID == $(actionElement).val()) {
                                        $(actionElement).prop('checked', true);
                                        return ;
                                    }
                                });
                            });
                        });

                        $('#role-card ul li a[href="#tab-search"]').addClass('disabled');
                        $('#role-card ul li a[href="#tab-search"]').removeAttr('data-bs-toggle');
                        $('#role-card ul li a[href="#tab-register"]').tab('show');
                        if ($('#btnRoleUpdate').length) $('#btnRoleUpdate').show();
                        if ($('#btnRoleCreate').length) $('#btnRoleCreate').hide();
                    };
                });
            }
        },
        _Operation: {
            fnRoleChangeState: function (RoleID, StateID) {
                const options = {
                    url: Uti.Url.Base + '/Security/Role/RoleChangeState',
                    data: {
                        CompanyID: $('#cboCompanyID').val(),
                        RoleID: RoleID,
                        StateID: StateID
                    },
                    type: Uti.Variable.FetchAjax.Type.Post
                };
                Uti.Ajax.Custom(options, function (response) { 
                    Uti.Modal.Message(response.type, response.message, response.function);
                    if (response.type === Uti.Message.Type.Session) {
                        Uti.Modal.Process();
                    }
                    if (response.type === Uti.Message.Type.Success) {
                        Role._Search.fnRoleDataTable();
                    }
                });              
            },
            fnRoleCreateUpdate: function () {
              if ($('#frmRoleCreateUpdate').valid()) { 
                const RolePermission = new Array();
                $('#div-treeview-page input:checkbox[name=chkPageID]:checked').each(function (pageIndex, pageElement) {
                    RolePermission.push({
                        PageID: parseInt($(pageElement).val()),
                        PageActionID: 0
                    });
                });
                $('#div-treeview-page input:hidden[name=chkPageID]').each(function (pageIndex, pageElement) {
                    const PageID = parseInt($(pageElement).val());
                    $('#' + PageID + ' input:checkbox[name=chkPageActionID]:checked').each(function (pageActionIndex, pageActionElement) {
                        RolePermission.push({
                            PageID: PageID,
                            PageActionID: parseInt($(pageActionElement).val())
                        });
                    });
                });
                const RoleID = parseInt($('#txtRoleID').val() === 'GENERADO' ? 0 : $('#txtRoleID').val())
                const options = {
                    url: Uti.Url.Base + '/Security/Role/' + (RoleID == 0 ? 'RoleCreate' : 'RoleUpdate') + '',
                    data: {
                        RoleID: RoleID,
                        CompanyID: $('#cboCompanyID').val(),
                        RoleCode: $('#txtRoleCode').val().trim(),
                        RoleName: $('#txtRoleName').val().trim(),
                        RoleDescription: $('#txtRoleDescription').val().trim(),
                        StateID: $('#chkStateID').is(':checked') ? 1 : 0,
                        RolePermission: RolePermission
                    },
                    type: RoleID == 0 ? Uti.Variable.FetchAjax.Type.Post : Uti.Variable.FetchAjax.Type.Put
                };
                Uti.Ajax.Custom(options, function (response) {
                    Uti.Modal.Message(response.type, response.message, response.function);
                    if (response.type === Uti.Message.Type.Session) {
                        Uti.Modal.Process();
                    }
                    if (response.type === Uti.Message.Type.Success) {
                        Role._Clear.fnRoleGet();
                        Role._Search.fnRoleDataTable();
                    }
                });
             }
           }
        }
    }
    Role._Init();
});