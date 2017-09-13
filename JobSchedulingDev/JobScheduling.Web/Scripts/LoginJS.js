MSLogin = {};
MSLogin.ErrorCodes = {
    IDP_ERROR: 2147776681,
    IDP_ERROR2: 2147776682,
    NAMESPACE_ERROR: 2147778860,
    PARSE_ERROR: 2147762276
};

var ErrorCodes = MSLogin.ErrorCodes;
MSLogin.Constants = {
    REDIRECT_COUNTDOWN_DEFAULT: 3,
    FEDERATION_QUERY_PARAMETERS: "",
    CDN_IMAGE_PATH: "images/",
    CDN_MOBILE_CSS_PATH: "",
    LATENCY_THRESHOLD: 2e3,
    HIP_DATA_URL: "",
    PREFETCH_URL: "",
    LATENCY_SENSITIVITY_DEFAULT: 1,
    OTHER_ACCOUNT_TEXT: "",
    MAX_TILE_TEXT_LENGTH: 25,
    PREFILL_MEMBER_NAME: "",
    MEMBER_NAME: "",
    DEFAULT_FOOTER_LINKS: {},
    DEFAULT_ENABLED_FOOTER_LINKS: ["legal", "privacy", "feedback"],
    FOOTER_LINKS: {},
    REDIRECT_MESSAGES: {},
    HAS_SUBMITTED: false,
    METRICS_MODE: 1,
    SUBMIT_METRICS_ON_REDIRECT: false,
    SUBMIT_METRICS_ON_POST: false,
    MOBILE_WIDTH_THRESHOLD: 600,
    MOBILE_HEIGHT_THRESHOLD: 600,
    PARTNER_NAME: "",
    MSA_LABEL: "",
    FEATURE_SLOT_MASK: 0,
    FEATURE_SLOT_THRESHOLD: 2147483647,
    SAVED_USER_COOKIE: "MSSavedUser",
    PREFILL_USER_COOKIE: "MSPPre",
    SAVED_USER_COOKIE_INFO_DELIMITER: "$",
    SAVED_USER_COOKIE_USER_DELIMITER: "|",
    ANCHORED_FOOTER_LINKS: false,
    IE_MOBILE_CSS_APPLIED: false,
    OPTIN_FEATURE_EXPERIMENTS: null,
    MAX_USER_TILES: 1,
    MSA_ENABLED: false,
    DIR: "ltr",
    TWOFA_POLLING_INTERVAL: null,
    TWOFA_MAX_POLLS: null,
    TWOFA_REDIRECT_URL: null,
    TWOFA_PROOFUP_REDIRECT_URL: null,
    TWOFA_MARKET: null,
    TWOFA_MAXMETHODS: 20,
    TWOFA_OTP_MAX_LEN: 6,
    TWOFA_TIMEOUT: 1e4,
    TWOFA_ANIMATION_INTERVAL: 3e3,
    EMAIL_DISCOVERY_SERVICE_TIMEOUT: 3e3,
    EMAIL_DISCOVERY_SERVICE_URI: null,
    EMAIL_DISCOVERY_DEFAULT_TILES: null,
    MSA_AUTH_URL: null,
    TENANT_BRANDING: null,
    LCID: null,
    MSA_ACCOUNT_FOR_TEXT: "",
    AAD_ACCOUNT_FOR_TEXT: "",
    HIP_IMAGE_ALT_TEXT: null,
    DEFAULT_ILLUSTRATION: "",
    DEFAULT_LOGO: "",
    DEFAULT_LOGO_ALT: "",
    DEFAULT_BACKGROUND_COLOR: "#333333",
    GENERIC_ERROR_CODES: [ErrorCodes.IDP_ERROR, ErrorCodes.IDP_ERROR2, ErrorCodes.NAMESPACE_ERROR, ErrorCodes.PARSE_ERROR]
};
MSLogin.Constants.State = {
    NONE: 0,
    FEDERATED: 1,
    MANAGED: 2,
    INVALID: 3,
    PENDING: 4
};
MSLogin.Constants.MSAccount = {
    EXISTS: 0,
    NOT_EXIST: 1,
    THROTTLED: 2
};
MSLogin.Constants.TokenizedStringMsgs = {
    GENERIC_ERROR: "",
    UPN_DISAMBIGUATE_MESSAGE: ""
};
MSLogin.Constants.NameSpaceType = {
    UNKNOWN: "Unknown",
    FEDERATED: "Federated",
    MANAGED: "Managed"
};
MSLogin.Constants.EmailDiscoveryAccountState = {
    AAD_UNKNOWN_AND_MSA_NOT_EXIST: 0,
    AAD_UNKNOWN_AND_MSA_EXISTS: 1,
    AAD_UNKNOWN_AND_MSA_THROTTLED: 2,
    AAD_UNKNOWN_AND_MSA_TIMED_OUT: 3,
    AAD_FEDERATED_AND_MSA_NOT_EXIST: 4,
    AAD_FEDERATED_AND_MSA_EXISTS: 5,
    AAD_FEDERATED_AND_MSA_THROTTLED: 6,
    AAD_FEDERATED_AND_MSA_TIMED_OUT: 7,
    AAD_MANAGED_AND_MSA_NOT_EXIST: 8,
    AAD_MANAGED_AND_MSA_EXISTS: 9,
    AAD_MANAGED_AND_MSA_THROTTLED: 10,
    AAD_MANAGED_AND_MSA_TIMED_OUT: 11,
    AAD_TIMED_OUT_AND_MSA_NOT_EXIST: 12,
    AAD_TIMED_OUT_AND_MSA_EXISTS: 13,
    AAD_TIMED_OUT_AND_MSA_THROTTLED: 14,
    AAD_TIMED_OUT_AND_MSA_TIMED_OUT: 15,
    ERROR: 16
};
MSLogin.Constants.CancelAction = {
    FROM_REDIRECT_TO_LOGIN: 0,
    FROM_SHOW_USER_TO_TILES: 1,
    FROM_MANY_TO_EMAILDISCOVERY_INIT: 2,
    FROM_EMAILDISCOVERY_START_TO_TILES: 3,
    FROM_EMAILDISCOVERY_SPLITTER_TO_EMAILDISCOVERY_START: 4,
    FROM_EMAILDISCOVERY_AAD_LOGIN_TO_EMAILDISCOVERY_START: 5,
    FROM_EMAILDISCOVERY_AAD_LOGIN_TO_EMAILDISCOVERY_SPLITTER: 6,
    FROM_EMAILDISCOVERY_LOOKING_FOR_ACCOUNT_TO_EMAILDISCOVERY_START: 7,
    FROM_EMAILDISCOVERY_REDIRECT_TO_EMAILDISCOVERY_START: 8,
    FROM_EMAILDISCOVERY_REDIRECT_TO_EMAILDISCOVERY_SPLITTER: 9,
    FROM_EMAILDISCOVERY_SPLITTER_FALLBACK_TO_TILES: 10
};
MSLogin.BackActionStack = function () {
    this.BackActionStack = []
};
MSLogin.BackActionStack.prototype.AddAction = function (a) {
    this.BackActionStack.push(a)
};
MSLogin.BackActionStack.prototype.TriggerCancelAction = function () {
    var a = this,
	b = a.BackActionStack.pop();
    alert(a);
    switch (b) {
        case Constants.CancelAction.FROM_REDIRECT_TO_LOGIN:
            User.CancelRedirect();
            break;
        case Constants.CancelAction.FROM_SHOW_USER_TO_TILES:
            Tiles.CancelTileRedirect();
            break;
        case Constants.CancelAction.FROM_MANY_TO_EMAILDISCOVERY_INIT:
            EmailDiscovery.Init();
            break;
        case Constants.CancelAction.FROM_EMAILDISCOVERY_START_TO_TILES:
            EmailDiscovery.BackToUserTiles();
            break;
        case Constants.CancelAction.FROM_EMAILDISCOVERY_SPLITTER_TO_EMAILDISCOVERY_START:
            EmailDiscovery.HideSplitter();
            EmailDiscovery.ShowEmailDiscovery();
            break;
        case Constants.CancelAction.FROM_EMAILDISCOVERY_AAD_LOGIN_TO_EMAILDISCOVERY_START:
            EmailDiscovery.HideAADLoginLayout();
            EmailDiscovery.ShowEmailDiscovery();
            break;
        case Constants.CancelAction.FROM_EMAILDISCOVERY_AAD_LOGIN_TO_EMAILDISCOVERY_SPLITTER:
            EmailDiscovery.HideAADLoginLayout();
            if (Context.email_discovery_use_msa_api) a.TriggerCancelAction();
            else EmailDiscovery.ShowSplitter();
            break;
        case Constants.CancelAction.FROM_EMAILDISCOVERY_LOOKING_FOR_ACCOUNT_TO_EMAILDISCOVERY_START:
            EmailDiscovery.CancelLookingForAccount();
            break;
        case Constants.CancelAction.FROM_EMAILDISCOVERY_REDIRECT_TO_EMAILDISCOVERY_START:
            EmailDiscovery.CancelRedirect();
            EmailDiscovery.ShowEmailDiscovery();
            break;
        case Constants.CancelAction.FROM_EMAILDISCOVERY_REDIRECT_TO_EMAILDISCOVERY_SPLITTER:
            EmailDiscovery.CancelRedirect();
            if (Context.email_discovery_use_msa_api) a.TriggerCancelAction();
            else EmailDiscovery.ShowSplitter();
            break;
        case Constants.CancelAction.FROM_EMAILDISCOVERY_SPLITTER_FALLBACK_TO_TILES:
            EmailDiscovery.HideSplitter();
            a.TriggerCancelAction()
    }
};
MSLogin.BackActionStack.prototype.RemoveLastCancelAction = function () {
    this.BackActionStack.pop()
};
MSLogin.BackActionStack.prototype.DoesCancelActionExist = function () {
    return this.BackActionStack.length > 0
};
MSLogin.BackActionStack.prototype.ClearCancelActions = function () {
    this.BackActionStack = []
};
var Constants = MSLogin.Constants;
MSLogin.Context = {
    whr: "",
    use_instrumentation: false,
    query_search_params: document.location.search,
    federated_domain: "",
    redirect_countdown: Constants.REDIRECT_COUNTDOWN_DEFAULT,
    redirect_auth_url: "",
    redirect_target: "",
    animationTid: null,
    redirectTid: null,
    metrics_on_redirect_submitted: false,
    cobranding_image_visible: false,
    prefetch_initiated: false,
    user_tiles: null,
    email_discovery_lookup_xhr: null,
    email_discovery_mode: false,
    email_discovery_tiles_mode: false,
    email_discovery_response: null,
    email_discovery_timeout_occurred: false,
    email_discovery_splitter_shown: false,
    email_discovery_workflow_state: 0,
    email_discovery_easi_user: false,
    email_discovery_use_msa_api: true,
    cred_height: -1,
    has_forced_cred_block_height: false,
    original_orientation: window.orientation,
    back_action_stack: new MSLogin.BackActionStack,
    tenant_branding_tid: null,
    tenant_branding_retry_count: 2e3,
    tenant_branding_polling_interval: 20
};
MSLogin.Context.username_state = {
    is_empty: true,
    is_partial: false,
    is_timeout: false,
    home_realm_state: 0,
    disable_password: true,
    enable_guests: true,
    enable_redirect: false,
    enable_progress_bar: false,
    last_checked_email: ""
};
var Context = MSLogin.Context;
MSLogin.Background = {
    delayed_background_fetch_url: "",
    background_image_ratio: 0,
    background_image_loaded: false,
    background_image_wait: null,
    winW: -1,
    winH: 460,
    alt_image_path: "",
    alt_background_path: "",
    background_resized: false
};
var Background = MSLogin.Background;
MSLogin.Instrument = {
    latency_sensitivity: Constants.LATENCY_SENSITIVITY_DEFAULT,
    home_realm_start: null,
    home_realm_load_time: 0,
    prefetch_done: false,
    prefetch_start: null,
    prefetch_load_time: 0,
    background_image_loaded: false,
    background_image_start: null,
    background_image_load_time: 0,
    logo_image_loaded: false,
    logo_image_start: null,
    logo_image_load_time: 0,
    email_discovery_ui_code: -1,
    email_discovery_splitter_choice: -1,
    possible_easi_user: 0,
    tenant_branding_start_time: (new Date).getTime(),
    tenant_branding_end_time: 0,
    tenant_branding_json_end_time: 0,
    tenant_branding_load_error: false,
    has_tenant_branding: false
};
var Instrument = MSLogin.Instrument;
MSLogin.User = {
    UpdateUsernameState: function () {
        var b = true,
		a = false,
		c = $("#cred_userid_inputtext").val();
        Context.username_state.is_empty = a;
        Context.username_state.is_partial = a;
        if (c == "") {
            Context.username_state.is_empty = b;
            Context.username_state.is_partial = b;
            Context.username_state.is_timeout = a;
            Context.username_state.disable_password = b;
            Context.username_state.enable_redirect = a;
            Context.username_state.home_realm_state = Constants.State.NONE
        }
        var d = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
        if (!d.test(c)) {
            Context.username_state.is_empty = a;
            Context.username_state.is_partial = b;
            Context.username_state.home_realm_state = Constants.State.NONE
        }
        if (Context.username_state.is_timeout) {
            Context.username_state.is_timeout = b;
            Context.username_state.disable_password = a;
            Context.username_state.enable_redirect = a
        }
        switch (Context.username_state.home_realm_state) {
            case Constants.State.FEDERATED:
                Context.username_state.is_timeout = a;
                Context.username_state.disable_password = b;
                Context.username_state.enable_redirect = b;
                break;
            case Constants.State.MANAGED:
                Context.username_state.is_timeout = a;
                Context.username_state.disable_password = a;
                Context.username_state.enable_redirect = a;
                break;
            case Constants.State.INVALID:
                Context.username_state.is_timeout = a;
                Context.username_state.disable_password = b;
                Context.username_state.enable_redirect = a;
                break;
            default:
                Context.username_state.is_timeout = a;
                Context.username_state.disable_password = b;
                Context.username_state.enable_redirect = a
        }
        $("#home_realm_discovery").val(Context.username_state.home_realm_state)
    },
    UsernameOnChangeHandler: function () {
        var j = ".guest_redirect_container",
		i = "#guest_hint_text",
		h = ".login_guest_container",
		c = "#redirect_message_container",
		e = "hidden",
		g = "#cred_password_container",
		l = "disabled_button",
		d = "#cred_sign_in_button",
		a = "opacity",
		f = "visible",
		b = "visibility",
		k = "#recover_container";
        User.UpdateUsernameState();
        $(k).css(b, f);
        $(k).css(a, 1);
        if (Context.username_state.disable_password) $(d).addClass(l);
        else $(d).removeClass(l);
        if (Context.username_state.enable_redirect) {
            $(g).css(a, 0);
            $(g).css(b, e);
            $(c).css("display", "block");
            $(c).css(a, 1);
            $(c).css(b, f);
            $(h).css(a, 0);
            $(d).css(a, 0);
            $(d).css(b, e)
        } else {
            $(g).css(b, f);
            $(g).css(a, 1);
            $(c).css(a, 0);
            $(c).css(b, e);
            $(c).css("display", "none");
            $(h).css(a, 1);
            $(d).css(a, 1);
            $(d).css(b, f)
        }
        if (Context.username_state.enable_guests) {
            $(i).show();
            $(j).show()
        } else {
            $(i).hide();
            $(j).hide()
        }
        if (!Context.username_state.enable_progress_bar) {
            clearInterval(Context.animationTid);
            $("div.progress").css(b, e)
        }
    },
    setupPrefetching: function () {
        if (Context.prefetch_initiated) return;
        if (!Support.is_prefetch_supported()) return;
        if (Constants.PREFETCH_URL != "") if (User.latencySensitivity() >= -1) {
            Instrument.prefetch_start = Util.now();
            Util.PrefetchContent(Constants.PREFETCH_URL);
            Context.prefetch_initiated = true
        }
    },
    onLoadImage: function () {
        var b = "background-color",
		a = "#background_page_overlay",
		c = $("#background_background_image");
        Background.background_image_ratio = c.width() / c.height();
        Background.background_image_loaded = true;
        if (Background.background_resized) {
            $(a).css("visibility", "visible");
            $(a).css(b, $("html").css(b));
            $(a).fadeOut(500, "linear")
        }
    },
    updateGuestLink: function () {
        var b = "#guest_redirect_link",
		a = $(b).attr("href");
        if (a == "" || a == undefined) return;
        a = User.addFederatedRedirectQSParameters(a);
        $(b).attr("href", a)
    },
    setupCredValidation: function () {
        var b = "#cred_password_inputtext",
		a = "#cta_error_message_text";
        $("#credentials").validate({
            errorLabelContainer: a,
            wrapper: "li",
            submitHandler: function (a) {
                $(b).focus();
                a.submit()
            },
            showErrors: function (d, c) {
                if (c.length > 0) {
                    $(a).html("");
                    $(a).html(c[0].message)
                }
                try {
                    if (d["user_id"]) {
                        Context.username_state.is_partial = true;
                        Context.username_state.disable_password = true;
                        Context.username_state.home_realm_state = Constants.State.NONE;
                        $(b).blur();
                        $("#cred_userid_inputtext").focus();
                        User.UsernameOnChangeHandler()
                    }
                } catch (e) { }
            }
        })
    },
    forceOrientationResize: function () {
        Context.original_orientation = -1;
        User.resize_bg(Context.original_orientation)
    },
    setupCredFieldsWithPlaceHolder: function () {
        var p = "#cred_userid_inputtext",
		g = "hidden",
		f = "alpha(opacity=0)",
		e = "#FFFFFF",
		j = "visible",
		d = "visibility",
		i = "alpha(opacity=100)",
		c = "filter",
		a = ".placeholder",
		h = "transparent",
		b = "background",
		l = "input[placeholder]",
		o = "input#cred_keep_me_signed_in_checkbox",
		k = "input#cred_password_inputtext",
		m = "input#cred_userid_inputtext";
        $(m).focus(function () {
            Context.username_state.enable_redirect && User.CancelRedirect()
        });
        $(m).blur(function () {
            if (/Android|webOS|iPhone|iPad|iPod|BlackBerry/i.test(navigator.userAgent)) {
                User.RefreshDomainState();
                User.forceOrientationResize()
            }
            User.UsernameOnChangeHandler()
        });
        $(m).change(function () {
            User.disablePassword()
        });
        $(m).keydown(function (a) {
            User.KeyPressEnter(a, User.RefreshDomainState, 13);
            Context.email_discovery_workflow_state != EmailDiscovery.WorkflowStates.INIT && User.KeyPressEnter(a, User.RefreshDomainState, 9)
        });
        $(k).keypress(function (a) {
            User.KeyPressEnter(a, Post.SubmitCreds, 13)
        });
        $(o).keypress(function (a) {
            User.KeyPressEnter(a, Post.SubmitCreds, 13)
        });
        $(o).change(function () {
            User.updateGuestLink()
        });
        $(k).click(function () {
            User.UsernameOnChangeHandler(); !Context.username_state.is_empty && !Context.username_state.is_partial && User.RefreshDomainState()
        });
        $(k).focus(function () { });
        $(k).blur(function () {
            User.forceOrientationResize()
        });
        Util.debug_console("placeholder supported: " + Support.is_placeholder_supported());
        if (!Support.is_placeholder_supported()) {
            $(l).each(function () {
                var b = "placeholder",
				c = this,
				d = $(c).attr(b),
				a = $('<span class="placeholder">' + d + "</span>").appendTo($(c).parent());
                $(c).before(a);
                a.css("position", "absolute");
                a.css("display", "block");
                a.addClass("normaltext");
                a.click(function () {
                    $(this).siblings("input").focus()
                });
                $(c).attr(b, " ")
            });
            $(l).focus(function () {
                var k = this;
                if ($(k).val() == "") {
                    $(k).css(b, h);
                    $(k).siblings(a).css(c, i);
                    $(k).siblings(a).css(d, j)
                } else {
                    $(k).css(b, e);
                    $(k).siblings(a).css(c, f);
                    $(k).siblings(a).css(d, g)
                }
            });
            $(l).keydown(function () {
                $(this).css(b, e);
                $(this).siblings(a).css(c, i);
                $(this).siblings(a).css(d, g)
            });
            $(l).keyup(function () {
                var i = this;
                if ($(i).val() == "") {
                    $(i).css(b, h);
                    $(i).siblings(a).css(c, f);
                    $(i).siblings(a).css(d, j)
                } else {
                    $(i).css(b, e);
                    $(i).siblings(a).css(c, f);
                    $(i).siblings(a).css(d, g)
                }
            });
            $(l).blur(function () {
                var k = this;
                if ($(k).val() == "") {
                    $(k).css(b, h);
                    $(k).siblings(a).css(c, i);
                    $(k).siblings(a).css(d, j)
                } else {
                    $(k).css(b, e);
                    $(k).siblings(a).css(c, f);
                    $(k).siblings(a).css(d, g)
                }
            });
            $(k).parent().click(function () {
                User.UsernameOnChangeHandler(); !Context.username_state.is_empty && !Context.username_state.is_partial && User.RefreshDomainState()
            })
        }
        var n = decodeURIComponent(Util.ExtractQSParam("username")),
		q = $(p).val();
        if (n != "") {
            var r = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
            r.test(n) && q == "" && $(p).val(n)
        }
        $("#redirect_cta_text").hide()
    },
    setupCallToActionMessages: function () {
        $.each(Constants.GENERIC_ERROR_CODES,
		function (b, a) {
		    $("#cta_client_error_text").append($("<div/>").addClass("client_error_msg").addClass(a.toString()).html(Constants.TokenizedStringMsgs.GENERIC_ERROR.replace("#~#ErrorCode#~#", a)))
		})
    },
    disablePassword: function () {
        var a = "input#cred_userid_inputtext";
        Util.debug_console("Username value: " + $(a).val());
        if ($(a).val() == "");
    },
    enablePassword: function () { },
    resize_bg: function (e) {
        var d = " x ",
		b = "#background_background_image";
        if (e != undefined) Context.original_orientation = e;
        if (document.body && document.body.offsetWidth) {
            Background.winW = document.body.offsetWidth;
            Background.winH = document.body.offsetHeigh
        }
        if (document.compatMode == "CSS1Compat" && document.documentElement && document.documentElement.offsetWidth) {
            Background.winW = document.documentElement.offsetWidth;
            Background.winH = document.documentElement.offsetHeight
        }
        if (window.innerWidth && window.innerHeight) {
            Background.winW = window.innerWidth;
            Background.winH = window.innerHeight
        }
        var h = document.documentMode;
        $.browser.msie && h >= 10 && $("td#login_panel_center").css("height", Background.winH - 10);
        Support.renderForVirtualKeyboard();
        if (!Background.background_image_loaded && Background.delayed_background_fetch_url == null) return;
        if (Background.background_resized && typeof window.orientation != "undefined") if (Context.original_orientation == window.orientation) return;
        Context.original_orientation = window.orientation;
        if (Support.showIllustration() && Background.delayed_background_fetch_url != null) {
            $(b).error(function () {
                Instrument.tenant_branding_load_error = true
            });
            User.UpdateBackground(Background.delayed_background_fetch_url, $("html").css("background-color"))
        }
        Util.debug_console("resize_bg Background: " + Background.winW + d + Background.winH);
        var g = Support.getViewport();
        Util.debug_console("resize_bg Viewport: " + g[0] + d + g[1]);
        Util.debug_console("resize_bg JQuery: " + $("body").width() + d + $("body").height());
        $("img#background_background_image").error(function () {
            $(this).hide()
        });
        if (!Background.background_image_loaded || Background.background_image_ratio == 0) return;
        var i = $(document).width() - 400,
		f = $(document).height(),
		c = i,
		a = Math.round(c / Background.background_image_ratio);
        $(b).width(c);
        $(b).height(a);
        if (a < f) {
            a = f;
            c = Math.round(a * Background.background_image_ratio);
            $(b).width(c);
            $(b).height(a)
        }
        if (Background.background_image_loaded && !Background.background_resized) {
            Background.background_resized = true;
            User.onLoadImage()
        }
        Util.debug_console("bg_image: " + c + d + a)
    },
    getElementHeightSpacing: function (b, c) {
        var a = 0;
        a += $(b).height();
        if (c) {
            a += parseInt($(b).css("padding-top"));
            a += parseInt($(b).css("padding-bottom"))
        }
        return a
    },
    getElementHeightMargin: function (b) {
        var a = 0;
        a += parseInt($(b).css("margin-top"));
        a += parseInt($(b).css("margin-bottom"));
        return a
    },
    doesElementExist: function (a) {
        return $(a).height() != null
    },
    moveFooterToBottom: function () { },
    latencySensitivity: function () {
        var a = Constants.LATENCY_SENSITIVITY_DEFAULT,
		b = Util.ExtractQSParam("bandwidth");
        if (Support.isUXFeatureEnabled(16)) {
            var c = performance.timing;
            if (c.responseEnd - c.fetchStart > Constants.LATENCY_THRESHOLD) b = "low"
        }
        if (Support.isUXFeatureEnabled(8)) b = "low";
        switch (b) {
            case "low":
                a = -1;
                break;
            case "high":
                a = 1;
                break;
            case "critical":
                a = -2
        }
        Instrument.latency_sensitivity = a;
        return a
    },
    updateClientMetricsMode: function () {
        var b = true,
		a = false;
        switch (Constants.METRICS_MODE) {
            case 0:
                Constants.SUBMIT_METRICS_ON_REDIRECT = a;
                Constants.SUBMIT_METRICS_ON_POST = a;
                break;
            case 1:
                Constants.SUBMIT_METRICS_ON_REDIRECT = a;
                Constants.SUBMIT_METRICS_ON_POST = b;
                break;
            case 2:
                Constants.SUBMIT_METRICS_ON_REDIRECT = b;
                Constants.SUBMIT_METRICS_ON_POST = a;
                break;
            case 3:
                Constants.SUBMIT_METRICS_ON_REDIRECT = b;
                Constants.SUBMIT_METRICS_ON_POST = b;
                break;
            default:
                Constants.SUBMIT_METRICS_ON_REDIRECT = a;
                Constants.SUBMIT_METRICS_ON_POST = a
        }
    },
    GetHashedImagePath: function (a) {
        return a
    },
    UpdateBackground: function (b, a) {
        var h = "img#background_background_image",
		g = "opacity",
		f = "#background_company_name_text",
		e = "visibility",
		d = "#background_background_image",
		c = "background-color";
        if (a == null || a == "") a = $("html").css(c);
        $("#background_page_overlay").css(c, a);
        $(d).css(c, a);
        $("#background_branding_container").css(c, a);
        $("html").css(c, a);
        if (Background.winW < Constants.MOBILE_WIDTH_THRESHOLD) {
            Util.debug_console("Screen size too small to fetch background.");
            Background.delayed_background_fetch_url = b;
            return
        }
        $(d).error(function () {
            $(this).show()
        });
        User.onLoadImage();
        Background.delayed_background_fetch_url = null;
        if (!Support.showIllustration() || b == "" || User.latencySensitivity() < 0) {
            Util.debug_console("Image load was cancelled.");
            $(d).css(e, "hidden");
            if (Support.isUXFeatureEnabled(8) || Support.isUXFeatureEnabled(16)) {
                $(f).css(g, 1);
                Support.isUXFeatureEnabled(16) && b != "" && $("#auto_low_bandwidth_background_notification").css(e, "visible")
            } else $(f).css(g, 0);
            setTimeout(function () {
                User.setupPrefetching()
            },
			1e3)
        } else {
            var i = User.GetHashedImagePath(b);
            $(h).attr("src", i);
            $(h).css(e, "visible");
            $(f).css(g, 0);
            alt_background_image = b;
            Util.debug_console("Set background image: " + b);
            Instrument.background_image_start = Util.now()
        }
        Background.background_image_ratio = 0;
        Util.debug_console("Fetched background image.")
    },
    UpdateLogo: function (a, e) {
        var c = "visibility",
		b = ".login_workload_logo_container",
		d = User.GetHashedImagePath(a);
        if (a == "") {
            $(".workload_img").hide();
            $(b).append($("<h1/>").attr("id", "login_workload_logo_text").css(c, "visible").addClass("gianttext").html(e));
            Util.debug_console("Set logo image failover.")
        } else {
            $(b).append($("<img/>").attr("id", "login_workload_logo_image").css(c, "visible").addClass("workload_img").attr("src", d).attr("alt", Constants.PARTNER_NAME));
            $("#login_workload_logo_text").remove();
            alt_logo_image = a;
            Util.debug_console("Set logo image: " + a)
        }
    },
    RefreshDomainState: function () {
        var c = "content",
		b = "meta[name=PageID]",
		a = false,
		d = $("input#cred_userid_inputtext").val();
        User.UpdateUsernameState();
        if ($(b).attr(c) == "strongauthcheck.2.0" || $(b).attr(c) == "proofup.2.0" || $(b).attr(c) == "i5028.2.0") return false;
        else if (Context.username_state.is_empty || Context.username_state.is_partial) a = false;
        else if (d.toLowerCase() != Context.username_state.last_checked_email.toLowerCase()) a = true;
        else if (Context.username_state.home_realm_state == Constants.State.NONE) a = true;
        a && User.CheckUser();
        return a
    },
    CheckUser: function () {
        if (EmailDiscovery.ShouldPerformDiscovery()) EmailDiscovery.PerformEmailBasedDiscovery();
        else User.PerformHomeRealmDiscovery()
    },
    PerformHomeRealmDiscovery: function () {
        var a = $("input#cred_userid_inputtext").val();
        a = $.trim(a);
        $("div.progress").css("visibility", "visible");
        User.startAnimation();
        Context.animationTid = setInterval(User.startAnimation, 3500);
        Context.username_state.enable_progress_bar = true;
        $.ajax({
            url: "/GetUserRealm.srf",
            dataType: "json",
            data: {
                login: encodeURI(a),
                handler: "1",
                extended: "1"
            },
            beforeSend: function () {
                Instrument.home_realm_start = Util.now()
            },
            success: User.ReceiveTenantInfo,
            error: User.ReceivedHomeRealmError
        });
        Context.username_state.home_realm_state = Constants.State.PENDING;
        Context.username_state.last_checked_email = a;
        User.resize_bg()
    },
    showGenericError: function (a) {
        Support.hideClientErrorMessages();
        Support.hideClientMessages();
        Support.showClientError(a)
    },
    ReceivedHomeRealmError: function (c, a) {
        $("div.progress").css("visibility", "hidden");
        $("input#cred_password_inputtext").focus();
        Util.debug_console("Received home realm error");
        Context.username_state.home_realm_state = Constants.State.NONE;
        clearInterval(Context.animationTid);
        Context.username_state.home_realm_state = Constants.State.NONE;
        User.UsernameOnChangeHandler();
        a == "parsererror" && User.showGenericError(ErrorCodes.PARSE_ERROR)
    },
    ReceiveTenantCustom: function (a) {
        tenant_info.background_color = a.BackgroundColor;
        tenant_info.background_url = a.BackgroundPath;
        tenant_info.logo_url = a.LogoPath;
        tenant_info.boilerplate1 = a.BoilerPlate1;
        tenant_info.boilerplate2 = a.BoilerPlate2;
        tenant_info.cta1 = a.CTA1;
        tenant_info.cta2 = a.CTA2;
        Util.debug_console(a.LogoPath);
        clearInterval(Context.animationTid);
        Context.username_state.home_realm_state = Constants.State.MANAGED;
        User.UsernameOnChangeHandler();
        $("#cred_userid_inputtext").blur();
        $("#cred_password_inputtext").focus();
        setTimeout(User.resize_bg, 300)
    },
    ReceivedTenantError: function () {
        Util.debug_console("Received tenant error.")
    },
    KeyPressEnter: function (a, c, d) {
        var b;
        if (a && a.which) b = a.which;
        else if (window.event) {
            a = window.event;
            b = a.keyCode
        }
        b == d && c();
        return b
    },
    StartRedirection: function (c) {
        var b = "meta[name=PageID]",
		a = "#cred_userid_container";
        Context.redirect_countdown = Constants.REDIRECT_COUNTDOWN_DEFAULT;
        if (c && $(a).css("display") == "none") return;
        if ($(a).css("display") == undefined) return;
        if (Support.isUXFeatureEnabled(4096) && ($(b).attr("content") == "EmailBasedDiscovery.2.0" || $(b).attr("content") == "EmailBasedDiscoveryTiles.2.0")) {
            $("#cred_keep_me_signed_in_checkbox").removeAttr("checked");
            EmailDiscovery.is_enabled_workflow = true
        }
        $("#cred_cancel_button").hide();
        Context.username_state.enable_progress_bar = true;
        Context.username_state.enable_redirect = true;
        Context.username_state.enable_guests = false;
        cancel_redirect = false;
        $("#cred_password_container").css("opacity", 0);
        User.UsernameOnChangeHandler();
        Support.isUXFeatureEnabled(4096) && EmailDiscovery.IsDiscoveryPage() && $("#cred_kmsi_container").hide();
        Context.redirectTid = setTimeout("User.ShowRedirect('" + Context.federated_domain + "');$('a#redirect_cancel_link').focus();", 100);
        return Context.redirectTid
    },
    ReceiveTenantInfo: function (a) {
        var b = true;
        Instrument.home_realm_load_time = Util.now() - Instrument.home_realm_start;
        Context.username_state.enable_progress_bar = false;
        Context.username_state.disable_password = false;
        Context.username_state.last_checked_email = a.Login;
        if (a.NameSpaceType == Constants.NameSpaceType.FEDERATED) {
            Context.back_action_stack.AddAction(Constants.CancelAction.FROM_REDIRECT_TO_LOGIN);
            b = MSLogin.User.ReceiveFederatedDomain(a)
        } else if (a.NameSpaceType == Constants.NameSpaceType.MANAGED) b = MSLogin.User.ReceiveManagedDomain(a);
        else b = MSLogin.User.ReceiveUnknownDomain(a);
        if (b) {
            $("#cred_userid_inputtext").blur();
            $("#cred_password_inputtext").focus()
        }
    },
    ReceiveUnknownDomain: function (a) {
        if (a.State == 1 && !a.FederationGlobalVersion) {
            Context.username_state.home_realm_state = Constants.State.INVALID;
            User.UsernameOnChangeHandler();
            Util.debug_console("federationGlobalVersion bad");
            $("#cred_password_inputtext").blur();
            $("#cred_userid_inputtext").focus();
            return false
        }
        Context.username_state.home_realm_state = Constants.State.INVALID;
        clearInterval(Context.animationTid);
        User.UsernameOnChangeHandler();
        Util.debug_console("Unknown home realm reply");
        $("input#cred_password_inputtext").focus();
        return true
    },
    ReceiveFederatedDomain: function (a) {
        Context.username_state.home_realm_state = Constants.State.FEDERATED;
        tenant_info.company_name = "";
        tenant_info.domain = a.DomainName;
        if (a.DomainName == Context.whr) {
            Context.username_state.home_realm_state = Constants.State.MANAGED;
            Util.debug_console("whr set already");
            User.UsernameOnChangeHandler();
            $("#cred_userid_inputtext").blur();
            $("#cred_password_inputtext").focus();
            return false
        }
        Context.federated_domain = a.DomainName;
        Context.redirect_auth_url = a.AuthURL;
        Context.redirect_target = User.addFederatedRedirectQSParameters(a.AuthURL);
        Util.updateRedirectionMessage(a.AuthURL);
        a.FederationGlobalVersion == 1 && User.Redirect(Context.redirect_target);
        User.StartRedirection(a, true);
        return true
    },
    ReceiveManagedDomain: function (a) {
        if (a.DomainName != undefined) Context.whr = a.DomainName;
        else Context.whr = a.Login.split("@")[1];
        Context.username_state.home_realm_state = Constants.State.MANAGED;
        tenant_info.company_name = a.FederationBrandName;
        tenant_info.domain = a.DomainName;
        tenant_info.BackgroundColor = $("#background_branding_container").css("background");
        tenant_info.BackgroundPath = $("#background_background_image").attr("src");
        tenant_info.LogoPath = $("#login_workload_logo_image").attr("src");
        tenant_info.BoilerPlate1 = "";
        tenant_info.BoilerPlate2 = "";
        tenant_info.CTA1 = $("#login_cta_text").text();
        tenant_info.CTA2 = "";
        tenant_info.HadBoilerPlate = false;
        User.ReceiveTenantCustom(tenant_info);
        return false
    },
    Redirect: function (a) {
        window.location.assign(a)
    },
    ShowRedirect: function (b) {
        var a = "#redirect_message_container";
        Context.redirect_target = User.addFederatedRedirectQSParameters(Context.redirect_auth_url);
        if (Constants.SUBMIT_METRICS_ON_REDIRECT && !Context.metrics_on_redirect_submitted) {
            Post.SubmitInstrumentation();
            Context.metrics_on_redirect_submitted = true
        }
        Context.username_state.enable_guests = false;
        Support.isUXFeatureEnabled(4096) && EmailDiscovery.IsDiscoveryPage() && $("#cred_kmsi_container").hide();
        User.UsernameOnChangeHandler();
        if (Context.redirect_countdown == 0) User.Redirect(Context.redirect_target);
        else Context.redirect_countdown == 1 && $("#background_page_overlay").css("z-index", 0);
        $("#cred_userid_container").hide();
        $("#cred_password_container").hide();
        $(".login_cta_container").hide();
        $("#recover_container").hide();
        $("#redirect_cta_text").show();
        $("input#cred_password_inputtext").hide();
        $("#redirect_company_name_text").html("");
        $(a).show();
        $(a).css("visibility", "visible");
        $(a).css("opacity", 1);
        Context.redirect_countdown = Context.redirect_countdown - 1;
        Context.redirectTid = setTimeout("User.ShowRedirect('" + b + "')", 1e3);
        Util.debug_console("Redirecting in " + Context.redirect_countdown)
    },
    ForceRedirect: function () {
        window.location.replace(Context.redirect_target)
    },
    CancelRedirect: function () {
        var b = "input#cred_password_inputtext",
		a = "#cred_password_container";
        clearTimeout(Context.redirectTid);
        Context.back_action_stack.DoesCancelActionExist() && $("#cred_cancel_button").show();
        Support.isUXFeatureEnabled(4096) && EmailDiscovery.IsDiscoveryPage() && $("#cred_kmsi_container").show();
        $("#cred_userid_container").show();
        $(a).show();
        $(".login_cta_container").show();
        $("#recover_container").show();
        $("#redirect_cta_text").hide();
        $(a).css("opacity", 1);
        $(b).css("display", "inline-block");
        $(b).blur();
        $("#redirect_company_name_text").html("");
        if (Context.username_state.home_realm_state == Constants.State.FEDERATED) Context.username_state.home_realm_state = Constants.State.NONE;
        Context.username_state.disable_password = true;
        Context.username_state.enable_progress_bar = false;
        Context.username_state.enable_redirect = false;
        Context.username_state.enable_guests = true;
        User.UsernameOnChangeHandler();
        $("input#cred_userid_inputtext").focus()
    },
    startAnimation: function () {
        var a = 100;
        $(".pip").stop(true, true);
        $(".pip").each(function () {
            User.animatePip($(this), a);
            a += 100
        })
    },
    animatePip: function (f, g) {
        var b = "easeInSine",
		a = "easeOutSine",
		c = $(".login_footer_container").outerWidth(),
		d = c / 3,
		e = c - 3;
        if (Constants.DIR == "ltr") f.css("left", 0).hide().delay(g).show().animate({
            left: d
        },
		{
		    duration: 1e3,
		    easing: a
		}).animate({
		    left: e
		},
		{
		    duration: 999,
		    easing: b
		});
        else f.css("right", 0).hide().delay(g).show().animate({
            right: d
        },
		{
		    duration: 1e3,
		    easing: a
		}).animate({
		    right: e
		},
		{
		    duration: 999,
		    easing: b
		})
    },
    addFederatedRedirectQSParameters: function (a) {
        var b = "&LoginOptions=",
		d = "username=";
        User.resize_bg();
        var g = decodeURIComponent(Util.ExtractQSParam("cbcxt"));
        a = a.replace(/cbcxt=[^&]*/i, "cbcxt=" + encodeURIComponent(g));
        var j = decodeURIComponent(Util.ExtractQSParam("vv"));
        a = a.replace(/vv=[^&]*/i, "vv=" + encodeURIComponent(j));
        var f = decodeURIComponent(Util.ExtractQSParam("username")),
		e = $("#cred_userid_inputtext").val();
        if (e != "") a = a.replace(/username=[^&]*/i, d + encodeURIComponent(e));
        else if (f != "") a = a.replace(/username=[^&]*/i, d + encodeURIComponent(f));
        var h = decodeURIComponent(Util.ExtractQSParam("mkt"));
        a = a.replace(/mkt=[^&]*/i, "mkt=" + encodeURIComponent(h));
        var i = decodeURIComponent(Util.ExtractQSParam("lc"));
        a = a.replace(/lc=[^&]*/i, "lc=" + encodeURIComponent(i));
        var c = decodeURIComponent(Util.ExtractQSParam("popupui"));
        a = a.replace(/popupui=[^&]*/i, "popupui=" + encodeURIComponent(c));
        if (c && a.indexOf("popupui") < 0) a += "&popupui=" + encodeURIComponent(c);
        else if (Background.winW < Constants.MOBILE_WIDTH_THRESHOLD) a += "&popupui=1";
        Constants.FEDERATION_QUERY_PARAMETERS = Constants.FEDERATION_QUERY_PARAMETERS.replace(/&username(=[^&]*)?|^username(=[^&]*)?&?/i, "");
        if (a.indexOf("wtrealm") == -1) a += "&" + Constants.FEDERATION_QUERY_PARAMETERS;
        if (a.indexOf("wctx") == -1 && Constants.FEDERATION_QUERY_PARAMETERS.indexOf("wctx") == -1) a += "&wctx=" + encodeURIComponent(Context.query_search_params);
        a = a.replace(/guests%3D1/i, "");
        if ($("#cred_keep_me_signed_in_checkbox").attr("checked")) a += encodeURIComponent(b + LoginOption.REMEMBER_PASSWORD);
        else if (Support.isUXFeatureEnabled(4096) && EmailDiscovery.IsDiscoveryPage()) a += encodeURIComponent(b + LoginOption.REMEMBER_USER_TILE);
        else a += encodeURIComponent(b + LoginOption.NOTHING_CHECKED);
        return a
    },
    AddFooterLink: function (b, d, e, a) {
        var c = "footer_link_" + b;
        if (a == "" || a == null) a = c;
        $(".corporate_footer").append($("<span/>").addClass("corp_link").append($("<a/>").attr("tabindex", 40 + b).attr("id", c).addClass(a).attr("href", e).html(d)))
    },
    DrawDefaultFooterLinks: function () {
        $.each(Constants.DEFAULT_ENABLED_FOOTER_LINKS,
		function (b, a) {
		    if (Constants.DEFAULT_FOOTER_LINKS[a] == undefined) return;
		    User.AddFooterLink(b, Constants.DEFAULT_FOOTER_LINKS[a]["label"], Constants.DEFAULT_FOOTER_LINKS[a]["url"], "footer_link_" + a)
		})
    },
    ProcessFooterLinks: function (b) {
        var a = -1;
        $.each(b,
		function (e, b) {
		    var c = b[0],
			f = b[1],
			d = b[2];
		    a += 1;
		    User.AddFooterLink(e, c, f, d)
		});
        a < 0 && User.DrawDefaultFooterLinks()
    },
    getTenantBrandingContext: function (b, d) {
        var c = false,
		a = {},
		e = [["BannerLogo", true, Constants.DEFAULT_LOGO], ["TileLogo", true, ""], ["Illustration", true, Constants.DEFAULT_ILLUSTRATION], ["BackgroundColor", c, Constants.DEFAULT_BACKGROUND_COLOR], ["CompanyName", c, ""], ["UserIdLabel", c, $("#cred_userid_inputtext").attr("placeholder")], ["BoilerPlateText", c, ""]];
        $.each(e,
		function (g, e) {
		    var c = e[0],
			f = e[1];
		    if (b[d] != undefined && b[d][c] != undefined && b[d][c] != "") a[c] = b[d][c];
		    else a[c] = b["0"][c];
		    if (a[c] == null || a[c] == undefined) a[c] = e[2];
		    if (f) a[c] = a[c].toLowerCase()
		});
        return a
    },
    formatTenantBrandingData: function (a) {
        result = {};
        $.each(a,
		function (c, a) {
		    var b = a["Locale"];
		    if (b >= 0) result[b] = a
		});
        return result
    },
    processTenantBranding: function (a) {
        var c = "#footer_links_container";
        if (a == null) return;
        Instrument.has_tenant_branding = true;
        if (!("0" in a)) a = formatTenantBrandingData(a);
        var b = User.getTenantBrandingContext(a, Constants.LCID),
		f = $("<div/>").attr("id", "boiler_plate").addClass("tinytext").text(b.BoilerPlateText);
        $(".footer_inner_container").before(f);
        var e = $(c).height() + $("#boiler_plate").outerHeight();
        $(c).css("height", e + "px");
        $(c).css("margin-top", "-" + e + "px");
        var d = $(".cred").height();
        d += $(".login_cred_options_container").height();
        $(".cred").css("height", d);
        User.UpdateBackground(b.Illustration, b.BackgroundColor);
        User.UpdateLogo(b.BannerLogo, Constants.DEFAULT_LOGO_ALT);
        $("#login_workload_logo_text").hide();
        Support.showClientMessage(30008);
        Instrument.tenant_branding_end_time = (new Date).getTime()
    },
    GetBrandingInfo: function (b) {
        var a = document.createElement("script");
        a.type = "text/javascript";
        a.src = b;
        Context.tenant_branding_tid = setInterval(User.ApplyTenantBranding, Constants.tenant_branding_polling_interval);
        document.body.appendChild(a)
    },
    ApplyTenantBranding: function () {
        if (typeof LoginTenantBranding != "undefined") {
            Instrument.tenant_branding_json_end_time = (new Date).getTime();
            Constants.TENANT_BRANDING = LoginTenantBranding
        }
        if (Constants.tenant_branding_retry_count > 0) {
            Constants.tenant_branding_retry_count -= Constants.tenant_branding_polling_interval;
            if (Constants.tenent_branding_retry_count <= 0) {
                Instrument.tenant_branding_load_error = true;
                clearInterval(Context.tenant_branding_tid);
                Context.tenant_branding_tid = null;
                User.UpdateLogo(Constants.DEFAULT_LOGO, Constants.DEFAULT_LOGO_ALT);
                User.UpdateBackground(Constants.DEFAULT_ILLUSTRATION, Constants.DEFAULT_BACKGROUND_COLOR);
                return
            }
        }
        if (Constants.TENANT_BRANDING != null && Constants.TENANT_BRANDING != "") {
            clearInterval(Context.tenant_branding_tid);
            Context.tenant_branding_tid = null;
            User.processTenantBranding(Constants.TENANT_BRANDING)
        }
    }
};
var User = MSLogin.User,
tenant_info = {
    company_name: "",
    cta1: "",
    cta2: "",
    boilerplate1: "",
    boilerplate2: "",
    background_url: "",
    background_color: "",
    logo_url: "",
    domain: "",
    lang: ""
};
$(document).ready(function () {
    pageOnReady()
});
function pageOnReady() {
    var a = "#cred_cancel_button",
	c = "#cred_sign_in_button",
	b = "#background_background_image";
    $(b).css("width", "auto");
    $(b).css("height", "auto");
    $(b).load(function () {
        if (this.width != undefined) Background.background_image_loaded = true;
        User.onLoadImage();
        Instrument.background_image_load_time = Util.now() - Instrument.background_image_start;
        Instrument.background_image_loaded = true;
        setTimeout(function () {
            User.setupPrefetching()
        },
		1e3);
        User.resize_bg()
    });
    if ($.browser.msie && $.browser.version.slice(0, 1) == "8") document.body.onresize = function () {
        User.resize_bg();
        User.moveFooterToBottom()
    };
    else $(window).resize(function () {
        User.resize_bg();
        User.moveFooterToBottom()
    });
    $("#cred_userid_inputtext").val() != undefined && User.setupCredFieldsWithPlaceHolder();
    Context.whr = decodeURIComponent(Util.ExtractQSParam("whr"));
    User.updateGuestLink();
    $("#redirect_cancel_link").attr("href", "javascript:Context.back_action_stack.TriggerCancelAction();");
    $("#login_panel").show();
    $("#login_no_script_panel").hide();
    $(".login_cred_container").append($("<input/>").attr("id", "home_realm_discovery").attr("type", "hidden").val(Context.username_state.home_realm_state));
    $(c).click(function () {
        Post.SubmitCreds()
    });
    $(c).keydown(function (a) {
        User.KeyPressEnter(a, Post.SubmitCreds, 13)
    });
    $(".refresh_domain_state").click(function () {
        User.RefreshDomainState()
    });
    $("input[type=checkbox]").click(function () {
        User.RefreshDomainState()
    });
    $(a).click(function () {
        Context.back_action_stack.TriggerCancelAction()
    });
    $(a).keydown(function (b) {
        User.KeyPressEnter(b,
		function () {
		    $(a).click();
		    b.preventDefault()
		},
		13)
    });
    $("#background_company_name_text").text(Constants.PARTNER_NAME);
    Constants.FEDERATION_QUERY_PARAMETERS = Constants.FEDERATION_QUERY_PARAMETERS.replace(/&username(=[^&]*)?|^username(=[^&]*)?&?/, "");
    $("#footer_links_container").addClass("sticky_footer");
    Support.renderBrowserSpecific()
}
$(window).load(function () {
    var d = "form#credentials",
	c = "meta[name=PageID]",
	b = "input#cred_userid_inputtext";
    Support.renderBrowserSpecific();
    User.updateClientMetricsMode();
    if ($(b).val() != undefined && $(b).val().indexOf("@") != -1) if (EmailDiscovery.ShouldPerformDiscovery()) {
        $("#cred_continue_button").focus();
        $("#redirect_message_container").hide()
    } else {
        Context.username_state.home_realm_state = Constants.State.MANAGED;
        User.UsernameOnChangeHandler();
        Util.debug_console("startup");
        $(b).focus();
        if (!Constants.MSA_ENABLED) {
            $("input#cred_password_inputtext").focus();
            User.RefreshDomainState()
        }
    } else {
        User.UsernameOnChangeHandler();
        $(b).focus()
    }
    if (Support.isUXFeatureEnabled(4096) && ($(c).attr("content") == "EmailBasedDiscovery.2.0" || $(c).attr("content") == "EmailBasedDiscoveryTiles.2.0")) {
        $("#cred_keep_me_signed_in_checkbox").removeAttr("checked");
        EmailDiscovery.is_enabled_workflow = true
    }
    var a = $(d).attr("action");
    if (a != null && a != undefined) {
        a = a.replace(/guests=1&/i, "");
        $(d).attr("action", a)
    }
})
MSLogin.Util = {
    PrefetchContent: function (b) {
        var a = "seamless";
        $("#login_prefetch_container").append($("<iframe/>").attr("src", b).attr(a, a).attr("scrolling", "no").attr("id", "login_prefetch_iframe").load(function () {
            Instrument.prefetch_load_time = new Date - Instrument.prefetch_start;
            Instrument.prefetch_done = true
        }))
    },
    getCookie: function (e) {
        for (var c, d, b = document.cookie.split(";"), a = 0; a < b.length; a++) {
            c = b[a].substr(0, b[a].indexOf("="));
            d = b[a].substr(b[a].indexOf("=") + 1);
            c = c.replace(/^\s+|\s+$/g, "");
            if (c == e) return unescape(d)
        }
    },
    setCookie: function (e, g, c) {
        var b = new Date;
        b.setDate(b.getDate() + c);
        var a = document.location.hostname.split("."),
		d = a.length,
		f = g + (c == null ? "" : ";domain=." + a[d - 2] + "." + a[d - 1] + ";path=/; expires=" + b.toUTCString()); + "; secure";
        document.cookie = e + "=" + f
    },
    eraseCookie: function (c) {
        var e = ";path=/;expires=Thu, 30-Oct-1980 00:00:01 GMT;",
		d = "= ;domain=.",
		b = document.location.hostname.split("."),
		f = b.length,
		a = c + d + b[f - 2] + "." + b[f - 1] + e;
        Util.debug_console(a);
        document.cookie = a;
        a = c + d + document.location.hostname + e;
        Util.debug_console(a);
        document.cookie = a
    },
    exists: function (a) {
        return a ? true : a == 0 || a == false || a == ""
    },
    valOrDefault: function (a, b) {
        return Util.exists(a) ? a : b
    },
    debug_console: function () { },
    ExtractToken: function (b, d, c, e, h) {
        c = Util.valOrDefault(c, "&");
        e = Util.valOrDefault(e, "=");
        var f = Util.valOrDefault(h, null);
        if (!b) return f;
        var a = b.indexOf(d + e);
        if (0 == a) a += d.length + 1;
        else if (0 < a) {
            a = b.indexOf(c + d + e);
            if (0 < a) a += c.length + d.length + 1
        }
        if (-1 != a) {
            var g = b.indexOf(c, a);
            if (-1 == g) g = b.length;
            f = b.substring(a, g)
        }
        return f
    },
    ExtractQSParam: function (b) {
        var a = Context.query_search_params.toLowerCase();
        if (a) a = a.substr(1);
        return Util.ExtractToken(a, b.toLowerCase(), "&", "=", "")
    },
    AddQSParamIfNotExists: function (a, b, d) {
        var g = "[?&]" + b + "=([^&]+?)($|&)",
		i = new RegExp(g, "i"),
		h = a.match(i);
        if (!h) {
            var e = "([?&])" + b + "=?(&|$)",
			f = new RegExp(e, "i");
            a = a.replace(f, "$1");
            var c = Util.ExtractQSParam(b);
            if (c && (!d || d.test(c))) {
                if (a.indexOf("?") == -1) a += "?";
                else if (a.indexOf("?") < a.length - 1) a += "&";
                a += b + "=" + c
            }
        }
        return a
    },
    isMSA: function (a) {
        if (a.indexOf("login.live.com") != -1 || a.indexOf("login.live-int.com") != -1 || a.indexOf("login.microsoft.com") != -1) return true;
        return false
    },
    updateRedirectionMessage: function (b) {
        var a = Constants.REDIRECT_MESSAGES["AAD"];
        if (Util.isMSA(b)) a = Constants.REDIRECT_MESSAGES["MSA"];
        $("#redirect_message_text").html(a)
    },
    now: function () {
        var a = window.performance || {};
        a.now = function () {
            return a.now || a.webkitNow || a.msNow || a.oNow || a.mozNow ||
			function () {
			    return (new Date).getTime()
			}
        }();
        return a.now()
    }
};
var Util = MSLogin.Util
var PostType = {
    INVALID: "0",
    PASSWORD: "11",
    FEDERATION: "13",
    SHA1_PASSWORD: "15",
    SHA1HASH_PASSWORD: "16"
},
LoginOption = {
    EMPTY: "0",
    REMEMBER_PASSWORD: "1",
    REMEMBER_USER_TILE: "2",
    NOTHING_CHECKED: "3",
    OTHER_SAVED_USERS: "4",
    ACTIVE_USER: "5"
};
MSLogin.Post = {
    DetermineBrowser: function () {
        if ($.browser.msie) return "MSIE";
        else if ($.browser.chrome) return "Chrome";
        else if ($.browser.webkit) return "Webkit";
        else if ($.browser.safari) return "Safari";
        else if ($.browser.mozilla) return "Firefox";
        else if ($.browser.opera) return "Opera";
        else return "Unknown"
    },
    IsSubmitReady: function () {
        var c = "input#cred_password_inputtext",
		b = "input#cred_userid_inputtext",
		a = false;
        switch (Context.username_state.home_realm_state) {
            case Constants.State.FEDERATED:
                return a;
                break;
            case Constants.State.MANAGED:
                break;
            case Constants.State.INVALID:
                break;
            case Constants.State.NONE:
                User.UpdateUsernameState();
                if (!Context.username_state.is_empty && !Context.username_state.is_partial) {
                    User.RefreshDomainState();
                    return a
                }
                break;
            default:
                return a
        }
        if (!Support.validateCredInputs()) {
            if ($(b).css("display") != undefined && $(b).css("display") != "none" && $(c).val() != "") $(b).focus();
            else $(c).focus();
            return a
        }
        var e = $(b).val(),
		d = $(c).val();
        if (e == "" && d == "") return true;
        return true
    },
    GetCredParameters: function (f) {
        var d = "LoginOptions",
		a = [];
        if (Context.use_instrumentation && f) try {
            var e = new Date,
			c = e.getTime() - e.getMilliseconds() - 1.2e5,
			b = window.performance.timing;
            a.push({
                name: "n1",
                value: b.navigationStart - c
            });
            a.push({
                name: "n2",
                value: b.redirectStart - c
            });
            a.push({
                name: "n3",
                value: b.redirectEnd - c
            });
            a.push({
                name: "n4",
                value: b.fetchStart - c
            });
            a.push({
                name: "n5",
                value: b.domainLookupStart - c
            });
            a.push({
                name: "n6",
                value: b.domainLookupEnd - c
            });
            a.push({
                name: "n7",
                value: b.connectStart - c
            });
            a.push({
                name: "n8",
                value: b.secureConnectionStart - c
            });
            a.push({
                name: "n9",
                value: b.connectEnd - c
            });
            a.push({
                name: "n10",
                value: b.requestStart - c
            });
            a.push({
                name: "n11",
                value: b.responseStart - c
            });
            a.push({
                name: "n12",
                value: b.responseEnd - c
            });
            a.push({
                name: "n13",
                value: b.domLoading - c
            });
            a.push({
                name: "n14",
                value: b.domInteractive - c
            });
            a.push({
                name: "n15",
                value: b.domContentLoadedEventEnd - b.domContentLoadedEventStart
            });
            a.push({
                name: "n16",
                value: b.domComplete - c
            });
            a.push({
                name: "n17",
                value: b.loadEventStart - c
            });
            a.push({
                name: "n18",
                value: b.loadEventEnd - c
            });
            a.push({
                name: "n19",
                value: Instrument.home_realm_load_time
            });
            a.push({
                name: "n20",
                value: Instrument.latency_sensitivity
            });
            a.push({
                name: "n21",
                value: Instrument.prefetch_done ? 1 : 0
            });
            a.push({
                name: "n22",
                value: Instrument.prefetch_load_time
            });
            a.push({
                name: "n23",
                value: Instrument.background_image_loaded ? 1 : 0
            });
            a.push({
                name: "n24",
                value: Instrument.background_image_load_time
            });
            a.push({
                name: "n25",
                value: Tiles.users != null ? Tiles.users.length - 1 : 0
            });
            a.push({
                name: "n26",
                value: Instrument.email_discovery_ui_code
            });
            a.push({
                name: "n27",
                value: Instrument.email_discovery_splitter_choice
            });
            a.push({
                name: "n28",
                value: Instrument.possible_easi_user
            });
            a.push({
                name: "n29",
                value: Instrument.tenant_branding_end_time - Instrument.tenant_branding_start_time
            });
            a.push({
                name: "n30",
                value: Instrument.tenant_branding_json_end_time - Instrument.tenant_branding_start_time
            });
            a.push({
                name: "n31",
                value: Instrument.tenant_branding_load_error
            });
            a.push({
                name: "n32",
                value: Instrument.has_tenant_branding
            })
        } catch (g) { }
        a.push({
            name: "type",
            value: PostType.PASSWORD
        });
        if ($("#cred_keep_me_signed_in_checkbox").attr("checked")) a.push({
            name: d,
            value: LoginOption.REMEMBER_PASSWORD
        });
        else if (Support.isUXFeatureEnabled(4096) && EmailDiscovery.IsDiscoveryPage()) a.push({
            name: d,
            value: LoginOption.REMEMBER_USER_TILE
        });
        else a.push({
            name: d,
            value: LoginOption.NOTHING_CHECKED
        });
        a.push({
            name: "NewUser",
            value: 1
        });
        a.push({
            name: "idsbho",
            value: 1
        });
        a.push({
            name: "PwdPad",
            value: ""
        });
        a.push({
            name: "sso",
            value: ""
        });
        var h = decodeURIComponent(Util.ExtractQSParam("vv"));
        a.push({
            name: "vv",
            value: h
        });
        a.push({
            name: "uiver",
            value: 1
        });
        a.push({
            name: "i12",
            value: window.location.protocol == "https:" ? 1 : 0
        });
        a.push({
            name: "i13",
            value: Post.DetermineBrowser()
        });
        a.push({
            name: "i14",
            value: $.browser.version
        });
        a.push({
            name: "i15",
            value: Background.winW
        });
        a.push({
            name: "i16",
            value: Background.winH
        });
        Util.exists(HIP) && HIP.data != null && a.push({
            name: "HIPSolution",
            value: HIP.getSolution()
        });
        return a
    },
    SubmitCreds: function () {
        if (!Post.IsSubmitReady()) return;
        $("#cred_password_inputtext").focus();
        var a = [];
        a = Post.GetCredParameters(Constants.SUBMIT_METRICS_ON_POST);
        $.each(a,
		function (b, a) {
		    a.value != undefined && $("<input />").attr("type", "hidden").attr("name", a.name).attr("value", a.value).appendTo("#credentials")
		});
        if (!Constants.HAS_SUBMITTED) {
            Constants.HAS_SUBMITTED = true;
            $("form#credentials").submit()
        }
    },
    SubmitInstrumentation: function () {
        var a = [];
        a = Post.GetCredParameters(Constants.SUBMIT_METRICS_ON_REDIRECT);
        a["login"] = "federated";
        a["passwd"] = "";
        a["HIPSolution"] = "";
        a["LoginOptions"] = LoginOption.NOTHING_CHECKED;
        var b = {};
        $.each(a,
		function (c, a) {
		    if (a.value != undefined) b[a.name] = a.value
		});
        $.ajax({
            url: $("#credentials").attr("action") + "&clientMetrics&federatedDomain=" + Context.federated_domain,
            type: "POST",
            dataType: "json",
            data: b,
            async: true,
            timeout: 200
        })
    }
};
var Post = MSLogin.Post
MSLogin.Support = {
    are_cookies_enabled: function () {
        var b = "testcookie",
		a = navigator.cookieEnabled ? true : false;
        if (typeof navigator.cookieEnabled == "undefined" && !a) {
            document.cookie = b;
            a = document.cookie.indexOf(b) != -1 ? true : false
        }
        return a
    },
    is_placeholder_supported: function () {
        if ($.browser.msie) {
            var a = document.documentMode;
            if (a < 12) return false
        }
        if (navigator.userAgent.indexOf("rv:11.0") >= 0) return false;
        var b = document.createElement("input");
        return "placeholder" in b
    },
    is_prefetch_supported: function () {
        if (navigator.userAgent.match(/iPad/)) return false;
        return true
    },
    is_audio_captcha_supported: function () {
        var b = false,
		c = document.documentMode,
		a = true;
        if ($.browser.msie && c <= 8) a = b;
        else if (navigator.userAgent.match(/IEMobile/)) a = b;
        else if (navigator.userAgent.match(/Android 2\.3/)) a = b;
        else if (navigator.userAgent.match(/iPad/)) a = b;
        else if (navigator.userAgent.match(/iPhone/)) a = b;
        return a
    },
    showClientError: function (d) {
        var c = "#cta_client_error_text",
		a = "#login_cta_text",
		b = "#cta_error_message_text";
        if (d == 0) {
            $(b).hide();
            $(a).hide();
            $(c).hide();
            $(a).show()
        } else {
            $(".client_error_msg").hide();
            $(b).hide();
            $(a).hide();
            $(c).show();
            $(".client_error_msg." + d.toString()).show()
        }
    },
    hideClientErrorMessages: function () {
        var b = "#login_cta_text",
		a = "#cta_error_message_text";
        $(a).hide();
        $(b).hide();
        $("#cta_client_error_text").hide();
        $(".client_error_msg").hide();
        $(a).hide();
        $(b).hide()
    },
    showClientMessage: function (a) {
        $(".cta_message_text").hide();
        $("#cta_client_message_text").show();
        $(".cta_message_text." + a).show()
    },
    hideClientMessages: function () {
        $(".cta_message_text").hide();
        $("#cta_client_message_text").hide()
    },
    validateUsernameCredInput: function (b) {
        var a = /^([^\@]+)\@(.+)$/;
        return a.test(b)
    },
    validateCredInputs: function () {
        var c = "input#cred_password_inputtext",
		b = $("input#cred_userid_inputtext").val(),
		d = $(c).val(),
		a = 30067;
        b = b.toLowerCase();
        if (b == "" && d == "") a = 30067;
        else if (b == "") a = 30067;
        else if (!Support.validateUsernameCredInput(b)) a = 30064;
        else if (d == "") {
            a = 30111;
            $(c).focus()
        } else a = 0;
        Support.showClientError(a);
        if (a) return false;
        else return true
    },
    isUXFeatureEnabled: function (b) {
        var a = false,
		d = parseInt(Util.getCookie(MSLogin.OptIn.optin_cookie_name)),
		c = parseInt(Util.ExtractQSParam("msoptin-newui"));
        if (b & Constants.FEATURE_SLOT_MASK) a = true;
        else if (b & d) a = true;
        if (b & c) a = true;
        if (a && !(b & Constants.FEATURE_SLOT_THRESHOLD)) a = false;
        return a
    },
    forceCredBlockHeight: function () {
        var b = ".login_cred_options_container";
        if (Context.has_forced_cred_block_height || $(b) == undefined) return;
        var a = $(".cred").height();
        a += $(b).height();
        $(".cred").css("height", a);
        Context.has_forced_cred_block_height = true
    },
    renderBrowserSpecificForNarrowWidth: function (b) {
        var j = "padding-left",
		i = ".login_panel",
		h = "input#cred_password_inputtext",
		d = "15px",
		g = "input#cred_userid_inputtext",
		c = "0px",
		f = ".login_cred_field_container",
		e = "span.placeholder",
		a = "margin-left";
        User.resize_bg();
        if (Background.winW < Constants.MOBILE_WIDTH_THRESHOLD) {
            $.browser.msie && b >= 8 && $(".push").css("height", "2em");
            if ($.browser.msie && b <= 8) {
                if (!Constants.IE_MOBILE_CSS_APPLIED) {
                    $("body").append($("<div/>").attr("id", "ie_legacy_container"));
                    $("div#ie_legacy_container").append($("<link/>").attr("href", Constants.CDN_MOBILE_CSS_PATH).attr("rel", "stylesheet").attr("type", "text/css"));
                    Constants.IE_MOBILE_CSS_APPLIED = true
                }
                $("div#cred_userid_container span.input_field").css(a, "-15px");
                $("div#cred_password_container span.input_field").css(a, "-15px");
                $(e).css(a, "10px");
                $(".login_cred_container").css("margin-top", "-20px")
            }
            if ($.browser.msie && b == 8) {
                $("table.tile_link").css(a, "50px");
                $(f).css(a, c);
                $(g).css(a, d);
                $(h).css(a, d);
                $(e).css("left", "-17px");
                $("div.footer").css(a, c)
            }
            if ($.browser.msie && b <= 8);
            if ($.browser.msie && b <= 7) {
                $(i).css(j, c);
                $(f).css(a, c);
                $(g).css(a, d);
                $(h).css(a, d)
            }
            $.browser.msie && b <= 6 && $(i).css(j, "50px")
        }
    },
    renderBrowserSpecificForNarrowHeight: function () {
        var c = "sticky_footer",
		b = "#footer_links_container";
        User.resize_bg();
        if (Background.winH < Constants.MOBILE_HEIGHT_THRESHOLD) {
            var a = true;
            if (navigator.userAgent.match(/IEMobile/)) a = false;
            if (navigator.userAgent.match(/Android/)) a = false;
            if (navigator.userAgent.match(/iPhone/)) a = false;
            if (!a) $(b).removeClass(c);
            else $(b).addClass(c)
        }
    },
    renderBrowserSpecific: function () {
        var i = "#login_panel_center",
		n = "padding-bottom",
		e = ".login_footer_container",
		m = "table.user_tile",
		l = "background-color",
		h = "body",
		d = "margin-left",
		k = ".show_other",
		b = "0px",
		j = ".login_guest_container",
		g = "margin-bottom",
		f = ".cred",
		c = "height",
		a = document.documentMode;
        if ($.browser.msie && a >= 8) {
            $(".push").css("display", "inline-block");
            $(".push").css(c, "6em")
        }
        if (navigator.userAgent.match(/iPad/) || navigator.userAgent.match(/iPhone/)) {
            Support.forceCredBlockHeight();
            $(f).css(g, "-3em");
            $(j).css(g, b)
        }
        $.browser.msie && a > 8 && $(k).css(d, "-5px");
        if ($.browser.msie && a <= 7) {
            $(".ie_legacy").css("display", "none");
            $(h).css(l, $("html").css(l));
            $("a.tile_link").css(d, b);
            $(m).css(d, b);
            $(m).css(g, "10px");
            $(k).css(d, b)
        }
        $.browser.msie && a <= 6 && $(h).css("overflow-y", "hidden");
        $.browser.mozilla && $(".input_border").addClass("high_contrast_border");
        if (navigator.userAgent.match(/IEMobile/));
        if (navigator.userAgent.match(/Android 2\.3/)) {
            $("meta[name=PageID]").attr("content") != "optin.2.0" && $(h).css("font-size", "0.5em");
            $(e).css("margin-top", "-12em");
            $(j).css("padding-top", b);
            $(f).css(n, "10em");
            Support.forceCredBlockHeight()
        }
        if ($.browser.msie && a == 10) {
            $(f).css(n, "100px");
            var o = $(window).height() - $(e).height() + 30;
            $(i).css(c, o + "px");
            document.body.onresize = function () {
                var a = $(window).height() - $(e).height() + 30;
                $(i).css(c, a + "px")
            }
        }
        $.browser.webkit && $("form :input").blur(function () {
            User.resize_bg()
        });
        Support.renderBrowserSpecificForNarrowWidth(a);
        Support.renderBrowserSpecificForNarrowHeight(a)
    },
    getViewport: function () {
        var c = "undefined",
		b,
		a;
        if (typeof window.innerWidth != c) b = window.innerWidth,
		a = window.innerHeight;
        else if (typeof document.documentElement != c && typeof document.documentElement.clientWidth != c && document.documentElement.clientWidth != 0) b = document.documentElement.clientWidth,
		a = document.documentElement.clientHeight;
        else b = document.getElementsByTagName("body")[0].clientWidth,
		a = document.getElementsByTagName("body")[0].clientHeight;
        return [b, a]
    },
    showIllustration: function () {
        var b = false,
		c = Support.getViewport(),
		a = true;
        if (Background.winW < Constants.MOBILE_WIDTH_THRESHOLD) a = b;
        else if (c[0] < Constants.MOBILE_WIDTH_THRESHOLD) a = b;
        else if (navigator.userAgent.match(/IEMobile/)) a = b;
        else if (navigator.userAgent.match(/Android 2\.3/)) a = b;
        else if (Support.isUXFeatureEnabled(512) && Util.ExtractQSParam("popupui") == "1") a = b;
        return a
    },
    showTileTooltips: function () {
        var a = true;
        if (navigator.userAgent.match(/iPad/)) a = false;
        else if (navigator.userAgent.match(/iPhone/)) a = false;
        return a
    },
    renderForVirtualKeyboard: function () {
        var d = "#background_background_image",
		b = "#background_branding_container",
		c = "height",
		a = "#login_panel";
        if (!navigator.userAgent.match(/iPad/)) return;
        $(a).css(c, "100px");
        $(b).css(c, "100px");
        if (Util.ExtractQSParam("popupui") != "1") if (Constants.DIR == "ltr") {
            offset_left = $(window).width() - $(a).offset().left;
            offset_expected = $(window).width() - 500;
            offset_left != offset_expected && $(a).offset({
                left: offset_expected
            })
        } else $(a).offset({
            left: 0
        });
        $(a).offset().top != 0 && $(a).offset({
            top: 0
        });
        $(d).length > 0 && $(d).offset().top != 0 && $(d).offset({
            top: 0
        });
        $(b).length > 0 && $(b).offset().top != 0 && $(b).offset({
            top: 0
        });
        $(a).css(c, $("body").height());
        $(b).css(c, $("body").height())
    }
};
var Support = MSLogin.Support;
jQuery.support.placeholder = function () {
    return Support.is_placeholder_supported()
}();
jQuery.support.cookies = function () {
    var b = "testcookie",
	a = navigator.cookieEnabled ? true : false;
    if (typeof navigator.cookieEnabled == "undefined" && !a) {
        document.cookie = b;
        a = document.cookie.indexOf(b) != -1 ? true : false
    }
    return a
}()
var users = null;
MSLogin.Tiles = {
    tile_showing_user_cred: true,
    users: null,
    cred_length_resized: false,
    updateFirstUserTile: function (a, b) {
        $("#login_user_chooser").html("");
        $.each(users,
		function (e, c) {
		    if (c.login.toLowerCase() == a.toLowerCase() && c.isLive == b) {
		        image_path = c.imageAAD;
		        if (c.isLive) image_path = c.imageMSA;
		        if (image_path == "") image_path = "AD_Glyph.png";
		        image_path = Constants.CDN_IMAGE_PATH + image_path;
		        var d = Tiles.drawTile(c, image_path);
		        e == 0 && $("#" + d).focus();
		        $("#forget_tile_link").attr("href", "javascript:Tiles.forgetUserFromCookie('" + a + "', " + c.isGuest + ");")
		    }
		})
    },
    showUser: function (b, a) {
        if (Tiles.tile_showing_user_cred) return;
        $("#login_user_chooser table").hide();
        $(".login_cta_container").css("visibility", "visible");
        $("#login_cta_text").show();
        $("#tiles_cta_text").hide();
        Tiles.updateFirstUserTile(b, a);
        $("#login_user_chooser table:first").show();
        $.each(users,
		function (d, c) {
		    if (c.login.toLowerCase() == b.toLowerCase() && c.isLive == a) if (c.authUrl == "") Tiles.showManagedTileUser(c);
		    else Tiles.showFederatedTileUser(c)
		});
        Context.back_action_stack.AddAction(Constants.CancelAction.FROM_SHOW_USER_TO_TILES);
        Tiles.tile_showing_user_cred = true
    },
    showManagedTileUser: function (d) {
        var c = "meta[name=PageID]",
		a = "checked",
		b = "#cred_keep_me_signed_in_checkbox";
        Tiles.disableActiveTile();
        $(".login_cred_container").show();
        $(".login_cred_field_container").show();
        $(".login_cred_container li").show();
        $("#cred_userid_inputtext").val(d.login);
        $("#cred_password_container").show();
        $(".login_cred_options_container").show();
        $(b).attr(a, a);
        Context.username_state.enable_guests = false;
        $("#guest_hint_text").hide();
        $(".guest_redirect_container").hide();
        $("#cred_cancel_button").show();
        if (Support.isUXFeatureEnabled(4096) && ($(c).attr("content") == "EmailBasedDiscovery.2.0" || $(c).attr("content") == "EmailBasedDiscoveryTiles.2.0")) {
            $(b).removeAttr(a);
            EmailDiscovery.is_enabled_workflow = true
        }
        User.enablePassword();
        $("#cred_sign_in_button").show();
        $("#cred_password_inputtext").focus();
        User.RefreshDomainState()
    },
    showFederatedTileUser: function (a) {
        var b = "meta[name=PageID]";
        Context.username_state.home_realm_state = Constants.State.FEDERATED;
        Tiles.disableActiveTile();
        $("#cred_userid_inputtext").val(a.login);
        $(".login_cred_field_container").show();
        $(".login_cred_options_container").show();
        $(".login_cred_options_container div.subtext").hide();
        $("#cred_sign_in_button").hide();
        $("#cred_kmsi_container").hide();
        $("#forget_tile_container").show();
        $("#cred_password_container").hide();
        $(".login_cta_container").hide();
        $("#recover_container").hide();
        $("#redirect_cta_text").show();
        $("input#cred_password_inputtext").hide();
        $("#redirect_company_name_text").html("");
        $("div.progress").css("visibility", "visible");
        User.startAnimation();
        Context.animationTid = setInterval(User.startAnimation, 3500);
        Context.username_state.enable_progress_bar = true;
        Context.username_state.enable_redirect = true;
        Context.username_state.enable_guests = false;
        if (Support.isUXFeatureEnabled(4096) && ($(b).attr("content") == "EmailBasedDiscovery.2.0" || $(b).attr("content") == "EmailBasedDiscoveryTiles.2.0")) {
            $("#cred_keep_me_signed_in_checkbox").removeAttr("checked");
            EmailDiscovery.is_enabled_workflow = true
        }
        Util.updateRedirectionMessage(a.authUrl);
        Context.redirect_auth_url = a.authUrl;
        Context.redirect_target = User.addFederatedRedirectQSParameters(a.authUrl);
        User.UsernameOnChangeHandler();
        User.StartRedirection(false)
    },
    disableActiveTile: function () {
        var b = "default",
		a = "cursor",
		d = "#login_user_chooser .tile_link:first",
		c = "disabled_tile";
        $("#login_user_chooser .tile_link:first .user_tile").addClass(c);
        $(d).addClass(c);
        $(d).attr("tabindex", "-1");
        $("#login_user_chooser .tile_link:first tr").css(a, b);
        $(".tile_primary_name").css(a, b);
        $(".tile_secondary_name").css(a, b)
    },
    enableActiveTile: function () {
        var b = "pointer",
		a = "cursor",
		c = "#login_user_chooser .tile_link:first",
		d = "disabled_tile";
        $("#login_user_chooser .tile_link:first .user_tile").removeClass(d);
        $(c).removeClass(d);
        $(c).attr("tabindex", "1");
        $("#login_user_chooser .tile_link:first tr").css(a, b);
        $(".tile_primary_name").css(a, b);
        $(".tile_secondary_name").css(a, b)
    },
    showOtherOption: function () {
        var c = ".login_cred_field_container",
		b = "#cred_userid_inputtext",
		a = true;
        if (Tiles.tile_showing_user_cred) return;
        if (EmailDiscovery.IsTilesModeActivated() || EmailDiscovery.ShouldPerformDiscovery()) {
            Tiles.tile_showing_user_cred = a;
            Context.back_action_stack.AddAction(Constants.CancelAction.FROM_EMAILDISCOVERY_START_TO_TILES);
            EmailDiscovery.Init();
            $(b).val("");
            return
        } else Context.back_action_stack.AddAction(Constants.CancelAction.FROM_SHOW_USER_TO_TILES);
        Context.username_state.enable_progress_bar = a;
        User.UsernameOnChangeHandler();
        $("#cred_cancel_button").show();
        $(".login_cred_container").show();
        $(c).addClass("show_other");
        $(c).show();
        $("#cred_userid_container").show();
        $("#login_user_chooser").hide();
        $(".login_cred_options_container").show();
        $(".login_cta_container").css("visibility", "visible");
        $("#login_cta_text").show();
        $("#tiles_cta_text").hide();
        $("#forget_tile_container").hide();
        $("#cred_sign_in_button").show();
        $("#cred_keep_me_signed_in_checkbox").removeAttr("checked");
        $(b).val("");
        $(b).focus();
        Context.username_state.enable_guests = a;
        Tiles.tile_showing_user_cred = a;
        Support.renderBrowserSpecific()
    },
    CancelTileRedirect: function () {
        var a = ".login_cred_field_container";
        Tiles.enableActiveTile();
        $("#cta_error_message_text").remove();
        if (Constants.DEFAULT_CTA_MESSAGE != "");
        $(a).removeClass("show_other");
        $(a).hide();
        $(".login_cred_options_container").hide();
        $(".login_userid_container").hide();
        $(".login_cred_options_container div.subtext").show();
        $("#create_msa_account_container").hide();
        $("#cred_sign_in_button").show();
        $("#cred_kmsi_container").show();
        $("#forget_tile_container").show();
        Tiles.tile_showing_user_cred = false;
        Support.hideClientErrorMessages();
        User.CancelRedirect();
        $("#login_user_chooser").html("");
        Tiles.drawUsers()
    },
    getStringTileID: function (b) {
        var a;
        if (b.id == undefined) {
            a = b.login.replace("@", "_");
            while (a.indexOf(".") > 0) a = a.replace(".", "_");
            while (a.indexOf(" ") > 0) a = a.replace(" ", "_");
            if (b.isLive) a += "-live";
            a = a.toLowerCase()
        } else a = b.id;
        return a
    },
    drawTile: function (a, v) {
        var u = " .tile_secondary_name",
		t = "smalltext",
		q = "pointer",
		p = "cursor",
		o = "_link",
		n = "href",
		m = "<a/>",
		j = "<div/>",
		i = a.name,
		f = a.login,
		l = a.name,
		w = a.login,
		k = null,
		h = null;
        if (a.isLive) {
            k = Constants.MSA_ACCOUNT_FOR_TEXT + " " + a.login;
            h = Constants.MSA_ACCOUNT_FOR_TEXT
        } else {
            k = Constants.AAD_ACCOUNT_FOR_TEXT + " " + a.login;
            h = Constants.AAD_ACCOUNT_FOR_TEXT
        }
        var b = Tiles.getStringTileID(a);
        if (i.toLowerCase() == f.toLowerCase()) f = "";
        var s = a.ignore_length == undefined || a.ignore_length == false;
        if (s && i.length > Constants.MAX_TILE_TEXT_LENGTH) i = i.substring(0, Constants.MAX_TILE_TEXT_LENGTH) + "...";
        else l = "";
        if (s && f.length > Constants.MAX_TILE_TEXT_LENGTH) f = f.substring(0, Constants.MAX_TILE_TEXT_LENGTH) + "...";
        var g = "",
		e = "",
		d = "",
		c = null;
        if ($("meta[name=PageID]").attr("content").indexOf("reauth") >= 0) {
            g = "";
            e = j;
            d = "alt"
        } else if (Context.email_discovery_mode) {
            g = "javascript:" + a.link + "();";
            e = m;
            d = n;
            if (a.isLive) c = function () {
                Context.email_discovery_easi_user = true;
                Instrument.email_discovery_splitter_choice = EmailDiscovery.WorkflowStates.MSA;
                EmailDiscovery.LoginMSOAccount()
            };
            else c = function () {
                Instrument.email_discovery_splitter_choice = EmailDiscovery.WorkflowStates.AAD;
                EmailDiscovery.LoginAADAccount()
            };
            k = a.name;
            h = a.name
        } else if (a.link == "other") {
            g = "javascript:Tiles.showOtherOption();";
            e = m;
            d = n;
            c = function () {
                Tiles.showOtherOption()
            };
            k = a.name;
            h = a.name
        } else if (a.link != "") {
            g = "javascript:Tiles.showUser('" + a.link + "', " + a.isLive + ");";
            e = m;
            d = n;
            c = function () {
                Tiles.showUser(a.link, a.isLive)
            }
        } else {
            g = "";
            e = j;
            d = "alt"
        }
        $("#login_user_chooser").append($(e).attr("id", b + o).attr(d, g).attr("tabindex", "1").addClass("tile_link").addClass("tooltip").attr("aria-label", k).append($("<table/>").attr("id", b).addClass("user_tile").append($("<tr/>").addClass(b).click(c).css(p, q).append($("<td/>").append($(j).addClass("glyph").append($("<img/>").addClass("ad_glyph").addClass(b).attr("src", v).attr("alt", h)))).append($("<td/>").addClass("tile_name").append($(j).addClass("bigtext").addClass("tile_primary_name").addClass(b).html(i).click(c).css(p, q)).append($(j).addClass(t).addClass("tile_secondary_name").addClass(b).html(f).click(c).css(p, q))))));
        l != "" && Support.showTileTooltips() && $("#" + b + o).append($("<span/>").addClass("tile_link_tooltip").addClass(t).html(l));
        if (Util.isMSA(a.authUrl) && !Context.email_discovery_mode) {
            var r = $("#" + b + u).text();
            r = r + " " + Constants.MSA_LABEL;
            $("#" + b + u).html(r)
        }
        return b + o
    },
    drawUsers: function () {
        $(".login_cta_container").css("visibility", "visible");
        $("#login_cta_text").hide();
        $("#tiles_cta_text").show();
        $(".login_cred_container li").hide();
        $("#login_user_chooser").show();
        $("#cred_userid_container").hide();
        $("#switch_user_container").hide();
        Tiles.drawUserTiles()
    },
    drawUserTiles: function () {
        var a = "";
        $.each(users,
		function (d, b) {
		    a = b.imageAAD;
		    if (b.isLive) a = b.imageMSA;
		    if (a == "") a = "AD_Glyph.png";
		    a = Constants.CDN_IMAGE_PATH + a;
		    var c = "";
		    if (b.link == "other" || d < Constants.MAX_USER_TILES || Context.email_discovery_mode) c = Tiles.drawTile(b, a);
		    c != "" && d == 0 && $("#" + c).focus()
		});
        Tiles.tile_showing_user_cred = false;
        Context.username_state.enable_guests = true;
        User.moveFooterToBottom();
        if (!Tiles.cred_length_resized) {
            var b = $(".cred").height();
            b += $(".login_user_chooser").height();
            $(".cred").css("height", b);
            Tiles.cred_length_resized = true
        }
        Support.renderBrowserSpecific()
    },
    forgetAccount: function () {
        Tiles.tile_showing_user_cred = false;
        $("#forget_tile_container").hide();
        $("#cred_keep_me_signed_in_checkbox").removeAttr("checked");
        $(".login_cta_container").css("visibility", "visible");
        $("#login_cta_text").show();
        $("#tiles_cta_text").hide();
        Util.eraseCookie("MSPPre");
        Tiles.CancelTileRedirect();
        Tiles.showOtherOption();
        $("#cred_cancel_button").hide()
    },
    forceSignIn: function (e) {
        var b = "checked",
		d = "#cred_keep_me_signed_in_checkbox";
        Tiles.disableActiveTile();
        Tiles.tile_showing_user_cred = false;
        $("#login_cta_text").show();
        $("#tiles_cta_text").hide();
        $("#login_user_chooser table").hide();
        $("#login_user_chooser table:first").show();
        $("#switch_user_container").show();
        var a = null;
        $.each(users,
		function (c, b) {
		    if (b.login.toLowerCase() == e.toLowerCase() && b.isLive == false) {
		        Tiles.showManagedTileUser(b);
		        a = b
		    }
		});
        $("#cred_cancel_button").hide();
        $("#forget_tile_container").hide();
        var c = false;
        a != null && a.isLive != null && Tiles.ApplyToEachSavedUsers(function (b) {
            if (b[0].length != 0 && b[0].toLowerCase() == e.toLowerCase() && b[1] == (a.isLive ? "1" : "0")) c = true
        });
        if (c) $(d).attr(b, b);
        else $(d).removeAttr(b);
        $(".login_cta_container").show()
    },
    hasUserNameQS: function () {
        var a = Util.ExtractQSParam("username");
        a = decodeURIComponent(a);
        if (a == "") return false;
        return true
    },
    showUserName: function () {
        var c = "#cred_userid_inputtext",
		a = Util.ExtractQSParam("username");
        a = decodeURIComponent(a);
        var d = null,
		b = false;
        $.each(Tiles.users,
		function (e, c) {
		    if (c.login.toLowerCase() == a.toLowerCase() && !b) {
		        b = true;
		        d = c
		    }
		});
        if (b) {
            Tiles.showUser(a, d.isLive);
            $("#cred_password_inputtext").focus()
        } else {
            Tiles.showOtherOption();
            $(c).val(a);
            $(c).focus()
        }
    },
    showIsNotMe: function (b, c) {
        var a = ".login_cta_container";
        Tiles.drawUsers();
        Tiles.showUser(b, c);
        $(".login_cred_container li").hide();
        $("#login_user_chooser").show("slow");
        $(a).show();
        $(a).css("visibility", "visible");
        $(".login_cred_options_container").show();
        $(".login_cred_options_container div.subtext").hide();
        $("#submit_button").hide();
        $("#continue_as_user_container").show();
        $("#continue_as_user_link").focus();
        $("#switch_user_container").show()
    },
    updateContinueAsUserUrl: function (d) {
        var c = "href",
		b = "#continue_as_user_link",
		a = $(b).attr(c);
        a = a.replace("post.srf", "secure.srf");
        a = a.replace(new RegExp("username=[^\\&]*", "gm"), "");
        if (a.indexOf("msoptin-newui") < 0) a += "&msoptin-newui";
        $(b).attr(c, a);
        a = Util.ExtractQSParam("ru");
        if (a != "") {
            a = d;
            $(b).attr(c, a)
        }
    },
    forgetUserFromCookie: function (d, f) {
        Tiles.tile_showing_user_cred = false;
        $("#forget_tile_container").hide();
        $("#cred_keep_me_signed_in_checkbox").removeAttr("checked");
        $(".login_cta_container").css("visibility", "visible");
        $("#login_cta_text").show();
        $("#tiles_cta_text").hide();
        $("#login_userid_inputtext").val("");
        $("#login_password_inputtext").val("");
        Tiles.CancelTileRedirect();
        var b = [];
        Tiles.ApplyToEachSavedUsers(function (a) {
            if (a[0].length != 0 && a[0].toLowerCase() == d.toLowerCase() && a[1] == Number(f).toString()) return;
            else b.push([a[0], a[1], a[2]])
        });
        var e = Util.getCookie(Constants.PREFILL_USER_COOKIE);
        if (b.length == 0) {
            Util.eraseCookie(Constants.SAVED_USER_COOKIE);
            if (EmailDiscovery.IsTilesModeActivated()) {
                Context.email_discovery_tiles_mode = false;
                Context.email_discovery_mode = true
            }
            Tiles.showOtherOption();
            $("#login_user_chooser").hide();
            Context.back_action_stack.ClearCancelActions();
            $("#cred_cancel_button").hide();
            e != null && e.indexOf(d) >= 0 && Util.eraseCookie(Constants.PREFILL_USER_COOKIE);
            return
        }
        var c = Constants.SAVED_USER_COOKIE_INFO_DELIMITER,
		a = "";
        $.each(b,
		function (d, b) {
		    if (d) a += Constants.SAVED_USER_COOKIE_USER_DELIMITER;
		    a += b[0] + c + b[1] + c + b[2]
		});
        Util.setCookie(Constants.SAVED_USER_COOKIE, a, 30);
        Util.eraseCookie(Constants.PREFILL_USER_COOKIE);
        location.reload()
    },
    ApplyToEachSavedUsers: function (b) {
        var a = Util.getCookie(Constants.SAVED_USER_COOKIE),
		d = Constants.SAVED_USER_COOKIE_USER_DELIMITER,
		c = Constants.SAVED_USER_COOKIE_INFO_DELIMITER;
        if (a == undefined || a == null || a == "") return;
        $.each(a.split(d),
		function (e, a) {
		    if (a == undefined || a == null || a == "") return;
		    var d = a.split(c);
		    if (d.length != 3) return;
		    else if (d[0].length == 0) return;
		    b(d)
		})
    }
};
var Tiles = MSLogin.Tiles
HIP_MODE = {
    VISUAL: 0,
    AUDIO: 1
};
$("#hip_code_inputtext").focus(function () {
    HIP.startTime = new Date
});
$("#hip_code_inputtext").blur(function () {
    HIP.endTime = new Date
});
var HIP = {
    data: null,
    mode: HIP_MODE.VISUAL,
    startTime: new Date,
    endTime: new Date,
    getChallengeScript: function () {
        var a = HIP.data.challengeURL;
        a += "?k=" + HIP.data.captchaKey;
        a += "&ajax=1";
        a += "&" + HIP.data.challengeExtraParams;
        $("#hip_script_container").append($("<script/>").attr("src", a));
        return a
    },
    getToken: function () {
        return HIP.data.visualToken
    },
    getStreamURL: function () {
        if (HIP.mode == HIP_MODE.VISUAL) return HIP.data.visualImageURL;
        else if (HIP.mode == HIP_MODE.AUDIO) return HIP.data.audioStreamURL
    },
    hideVisual: function () {
        $("#hip_show_image_container").hide();
        $("#hip_show_audio_container").show();
        HIP.mode = HIP_MODE.VISUAL;
        HIP.getNextValue()
    },
    hideAudio: function () {
        $("#hip_show_image_container").show();
        $("#hip_show_audio_container").hide();
        HIP.mode = HIP_MODE.AUDIO;
        HIP.getNextValue()
    },
    createPlayer: function () {
        var a = "controls";
        $("#hip_image_container").html($("<audio/>").attr(a, a).attr("id", "hip_audio_control").append($("<source/>").attr("src", HIP.getStreamURL()).attr("type", "audio/wav")))
    },
    playPlayer: function () {
        $("#hip_audio_control").play()
    },
    stopPlayer: function () {
        $("#hip_audio_control").stop()
    },
    getChallenge: function (c) {
        for (var b, a = 0; a < c; a++) b = Util.getCookie("HIPChallenge" + a);
        return b
    },
    getSolution: function () {
        var a = "Fr=";
        a += ",type=";
        if (HIP.mode == HIP_MODE.VISUAL) a += "visual";
        else if (HIP.mode == HIP_MODE.AUDIO) a += "audio";
        a += ",Solution=" + $("#hip_code_inputtext").val();
        a += ",HIPFrame=" + HIP.getToken();
        a += "|HIPChallenge0=" + HIP.getChallenge(parseInt(HIP.data.visualNumber));
        a += "|HIPTime=" + (HIP.endTime - HIP.startTime);
        return a
    },
    refresh: function () {
        var a = "#hip_challenge_image";
        if (HIP.data == null) return;
        if (HIP.mode == HIP_MODE.VISUAL) {
            $("#hip_image_container").html($("<img/>").attr("id", "hip_challenge_image").attr("alt", Constants.HIP_IMAGE_ALT_TEXT).attr("src", HIP.getStreamURL()));
            $(a).css("width", HIP.data.visualImageWidth);
            $(a).css("height", HIP.data.visualImageHeight)
        } else if (HIP.mode == HIP_MODE.AUDIO) return HIP.createPlayer()
    },
    getNextValue: function () {
        var a = Constants.HIP_DATA_URL;
        a = a.replace("gethip.srf", "hip_data.srf");
        if (HIP.mode == HIP_MODE.AUDIO) a = a.replace("visual", "audio");
        $("#hip_script_container").append($("<script/>").attr("type", "text/javascript").attr("src", a));
        HIP.data != null && $("#hip_help_link").attr("href", HIP.data.helpLink)
    }
};
$(document).ready(function () {
    var d = "#hip_show_image_link",
	c = "#hip_show_audio_link",
	a = "href";
    $("#hip_refresh_link").attr(a, "javascript:HIP.getNextValue();");
    if (Support.is_audio_captcha_supported()) {
        $(c).attr(a, "javascript:HIP.hideAudio();");
        $(d).attr(a, "javascript:HIP.hideVisual();")
    } else {
        $(c).hide();
        $(d).hide();
        $("#hip_divider_text").hide()
    }
    var b = $(".cred").height();
    b += $(".login_cred_options_container").height();
    $(".cred").css("height", b)
})
MSLogin.OptIn = {
    optin_cookie_name: "MSOptIn",
    optin_cookie_value: "newui",
    optin_button_id: "newui_optin_button",
    optin_label_id: "newui_optin_label",
    opted_in_button_text: "Opt out",
    opted_out_button_text: "Opt in",
    exp_container_label: "experiment_container",
    exp_status: 0,
    optInNewUI: function () {
        Util.setCookie("MSOptIn", MSLogin.OptIn.optin_cookie_value, 60);
        $("#" + MSLogin.OptIn.optin_button_id).html(MSLogin.OptIn.opted_in_button_text);
        $("#" + MSLogin.OptIn.optin_button_id).click(MSLogin.OptIn.optOutNewUI)
    },
    optOutNewUI: function () {
        Util.setCookie(MSLogin.OptIn.optin_cookie_name, "", 0);
        $("#" + MSLogin.OptIn.optin_label_id).html(MSLogin.OptIn.opted_out_label_text);
        $("#" + MSLogin.OptIn.optin_button_id).html(MSLogin.OptIn.opted_out_button_text);
        $("#" + MSLogin.OptIn.optin_button_id).click(MSLogin.OptIn.optInNewUI)
    },
    saveStatus: function () {
        Util.setCookie(MSLogin.OptIn.optin_cookie_name, MSLogin.OptIn.exp_status, 60)
    },
    toggleButton: function (b) {
        var a = b.split("_")[1];
        $.each(Constants.OPTIN_FEATURE_EXPERIMENTS,
		function (b) {
		    if (b == a) {
		        var d = (parseInt(b) & MSLogin.OptIn.exp_status) > 0,
				c = MSLogin.OptIn.opted_out_button_text;
		        if (d) {
		            MSLogin.OptIn.exp_status = MSLogin.OptIn.exp_status & ~b;
		            c = MSLogin.OptIn.opted_out_button_text
		        } else {
		            MSLogin.OptIn.exp_status = MSLogin.OptIn.exp_status | b;
		            c = MSLogin.OptIn.opted_in_button_text
		        }
		        $("#optin_" + b).html(c);
		        MSLogin.OptIn.saveStatus()
		    }
		})
    },
    drawExperiment: function (b, e) {
        var c = "<br/>";
        if (b == null || b == undefined) return;
        var b = parseInt(b),
		f = (b & MSLogin.OptIn.exp_status) > 0,
		a = $("#" + MSLogin.OptIn.exp_container_label);
        a.append($("<div/>").addClass("bigtext").html(e[0]));
        a.append($("<div/>").addClass("status").addClass("normaltext").html(e[1]));
        var d = MSLogin.OptIn.opted_out_button_text;
        if (f) d = MSLogin.OptIn.opted_in_button_text;
        a.append($("<span/>").addClass("button").addClass("bigtext").attr("id", "optin_" + b).attr("tabindex", "1").html(d).click(function () {
            MSLogin.OptIn.toggleButton("optin_" + b)
        }));
        a.append($(c));
        a.append($(c));
        a.append($(c))
    },
    init: function () {
        if ($("meta[name=PageID]").attr("content") != "optin.2.0") return;
        User.UpdateBackground("", "");
        MSLogin.OptIn.exp_status = parseInt(Constants.FEATURE_SLOT_MASK);
        var a = parseInt(Util.getCookie(MSLogin.OptIn.optin_cookie_name));
        MSLogin.OptIn.exp_status = MSLogin.OptIn.exp_status | a;
        MSLogin.OptIn.drawOptinPanel();
        $.each(Constants.OPTIN_FEATURE_EXPERIMENTS,
		function (b, a) {
		    MSLogin.OptIn.drawExperiment(b, a)
		});
        $(".cred").css("padding-top", "20px");
        $(".cred").css("height", $(".login_cred_options_container").height());
        $(".login_inner_container").css("display", "inline-block")
    },
    drawOptinPanel: function () {
        var a = "<div/>",
		b = $(a).addClass("subtext").attr("id", "experiment_container");
        $.each(MSLogin.OptIn.optin_messaging,
		function (d, c) {
		    b.append($(a).addClass("bigtext").html(c.headline));
		    b.append($(a).addClass("status normaltext").html(c.text));
		    if (navigator.userAgent.match(/Android 2\.3/)) return false
		});
        b.append($(a).attr("id", MSLogin.OptIn.exp_container_label));
        $(".login_cred_options_container").html(b)
    },
    optin_messaging: [{
        headline: "Windows Azure AD Sign-in Enhancements",
        text: "The Windows Azure AD team is continuously working to improve the sign-in experience to Office 365, Windows Azure and other Microsoft services used by organizations."
    },
	{
	    headline: "",
	    text: "From time to time, new features or capabilities may be released in Preview, allowing you to try them out and provide feedback before they become part of the mainline experience. These features are safe to try (you can easily revert to the mainline experience by opting out) but are not supported as part of the production service."
	},
	{
	    headline: "How to test the new sign-in features",
	    text: "Features that are available in Preview are listed below. You will need to opt in from each device you want to test the features with."
	}]
};
$(document).ready(function () {
    MSLogin.OptIn.init()
})
MSLogin.EmailDiscovery = {
    Strings: {},
    WorkflowStates: {
        NONE: 0,
        INIT: 1,
        LOOKING: 2,
        SPLITTER: 3,
        MSA: 4,
        AAD: 5
    },
    is_enabled_workflow: false,
    IsDiscoveryPage: function () {
        return Support.isUXFeatureEnabled(32) && EmailDiscovery.is_enabled_workflow
    },
    ShouldPerformDiscovery: function () {
        return Support.isUXFeatureEnabled(32) && Context.email_discovery_mode
    },
    IsTilesModeActivated: function () {
        return Support.isUXFeatureEnabled(32) && Context.email_discovery_tiles_mode
    },
    Init: function () {
        var a = "input#cred_userid_inputtext";
        Context.email_discovery_mode = true;
        if (EmailDiscovery.ShouldPerformDiscovery()) EmailDiscovery.is_enabled_workflow = true;
        $("#cred_sign_in_button").hide();
        $("#cred_cancel_button").hide();
        $("#cred_kmsi_container").hide();
        $("#recover_container").hide();
        $("#cred_password_container").hide();
        $("#login_cta_text").hide();
        $("#login_user_chooser").hide();
        $("#forget_tile_container").hide();
        $("#guest_hint_text").hide();
        $(".guest_redirect_container").hide();
        $(".login_cta_container").show();
        $(".login_cred_options_container").addClass("email_discovery");
        $("#cred_keep_me_signed_in_checkbox").removeAttr("checked");
        $("#looking_container").hide();
        $("div.progress").css("visibility", "hidden");
        Context.username_state.last_checked_email = "";
        Context.username_state.home_realm_state = Constants.State.NONE;
        Context.email_discovery_workflow_state = EmailDiscovery.WorkflowStates.NONE;
        var c = decodeURIComponent(Util.ExtractQSParam("username")),
		b = $("#cred_userid_inputtext").val();
        if (EmailDiscovery.IsTilesModeActivated() && c != b) {
            $(a).val("");
            $(a).focus()
        }
        EmailDiscovery.HideSplitter();
        EmailDiscovery.ShowEmailDiscovery()
    },
    Initialize: function () {
        $("input#cred_userid_inputtext").keydown(function (a) {
            EmailDiscovery.ShouldPerformDiscovery() && User.KeyPressEnter(a,
			function () {
			    a.preventDefault();
			    EmailDiscovery.ValidateUserInput()
			},
			13)
        });
        $("#cred_continue_button").keydown(function (a) {
            EmailDiscovery.ShouldPerformDiscovery() && User.KeyPressEnter(a,
			function () {
			    a.preventDefault();
			    EmailDiscovery.ValidateUserInput();
			    User.RefreshDomainState()
			},
			13)
        });
        $("#cred_try_again_button").keydown(function (a) {
            EmailDiscovery.ShouldPerformDiscovery() && User.KeyPressEnter(a,
			function () {
			    a.preventDefault();
			    EmailDiscovery.ValidateUserInput();
			    User.RefreshDomainState()
			},
			13)
        });
        $("a#looking_cancel_link").attr("href", "javascript:Context.back_action_stack.TriggerCancelAction();")
    },
    SetupTilesForSplitter: function () {
        if (users == Constants.EMAIL_DISCOVERY_DEFAULT_TILES) return;
        Context.email_discovery_splitter_shown = true;
        Context.user_tiles = Tiles.users;
        Tiles.users = Constants.EMAIL_DISCOVERY_DEFAULT_TILES;
        users = Tiles.users;
        $.each(users,
		function (b, a) {
		    a.ignore_length = true
		})
    },
    RevertUserTiles: function () {
        if (users != Constants.EMAIL_DISCOVERY_DEFAULT_TILES) return;
        Context.email_discovery_splitter_shown = false;
        Tiles.users = Context.user_tiles;
        users = Tiles.users
    },
    ShowPostErrorMessage: function () {
        Context.email_discovery_mode = false;
        EmailDiscovery.is_enabled_workflow = true;
        Context.email_discovery_splitter_shown = false;
        $("meta[name=PageID]").attr("content").indexOf("EmailBasedDiscoveryTiles") == -1 && Context.back_action_stack.AddAction(Constants.CancelAction.FROM_MANY_TO_EMAILDISCOVERY_INIT);
        $("#cred_cancel_button").show()
    },
    ValidateUserInput: function () {
        var a = $("input#cred_userid_inputtext").val().toLowerCase();
        User.UpdateUsernameState();
        if (Context.username_state.is_empty) {
            Context.email_discovery_workflow_state = EmailDiscovery.WorkflowStates.NONE;
            EmailDiscovery.ShowEmailDiscovery(30127)
        } else if (Context.username_state.is_partial) {
            Context.email_discovery_workflow_state = EmailDiscovery.WorkflowStates.NONE;
            EmailDiscovery.ShowEmailDiscovery(30145)
        }
    },
    PerformEmailBasedDiscovery: function () {
        var a = $("input#cred_userid_inputtext").val().toLowerCase();
        a = $.trim(a);
        Context.email_discovery_timeout_occurred = false;
        Context.email_discovery_error_message = null;
        if (!Support.validateUsernameCredInput(a)) {
            Context.email_discovery_workflow_state = EmailDiscovery.WorkflowStates.NONE;
            EmailDiscovery.ShowEmailDiscovery(30064);
            return
        }
        Context.email_discovery_lookup_xhr = $.ajax({
            url: Constants.EMAIL_DISCOVERY_SERVICE_URI,
            dataType: "json",
            data: {
                login: encodeURI(a),
                ismsa: "1"
            },
            success: EmailDiscovery.ReceiveHomeRealmInfo,
            error: EmailDiscovery.ReceiveHomeRealmError,
            timeout: Constants.EMAIL_DISCOVERY_SERVICE_TIMEOUT
        });
        Context.username_state.last_checked_email = a;
        Context.username_state.home_realm_state = Constants.State.PENDING;
        MSLogin.EmailDiscovery.HideEmailDiscovery();
        MSLogin.EmailDiscovery.ShowLookingForAccountLayout()
    },
    ReceiveHomeRealmInfo: function (a) {
        Context.email_discovery_response = a;
        EmailDiscovery.HideLookingForAccountLayout();
        Context.back_action_stack.RemoveLastCancelAction();
        var b = EmailDiscovery.GetAccountStateFromResponse(a);
        EmailDiscovery.PerformUIAction(b)
    },
    GetAccountStateFromResponse: function (d) {
        var g = "TIMED_OUT",
		a = null,
		c = Constants.EmailDiscoveryAccountState;
        if (d == a || d.MSA == a || d.AAD == a) {
            Context.username_state.home_realm_state = Constants.State.INVALID;
            return c.ERROR
        }
        var f = d.MSA,
		i = d.AAD,
		e = "",
		b = "";
        if (f == a || f.res == a || !(f.error == undefined)) e = g;
        else {
            e = EmailDiscovery.GetEnumKeyName(Constants.MSAccount, f.res);
            if (e == a) return c.ERROR
        }
        if (i == "timeout") {
            b = g;
            Context.username_state.home_realm_state = Constants.State.INVALID
        } else {
            b = EmailDiscovery.GetEnumKeyName(Constants.NameSpaceType, i.NameSpaceType);
            if (b == a) {
                Context.username_state.home_realm_state = Constants.State.INVALID;
                return c.ERROR
            }
            Context.username_state.home_realm_state = Constants.State[b]
        }
        var j = "AAD_" + b + "_AND_MSA_" + e,
		h = c[j];
        if (h == undefined) return c.Error;
        return h
    },
    PerformUIAction: function (e) {
        var c = true,
		d = "#create_msa_account_container",
		a = Constants.EmailDiscoveryAccountState,
		b = Context.email_discovery_response,
		f = Constants.EMAIL_DISCOVERY_DEFAULT_TILES;
        Context.email_discovery_easi_user = false;
        Context.email_discovery_splitter_shown = false;
        Instrument.email_discovery_ui_code = e;
        switch (e) {
            case a.AAD_UNKNOWN_AND_MSA_NOT_EXIST:
                $(d).show();
                EmailDiscovery.ShowEmailDiscovery(30146);
                break;
            case a.AAD_UNKNOWN_AND_MSA_EXISTS:
                Context.email_discovery_easi_user = c;
                EmailDiscovery.LoginMSOAccount();
                break;
            case a.AAD_FEDERATED_AND_MSA_NOT_EXIST:
                if (Util.isMSA(b.AAD.AuthURL)) {
                    $(d).show();
                    EmailDiscovery.ShowEmailDiscovery(30146);
                    break
                }
            case a.AAD_MANAGED_AND_MSA_NOT_EXIST:
                EmailDiscovery.LoginAADAccount();
                break;
            case a.AAD_FEDERATED_AND_MSA_EXISTS:
                if (Util.isMSA(b.AAD.AuthURL)) {
                    EmailDiscovery.LoginMSOAccount();
                    break
                }
                EmailDiscovery.SetupTilesForSplitter();
                Context.redirect_auth_url = b.AAD.AuthURL;
                Context.redirect_target = User.addFederatedRedirectQSParameters(b.AAD.AuthURL);
                Context.federated_domain = b.AAD.DomainName;
                users[0].authUrl = b.AAD.AuthURL;
            case a.AAD_MANAGED_AND_MSA_EXISTS:
                Context.back_action_stack.AddAction(Constants.CancelAction.FROM_EMAILDISCOVERY_SPLITTER_TO_EMAILDISCOVERY_START);
                EmailDiscovery.ShowSplitter();
                break;
            case a.AAD_FEDERATED_AND_MSA_THROTTLED:
            case a.AAD_FEDERATED_AND_MSA_TIMED_OUT:
                EmailDiscovery.SetupTilesForSplitter();
                Context.redirect_auth_url = b.AAD.AuthURL;
                Context.redirect_target = User.addFederatedRedirectQSParameters(b.AAD.AuthURL);
                Context.federated_domain = b.AAD.DomainName;
                users[0].authUrl = b.AAD.AuthURL;
                Context.email_discovery_timeout_occurred = c;
                Context.back_action_stack.AddAction(Constants.CancelAction.FROM_EMAILDISCOVERY_SPLITTER_TO_EMAILDISCOVERY_START);
                EmailDiscovery.ShowSplitter();
                break;
            case a.AAD_MANAGED_AND_MSA_THROTTLED:
            case a.AAD_MANAGED_AND_MSA_TIMED_OUT:
                Context.email_discovery_timeout_occurred = c;
                Context.back_action_stack.AddAction(Constants.CancelAction.FROM_EMAILDISCOVERY_SPLITTER_TO_EMAILDISCOVERY_START);
                EmailDiscovery.ShowSplitter();
                break;
            case a.AAD_TIMED_OUT_AND_MSA_THROTTLED:
            case a.AAD_TIMED_OUT_AND_MSA_TIMED_OUT:
            case a.AAD_UNKNOWN_AND_MSA_THROTTLED:
            case a.AAD_UNKNOWN_AND_MSA_TIMED_OUT:
            case a.AAD_TIMED_OUT_AND_MSA_NOT_EXIST:
            case a.ERROR:
                $(d).show();
            case a.AAD_TIMED_OUT_AND_MSA_EXISTS:
            default:
                Context.email_discovery_timeout_occurred = c;
                if (Context.email_discovery_use_msa_api) Context.back_action_stack.AddAction(Constants.CancelAction.FROM_EMAILDISCOVERY_SPLITTER_TO_EMAILDISCOVERY_START);
                else EmailDiscovery.IsTilesModeActivated() && Context.back_action_stack.AddAction(Constants.CancelAction.FROM_EMAILDISCOVERY_SPLITTER_FALLBACK_TO_TILES);
                EmailDiscovery.ShowSplitter()
        }
        if (Context.email_discovery_easi_user) Instrument.possible_easi_user = 1
    },
    ReceiveHomeRealmError: function (c, a) {
        if (a == "abort") {
            EmailDiscovery.HideLookingForAccountLayout();
            EmailDiscovery.ShowEmailDiscovery()
        } else EmailDiscovery.HandleTimeoutHomeRealmInfo()
    },
    HandleTimeoutHomeRealmInfo: function () {
        Context.email_discovery_response = null;
        Context.email_discovery_timeout_occurred = true;
        Context.username_state.home_realm_state = Constants.State.INVALID;
        EmailDiscovery.HideLookingForAccountLayout();
        EmailDiscovery.PerformUIAction(Constants.EmailDiscoveryAccountState.ERROR)
    },
    ShowEmailDiscovery: function (a) {
        var f = "#cred_continue_button",
		e = "#cred_try_again_button",
		d = ".login_cred_options_container",
		c = "#cred_userid_inputtext";
        if (Context.email_discovery_workflow_state == EmailDiscovery.WorkflowStates.INIT) return;
        Context.email_discovery_workflow_state = EmailDiscovery.WorkflowStates.INIT;
        if (!Context.email_discovery_use_msa_api) {
            EmailDiscovery.HideEmailDiscovery();
            EmailDiscovery.HandleTimeoutHomeRealmInfo();
            EmailDiscovery.ShowSplitter();
            return
        }
        var b = false;
        Support.hideClientErrorMessages();
        Support.hideClientMessages();
        Context.username_state.home_realm_state = Constants.State.NONE;
        Context.username_state.last_checked_email = "";
        if (a == undefined) Support.showClientMessage(30136);
        else {
            Support.showClientError(a);
            if (a == 30146) b = true
        }
        $("#forget_tile_container").hide();
        Context.back_action_stack.DoesCancelActionExist() && $("#cred_cancel_button").show();
        $("#cred_userid_container").show();
        $(c).show();
        $(".login_cred_field_container").show();
        $(d).show();
        $(d).addClass("email_discovery");
        $(c).focus();
        if (b) {
            $(e).show();
            $(f).hide()
        } else {
            $(f).show();
            $(e).hide()
        }
        $("#cred_password_container").hide();
        $("#recover_container").hide()
    },
    HideEmailDiscovery: function () {
        $("#cred_userid_container").hide();
        $("#cred_continue_button").hide();
        $("#cred_cancel_button").hide();
        $("#cred_try_again_button").hide();
        $("#create_msa_account_container").hide();
        Support.hideClientErrorMessages();
        Support.hideClientMessages();
        Context.username_state.enable_redirect = false;
        Context.username_state.enable_guests = false;
        User.UsernameOnChangeHandler()
    },
    BackToUserTiles: function () {
        EmailDiscovery.HideEmailDiscovery();
        Context.email_discovery_mode = false;
        EmailDiscovery.RevertUserTiles();
        Tiles.drawUsers();
        $("#tiles_cta_text").show();
        $(".login_cred_options_container").removeClass("email_discovery");
        $("#create_msa_account_container").hide();
        Context.username_state.home_realm_state = Constants.State.NONE;
        Tiles.CancelTileRedirect()
    },
    ShowLookingForAccountLayout: function () {
        if (Context.email_discovery_workflow_state == EmailDiscovery.WorkflowStates.LOOKING) return;
        Context.email_discovery_workflow_state = EmailDiscovery.WorkflowStates.LOOKING;
        Context.back_action_stack.AddAction(Constants.CancelAction.FROM_EMAILDISCOVERY_LOOKING_FOR_ACCOUNT_TO_EMAILDISCOVERY_START);
        $("#looking_container").show();
        $("a#looking_cancel_link").focus();
        $("div.progress").css("visibility", "visible");
        User.startAnimation();
        Context.animationTid = setInterval(User.startAnimation, 3500)
    },
    HideLookingForAccountLayout: function () {
        $("#looking_container").hide();
        $("div.progress").css("visibility", "hidden");
        clearInterval(Context.animationTid)
    },
    CancelLookingForAccount: function () {
        Context.email_discovery_lookup_xhr.abort()
    },
    ShowSplitter: function () {
        var a = ".login_cred_options_container";
        if (Context.email_discovery_workflow_state == EmailDiscovery.WorkflowStates.SPLITTER) return;
        Context.email_discovery_workflow_state = EmailDiscovery.WorkflowStates.SPLITTER;
        Context.email_discovery_splitter_shown = true;
        EmailDiscovery.SetupTilesForSplitter();
        $(".login_cta_container").show();
        $("#login_user_chooser").show();
        $(".login_user_chooser").show();
        Tiles.drawUsers();
        Context.back_action_stack.DoesCancelActionExist() && $("#cred_cancel_button").show();
        if (!Context.email_discovery_use_msa_api) {
            !EmailDiscovery.IsTilesModeActivated() && $(a).removeClass("email_discovery");
            $(a).css("display", "block");
            $("#create_msa_account_container").show();
            Support.showClientMessage("30173")
        } else {
            $(a).css("display", "block");
            if (Context.email_discovery_timeout_occurred) Support.showClientMessage(30140);
            else {
                var b = $("input#cred_userid_inputtext").val().toLowerCase();
                b = $.trim(b);
                var c = Constants.TokenizedStringMsgs.UPN_DISAMBIGUATE_MESSAGE.replace("#~#MemberName_LS#~#", b);
                $("#upn_needs_disambiguation_text").text(c);
                Support.showClientMessage(30139)
            }
        }
    },
    HideSplitter: function () {
        var a = "#login_user_chooser";
        $("#cred_cancel_button").hide();
        $(a).empty();
        $(".login_user_chooser").hide();
        $(a).hide();
        $("#create_msa_account_container").hide();
        Support.hideClientErrorMessages();
        Support.hideClientMessages()
    },
    BackSplitter: function () {
        EmailDiscovery.HideSplitter();
        EmailDiscovery.ShowEmailDiscovery()
    },
    LoginMSOAccount: function () {
        if (Context.email_discovery_workflow_state == EmailDiscovery.WorkflowStates.MSA) return;
        Context.email_discovery_workflow_state = EmailDiscovery.WorkflowStates.MSA;
        var a = {
            DomainName: "LIVE.COM",
            AuthURL: Constants.MSA_AUTH_URL
        };
        EmailDiscovery.HideSplitter();
        EmailDiscovery.InitRedirect();
        User.ReceiveFederatedDomain(a)
    },
    LoginAADAccount: function () {
        if (Context.email_discovery_workflow_state == EmailDiscovery.WorkflowStates.AAD) return;
        Context.email_discovery_workflow_state = EmailDiscovery.WorkflowStates.AAD;
        EmailDiscovery.HideSplitter();
        if (!Context.email_discovery_use_msa_api || (Context.email_discovery_timeout_occurred || Context.email_discovery_response == null) && Context.username_state.home_realm_state == Constants.State.INVALID) {
            EmailDiscovery.ShowAADLoginLayout();
            Context.username_state.home_realm_state = Constants.State.NONE;
            $("#cred_userid_inputtext").focus()
        } else if (Context.username_state.home_realm_state == Constants.State.FEDERATED) {
            if (Context.email_discovery_timeout_occurred && !Util.isMSA(Context.email_discovery_response.AAD.AuthURL)) Context.email_discovery_easi_user = false;
            EmailDiscovery.InitRedirect();
            User.ReceiveFederatedDomain(Context.email_discovery_response.AAD)
        } else {
            EmailDiscovery.ShowAADLoginLayout();
            User.ReceiveManagedDomain(Context.email_discovery_response.AAD)
        }
    },
    InitRedirect: function () {
        var a = ".login_cred_options_container";
        $("input#cred_userid_inputtext").hide();
        $("#cred_userid_container").show();
        $(".login_cred_field_container").show();
        $(a).show();
        $("#cred_kmsi_container").show();
        if (Context.email_discovery_splitter_shown) Context.back_action_stack.AddAction(Constants.CancelAction.FROM_EMAILDISCOVERY_REDIRECT_TO_EMAILDISCOVERY_SPLITTER);
        else Context.back_action_stack.AddAction(Constants.CancelAction.FROM_EMAILDISCOVERY_REDIRECT_TO_EMAILDISCOVERY_START);
        $("div.progress").css("visibility", "visible");
        $(a).removeClass("email_discovery");
        User.startAnimation();
        Context.animationTid = setInterval(User.startAnimation, 3500)
    },
    ShowAADLoginLayout: function () {
        var a = ".login_cred_options_container";
        Context.email_discovery_mode = false;
        if (Context.email_discovery_splitter_shown) Context.back_action_stack.AddAction(Constants.CancelAction.FROM_EMAILDISCOVERY_AAD_LOGIN_TO_EMAILDISCOVERY_SPLITTER);
        else Context.back_action_stack.AddAction(Constants.CancelAction.FROM_EMAILDISCOVERY_AAD_LOGIN_TO_EMAILDISCOVERY_START);
        $(".login_cred_field_container").show();
        $(a).show();
        $(a).removeClass("email_discovery");
        $("#cred_userid_inputtext").show();
        $("#cred_password_inputtext").show();
        $("#cred_userid_container").show();
        $("#cred_sign_in_button").show();
        $("#cred_kmsi_container").show();
        $("#recover_container").show();
        $("#cred_password_container").show();
        $("#login_cta_text").show();
        $("#cred_cancel_button").show()
    },
    HideAADLoginLayout: function () {
        Context.email_discovery_mode = true;
        $(".login_cred_options_container").addClass("email_discovery");
        $("#cred_password_inputtext").val("");
        $("#cred_sign_in_button").hide();
        $("#cred_kmsi_container").hide();
        $("#recover_container").hide();
        $("#cred_password_container").hide();
        $("#login_cta_text").hide();
        $("#cred_cancel_button").hide()
    },
    CancelRedirect: function () {
        clearTimeout(Context.redirectTid);
        Context.username_state.home_realm_state = Constants.State.NONE;
        $("div.progress").css("visibility", "hidden");
        clearInterval(Context.animationTid);
        $("#redirect_message_container").hide();
        $("#cred_sign_in_button").hide();
        $("#redirect_cta_text").hide();
        $("#redirect_company_name_text").html("");
        $("#cred_kmsi_container").hide();
        $(".login_cred_options_container").addClass("email_discovery");
        Context.email_discovery_mode = true
    },
    GetEnumKeyName: function (a, c) {
        for (var b in a) if (a[b] == c) return b;
        return null
    }
};
var EmailDiscovery = MSLogin.EmailDiscovery;
$(document).ready(function () {
    EmailDiscovery.Initialize()
})
MSStrongAuth = {};
MSStrongAuth.ProofUp = {
    doSubmit: function () {
        var b = "#fmProofup",
		a = "#login_workload_logo_image";
        if ($(a).length > 0 && $(a).attr("src").toLowerCase().indexOf("office365") >= 0) {
            var c = $(b).attr("action"),
			d = c.indexOf("?") == -1 ? "?" : "&";
            $(b).attr("action", c + d + "BrandContextID=O365")
        }
        document.fmProofup.submit()
    }
};
var ProofUp = MSStrongAuth.ProofUp
MSStrongAuth = {
    appendQPToURL: function () {
        var b = "#switch_user_link";
        if ($(b).length == 0) return;
        var a = $(b).attr("href"),
		c = /^\d+$/,
		d = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;
        a = Util.AddQSParamIfNotExists(a, "id", c);
        a = Util.AddQSParamIfNotExists(a, "lc", c);
        a = Util.AddQSParamIfNotExists(a, "username", d);
        a = Util.AddQSParamIfNotExists(a, "ru");
        a = Util.AddQSParamIfNotExists(a, "whr");
        a = Util.AddQSParamIfNotExists(a, "wreply");
        a = Util.AddQSParamIfNotExists(a, "wauth");
        $(b).attr("href", a)
    }
};
MSStrongAuth.StrongAuthCheck = {
    currentPoll: 0,
    intervalId: 0,
    enablePolling: 1,
    authMethods: null,
    currentAuthMethod: null,
    currentAuthMethodId: "",
    authInProgress: 0,
    endAuthInProgress: 0,
    getProofupRedirectURL: function () {
        var a = Constants.TWOFA_PROOFUP_REDIRECT_URL + "&mkt=" + Constants.TWOFA_MARKET,
		b = Util.ExtractQSParam("id"),
		c = Util.ExtractQSParam("lc"),
		d = /^\d+$/;
        if (d.test(b)) a += "&id=" + b;
        if (d.test(c)) a += "&lc=" + c;
        return a
    },
    areValidMethodsPresent: function (a) {
        if (a == null || a.length == 0) return false;
        for (i = 0; i < a.length; i++) if (a[i].AuthMethodDeviceId != null && a[i].AuthMethodDeviceId != "") return true;
        return false
    },
    initAuthMethods: function (a) {
        StrongAuthCheck.authInProgress = 1;
        $.ajax({
            url: "StrongAuthCheck.srf?mkt=" + Constants.TWOFA_MARKET,
            timeout: Constants.TWOFA_TIMEOUT,
            data: a,
            dataType: "json",
            type: "POST",
            contentType: "application/json",
            success: StrongAuthCheck.initAuthSuccess,
            error: StrongAuthCheck.showUnknownError
        })
    },
    beginAuthUsingMethod: function (a) {
        var d = ".tile_secondary_name",
		c = "display",
		b = "#tfa_phone_text";
        StrongAuthCheck.currentAuthMethod = a;
        StrongAuthCheck.currentAuthMethodId = a.AuthMethodId;
        StrongAuthCheck.authInProgress = 1;
        var e = '{ "Method":"BeginAuth","SessionId":"' + a.SessionId + '","ContextId":"' + a.ContextId + '","AuthMethodId":"' + a.AuthMethodId + '"}';
        if (a.AuthMethodId.toUpperCase().indexOf("TWOWAYVOICE") == 0) $(".tfa_results_text.30050").show();
        else if (a.AuthMethodId.toUpperCase().indexOf("TWOWAYSMS") == 0) $(".tfa_results_text.30049").show();
        else a.AuthMethodId.toUpperCase().indexOf("PHONEAPPNOTIFICATION") == 0 && $(".tfa_results_text.30051").show();
        if (a.AuthMethodDeviceId != "none") {
            $(b).text(a.AuthMethodDeviceId);
            $(b).css(c, "block");
            $(d).show();
            $(d).text(a.AuthMethodDeviceId)
        } else {
            $(b).css(c, "none");
            $(d).hide()
        }
        $("#tfa_options_link_container").css(c, "block");
        StrongAuthCheck.beginAuth(e)
    },
    beginAuth: function (a) {
        $.ajax({
            url: "StrongAuthCheck.srf?mkt=" + Constants.TWOFA_MARKET,
            timeout: Constants.TWOFA_TIMEOUT,
            data: a,
            dataType: "json",
            type: "POST",
            contentType: "application/json",
            success: StrongAuthCheck.beginAuthSuccess,
            error: StrongAuthCheck.showUnknownError
        })
    },
    checkPolling: function (a) {
        var b = '{ "Method":"EndAuth", "SessionId":"' + a.SessionId + '","ContextId":"' + a.ContextId + '", "AuthMethodId":"' + a.AuthMethodId + '"}';
        if (StrongAuthCheck.enablePolling == 1) {
            StrongAuthCheck.currentPoll++;
            if (StrongAuthCheck.currentPoll < Constants.TWOFA_MAX_POLLS) StrongAuthCheck.poll(b);
            else StrongAuthCheck.showAuthTimeoutError(a)
        }
    },
    poll: function (a) {
        setTimeout(function () {
            $.ajax({
                url: "StrongAuthCheck.srf?mkt=" + Constants.TWOFA_MARKET,
                timeout: Constants.TWOFA_TIMEOUT,
                data: a,
                dataType: "json",
                type: "POST",
                contentType: "application/json",
                success: StrongAuthCheck.endAuthSuccess,
                error: StrongAuthCheck.showUnknownError
            })
        },
		Constants.TWOFA_POLLING_INTERVAL)
    },
    redirect: function () {
        if (StrongAuthCheck.enablePolling == 1) window.location.href = Constants.TWOFA_REDIRECT_URL
    },
    redirectProofup: function () {
        window.location.href = StrongAuthCheck.getProofupRedirectURL()
    },
    showInitError: function (a) {
        $(".tfa_results_text").hide();
        $("#redirect_dots_animation").hide();
        $(".tfa_error_text.30062").show();
        StrongAuthCheck.showErrorInfo(a);
        clearInterval(StrongAuthCheck.intervalId)
    },
    showAuthError: function (a) {
        var f = ".tfa_results_text",
		c = "30108",
		e = "30109",
		d = "30107";
        if (StrongAuthCheck.authInProgress != 1) return;
        var b = "30062";
        if (a.ResultValue == "UserVoiceAuthFailedCallWentToVoicemail") b = "30106";
        else if (a.ResultValue == "UserVoiceAuthFailedInvalidPhoneInput") b = d;
        else if (a.ResultValue == "UserVoiceAuthFailedInvalidPhoneNumber") b = e;
        else if (a.ResultValue == "UserAuthFailedDuplicateRequest") b = c;
        else if (a.ResultValue == "UserVoiceAuthFailedPhoneUnreachable") b = c;
        else if (a.ResultValue == "UserVoiceAuthFailedPhoneHungUp") b = d;
        else if (a.ResultValue == "UserVoiceAuthFailedInvalidExtension") b = e;
        else if (a.ResultValue == "UserVoiceAuthFailedProviderCouldntSendCall") b = c;
        else if (a.ResultValue == "User2WaySMSAuthFailedProviderCouldntSendSMS" || a.ResultValue == "SMSAuthFailedProviderCouldntSendSMS") b = c;
        else if (a.ResultValue == "User2WaySMSAuthFailedNoResponseTimeout" || a.ResultValue == "SMSAuthFailedNoResponseTimeout" || a.ResultValue == "PhoneAppNoResponse") b = "30106";
        else if (a.ResultValue == "User2WaySMSAuthFailedWrongCodeEntered" || a.ResultValue == "SMSAuthFailedWrongCodeEntered" || a.ResultValue == "OathCodeIncorrect" || a.ResultValue == "OathCodeDuplicate" || a.ResultValue == "OathCodeOld" || a.ResultValue == "PhoneAppInvalidResult" || a.ResultValue == "PhoneAppDenied") b = d;
        else if (a.ResultValue == "InvalidFormat") b = e;
        else if (a.ResultValue == "PhoneAppTokenChanged") b = "30178";
        if (StrongAuthCheck.currentAuthMethod == null) $(f).hide();
        else if (StrongAuthCheck.currentAuthMethod.AuthMethodId.toUpperCase().indexOf("ONEWAYSMS") == 0) $(".tfa_results_text.30167").show();
        else if (StrongAuthCheck.currentAuthMethod.AuthMethodId.toUpperCase().indexOf("PHONEAPPOTP") == 0) $(".tfa_results_text.30166").show();
        else $(f).hide();
        $("#redirect_dots_animation").hide();
        $(".tfa_error_text." + b).show();
        StrongAuthCheck.showErrorInfo(a);
        clearInterval(StrongAuthCheck.intervalId)
    },
    showAuthTimeoutError: function (b) {
        var a = ".tfa_results_text";
        if (StrongAuthCheck.authInProgress != 1) return;
        if (StrongAuthCheck.currentAuthMethod == null) $(a).hide();
        else if (StrongAuthCheck.currentAuthMethod.AuthMethodId.toUpperCase().indexOf("ONEWAYSMS") == 0) $(".tfa_results_text.30167").show();
        else if (StrongAuthCheck.currentAuthMethod.AuthMethodId.toUpperCase().indexOf("PHONEAPPOTP") == 0) $(".tfa_results_text.30166").show();
        else $(a).hide();
        $("#redirect_dots_animation").hide();
        $(".tfa_error_text.30106").show();
        StrongAuthCheck.showErrorInfo(b);
        clearInterval(StrongAuthCheck.intervalId);
        StrongAuthCheck.endAuthInProgress = 0
    },
    showUnknownError: function (d, b) {
        var a = ".tfa_results_text";
        if (StrongAuthCheck.authInProgress != 1) return;
        if (StrongAuthCheck.currentAuthMethod == null) $(a).hide();
        else if (StrongAuthCheck.currentAuthMethod.AuthMethodId.toUpperCase().indexOf("ONEWAYSMS") == 0) $(".tfa_results_text.30167").show();
        else if (StrongAuthCheck.currentAuthMethod.AuthMethodId.toUpperCase().indexOf("PHONEAPPOTP") == 0) $(".tfa_results_text.30166").show();
        else $(a).hide();
        $("#redirect_dots_animation").hide();
        if (b == "timeout") $(".tfa_error_text.30106").show();
        else $(".tfa_error_text.30062").show();
        clearInterval(StrongAuthCheck.intervalId);
        StrongAuthCheck.endAuthInProgress = 0
    },
    initAuthSuccess: function (a) {
        if (a.Result == "true") {
            StrongAuthCheck.authMethods = a;
            if (a.AuthMethodId == "") {
                StrongAuthCheck.redirectProofup();
                return
            }
            if (a.AuthMethodDeviceId == null || a.AuthMethodDeviceId == "") {
                $("#redirect_dots_animation").css("display", "none");
                $(".tfa_results_text").hide();
                clearInterval(StrongAuthCheck.intervalId);
                if (!StrongAuthCheck.areValidMethodsPresent(a.Methods)) $(".tfa_error_text.30125").show();
                else {
                    $(".tfa_error_text.30126").show();
                    $("#tfa_options_link_container").css("display", "block")
                }
            } else if (a.BypassTFA == "true") {
                StrongAuthCheck.enablePolling = 1;
                StrongAuthCheck.redirect()
            } else StrongAuthCheck.beginAuthUsingMethod(a)
        } else StrongAuthCheck.showInitError(a)
    },
    beginAuthSuccess: function (a) {
        var f = "input#tfa_code_inputtext",
		e = "PHONEAPPOTP",
		d = "ONEWAYSMS";
        if (a.Result == "true") {
            StrongAuthCheck.currentAuthMethod.SessionId = a.SessionId;
            var c = false,
			b;
            if (a.AuthMethodId.toUpperCase().indexOf(d) == 0 && StrongAuthCheck.currentAuthMethodId && StrongAuthCheck.currentAuthMethodId.toUpperCase().indexOf(d) == 0) {
                b = "30167";
                c = true
            } else if (a.AuthMethodId.toUpperCase().indexOf(e) == 0 && StrongAuthCheck.currentAuthMethodId && StrongAuthCheck.currentAuthMethodId.toUpperCase().indexOf(e) == 0) {
                b = "30166";
                c = true
            }
            if (c) {
                clearInterval(StrongAuthCheck.intervalId);
                $(f).val("");
                $("#redirect_dots_animation").hide();
                $(".tfa_results_text").hide();
                $(".tfa_results_text." + b).show();
                $("#tfa_code_container").show();
                $("#tfa_code_inputtext").focus();
                $("#tfa_button_container").show();
                $(f).keypress(function (a) {
                    User.KeyPressEnter(a, StrongAuthCheck.submitCode, 13)
                })
            } else {
                var g = '{ "Method":"EndAuth","SessionId":"' + a.SessionId + '","ContextId":"' + a.ContextId + '", "AuthMethodId":"' + a.AuthMethodId + '"}';
                StrongAuthCheck.currentPoll = 0;
                StrongAuthCheck.poll(g)
            }
        } else StrongAuthCheck.currentAuthMethod.SessionId == a.SessionId && StrongAuthCheck.showAuthError(a)
    },
    endAuthSuccess: function (a) {
        if (a.Result == "true") StrongAuthCheck.redirect();
        else if (a.Retry == "true") StrongAuthCheck.checkPolling(a);
        else if (StrongAuthCheck.currentAuthMethod.SessionId == a.SessionId) {
            StrongAuthCheck.showAuthError(a);
            StrongAuthCheck.endAuthInProgress = 0
        }
    },
    showOptions: function () {
        var c = "block",
		b = "none",
		a = "display";
        clearInterval(StrongAuthCheck.intervalId);
        StrongAuthCheck.enablePolling = 0;
        StrongAuthCheck.authInProgress = 0;
        StrongAuthCheck.currentAuthMethodId = "";
        $("#redirect_dots_animation").css(a, b);
        $(".tfa_results_text").css(a, b);
        $("#tfa_phone_text").css(a, b);
        $("#tfa_title_text").css(a, b);
        $(".tfa_error_text").css(a, b);
        $("#tfa_options_link_container").css(a, b);
        $("#tfa_code_container").css(a, b);
        $("#tfa_button_container").css(a, b);
        $("#tfa_client_side_error_text").hide();
        $("#tfa_options_title_text").css(a, c);
        $("#tfa_options_list").css(a, c);
        $.each(StrongAuthCheck.authMethods.Methods,
		function (b, d) {
		    if (d.AuthMethodDeviceId != null && d.AuthMethodDeviceId != "") if (b < Constants.TWOFA_MAXMETHODS) {
		        $("#link" + b).text(d.MethodDisplayString);
		        var e = "javascript:StrongAuthCheck.beginAltAuth(" + b + ")";
		        $("#link" + b).attr("href", e);
		        $("#listItem" + b).css(a, c)
		    }
		})
    },
    beginAltAuth: function (d) {
        var c = ".tfa_error_text",
		b = "block",
		a = "display";
        $("#redirect_dots_animation").css(a, b);
        $("#tfa_phone_text").css(a, b);
        $("#tfa_title_text").css(a, b);
        $(c).css(a, b);
        $("#tfa_options_link_container").css(a, b);
        $(c).hide();
        $(".tfa_results_text").css("visibility", "visible");
        $("#tfa_options_title_text").css(a, "none");
        $("#tfa_options_list").css(a, "none");
        StrongAuthCheck.enablePolling = 1;
        StrongAuthCheck.endAuthInProgress = 0;
        User.startAnimation();
        StrongAuthCheck.intervalId = setInterval(User.startAnimation, Constants.TWOFA_ANIMATION_INTERVAL);
        StrongAuthCheck.beginAuthUsingMethod(StrongAuthCheck.authMethods.Methods[d])
    },
    submitCode: function (a) {
        $(".tfa_error_text").hide();
        if (StrongAuthCheck.endAuthInProgress == 1) return;
        var a = $("input#tfa_code_inputtext").val();
        a = $.trim(a);
        if (!StrongAuthCheck.validateOTPPost(a)) return;
        StrongAuthCheck.endAuthInProgress = 1;
        $("#tfa_client_side_error_text").hide();
        var b = '{ "Method":"EndAuth", "SessionId":"' + StrongAuthCheck.currentAuthMethod.SessionId + '", "ContextId":"' + StrongAuthCheck.currentAuthMethod.ContextId + '", "AuthMethodId":"' + StrongAuthCheck.currentAuthMethod.AuthMethodId + '", "AdditionalAuthData":"' + a + '"}';
        StrongAuthCheck.currentPoll = 0;
        $("#redirect_dots_animation").show();
        setTimeout(User.startAnimation, 0);
        StrongAuthCheck.intervalId = setInterval(User.startAnimation, Constants.TWOFA_ANIMATION_INTERVAL);
        StrongAuthCheck.poll(b)
    },
    validateOTPPost: function (a) {
        if (a.length != Constants.TWOFA_OTP_MAX_LEN || Math.ceil(a) != Math.floor(a)) {
            $("#tfa_client_side_error_text").show();
            return false
        }
        return true
    },
    showErrorInfo: function (b) {
        var c = "#tfa_error_text",
		a = "(" + b.ErrCode;
        if (b.SessionId.length != 0) a = a + ", " + b.SessionId;
        a = a + ")";
        $(c).show();
        $(c).text(a)
    }
};
var StrongAuthCheck = MSStrongAuth.StrongAuthCheck;
$(window).load(function () {
    var a = "#redirect_dots_animation";
    if ($("meta[name=PageID]").attr("content") != "strongauthcheck.2.0") return;
    $(a).css("display", "block");
    $(a).css("visibility", "visible");
    User.setupCredFieldsWithPlaceHolder();
    setTimeout("User.startAnimation()", 0);
    StrongAuthCheck.intervalId = setInterval(User.startAnimation, Constants.TWOFA_ANIMATION_INTERVAL);

});
$(document).ready(function () {
    var a = "meta[name=PageID]";
    if ($(a).attr("content") != "strongauthcheck.2.0" && $(a).attr("content") != "proofup.2.0") return;

})
MSLogout = {
    logoutstate: null,
    success: [],
    error: [],
    noOfIncompleteSites: 0,
    thirdPartyCookieEnabled: -1,
    showSignOutInfo: function () {
        MSLogout.checkThirdPartyCookies();
        $.each(MSLogout.logoutstate.sites,
		function (b, a) {
		    MSLogout.checkSiteCookies(a)
		})
    },
    checkThirdPartyCookies: function () {
        if (MSLogout.logoutstate.thirdPartyCookieURL == undefined) return;
        var a = $("<img/>").attr("src", MSLogout.logoutstate.thirdPartyCookieURL).load(function () {
            MSLogout.thirdPartyCookieEnabled = ThirdPartyCookieStates.ENABLED
        }).error(function () {
            MSLogout.thirdPartyCookieEnabled = ThirdPartyCookieStates.DISABLED
        }()).css("visibility", "hidden")
    },
    checkSiteCookies: function (a) {
        var a = a;
        MSLogout.noOfIncompleteSites++;
        var b = $("<img/>").attr("src", a.url).load(function () {
            var b = a;
            return function () {
                MSLogout.noOfIncompleteSites--;
                MSLogout.success.push(b)
            }
        }()).error(function () {
            var b = a;
            return function () {
                MSLogout.noOfIncompleteSites--;
                MSLogout.error.push(b)
            }
        }()).css("visibility", "hidden");
        $("body").append(b)
    },
    displaySiteList: function () {
        var a = "#error_signed_out_sites";
        if (MSLogout.thirdPartyCookieEnabled == ThirdPartyCookieStates.NOT_LOADED || MSLogout.noOfIncompleteSites != 0) return;
        $(".signed_out_sites").show();
        $("#more_info_text").hide();
        var b = "#success_signed_out_sites";
        if (MSLogout.thirdPartyCookieEnabled == ThirdPartyCookieStates.DISABLED) b = a;
        $.each(MSLogout.success,
		function (c, a) {
		    $(b).append($("<div/>").attr("id", "site_" + a.id).text(a.name + " (" + a.id + ")"))
		});
        $.each(MSLogout.error,
		function (c, b) {
		    $(a).append($("<div/>").attr("id", "site_" + b.id).text(b.name + " (" + b.id + ")"))
		});
        MSLogout.displaySAMLList()
    },
    displaySAMLList: function () {
        var a = MSLogout.logoutstate.samlNamespace,
		b;
        if (a == undefined) return;
        if (a.status) b = "#success_signed_out_sites";
        else b = "#error_signed_out_sites";
        $(b).append($("<div/>").attr("id", "site_saml").text(a.name))
    }
};
MSLogout.ThirdPartyCookieStates = {
    NOT_LOADED: -1,
    DISABLED: 0,
    ENABLED: 1
};






