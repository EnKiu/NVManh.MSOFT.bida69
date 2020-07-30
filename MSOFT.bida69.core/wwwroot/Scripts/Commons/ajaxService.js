
class AjaxJSON {
    constructor() {
        this.Ajax = {};
    }
    init(method, url, param, async, callback) {
        this.Ajax.method = method;
        this.Ajax.url = url;
        this.Ajax.data = JSON.stringify(param);
        this.Ajax.contentType = 'application/json';
        this.Ajax.async = async === false ? false : true;
        this.Ajax.headers = AjaxJSON.setRequestHeader();
        this.Ajax.beforeSend = function () {
            commonJS.showMask();
        };
        this.Ajax.complete = function (res) {
            commonJS.hideMask();
            if (method !== "GET" && res.status < 400 && res.responseJSON.Success) {
                commonJS.showSuccessMsg2();
            }
        };
        this.Ajax.callback = callback;

        //if (param) {
        //    this.Ajax.data = param;
        //}
    }

   /**
    * Thực hiện thêm mới các thuộc tính vào Request header
    * Author: NVMANH (04/06/2019)
    * @returns {object} The sum of the two numbers.
    * */
    static setRequestHeader() {
        var tokenKey = 'accessToken';
        var token = sessionStorage.getItem(tokenKey);
        var headers = { 'X-Requested-With': 'XMLHttpRequest', 'Access-Control-Allow-Methods': 'GET,PUT,POST,DELETE,PATCH,OPTIONS' };
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }
        return headers;
    }

    get(url, param, async, callback) {
        var me = this;
        me.init("GET", url, param, async, callback);
        //(async () => {
        $.ajax(me.Ajax)
            .done(function (res) {
                me.processSuccessRequest(res, callback);
            })
            .fail(me.processFailRequest.bind(me));
    }

    post(url, param, async, callback) {
        try {
            //commonJS.showMask();
            var me = this;
            me.init("POST", url, param, async, callback);

            $.ajax(me.Ajax)
                .done(function (res) {
                    me.processSuccessRequest(res, callback);
                })
                .fail(me.processFailRequest.bind(me));
        } catch (e) {
            console.log(e);
        } finally {
            commonJS.hideMask();
        }

    }
    put(url, param, async, callback) {
        try {
            //commonJS.showMask();
            var me = this;
            me.init("PUT", url, param, async, callback);
            $.ajax(me.Ajax)
                .done(function (res) {
                    me.processSuccessRequest(res, callback);
                })
                .fail(me.processFailRequest.bind(me));
        } catch (e) {
            console.log(e);
        } finally {
            commonJS.hideMask();
        }

    }

    patch(url, param, async, callback) {
        try {
            //commonJS.showMask();
            var me = this;
            me.init("PATCH", url, param, async, callback);
            $.ajax(me.Ajax)
                .done(function (res) {
                    me.processSuccessRequest(res, callback);
                })
                .fail(me.processFailRequest.bind(me));
        } catch (e) {
            console.log(e);
        } finally {
            commonJS.hideMask();
        }

    }

    delete(url, param, async, callback) {
        try {
            //commonJS.showMask();
            var me = this;
            me.init("DELETE", url, param, async, callback);
            $.ajax(me.Ajax)
                .done(function (res) {
                    me.processSuccessRequest(res, callback);
                })
                .fail(me.processFailRequest.bind(me));
        } catch (e) {
            console.log(e);
        } finally {
            commonJS.hideMask();
        }

    }

    /**
     * Hàm xử lý khi Ajax được xử lý thành công
     * Created By: ManhNV (07/07/2019)
     *  @param {object} res the object response from server.
     *  @param {Function} callback The first number
     *  Author: NVMANH (11/07/2019)
     * */
    processSuccessRequest(res, callback) {
        if (res.Success) {
            callback(res.Data);
        } else {
            console.log(res);
            alert(res.Data.Message);
        }
    }

    /**
     * Hàm xử lý khi Ajax được xử lý thất bại
     *  @param {object} res the object response from server
     *  Author: NVMANH (11/07/2019)
     * Created By: ManhNV (07/07/2019)
     * */
    processFailRequest(res) {
        console.log(res);
        var me = this;
        if (res.status === 401) {
            me.processNotAuthenication();
        }
    }
    /**
     * Hàm xử lý nếu người dùng không có quyền truy cập
     * Created By: ManhNV (07/07/2019)
     * */
    processNotAuthenication() {
        //$('body').html("Bạn không có quyền truy cập trang này");
        var returnPath = encodeURIComponent(window.location.pathname);
        window.location.replace("/Admin/Login?returnUrl=" + returnPath);
        $('.loading').removeClass('loading');
    }
}
ajaxJSON = new AjaxJSON();