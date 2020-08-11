$(document).ready(function () {
    adminJS = new Admin();
});

class Admin {
    constructor() {
        this.loadBidaStatus();
        this.initForm();
        this.initEvents();
    }

    /** ----------------------------------------------------------------
     * Thiết lập các Form hiển thị
     * Author: NVMANH (12/07/2019)
     * */
    initForm() {
        //Form chi tiết hóa đơn
        this.FrmBidaDetail = new Dialog('#frmBidaDetail', this, null, this.loadOrderDetail.bind(this));
        //form chọn nhanh mặt hàng
        this.FrmSelectInventory = new Dialog('#frmSelectInventory', this, null, this.initFrmSelectInventoryBeforeOpen.bind(this));
        // Form nhập số lượng mặt hàng:
        var buttonAccept = new Button("Đồng ý", this.btnSubmitQuantityInvSelect.bind(this), this, "btn-primary", "submit", "frmSubmitQuantity", "fa fa-check");
        this.FrmInputQuantityInventory = new Dialog("#frmInputQuantityInventory", this, [buttonAccept], this.initFrmInputQuantityInventory.bind(this));
        this.FrmOrderPrint = new Dialog('#frmOrderPrint', this, null, this.initFrmOrderPrint.bind(this));
        this.FrmSelectTimeClock = new Dialog('#frmSelectTimeClock', this, null, this.initFrmSelectTimeClockBeforeOpen.bind(this));

        this.FrmSelectServiceChange = new Dialog('#frmSelectServiceChange', this, null, this.initFrmSelectServiceChangeBeforeOpen.bind(this));
        $(dtDateSelect).val((new Date()).ddmmyyyy());
    }
    /* ----------------------------------------------------------------
     * Bind Event fo element
     * CreatedBy: NVMANH (01/07/2019)
     * ----------------------------------*/
    initEvents() {
        var me = this;
        // chọn tab:
        $('a[data-toggle="tab"]').on('shown.bs.tab', this.tabChange.bind(this));

        $('#listBida').on("click", ".bida-item", this.bidaItemOnClick.bind(this));
        $('#listBida').on("click", ".btnQuickAddInventory", this.btnQuickAddOnClick.bind(this));
        $(document).on("click", ".btnAddInventoryToOrder", this.btnAddInventoryToOrderOnClick.bind(this));
        $('#listBida').on("click", '.btnActiveBida', this.changeBidaToInUse.bind(this));
        $("#frmSelectInventory").on('dblclick', 'tbody tr', this.rowInventorySelectedOnDblClick.bind(this));
        $("#btnSubmitQuantity").on('click', this.btnSubmitQuantityInvSelect.bind(this));
        $("#btnCancelSubmitQuantity").on('click', function () { this.FrmInputQuantityInventory.close() }.bind(this));
        $("#btnCancelSelectInventory").click(function () { this.FrmSelectInventory.close() }.bind(this));
        $("#btnCancelPayOrder").click(function () { this.FrmOrderPrint.close() }.bind(this));
        $("#btnSaveOrderDetail").click(this.btnAcceptPayOrderOnClick.bind(this));
        // Sự kiện trên Form chi tiết hóa đơn theo từng bàn:
        $('[FORM-MASTER]').on('click', '.btnAcceptPayOrder', this.btnAcceptPayOrderOnClick.bind(this)); // Nhấn vào hiển thị form thông tin hóa đơn.
        $("#frmBidaDetail #btnCloseBidaOrderDetail").click(function () { this.FrmBidaDetail.close(); clearInterval(this.FrmBidaDetail.Interval); }.bind(this));
        $('#frmBidaDetail').on('click', '#btnDeleteBidaOrder', this.btnDeleteOrderOnClick.bind(this)); // Hủy hóa đơn
        $('#frmBidaDetail').on('click', "#btnUpdateTimeStart", this.btnUpdateTimeStartOnClick.bind(this));
        $('#frmBidaDetail').on('click', "#btnForwardService", this.btnForwardServiceOnClick.bind(this));
        $('#frmBidaDetail').on('click', ".cell-delete", this.refDetailOnDelete.bind(this)); // Xóa 1 dòng chi tiết trong hóa đơn


        $("#btnPayAndPrintOrder").click(this.payAndPrintOrder.bind(this));
        $('#frmOrderPrint').on('click', '.discount', this.discountInfoOnClick.bind(this));
        $('#frmOrderPrint').on('click', '.totalAmount', this.totalAmountInfoOnClick.bind(this));
        $('#frmOrderPrint').on('blur', '.txtTotalAmount input', this.txtTotalAmountOnBlur.bind(this));
        $('#frmOrderPrint').on('blur', '.txtDiscount input', this.txtDiscountOnBlur.bind(this));

        $('#frmBidaDetail .tbInventotySelected').on('dblclick', "tr", this.changeQuantityInventorySelected.bind(this));
        $('#frmSelectTimeClock').on('click', '#btnSubmitSelectTimeClock', this.btnSubmitSelectTimeClockOnClick.bind(this));
        $('#frmSelectTimeClock').on('click', '#btnCancelSelectTimeClock', function () { this.FrmSelectTimeClock.close() }.bind(this));
        $('#frmSelectServiceChange').on('click', "#btnSubmitServiceChange", this.btnSubmitServiceChangeOnClick.bind(this));
        $('#frmSelectServiceChange').on('click', "#btnCancelSubmitServiceChange", function () { this.FrmSelectServiceChange.close() }.bind(this));
    }

    /* ----------------------------------------------------------------
     * Change Tab Panel
     * CreatedBy: NVMANH (11/11/2019)
     * ----------------------------------*/
    tabChange(e) {
        var target = $(e.target).attr("href") // activated tab
        switch (target) {
            case "#nav-home":
                break;
            case "#nav-inventory":
                inventoryJS.loadData();
                break;
            case "#nav-statistic":
                statisticJS.loadData();
                break;
            case "#nav-setting":
                break;
            default:
        }
    }
    /* ----------------------------------------------------------------
     * Click Add Inventory Button -> Show Form Select Inventory
     * CreatedBy: NVMANH (01/07/2019)
     * ----------------------------------*/
    btnAddInventoryToOrderOnClick(sender) {
        var formMaster = $(sender.currentTarget).closest("[form-master]");
        // Gán để xác định khi lựa chọn chi tiết hàng hóa sẽ đổ dữ liệu về form nào:
        this.DialogDetailMaster = formMaster;
        event.preventDefault();
        this.FrmSelectInventory.show();
        event.stopPropagation();
    }

