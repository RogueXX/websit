! function(i) {
    function t(e) {
        if (n[e]) return n[e].exports;
        var s = n[e] = {
            exports: {},
            id: e,
            loaded: !1
        };
        return i[e].call(s.exports, s, s.exports, t), s.loaded = !0, s.exports
    }
    var n = {};
    return t.m = i, t.c = n, t.p = "", t(0)
}([function(i, t, n) {
    "use strict";

    function e(i, t) {
        a(i);
        var n = location.href.replace(/(https:\/\/|http:\/\/|\/\/)/, "");
        i.find(".channel-item").each(function(t, e) {
            function s() {
                for (var t = i.find(".channel-item.selected").width(), n = 20, e = i.find(".channel-item.selected").index(), s = 0; s < e; s++) n = n + i.find(".channel-item").eq(s).width() + 40;
                i.find(".slug").css({
                    width: t,
                    left: n
                })
            }
            var a, r = $(this).find(".channel-link");
            a = r.attr("href").indexOf("javascript:") == -1 ? r.attr("href") : r.attr("data-href"), a.indexOf("javascript:") == -1 && n.indexOf(a.replace(/(https:\/\/|http:\/\/|\/\/)/, "")) > -1 && (i.find(".channel-item").removeClass("selected"), $(this).addClass("selected"), s()), $(this).on("mouseover", function() {
                for (var n = $(this).width(), e = 20, s = 0; s < t; s++) e = e + i.find(".channel-item").eq(s).width() + 40;
                i.find(".slug").css({
                    width: n,
                    left: e
                })
            }), $(this).mouseout(function(i) {
                s()
            })
        })
    }

    function s(i) {
        if (isMobile) {
            var t = $(window).height();
            i.find(".main").height(t), i.find(".open").click(function(t) {
                i.find(".main").css("right", 0)
            }), document.addEventListener("click", function(t) {
                var n = $(t.target);
                0 == n.closest(".main").length && 0 == n.closest(".open").length && i.find(".main").css("right", -400 / 75 + "rem")
            }), i.find(".close").click(function(t) {
                i.find(".main").css("right", -400 / 75 + "rem")
            }), i.find(".page").click(function(i) {
                if (i.preventDefault(), $(this).siblings("a").length > 0) $(this).siblings(".sub").eq(0).attr("style") == "height: " + 85 / 75 + "rem" ? ($(this).siblings(".sub").attr("style", "height: 0"), $(this).find("i").removeClass("icon-reduce").addClass("icon-icon-tbar-add")) : ($(this).siblings(".sub").attr("style", "height: " + 85 / 75 + "rem"), $(this).find("i").removeClass("icon-icon-tbar-add").addClass("icon-reduce"));
                else {
                    var t = $(this).attr("href");
                    window.open(t, "_self")
                }
            })
        } else i.find(".yq-channel").css("margin-right", i.find(".yq-right.PC").width() + "px")
    }

    function a(i) {
        var t = window.onscroll;
        window.onscroll = function() {
            t && t(), clearTimeout(r), r = setTimeout(function() {
                var t = i.find(".module-wrap");
                $(window).scrollTop() > 0 ? t.removeClass("bg-transparent") : t.addClass("bg-transparent")
            }, 200)
        }
    }
    n(2);
    var r, c = $(".wb-zc-aliyun-xfmod-yunqi-topbar");
    c.each(function() {
        var i = $(this).find("textarea.schemaData"),
            t = i.val(),
            n = JSON.parse(t),
            a = $(this);
        n && e($(this), n), $(document).ready(function() {
            s(a)
        }), $("head").append('<link rel="icon" href="//img.alicdn.com/tfs/TB1cCopQFXXXXbKXXXXXXXXXXXX-32-32.ico" type="image/x-icon">')
    }), i.exports = e
}, , function(i, t) {}]);