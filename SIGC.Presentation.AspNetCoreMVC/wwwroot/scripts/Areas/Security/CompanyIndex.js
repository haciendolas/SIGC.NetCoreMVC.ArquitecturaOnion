$(function () {
    const Company = {
        _Init: function () {          
            Company._Search.fnCompanyDataTable();
            Company._Search.fnPageTreeView();
            $('#stxtCompanyDocumentNumber').keypress(function (event) {
                return Uti.KeyBoard.Numbers(event);
            });
            $('#stxtCompanySocialReason').keypress(function (event) {
                return Uti.KeyBoard.LettersAndNumbers(event);
            });
            $('#stxtCompanyDocumentNumber,#stxtCompanySocialReason').on('keyup', Uti.SetTimeout.Debounce((event) => {
                const keyCode = event.keyCode ? event.keyCode : event.which;
                if (!(keyCode == 32 || keyCode == '32')) {
                    Company._Search.fnCompanyDataTable();
                };
               })
            );
            $('#scboTaxpayerTypeID,#scboRubroID,#scboStateID').on('change', function(){
                Company._Search.fnCompanyDataTable();
            });
        },
        _Search: {
            fnCompanyDataTable: function () {             
                $('#dtCompany').dataTable({
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
                                $('#dtCompany').DataTable().search(valor).draw();
                            };
                        })
                        );
                    },
                    bJQueryUI: false,
                    bAutoWidth: false,
                    bDestroy: true,
                    sServerMethod: "POST",
                    sAjaxSource: Uti.Url.Base + '/Security/Company/CompanyDataTable',
                    fnServerParams: function (aoData) {
                        aoData.push( 
                            { name: 'sTaxpayerTypeID', value: $('#scboTaxpayerTypeID').val() },
                            { name: 'sRubroID', value: $('#scboRubroID').val() },
                            { name: 'sStateID', value: $('#scboStateID').val() },
                            { name: 'sCompanyDocumentNumber', value: $('#stxtCompanyDocumentNumber').val().trim() },
                            { name: 'sCompanySocialReason', value: $('#stxtCompanySocialReason').val().trim() },
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
                            Role._Search.fnRoleGet(data[0]);
                        }).tooltip();
                        $(row).find('a[name=slnkInactive]').on('click', function () {
                            Company._Operation.fnCompanyChangeState(data[0], Uti.Variable.StateType.Inactive);
                        }).tooltip();
                        $(row).find('a[name=slnkActive]').on('click', function () {
                            Company._Operation.fnCompanyChangeState(data[0], Uti.Variable.StateType.Active);
                        }).tooltip();              
                    },
                    drawCallback: function (data) {
                        const response = data.json;
                    }
                });
            },
            fnPageTreeView: function () {
                const options = {
                    url: Uti.Url.Base + '/Security/Page/PageList',
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
            fnCompanyChangeState: function (CompanyID, StateID) {
                const options = {
                    url: Uti.Url.Base + '/Security/Company/CompanyChangeState',
                    data: {               
                        CompanyID: CompanyID,
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
                        Company._Search.fnCompanyDataTable();
                    }
                });
            }
        }
    };
    Company._Init();
});