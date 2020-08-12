
$(document).ready(function () {
    $(document).click(commonJS.setHideOnClickOutSide);
    $(document).on('click', 'table tbody tr', commonJS.rowTable_OnClick);
    $('.text-required').on('blur', commonJS.requiredValidation);
    //$('.clockpicker').clockpicker();
    $(document).on("click", function () {
        $('.hide-if-out').hide();
    });
});

var commonJS = {
    Interval: [],
    showMask: function (sender) {
        if (sender) {
            $(sender).addClass('loading');
        } else {
            $('html').addClass('loading');
        }
    },
    hideMask: function (sender) {
        if (sender) {
            $(sender).removeClass('loading');
        } else {
            $('html').removeClass('loading');
        }
    },
    showLoadingBody: function () {
        var html = '<div class="loading-body"></div >';
        $('body').append(html);
    },
    hideLoadingBody: function () {
        var html = '<div class="loading-body"></div >';
        $('.loading-body').remove();
    },
    /* -------------------------------------
     * Hiển thị câu thông báo
     */
    showNotice: function (msg, width) {
        var msgBox = $('body').find('#message-box');
        if (msgBox.length) {
            msgBox.remove();
        }
        var html = '<div id="message-box" title="Thông báo">' +
            msg +
            '</div >';
        $('body').append(html);
        // Hiển thị Popup:
        $(function () {
            $("#message-box").dialog({
                closeOnEscape: false,
                modal: true,
                resizable: false,
                fluid: true,
                class: "bottom-dialogmessage",
                width: 'auto',
                maxWidth: width ? width : 400,
                open: function () {
                    var $this = $(this);
                    // focus first button and bind enter to it
                    var buttons = $this.parent().find('.ui-dialog-buttonpane button');
                    $this.parent().find('.ui-dialog-buttonpane button:first').focus();
                },
                create: function () {
                    var $this = $(this);
                    // focus first button and bind enter to it
                    var buttons = $this.parent().find('.ui-dialog-buttonpane button');
                    $this.parent().find('.ui-dialog-buttonpane button:first').focus();
                    $this.keypress(function (e) {
                        if (e.keyCode === $.ui.keyCode.ENTER) {
                            $this.parent().find('.ui-dialog-buttonpane button:first').click();
                            return false;
                        }
                    });
                },
                buttons: [
                    {
                        id: 'btnOK',
                        class: 'btn btn-primary',
                        width: '',
                        html: '<i class="fa fa-check"></i> Đồng ý',
                        click: function () {
                            $(this).dialog("close");
                        }
                    }
                ]
            });

        });
    },

    /* -----------------------------------------
     * Hiển thị hộp thoại cảnh báo
     * Created by: NVMANH (03/03/2018)
     */
    showWarning: function (msg) {

    },
    /* -----------------------------------------
     * Hiển thị hộp thoại xác nhận xóa dữ liệu
     * Created by: NVMANH (03/03/2018)
     */
    showConfirm: function (msg, confirmCallBack) {
        if (!$('body').find('#message-box').length) {
            var html = $('<div id="message-box" title="Xác nhận"></div >');
            $(html).html(msg);
            $('body').append(html);
        } else {
            $('#message-box').html(msg);
        }

        $(function () {
            $("#message-box").dialog({
                modal: true,
                resizable: false,
                fluid: true,
                class: "bottom-dialogmessage",
                buttons: [
                    {
                        id: 'btnAcceptDelete',
                        class: 'btn btn-primary',
                        width: '',
                        html: '<i class="fa fa-check"></i> Đồng ý',
                        click: function () {
                            confirmCallBack();
                            $(this).dialog("close");
                        }
                    },
                    {
                        id: 'btnCancelDelete',
                        class: 'btn btn-warning',
                        width: '',
                        html: '<i class="fa fa-close"></i> Hủy bỏ',
                        click: function () {
                            $(this).dialog("close");
                        }
                    }
                ]
            });
        });
    },

    /* -----------------------------------------
     * Hiển thị hộp thoại thông báo thành công
     * Created by: NVMANH (03/03/2018)
     */
    showSuccessMsg2: function (msg) {
        $('body').append('<div class="msg-success" style="display:none">' + (msg || 'Thành công') + '</div>');
        $('.msg-success').slideDown(100);
        setTimeout(function () {
            $('.msg-success').slideUp(100);
            $('.msg-success').remove();
        }, 1500);
    },
    showSuccessMsg() {
        $('body').append(' <div class="box-msg-success"><div class= "content-msg-success" >Thành công</div></div >');
        $('.box-msg-success').fadeIn(100);
        $('.box-msg-success').fadeOut(2000);
        $('.box-msg-success').remove();
    },

    /* -------------------------------------------------------------------------
     * Buid dữ liệu vào table dựa vào config trên thead (các th phải có fiedData)
     * Created by: NVMANH (03/03/2018)
     */
    buidDataToTable(table, data, setFieldValueCustomCallBack) {
        try {
            var fields = table.find('thead th'),
                keyRow = table.attr('keyRow'),
                tbody = table.find('tbody');
            tbody.empty();
            $.each(data, function (index, item) {
                var rowHTML = $('<tr></tr>');
                $.each(fields, function (i, field) {
                    var fieldName = field.getAttribute('fieldData');
                    var fieldValue = item[fieldName] ? item[fieldName] : "";
                    fieldValue = i > 0 ? fieldValue : '<div style="text-align:center">{0}</div>'.format(index + 1);
                    if (setFieldValueCustomCallBack) {
                        fieldValue = setFieldValueCustomCallBack(fieldName, fieldValue, item);
                    }
                    var cellHTML = $('<td></td>');
                    var typeValue = typeof fieldValue;
                    switch (typeValue) {
                        case "number":
                            cellHTML.append(fieldValue.formatMoney());
                            cellHTML.addClass('text-align-right');
                            break;
                        default:
                            var dataType = field.getAttribute('datatype');
                            // check nếu dữ liệu là ngày tháng:
                            if (new Date(fieldValue) !== 'Invalid Date' && dataType === 'datetime') {
                                fieldValue = new Date(fieldValue);
                                var format = field.getAttribute('Format');
                                if (format && fieldValue[format]) {
                                    fieldValue = fieldValue[format]();
                                }
                            }
                            cellHTML.append(fieldValue);
                            break;
                    }

                    // Xét cột action nếu có:
                    var actions = field.getAttribute("Actions");
                    if (actions) {
                        if (actions.match('delete')) {
                            var iconDeleteHTML = $('<div class="w-100" style="display:grid;justify-items: center;"><div class="cell-delete" title="Xóa"></div></div>');
                            iconDeleteHTML.find('.cell-delete').data('id', item[keyRow]);
                            cellHTML.attr('style', 'vertical-align: middle !important;');
                            cellHTML.append(iconDeleteHTML);
                        }
                    }
                    rowHTML.append(cellHTML);
                    rowHTML.data("id", item[keyRow]);
                    rowHTML.data("dataJson", item);
                });
                tbody.append(rowHTML);
            });
            commonJS.setFirstRowSelected(table);
        } catch (e) {
            console.log(e);
        }
    },
    getFirstRowTableSelected(table) {
        var rowSelected = $(table).find('tbody tr.row-selected').first();

        return rowSelected;
    },

    getFirstItemIdSelectedInTable(table) {
        var rowSelected = $(table).find('tbody tr.row-selected').first();
        var itemId = rowSelected.data("id");
        return itemId;
    },

    /* --------------------------------------------
     * Chuyển các ký tự có dấu tiếng việc sang chữ không dấu
     * Created by: NVMANH (03/03/2018)
     */
    change_alias: function (alias) {
        var str = alias;
        str = str.toLowerCase();
        str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
        str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
        str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
        str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
        str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
        str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
        str = str.replace(/đ/g, "d");
        str = str.replace(/!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\;|\'|\"|\&|\#|\[|\]|~|\$|_|`|-|{|}|\||\\/g, " ");
        str = str.replace(/ + /g, " ");
        str = str.trim();
        return str;
    },

    /* --------------------------------------------
     * Select vào dòng trong table dữ liệu
     * Created by: NVMANH (03/03/2018)
     */
    rowTable_OnClick: function () {
        $(this).siblings('.row-selected').removeClass('row-selected');
        this.classList.add('row-selected');
        $(this).siblings('.rowSelected').removeClass('rowSelected');
        this.classList.add('rowSelected');
    },

    /* --------------------------------------------
     * Select vào dòng đầu tiên trong bảng dữ liệu
     * Created by: NVMANH (03/03/2018)
     */
    setFirstRowSelected: function (table) {
        var tBodys = table[0].tBodies,
            firstRow = null;
        if (tBodys.length > 0) {
            var tBody = tBodys[0],
                rows = tBody.rows;
            firstRow = rows.length > 0 ? rows[0] : null;
        }
        if (firstRow) {
            firstRow.classList.add('rowSelected');
            firstRow.classList.add('row-selected');
        }
    },

    /* -------------------------------------------------------------------------------
     * Hiển thị cảnh báo khi validate dữ liệu trống (các trường yêu cầu bắt buộc nhập)
     * Created by: NVMANH (03/03/2018)
     */
    validateEmpty: function (sender) {
        var target = sender.target,
            idEmpty = target.id + '-empty';
        value = target.value,
            parent = $(this).parent(),
            currentThisWith = $(this).width();

        if (!value || value === '') {
            target.classList.add('validate-error');
            if (parent.find('.divError').length === 0) {
                parent.append('<div id="' + idEmpty + '" class="divError" title="Không được để trống trường này"></div>');
            }
        } else {
            target.classList.remove('validate-error');
            target.title = "";
            $('#' + idEmpty).remove();
        }
    },
    requiredValidation: function (sender, e) {
        if (!$(this).val()) {
            $(this).addClass('required-border');
            $(this).parent().attr('title', "Thông tin này không được để trống");
            $(this).parent().addClass('wrap-control');
            var nextElement = $(this).next();
            if (!$(nextElement).hasClass('box-required-after')) {
                $(this).after('<div class="box-required-after"></div>');
            }
            return false;
        } else {
            $(this).removeClass('required-border');
            $(this).next('.box-required-after').remove();
            $(this).parent().removeAttr('title');
            return true;
        }

    },

    /* -------------------------------------------------------------------------------
     * Chỉ cho phép nhập các ký tự số
     * Created by: NVMANH (03/03/2018)
     */
    isNumberKey: function (evt) {
        var charCode = evt.which ? evt.which : event.keyCode;
        if (charCode === 59 || charCode === 46)
            return true;
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;
        return true;
    },

    /* --------------------------------------------
     * Ẩn hiện các box có thực hiện ẩn hiện khi nhấn click
     * Điều kiện là phải có class .hide-of-out-side
     * Created by: NVMANH (03/03/2018)
     */
    setHideOnClickOutSide: function () {
        var target = event.target;
        if (!$(target).hasClass('hide-if-outside')) {
            $('.hide-if-outside').hide();
        }
    },

    /* --------------------------------------------
     * Đọc nội dung file
     * Created by: NVMANH (03/03/2018)
     */
    readFile: function (file) {
        var rawFile = new XMLHttpRequest();
        rawFile.open("GET", file, false);
        rawFile.onreadystatechange = function () {
            if (rawFile.readyState === 4) {
                if (rawFile.status === 200 || rawFile.status === 0) {
                    var allText = rawFile.responseText;
                    alert(allText);
                }
            }
        };
        rawFile.send(null);
    },

    /* --------------------------------------------
     * Thực hiện validate Email đúng định dạng
     * Created by: NVMANH (03/03/2018)
     */
    validateEmail: function (email) {
        var filter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
        if (filter.test(email)) {
            return true;
        }
        else {
            return false;
        }
    },

    // Hàm kiểm tra trình duyệt đang sử dụng:
    // Createted by: NVMANH (11/04/2019)
    getBrowser: function () {
        var nVer = navigator.appVersion;
        var nAgt = navigator.userAgent;
        var browserName = navigator.appName;
        var fullVersion = '' + parseFloat(navigator.appVersion);
        var majorVersion = parseInt(navigator.appVersion, 10);
        var nameOffset, verOffset, ix;

        // In Opera, the true version is after "Opera" or after "Version"
        if ((verOffset = nAgt.indexOf("Opera")) !== -1) {
            browserName = "Opera";
            fullVersion = nAgt.substring(verOffset + 6);
            if ((verOffset = nAgt.indexOf("Version")) !== -1)
                fullVersion = nAgt.substring(verOffset + 8);
        }
        // In MSIE, the true version is after "MSIE" in userAgent
        else if ((verOffset = nAgt.indexOf("MSIE")) !== -1) {
            browserName = "Microsoft Internet Explorer";
            fullVersion = nAgt.substring(verOffset + 5);
        }
        // In Chrome, the true version is after "Chrome" 
        else if ((verOffset = nAgt.indexOf("Chrome")) !== -1) {
            browserName = "Chrome";
            fullVersion = nAgt.substring(verOffset + 7);
        }
        // In Safari, the true version is after "Safari" or after "Version" 
        else if ((verOffset = nAgt.indexOf("Safari")) !== -1) {
            browserName = "Safari";
            fullVersion = nAgt.substring(verOffset + 7);
            if ((verOffset = nAgt.indexOf("Version")) !== -1)
                fullVersion = nAgt.substring(verOffset + 8);
        }
        // In Firefox, the true version is after "Firefox" 
        else if ((verOffset = nAgt.indexOf("Firefox")) !== -1) {
            browserName = "Firefox";
            fullVersion = nAgt.substring(verOffset + 8);
        }
        // In most other browsers, "name/version" is at the end of userAgent 
        else if ((nameOffset = nAgt.lastIndexOf(' ') + 1) <
            (verOffset = nAgt.lastIndexOf('/'))) {
            browserName = nAgt.substring(nameOffset, verOffset);
            fullVersion = nAgt.substring(verOffset + 1);
            if (browserName.toLowerCase() === browserName.toUpperCase()) {
                browserName = navigator.appName;
            }
        }
        // trim the fullVersion string at semicolon/space if present
        if ((ix = fullVersion.indexOf(";")) !== -1)
            fullVersion = fullVersion.substring(0, ix);
        if ((ix = fullVersion.indexOf(" ")) !== -1)
            fullVersion = fullVersion.substring(0, ix);

        majorVersion = parseInt('' + fullVersion, 10);
        if (isNaN(majorVersion)) {
            fullVersion = '' + parseFloat(navigator.appVersion);
            majorVersion = parseInt(navigator.appVersion, 10);
        }
        var browser = {
            Name: browserName,
            FullVersion: fullVersion,
            MajorVersion: majorVersion,
            NavAppName: navigator.appNam,
            NavUserAgen: navigator.userAgent
        };
        return browser;
    },

    /** ---------------------------------------------------------------------------
     * Set thời gian được tính toán từ 1 khoảng thời gian đến thời điểm hiện tại:
     * @param {any} elForShow element sẽ hiển thị thông tin (d hh:mm:ss)
     * @param {any} timeStart thời gian bắt đầu tính toán
     * @param {any} timeLoop  thời gian sẽ lặp lại việc tính toán (có thể không truyền)
     * @param {any} callback  phương thức callback sau khi thực hiện tính toán được
     * Author: NVMANH (19/07/2019)
     */
    setCalculatorTime(elForShow, timeStart, timeLoop, callback) {
        // Set the date we're counting down to
        var countDownDate = timeStart;//new Date("Wed Jun 19 2019 16:58:17").getTime();
        if (timeLoop) {
            // Update the count down every 1 second
            var x = window.setInterval(function () {

                // Get today's date and time
                var now = new Date().getTime();

                // Find the distance between now and the count down date
                var distance = now - timeStart;

                // Time calculations for days, hours, minutes and seconds
                var timeInfo = commonJS.getTimeDistance(timeStart);
                var days = timeInfo.Days;
                var hours = timeInfo.Hours;
                var minutes = timeInfo.Minutes;
                var seconds = timeInfo.Seconds;
                var hoursText = hours < 10 ? "0" + hours : hours;
                var minutesText = minutes < 10 ? "0" + minutes : minutes;
                var dayText = days > 0 ? days + "d " : "";
                seconds = seconds < 10 ? "0" + seconds : seconds;

                // Display the result in the element with id="demo"
                var element = $(elForShow)[0];
                element.innerHTML = dayText + hoursText + ":" + minutesText + ":" + seconds;
                if (callback) {
                    callback(elForShow, days, hours, minutes, seconds, x);
                }
            }, timeLoop);
            commonJS.Interval.push(x);
        }
    },

    /** ---------------------------------------------------------------------------
     * Hàm thực hình tính toán khoảng cách giữa 2 khoảng thời gian
     * @param {any} startTimne Thời gian bắt đầu
     * @param {any} endTime Thời gian kết thúc (Nếu không truyền tham số vào thì sẽ lấy ngày hiện tại)
     * Author: NVMANH (19/07/2019)
     */
    getTimeDistance(startTimne, endTime) {
        // Find the distance between now and the count down date
        if (!endTime) {
            endTime = new Date().getTime();
        }

        var distance = endTime - startTimne;

        // Time calculations for days, hours, minutes and seconds
        var days = Math.floor(distance / (1000 * 60 * 60 * 24));
        var hours = Math.floor(distance % (1000 * 60 * 60 * 24) / (1000 * 60 * 60));
        var minutes = Math.floor(distance % (1000 * 60 * 60) / (1000 * 60));
        var seconds = Math.floor(distance % (1000 * 60) / 1000);

        return {
            Days: days,
            Hours: hours,
            Minutes: minutes,
            Seconds: seconds
        }
    },

    /**
     * Lấy khoảng thời gian theo ngày (từ đầu tới cuối ngày)
     * @param {date} date (Ngày)
     * @returns {object} Đối tượng bao gồm: Ngày bắt đầu và ngày kết thúc.
     * Author: NVMANH (02/08/2019)
     */
    getRangeDateTimeInDay(date) {
        var startDateTime = date.setHours(0, 0, 0, 0);
        var endDateTime = date.setHours(23, 59, 59, 999);
        return {
            StartDateTime: new Date(startDateTime),
            EndDateTime: new Date(endDateTime)
        };
    },

    /**
     * Lấy khoảng thời gian theo tuần của một ngày bất kỳ (từ đầu tới cuối tuần)
     * @param {any} date (Ngày)
     * @returns {object} Đối tượng bao gồm: Ngày bắt đầu và ngày kết thúc.
     * Author: NVMANH (02/08/2019)
     */
    getRangeDateTimeWeekOfDate(date) {
        // Lấy ngày đầu tuần (thứ 2):
        var first = date.getDate() - date.getDay() + 1; // First day is the day of the month - the day of the week

        var monday = new Date(date.setDate(first));
        var sunday = monday.addDays(6);

        return {
            StartDateTime: new Date(monday.setHours(0, 0, 0, 0)),
            EndDateTime: new Date(sunday.setHours(23, 59, 59, 999))
        };
    },

    /**
     * Lấy khoảng thời gian theo tuần của một ngày bất kỳ (từ đầu tới cuối tuần)
     * @param {any} date (Ngày)
     * @returns {object} Đối tượng bao gồm: Ngày bắt đầu và ngày kết thúc.
     * Author: NVMANH (02/08/2019)
     */
    getRangeDateTimeMonthOfDate(date) {
        var startDateTime = new Date(date.setDate(1));
        var endDateTime = new Date(date.getFullYear(), date.getMonth() + 1, 0);
        return {
            StartDateTime: new Date(startDateTime.setHours(0, 0, 0, 0)),
            EndDateTime: new Date(endDateTime.setHours(23, 59, 59, 999))
        };
    },

    /**
     * Lấy khoảng thời gian theo tuần của một ngày bất kỳ (từ đầu tới cuối tuần)
     * @param {any} date (Ngày)
     * @returns {object} Đối tượng bao gồm: Ngày bắt đầu và ngày kết thúc.
     * Author: NVMANH (02/08/2019)
     */
    getRangeDateTimeQuarterOfDate(date) {
        var thisQuater = Math.ceil((date.getMonth() + 1) / 3);
        var startMonth = thisQuater * 3 - 3;
        var endMonth = thisQuater * 3 - 1;
        var startDateTime = new Date(date.getFullYear(), startMonth, 1);
        var endDateTime = new Date(date.getFullYear(), endMonth + 1, 0);
        return {
            StartDateTime: new Date(startDateTime.setHours(0, 0, 0, 0)),
            EndDateTime: new Date(endDateTime.setHours(23, 59, 59, 999))
        };
    },
    /**
    * Lấy khoảng thời gian theo tuần của một ngày bất kỳ (từ đầu tới cuối tuần)
    * @param {any} date (Ngày)
    * @returns {object} Đối tượng bao gồm: Ngày bắt đầu và ngày kết thúc.
    * Author: NVMANH (02/08/2019)
    */
    getRangeDateTimeYearOfDate(date) {
        var currentYear = date.getFullYear();
        var startDateTime = new Date(currentYear, 0, 1);
        var endDateTime = new Date(currentYear, 12, 0);
        return {
            StartDateTime: new Date(startDateTime.setHours(0, 0, 0, 0)),
            EndDateTime: new Date(endDateTime.setHours(23, 59, 59, 999))
        };
    }
};
var initCommon = {
    intMessageBoxClass: function () {

    }
};


jQuery.fn.removeAllAttributes = function () {
    return this.each(function () {
        var attributes = $.map(this.attributes, function (item) {
            return item.name;
        });
        var img = $(this);
        $.each(attributes, function (i, item) {
            img.removeAttr(item);
        });
    });
};

/* --------------------------------------------
 * Phương thức định dạng string
 * Created by: NVMANH (03/03/2018)
 */
if (!String.prototype.format) {
    String.prototype.format = function () {
        var args = arguments;
        return this.replace(/{(\d+)}/g, function (match, number) {
            return typeof args[number] !== 'undefined'
                ? args[number]
                : match
                ;
        });
    };
}

if (!String.prototype.replaceAll) {
    String.prototype.replaceAll = function (search, replacement) {
        var target = this;
        return target.split(search).join(replacement);
    };
}


/* Định dạng hiển thị tiền tệ */
if (!Number.prototype.formatMoney) {
    Number.prototype.formatMoney = function () {
        return this.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,");
    };
}

/* So sánh 2 object */
if (!Object.compare) {
    Object.compare = function (obj1, obj2) {
        var isDifferent = false;
        for (var property in obj1) {
            if (obj1[property] !== obj2[property]) {
                isDifferent = true;
                break;
            }
        }
        return isDifferent;
    };
}
/* Định dạng ngày tháng năm */
if (!Date.prototype.ddmmyyyy) {
    Date.prototype.ddmmyyyy = function () {
        var mm = this.getMonth() + 1; // getMonth() is zero-based
        var dd = this.getDate();

        return [(dd > 9 ? '' : '0') + dd + '/',
        (mm > 9 ? '' : '0') + mm + '/',
        this.getFullYear()
        ].join('');
    };
}

/* Lấy giờ hiện tại theo định dạng hh:mm:ss */
if (!Date.prototype.time) {
    Date.prototype.time = function () {
        var hh = this.getHours(), // getMonth() is zero-based
            mm = this.getMinutes(),
            ss = this.getSeconds();

        return [(hh > 9 ? '' : '0') + hh + ':',
        (mm > 9 ? '' : '0') + mm + ':',
        (ss > 9 ? '' : '0') + ss
        ].join('');
    };
}

/* Lấy giờ hiện tại*/
if (!Date.prototype.hhmm) {
    Date.prototype.hhmm = function () {
        var hh = this.getHours(), // getMonth() is zero-based
            mm = this.getMinutes();

        return [(hh > 9 ? '' : '0') + hh + ':',
        (mm > 9 ? '' : '0') + mm
        ].join('');
    };
}
if (!Date.prototype.addDays) {
    Date.prototype.addDays = function (days) {
        var date = new Date(this.valueOf());
        date.setDate(date.getDate() + days);
        return date;
    };
}

/* Lấy giờ hiện tại*/
if (!Date.prototype.hhmmss) {
    Date.prototype.hhmmss = function () {
        var hh = this.getHours(), // getMonth() is zero-based
            mm = this.getMinutes(),
            ss = this.getSeconds();
        ss = ss < 10 ? '0' + ss : ss;

        return [(hh > 9 ? '' : '0') + hh + ':',
        (mm > 9 ? '' : '0') + mm + ':' + ss
        ].join('');
    };
}

if (!Date.prototype.formatDateStringInvariantCulture) {
    Date.prototype.formatDateStringInvariantCulture = function () {

        var day = this.getDate();       // yields date
        var month = this.getMonth() + 1;    // yields month (add one as '.getMonth()' is zero indexed)
        var year = this.getFullYear();  // yields year
        var hour = this.getHours();     // yields hours 
        var minute = this.getMinutes(); // yields minutes
        var second = this.getSeconds(); // yields seconds

        day = (day > 9 ? '' : '0') + day;
        month = (month > 9 ? '' : '0') + month;
        hour = (hour > 9 ? '' : '0') + hour;
        minute = (minute > 9 ? '' : '0') + minute;
        second = (second > 9 ? '' : '0') + second;

        // After this construct a string with the above results as below
        return time = day + "/" + month + "/" + year + " " + hour + ':' + minute + ':' + second;
    };
}

// Lấy tên Property chính xác (phân biệt hoa thường)
// CreatedBy: NVMANH (12/08/2020)
if (!Object.GetBasicPropertyName) {
    Object.GetBasicPropertyName = function (obj,propertyName) {
        for (var prop in obj) {
            // force property name and value to lower case for comparison
            if (prop.toLowerCase() === propertyName.toLowerCase()) {
                duplicateFlag = true;
                return prop;
                break;
            }   // No else branch needed because duplicate starts off false
        }
        return null;
    }
}

// jqueryui defaults
$.extend($.ui.dialog.prototype.options, {
    create: function () {
        var $this = $(this);

        // focus first button and bind enter to it
        var buttons = $this.parent().find('.ui-dialog-buttonpane button');
        $this.parent().find('.ui-dialog-buttonpane button:first').focus();
        $this.keypress(function (e) {
            if (e.keyCode === $.ui.keyCode.ENTER) {
                $this.parent().find('.ui-dialog-buttonpane button:first').click();
                return false;
            }
        });
    }
});

