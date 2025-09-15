$(function () {
    const Role = {
        _Init: function () {   
            Role._Library.fnDataTableRole();
            $('#scboStateID').on('change', function () {
                Role._Library.fnDataTableRole();
            });
            $('#stxtRoleName').on('keyup', Uti.SetTimeout.Debounce((event) => {
                        const keyCode = event.keyCode ? event.keyCode : event.which;           
                        if (!(keyCode == 32 || keyCode == '32')) {
                            Role._Library.fnDataTableRole();
                        };
                    })
            );
            $('#sbtnBuscar').on('click', function () {
                Role._Library.fnDataTableRole();
            });
        },
        _Search: {

        },
        _Library: {
            fnDataTableRole: function(){
                $('#dtRol').dataTable({
                    oLanguage: {
                        sUrl: Uti.DataTable.sUrl,
                    },
                    bProcessing: true,
                    bServerSide: true,
                    iDisplayLength: Uti.DataTable.iDisplayLength.NumRows10,
                    sDom: '<r>t<Fp>',
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
                        { bSortable: false,aTargets: [3], sClass: 'text-center' },
                        { bSortable: true, aTargets: [4], sClass: 'text-center' },
                        { bSortable: true, aTargets: [5], sClass: 'text-center' },
                        { bSortable: false,aTargets: [6], sClass: 'actions text-center' },
                    ],
                    order: [[0, 'desc']],
                    bSort: false,
                    rowCallback: function (row, data, dataIndex) {
                        $(row).find('a[name=linkEditar]').click(function () {
                            //Role._Search.fnGetRole(data[0]);
                        });
                        $(row).find('a[name=linkActivar]').click(function () {
                         //   Role._Operation.fnChangeStateRole(data[0], Uti.Variable.StateType.Active);
                        });
                        $(row).find('a[name=linkDesactivar]').click(function () {
                          //  Role._Operation.fnChangeStateRole(data[0], Uti.Variable.StateType.Inactive);
                        });
                    },
                    drawCallback: function (data) {
                        const response = data.json;                      
                    }
                });
            }
        }
    }
    Role._Init();
});