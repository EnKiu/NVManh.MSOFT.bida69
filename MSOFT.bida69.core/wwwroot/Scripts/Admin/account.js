$(document).ready(function () {
    $(document).on('click', '.btn-logout', Account.Logout);
})
class Account {
    constructor() {
        this.UserName = $("").val();
        this.Password = $("").val();
        this.TokenKey = "accessToken";
        this.InitEvents();
    }
    InitEvents() {
        $('form#frmLogin').on('submit', this.Login.bind(this));
        $('form#frmRegister').on('submit', this.Register.bind(this));
        $('#login-form').on('click', '#linkRegister', this.ShowRegisterForm.bind(this));
        $('#register-form').on('click', '#linkLogin', this.ShowLoginForm.bind(this));
        $('#navUserInfo').on('click', '#navLogout', Account.bind(this));
        $('#btnSubmitRegister').on('click', this.Register);
        this.SetCustomValidity();
    }
    /**
     * Tùy chỉnh lại việc validate dữ liệu của Bootstrap
     * @param {any} sender
     */
    SetCustomValidity(sender) {
        var msg = "";
        var elements = document.getElementsByTagName("input");

        for (var i = 0; i < elements.length; i++) {
            elements[i].oninvalid = function (e) {
                if (!e.target.validity.valid) {
                    switch (e.target.name) {
                        case 'username':
                            e.target.setCustomValidity("Tài khoản không được để trống");
                            break;
                        case 'password':
                            e.target.setCustomValidity("Mật khẩu không được để trống");
                            break;
                        default:
                            e.target.setCustomValidity("");
                            break;

                    }
                }
            };
            elements[i].oninput = function (e) {
                e.target.setCustomValidity(msg);
            };
        }
    }

    /**
     * Kiểm tra lại thông tin người dùng
     * */
    CheckAuth() {
        var partUrl = window.location.pathname;
        if (partUrl !== "/Account/Login") {
            var headers = ajaxJSON.setRequestHeader();
            $.ajax({
                url: "/api/Account/UserInfo",
                dataType: 'json',
                async: false,
                headers: headers
            }).done(function (res) {
                $('#lblUserName').text(res["Email"]);
                $('#navLogin').hide();
                $('#navUserInfo').show();
            }).fail(function (res) {
                $('#navLogin').show();
                $('#navUserInfo').hide();
            });
        }
    }

    /**
     * Thực hiện đăng nhập hệ thống
     * @param {any} sender
     * @param {any} e
     */
    Login(sender, e) {
        var self = this;
        sender.preventDefault();
        var loginData = {
            grant_type: 'password',
            username: $('#login-form input[name="username"]').val(),//self.loginEmail(),
            password: $('#login-form input[name="password"]').val()//self.loginPassword()
        };
        commonJS.showLoadingBody();
        $.ajax({
            type: 'POST',
            url: '/users/authenticate',
            async: true,
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(loginData)
        }).done(function (res) {
            // Cache the access token in session storage.
            sessionStorage.setItem(self.TokenKey, res.Token);
            var returnUrl = decodeURIComponent(window.location.search);
            returnUrl = returnUrl.replace("?returnUrl=", "");
            window.location.href = window.location.origin + returnUrl;
            $('#login-form .showErrorValid').hide();
            commonJS.hideMask($('body'));
        }).fail(function (res) {
            $('#login-form .showErrorValid').html('');
            if (res.status === 400) {
                $('#login-form .showErrorValid').append(res.responseJSON['message']);
                $('#login-form .showErrorValid').show();
            }
            showError(res);
            commonJS.hideLoadingBody();
        });
    }

    /**
     * Đăng xuất khỏi hệ thống
     * */
    static Logout() {
        commonJS.showMask($('body'));
        var self = this;
        sessionStorage.removeItem(self.TokenKey);
        window.location.replace("/Admin/Login");
        return;
        //ajaxJSON.post('/api/Account/Logout', {}, true, function (data) {
        //    // Successfully logged out. Delete the token.
        //    sessionStorage.removeItem(self.TokenKey);
        //    window.location.replace("/Admin/Login");
        //});
        commonJS.showMask($('body'));
        $.ajax({
            type: 'PUT',
            url: '/users/logout',
            async: true,
            contentType: 'application/json; charset=utf-8',
            data: {},
            headers: AjaxJSON.setRequestHeader(),
        }).done(function (res) {
            // Successfully logged out. Delete the token.
            sessionStorage.removeItem(self.TokenKey);
            window.location.replace("/Admin/Login");
            commonJS.hideMask($('body'));
        }).fail(function (res) {
            showError(res);
            commonJS.hideMask();
        });
    }

    /**Hiển thị form đăng nhập */
    ShowLoginForm() {
        $('#login-form .showErrorValid').html('');
        $('#register-form .showErrorValid').hide();
        $('#login-form').show();
        $('#register-form').hide();
        $('#login-form input[name="username"]').focus();
    }

    /**Hiển thị form đăng ký */
    ShowRegisterForm() {
        $('#register-form input').val(null);
        $('#login-form .showErrorValid').html('');
        $('#register-form .showErrorValid').hide();
        $('#login-form').hide();
        $('#register-form').show();
        $('#register-form input[name="username"]').focus();
    }

    /**
     * Thực hiện Đăng ký tài khoản mới
     * @param {any} sender
     */
    Register(sender) {
        //alert('Hiện tại tính năng này đang tạm khóa, vui lòng liên hệ admin để được trợ giúp! (Mạnh msoft.vn)');
        //return;
        sender.preventDefault();
        var data = {
            UserName: $('#register-form input[name="username"]').val(),
            //PhoneNumber: $('#register-form input[name="phonenumber"]').val(),
            //Email: $('#register-form input[name="username"]').val(),
            Password: $('#register-form input[name="password"]').val(),
            ConfirmPassword: $('#register-form input[name="confirmpassword"]').val()
        };
        commonJS.showMask($('body'));
        $.ajax({
            type: 'POST',
            url: '/users/register',
            contentType: 'application/json; charset=utf-8',
            async: true,
            data: JSON.stringify(data)
        }).done(function (res) {
            $('.showErrorValid').hide();
            $('#login-form input[name="username"]').val(data.Email);
            $('#login-form input[name="password"]').val(data.Password);
            $('#login-form').show();
            $('#register-form').hide();
            commonJS.hideMask($('body'));
        }).fail(function (res) {
            showError(res);
            commonJS.hideMask($('body'));
        });
    }
}
accountJS = new Account();

function showError(jqXHR) {
    $('.showErrorValid ul').html('');
    var response = jqXHR.responseJSON;
    if (response) {
        if (response.errors) {
            var errors = response.errors;
            debugger;
            for (var prop in errors) {
                if (errors.hasOwnProperty(prop)) {
                    var msgArr = errors[prop]; // expect array here
                    if (msgArr.length) {
                        for (var i = 0; i < msgArr.length; ++i) {
                            $('.showErrorValid ul').append('<li>' + prop + ': ' + msgArr[i] + '</li>');
                        }
                    }
                }
            }
        }
        $('#register-form .showErrorValid').show();
    }
}