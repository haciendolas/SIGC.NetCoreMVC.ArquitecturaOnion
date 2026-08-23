$(function () {
    let CatalogValidate = null;
    const Catalog = {
        _Init: function () {
            $("#txtCatalogName").stringToSlug({
                setEvents: 'keyup keydown blur',
                getPut: '#txtCatalogSlug',
                space: '-'
            });
            Catalog._Validation.fnCatalogCreateUpdateValidate();
            Catalog._Other.fnOpenFile();
            Catalog._Search.fnCatalogDataTable();
            Catalog._Other.fnChoices();
            Catalog._Search.fnCatalogPresentationComboBox();
            $('#stxtCatalogName,#txtCatalogName').keypress(function (event) {
                return Uti.KeyBoard.LettersAndNumbers(event);
            });
            $('#stxtCatalogName').on('keyup', Uti.SetTimeout.Debounce((event) => {
                const keyCode = event.keyCode ? event.keyCode : event.which;
                if (!(keyCode == 32 || keyCode == '32')) {
                    Catalog._Search.fnCatalogDataTable();
                };
            }));
            $('#scboStateID,#scboCatalogTypeID,#scboCategoryID,#scboManufacturerID,#scboBrandID').on('change', function () {
                Catalog._Search.fnCatalogDataTable();
            });
            if ($('#btnCatalogCreate').length > 0) {
                $('#btnCatalogCreate').on('click', function () {
                    Catalog._Operation.fnCatalogCreateUpdate();
                });
            };
            if ($('#btnCatalogUpdate').length > 0) {
                $('#btnCatalogUpdate').hide();
                $('#btnCatalogUpdate').on('click', function () {
                    Catalog._Operation.fnCatalogCreateUpdate();
                });
            };
            $('#btnCatalogNew').on('click', function () {
                Catalog._Clear.fnCatalogGet();
            });
            $('#btnQuitar').hide();
            $('#btnQuitar').on('click', function () {
                Uti.Image.Preview('imgCatalogImage');
                $('#profile-img-file-input').val('');
                $(this).hide();
                $('#hdCatalogImageBandera').val('DELETE');
            });
            $('#cboUnitMeasureID').on('change', function () {
                const UnitMeasureID = $(this).val();
                if (UnitMeasureID) {
                    Catalog._Search.fnPresentationComboBox(UnitMeasureID);
                };
            });
            $('#btnAttributeSearchOpen').on('click', function () {
                Catalog._Modal.fnAttributeSearchOpen();
            });
            $('#btnModalAttributeAccept').on('click', function () {
                Catalog._Other.fnAttributeChecked();
            });
        },
        _Clear: {
            fnCatalogGet: function () {
                $('#txtCatalogID').val('GENERADO');
                $('#txtCatalogName,#txtCatalogSlug').val('');
                $('#chkStateID').prop('checked', true);
                if ($('#btnCatalogUpdate').length > 0) $('#btnCatalogUpdate').hide();
                if ($('#btnCatalogCreate').length > 0) $('#btnCatalogCreate').show();
                Catalog._Other.fnCatalogTabs();
                Catalog._Validation.fnCatalogCreateUpdateReset();
                Uti.Image.Preview('imgCatalogImage');
                $('#txtCatalogName').focus();
                $('#hdCatalogImage,#hdCatalogImageBandera').val('');
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
                        $('#imgCatalogImage').fadeIn('fast').attr('src', tmppath);
                        $('#btnQuitar').show();
                        $('#hdCatalogImageBandera').val('');
                    }
                });
            },
            fnCatalogTabs: function () {
                $('#Catalog-card ul li a[href="#tab-search"]').removeClass('disabled');
                $('#Catalog-card ul li a[href="#tab-search"]').attr('data-bs-toggle', 'tab');
            },
            fnChoices: function () {
                new Choices('#cboCategoryID', {
                    noResultsText: 'No se encontraron registros'
                });
                new Choices('#cboManufacturerID', {
                    noResultsText: 'No se encontraron registros'
                });
                new Choices('#cboBrandID', {
                    noResultsText: 'No se encontraron registros'
                });
                new Choices('#cboPharmaceuticalFormID', {
                    noResultsText: 'No se encontraron registros'
                });
                new Choices('#cboTaxAffectationTypeID', {
                    noResultsText: 'No se encontraron registros'
                });                
            },
            fnAttributeChecked: function () {
                const attributes = [];
                $('#tb-modal-attribute-list input:checkbox[name=chkAttributeValueID]:checked').each(function (index, element) {
                    console.log(index)                 
                    const attributeID = $(element).data('attributeid');
                    let attribute = attributes.find(x => x.attributeID === attributeID);
                    if (!attribute) {
                        attribute = {
                            attributeID: attributeID,
                            attributeName: $(element).data('attributename'),
                            attributeValues: []
                        };
                        attributes.push(attribute);
                    };
                    attribute.attributeValues.push({
                        attributeValueID: $(element).data('attributevalueid'),
                        attributeValueName: $(element).data('attributevaluename')
                    });                                
                });
                if (attributes.length === 0) {
                    Uti.Modal.Toastify(Uti.Message.Description.AtLeastOneItemMustBeSelected('un valor'), Uti.Message.Type.Warning);
                    return;
                };
                const catalogName = $('input:text[name=CatalogName]').val().trim();
                const catalogVariantName = attributes
                    .map(item =>
                        item.attributeName + '/' +
                        item.attributeValues.map(subItem => subItem.attributeValueName).join('/')
                ).join(' - ');
                const attributeValueIDs = attributes.flatMap(item =>
                    item.attributeValues.map(subItem => subItem.attributeValueID)
                );
                $('#txtCatalogVariantName').val(catalogVariantName.trim()).data("attributeValueIDs", attributeValueIDs);
                $('#span-catalog-variant-name').text(catalogVariantName);
                $('#txtCatalogVariantSKU').val(Catalog._Other.fnGetInitials(catalogName) + '-' + catalogVariantName.replace(/\s+/g, '').replace(/[\/-]/g, '-'));
                Catalog._Modal.fnAttributeSearchClose();
            },
            fnGetInitials(text) {
                return text
                    .trim()
                    .split(/\s+/)
                    .map(word => word.charAt(0))
                    .join('')
                    .toUpperCase();
            }
        },
        _Validation: {
            fnCatalogCreateUpdateReset: function () {
                CatalogValidate.resetForm();
                $('#frmCatalogCreateUpdate *').removeClass(['invalid-feedback', 'is-invalid']);
            },
            fnCatalogCreateUpdateValidate: function () {
                CatalogValidate = $('#frmCatalogCreateUpdate').validate({
                    rules: {
                        CatalogSlug: { required: true, minlength: 3, maxlength: 100 },
                        CatalogName: { required: true, minlength: 3, maxlength: 100 },
                    },
                    messages: {
                        CatalogSlug: { required: '*Campo requerido', minlength: '*Mínimo 3 caracteres', maxlength: '*Máximo 100 caracteres' },
                        CatalogName: { required: '*Campo requerido', minlength: '*Mínimo 3 caracteres', maxlength: '*Máximo 100 caracteres' }
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
        _Modal: {
            fnAttributeSearchOpen: function () {
                $('#modal-attribute-search').modal('show');                
            },
            fnAttributeSearchClose: function () {
                $('#modal-attribute-search').modal('hide');
            }
        },
        _Search: {
            fnCatalogDataTable: function () {
                $('#dtCatalog').dataTable({
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
                        const input = $('#dtCatalog_filter input');
                        input.removeClass().addClass('form-control');
                        input.attr({ placeholder: 'Buscar categoría...', type: 'text' });
                        input.off();
                        input.on('keyup', Uti.SetTimeout.Debounce((event) => {
                            const valor = event.target.value;
                            const keyCode = event.keyCode ? event.keyCode : event.which;
                            if (!(keyCode == 32 || keyCode == '32')) {
                                $('#dtCatalog').DataTable().search(valor).draw();
                            };
                        })
                        );
                    },
                    bJQueryUI: false,
                    bAutoWidth: false,
                    bDestroy: true,
                    sServerMethod: "POST",
                    sAjaxSource: Uti.Url.Base + '/Product/Catalog/CatalogDataTable',
                    fnServerParams: function (aoData) {
                        aoData.push(
                            { name: 'CatalogTypeID', value: $('#scboCatalogTypeID').val() },
                            { name: 'CategoryID', value: $('#scboCategoryID').val() },
                            { name: 'ManufacturerID', value: $('#scboManufacturerID').val() },
                            { name: 'BrandID', value: $('#scboBrandID').val() },
                            { name: 'RecordStateID', value: $('#scboStateID').val() },
                            { name: 'Search', value: $('#stxtCatalogName').val().trim() }
                          
                        );
                    },
                    sPaginationType: 'full_numbers',
                    aoColumnDefs: [
                        { bSortable: true,  aTargets: [0], sClass: 'text-center' },
                        { bSortable: true,  aTargets: [1], sClass: 'text-left' },
                        { bSortable: true,  aTargets: [2], sClass: 'text-left' },
                        { bSortable: false, aTargets: [3], sClass: 'text-center' },
                        { bSortable: true,  aTargets: [4], sClass: 'text-center' },
                        { bSortable: true,  aTargets: [5], sClass: 'text-center' },
                        { bSortable: false, aTargets: [6], sClass: 'text-center' },
                        { bSortable: false, aTargets: [7], sClass: 'text-center' },
                        { bSortable: false, aTargets: [8], sClass: 'text-center' },
                        { bSortable: false, aTargets: [9], sClass: 'text-center' },
                        { bSortable: false, aTargets: [10], sClass: 'text-center' },
                        { bSortable: false, aTargets: [11], sClass: 'text-center' }
                    ],
                    order: [[0, 'desc']],
                    bSort: false,
                    rowCallback: function (row, data, dataIndex) {
                        $(row).find('a[name=slnkEdit]').on('click', function () {
                            Catalog._Search.fnCatalogGet(data[0]);
                        }).tooltip();
                        $(row).find('a[name=slnkInactive]').on('click', function () {
                            Catalog._Operation.fnCatalogChangeState(data[0], Uti.Variable.StateType.Inactive);
                        }).tooltip();
                        $(row).find('a[name=slnkActive]').on('click', function () {
                            Catalog._Operation.fnCatalogChangeState(data[0], Uti.Variable.StateType.Active);
                        }).tooltip();
                    },
                    drawCallback: function (data) {
                        const response = data.json;
                    }
                });
            },
            fnCatalogGet: function (CatalogID) {
                const options = {
                    url: Uti.Url.Base + '/Product/Catalog/CatalogGet/' + CatalogID,
                    type: Uti.Variable.FetchAjax.Type.Get
                };
                Uti.Ajax.Custom(options, function (response) {
                    Uti.Modal.Message(response.type, response.message, response.function);
                    if (response.type === Uti.Message.Type.Session) {
                        Uti.Modal.Process();
                    }
                    if (response.type === Uti.Message.Type.Query) {
                        const { data: rowData } = response;
                        Catalog._Clear.fnCatalogGet();
                        $('#txtCatalogID').val(rowData.CatalogId);
                        $('#txtCatalogName').val(rowData.CatalogName.trim());
                        $('#txtCatalogSlug').val(rowData.CatalogSlug.trim());
                        $('#chkStateID').attr('checked', rowData.recordStateID == Uti.Variable.StateType.Active);
                        $('#hdCatalogImage').val(rowData.CatalogImage.trim());
                        Uti.Image.Preview('imgCatalogImage', rowData.CatalogUrl.trim());
                        if (rowData.CatalogImage.trim() != '') $('#btnQuitar').show();
                        $('#Catalog-card ul li a[href="#tab-search"]').addClass('disabled');
                        $('#Catalog-card ul li a[href="#tab-search"]').removeAttr('data-bs-toggle');
                        $('#Catalog-card ul li a[href="#tab-register"]').tab('show');
                        if ($('#btnCatalogUpdate').length > 0) $('#btnCatalogUpdate').show();
                        if ($('#btnCatalogCreate').length > 0) $('#btnCatalogCreate').hide();
                    };
                });
            },
            fnPresentationComboBox: function (UnitMeasureID) {
                const options = {
                    url: Uti.Url.Base + '/Product/Presentation/PresentationList/' + UnitMeasureID,
                    type: Uti.Variable.FetchAjax.Type.Get
                };
                Uti.Ajax.Custom(options, function (response) {
                    Uti.Modal.Message(response.type, response.message, response.function);
                    if (response.type === Uti.Message.Type.Session) {
                        Uti.Modal.Process();
                    }
                    if (response.type === Uti.Message.Type.Query) {
                        const { data: rowData } = response;
                        $('#cboPresentationID').empty();
                        let options = '';
                        if (rowData && rowData.length>0) {   
                            options = `<option value="">${Uti.Message.Description.Select}</option>`;
                            rowData.forEach(row => {
                                options += `<option value="${row.presentationID}" presentationEquivalence="${row.presentationEquivalence}">${row.presentationName}</option>`;
                            });                          
                        }
                        else {
                            options = `<option value="">${Uti.Message.Description.NoRecordsFound}</option>`;
                        }
                    $('#cboPresentationID').append(options);
                    };
                });
            },
            fnCatalogPresentationComboBox: function () {          
                const CatalogID = $('#txtCatalogID').val() == 'GENERADO' ? 0 : $('#txtCatalogID').val(); 
                const options = {
                    url: Uti.Url.Base + '/Product/CatalogPresentation/CatalogPresentationList/' + CatalogID,
                    type: Uti.Variable.FetchAjax.Type.Get
                };
                Uti.Ajax.Custom(options, function (response) {
                    Uti.Modal.Message(response.type, response.message, response.function);
                    if (response.type === Uti.Message.Type.Session) {
                        Uti.Modal.Process();
                    }
                    if (response.type === Uti.Message.Type.Query) {
                        const { data: rowData } = response;
                        $('#cboCatalogPresentationID').empty();
                        let options = '';
                        if (rowData && rowData.length > 0) {
                            options = `<option value="">${Uti.Message.Description.Select}</option>`;
                            rowData.forEach(row => {
                                debugger
                                options += `<optgroup label="${row.catalogVariantName}">`;
                                row.catalogPresentations.forEach(subRow => {
                                    options += `<option value="${subRow.catalogPresentationID}">${subRow.catalogPresentationName}</option>`;
                                })
                                options += `</optgroup>`;
                            });
                        }
                        else {
                            options = `<option value="">${Uti.Message.Description.NoRecordsFound}</option>`;
                        }
                        $('#cboCatalogPresentationID').append(options);
                        /*
                        <select>
    <optgroup label="Color">
        <option value="1">Rojo</option>
        <option value="2">Azul</option>
        <option value="3">Negro</option>
    </optgroup>
 
</select>
                        */
                    };
                });
            }
        },
        _Operation: {
            fnCatalogChangeState: function (CatalogId, StateID) {
                const options = {
                    url: Uti.Url.Base + '/Product/Catalog/CatalogChangeState',
                    data: {
                        CatalogId: CatalogId,
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
                        Catalog._Search.fnCatalogDataTable();
                    }
                });
            },
            fnCatalogCreateUpdate: function () {
                if ($('#frmCatalogCreateUpdate').valid()) {
                    const file = document.getElementById('profile-img-file-input').files[0];
                    if (file) {
                        if (!(file.type == 'image/png' || file.type == 'image/jpeg' || file.type == 'image/jpg')) {
                            Uti.Modal.Message(Uti.Message.Type.Warning, 'Solo se admite archivos con extensión: (jpg,png,jpeg)');
                            return;
                        };
                    };
                    const CatalogId = $('#txtCatalogID').val() == 'GENERADO' ? 0 : $('#txtCatalogID').val();

                    var formData = new FormData();
                    formData.append('CatalogId', CatalogId);
                    formData.append('CatalogName', $('#txtCatalogName').val().trim());
                    formData.append('CatalogSlug', $('#txtCatalogSlug').val().trim());
                    formData.append('RecordStateId', $('#chkStateID').is(':checked') ? Uti.Variable.StateType.Active : Uti.Variable.StateType.Inactive);
                    formData.append('FormFile', file);
                    formData.append('CatalogImage', $('#hdCatalogImage').val().trim());
                    formData.append('CatalogImageBandera', $('#hdCatalogImageBandera').val().trim());

                    const options = {
                        url: Uti.Url.Base + '/Product/Catalog/' + (CatalogId == 0 ? 'CatalogCreate' : 'CatalogUpdate') + '',
                        data: formData,
                        type: CatalogId == 0 ? Uti.Variable.FetchAjax.Type.Post : Uti.Variable.FetchAjax.Type.Put
                    };
                    Uti.Ajax.Custom(options, function (response) {
                        Uti.Modal.Message(response.type, response.message, response.function);
                        if (response.type === Uti.Message.Type.Session) {
                            Uti.Modal.Process();
                        };
                        if (response.type === Uti.Message.Type.Success) {
                            Uti.Modal.Process();
                            Catalog._Search.fnCatalogDataTable();
                            Catalog._Clear.fnCatalogGet();
                        };
                    });
                }
            },
        }
    }
    Catalog._Init();
});