    /* ----------------------------------------------------------------
     * Quick Add (Show form Order Detail and Form Select Inventory)
     * CreatedBy: NVMANH (01/07/2019)
     * ----------------------------------*/
    btnQuickAddOnClick(event) {
        var me = this;
        $('.bida-item').removeClass('item-selected');
        $(event.currentTarget).parents('.bida-item').addClass('item-selected');
        var refid = $('.bida-item.item-selected').data("refid");
        me.FrmBidaDetail.RefID = refid;
        me.FrmBidaDetail.ViewMode = false;
        me.FrmBidaDetail.show();
        me.FrmSelectInventory.show();
        event.stopPropagation();
    }

    /* ----------------------------------------------------------------
     * Hủy hóa đơn
     * CreatedBy: NVMANH (01/07/2019)
     * ----------------------------------*/
    btnDeleteOrderOnClick() {
        var me = this;
        commonJS.showConfirm("<div style='color:red'><b>Chú có chắc chắn muốn hủy hóa đơn này?</b></div>", function () {
            // Lấy RefID và ServiceID:
            var refId = $('.bida-item.item-selected').data("refid");
            // Thực hiện xóa và cập nhật trạng thái bàn:
            ajaxJSON.delete("refs/RefDetailAndRefService/{0}".format(refId), {}, true, function () {
                me.FrmBidaDetail.close();
                var intervalID = $('.bida-item.item-selected')[0].Interval;
                if (commonJS.Interval.indexOf(intervalID) >= 0) {
                    clearInterval(intervalID);// Xóa bộ đếm thời gian.
                }
                me.loadBidaStatus();
            })
        })
    }

    /**
     * Hiển thị form cho phép cập nhật thời gian bắt đầu sử dụng dịch vụ
     * Created by: NVMANH (04/08/2019)
     * */
    btnUpdateTimeStartOnClick() {
        $('#service-name-selected').html($('.bida-detail-title').html());
        var timeStart = $('.timeStartService')[0].StartTime;
        $(timeStartPicker).val('{0}:{1}'.format(timeStart.getHours(), timeStart.getMinutes()));
        $(dtDateSelect).val(timeStart.ddmmyyyy());
        this.FrmSelectTimeClock.show();
    }

    /**
     * Hiển thị form cho phép chuyển bàn sử dụng dịch vụ
     * Created by: NVMANH (04/08/2019)
     * */
    btnForwardServiceOnClick() {
        var me = this;
        $('.bida-name-service').html($('.bida-detail-title').html());
        me.FrmSelectServiceChange.show();
    }

    /**
     * chuyển hóa đơn dịch vụ sang bàn khác
     * Created by: NVMANH (04/08/2019)
     * */
    btnSubmitServiceChangeOnClick() {
        event.preventDefault();
        var me = this;
        // Lấy refServiceID:
        var refServiceID = me.FrmBidaDetail.RefServiceID;
        // ID service sẽ chuyển tới:
        var serviceID = $(cbxService).val();
        if (serviceID) {
            ajaxJSON.put("/rs/service/{0}/{1}".format(refServiceID, serviceID), {}, true, function (data) {
                if (data) {
                    // Clear interval và build lại:
                    var intevals = commonJS.Interval;
                    for (var i = 0; i < intevals.length; i++) {
                        clearInterval(intevals[i]);
                    }
                    commonJS.Interval = [];
                    $('.bida-item.item-selected').removeClass('item-selected');
                    me.loadBidaStatus(false);
                    // Set lại bàn chọn (để load đúng dữ liệu chi tiết);
                    var newBidaSelected = $('#' + serviceID).parents('.bida-item');
                    newBidaSelected.addClass('item-selected');
                    me.loadOrderDetail();
                    me.FrmSelectServiceChange.close();
                }
            })
        }
        event.stopPropagation();
    }

    /**
     * Cập nhật thời gian bát đầu sử dụng dịch vụ
     * Created by: NVMANH (04/08/2019)
     * */
    btnSubmitSelectTimeClockOnClick() {
        event.preventDefault();
        var me = this;
        var form = document.getElementById('frmSubmitTimeClock');
        if (!form.checkValidity()) {
            form.classList.add('was-validated');
            return;
        }
        // Lấy thông tin ngày:
        var dateTime = $(dtDateSelect).datepicker('getDate'),
            refServiceID = me.FrmBidaDetail.RefServiceID;
        // Lấy thông tin giờ:
        var time = $(timeStartPicker).val().split(':'),
            hour = parseInt(time[0]),
            min = parseInt(time[1]);
        dateTime = new Date(dateTime.setHours(hour, min));
        // Gọi Service thực hiện cập nhật lại thời gian:
        ajaxJSON.put('/rs/timeStart', { RefServiceID: refServiceID, TimeStart: dateTime }, true, function (data) {
            if (data) {
                // Clear interval và build lại:
                window.clearInterval(me.FrmBidaDetail.Interval);
                window.clearInterval($('.bida-item.item-selected')[0].Interval);

                // Build lại bộ đếm thời gian và tính tiền:
                var bidaItemIDSelected = $('.bida-item.item-selected').data('ServiceID');
                commonJS.setCalculatorTime('#' + bidaItemIDSelected, dateTime, 1000, me.calculatorMoneyByTimeUse.bind(me));
                me.setMoneyTotalForFormWithRealTime(dateTime);
                me.FrmSelectTimeClock.close();
                $('.bida-item.item-selected .bida-startTime').html('Bắt đầu lúc <strong>{0}</strong> ({1})'.format(dateTime.time(), dateTime.ddmmyyyy()));

            }
        });
        event.stopPropagation();
    }

    /* ----------------------------------------------------------------
     * Select Inventory To Ref
     * CreatedBy: NVMANH (01/07/2019)
     * ----------------------------------*/
    rowInventorySelectedOnDblClick(event) {
        var rowEl = event.currentTarget;
        var inventoryName = rowEl.cells[1].textContent;
        $('#ivt-name-selected').html(inventoryName);
        $('#frmInputQuantityInventory').dialog('option', 'title', 'Nhập số lượng');
        this.FrmInputQuantityInventory.Mode = 'add';
        this.FrmInputQuantityInventory.show();

    }

    /* ----------------------------------------------------------------
     * Thay đổi số lượng hàng hóa
     * CreatedBy: NVMANH (01/07/2019)
     * ----------------------------------*/
    changeQuantityInventorySelected() {
        if (this.FrmBidaDetail.ViewMode) {
            return;
        }
        var inventorySelected = $('#frmBidaDetail .tbInventotySelected tr.row-selected');
        var refDetailId = inventorySelected.data('id'),
            inventoryName = inventorySelected.find('td')[1].textContent,
            currentQuantity = inventorySelected.find('td')[2].textContent;
        $('#txtQuantity').val(currentQuantity);
        $('#ivt-name-selected').html(inventoryName);
        $('#frmInputQuantityInventory').dialog('option', 'title', 'Cập nhật số lượng');
        this.FrmInputQuantityInventory.Mode = 'edit';
        this.FrmInputQuantityInventory.show();

    }

