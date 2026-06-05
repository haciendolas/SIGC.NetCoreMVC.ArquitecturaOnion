$(function () {
    let UserValidate = null;
    const User = {
        _Init: function () {
            User._Validation.fnUserCreateUpdateValidate();
            User._Search.fnUserDataTable();
            User._Search.fnRoleCheckbox();
            User._Other.fnOpenFile();     
            User._Other.fnUserTabs();
            $('#txtsUserFullName,#txtUserLastName,#txtUserFirstName').keypress(function (event) {
                return Uti.KeyBoard.LettersAndNumbers(event);
            });
            $('#scboStateID').on('change', function () {
                User._Search.fnUserDataTable();
            });
            $('#txtsUserName,#txtsUserFullName').on('keyup', Uti.SetTimeout.Debounce((event) => {
                const keyCode = event.keyCode ? event.keyCode : event.which;
                 if (!(keyCode == 32 || keyCode == '32')) {
                     User._Search.fnUserDataTable();
                 };
               })
            );
            $('#btn-modal-yes').on('click', function () {
                User._Operation.fnUserCompanyChangeState(parseInt($('#message-modal-generic #hd-modal-id').val()), Uti.Variable.StateType.Delete);
            });
            if ($('#btnUserCreate').length>0) {
                $('#btnUserCreate').on('click', function () {
                    User._Operation.fnUserCreateUpdate();
                });
            };
            if ($('#btnUserUpdate').length>0) {
                $('#btnUserUpdate').hide();
                $('#btnUserUpdate').on('click', function () {
                    User._Operation.fnUserCreateUpdate();
                });
            };
            $('#btnUserNew').on('click', function () {
                User._Clear.fnUserCompanyGet();
            });
            if ($('#cboCompanyID').length) {
                $('#cboCompanyID').on('change', function () {
                    User._Search.fnUserDataTable();
                    User._Search.fnRoleCheckbox();
                });
            };
            $('#btnQuitar').hide();
            $('#btnQuitar').on('click', function () {
                Uti.Image.Preview('imgUserPhoto');
                $('#profile-img-file-input').val('');
                $(this).hide();
                $('#hdUserPhotoBandera').val('DELETE');
            });
        },
        _Clear: {
            fnUserCompanyGet: function () {
                $('#txtUserID').val('GENERADO');
                $('#txtUserLastName,#txtUserFirstName,#txtUserName,#txtUserPassword').val(''); 
                $('#txtUserMail,#hdUserPhoto,#hdUserPhotoBandera').val(''); 
                $('#chkStateID').prop('checked', true);
                $('#div-roles input:checkbox').prop('checked', false);
                if ($('#btnUserUpdate').length > 0) $('#btnUserUpdate').hide();
                if ($('#btnUserCreate').length > 0) $('#btnUserCreate').show();
                User._Other.fnUserTabs();
                User._Validation.fnUserCreateUpdateReset();
                Uti.Image.Preview('imgUserPhoto');
                $('#txtUserLastName').focus();                 
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
                        $('#imgUserPhoto').fadeIn('fast').attr('src', tmppath);  
                        $('#btnQuitar').show();
                        $('#hdUserPhotoBandera').val('');
                    }
                });
            },
            fnUserTabs: function () {
                $('#user-card ul li a[href="#tab-search"]').removeClass('disabled');
                $('#user-card ul li a[href="#tab-search"]').attr('data-bs-toggle', 'tab');            
            }   
        },
        _Validation: {
            fnUserCreateUpdateReset: function () {
                UserValidate.resetForm();
                $('#frmUserCreateUpdate *').removeClass(['invalid-feedback', 'is-invalid']);
            },
            fnUserCreateUpdateValidate: function () {
                UserValidate = $('#frmUserCreateUpdate').validate({
                    rules: {                        
                        UserLastName: { required: true, minlength: 3, maxlength: 30 }, 
                        UserFirstName: { required: true, minlength: 3, maxlength: 50 },
                        UserName: { required: true, minlength: 3, maxlength: 15 },
                        UserPassword: { required: true, minlength: 3, maxlength: 20 },                       
                        UserMail: { required: false, email: true, minlength: 10, maxlength: 100 }                       
                    },
                    messages: { 
                        UserLastName: { required: '*Campo requerido', minlength: '*Mínimo 3 caracteres', maxlength: '*Máximo 30 caracteres' }, 
                        UserFirstName: { required: '*Campo requerido', minlength: '*Mínimo 3 caracteres', maxlength: '*Máximo 50 caracteres' },
                        UserName: { required: '*Campo requerido', minlength: '*Mínimo 3 caracteres', maxlength: '*Máximo 15 caracteres' },
                        UserPassword: { required: '*Campo requerido', minlength: '*Mínimo 3 caracteres', maxlength: '*Máximo 20 caracteres' }, 
                        UserMail: { email: '*Campo formato incorrecto', minlength: '*Mínimo 10 caracteres', maxlength: '*Máximo 100 caracteres' }               
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
            fnUserDataTable: function () {
                $('#dtUser').dataTable({
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
                        const input = $('#dtUser_filter input');
                        input.removeClass().addClass('form-control');
                        input.attr({ placeholder: 'Buscar usuario...', type: 'text' });
                        input.off();
                        input.on('keyup', Uti.SetTimeout.Debounce((event) => {
                            const valor = event.target.value;
                            const keyCode = event.keyCode ? event.keyCode : event.which;
                            if (!(keyCode == 32 || keyCode == '32')) {
                                $('#dtUser').DataTable().search(valor).draw();
                            };
                        })
                        );
                    },
                    bJQueryUI: false,
                    bAutoWidth: false,
                    bDestroy: true,
                    sServerMethod: "POST",
                    sAjaxSource: Uti.Url.Base + '/Security/User/UserDataTable',
                    fnServerParams: function (aoData) {
                        aoData.push(
                            { name: 'StateID', value: $('#scboStateID').val() },
                            { name: 'CompanyID', value: $('#cboCompanyID').val() },
                            { name: 'sSearch', value: $('#txtsUserName').val().trim() },
                            { name: 'UserFullName', value: $('#txtsUserFullName').val().trim() }
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
                        { bSortable: false, aTargets: [8], sClass: 'text-center' },
                        { bSortable: false, aTargets: [9], sClass: 'text-center' },
                        { bSortable: false, aTargets: [10], sClass: 'text-center' }
                    ],
                    order: [[0, 'desc']],
                    bSort: false,
                    rowCallback: function (row, data, dataIndex) {
                        $(row).find('a[name=slnkEdit]').on('click', function () {
                            User._Search.fnUserCompanyGet(data[0]);
                        }).tooltip();
                        $(row).find('a[name=slnkInactive]').on('click', function () {
                            User._Operation.fnUserCompanyChangeState(data[0], Uti.Variable.StateType.Inactive);
                        }).tooltip();
                        $(row).find('a[name=slnkActive]').on('click', function () {
                            User._Operation.fnUserCompanyChangeState(data[0], Uti.Variable.StateType.Active);
                        }).tooltip();
                        $(row).find('a[name=slnkDelete]').on('click', function () {
                            Uti.Modal.Message(Uti.Message.Type.ConfirmDelete);
                            $('#message-modal-generic #hd-modal-id').val(data[0]);
                        }).tooltip();
                    },
                    drawCallback: function (data) {
                        const response = data.json;
                    }
                });
            },
            fnRoleCheckbox: function () {            
                const CompanyID = $('#cboCompanyID').val();
                const options = {
                    url: Uti.Url.Base + '/Security/Role/RoleList/' + CompanyID,
                    type: Uti.Variable.FetchAjax.Type.Get
                };
                Uti.Ajax.Custom(options, function (response) {
                    Uti.Modal.Message(response.type, response.message, response.function);
                    if (response.type === Uti.Message.Type.Session) {
                        Uti.Modal.Process();
                    };
                    if (response.type === Uti.Message.Type.Query) {
                        $('#div-roles').html('');
                        const { data: rowData } = response;                     
                        rowData.forEach(item => {
                            const myHtml = '<div class="form-check form-check-secondary mb-2">'
                                        + '<input class="form-check-input" style="width:20px;height:20px" type="checkbox" id="chkRoleID_' + item.roleID + '" name = "chkRoleID" value=' + item.roleID + '>'
                                        + '<label class="form-check-label p-1" for="chkRoleID_'+item.roleID+'">'
                                        +  item.roleName
                                        + '</label>'
                                        + '</div>';
                            $('#div-roles').append(myHtml);
                        });
                    };
                });
            },
            fnUserCompanyGet: function (UserID) {
                const CompanyID =  $('#cboCompanyID').val();
                const options = {
                    url: Uti.Url.Base + '/Security/User/UserCompanyGet/' + UserID + '/' + CompanyID,
                    type: Uti.Variable.FetchAjax.Type.Get
                };
                Uti.Ajax.Custom(options, function (response) {
                    Uti.Modal.Message(response.type, response.message, response.function);
                    if (response.type === Uti.Message.Type.Session) {
                        Uti.Modal.Process();
                    }
                    if (response.type === Uti.Message.Type.Query) {
                        const { data: rowData } = response;
                        User._Clear.fnUserCompanyGet();  
                        $('#txtUserID').val(rowData.userID);    
                        $('#chkStateID').attr('checked', rowData.stateID == Uti.Variable.StateType.Active);
                        $('#txtUserLastName').val(rowData.userLastName.trim());     
                        $('#txtUserFirstName').val(rowData.userFirstName.trim());
                        $('#txtUserName').val(rowData.userName.trim());
                        $('#txtUserPassword').val(rowData.userPassword.trim());
                        $('#txtUserMail').val(rowData.userMail.trim()); 
                        $('#hdUserPhoto').val(rowData.userPhoto.trim());
                        Uti.Image.Preview('imgUserPhoto', rowData.userUrl.trim());
                        if ($('#cboCompanyID').length > 0) $('#cboCompanyID').attr('disabled', 'disabled');
                        if (rowData.userPhoto.trim() != '') $('#btnQuitar').show();
                        rowData.roleIDs.forEach(roleID => {
                            $('#div-roles input:checkbox[name=chkRoleID]').each(function (roleIndex, roleElement) {
                                if (roleID == $(roleElement).val()) {
                                    $(roleElement).prop('checked', true);
                                    return false;
                                }
                            });
                        });
                        $('#user-card ul li a[href="#tab-search"]').addClass('disabled');
                        $('#user-card ul li a[href="#tab-search"]').removeAttr('data-bs-toggle');          
                        $('#user-card ul li a[href="#tab-register"]').tab('show');
                        if ($('#btnUserUpdate').length > 0) $('#btnUserUpdate').show();
                        if ($('#btnUserCreate').length > 0) $('#btnUserCreate').hide();
                    };
                });
            }
        },
        _Operation: {
            fnUserCompanyChangeState: function (UserID, StateID) {
                const options = {
                    url: Uti.Url.Base + '/Security/User/UserCompanyChangeState',
                    data: {
                        CompanyID: $('#cboCompanyID').val(),
                        UserID: UserID,
                        StateID: StateID
                    },
                    type: Uti.Variable.FetchAjax.Type.Put
                };
                Uti.Ajax.Custom(options, function (response) {
                    Uti.Modal.Message(response.type, response.message, response.function);
                    if (response.type === Uti.Message.Type.Session) {
                        Uti.Modal.Process();
                    }
                    if (response.type === Uti.Message.Type.Success) {
                        User._Search.fnUserDataTable();
                    }
                });
            },
            fnUserCreateUpdate: function () {
                if ($('#frmUserCreateUpdate').valid()) {
                    const file = document.getElementById('profile-img-file-input').files[0];
                    if (file) {
                        if (!(file.type == 'image/png' || file.type == 'image/jpeg' || file.type == 'image/jpg')) {
                            Uti.Modal.Message(Uti.Message.Type.Warning, 'Solo se admite archivos con extensión: (jpg,png,jpeg)');
                            return;
                        };
                    }; 
                    const UserID = $('#txtUserID').val() == 'GENERADO' ? 0 : $('#txtUserID').val(); 

                    var formData = new FormData();
                    formData.append('CompanyID', $('#cboCompanyID').val());
                    formData.append('UserID', UserID);
                    formData.append('UserFirstName', $('#txtUserFirstName').val().trim());
                    formData.append('UserLastName', $('#txtUserLastName').val().trim());
                    formData.append('UserName', $('#txtUserName').val().trim());
                    formData.append('UserPassword', $('#txtUserPassword').val().trim());
                    formData.append('UserMail', $('#txtUserMail').val().trim());  
                    formData.append('StateID', $('#chkStateID').is(':checked') ? Uti.Variable.StateType.Active : Uti.Variable.StateType.Inactive);
                    formData.append('FormFile', file);
                    formData.append('UserPhoto', $('#hdUserPhoto').val().trim());
                    formData.append('UserPhotoBandera', $('#hdUserPhotoBandera').val().trim());
                    $('#div-roles input:checkbox[name=chkRoleID]:checked').each(function (roleIndex, roleElement) { 
                        formData.append('RoleIDs', $(roleElement).val());
                    }); 

                    const options = {
                        url: Uti.Url.Base + '/Security/User/' + (UserID == 0 ? 'UserCreate' : 'UserUpdate') + '',
                        data: formData,
                        type: UserID == 0 ? Uti.Variable.FetchAjax.Type.Post : Uti.Variable.FetchAjax.Type.Put
                    };
                    Uti.Ajax.Custom(options, function (response) {
                        Uti.Modal.Message(response.type, response.message, response.function);
                        if (response.type === Uti.Message.Type.Session) {
                            Uti.Modal.Process();
                        };
                        if (response.type === Uti.Message.Type.Success) {
                            Uti.Modal.Process();
                            User._Search.fnUserDataTable();
                            User._Clear.fnUserCompanyGet();
                        };
                    });
                }
            }
        }
    };
    User._Init();
});