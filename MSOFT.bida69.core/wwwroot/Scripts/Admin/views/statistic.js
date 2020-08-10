
$(document).ready(function () {
    statisticJS = new Statistic();
})

class Statistic {
    constructor() {
        this.initEvent();
        this.inintForm();
    }

    inintForm() {
        $(dtFromDate).val((new Date()).ddmmyyyy());//$(dtFromDate)..datepicker("getDate")
        $(dtToDate).val((new Date()).ddmmyyyy());
    }

    initEvent() {
        $('#cbxSelectDateRange').change(this.selectDateRange);
        $('#btnStatistic').click(this.btnStatisticOnClick.bind(this));
        $('#tbStatistic').on('dblclick', 'tbody tr', this.rowStatisticOnClick);

    }

    loadData() {
        // Lấy thông tin thời gian bắt đầu, thời gian kết thúc:
        var fromDate = $('#dtFromDate').datepicker("getDate");
        var toDate = $('#dtToDate').datepicker("getDate");
        if (fromDate && toDate) {
            fromDate = '{0}-{1}-{2}'.format(fromDate.getFullYear(), fromDate.getMonth() + 1, fromDate.getDate());
            toDate = '{0}-{1}-{2}'.format(toDate.getFullYear(), toDate.getMonth() + 1, toDate.getDate());
            // Gọi Service thực hiện thống kê:
            ajaxJSON.get("/refs/RefDataStatistic/{0}/{1}".format(fromDate, toDate), {}, true, this.buidRefDataStatistic.bind(this));
        }
    }

    selectDateRange() {
        var optionSelected = $(this).find('option:selected').val(),
            dtFromDate = document.getElementById('dtFromDate'),
            dtToDate = document.getElementById('dtToDate'),
            timeRange = { StartDateTime: null, EndDateTime: null };
        switch (optionSelected) {
            case "Today":
                timeRange = commonJS.getRangeDateTimeInDay(new Date());
                break;
            case "ThisWeek":
                timeRange = commonJS.getRangeDateTimeWeekOfDate(new Date());
                break;
            case "ThisMonth":
                timeRange = commonJS.getRangeDateTimeMonthOfDate(new Date());
                break;
            case "ThisQuarter":
                timeRange = commonJS.getRangeDateTimeQuarterOfDate(new Date());
                break;
            case "ThisYear":
                timeRange = commonJS.getRangeDateTimeYearOfDate(new Date());
                break;
            default:
        }
        $(dtFromDate).val(timeRange['StartDateTime'].ddmmyyyy());//$(dtFromDate)..datepicker("getDate")
        $(dtToDate).val(timeRange['EndDateTime'].ddmmyyyy());
    }

    btnStatisticOnClick() {
        event.preventDefault();
        this.loadData();
        event.stopPropagation();
    }

    /** -------------------------------------------
     * Build thông tin dữ liệu thống kê 
     * @param {any} data dữ liệu thống kê lấy từ server
     * Author: NVMANH (01/11/2011)
     */
    buidRefDataStatistic(data) {
        var totalAmount = 0,
            totalAmountService = 0,
            totalAmountInventory = 0;
        commonJS.buidDataToTable($('table#tbStatistic'), data, function (fieldName, fieldValue, item) {
            if (fieldName === "RefDate") {
                fieldValue = new Date(fieldValue);
                fieldValue = '<center>{0} ({1})</center>'.format(fieldValue.ddmmyyyy(), fieldValue.hhmmss());
            }

            // Tính cộng dồn tổng doanh thu (Không cộng dồn với các hóa đơn bị hủy)
            if (item['RefState'] !== 3) {
                switch (fieldName) {
                    case "TotalAmountService":
                        totalAmountService += fieldValue || 0;
                        break;
                    case "TotalAmountInventory":
                        totalAmountInventory += fieldValue || 0;
                        break;
                    case "TotalAmount":
                        totalAmount += fieldValue || 0;
                        break;
                    default:
                }

            }

            if ((fieldName === "TotalAmount" || fieldName === "TotalAmountService" || fieldName === "TotalAmountInventory") && fieldValue > 0) {
                fieldValue = $('<div class="text-align-right"></div>').append(fieldValue.formatMoney());
            }
            if (fieldName === "RefState") {
                switch (fieldValue) {
                    case 0:
                        fieldValue = '<span>Chưa thanh toán</span>';
                        break;
                    case 1:
                        fieldValue = "<span class='ref-payed-text'>Đã thanh toán</span>";
                        break;
                    case 2:
                        fieldValue = '<span style="color:#f0ad4e">Hóa đơn nợ</span>';
                        break;
                    case 3:
                        fieldValue = '<span class="ref-deleted-text">Hóa đơn bị hủy</span>';
                        break;
                    default:
                        fieldValue = '<span>Chưa thanh toán</span>';
                        break;
                }
            }
            if (item['RefState'] === 3) {
                fieldValue = $('<span class="text-decoration-through-red"></span>').append(fieldValue);
            }
            if (item['RefState'] === 2) {
                fieldValue = $('<span style="color:#f0ad4e"></span>').append(fieldValue);
            }
            return fieldValue;
        });
        if (totalAmount && totalAmount != null) {
            $("#totalAmountStatistic").html(totalAmount.formatMoney() + '<sup>đ</sup>');
        }
        if (totalAmountService && totalAmountService != null) {
            $("#totalAmountService").html(totalAmountService.formatMoney() + '<sup>đ</sup>');
        }
        if (totalAmountInventory && totalAmountInventory != null) {
            $("#totalAmountInventory").html(totalAmountInventory.formatMoney() + '<sup>đ</sup>');
        }

    }

    rowStatisticOnClick() {
        adminJS.FrmBidaDetail.RefID = $(this).data('id');
        adminJS.FrmBidaDetail.ViewMode = true;
        adminJS.FrmBidaDetail.show();
        $('.service-toolbar').hide();
        $('.refDetailToolbar-btn').hide();
        $('.cell-delete').hide();
        $('#btnAcceptPayOrder').hide();
        $('#btnDeleteOrder').hide();
    }
}
$(function () {
    $("#dtFromDate").datepicker({
        changeMonth: false,
        changeYear: false,
        dateFormat: 'dd/mm/yy'
    });

    $("#dtToDate").datepicker({
        changeMonth: false,
        changeYear: false,
        dateFormat: 'dd/mm/yy'
    });
});