    /* ----------------------------------------------------------------
     * Hủy hóa đơn
     * CreatedBy: NVMANH (01/07/2019)
     * ----------------------------------*/
    refDetailOnDelete(sender) {
        var me = this;
        var currentTarget = $(sender.currentTarget);
        var refDetailId = currentTarget.data('id');
        var currentRow = currentTarget.parents('tr');
        var inventoryName = currentRow.find('td')[1].textContent;
        commonJS.showConfirm("Chú có chắc chắn muốn xóa <b><span style='color:red'>{0}</span></b> khỏi hóa đơn này?".format(inventoryName), function () {
            ajaxJSON.delete('rd/{0}'.format(refDetailId), {}, true, function (data) {
                if (data) {
                    //currentRow.remove();
                    me.loadOrderDetail();
                }
            })
        })
    }

    /* ----------------------------------------------------------------
     * Chọn số lượng hàng hóa vào hóa đơn
     * CreatedBy: NVMANH (01/07/2019)
     * ----------------------------------*/
    btnSubmitQuantityInvSelect() {
        event.preventDefault();
        var me = this;
        var form = document.getElementById('frmSubmitQuantity');
        if (!form.checkValidity()) {
            form.classList.add('was-validated');
            return;
        }
        // Get id inventory selected:
        switch (this.FrmInputQuantityInventory.Mode) {
            case 'add':
                me.addNewRefDetail();
                break;
            case 'edit':
                me.updateQuantityRefDetail();
                break;
            default:
        }


        this.FrmInputQuantityInventory.close();
        event.stopPropagation();
    }

    /**
     * Thực hiện thêm mới chi tiết hàng hóa cho hóa đơn
     * Author: NVMANH (04/08/2019)
     * */
    addNewRefDetail() {
        var me = this;
        //TODO: thực hiện cập nhật số lượng hàng hóa với Form chi tiết hóa đơn tương ứng:
        var dialogDetaiMaster = me.DialogDetailMaster; //-- xác định xem Form chi tiết là form nào (bán hàng hay có kèm dịch vụ)
        // Get Quantity:
        var quantity = $('#txtQuantity').val();
        quantity = (quantity ? parseFloat(quantity) : 0);
        var inventoryID = commonJS.getFirstItemIdSelectedInTable($('table#tbListInventotyToSelect'));
        var recordSelected = commonJS.getFirstRowTableSelected($('table#tbListInventotyToSelect')).data('dataJson');
        var formId = this.DialogDetailMaster.attr('id');// frmBidaDetail
        // Nếu là cập nhật cho hóa đơn của bàn bida:
        switch (formId) {
            case "frmOrderDetail":
                debugger
                var inventoryHasExist = false;
                var newData = saleJS.data.map((item, index) => {
                    if (item["InventoryID"] == recordSelected["InventoryID"]) {
                        inventoryHasExist = true;
                        item.Quantity = item.Quantity + quantity;
                    }
                    return item;
                })
                saleJS.data = newData;
                if (!inventoryHasExist) {
                    recordSelected.Quantity = quantity;
                    recordSelected["UnitPrice"] = recordSelected.Price;
                    recordSelected.TotalAmount = recordSelected.Price * parseInt(quantity);
                    saleJS.data.push(recordSelected);
                }
                saleJS.buildRowHtmlData(saleJS.data);
                break;
            case "frmBidaDetail":
                // Get RefID:
                var refId = $('.bida-item.item-selected').data("refid");// this.RefId;
                // Call service:
                var refDetail = {
                    RefID: refId,
                    InventoryID: inventoryID,
                    Quantity: quantity
                }
                ajaxJSON.post("/rd", refDetail, true, function (data) {
                    //Load lại dữ liệu chi tiết hóa đơn:
                    me.loadOrderDetail();
                })
                break;
            default:
        }
    }

    /**
     * Thực hiện cập nhật số lượng chi tiết hàng hóa cho hóa đơn
     * Author: NVMANH (04/08/2019)
     * */
    updateQuantityRefDetail() {
        var me = this;
        // Get Quantity:
        var quantity = $('#txtQuantity').val();
        var refDetailId = commonJS.getFirstItemIdSelectedInTable($('#frmBidaDetail table.tbInventotySelected'));
        ajaxJSON.put("/rd/quantity", { RefDetailID: refDetailId, Quantity: quantity }, true, function (data) {
            me.loadOrderDetail();
        })
    }
    /* ----------------------------------------------------------------
     * Show info of item
     * CreatedBy: NVMANH (01/07/2019)
     * ----------------------------------*/
    loadBidaStatus(async) {
        var me = this;
        async = (async == undefined) ? true : async;
        // Get Bidas are active:
        ajaxJSON.get("/rs", {}, async, function (data) {
            var bidas = data;
            $('#listBida').empty();
            $.each(bidas, function (index, item) {
                var bidaItemHTML = '';
                var bidaItemID = item['ServiceID'];
                var startTime = new Date(item["StartTime"]);
                bidaItemHTML = $(bidaItemHTML + '<div class="col-sm-4 bida-item-flex">'
                    + '<div class="bida-item w-100">'
                    + '<div class="bida-header bida-header-active">'
                    + '<div class="bida-title">' + item['ServiceName'] + '</div>'
                    + '<div id="' + bidaItemID + '" class="bida-status"></div>'
                    + '</div>'
                    + '<div class="bida-content">'
                    + '<div class="bida-info-time" >'
                    + '    <div class="bida-info-item-icons bida-info-time-icons"></div>'
                    + '    <div class="bida-info-time-content">'
                    + '        <div class="bida-startTime bida-text-content-item">Bắt đầu lúc <strong>{0}</strong> ({1})</div>'.format(startTime.time(), startTime.ddmmyyyy())
                    + '    </div>'
                    + '</div>'
                    + '<div class="bida-info-price">'
                    + '    <div class="bida-info-item-icons bida-info-price-icons"></div>'
                    + '    <div class="bida-info-price-content bida-service-amount">Tiền bàn:</div>'
                    + '</div>'
                    + '<div class="bida-info-price inventory-info-price">'
                    + '    <div class="bida-info-item-icons bida-info-price-icons"></div>'
                    + '    <div class="bida-info-price-content">Tiền dịch vụ: <TotalAmountInventory>{0}</TotalAmountInventory> <sup>đ</sup></div>'.format(item['TotalAmountInventory'].formatMoney())
                    + '</div>'
                    + '<button class="btnActiveBida btn btn-primary btn-lg btn-block btn-bida-open">Mở cho khách</button>'
                    + '<div class="bida-totalAmount-box"></div>'
                    + '</div>'
                    + '<div class="bida-footer">'
                    + '    <button class="btn btn-danger btn-flex btnQuickAddInventory" btn-footer><i class="fas fa-cart-plus"></i> <span>Thêm nhanh</span></button>'
                    + '    <button class="btn btn-success btn-ct btn-flex btnPay" btn-footer><i class="fab fa-paypal"></i>  <span>Thanh toán</span></button>'
                    //+ '    <div class="btn btn-primary btn-flex"><span class="glyphicon glyphicon-list"></span> Chi tiết</div>'
                    + '</div>'
                    + '</div>'
                    + '</div>');
                bidaItemHTML.find('.bida-item').data('ServiceID', item['ServiceID']);
                bidaItemHTML.find('.bida-item').data('refid', item['RefID']);
                $('#listBida').append(bidaItemHTML);
                // Set time used:
                if (item['InUse']) {
                    bidaItemHTML.find('.bida-content .btnActiveBida').hide();
                    var timeStart = new Date(item['StartTime']).getTime();
                    commonJS.setCalculatorTime('#' + bidaItemID, startTime, 1000, me.calculatorMoneyByTimeUse.bind(me));
                    // Tính tiền:
                } else {
                    bidaItemHTML.find('.bida-status').append("EMPTY");
                    bidaItemHTML.find('.bida-item').addClass('bida-item-empty');
                    bidaItemHTML.find('.bida-header').addClass('bida-header-empty');
                    bidaItemHTML.find('.bida-title').addClass('bida-title-empty');
                    bidaItemHTML.find('button[btn-footer]').attr('disabled', true);
                    bidaItemHTML.find('button').removeClass('btn-danger');
                    bidaItemHTML.find('button').removeClass('btn-success');
                    bidaItemHTML.find('button[btn-footer]').addClass('btn-secondary');
                    bidaItemHTML.find('.bida-content').addClass('bida-content-empty');
                    bidaItemHTML.find('.bida-content .bida-info-time').hide();
                    bidaItemHTML.find('.bida-content .bida-info-price').hide();
                    bidaItemHTML.find('.bida-content .btnActiveBida').show();
                }

            })
        })
    }

