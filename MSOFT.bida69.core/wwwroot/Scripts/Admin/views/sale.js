$(document).ready(function () {
    saleJS = new SaleJS();
})

class SaleJS {
    constructor() {
        this.FrmOrderDetail = new Dialog('#frmOrderDetail', this, null);
        this.initEvents();
    }

    initEvents() {
        $(document).on('click', "#btnSale", this.btnSaleOnClick.bind(this));
    }

    btnSaleOnClick() {
        var me = this;
        me.FrmOrderDetail.show();
    }
    
}