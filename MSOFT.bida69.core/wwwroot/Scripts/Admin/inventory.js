$(document).ready(function () {
    inventoryJS = new InventoryJS();
})

class InventoryJS {
    constructor() {
        this.initForm();
        this.initEvents();
        //this.loadData();
    }
    /**
     * Setting form
     * Author: NVMANH (07/07/2019)
     * */
    initForm() {
        this.DialogFormDetail = new Dialog("#FrmInventoryDetail", this, null, this.initDialogBeforeOpen);
        this.MasterTable = $('table#tbListInventoty');
        this.ItemID = "InventoryID";
        this.ApiService = "/inventories";
        this.initFormDetail();
    }
    initFormDetail() {
        // Load dữ liệu các combobox:
        var comboboxs = $('combobox');
        $.each(comboboxs, function (index, cbx) {
            Combobox.buildComboboxElement(cbx);
        });
    }
    /**
     * Init Events
     * Author: NVMANH (07/07/2019)
     * */
    initEvents() {
        $("#btnAddInventory").on('click', this.toolbarItemOnClick.bind(this));
        $("#btnEditInventory").on('click', this.toolbarItemOnClick.bind(this));
        $("#btnDeleteInventory").on('click', this.toolbarItemOnClick.bind(this));
        $("#btnRefreshInventory").on('click', this.toolbarItemOnClick.bind(this));
        $('#txtInventoryName').on('blur', this.generateInventoryCode.bind(this));
        $('table#tbListInventoty').on('dblclick', 'tr', this.rowOnDoubleClick.bind(this));
        $('#btnSaveDetail').on('click', this.save.bind(this));
        $('#btnCancel').click(function () { this.DialogFormDetail.close() }.bind(this));
    }
    /**
     * Load Data
     * Author: NVMANH (07/07/2019)
     * */
    loadData() {
        var me = this;
        me.MasterTable.find('tbody').empty();
        ajaxJSON.get(me.ApiService, {}, true, function (data) {
            commonJS.buidDataToTable($('table#tbListInventoty'), data);
        })
    }
    /**
     * Auto Generate code
     * Author: NVMANH (07/07/2019)
     * */
    toolbarItemOnClick(sender) {
        var commandName = sender.currentTarget.getAttribute("command");
        this.FormMode = commandName;
        switch (commandName) {
            case "Add":
                this.btnAddOnClick();
                break;
            case "Edit":
                this.btnEditOnClick();
                break;
            case "Delete":
                this.btnDeleteOnClick();
                break;
            case "Refresh":
                this.loadData();
                break;
            default:
                break;
        }

    }

    /**
     * Auto Generate code
     * Author: NVMANH (07/07/2019)
     * */
    generateInventoryCode() {
        if (this.FormMode === "Add") {
            var value = $('#txtInventoryName').val();
            var texts = value.split(' ');
            var inventoryCode = '';
            $.each(texts, function (index, text) {
                var prefixText = commonJS.change_alias(text.trim()).toUpperCase();
                inventoryCode += prefixText;
            })
            $('#txtInventoryCode').val(inventoryCode);
        }
    }

    rowOnDoubleClick() {
        this.FormMode = "Edit";
        this.btnEditOnClick();
    }

    /**
     * Show form Add Inventory
     * Author: NVMANH (07/07/2019)
     * */
    btnAddOnClick() {
        var me = this;

        this.DialogFormDetail.show();
    }

    /**
     * Edit Inventory
     * Author: NVMANH (07/07/2019)
     * */
    btnEditOnClick() {
        var me = this;
        // Get first row selected:
        var rowSelected = commonJS.getFirstRowTableSelected(this.MasterTable);
        if (rowSelected.length) {
            this.DialogFormDetail.show();
            var inventoryID = rowSelected.data("id");
            // Get data by id:
            ajaxJSON.get(this.ApiService + '/' + inventoryID, {}, true, function (data) {
                var inventory = data;
                    var inputs = me.DialogFormDetail.Dialog.find('[dataindex]');
                    $.each(inputs, function (index, input) {
                        var fieldName = input.getAttribute('dataindex');
                        var basicPropertyName = Object.GetBasicPropertyName(inventory, fieldName);
                        if (basicPropertyName) {
                            input.value = inventory[basicPropertyName];
                        }
                    })
            })
        } else {
            commonJS.showNotice("Vui lòng chọn mặt hàng muốn sửa");
        }
    }

    /**
     * Delete Inventory
     * Created By: NVMANH (07/07/2019)
     * */
    btnDeleteOnClick() {
        var me = this;
        commonJS.showConfirm("Chú có chắc chắn muốn xóa mặt hàng đang chọn?", function () {
            var itemId = commonJS.getFirstItemIdSelectedInTable(me.MasterTable);
            // Call service put:
            ajaxJSON.delete(me.ApiService + "/" + itemId, {}, true, function (res) {
                me.DialogFormDetail.close();
                me.loadData();
            })
        });
    }

    btnRefresherOnClick() {

    }

    save() {
        event.preventDefault();
        var me = this;
        // Fetch all the forms we want to apply custom Bootstrap validation styles to
        var formSubmit = document.getElementById('frmSubmit');
        var isValid = me.doValidate(formSubmit);
        if (isValid) {
            var fields = $('[dataindex]');
            var entity = {};
            entity[me.ItemID] = commonJS.getFirstItemIdSelectedInTable(me.MasterTable);
            $.each(fields, function (index, field) {
                var fieldName = field.getAttribute("dataindex");
                entity[fieldName] = field.value;
            })
            if (me.FormMode === 'Add') {
                // Call service post:
                entity[me.ItemID] = null;
                ajaxJSON.post(me.ApiService, entity, true, function (res) {
                    me.DialogFormDetail.close();
                    me.loadData();
                })
            } else {
                // Call service put:
                ajaxJSON.put(me.ApiService, entity, true, function (res) {
                    me.DialogFormDetail.close();
                    me.loadData();
                })
            }

        } else {
            commonJS.showWarning('Vui lòng kiểm tra lại dữ liệu, dữ liệu không hợp lệ!');
        }
        event.stopPropagation();
    }

    /**
     * Hàm thực hiện validate dữ liệu
     * @param {any} form
     * Created By: NVMANH (05/07/2019)
     */
    doValidate(form) {
        var me = this;
        var isValid = true;
        if (!form.checkValidity()) {
            isValid = false;
        }

        if (!me.doValidateCustom(form)) {
            isValid = false;
        }

        if (!isValid) {
            form.classList.add('was-validated');
        }
        return isValid;
    }

    /**
     * Validate custom
     * CreateBy: NVMANH (06/07/2019)
     * */
    doValidateCustom(form) {
        var isValidCustom = true;
        var comboboxs = $(form).find('select');
        $.each(comboboxs, function (index, combobox) {
            var itemSelected = combobox.options[combobox.selectedIndex];
            var valueSelectedItem = $(itemSelected).attr("value");
            if (!valueSelectedItem) {
                isValidCustom = false;
                combobox.classList.add('border-red');
                $(combobox).next().show();
                //$(combobox).siblings('.invalid-feedback').show();
            } else {
                combobox.classList.remove('border-red');
                $(combobox).next().hide();
            }
        })
        return isValidCustom;
    }

    /**
     * Thiết lập form chi tiết trước khi Open
     * CreatedBy: NVMANH (06/07/2019)
     * */
    initDialogBeforeOpen() {
        var frm = document.getElementById('frmSubmit');
        if (frm) {
            frm.reset();
        }
    }
}