    /** ------------------------------------------------------------------
     * Hiển thị số tiền tính toán theo thời gian sử dụng (update realtime)
     * @param {any} elForShow Element sẽ hiển thị thông tin thời gian (tính đến hiện tại)
     * @param {any} days số ngày đã sử dụng
     * @param {any} hours số giờ đã sử dụng
     * @param {any} minutes số phút đã sử dụng
     * @param {any} seconds Số giây đã sử dụng
     * Author: NVMANH (17/07/2019)
     */
    calculatorMoneyByTimeUse(elForShow, days, hours, minutes, seconds, xInterval) {
        var element = $(elForShow).first();
        // - Tổng số tiền:
        var amount = this.getTotalMoneyByHours(days, hours, minutes, seconds);

        var amountInfoHTML = 'Tiền bàn: {0} <sup>đ</sup> (40.000 <sup>đ</sup>/giờ)'.format(amount.formatMoney());
        var elShowAmountInfo = $(element).parents('.bida-item');
        elShowAmountInfo.find('.bida-service-amount').html(amountInfoHTML);
        elShowAmountInfo[0].Interval = xInterval;

        //console.log(elShowAmountInfo[0]);
        // If the count down is finished, write some text
        if (hours > 10 || days > 1) {
            //clearInterval(x);
            element.addClass("text-read-color");
            //document.getElementById("bida-status").innerHTML = "EXPIRED";
        }
        // Tổng tiền dịch vụ khác(đồ ăn, đồ uống):
        var totalAmountInventory = elShowAmountInfo.find('totalamountinventory').html().replaceAll(',', '');
        totalAmountInventory = totalAmountInventory ? parseFloat(totalAmountInventory.replaceAll(',', '')) : 0;
        var totalAmount = amount + totalAmountInventory;
        elShowAmountInfo.find('.bida-totalAmount-box').html(totalAmount.formatMoney() + ' <sup> đ</sup>');
    }

    /** ----------------------------------------------------------------
     * Lấy thông tin tổng tiền dịch vụ dựa vào tổng số thời gian sử dụng
     * @param {any} days số ngày sử dụng
     * @param {any} hours số giờ sử dụng
     * @param {any} minutes số phút sử dụng
     * @param {any} seconds số giây sử dụng
     * Author: NVMANH (17/07/2019)
     */
    getTotalMoneyByHours(days, hours, minutes, seconds) {
        // - Tổng số giờ sử dụng:
        var hoursUsed = (days * 24 + hours) + minutes / 60.00;
        hoursUsed = parseFloat(hoursUsed.toFixed(4));
        ///console.log(hoursUsed);
        // - Tổng số tiền:
        var amount = Math.ceil(hoursUsed * 40) * 1000;
        //var amount = amount.formatMoney(); // Định dạng hiển thị kiểu tiền tệ.
        return amount;
    }

    /** ----------------------------------------------------------------
     * Change item to in use
     * @param {any} event
     * Author: NVMANH (08/07/2019)
     */
    changeBidaToInUse(event) {
        var me = this;
        var currentButton = event.currentTarget;
        event.preventDefault();
        var msg = "Xác nhận chuyển bàn sang trạng thái có khách?<br/>" +
            '<div>Thời gian sẽ được bắt đầu tính từ: <br/><div class="time-start-notice"><strong>{0}</strong> (<strong>{1}</strong>)</div></div><hr/>';
        var currentDateTime = new Date();
        var time = currentDateTime.time();
        msg = msg.format(time, currentDateTime.ddmmyyyy());
        commonJS.showConfirm(msg, function (sender) {
            var bidaEl = $(currentButton).parents('.bida-item');
            var bidaID = bidaEl.data('ServiceID');
            ajaxJSON.patch("/service/edit/inuser", [bidaID, true], true, function (res) {
                bidaEl.data('refid', res);
                bidaEl.find('.bida-status').html('');;
                bidaEl.removeClass('bida-item-empty');
                bidaEl.find('.bida-header').removeClass('bida-header-empty');
                bidaEl.find('.bida-title').removeClass('bida-title-empty');
                bidaEl.find('button[btn-footer]').removeAttr('disabled');
                bidaEl.find('.btnQuickAddInventory').addClass('btn-danger');
                bidaEl.find('.btnPay').addClass('btn-success');
                bidaEl.find('button[btn-footer]').removeClass('btn-secondary');
                bidaEl.find('.bida-content').removeClass('bida-content-empty');
                bidaEl.find('.bida-content .bida-info-time').show();
                bidaEl.find('.bida-content .bida-info-price').show();
                bidaEl.find('.bida-content .btnActiveBida').hide();
                //me.setCalculatorTime(bidaID, currentDateTime);
                commonJS.setCalculatorTime('#' + bidaID, currentDateTime, 1000, me.calculatorMoneyByTimeUse.bind(me));
                // Hiển thị thời gian bắt đầu:
                var timeEl = bidaEl.find('.bida-info-time-content');
                timeEl.html('Bắt đầu lúc <strong>{0}</strong> ({1})'.format(currentDateTime.time(), currentDateTime.ddmmyyyy()));
            })
        });
        event.stopPropagation();
    }