//....
function baseClass() {
    this.showMsg = function () {
        alert("baseClass::showMsg");
    }

    this.baseShowMsg = function () {
        alert("baseClass::baseShowMsg");
    }
}
baseClass.showMsg = function () {
    alert("baseClass::showMsg static");
}

function extendClass() {
    this.showMsg = function () {
        alert("extendClass::showMsg");
    }
}
extendClass.showMsg = function () {
    alert("extendClass::showMsg static")
}

extendClass.prototype = new baseClass();
var instance = new extendClass();

//instance.showMsg(); //显示extendClass::showMsg
//instance.baseShowMsg(); //显示baseClass::baseShowMsg
//instance.showMsg(); //显示extendClass::showMsg

//baseClass.showMsg.call(instance);//显示baseClass::showMsg static
  
//var baseinstance = new baseClass();
//baseinstance.showMsg.call(instance);//显示baseClass::showMsg

 

function People(name) {
    this.name = name;
    this.Eat = function () {
        alert("People:"+ this.name+ "eat rice.");
    }
}

function Teacher(name) {
    this.name = name;
    this.TeachStudent = function () {
        alert("Teacher :"+this.name+"can teach student !");
    }
}

function Runner(name) {
    this.name = name;
    Runner.Eat = function () {
        alert("runner :"+this.name +" eat a lots than others");
    }
}

People.prototype.Runner = function () {
    alert("People can run");
}