$(function () {
    const Role = {
        _Init: function () {   
            Role._Search.fnRoleDataTable();       
            Role._Search.fnPageTreeView();
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
            $('#sbtnBuscar').on('click', function () {
                Role._Search.fnRoleDataTable();
            });
            $('#btn-modal-yes').on('click', function () {
                Role._Operation.fnRoleChangeState(1, $('#message-modal-generic #hd-modal-id').val(), Uti.Variable.StateType.Delete);
            });    
            $('#btnRoleCreate').on('click', function () {
                Role._Operation.fnRoleCreate();
            });
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
                            { name: 'sCompanyID', value: $('#scboCompanyID').val() },
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
                        $(row).find('a[name=slnkEdit]').on('click',function () {                   
                        }).tooltip();
                        $(row).find('a[name=slnkInactive]').on('click',function () {
                            Role._Operation.fnRoleChangeState(1, data[0], Uti.Variable.StateType.Inactive);                           
                        }).tooltip();
                        $(row).find('a[name=slnkActive]').on('click',function () {                           
                            Role._Operation.fnRoleChangeState(1, data[0], Uti.Variable.StateType.Active);
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
            fnPageTreeView: function () {
                const CompanyID = 1;              
                const options = {
                    url: Uti.Url.Base + '/Security/Role/PageList/' + CompanyID,               
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
            }
        },
        _Operation: {
            fnRoleChangeState: function (CompanyID, RoleID, StateID) {
                const options = {
                    url: Uti.Url.Base + '/Security/Role/RoleChangeState',
                    data: {
                        CompanyID: CompanyID,
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
            fnRoleCreate: function () {
                debugger
                const RolePermission = new Array();
                $('#div-treeview-page input:checkbox[name=chkPageID]:checked').each(function (pageIndex, pageElement) {            
                    RolePermission.push({
                        PageID: parseInt($(pageElement).val()),
                        PageActionID: 0
                    });
                });
                $('#div-treeview-page input:hidden[name=chkPageID]').each(function (pageIndex, pageElement) {
                    debugger
                    const PageID = parseInt($(pageElement).val());
                    $('#' + PageID + ' input:checkbox[name=chkPageActionID]:checked').each(function (pageActionIndex, pageActionElement){
                        RolePermission.push({
                            PageID: PageID,
                            PageActionID: parseInt($(pageActionElement).val())
                        });
                    });
                });         
                const options = {
                    url: Uti.Url.Base + '/Security/Role/RoleCreate',
                    data: {
                        RoleID: parseInt($('#txtRoleID').val() === 'GENERADO' ? 0 : $('#txtRoleID').val()),
                        CompanyID: 1,
                        RoleCode: $('#txtRoleCode').val().trim(),
                        RoleName: $('#txtRoleName').val().trim(),
                        RoleDescription: $('#txtRoleDescription').val().trim(),
                        StateID: $('#chkStateID').is(':checked') ? 1 : 0,
                        RolePermission: RolePermission
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
            }
        }
    }
    Role._Init();
});