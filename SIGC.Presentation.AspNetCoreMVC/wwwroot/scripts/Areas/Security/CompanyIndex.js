$(function () {
    let CompanyValidate = null;
    const Company = {
        _Init: function () {
            Company._Validation.fnCompanyCreateUpdateValidate();
            Company._Search.fnCompanyDataTable();
            Company._Search.fnPageTreeView();
            Company._Other.fnOpenFile();
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
            if ($('#btnCompanyCreate').length) {
                $('#btnCompanyCreate').on('click', function () {
                    Company._Operation.fnCompanyCreateUpdate();
                });
            };
            if ($('#btnCompanyUpdate').length) {
                $('#btnCompanyUpdate').hide();
                $('#btnCompanyUpdate').on('click', function () {
                    Company._Operation.fnCompanyCreateUpdate();
                });
            };
            $('#dtpCompanyBirthDate').val(Uti.Date.Today());      
            $('#span-dtpCompanyBirthDate').on('click', function () {
                $('#dtpCompanyBirthDate').click();
            });
        },
        _Clear: {

        },
        _Other: {
            fnOpenFile: function () {
                $('#profile-img-file-input').on('change', function (event) {
                    debugger
                    const _URL = window.URL || window.webkitURL;  //window.URL para firefox  webkitURL para chrome y otros navegadores
                    const file = event.target.files[0];
                    if (file) {
                        const tmppath = _URL.createObjectURL(file);
                        if (!(file.type == 'image/png' || file.type == 'image/jpeg' || file.type == 'image/jpg')) {
                            Uti.Modal.Message(Uti.Message.Type.Warning, 'Solo se admite archivos con extensión: (jpg,png,jpeg)');
                            return;
                        }
                        if (parseInt(file.size) >= 34000 && parseInt(file.size) <= 35000) {
                            $('#imgCompanyLogo').fadeIn('fast').attr('src', tmppath);
                        }
                        else {
                            $('#imgCompanyLogo').fadeIn('fast').attr('src', tmppath);
                            $('#div-quitar').show();
                        }
                    }
                });
            }
        },
        _Validation: {
            fnCompanyCreateUpdateReset: function () {
                CompanyValidate.resetForm();
                $('#frmCompanyCreateUpdate *').removeClass(['invalid-feedback', 'is-invalid']);
            },
            fnCompanyCreateUpdateValidate: function () {
                CompanyValidate = $('#frmCompanyCreateUpdate').validate({
                    rules: {
                        CountryID: { required: true},
                        CompanyDocumentNumber: { required: true, minlength: 8, maxlength: 11 },
                        CompanyBirthDate: { required: true,date:true },
                        TaxpayerTypeID: { required: true },
                        CompanySocialReason: { required: true, minlength: 20, maxlength: 150 },
                        CompanyTradeName: { required: true, minlength: 20, maxlength: 100 },
                        CompanyAddress: { required: true, minlength: 20, maxlength: 200 },
                        RubroID: { required: true },
                        CompanyCorporateEmail: { required: true, email:true, minlength: 10, maxlength: 150 },
                        CompanyPhone: { required: true, minlength: 6, maxlength: 15 },
                        CompanyMobile: { required: true, minlength: 9, maxlength: 15 },
                    },
                    messages: {
                        CountryID: { required: '*Campo requerido' },
                        CompanyDocumentNumber: { required: '*Campo requerido', minlength: '*Mínimo 8 caracteres', maxlength: '*Máximo 11 caracteres' },
                        CompanyBirthDate: { required: '*Campo requerido', date: '*Campo fecha incorrecto'},
                        TaxpayerTypeID: { required: '*Campo requerido' },
                        CompanySocialReason: { required: '*Campo requerido', minlength: '*Mínimo 20 caracteres', maxlength: '*Máximo 150 caracteres' },
                        CompanyTradeName: { required: '*Campo requerido', minlength: '*Mínimo 20 caracteres', maxlength: '*Máximo 100 caracteres' },
                        CompanyAddress: { required: '*Campo requerido', minlength: '*Mínimo 20 caracteres', maxlength: '*Máximo 200 caracteres' },
                        RubroID: { required: '*Campo requerido' },
                        CompanyCorporateEmail: { required: '*Campo requerido', email: '*Campo formato incorrecto',minlength: '*Mínimo 10 caracteres', maxlength: '*Máximo 150 caracteres' },
                        CompanyPhone: { required: '*Campo requerido', minlength: '*Mínimo 6 caracteres', maxlength: '*Máximo 15 caracteres' },
                        CompanyMobile: { required: '*Campo requerido', minlength: '*Mínimo 9 caracteres', maxlength: '*Máximo 15 caracteres' }
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
            },
            fnCompanyCreateUpdate: function () {
                if ($('#frmCompanyCreateUpdate').valid()) {
                    debugger
                    const file = document.getElementById('profile-img-file-input').files[0]; 
                    if (file) {
                        if (!(file.type == 'image/png' || file.type == 'image/jpeg' || file.type == 'image/jpg')) {
                            Uti.Modal.Message(Uti.Message.Type.Warning, 'Solo se admite archivos con extensión: (jpg,png,jpeg)');
                            return;
                        };           
                    };
                    CompanyID = $('#txtCompanyID').val() == 'GENERADO' ? 0 : $('#txtCompanyID').val();
                    CompanyBirthDate = $('#dtpCompanyBirthDate').val().trim().split('/');
                    
                    var formData = new FormData();
                    formData.append('CompanyId', CompanyID);
                    formData.append('CompanyTradeName', $('#txtCompanyTradeName').val().trim());
                    formData.append('CompanySocialReason', $('#txtCompanySocialReason').val().trim());
                    formData.append('CompanyDocumentNumber', $('#txtCompanyDocumentNumber').val().trim());
                    formData.append('CompanyBirthDate', CompanyBirthDate[2] + '/' + CompanyBirthDate[1] +'/'+ CompanyBirthDate[0]);
                    formData.append('CountryID', $('#cboCountryID').val());
                    formData.append('CompanyAddress', $('#txtCompanyAddress').val().trim());
                    formData.append('TaxpayerTypeID', $('#cboTaxpayerTypeID').val());
                    formData.append('RubroID', $('#cboRubroID').val());
                    formData.append('CompanyMobile', $('#txtCompanyMobile').val().trim());
                    formData.append('CompanyPhone', $('#txtCompanyPhone').val().trim());
                    formData.append('StateID', $('#chkStateID').is(':checked') ? Uti.Variable.StateType.Active : Uti.Variable.StateType.Inactive);
                    formData.append('FormFile', file);

                    const options = {
                        url: Uti.Url.Base + '/Security/Company/' + (CompanyID == 0 ? 'CompanyCreate' : 'CompanyUpdate') + '',
                        data: formData,               
                        type: CompanyID == 0 ? Uti.Variable.FetchAjax.Type.Post : Uti.Variable.FetchAjax.Type.Put
                    };
                    Uti.Ajax.Custom(options, function (response) {
                        Uti.Modal.Message(response.type, response.message, response.function);
                        if (response.type === Uti.Message.Type.Session) {
                            Uti.Modal.Process();
                        }
                        if (response.type === Uti.Message.Type.Success) {
                            Uti.Modal.Process(); 
                        }
                    }); 
                }
            }
        }
    };
    Company._Init();
});
/*
            $('#span-dtpFechaInspeccion').on('click', function () {
                $('#dtpFechaInspeccion').click();
            });

date: true ,
date: '*Campo fecha incorrecto'

 email: true
 email: '*Campo formato incorrecto'
 IFormFile FormFile
        fnOpenFile:function(){
          $('#flLogo').on('change', function (event) {
            const _URL = window.URL || window.webkitURL;  //window.URL para firefox  webkitURL para chrome y otros navegadores
            const file = event.target.files[0];
            if (file) {
              const tmppath = _URL.createObjectURL(file);
              if (!(file.type == 'image/png' || file.type == 'image/jpeg' || file.type == 'image/jpg')) {              
                Uti.Modal.Message(Uti.Message.Type.Error,'Solo se admite archivos con extensión : ( .jpg  ,&nbsp;.png ,&nbsp;.jpeg ) <br /> por ejemplo miarchivo.png'); 
                return;
              }
              if (parseInt(file.size) >= 34000 && parseInt(file.size) <= 35000) {
                $('#imgFoto').fadeIn('fast').attr('src', tmppath);               
              }
              else { 
                $('#imgLogo').fadeIn('fast').attr('src', tmppath);
                $('#div-quitar').show(); 
              }
            }
          });
        },
         fnCatalogGalleryCreateUpdate: function () {
                const file = document.getElementById('flImagen').files[0];
                const galleryTypeId = $("#cboGalleryTypeID").val();
                if (galleryTypeId.length == 0) {
                    alert('Debe seleccionar el tipo de galeria');
                    return;
                }
                if (!file) {
                    alert('Debe buscar la imagen del producto');
                    return;
                }
                if (!(file.type == 'image/png' || file.type == 'image/jpeg' || file.type == 'image/jpg')) {
                    alert('Solo se admite archivos con extensión: (.jpg,.png,.jpeg)');
                    return;
                }

                const CatalogId = parseInt($("#hdCatalogIDGallery").val());
                var formData = new FormData();
                    formData.append('CatalogGalleryId', 0);
                    formData.append('CatalogId', CatalogId);
                formData.append('GalleryTypeId', parseInt(galleryTypeId));  
                    formData.append('StateId', 1);                
                    formData.append('FormFile', file); 

                const options = {
                    url: Uti.Url.Base + '/Account/CatalogGalleryCreate',
                    data: formData,
                  //contentType: Uti.Variable.FetchAjax.ContentType.ApplicationFormUrlencodedCharset,
                    type: Uti.Variable.FetchAjax.Type.Post
                };
                Uti.Ajax.Custom(options, function (response) {
                    Uti.Modal.Message(response.type, response.message, response.function);
                    if (response.type === Uti.Message.Type.Session) {
                        Uti.Modal.Process();
                    }
                    if (response.type === Uti.Message.Type.Success) {
                        Uti.Modal.Process();
                        $('#cboGalleryTypeID,#flImagen').val('');
                        Catalog._Search.fnCatalogGalleryList(CatalogId);
                    }
                });
            } 
 

*/