$(function () {
    const Role = {
        _Init: function () {   
            Role._Search.fnDataTableRole();
            $('#scboStateID').on('change', function () {
                Role._Search.fnDataTableRole();
            });
            $('#stxtRoleName').on('keyup', Uti.SetTimeout.Debounce((event) => {
                        const keyCode = event.keyCode ? event.keyCode : event.which;           
                        if (!(keyCode == 32 || keyCode == '32')) {
                            Role._Search.fnDataTableRole();
                        };
                    })
            );
            $('#sbtnBuscar').on('click', function () {
                Role._Search.fnDataTableRole();
            });
        },
        _Search: {
            fnDataTableRole: function () {
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
                    sAjaxSource: Uti.Url.Base + '/Security/Role/RolePagination',
                    fnServerParams: function (aoData) {
                        aoData.push(
                            { name: 'sStateID', value: $('#scboStateID').val() },
                            { name: 'sCompanyID', value: $('#scboCompanyID').val() },
                            { name: 'sSearch', value: $.trim($('#stxtRoleName').val()) }
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
                        { bSortable: false, aTargets: [9], sClass: 'text-center' }
                    ],
                    order: [[0, 'desc']],
                    bSort: false,
                    rowCallback: function (row, data, dataIndex) {
                        $(row).find('a[name=slnkEdit]').click(function () {
                            //Role._Search.fnGetRole(data[0]);
                        }).tooltip();
                        $(row).find('a[name=slnkInactive]').click(function () {
                            //   Role._Operation.fnChangeStateRole(data[0], Uti.Variable.StateType.Active);
                        }).tooltip();
                        $(row).find('a[name=slnkActive]').click(function () {
                            //  Role._Operation.fnChangeStateRole(data[0], Uti.Variable.StateType.Inactive);
                        }).tooltip();
                        $(row).find('a[name=slnkDelete]').click(function () {
                            //  Role._Operation.fnChangeStateRole(data[0], Uti.Variable.StateType.Inactive);
                        }).tooltip();
                    },
                    drawCallback: function (data) {
                        const response = data.json;
                        //$('#dtRol_filter input').removeClass().addClass('form-control');
                    }
                });
            }
        } 
    }
    Role._Init();
});