    /** ----------------------------------------------------------------
    * Show Ref detail (Click vào các đối tượng thì hiển thị Form chi tiết)
    * @param {any} event
    * Author: NVMANH (08/07/2019)
    */
    bidaItemOnClick(event) {
        $('.bida-item').removeClass('item-selected');
        var currentTarget = event.currentTarget;
        currentTarget.classList.add('item-selected');
        // Chỉ hiện thị form chi tiết nếu bàn đang có khách:
        if (!$(currentTarget).hasClass('bida-item-empty')) {
            var refid = $('.bida-item.item-selected').data("refid");
            this.FrmBidaDetail.RefID = refid;
            this.FrmBidaDetail.ViewMode = false;
            this.FrmBidaDetail.show();
        }
    }

    initFrmSelectTimeClockBeforeOpen() {

    }

    initFrmSelectServiceChangeBeforeOpen() {
        var me = this;
        // Build lại dữ liệu cho combobox chọn dịch vụ chuyển tới:
        var comboboxSelectService = $('#cbxService');
        comboboxSelectService.empty();
        ajaxJSON.get("/service/ServiceNotInUse", {}, true, function (data) {
            if (data) {
                $.each(data, function (index, item) {
                    var optionHTML = '<option value="{0}">{1}</option>'.format(item['ServiceID'], item['ServiceName']);
                    comboboxSelectService.append(optionHTML);
                })
            }
        })
    }
    /** ----------------------------------------------------------------
     * Thiết lập hiển thị cho Form chọn mặt hàng
     * @param {any} event
     * Author: NVMANH (07/07/2019)
     */
    initFrmSelectInventoryBeforeOpen(event) {
        var me = this;
        // Gán RefId hiện tại của bản để phục vụ một số việc:
        me.RefId = $(event.currentTarget).parents('.bida-item').data("refid");
        //load danh sách hàng hóa để lựa chọn:
        commonJS.showMask('#frmSelectInventory');
        ajaxJSON.get("/inventories", {}, true, function (data) {
            commonJS.buidDataToTable($('table#tbListInventotyToSelect'), data);
            commonJS.hideMask('#frmSelectInventory');
        })
    }

    /** ----------------------------------------------------------------
     * Thiết lập dữ liệu và hiển thị cho Form chi tiết hóa đơn
     * Author: NVMANH (07/07/2019)
     */
    loadOrderDetail(sender) {
        var me = this;
        $('.totalMoney').empty();
        var formMaster = this.FrmBidaDetail.Dialog;
        // Gán để xác định khi lựa chọn chi tiết hàng hóa sẽ đổ dữ liệu về form nào:
        this.DialogDetailMaster = formMaster;
        var refid = me.FrmBidaDetail.RefID;

        // Clear interval nếu có tránh lỗi thực hiện tính toán bị ghi đè nhiều lần:
        if (me.FrmBidaDetail.Interval) {
            clearInterval(this.FrmBidaDetail.Interval);
        }
        ajaxJSON.get("/refs/refdetail/" + refid, {}, true, function (data) {
            var refDetails = data["RefDetails"];
            var refServices = data["RefServices"];
            var refState = data["RefState"];
            if (refServices.length > 0) {
                var refService = refServices[0],
                    timeStart = new Date(refService['StartTime']);
                var endTime = refService['EndTime'];
                if (!refState) {
                    me.setMoneyTotalForFormWithRealTime(timeStart);
                } else {
                    endTime = (endTime ? new Date(endTime) : new Date());
                    me.setMoneyTotalWithEndTime(data, timeStart, endTime);
                }
                $('.bida-detail-title').html(refService['ServiceName']);
                me.FrmBidaDetail.RefServiceID = refService["RefServiceID"];
            }
            //commonJS.buidDataToTable($("table#tbServiceUsed"), refServices, me.setMoneyServiceCalculatorForFormDetail.bind(me));
            commonJS.buidDataToTable($("#frmBidaDetail table.tbInventotySelected"), refDetails);
            // Tổng tiền thanh toán với dịch vụ:
            var totalAmountService = data["TotalAmountService"];
            $("table#tbServiceUsed").data('totalAmountService', totalAmountService);
            $("table#tbServiceUsed").data('refServices', refServices);
            // Tổng tiền thanh toán đối với hàng hóa:
            var totalAmountInventory = data["TotalAmountInventory"];
            $("#frmBidaDetail table.tbInventotySelected").data('totalAmountInventory', totalAmountInventory);
            $("#frmBidaDetail table.tbInventotySelected").data('refDetails', refDetails);
            $('.bida-item.item-selected').find('.inventory-info-price .bida-info-price-content').html('Tiền dịch vụ: <TotalAmountInventory>{0}</TotalAmountInventory> <sup>đ</sup></div>'.format(totalAmountInventory.formatMoney()));
            $('#frmBidaDetail .refDetailToolbar-title').html('+{0}<sup>đ</sup>'.format(totalAmountInventory.formatMoney()));
            //var totalAmount = "{0}{1}".format(data["TotalAmount"].formatMoney(), '<sup>đ</sup>');
            //$('.totalMoney').html(totalAmount);
            // Gán value vào data để lấy lúc cần:
            //$('.totalMoney').data('totalMoney', data["TotalAmount"]);
            me.hideControlInFrmBidaDetailByView();
        })
    }

