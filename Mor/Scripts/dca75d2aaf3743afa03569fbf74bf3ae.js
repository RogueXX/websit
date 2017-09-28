! function(a, b) {
    function c(a, b) {
        a = a.toString().split("."), b = b.toString().split(".");
        for (var c = 0; c < a.length || c < b.length; c++) {
            var d = parseInt(a[c], 10),
                e = parseInt(b[c], 10);
            if (window.isNaN(d) && (d = 0), window.isNaN(e) && (e = 0), d < e) return -1;
            if (d > e) return 1
        }
        return 0
    }
    var d = a.Promise,
        e = a.document,
        f = a.navigator.userAgent,
        g = /Windows\sPhone\s(?:OS\s)?[\d\.]+/i.test(f) || /Windows\sNT\s[\d\.]+/i.test(f),
        h = g && a.WindVane_Win_Private && a.WindVane_Win_Private.call,
        i = /iPhone|iPad|iPod/i.test(f),
        j = /Android/i.test(f),
        k = f.match(/WindVane[\/\s](\d+[._]\d+[._]\d+)/),
        l = Object.prototype.hasOwnProperty,
        m = b.windvane = a.WindVane || (a.WindVane = {}),
        n = (a.WindVane_Native, Math.floor(65536 * Math.random())),
        o = 1,
        p = [],
        q = 3,
        r = "hybrid",
        s = "wv_hybrid",
        t = "iframe_",
        u = "param_",
        v = "chunk_",
        w = 6e5,
        x = 6e5,
        y = 6e4;
    k = k ? (k[1] || "0.0.0").replace(/\_/g, ".") : "0.0.0";
    var z = {
            isAvailable: 1 === c(k, "0"),
            call: function(a, b, c, e, f, g) {
                var h, i;
                "number" == typeof arguments[arguments.length - 1] && (g = arguments[arguments.length - 1]), "function" != typeof e && (e = null), "function" != typeof f && (f = null), d && (i = {}, i.promise = new d(function(a, b) {
                    i.resolve = a, i.reject = b
                })), h = A.getSid();
                var j = {
                    success: e,
                    failure: f,
                    deferred: i
                };
                if (g > 0 && (j.timeout = setTimeout(function() {
                        z.onFailure(h, {
                            ret: "HY_TIMEOUT"
                        })
                    }, g)), A.registerCall(h, j), A.registerGC(h, g), z.isAvailable ? A.callMethod(a, b, c, h) : z.onFailure(h, {
                        ret: "HY_NOT_IN_WINDVANE"
                    }), i) return i.promise
            },
            fireEvent: function(a, b, c) {
                var d = e.createEvent("HTMLEvents");
                d.initEvent(a, !1, !0), d.param = A.parseData(b || A.getData(c)), e.dispatchEvent(d)
            },
            getParam: function(a) {
                return A.getParam(a)
            },
            setData: function(a, b) {
                A.setData(a, b)
            },
            onSuccess: function(a, b) {
                A.onComplete(a, b, "success")
            },
            onFailure: function(a, b) {
                A.onComplete(a, b, "failure")
            }
        },
        A = {
            params: {},
            chunks: {},
            calls: {},
            getSid: function() {
                return (n + o++) % 65536 + ""
            },
            buildParam: function(a) {
                return a && "object" == typeof a ? JSON.stringify(a) : a || ""
            },
            getParam: function(a) {
                return this.params[u + a] || ""
            },
            setParam: function(a, b) {
                this.params[u + a] = b
            },
            parseData: function(a) {
                var b;
                if (a && "string" == typeof a) try {
                    b = JSON.parse(a)
                } catch (a) {
                    b = {
                        ret: ["WV_ERR::PARAM_PARSE_ERROR"]
                    }
                } else b = a || {};
                return b
            },
            setData: function() {
                this.chunks[v + sid] = this.chunks[v + sid] || [], this.chunks[v + sid].push(chunk)
            },
            getData: function(a) {
                return this.chunks[v + a] ? this.chunks[v + a].join("") : ""
            },
            registerCall: function(a, b) {
                this.calls[a] = b
            },
            unregisterCall: function(a) {
                var b = {};
                return this.calls[a] && (b = this.calls[a], delete this.calls[a]), b
            },
            useIframe: function(a, b) {
                var c = t + a,
                    d = p.pop();
                d || (d = e.createElement("iframe"), d.setAttribute("frameborder", "0"), d.style.cssText = "width:0;height:0;border:0;display:none;"), d.setAttribute("id", c), d.setAttribute("src", b), d.parentNode || setTimeout(function() {
                    e.body.appendChild(d)
                }, 5)
            },
            retrieveIframe: function(a) {
                var b = t + a,
                    c = e.querySelector("#" + b);
                p.length >= q ? e.body.removeChild(c) : p.indexOf(c) < 0 && p.push(c)
            },
            callMethod: function(b, c, d, e) {
                if (d = A.buildParam(d), g) h ? a.WindVane_Win_Private.call(b, c, e, d) : this.onComplete(e, {
                    ret: "HY_NO_HANDLER_ON_WP"
                }, "failure");
                else {
                    var f = r + "://" + b + ":" + e + "/" + c + "?" + d;
                    if (i) this.setParam(e, d), this.useIframe(e, f);
                    else if (j) {
                        var k = s + ":";
                        window.prompt(f, k)
                    } else this.onComplete(e, {
                        ret: "HY_NOT_SUPPORT_DEVICE"
                    }, "failure")
                }
            },
            registerGC: function(a, b) {
                var c = this,
                    d = Math.max(b || 0, w),
                    e = Math.max(b || 0, y),
                    f = Math.max(b || 0, x);
                setTimeout(function() {
                    c.unregisterCall(a)
                }, d), i ? setTimeout(function() {
                    c.params[u + a] && delete c.params[u + a]
                }, e) : j && setTimeout(function() {
                    c.chunks[v + a] && delete c.chunks[v + a]
                }, f)
            },
            onComplete: function(a, b, c) {
                var d = this.unregisterCall(a),
                    e = d.success,
                    f = d.failure,
                    g = d.deferred,
                    h = d.timeout;
                h && clearTimeout(h), b = b ? b : this.getData(a), b = this.parseData(b);
                var k = b.ret;
                "string" == typeof k && (b = b.value || b, b.ret || (b.ret = [k])), "success" === c ? (e && e(b), g && g.resolve(b)) : "failure" === c && (f && f(b), g && g.reject(b)), i ? (this.retrieveIframe(a), this.params[u + a] && delete this.params[u + a]) : j && this.chunks[v + a] && delete this.chunks[v + a]
            }
        };
    for (var B in z) l.call(m, B) || (m[B] = z[B])
}(window, window.lib || (window.lib = {}));
! function(a, b) {
    function c(a) {
        Object.defineProperty(this, "val", {
            value: a.toString(),
            enumerable: !0
        }), this.gt = function(a) {
            return c.compare(this, a) > 0
        }, this.gte = function(a) {
            return c.compare(this, a) >= 0
        }, this.lt = function(a) {
            return c.compare(this, a) < 0
        }, this.lte = function(a) {
            return c.compare(this, a) <= 0
        }, this.eq = function(a) {
            return 0 === c.compare(this, a)
        }
    }
    b.env = b.env || {}, c.prototype.toString = function() {
        return this.val
    }, c.prototype.valueOf = function() {
        for (var a = this.val.split("."), b = [], c = 0; c < a.length; c++) {
            var d = parseInt(a[c], 10);
            isNaN(d) && (d = 0);
            var e = d.toString();
            e.length < 5 && (e = Array(6 - e.length).join("0") + e), b.push(e), 1 === b.length && b.push(".")
        }
        return parseFloat(b.join(""))
    }, c.compare = function(a, b) {
        a = a.toString().split("."), b = b.toString().split(".");
        for (var c = 0; c < a.length || c < b.length; c++) {
            var d = parseInt(a[c], 10),
                e = parseInt(b[c], 10);
            if (window.isNaN(d) && (d = 0), window.isNaN(e) && (e = 0), e > d) return -1;
            if (d > e) return 1
        }
        return 0
    }, b.version = function(a) {
        return new c(a)
    }
}(window, window.lib || (window.lib = {})),
function(a, b) {
    b.env = b.env || {};
    var c = a.location.search.replace(/^\?/, "");
    if (b.env.params = {}, c)
        for (var d = c.split("&"), e = 0; e < d.length; e++) {
            d[e] = d[e].split("=");
            try {
                b.env.params[d[e][0]] = decodeURIComponent(d[e][1])
            } catch (f) {
                b.env.params[d[e][0]] = d[e][1]
            }
        }
}(window, window.lib || (window.lib = {})),
function(a, b) {
    b.env = b.env || {};
    var c, d = a.navigator.userAgent;
    if (c = d.match(/Windows\sPhone\s(?:OS\s)?([\d\.]+)/)) b.env.os = {
        name: "Windows Phone",
        isWindowsPhone: !0,
        version: c[1]
    };
    else if (d.match(/Safari/) && (c = d.match(/Android[\s\/]([\d\.]+)/))) b.env.os = {
        version: c[1]
    }, d.match(/Mobile\s+Safari/) ? (b.env.os.name = "Android", b.env.os.isAndroid = !0) : (b.env.os.name = "AndroidPad", b.env.os.isAndroidPad = !0);
    else if (c = d.match(/(iPhone|iPad|iPod)/)) {
        var e = c[1];
        (c = d.match(/OS ([\d_\.]+) like Mac OS X/)) && (b.env.os = {
            name: e,
            isIPhone: "iPhone" === e || "iPod" === e,
            isIPad: "iPad" === e,
            isIOS: !0,
            version: c[1].split("_").join(".")
        })
    }
    b.env.os || (b.env.os = {
        name: "unknown",
        version: "0.0.0"
    }), b.version && (b.env.os.version = b.version(b.env.os.version))
}(window, window.lib || (window.lib = {})),
function(a, b) {
    b.env = b.env || {};
    var c, d = a.navigator.userAgent;
    (c = d.match(/(?:UCWEB|UCBrowser\/)([\d\.]+)/)) ? b.env.browser = {
        name: "UC",
        isUC: !0,
        version: c[1]
    }: (c = d.match(/MQQBrowser\/([\d\.]+)/)) ? b.env.browser = {
        name: "QQ",
        isQQ: !0,
        version: c[1]
    } : (c = d.match(/(?:Firefox|FxiOS)\/([\d\.]+)/)) ? b.env.browser = {
        name: "Firefox",
        isFirefox: !0,
        version: c[1]
    } : (c = d.match(/MSIE\s([\d\.]+)/)) || (c = d.match(/IEMobile\/([\d\.]+)/)) ? (b.env.browser = {
        version: c[1]
    }, d.match(/IEMobile/) ? (b.env.browser.name = "IEMobile", b.env.browser.isIEMobile = !0) : (b.env.browser.name = "IE", b.env.browser.isIE = !0), d.match(/Android|iPhone/) && (b.env.browser.isIELikeWebkit = !0)) : (c = d.match(/(?:Chrome|CriOS)\/([\d\.]+)/)) ? (b.env.browser = {
        name: "Chrome",
        isChrome: !0,
        version: c[1]
    }, d.match(/Version\/[\d+\.]+\s*Chrome/) && (b.env.browser.name = "Chrome Webview", b.env.browser.isWebview = !0)) : d.match(/Safari/) && (c = d.match(/Android[\s\/]([\d\.]+)/)) ? b.env.browser = {
        name: "Android",
        isAndroid: !0,
        version: c[1]
    } : d.match(/iPhone|iPad|iPod/) && (d.match(/Safari/) && (c = d.match(/Version\/([\d\.]+)/)) ? b.env.browser = {
        name: "Safari",
        isSafari: !0,
        version: c[1]
    } : (c = d.match(/OS ([\d_\.]+) like Mac OS X/)) && (b.env.browser = {
        name: "iOS Webview",
        isWebview: !0,
        version: c[1].replace(/\_/g, ".")
    })), b.env.browser || (b.env.browser = {
        name: "unknown",
        version: "0.0.0"
    }), b.version && (b.env.browser.version = b.version(b.env.browser.version))
}(window, window.lib || (window.lib = {})),
function(a, b) {
    b.env = b.env || {};
    var c = a.navigator.userAgent;
    c.match(/Weibo/i) ? b.env.thirdapp = {
        appname: "Weibo",
        isWeibo: !0
    } : c.match(/MicroMessenger/i) ? b.env.thirdapp = {
        appname: "Weixin",
        isWeixin: !0
    } : b.env.thirdapp = !1
}(window, window.lib || (window.lib = {})),
function(a, b) {
    b.env = b.env || {};
    var c, d, e = a.navigator.userAgent;
    (d = e.match(/WindVane[\/\s]([\d\.\_]+)/)) && (c = d[1]);
    var f = !1,
        g = "",
        h = "",
        i = "",
        j = a._ua_popLayer || "",
        k = !1,
        l = "";
    (d = e.match(/AliApp\(([A-Z\-]+)\/([\d\.]+)\)/i)) && (f = !0, g = d[1], i = d[2], h = g.indexOf("-PD") > 0 ? b.env.os.isIOS ? "iPad" : b.env.os.isAndroid ? "AndroidPad" : b.env.os.name : b.env.os.name), !g && e.indexOf("TBIOS") > 0 && (g = "TB"), j && (d = j.match(/PopLayer\/([\d\.]+)/i)) && (k = !0, l = d[1]), f ? b.env.aliapp = {
        windvane: b.version(c || "0.0.0"),
        appname: g || "unkown",
        version: b.version(i || "0.0.0"),
        platform: h || b.env.os.name,
        poplayer: k || !1,
        poplayerVersion: b.version(l || "0.0.0")
    } : b.env.aliapp = !1, b.env.taobaoApp = b.env.aliapp
}(window, window.lib || (window.lib = {}));