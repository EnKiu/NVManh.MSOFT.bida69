﻿$(document).ready(function () {
    saleJS = new SaleJS();
})

class SaleJS {
    constructor() {
        this.FrmOrderDetail = new Dialog('#frmOrderDetail', this, null, this.loadOrderDetail.bind(this));
        this.initEvents();
        this.data = [];
        this.totalAmount = 0;
    }

    initEvents() {
        $(document).on('click', "#btnSale", this.btnSaleOnClick.bind(this));
        // Các sự kiện trên Form hóa đơn bán hàng:
        $("#frmOrderDetail #btnCloseOrderDetail").click(function () { this.FrmOrderDetail.close(); }.bind(this));
        $('#frmOrderDetail').on('click', '#btnDeleteOrder', this.btnDeleteOrderOnClick.bind(this)); // Hủy hóa đơn
        $('#frmOrderDetail').on('click', ".cell-delete", this.refDetailOnDelete.bind(this)); // Xóa 1 dòng chi tiết trong hóa đơn
    }

    btnSaleOnClick() {
        var me = this;
        this.totalAmount = 0;
        me.FrmOrderDetail.ViewMode = false;
        me.FrmOrderDetail.show();
    }

    buildRowHtmlData(data) {
        var me = this;
        me.totalAmount = 0;
        $.each(data, function (index,item) {
            me.totalAmount += item["TotalAmount"] || 0;
        })
        commonJS.buidDataToTable($('#frmOrderDetail table.tbInventotySelected'), data);
        var totalAmountEl = saleJS.FrmOrderDetail.Dialog.find('.totalMoney');
        // Cập nhật tổng tiền của hóa đơn lên giao diện:
        var totalAmountHTML = "{0}{1}".format(this.totalAmount.formatMoney(), '<sup>đ</sup>');
        totalAmountEl.html(totalAmountHTML);
    }

    /**
     * Thực hiện hủy hóa đơn
     * */
    btnDeleteOrderOnClick() {
        var me = this;
        commonJS.showConfirm("<div style='color:red'><b>Chú có chắc chắn muốn hủy hóa đơn này?</b></div>", function () {
            commonJS.showSuccessMsg2("Đã hủy hóa đơn!");
            me.FrmOrderDetail.hide();
        })
    }

    /**
     * Xóa hàng hóa trong hóa đơn bán hàng
     * @param {any} sender
     * Author: NVMANH (31/07/2020)
     */
    refDetailOnDelete(sender) {
        var me = this;
        var currentTarget = $(sender.currentTarget);
        var currentRow = currentTarget.parents('tr');
        var currentRecordId = currentRow.data('dataJson')['InventoryID'];
        var inventoryName = currentRow.find('td')[1].textContent;
        commonJS.showConfirm("Chú có chắc chắn muốn xóa <b><span style='color:red'>{0}</span></b> khỏi hóa đơn này?".format(inventoryName), function () {
            me.data = me.data.filter(function (obj) { return obj['InventoryID'] != currentRecordId });
            me.buildRowHtmlData(me.data);
            commonJS.showSuccessMsg2("Đã xóa <b><span style='color:red'>{0}</span></b> khỏi hóa đơn!".format(inventoryName));
        })
    }
    loadOrderDetail() {
        var me = this;
        me.data = [];
        $('.totalMoney').empty();
        $('#frmOrderDetail #tbInventotySelected tbody').empty();
        if (me.FrmOrderDetail.ViewMode) {
            var refid = me.FrmOrderDetail.RefID;
            ajaxJSON.get("/refs/refdetail/" + refid, {}, true, function (data) {
                me.buildRowHtmlData(data["RefDetails"]);
                $('.service-toolbar').hide();
                $('.refDetailToolbar-btn').hide();
                $('.cell-delete').hide();
                $('#btnAcceptPayOrder').hide();
                $('#btnDeleteOrder').hide();
            })
        } else {
            $('.service-toolbar').show();
            $('.refDetailToolbar-btn').show();
            $('.cell-delete').show();
            $('#btnAcceptPayOrder').show();
            $('#btnDeleteOrder').show();
        }
    }
}