    /** ----------------------------------------------------------------------------
     * Hiển thị thông tin tổng tiền trên form chi tiết (Đã xác định thời gian dừng dịch vụ)
     * Thông thường sử dụng cho màn hình xem chi tiết thống kê
     * @param {any} ref hóa đơn đang xem hiện tại
     * @param {any} startTime Giờ bắt đầu
     * @param {any} endTime    Giờ kết thúc
     * Author: NVMANH (17/07/2019)
     */
    setMoneyTotalWithEndTime(ref, startTime, endTime) {
        var elTotalAmountService = $('.totalAmountService')[0],
            elTimeStartService = $('.timeUsedService')[0];
        var timeInfo = commonJS.getTimeDistance(startTime, endTime);
        var days = timeInfo.Days;
        var hours = timeInfo.Hours;
        var minutes = timeInfo.Minutes;
        var seconds = timeInfo.Seconds;
        var minutesText = minutes < 10 ? ('0' + minutes) : minutes;
        var hoursText = hours < 10 ? ('0' + hours) : hours;

        // Hiển thị thông tin thời gian bắt đầu sử dụng dịch vụ:
        $('.timeStartService').html('<b>{0}</b> ({1})'.format(startTime.hhmmss(), startTime.ddmmyyyy()));
        $('.timeStartService')[0].StartTime = startTime;

        // Hiển thị thông tin tổng thời gian sử dụng:
        var timeDetailHTML = !days ? ('{0}:{1}:{2}'.format(hoursText, minutesText, seconds)) : ('{0}d {1}:{2}:{3}'.format(days, hoursText, minutesText, seconds));
        $(elTimeStartService).html(timeDetailHTML);

        // Hiển thị thông tin tổng tiền sử dụng dịch vụ:
        var totalMoneyService = ref['TotalAmountService'];//this.getTotalMoneyByHours(days, hours, minutes, seconds);
        var totalMoneyServiceHTML = '+{0} <sup>đ</sup>'.format(totalMoneyService.formatMoney());
        $(elTotalAmountService).html(totalMoneyServiceHTML);
        $(elTotalAmountService).data("totalAmountService", totalMoneyService);

        // Lấy tổng tiền sử dụng hàng hóa:
        var totalAmountInventory = ref['TotalAmountInventory'];

        // Lấy tổng tiền hiện tại:
        var currentTotalAmount = (totalMoneyService + (totalAmountInventory || 0));

        // Cập nhật tổng tiền của hóa đơn lên giao diện:
        var totalAmount = "{0}{1}".format(currentTotalAmount.formatMoney(), '<sup>đ</sup>');
        $('#frmBidaOrderDetail .totalMoney').data('totalMoney', currentTotalAmount);
        $('#frmBidaOrderDetail .totalMoney').html(totalAmount);
    }

    /** ----------------------------------------------------------------------------
     * Hiển thị thông tin tổng tiền trên form chi tiết (tự tăng theo thời gian thực)
     * @param {any} startTime Giờ bắt đầu sử dụng dịch vụ
     * Author: NVMANH (17/07/2019)
     */
    setMoneyTotalForFormWithRealTime(startTime) {
        var elTotalAmountService = $('.totalAmountService')[0],
            elTimeStartService = $('.timeUsedService')[0];
        commonJS.setCalculatorTime(elTimeStartService, startTime, 1000, function (elTimeStartService, days, hours, minutes, second, x) {
            var minutesText = minutes < 10 ? ('0' + minutes) : minutes;
            var hoursText = hours < 10 ? ('0' + hours) : hours;
            var timeDetailHTML = !days ? ('{0}:{1}:{2}'.format(hoursText, minutesText, second)) : ('{0}d {1}:{2}:{3}'.format(days, hoursText, minutesText, second));
            $(elTimeStartService).html(timeDetailHTML);
            var amount = this.getTotalMoneyByHours(days, hours, minutes, second);
            // Lưu lại thông tin Interval, khi close thì clear đi tránh lỗi sai ghi đè tính toán cho các lần mở kê tiếp:
            this.FrmBidaDetail.Interval = x;
            var amountInfoHTML = '+{0} <sup>đ</sup>'.format(amount.formatMoney());
            $(elTotalAmountService).html(amountInfoHTML);
            $(elTotalAmountService).data("totalAmountService", amount);
            // Cập nhật tổng tiền của hóa đơn lên giao diện:
            // Lấy tổng tiền hiện tại:
            var currentTotalAmount = $('#frmBidaOrderDetail .totalMoney').data('totalMoney');
            var totalAmountInventory = $("#frmBidaDetail table.tbInventotySelected").data('totalAmountInventory');
            currentTotalAmount = (amount + totalAmountInventory);
            var totalAmount = "{0}{1}".format(currentTotalAmount.formatMoney(), '<sup>đ</sup>');
            $('#frmBidaOrderDetail .totalMoney').data('totalMoney', currentTotalAmount);
            $('#frmBidaOrderDetail .totalMoney').html(totalAmount);
            $('.timeStartService').html('<b>{0}</b> ({1})'.format(startTime.hhmmss(), startTime.ddmmyyyy()));
            $('.timeStartService')[0].StartTime = startTime;
        }.bind(this));
    }

    /** ----------------------------------------------------------------------------
     * Hiển thị thông tin tổng tiền trên form chi tiết (tự tăng theo thời gian thực)
     * @param {any} fieldName Trường thông tin cần hiển thị
     * @param {any} fieldValue Giá trị
     * @param {any} item    Đối tượng hiện tại đang tương tác.
     * Author: NVMANH (17/07/2019)
     */
    setMoneyTotalForFormWithRealTime2(fieldName, fieldValue, item) {
        if (fieldName === "TotalAmount") {
            var el = $('<div class="text-align-right"></div>')[0],
                startTime = new Date(item["StartTime"]);
            commonJS.setCalculatorTime(el, startTime, 1000, function (el, days, hours, minutes, second, x) {
                var amount = this.getTotalMoneyByHours(days, hours, minutes, second);
                // Lưu lại thông tin Interval, khi close thì clear đi tránh lỗi sai ghi đè tính toán cho các lần mở kê tiếp:
                this.FrmBidaDetail.Interval = x;
                var amountInfoHTML = '{0} <sup>đ</sup>'.format(amount.formatMoney());
                $(el).html(amountInfoHTML);
                //var oldTotalAmountService = $(el).data("totalAmountService");
                //oldTotalAmountService = oldTotalAmountService ? oldTotalAmountService : amount;
                $(el).data("totalAmountService", amount);
                //if (amount != oldTotalAmountService) {
                // Cập nhật tổng tiền của hóa đơn lên giao diện:
                // Lấy tổng tiền hiện tại:
                var currentTotalAmount = $('#frmBidaOrderDetail .totalMoney').data('totalMoney');
                var totalAmountInventory = $("#frmBidaDetail table.tbInventotySelected").data('totalAmountInventory');
                //console.log('------------------------------------------------------');
                //console.log(amount);
                //console.log(totalAmountInventory);
                //console.log(currentTotalAmount);
                currentTotalAmount = (amount + totalAmountInventory);
                var totalAmount = "{0}{1}".format(currentTotalAmount.formatMoney(), '<sup>đ</sup>');
                $('#frmBidaOrderDetail .totalMoney').data('totalMoney', currentTotalAmount);
                $('#frmBidaOrderDetail .totalMoney').html(totalAmount);
                //}
            }.bind(this));
            fieldValue = el;
        }
        return fieldValue;
    }
    /**
     * Ẩn hiện các button chức năng theo Mode của Form chi tiết
     * */
    hideControlInFrmBidaDetailByView() {
        if (this.FrmBidaDetail.ViewMode) {
            $('.service-toolbar').hide();
            $('.refDetailToolbar-btn').hide();
            $('.cell-delete').hide();
            $('#btnAcceptPayBidaOrder').hide();
            $('#btnDeleteBidaOrder').hide();
        } else {
            $('.service-toolbar').show();
            $('.refDetailToolbar-btn').show();
            $('.cell-delete').show();
            $('#btnAcceptPayBidaOrder').show();
            $('#btnDeleteBidaOrder').show();
        }
    }

