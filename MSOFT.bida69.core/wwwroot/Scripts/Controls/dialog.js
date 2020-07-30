class Button {
    constructor(text, clickCallBack, scope, cls, type, form, iconcls) {
        iconcls = iconcls ? ' <i class="{0}" ></i>'.format(iconcls) : "";
        this.class = "btn " + (cls ? cls : "");
        this.type = (type ? type : "submit");
        this.form = (form ? form : "");
        this.html = '{0} {1}'.format(iconcls, text);
        this.click = clickCallBack ? clickCallBack : this.btnOnClick;
    }
    buidButton() {

    }
    btnOnClick() {
        console.log("please set event for this button");
    }
}

/* --------------------------------------------
 * Description: Class khởi tạo các Dilog
 * Created by: NVMANH (18/06/2019)
 * */
class Dialog {
    constructor(el, scope, buttons, beforeOpenCallBack) {
        var me = this;
        this.Dialog = $(el).dialog({
            width: 'auto',
            minWidth: 370,
            //minHeight: 400,
            autoOpen: false,
            fluid: true,
            resizable: false,
            position: ({ my: "center", at: "center", of: window }),
            modal: true,
            dialogClass: "mnv-dialog",
            buttons: [],//(buttons ? buttons : this.getButtonsDefault(scope)),
            open: function () {
                // Căn giữa Dialog sẽ hiển thị:
                $(this).dialog('widget').position({ my: "center", at: "center", of: window });
                var formHeight = $(this).attr('form-height');
                if (formHeight) {
                    $(this).height(formHeight);
                }
                // Hàm thực hiện khi form được mở:
                if (beforeOpenCallBack) {
                    beforeOpenCallBack(event);
                }
            },
            close: function () {
                if (me.Interval) {
                    clearInterval(me.Interval);
                }
                if (me.beforeClose) {
                    me.beforeClose();
                }
                me.Dialog.dialog('close');
            }
           //beforeOpenCallBack ? beforeOpenCallBack:(scope && scope["initDialogBeforeOpen"]) ? scope["initDialogBeforeOpen"] : this.initDialogBeforeOpen,
        });
    }

    initDialogBeforeOpen() {
        alert('initDialogBeforeOpen');
    }
    show() {
        this.Dialog.dialog("open");
    }
    close() {
        this.Dialog.dialog("close");
    }
    getButtonsDefault(scope) {
        var buttons = [
            {
                class: "btn btn-primary",
                type: "submit",
                form: "frmSubmit",
                //text:"Cất",
                html: '<i class="fa fa-check"></i> Cất',
                id: "btnSave",
                click: scope ? (scope["save"] ? scope["save"].bind(scope) : this.save.bind(this)) : this.save,
            },
            {
                html: '<i class="fa fa-close"></i> Huỷ bỏ',
                class: "btn btn-danger btn-cancel",
                id: "btnCancel",
                click: this.cancel.bind(this),
            }
        ];
        return buttons;
    }
    save() {
        alert("save Dialog");
    }

    cancel() {
        this.close();
    }
}