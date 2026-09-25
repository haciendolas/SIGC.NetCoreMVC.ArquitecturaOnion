$(function () {
    let CatalogValidate = null;
    let CatalogVariantValidate = null;
    let ChoicesControl = null;
    const Catalog = {
        _Init: function () {
            $("#txtCatalogName").stringToSlug({
                setEvents: 'keyup keydown blur',
                getPut: '#txtCatalogSlug',
                space: '-'
            });
            Catalog._Validation.fnCatalogCreateUpdateValidate();
            Catalog._Validation.fnCatalogVariantCreateUpdateValidate()
         //   Catalog._Other.fnCatalogTabs();
            Catalog._Other.fnOpenFile();
            Catalog._Search.fnCatalogDataTable();
            ChoicesControl = Catalog._Other.fnChoices();
            ChoicesControl;           
            Catalog._Search.fnWarehouseComboBox();
            Catalog._Search.fnCatalogVariantGrid();
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
            if ($('#btnCatalogVariantCreate').length > 0) {
                $('#btnCatalogVariantCreate').on('click', function () {
                    Catalog._Operation.fnCatalogVariantCreateUpdate();
                });
            };
            if ($('#btnCatalogVariantUpdate').length > 0) {
                $('#btnCatalogVariantUpdate').hide();
                $('#btnCatalogVariantUpdate').on('click', function () {
                    Catalog._Operation.fnCatalogVariantCreateUpdate();
                });
            }; 
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
            $('#cboGlobalEstablishmentID').on('change', function () {
                Catalog._Search.fnWarehouseComboBox();
            });
            $('#chkCatalogHasVariants').on('change', function () {
                if ($(this).is(':checked')) {
                    $('#btnAttributeSearchOpen').show();
                    $('#txtCatalogVariantName').val('').attr('disabled', false);
                }
                else {
                    $('#btnAttributeSearchOpen').hide();
                    $('#txtCatalogVariantName').val('Único').attr('disabled', true);
                }
            });
            $('#txtCatalogVariantName').val('Único').attr('disabled', true);
            $('#btnAttributeSearchOpen').hide();
            $('#cboPresentationID').on('change', function () {
                const equivalence = $(this).find('option:selected').attr('presentationequivalence');
                $('#txtCatalogPresentationEquivalence').val(equivalence);               
            });
            $('#txtCatalogName').on('input', function () {
                $('input[name="CatalogNameLabel"]').val($(this).val());
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
                    const _URL = window.URL || window.webkitURL;
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
                $('#catalog-card ul li a[href="#tab-search"]').removeClass('disabled');
                $('#catalog-card ul li a[href="#tab-search"]').attr('data-bs-toggle', 'tab');
                $('#catalog-card ul li a[href="#tab-presentation"]').addClass('disabled');
                $('#catalog-card ul li a[href="#tab-presentation"]').removeAttr('data-bs-toggle');
                $('#catalog-card ul li a[href="#tab-price"]').addClass('disabled');
                $('#catalog-card ul li a[href="#tab-price"]').removeAttr('data-bs-toggle');
                $('#catalog-card ul li a[href="#tab-tax"]').addClass('disabled');
                $('#catalog-card ul li a[href="#tab-tax"]').removeAttr('data-bs-toggle');
                $('#catalog-card ul li a[href="#tab-configuration"]').addClass('disabled');
                $('#catalog-card ul li a[href="#tab-configuration"]').removeAttr('data-bs-toggle');
            },
            fnPresentationTabs: function () {           
                $('#catalog-card ul li a[href="#tab-register"]').addClass('disabled');
                $('#catalog-card ul li a[href="#tab-register"]').removeAttr('data-bs-toggle');
                $('#catalog-card ul li a[href="#tab-presentation"]').tab('show');
            },
            fnPriceTabs: function () {
                $('#catalog-card ul li a[href="#tab-presentation"]').addClass('disabled');
                $('#catalog-card ul li a[href="#tab-presentation"]').removeAttr('data-bs-toggle');
                $('#catalog-card ul li a[href="#tab-price"]').tab('show');
            },
            fnTaxTabs: function () {
                $('#catalog-card ul li a[href="#tab-price"]').addClass('disabled');
                $('#catalog-card ul li a[href="#tab-price"]').removeAttr('data-bs-toggle');
                $('#catalog-card ul li a[href="#tab-tax"]').tab('show');
            },
            fnConfigurationTabs: function () {
                $('#catalog-card ul li a[href="#tab-tax"]').addClass('disabled');
                $('#catalog-card ul li a[href="#tab-tax"]').removeAttr('data-bs-toggle');
                $('#catalog-card ul li a[href="#tab-configuration"]').tab('show');
            },
            fnChoices: function () {
                return {
                    CboCategoryID: new Choices('#cboCategoryID', {
                        noResultsText: 'No se encontraron registros'
                    }),
                    CboManufacturerID : new Choices('#cboManufacturerID', {
                        noResultsText: 'No se encontraron registros'
                    }),
                    CboBrandID: new Choices('#cboBrandID', {
                        noResultsText: 'No se encontraron registros'
                    }),
                    CboPharmaceuticalFormID :  new Choices('#cboPharmaceuticalFormID', {
                        noResultsText: 'No se encontraron registros'
                    }),
                    CboTaxAffectationTypeID : new Choices('#cboTaxAffectationTypeID', {
                        noResultsText: 'No se encontraron registros'
                    }),
                    CboTherapeuticActionID : new Choices('#cboTherapeuticActionID', {
                        removeItemButton: true,
                        noResultsText: 'No se encontraron registros'
                    }),
                    CboActiveIngredientID : new Choices('#cboActiveIngredientID', {
                        removeItemButton: true,
                        noResultsText: 'No se encontraron registros'
                    }),
                    CboUnitMeasureID: new Choices('#cboUnitMeasureID', {
                        noResultsText: 'No se encontraron registros'
                    })
                }
            },
            fnAttributeChecked: function () {
                const attributes = [];
                $('#tb-modal-attribute-list input:checkbox[name=chkAttributeValueID]:checked').each(function (index, element) {                                 
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
                    ignore:[],
                    rules: {
                        CatalogTypeID: { required: true },
                        CatalogSlug: { required: true, minlength: 3, maxlength: 200 },
                        CatalogName: { required: true, minlength: 3, maxlength: 200 },
                        CategoryID: { required: true },
                        SaleConditionID: { required: true },
                        ManufacturerID: { required: true },
                        BrandID: { required: true },
                    },
                    messages: {
                        CatalogTypeID: { required: '*Campo requerido'},
                        CatalogSlug: { required: '*Campo requerido', minlength: '*Mínimo 3 caracteres', maxlength: '*Máximo 200 caracteres' },
                        CatalogName: { required: '*Campo requerido', minlength: '*Mínimo 3 caracteres', maxlength: '*Máximo 200 caracteres' },
                        CategoryID: { required: '*Campo requerido' },
                        SaleConditionID: { required: '*Campo requerido' },
                        ManufacturerID: { required: '*Campo requerido' },
                        BrandID: { required: '*Campo requerido' },
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
            },
            fnCatalogVariantCreateUpdateReset: function () {
                CatalogVariantValidate.resetForm();
                $('#frmCatalogVariantCreateUpdate *').removeClass(['invalid-feedback', 'is-invalid']);
            },
            fnCatalogVariantCreateUpdateValidate: function () {
                CatalogVariantValidate = $('#frmCatalogVariantCreateUpdate').validate({
                    ignore: [],
                    rules: {                      
                        CatalogVariantSKU: { minlength: 3, maxlength: 50 },
                        CatalogVariantName: {required: true, minlength: 3, maxlength: 100 },
                        UnitMeasureID: { required: true },
                        PresentationID: { required: true },
                        CatalogPresentationEquivalence: { required: true },
                        CatalogPresentationSKU: { maxlength: 50 },
                        CatalogPresentationBarcode: { maxlength: 50 },
                    },
                    messages: {                      
                        CatalogVariantSKU: { minlength: '*Mínimo 3 caracteres', maxlength: '*Máximo 50 caracteres' },
                        CatalogVariantName: { required: '*Campo requerido', minlength: '*Mínimo 3 caracteres', maxlength: '*Máximo 100 caracteres' },
                        UnitMeasureID: { required: '*Campo requerido' },
                        CategoryID: { required: '*Campo requerido' },
                        PresentationID: { required: '*Campo requerido' },
                        CatalogPresentationEquivalence: { required: '*Campo requerido' },
                        CatalogPresentationSKU: { maxlength: '*Máximo 50 caracteres' },
                        CatalogPresentationBarcode: { maxlength: '*Máximo 50 caracteres' },
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
            },
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
                        $(row).find('a[name=lnkEdit]').on('click', function () {
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
                        $('#txtCatalogID').val(rowData.catalogID);
                        $('#cboCatalogTypeID').val(rowData.catalogTypeID)
                        $('#txtCatalogCode').val(rowData.catalogCode.trim());
                        $('#txtCatalogName').val(rowData.catalogName.trim());
                        $('#txtCatalogSlug').val(rowData.catalogSlug.trim());
                        $('#chkCatalogStateID').attr('checked', rowData.recordStateID == Uti.Variable.StateType.Active);
                        ChoicesControl.CboCategoryID.setChoiceByValue(String(rowData.categoryID));
                        ChoicesControl.CboManufacturerID.setChoiceByValue(String(rowData.manufacturerID));
                        ChoicesControl.CboBrandID.setChoiceByValue(String(rowData.brandID));                   
                        $('#cboSaleConditionID').val(rowData.saleConditionID);
                        ChoicesControl.CboPharmaceuticalFormID.setChoiceByValue(String(rowData.pharmaceuticalFormID));
                        ChoicesControl.CboTherapeuticActionID.setChoiceByValue((rowData.therapeuticActionIDs || []).map(id=>id.toString()));    
                        ChoicesControl.CboActiveIngredientID.setChoiceByValue((rowData.activeIngredientIDs || []).map(id=>id.toString()));  
                        $('#txtCatalogConcentration').val(rowData.catalogConcentration.trim());
                        $('#txtCatalogDescription').val(rowData.catalogDescription.trim());
                        $('#chkCatalogHasVariants').attr('checked', rowData.catalogHasVariants);
                        $('#hdCatalogImage').val(rowData.catalogImage.trim());
                        Uti.Image.Preview('imgCatalogImage', rowData.catalogUrl.trim());
                        if (rowData.catalogImage.trim() != '') $('#btnQuitar').show();
                        $('#catalog-card ul li a[href="#tab-search"]').addClass('disabled');
                        $('#catalog-card ul li a[href="#tab-search"]').removeAttr('data-bs-toggle');
                        $('#catalog-card ul li a[href="#tab-register"]').tab('show');
                        if ($('#btnCatalogUpdate').length > 0) $('#btnCatalogUpdate').show();
                        if ($('#btnCatalogCreate').length > 0) $('#btnCatalogCreate').hide();
                    };
                });
            },
            fnPresentationComboBox: function (UnitMeasureID, async = true) {
                const options = {
                    url: Uti.Url.Base + '/Product/Presentation/PresentationList/' + UnitMeasureID,
                    type: Uti.Variable.FetchAjax.Type.Get,
                    async: async
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
                    };
                });
            },
            fnWarehouseComboBox: function () {
                const EstablishmentID = $('#cboGlobalEstablishmentID').val() === '' ? 0 : $('#cboGlobalEstablishmentID').val();
                const options = {
                    url: Uti.Url.Base + '/Organization/Warehouse/WarehouseList/' + EstablishmentID,
                    type: Uti.Variable.FetchAjax.Type.Get,
                    preload:false
                };
                Uti.Ajax.Custom(options, function (response) {
                    Uti.Modal.Message(response.type, response.message, response.function);
                    if (response.type === Uti.Message.Type.Session) {
                        Uti.Modal.Process();
                    }
                    if (response.type === Uti.Message.Type.Query) {
                        const { data: rowData } = response;
                        $('#cboWarehouseID').empty();
                        let options = '';
                        if (rowData && rowData.length > 0) {
                            options = `<option value="">${Uti.Message.Description.Select}</option>`;
                            rowData.forEach(row => {
                                options += `<option value="${row.warehouseID}">${row.warehouseCode}-${row.warehouseName}</option>`;
                            });
                        }
                        else {
                            options = `<option value="">${Uti.Message.Description.NoRecordsFound}</option>`;
                        }
                        $('#cboWarehouseID').append(options);
                    };
                });
            },
            fnCatalogVariantGrid: function (preload = false) {
                const CatalogID = $('#txtCatalogID').val() == 'GENERADO' ? 0 : $('#txtCatalogID').val();   
                const options = {
                    url: Uti.Url.Base + '/Product/CatalogVariant/CatalogVariantList/' + CatalogID,
                    type: Uti.Variable.FetchAjax.Type.Get,
                    preload: preload
                };
                Uti.Ajax.Custom(options, function (response) {
                    Uti.Modal.Message(response.type, response.message, response.function);
                    if (response.type === Uti.Message.Type.Session) {
                        Uti.Modal.Process();
                    }
                    if (response.type === Uti.Message.Type.Query) {
                        const { data: rowData } = response;    
                        if (rowData && rowData.length > 0) {
                            const linkUpdate = Uti.Variable.Control();
                                  linkUpdate.Type = Uti.Variable.ButtonType.Update;
                            const linkChange = Uti.Variable.Control();
                                  linkChange.Type = Uti.Variable.ButtonType.Change;
                            const linkUnchange = Uti.Variable.Control();
                                  linkUnchange.Type = Uti.Variable.ButtonType.Unchange;
                            const linkAdd = Uti.Variable.Control();
                                  linkAdd.Type = Uti.Variable.ButtonType.Add;

                            rowData.forEach(catalogVariant => {
                                const attributeValueIDs = catalogVariant.catalogVariantValues.map(catalogVariantValue => catalogVariantValue.attributeValueID);
                                const containerVariantButtonId = `acction${catalogVariant.catalogVariantID}`;
                                const containerPresentationId = `catalog-presentation${catalogVariant.catalogVariantID}`;
                                const collapse = `collapse${catalogVariant.catalogVariantID}`;
                                const grid = `<div class="card">
                                            <div class="card-header p-2" style="background:#f3f3f9">
                                                <div class="d-flex align-items-center">
                                                    <div class="flex-grow-1">
                                                        <h6 class="card-title mb-0">${catalogVariant.catalogVariantName} &nbsp;&nbsp;&nbsp;&nbsp;SKU: ${catalogVariant.catalogVariantSKU}</h6>
                                                    </div>
                                                    <div class="flex-shrink-0" id="${containerVariantButtonId}">
                                                        <ul class="list-inline card-toolbar-menu d-flex align-items-center mb-0">
                                                            <li class="list-inline-item">
                                                                  ${catalogVariant.catalogVariantStateID === Uti.Variable.StateType.Active ? Uti.Control.LinkHRef(linkAdd) : "&nbsp;&nbsp;"}
                                                            </li>
                                                            <li class="list-inline-item">
                                                                  ${catalogVariant.catalogVariantStateID === Uti.Variable.StateType.Active ? Uti.Control.LinkHRef(linkUnchange) : Uti.Control.LinkHRef(linkChange)}
                                                            </li>
                                                             <li class="list-inline-item">
                                                                <a class="align-middle minimize-card link-primary" data-bs-toggle="collapse" href="#${collapse}" role="button" aria-expanded="false" aria-controls="${collapse}">
                                                                    <i class="ri-arrow-up-s-line align-middle plus fs-24 "></i>
                                                                    <i class="ri-arrow-down-s-line align-middle minus fs-24"></i>
                                                                </a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card-body pb-2 collapse show" id="${collapse}">
                                                <div class="table-responsive table-card p-1">
                                                    <table class="table align-middle" id="dtCompany">
                                                        <thead class="table-light text-muted">
                                                            <tr>
                                                                <th class="text-center" style="width:10%">ID</th>
                                                                <th class="text-center" style="width:13%">Unid.Medida </th>
                                                                <th class="text-center" style="width:18%">Presentación</th>
                                                                <th class="text-center" style="width:11%">Equivalencia</th>
                                                                <th class="text-center" style="width:11%">SKU</th>
                                                                <th class="text-center" style="width:11%">COD.QR</th>
                                                                <th class="text-center" style="width:10%">Por Defecto</th>
                                                                <th class="text-center" style="width:10%">Estado</th>
                                                                <th class="text-center" style="width:6%"colspan="2">Opciones</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody id="${containerPresentationId}">
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>`;
                                $('#div-variant').append(grid);
                                const element = $('#' + containerVariantButtonId);
                                element.find('a[name=slnkAdd]').on('click', function () {
                                  
                                }).tooltip();
                                element.find('a[name=slnkInactive]').on('click', function () {

                                }).tooltip();
                                element.find('a[name=slnkInactive]').on('click', function () {

                                }).tooltip();

                              catalogVariant.catalogPresentations.forEach(catalogPresentation => {
                                    const column = `
                                            <td class="text-center">${catalogPresentation.catalogPresentationID}</td>
                                            <td>${catalogPresentation.unitMeasureName}</td>
                                            <td>${catalogPresentation.presentationName}</td>
                                            <td class="text-center">${catalogPresentation.catalogPresentationEquivalence}</td>
                                            <td class="text-center">${catalogPresentation.catalogPresentationSKU}</td>
                                            <td class="text-center">${catalogPresentation.catalogPresentationBarcode}</td>
                                            <td class="text-center">
                                               <div class="form-check form-radio-success text-center">
                                                     <input type="radio" class="form-check-input" name="chkIsDefault${catalogVariant.catalogVariantID}" id="chkIsDefault${catalogPresentation.catalogPresentationID}" style="width:23px;height:23px" ${(catalogPresentation.catalogPresentationIsDefault ? "checked" : "")}>                                                    
                                                </div>
                                            </td>
                                            <td class="text-center">${Uti.Control.SpanStateType(catalogPresentation.catalogPresentationStateID)}</td>
                                            <td class="text-center" style="width:3%">${catalogPresentation.catalogPresentationStateID === Uti.Variable.StateType.Active ? Uti.Control.LinkHRef(linkUpdate) : "&nbsp;&nbsp;"}</td>
                                            <td class="text-center" style="width:3%">${catalogPresentation.catalogPresentationStateID === Uti.Variable.StateType.Active ? Uti.Control.LinkHRef(linkUnchange) : Uti.Control.LinkHRef(linkChange)}</td>
                                          `;
                                  const element = $('#' + containerPresentationId);
                                  element.append(column).fadeIn('slow');
                                  element.find('a[name=slnkEdit]').on('click', function () {
                                      $('#hdCatalogVariantID').val(catalogVariant.catalogVariantID);                                    
                                      $('#txtCatalogVariantName').val(catalogVariant.catalogVariantName.trim()).data("attributeValueIDs", attributeValueIDs);
                                      $('#txtCatalogVariantSKU').val(catalogVariant.catalogVariantName.trim());
                                      $('#span-catalog-variant-name').text(catalogVariant.catalogVariantName.trim());
                                      $('#hdCatalogPresentationID').val(catalogPresentation.catalogPresentationID);                                                                       
                                      ChoicesControl.CboUnitMeasureID.setChoiceByValue(String(catalogPresentation.unitMeasureID))
                                      Catalog._Search.fnPresentationComboBox(catalogPresentation.unitMeasureID, false);
                                      $('#cboPresentationID').val(catalogPresentation.presentationID);
                                      $('#txtCatalogPresentationEquivalence').val(catalogPresentation.catalogPresentationEquivalence);
                                      $('#txtCatalogPresentationSKU').val(catalogPresentation.catalogPresentationSKU);
                                      $('#txtCatalogPresentationBarcode').val(catalogPresentation.catalogPresentationBarcode);                                    
                                      $('#chkCatalogPresentationStateID').attr('checked', catalogPresentation.catalogPresentationStateID == Uti.Variable.StateType.Active);                                   
                                  }).tooltip();
                                  element.find('a[name=slnkInactive]').on('click', function () {
                                      alert('Inactivar' + catalogPresentation.unitMeasureID);
                                  }).tooltip();
                                  element.find('a[name=slnkActive]').on('click', function () {
                                      alert('Activar' + catalogPresentation.catalogPresentationID);
                                  }).tooltip();
                                });
                            });
                        }
                        else {                           
                            $('#div-variant').append(Uti.Message.Description.NoRecordsFound);
                        }                      
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
                    const CatalogID = $('#txtCatalogID').val() == 'GENERADO' ? 0 : $('#txtCatalogID').val();

                    var formData = new FormData();
                    formData.append('CatalogID', CatalogID);
                    formData.append('CatalogTypeID', $('#cboCatalogTypeID').val());
                    formData.append('CatalogCode', $('#txtCatalogCode').val().trim());
                    formData.append('RecordStateID', $('#chkCatalogStateID').is(':checked') ? Uti.Variable.StateType.Active : Uti.Variable.StateType.Inactive);
                    formData.append('CatalogName', $('#txtCatalogName').val().trim());
                    formData.append('CatalogSlug', $('#txtCatalogSlug').val().trim());
                    formData.append('CategoryID', $('#cboCategoryID').val());
                    formData.append('ManufacturerID', $('#cboManufacturerID').val());
                    formData.append('BrandID', $('#cboBrandID').val());
                    formData.append('SaleConditionID', $('#cboSaleConditionID').val());
                    formData.append('PharmaceuticalFormID', $('#cboPharmaceuticalFormID').val());

                    const therapeuticActions = $('#cboTherapeuticActionID').val() || [];
                    therapeuticActions.forEach((id, index) => {
                        formData.append(`CatalogTherapeuticActions[${index}].TherapeuticActionID`, id );
                    });

                    const activeIngredients = $('#cboActiveIngredientID').val() || [];
                    activeIngredients.forEach((id, index) => {
                        formData.append(`CatalogActiveIngredients[${index}].ActiveIngredientID`, id);
                        formData.append(`CatalogActiveIngredients[${index}].CatalogActiveIngredientQuantity`, null);
                    });

                    formData.append('CatalogHasVariants', $('#chkCatalogHasVariants').is(':checked'));
                    formData.append('ActiveIngredientID', $('#cboActiveIngredientID').val());
                    formData.append('CatalogConcentration', $('#txtCatalogConcentration').val().trim());
                    formData.append('CatalogDescription', $('#txtCatalogDescription').val().trim());
                    formData.append('CatalogBrandType', 'NINGUNO');
                    formData.append('FormFile', file);
                    formData.append('CatalogImage', $('#hdCatalogImage').val().trim());
                    formData.append('CatalogImageBandera', $('#hdCatalogImageBandera').val().trim());

                    const options = {
                        url: Uti.Url.Base + '/Product/Catalog/' + (CatalogID == 0 ? 'CatalogCreate' : 'CatalogUpdate') + '',
                        data: formData,
                        type: CatalogID == 0 ? Uti.Variable.FetchAjax.Type.Post : Uti.Variable.FetchAjax.Type.Put
                    };
                    Uti.Ajax.Custom(options, function (response) {                       
                        Uti.Modal.Message(response.type, response.message, response.function);
                        if (response.type === Uti.Message.Type.Session) {
                            Uti.Modal.Process();
                        };
                        if (response.type === Uti.Message.Type.Success) {                           
                            if (response.data) { 
                               $('#txtCatalogID').val(response.data.catalogID);
                            };
                            Uti.Modal.Process();
                            Catalog._Search.fnCatalogDataTable();
                            if (CatalogID === 0) {
                                Catalog._Other.fnPresentationTabs();
                            }
                            else {
                                //Catalog._Clear.fnCatalogGet();
                            }
                         
                        };
                    });
                }
            },
            fnCatalogVariantCreateUpdate: function () {
                if ($('#frmCatalogVariantCreateUpdate').valid()) {               
                    const CatalogVariantID = $('#hdCatalogVariantID').val() == '' ? 0 : $('#hdCatalogVariantID').val();   
                    const options = {
                        url: Uti.Url.Base + '/Product/CatalogVariant/' + (CatalogVariantID == 0 ? 'CatalogVariantCreate' : 'CatalogVariantUpdate') + '',
                        data: {
                            CatalogVariantID: CatalogVariantID,
                            CatalogID: $('#txtCatalogID').val() == 'GENERADO' ? 0 : $('#txtCatalogID').val(),
                            CatalogVariantName: $('#txtCatalogVariantName').val().trim(),
                            CatalogVariantSKU: $('#txtCatalogVariantSKU').val().trim(),
                            RecordStateID: Uti.Variable.StateType.Active,
                            CatalogVariantValues:($('#txtCatalogVariantName').data().attributeValueIDs ?? []).map(map => ({
                                AttributeValueID: map,
                            })) , 
                            CatalogPresentations: [{
                                CatalogPresentationID: $('#hdCatalogPresentationID').val()==='' ? 0 :$('#hdCatalogPresentationID').val(),
                                PresentationID: $('#cboPresentationID').val(),
                                CatalogPresentationIsDefault: true,
                                CatalogPresentationEquivalence: $('#txtCatalogPresentationEquivalence').val(),
                                CatalogPresentationSKU: $('#txtCatalogPresentationSKU').val().trim(),
                                CatalogPresentationBarcode: $('#txtCatalogPresentationBarcode').val().trim(),
                                RecordStateID: $('#chkCatalogPresentationStateID').is(':checked') ? Uti.Variable.StateType.Active : Uti.Variable.StateType.Inactive
                            }]
                        },
                        type: CatalogVariantID == 0 ? Uti.Variable.FetchAjax.Type.Post : Uti.Variable.FetchAjax.Type.Put
                    };
                    Uti.Ajax.Custom(options, function (response) {
                        Uti.Modal.Message(response.type, response.message, response.function);
                        if (response.type === Uti.Message.Type.Session) {
                            Uti.Modal.Process();
                        };
                        if (response.type === Uti.Message.Type.Success) {
                            if (response.data) {
                                $('#hdCatalogVariantID').val(response.data.catalogVariantID);
                            };
                            Uti.Modal.Process();
                            Catalog._Search.fnCatalogVariantGrid();
                            if (CatalogVariantID === 0) {
                                Catalog._Other.fnPriceTabs();
                                Catalog._Search.fnCatalogPresentationComboBox();
                            }
                        };
                    });
                }
            },
        }
    }
    Catalog._Init();
});