    /** ----------------------------------------------------------------
     * Reset lại form chọn số lượng mặt hàng
     * @param {any} event
     * Author: NVMANH (07/07/2019)
     */
    initFrmInputQuantityInventory() {
        var form = document.getElementById('frmSubmitQuantity');
        form.reset();
        $('#txtQuantity').select();
    }

    /**-----------------------------------------------------------------
     * Thiết lập hiển thị dữ liệu cho form hóa đơn
     * Author: NVMANH (30/07/2019)
     * */
    initFrmOrderPrint() {
        //$('.totalAmount').empty();
        //$('.txtTotalAmount').empty();
        // Sinh hóa đơn mới, sinh mã hóa đơn mới:
        var orderCode;
        ajaxJSON.get("/refs/NewRefCode", {}, true, function (res) {
            if (res) {
                $("#REF_CODE").html(res);
            }
        })
        var me = this;
        // thực hiện cập nhật số lượng hàng hóa với Form chi tiết hóa đơn tương ứng:
        var dialogDetaiMaster = me.DialogDetailMaster; //-- xác định xem Form chi tiết là form nào (bán hàng hay có kèm dịch vụ)
        var formId = this.DialogDetailMaster.attr('id');// frmBidaDetail
        switch (formId) {
            case "frmOrderDetail":
                me.initFrmOrderPrintForSaleOrder();
                break;
            case "frmBidaDetail":
                me.initFrmOrderPrintForBidaOrderDetail();
                break;
            default:
        }
    }

    /**
     * Hiển thị chi tiết hóa đơn bán hàng (chỉ bán hàng không có dịch vụ kèm theo)
     * */
    initFrmOrderPrintForSaleOrder() {
        var me = this;
        $("#frmOrderPrint .order-title").html("Bán hàng");
        $("#frmOrderPrint .timeInfo").hide();
        $("#frmOrderPrint #total-info-box").hide();
        $('#frmOrderPrint #refDetail').empty();
        var refDetails = saleJS.data;
        if (refDetails.length > 0) {
            var totalAmount = 0;
            $.each(refDetails, function (index, item) {
                var itemName = item["InventoryName"],
                    unitPrice = item["Price"],
                    quantity = parseInt(item["Quantity"]),
                    amount = unitPrice * quantity;
                totalAmount += amount;
                var itemHTML = '<div class="refItem">'
                    + '<div class="itemName"> <b>{0}</b></div >'
                    + '<div class="itemDetail display-flex" style="display:flex; padding:0 3px;">'
                    + '    <div class="quantity text-align-left">{1} x {2}</div>'
                    + '    <div class="total text-align-right">{3}<sup>đ</sup><span>&nbsp;&nbsp;&nbsp;</span></div>'
                    + '</div>'
                    + '</div>';
                itemHTML = itemHTML.format(itemName, unitPrice.formatMoney(), quantity, amount.formatMoney());
                $('#frmOrderPrint #refDetail').append(itemHTML);
            })
            // Tổng tiền:
            $('#frmOrderPrint .totalAmount span').html(totalAmount.formatMoney());
            $('#frmOrderPrint .txtTotalAmount input').val(totalAmount);
            me.FrmOrderPrint.TotalAmount = totalAmount;
        }
    }

    /**
     * Hiển thị chi tiết hóa đơn bán hàng (Hóa đơn theo bàn Bida)
     * */
    initFrmOrderPrintForBidaOrderDetail() {
        var me = this;
        $("#frmOrderPrint .timeInfo").show();
        $("#frmOrderPrint #total-info-box").show();
        var refid = $('.bida-item.item-selected').data("refid");
        if (refid) {
            me.FrmOrderPrint.RefID = refid;
            ajaxJSON.get("/refs/refdetail/" + refid, {}, true, function (data) {
                var refDetails = data["RefDetails"];
                var refServices = data["RefServices"];
                // Hiển thị thông tin bàn:
                if (refServices.length > 0) {
                    var serviceName = refServices[0]["ServiceName"],
                        startTime = new Date(refServices[0]["StartTime"]),
                        endTime = new Date(refServices[0]["EndTimeToPay"]),
                        totalTime = endTime - startTime;
                    // Total hours:
                    var totalHours = Math.floor(totalTime / 3600000);
                    // Số phút còn dư:
                    var minutes = Math.floor((totalTime % 3600000) / 60000);

                    // Ngày, giờ bắt đầu:
                    var startTimeText = startTime ? startTime.hhmmss() : '';
                    var dateStart = startTime ? startTime.ddmmyyyy() : '';

                    // Ngày giờ kết thúc:
                    var endTimeText = endTime ? endTime.hhmmss() : '';
                    var dateEnd = endTime ? endTime.ddmmyyyy() : '';
                    me.FrmOrderPrint.EndTime = endTime;

                    $('#frmOrderPrint .order-title').html(serviceName);
                    $('#frmOrderPrint .timeStart').html('{0} ({1})'.format(startTimeText, dateStart));
                    $('#frmOrderPrint .timeEnd').html('{0} ({1})'.format(endTimeText, dateEnd));
                    $('#frmOrderPrint .timeTotal').html('{0} giờ {1} phút '.format(totalHours, minutes));
                }
                $('#frmOrderPrint #refDetail').empty();
                if (refDetails.length > 0) {
                    var totalAmount = 0;
                    $.each(refDetails, function (index, item) {
                        var itemName = item["InventoryName"],
                            unitPrice = item["UnitPrice"],
                            quantity = item["Quantity"],
                            amount = unitPrice * quantity;
                        totalAmount += amount;
                        var itemHTML = '<div class="refItem">'
                            + '<div class="itemName"> <b>{0}</b></div >'
                            + '<div class="itemDetail display-flex" style="display:flex; padding:0 3px;">'
                            + '    <div class="quantity text-align-left">{1} x {2}</div>'
                            + '    <div class="total text-align-right">{3}<sup>đ</sup><span>&nbsp;&nbsp;&nbsp;</span></div>'
                            + '</div>'
                            + '</div>';
                        itemHTML = itemHTML.format(itemName, unitPrice.formatMoney(), quantity, amount.formatMoney());
                        $('#frmOrderPrint #refDetail').append(itemHTML);
                    })

                }
                // Tổng tiền thanh toán với dịch vụ:
                var totalAmountService = data["TotalAmountService"];
                // Tổng tiền thanh toán đối với hàng hóa:
                var totalAmountInventory = data["TotalAmountInventory"];
                var totalAmount = data["TotalAmount"];
                $('#frmOrderPrint .totalInventoryInfo').html(totalAmountInventory.formatMoney());
                $('#frmOrderPrint .totalServiceInfo').html(totalAmountService.formatMoney());

                // Tổng tiền:
                $('#frmOrderPrint .totalAmount span').html(totalAmount.formatMoney());
                $('#frmOrderPrint .txtTotalAmount input').val(totalAmount);
                me.FrmOrderPrint.TotalAmount = totalAmount;
            })
        } else {
            commonJS.showWarning("Vui lòng chọn bàn trước khi thực hiện In hóa đơn thanh toán!");
        }
    }
    save() {

    }

    /** ------------------------------------------------
     * Nhấn button Thanh toán và In hóa đơn thì hiển thị Form hóa đơn chi tiết:
     * @param {any} event
     * Author: NVMANH (31/07/2019)
     */
    btnAcceptPayOrderOnClick(event) {
        event.preventDefault();
        this.FrmOrderPrint.show();
        event.stopPropagation();
    }

    /** ------------------------------------------------
     * Thực hiện thanh toán và In hóa đơn
     * @param {any} event
     * Author: NVMANH (31/07/2019)
     */
    payAndPrintOrder(event) {
        var me = this;
        // thực hiện cập nhật số lượng hàng hóa với Form chi tiết hóa đơn tương ứng:
        var dialogDetaiMaster = me.DialogDetailMaster; //-- xác định xem Form chi tiết là form nào (bán hàng hay có kèm dịch vụ)
        var formId = this.DialogDetailMaster.attr('id');// frmBidaDetail
        debugger;
        switch (formId) {
            case "frmOrderDetail":
                return me.payAndPrintSaleOrder();
                break;
            case "frmBidaDetail":
                return me.payAndPrintBidaOrder();
                break;
            default:
        }
        return true;
    }

    payAndPrintSaleOrder() {
        var me = this;
        // Build Ref:
        var ref = {
            RefNo: $("#REF_CODE").html(),
            RefDate: (new Date()).toLocaleString(),
            RefDetail: saleJS.data,
            TotalAmount: me.FrmOrderPrint.TotalAmount
        }

        ajaxJSON.post("/refs/RefSale", ref, true, function (res) {
            me.FrmOrderPrint.close();
            me.FrmOrderPrint.RefID = null;
            me.FrmOrderPrint.TotalAmount = 0;
            me.FrmOrderPrint.EndTime = null;
            saleJS.FrmOrderDetail.close();
        })

        var mywindow = window.open('Hóa đơn thanh toán', 'PRINT', 'height=1024,width=1280');
        mywindow.document.write('<html><head><title>In hóa đơn thanh toán</title>');
        mywindow.document.write(document.getElementById("orderPrint").innerHTML);
        mywindow.document.write('</body></html>');

        mywindow.document.close(); // necessary for IE >= 10
        mywindow.focus(); // necessary for IE >= 10*/

        mywindow.print();
        mywindow.close();
        return true;
    }

    payAndPrintBidaOrder() {
        var me = this;
        // Update ref and service:
        var refId = me.FrmOrderPrint.RefID,
            totalAmount = me.FrmOrderPrint.TotalAmount,
            timeEnd = me.FrmOrderPrint.EndTime.formatDateStringInvariantCulture();
        var paramObject = {
            refId: refId,
            totalAmount: totalAmount,
            timeEnd: timeEnd
        }
        ajaxJSON.put("/refs/RefAndService", paramObject, true, function (data) {
            me.FrmOrderPrint.close();
            me.FrmOrderPrint.RefID = null;
            me.FrmOrderPrint.TotalAmount = 0;
            me.FrmOrderPrint.EndTime = null;
            me.FrmBidaDetail.close();
            clearInterval($('.bida-item.item-selected')[0].Interval); // Xóa bộ đếm thời gian.
            me.loadBidaStatus();
        });

        var mywindow = window.open('Hóa đơn thanh toán', 'PRINT', 'height=1024,width=1280');
        mywindow.document.write('<html><head><title>In hóa đơn thanh toán</title>');
        mywindow.document.write(document.getElementById("orderPrint").innerHTML);
        mywindow.document.write('</body></html>');

        mywindow.document.close(); // necessary for IE >= 10
        mywindow.focus(); // necessary for IE >= 10*/

        mywindow.print();
        mywindow.close();
        return true;
    }
    discountInfoOnClick() {
        $('.txtDiscount').show();
        $('.discount').hide();
        $('.txtDiscount input').focus();
        $('.txtDiscount input').select();
    }

    totalAmountInfoOnClick() {
        $('.txtTotalAmount').show();
        $('.totalAmount').hide();
        $('.txtTotalAmount input').focus();
    }

    txtDiscountOnBlur(sender) {
        var amountDiscount = parseFloat(sender.currentTarget.value);
        if (amountDiscount) {
            var originTotalAmount = this.FrmOrderPrint.TotalAmount,
                totalAmount = originTotalAmount - amountDiscount;
            this.setValueForTotalAmountInOrder(totalAmount, amountDiscount);
        }
        $('.txtDiscount').hide();
        $('.discount').show();
    }

    txtTotalAmountOnBlur() {
        var totalAmount = this.value;
        $('.txtTotalAmount').hide();
        $('.totalAmount').show();
        $('.totalAmount input').select();
    }

    setValueForTotalAmountInOrder(totalPay, discount) {
        $('.txtTotalAmount input').val(totalPay);
        $('.totalAmount span').html(totalPay.formatMoney());
        $('.discount span').html(discount.formatMoney());
        $('.txtDiscount input').val(discount);
        this.FrmOrderPrint.TotalAmount = totalPay;
    }
}

$('#myTab a').on('click', function (e) {
    e.preventDefault()
    $(this).tab('show')
    alert(1);
})

$(function () {
    $("#dtDateSelect").datepicker({
        changeMonth: false,
        changeYear: false,
        dateFormat: 'dd/mm/yy'
    });
});