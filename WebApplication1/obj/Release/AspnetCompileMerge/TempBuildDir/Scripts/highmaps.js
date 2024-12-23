﻿/*
 Highmaps JS v7.0.1 (2018-12-19)

 (c) 2011-2018 Torstein Honsi

 License: www.highcharts.com/license
*/
(function (O, H) { "object" === typeof module && module.exports ? module.exports = O.document ? H(O) : H : "function" === typeof define && define.amd ? define(function () { return H(O) }) : O.Highcharts = H(O) })("undefined" !== typeof window ? window : this, function (O) {
    var H = function () {
        var a = "undefined" === typeof O ? window : O, z = a.document, C = a.navigator && a.navigator.userAgent || "", B = z && z.createElementNS && !!z.createElementNS("http://www.w3.org/2000/svg", "svg").createSVGRect, h = /(edge|msie|trident)/i.test(C) && !a.opera, d = -1 !== C.indexOf("Firefox"),
        t = -1 !== C.indexOf("Chrome"), w = d && 4 > parseInt(C.split("Firefox/")[1], 10); return a.Highcharts ? a.Highcharts.error(16, !0) : {
            product: "Highmaps", version: "7.0.1", deg2rad: 2 * Math.PI / 360, doc: z, hasBidiBug: w, hasTouch: z && void 0 !== z.documentElement.ontouchstart, isMS: h, isWebKit: -1 !== C.indexOf("AppleWebKit"), isFirefox: d, isChrome: t, isSafari: !t && -1 !== C.indexOf("Safari"), isTouchDevice: /(Mobile|Android|Windows Phone)/.test(C), SVG_NS: "http://www.w3.org/2000/svg", chartCount: 0, seriesTypes: {}, symbolSizes: {}, svg: B, win: a, marginNames: ["plotTop",
                "marginRight", "marginBottom", "plotLeft"], noop: function () { }, charts: []
        }
    }(); (function (a) {
    a.timers = []; var z = a.charts, C = a.doc, B = a.win; a.error = function (h, d, t) { var w = a.isNumber(h) ? "Highcharts error #" + h + ": www.highcharts.com/errors/" + h : h; t && a.fireEvent(t, "displayError", { code: h }); if (d) throw Error(w); B.console && console.log(w) }; a.Fx = function (a, d, t) { this.options = d; this.elem = a; this.prop = t }; a.Fx.prototype = {
        dSetter: function () {
            var a = this.paths[0], d = this.paths[1], t = [], w = this.now, r = a.length, v; if (1 === w) t = this.toD;
            else if (r === d.length && 1 > w) for (; r--;)v = parseFloat(a[r]), t[r] = isNaN(v) ? d[r] : w * parseFloat(d[r] - v) + v; else t = d; this.elem.attr("d", t, null, !0)
        }, update: function () { var a = this.elem, d = this.prop, t = this.now, w = this.options.step; if (this[d + "Setter"]) this[d + "Setter"](); else a.attr ? a.element && a.attr(d, t, null, !0) : a.style[d] = t + this.unit; w && w.call(a, t, this) }, run: function (h, d, t) {
            var w = this, r = w.options, v = function (a) { return v.stopped ? !1 : w.step(a) }, p = B.requestAnimationFrame || function (a) { setTimeout(a, 13) }, k = function () {
                for (var n =
                    0; n < a.timers.length; n++)a.timers[n]() || a.timers.splice(n--, 1); a.timers.length && p(k)
            }; h !== d || this.elem["forceAnimate:" + this.prop] ? (this.startTime = +new Date, this.start = h, this.end = d, this.unit = t, this.now = this.start, this.pos = 0, v.elem = this.elem, v.prop = this.prop, v() && 1 === a.timers.push(v) && p(k)) : (delete r.curAnim[this.prop], r.complete && 0 === Object.keys(r.curAnim).length && r.complete.call(this.elem))
        }, step: function (h) {
            var d = +new Date, t, w = this.options, r = this.elem, v = w.complete, p = w.duration, k = w.curAnim; r.attr &&
                !r.element ? h = !1 : h || d >= p + this.startTime ? (this.now = this.end, this.pos = 1, this.update(), t = k[this.prop] = !0, a.objectEach(k, function (a) { !0 !== a && (t = !1) }), t && v && v.call(r), h = !1) : (this.pos = w.easing((d - this.startTime) / p), this.now = this.start + (this.end - this.start) * this.pos, this.update(), h = !0); return h
        }, initPath: function (h, d, t) {
            function w(a) { var c, l; for (b = a.length; b--;)c = "M" === a[b] || "L" === a[b], l = /[a-zA-Z]/.test(a[b + 3]), c && l && a.splice(b + 1, 0, a[b + 1], a[b + 2], a[b + 1], a[b + 2]) } function r(a, g) {
                for (; a.length < c;) {
                a[0] = g[c - a.length];
                    var l = a.slice(0, m);[].splice.apply(a, [0, 0].concat(l)); q && (l = a.slice(a.length - m), [].splice.apply(a, [a.length, 0].concat(l)), b--)
                } a[0] = "M"
            } function v(a, b) { for (var l = (c - a.length) / m; 0 < l && l--;)g = a.slice().splice(a.length / u - m, m * u), g[0] = b[c - m - l * m], e && (g[m - 6] = g[m - 2], g[m - 5] = g[m - 1]), [].splice.apply(a, [a.length / u, 0].concat(g)), q && l-- } d = d || ""; var p, k = h.startX, n = h.endX, e = -1 < d.indexOf("C"), m = e ? 7 : 3, c, g, b; d = d.split(" "); t = t.slice(); var q = h.isArea, u = q ? 2 : 1, x; e && (w(d), w(t)); if (k && n) {
                for (b = 0; b < k.length; b++)if (k[b] ===
                    n[0]) { p = b; break } else if (k[0] === n[n.length - k.length + b]) { p = b; x = !0; break } void 0 === p && (d = [])
            } d.length && a.isNumber(p) && (c = t.length + p * u * m, x ? (r(d, t), v(t, d)) : (r(t, d), v(d, t))); return [d, t]
        }, fillSetter: function () { a.Fx.prototype.strokeSetter.apply(this, arguments) }, strokeSetter: function () { this.elem.attr(this.prop, a.color(this.start).tweenTo(a.color(this.end), this.pos), null, !0) }
    }; a.merge = function () {
        var h, d = arguments, t, w = {}, r = function (h, p) {
        "object" !== typeof h && (h = {}); a.objectEach(p, function (k, n) {
        !a.isObject(k, !0) ||
            a.isClass(k) || a.isDOMElement(k) ? h[n] = p[n] : h[n] = r(h[n] || {}, k)
        }); return h
        }; !0 === d[0] && (w = d[1], d = Array.prototype.slice.call(d, 2)); t = d.length; for (h = 0; h < t; h++)w = r(w, d[h]); return w
    }; a.pInt = function (a, d) { return parseInt(a, d || 10) }; a.isString = function (a) { return "string" === typeof a }; a.isArray = function (a) { a = Object.prototype.toString.call(a); return "[object Array]" === a || "[object Array Iterator]" === a }; a.isObject = function (h, d) { return !!h && "object" === typeof h && (!d || !a.isArray(h)) }; a.isDOMElement = function (h) {
        return a.isObject(h) &&
            "number" === typeof h.nodeType
    }; a.isClass = function (h) { var d = h && h.constructor; return !(!a.isObject(h, !0) || a.isDOMElement(h) || !d || !d.name || "Object" === d.name) }; a.isNumber = function (a) { return "number" === typeof a && !isNaN(a) && Infinity > a && -Infinity < a }; a.erase = function (a, d) { for (var h = a.length; h--;)if (a[h] === d) { a.splice(h, 1); break } }; a.defined = function (a) { return void 0 !== a && null !== a }; a.attr = function (h, d, t) {
        var w; a.isString(d) ? a.defined(t) ? h.setAttribute(d, t) : h && h.getAttribute && ((w = h.getAttribute(d)) || "class" !==
            d || (w = h.getAttribute(d + "Name"))) : a.defined(d) && a.isObject(d) && a.objectEach(d, function (a, d) { h.setAttribute(d, a) }); return w
    }; a.splat = function (h) { return a.isArray(h) ? h : [h] }; a.syncTimeout = function (a, d, t) { if (d) return setTimeout(a, d, t); a.call(0, t) }; a.clearTimeout = function (h) { a.defined(h) && clearTimeout(h) }; a.extend = function (a, d) { var h; a || (a = {}); for (h in d) a[h] = d[h]; return a }; a.pick = function () { var a = arguments, d, t, w = a.length; for (d = 0; d < w; d++)if (t = a[d], void 0 !== t && null !== t) return t }; a.css = function (h, d) {
    a.isMS &&
        !a.svg && d && void 0 !== d.opacity && (d.filter = "alpha(opacity\x3d" + 100 * d.opacity + ")"); a.extend(h.style, d)
    }; a.createElement = function (h, d, t, w, r) { h = C.createElement(h); var v = a.css; d && a.extend(h, d); r && v(h, { padding: 0, border: "none", margin: 0 }); t && v(h, t); w && w.appendChild(h); return h }; a.extendClass = function (h, d) { var t = function () { }; t.prototype = new h; a.extend(t.prototype, d); return t }; a.pad = function (a, d, t) { return Array((d || 2) + 1 - String(a).replace("-", "").length).join(t || 0) + a }; a.relativeLength = function (a, d, t) {
        return /%$/.test(a) ?
            d * parseFloat(a) / 100 + (t || 0) : parseFloat(a)
    }; a.wrap = function (a, d, t) { var h = a[d]; a[d] = function () { var a = Array.prototype.slice.call(arguments), d = arguments, p = this; p.proceed = function () { h.apply(p, arguments.length ? arguments : d) }; a.unshift(h); a = t.apply(this, a); p.proceed = null; return a } }; a.datePropsToTimestamps = function (h) { a.objectEach(h, function (d, t) { a.isObject(d) && "function" === typeof d.getTime ? h[t] = d.getTime() : (a.isObject(d) || a.isArray(d)) && a.datePropsToTimestamps(d) }) }; a.formatSingle = function (h, d, t) {
        var w = /\.([0-9])/,
        r = a.defaultOptions.lang; /f$/.test(h) ? (t = (t = h.match(w)) ? t[1] : -1, null !== d && (d = a.numberFormat(d, t, r.decimalPoint, -1 < h.indexOf(",") ? r.thousandsSep : ""))) : d = (t || a.time).dateFormat(h, d); return d
    }; a.format = function (h, d, t) {
        for (var w = "{", r = !1, v, p, k, n, e = [], m; h;) { w = h.indexOf(w); if (-1 === w) break; v = h.slice(0, w); if (r) { v = v.split(":"); p = v.shift().split("."); n = p.length; m = d; for (k = 0; k < n; k++)m && (m = m[p[k]]); v.length && (m = a.formatSingle(v.join(":"), m, t)); e.push(m) } else e.push(v); h = h.slice(w + 1); w = (r = !r) ? "}" : "{" } e.push(h);
        return e.join("")
    }; a.getMagnitude = function (a) { return Math.pow(10, Math.floor(Math.log(a) / Math.LN10)) }; a.normalizeTickInterval = function (h, d, t, w, r) { var v, p = h; t = a.pick(t, 1); v = h / t; d || (d = r ? [1, 1.2, 1.5, 2, 2.5, 3, 4, 5, 6, 8, 10] : [1, 2, 2.5, 5, 10], !1 === w && (1 === t ? d = d.filter(function (a) { return 0 === a % 1 }) : .1 >= t && (d = [1 / t]))); for (w = 0; w < d.length && !(p = d[w], r && p * t >= h || !r && v <= (d[w] + (d[w + 1] || d[w])) / 2); w++); return p = a.correctFloat(p * t, -Math.round(Math.log(.001) / Math.LN10)) }; a.stableSort = function (a, d) {
        var h = a.length, w, r; for (r = 0; r <
            h; r++)a[r].safeI = r; a.sort(function (a, p) { w = d(a, p); return 0 === w ? a.safeI - p.safeI : w }); for (r = 0; r < h; r++)delete a[r].safeI
    }; a.arrayMin = function (a) { for (var d = a.length, h = a[0]; d--;)a[d] < h && (h = a[d]); return h }; a.arrayMax = function (a) { for (var d = a.length, h = a[0]; d--;)a[d] > h && (h = a[d]); return h }; a.destroyObjectProperties = function (h, d) { a.objectEach(h, function (a, w) { a && a !== d && a.destroy && a.destroy(); delete h[w] }) }; a.discardElement = function (h) {
        var d = a.garbageBin; d || (d = a.createElement("div")); h && d.appendChild(h); d.innerHTML =
            ""
    }; a.correctFloat = function (a, d) { return parseFloat(a.toPrecision(d || 14)) }; a.setAnimation = function (h, d) { d.renderer.globalAnimation = a.pick(h, d.options.chart.animation, !0) }; a.animObject = function (h) { return a.isObject(h) ? a.merge(h) : { duration: h ? 500 : 0 } }; a.timeUnits = { millisecond: 1, second: 1E3, minute: 6E4, hour: 36E5, day: 864E5, week: 6048E5, month: 24192E5, year: 314496E5 }; a.numberFormat = function (h, d, t, w) {
        h = +h || 0; d = +d; var r = a.defaultOptions.lang, v = (h.toString().split(".")[1] || "").split("e")[0].length, p, k, n = h.toString().split("e");
        -1 === d ? d = Math.min(v, 20) : a.isNumber(d) ? d && n[1] && 0 > n[1] && (p = d + +n[1], 0 <= p ? (n[0] = (+n[0]).toExponential(p).split("e")[0], d = p) : (n[0] = n[0].split(".")[0] || 0, h = 20 > d ? (n[0] * Math.pow(10, n[1])).toFixed(d) : 0, n[1] = 0)) : d = 2; k = (Math.abs(n[1] ? n[0] : h) + Math.pow(10, -Math.max(d, v) - 1)).toFixed(d); v = String(a.pInt(k)); p = 3 < v.length ? v.length % 3 : 0; t = a.pick(t, r.decimalPoint); w = a.pick(w, r.thousandsSep); h = (0 > h ? "-" : "") + (p ? v.substr(0, p) + w : ""); h += v.substr(p).replace(/(\d{3})(?=\d)/g, "$1" + w); d && (h += t + k.slice(-d)); n[1] && 0 !== +h && (h +=
            "e" + n[1]); return h
    }; Math.easeInOutSine = function (a) { return -.5 * (Math.cos(Math.PI * a) - 1) }; a.getStyle = function (h, d, t) {
        if ("width" === d) return Math.max(0, Math.min(h.offsetWidth, h.scrollWidth, h.getBoundingClientRect ? Math.floor(h.getBoundingClientRect().width) : Infinity) - a.getStyle(h, "padding-left") - a.getStyle(h, "padding-right")); if ("height" === d) return Math.max(0, Math.min(h.offsetHeight, h.scrollHeight) - a.getStyle(h, "padding-top") - a.getStyle(h, "padding-bottom")); B.getComputedStyle || a.error(27, !0); if (h = B.getComputedStyle(h,
            void 0)) h = h.getPropertyValue(d), a.pick(t, "opacity" !== d) && (h = a.pInt(h)); return h
    }; a.inArray = function (a, d, t) { return d.indexOf(a, t) }; a.find = Array.prototype.find ? function (a, d) { return a.find(d) } : function (a, d) { var h, w = a.length; for (h = 0; h < w; h++)if (d(a[h], h)) return a[h] }; a.keys = Object.keys; a.offset = function (a) {
        var d = C.documentElement; a = a.parentElement || a.parentNode ? a.getBoundingClientRect() : { top: 0, left: 0 }; return {
            top: a.top + (B.pageYOffset || d.scrollTop) - (d.clientTop || 0), left: a.left + (B.pageXOffset || d.scrollLeft) -
                (d.clientLeft || 0)
        }
    }; a.stop = function (h, d) { for (var t = a.timers.length; t--;)a.timers[t].elem !== h || d && d !== a.timers[t].prop || (a.timers[t].stopped = !0) }; a.objectEach = function (a, d, t) { for (var h in a) a.hasOwnProperty(h) && d.call(t || a[h], a[h], h, a) }; a.objectEach({ map: "map", each: "forEach", grep: "filter", reduce: "reduce", some: "some" }, function (h, d) { a[d] = function (a) { return Array.prototype[h].apply(a, [].slice.call(arguments, 1)) } }); a.addEvent = function (h, d, t, w) {
        var r, v = h.addEventListener || a.addEventListenerPolyfill; r = "function" ===
            typeof h && h.prototype ? h.prototype.protoEvents = h.prototype.protoEvents || {} : h.hcEvents = h.hcEvents || {}; a.Point && h instanceof a.Point && h.series && h.series.chart && (h.series.chart.runTrackerClick = !0); v && v.call(h, d, t, !1); r[d] || (r[d] = []); r[d].push(t); w && a.isNumber(w.order) && (t.order = w.order, r[d].sort(function (a, k) { return a.order - k.order })); return function () { a.removeEvent(h, d, t) }
    }; a.removeEvent = function (h, d, t) {
        function w(k, n) { var e = h.removeEventListener || a.removeEventListenerPolyfill; e && e.call(h, k, n, !1) } function r(k) {
            var n,
            e; h.nodeName && (d ? (n = {}, n[d] = !0) : n = k, a.objectEach(n, function (a, c) { if (k[c]) for (e = k[c].length; e--;)w(c, k[c][e]) }))
        } var v, p;["protoEvents", "hcEvents"].forEach(function (a) { var n = h[a]; n && (d ? (v = n[d] || [], t ? (p = v.indexOf(t), -1 < p && (v.splice(p, 1), n[d] = v), w(d, t)) : (r(n), n[d] = [])) : (r(n), h[a] = {})) })
    }; a.fireEvent = function (h, d, t, w) {
        var r, v, p, k, n; t = t || {}; C.createEvent && (h.dispatchEvent || h.fireEvent) ? (r = C.createEvent("Events"), r.initEvent(d, !0, !0), a.extend(r, t), h.dispatchEvent ? h.dispatchEvent(r) : h.fireEvent(d, r)) : ["protoEvents",
            "hcEvents"].forEach(function (e) { if (h[e]) for (v = h[e][d] || [], p = v.length, t.target || a.extend(t, { preventDefault: function () { t.defaultPrevented = !0 }, target: h, type: d }), k = 0; k < p; k++)(n = v[k]) && !1 === n.call(h, t) && t.preventDefault() }); w && !t.defaultPrevented && w.call(h, t)
    }; a.animate = function (h, d, t) {
        var w, r = "", v, p, k; a.isObject(t) || (k = arguments, t = { duration: k[2], easing: k[3], complete: k[4] }); a.isNumber(t.duration) || (t.duration = 400); t.easing = "function" === typeof t.easing ? t.easing : Math[t.easing] || Math.easeInOutSine; t.curAnim =
            a.merge(d); a.objectEach(d, function (n, e) { a.stop(h, e); p = new a.Fx(h, t, e); v = null; "d" === e ? (p.paths = p.initPath(h, h.d, d.d), p.toD = d.d, w = 0, v = 1) : h.attr ? w = h.attr(e) : (w = parseFloat(a.getStyle(h, e)) || 0, "opacity" !== e && (r = "px")); v || (v = n); v && v.match && v.match("px") && (v = v.replace(/px/g, "")); p.run(w, v, r) })
    }; a.seriesType = function (h, d, t, w, r) {
        var v = a.getOptions(), p = a.seriesTypes; v.plotOptions[h] = a.merge(v.plotOptions[d], t); p[h] = a.extendClass(p[d] || function () { }, w); p[h].prototype.type = h; r && (p[h].prototype.pointClass = a.extendClass(a.Point,
            r)); return p[h]
    }; a.uniqueKey = function () { var a = Math.random().toString(36).substring(2, 9), d = 0; return function () { return "highcharts-" + a + "-" + d++ } }(); a.isFunction = function (a) { return "function" === typeof a }; B.jQuery && (B.jQuery.fn.highcharts = function () { var h = [].slice.call(arguments); if (this[0]) return h[0] ? (new (a[a.isString(h[0]) ? h.shift() : "Chart"])(this[0], h[0], h[1]), this) : z[a.attr(this[0], "data-highcharts-chart")] })
    })(H); (function (a) {
        var z = a.isNumber, C = a.merge, B = a.pInt; a.Color = function (h) {
            if (!(this instanceof
                a.Color)) return new a.Color(h); this.init(h)
        }; a.Color.prototype = {
            parsers: [{ regex: /rgba\(\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*,\s*([0-9]?(?:\.[0-9]+)?)\s*\)/, parse: function (a) { return [B(a[1]), B(a[2]), B(a[3]), parseFloat(a[4], 10)] } }, { regex: /rgb\(\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*\)/, parse: function (a) { return [B(a[1]), B(a[2]), B(a[3]), 1] } }], names: { white: "#ffffff", black: "#000000" }, init: function (h) {
                var d, t, w, r; if ((this.input = h = this.names[h && h.toLowerCase ? h.toLowerCase() :
                    ""] || h) && h.stops) this.stops = h.stops.map(function (d) { return new a.Color(d[1]) }); else if (h && h.charAt && "#" === h.charAt() && (d = h.length, h = parseInt(h.substr(1), 16), 7 === d ? t = [(h & 16711680) >> 16, (h & 65280) >> 8, h & 255, 1] : 4 === d && (t = [(h & 3840) >> 4 | (h & 3840) >> 8, (h & 240) >> 4 | h & 240, (h & 15) << 4 | h & 15, 1])), !t) for (w = this.parsers.length; w-- && !t;)r = this.parsers[w], (d = r.regex.exec(h)) && (t = r.parse(d)); this.rgba = t || []
            }, get: function (a) {
                var d = this.input, h = this.rgba, w; this.stops ? (w = C(d), w.stops = [].concat(w.stops), this.stops.forEach(function (d,
                    v) { w.stops[v] = [w.stops[v][0], d.get(a)] })) : w = h && z(h[0]) ? "rgb" === a || !a && 1 === h[3] ? "rgb(" + h[0] + "," + h[1] + "," + h[2] + ")" : "a" === a ? h[3] : "rgba(" + h.join(",") + ")" : d; return w
            }, brighten: function (a) { var d, h = this.rgba; if (this.stops) this.stops.forEach(function (d) { d.brighten(a) }); else if (z(a) && 0 !== a) for (d = 0; 3 > d; d++)h[d] += B(255 * a), 0 > h[d] && (h[d] = 0), 255 < h[d] && (h[d] = 255); return this }, setOpacity: function (a) { this.rgba[3] = a; return this }, tweenTo: function (a, d) {
                var h = this.rgba, w = a.rgba; w.length && h && h.length ? (a = 1 !== w[3] || 1 !==
                    h[3], d = (a ? "rgba(" : "rgb(") + Math.round(w[0] + (h[0] - w[0]) * (1 - d)) + "," + Math.round(w[1] + (h[1] - w[1]) * (1 - d)) + "," + Math.round(w[2] + (h[2] - w[2]) * (1 - d)) + (a ? "," + (w[3] + (h[3] - w[3]) * (1 - d)) : "") + ")") : d = a.input || "none"; return d
            }
        }; a.color = function (h) { return new a.Color(h) }
    })(H); (function (a) {
        var z = a.defined, C = a.extend, B = a.merge, h = a.pick, d = a.timeUnits, t = a.win; a.Time = function (a) { this.update(a, !1) }; a.Time.prototype = {
            defaultOptions: {}, update: function (a) {
                var d = h(a && a.useUTC, !0), v = this; this.options = a = B(!0, this.options || {}, a);
                this.Date = a.Date || t.Date; this.timezoneOffset = (this.useUTC = d) && a.timezoneOffset; this.getTimezoneOffset = this.timezoneOffsetFunction(); (this.variableTimezone = !(d && !a.getTimezoneOffset && !a.timezone)) || this.timezoneOffset ? (this.get = function (a, k) { var n = k.getTime(), e = n - v.getTimezoneOffset(k); k.setTime(e); a = k["getUTC" + a](); k.setTime(n); return a }, this.set = function (a, k, n) {
                    var e; if ("Milliseconds" === a || "Seconds" === a || "Minutes" === a && 0 === k.getTimezoneOffset() % 60) k["set" + a](n); else e = v.getTimezoneOffset(k), e = k.getTime() -
                        e, k.setTime(e), k["setUTC" + a](n), a = v.getTimezoneOffset(k), e = k.getTime() + a, k.setTime(e)
                }) : d ? (this.get = function (a, k) { return k["getUTC" + a]() }, this.set = function (a, k, n) { return k["setUTC" + a](n) }) : (this.get = function (a, k) { return k["get" + a]() }, this.set = function (a, k, n) { return k["set" + a](n) })
            }, makeTime: function (d, r, v, p, k, n) {
                var e, m, c; this.useUTC ? (e = this.Date.UTC.apply(0, arguments), m = this.getTimezoneOffset(e), e += m, c = this.getTimezoneOffset(e), m !== c ? e += c - m : m - 36E5 !== this.getTimezoneOffset(e - 36E5) || a.isSafari || (e -=
                    36E5)) : e = (new this.Date(d, r, h(v, 1), h(p, 0), h(k, 0), h(n, 0))).getTime(); return e
            }, timezoneOffsetFunction: function () { var d = this, h = this.options, v = t.moment; if (!this.useUTC) return function (a) { return 6E4 * (new Date(a)).getTimezoneOffset() }; if (h.timezone) { if (v) return function (a) { return 6E4 * -v.tz(a, h.timezone).utcOffset() }; a.error(25) } return this.useUTC && h.getTimezoneOffset ? function (a) { return 6E4 * h.getTimezoneOffset(a) } : function () { return 6E4 * (d.timezoneOffset || 0) } }, dateFormat: function (d, h, v) {
                if (!a.defined(h) ||
                    isNaN(h)) return a.defaultOptions.lang.invalidDate || ""; d = a.pick(d, "%Y-%m-%d %H:%M:%S"); var p = this, k = new this.Date(h), n = this.get("Hours", k), e = this.get("Day", k), m = this.get("Date", k), c = this.get("Month", k), g = this.get("FullYear", k), b = a.defaultOptions.lang, q = b.weekdays, u = b.shortWeekdays, x = a.pad, k = a.extend({
                        a: u ? u[e] : q[e].substr(0, 3), A: q[e], d: x(m), e: x(m, 2, " "), w: e, b: b.shortMonths[c], B: b.months[c], m: x(c + 1), o: c + 1, y: g.toString().substr(2, 2), Y: g, H: x(n), k: n, I: x(n % 12 || 12), l: n % 12 || 12, M: x(p.get("Minutes", k)), p: 12 >
                            n ? "AM" : "PM", P: 12 > n ? "am" : "pm", S: x(k.getSeconds()), L: x(Math.floor(h % 1E3), 3)
                    }, a.dateFormats); a.objectEach(k, function (a, b) { for (; -1 !== d.indexOf("%" + b);)d = d.replace("%" + b, "function" === typeof a ? a.call(p, h) : a) }); return v ? d.substr(0, 1).toUpperCase() + d.substr(1) : d
            }, resolveDTLFormat: function (d) { return a.isObject(d, !0) ? d : (d = a.splat(d), { main: d[0], from: d[1], to: d[2] }) }, getTimeTicks: function (a, r, v, p) {
                var k = this, n = [], e, m = {}, c; e = new k.Date(r); var g = a.unitRange, b = a.count || 1, q; p = h(p, 1); if (z(r)) {
                    k.set("Milliseconds",
                        e, g >= d.second ? 0 : b * Math.floor(k.get("Milliseconds", e) / b)); g >= d.second && k.set("Seconds", e, g >= d.minute ? 0 : b * Math.floor(k.get("Seconds", e) / b)); g >= d.minute && k.set("Minutes", e, g >= d.hour ? 0 : b * Math.floor(k.get("Minutes", e) / b)); g >= d.hour && k.set("Hours", e, g >= d.day ? 0 : b * Math.floor(k.get("Hours", e) / b)); g >= d.day && k.set("Date", e, g >= d.month ? 1 : Math.max(1, b * Math.floor(k.get("Date", e) / b))); g >= d.month && (k.set("Month", e, g >= d.year ? 0 : b * Math.floor(k.get("Month", e) / b)), c = k.get("FullYear", e)); g >= d.year && k.set("FullYear", e, c -
                            c % b); g === d.week && (c = k.get("Day", e), k.set("Date", e, k.get("Date", e) - c + p + (c < p ? -7 : 0))); c = k.get("FullYear", e); p = k.get("Month", e); var u = k.get("Date", e), x = k.get("Hours", e); r = e.getTime(); k.variableTimezone && (q = v - r > 4 * d.month || k.getTimezoneOffset(r) !== k.getTimezoneOffset(v)); r = e.getTime(); for (e = 1; r < v;)n.push(r), r = g === d.year ? k.makeTime(c + e * b, 0) : g === d.month ? k.makeTime(c, p + e * b) : !q || g !== d.day && g !== d.week ? q && g === d.hour && 1 < b ? k.makeTime(c, p, u, x + e * b) : r + g * b : k.makeTime(c, p, u + e * b * (g === d.day ? 1 : 7)), e++; n.push(r); g <= d.hour &&
                                1E4 > n.length && n.forEach(function (a) { 0 === a % 18E5 && "000000000" === k.dateFormat("%H%M%S%L", a) && (m[a] = "day") })
                } n.info = C(a, { higherRanks: m, totalRange: g * b }); return n
            }
        }
    })(H); (function (a) {
        var z = a.color, C = a.merge; a.defaultOptions = {
            colors: "#7cb5ec #434348 #90ed7d #f7a35c #8085e9 #f15c80 #e4d354 #2b908f #f45b5b #91e8e1".split(" "), symbols: ["circle", "diamond", "square", "triangle", "triangle-down"], lang: {
                loading: "Loading...", months: "January February March April May June July August September October November December".split(" "),
                shortMonths: "Jan Feb Mar Apr May Jun Jul Aug Sep Oct Nov Dec".split(" "), weekdays: "Sunday Monday Tuesday Wednesday Thursday Friday Saturday".split(" "), decimalPoint: ".", numericSymbols: "kMGTPE".split(""), resetZoom: "Reset zoom", resetZoomTitle: "Reset zoom level 1:1", thousandsSep: " "
            }, global: {}, time: a.Time.prototype.defaultOptions, chart: {
                styledMode: !1, borderRadius: 0, colorCount: 10, defaultSeriesType: "line", ignoreHiddenSeries: !0, spacing: [10, 10, 15, 10], resetZoomButton: {
                    theme: { zIndex: 6 }, position: {
                        align: "right",
                        x: -10, y: 10
                    }
                }, width: null, height: null, borderColor: "#335cad", backgroundColor: "#ffffff", plotBorderColor: "#cccccc"
            }, title: { text: "Chart title", align: "center", margin: 15, widthAdjust: -44 }, subtitle: { text: "", align: "center", widthAdjust: -44 }, plotOptions: {}, labels: { style: { position: "absolute", color: "#333333" } }, legend: {
                enabled: !0, align: "center", alignColumns: !0, layout: "horizontal", labelFormatter: function () { return this.name }, borderColor: "#999999", borderRadius: 0, navigation: { activeColor: "#003399", inactiveColor: "#cccccc" },
                itemStyle: { color: "#333333", cursor: "pointer", fontSize: "12px", fontWeight: "bold", textOverflow: "ellipsis" }, itemHoverStyle: { color: "#000000" }, itemHiddenStyle: { color: "#cccccc" }, shadow: !1, itemCheckboxStyle: { position: "absolute", width: "13px", height: "13px" }, squareSymbol: !0, symbolPadding: 5, verticalAlign: "bottom", x: 0, y: 0, title: { style: { fontWeight: "bold" } }
            }, loading: { labelStyle: { fontWeight: "bold", position: "relative", top: "45%" }, style: { position: "absolute", backgroundColor: "#ffffff", opacity: .5, textAlign: "center" } }, tooltip: {
                enabled: !0,
                animation: a.svg, borderRadius: 3, dateTimeLabelFormats: { millisecond: "%A, %b %e, %H:%M:%S.%L", second: "%A, %b %e, %H:%M:%S", minute: "%A, %b %e, %H:%M", hour: "%A, %b %e, %H:%M", day: "%A, %b %e, %Y", week: "Week from %A, %b %e, %Y", month: "%B %Y", year: "%Y" }, footerFormat: "", padding: 8, snap: a.isTouchDevice ? 25 : 10, headerFormat: '\x3cspan style\x3d"font-size: 10px"\x3e{point.key}\x3c/span\x3e\x3cbr/\x3e', pointFormat: '\x3cspan style\x3d"color:{point.color}"\x3e\u25cf\x3c/span\x3e {series.name}: \x3cb\x3e{point.y}\x3c/b\x3e\x3cbr/\x3e',
                backgroundColor: z("#f7f7f7").setOpacity(.85).get(), borderWidth: 1, shadow: !0, style: { color: "#333333", cursor: "default", fontSize: "12px", pointerEvents: "none", whiteSpace: "nowrap" }
            }, credits: { enabled: !0, href: "https://www.highcharts.com?credits", position: { align: "right", x: -10, verticalAlign: "bottom", y: -5 }, style: { cursor: "pointer", color: "#999999", fontSize: "9px" ,visibility:"hidden" }, text: "" }
        }; a.setOptions = function (B) {
        a.defaultOptions = C(!0, a.defaultOptions, B); a.time.update(C(a.defaultOptions.global, a.defaultOptions.time),
            !1); return a.defaultOptions
        }; a.getOptions = function () { return a.defaultOptions }; a.defaultPlotOptions = a.defaultOptions.plotOptions; a.time = new a.Time(C(a.defaultOptions.global, a.defaultOptions.time)); a.dateFormat = function (B, h, d) { return a.time.dateFormat(B, h, d) }
    })(H); (function (a) {
        var z, C, B = a.addEvent, h = a.animate, d = a.attr, t = a.charts, w = a.color, r = a.css, v = a.createElement, p = a.defined, k = a.deg2rad, n = a.destroyObjectProperties, e = a.doc, m = a.extend, c = a.erase, g = a.hasTouch, b = a.isArray, q = a.isFirefox, u = a.isMS, x = a.isObject,
        D = a.isString, E = a.isWebKit, l = a.merge, A = a.noop, F = a.objectEach, G = a.pick, f = a.pInt, y = a.removeEvent, L = a.splat, J = a.stop, T = a.svg, K = a.SVG_NS, M = a.symbolSizes, Q = a.win; z = a.SVGElement = function () { return this }; m(z.prototype, {
            opacity: 1, SVG_NS: K, textProps: "direction fontSize fontWeight fontFamily fontStyle color lineHeight width textAlign textDecoration textOverflow textOutline cursor".split(" "), init: function (a, f) { this.element = "span" === f ? v(f) : e.createElementNS(this.SVG_NS, f); this.renderer = a }, animate: function (f, b,
                c) { b = a.animObject(G(b, this.renderer.globalAnimation, !0)); 0 !== b.duration ? (c && (b.complete = c), h(this, f, b)) : (this.attr(f, null, c), b.step && b.step.call(this)); return this }, complexColor: function (f, c, y) {
                    var I = this.renderer, g, m, e, q, u, A, d, n, K, k, N, x = [], J; a.fireEvent(this.renderer, "complexColor", { args: arguments }, function () {
                        f.radialGradient ? m = "radialGradient" : f.linearGradient && (m = "linearGradient"); m && (e = f[m], u = I.gradients, d = f.stops, k = y.radialReference, b(e) && (f[m] = e = { x1: e[0], y1: e[1], x2: e[2], y2: e[3], gradientUnits: "userSpaceOnUse" }),
                            "radialGradient" === m && k && !p(e.gradientUnits) && (q = e, e = l(e, I.getRadialAttr(k, q), { gradientUnits: "userSpaceOnUse" })), F(e, function (a, f) { "id" !== f && x.push(f, a) }), F(d, function (a) { x.push(a) }), x = x.join(","), u[x] ? N = u[x].attr("id") : (e.id = N = a.uniqueKey(), u[x] = A = I.createElement(m).attr(e).add(I.defs), A.radAttr = q, A.stops = [], d.forEach(function (f) {
                                0 === f[1].indexOf("rgba") ? (g = a.color(f[1]), n = g.get("rgb"), K = g.get("a")) : (n = f[1], K = 1); f = I.createElement("stop").attr({ offset: f[0], "stop-color": n, "stop-opacity": K }).add(A);
                                A.stops.push(f)
                            })), J = "url(" + I.url + "#" + N + ")", y.setAttribute(c, J), y.gradient = x, f.toString = function () { return J })
                    })
                }, applyTextOutline: function (f) {
                    var b = this.element, I, y, l, g, e; -1 !== f.indexOf("contrast") && (f = f.replace(/contrast/g, this.renderer.getContrast(b.style.fill))); f = f.split(" "); y = f[f.length - 1]; if ((l = f[0]) && "none" !== l && a.svg) {
                    this.fakeTS = !0; f = [].slice.call(b.getElementsByTagName("tspan")); this.ySetter = this.xSetter; l = l.replace(/(^[\d\.]+)(.*?)$/g, function (a, f, b) { return 2 * f + b }); for (e = f.length; e--;)I =
                        f[e], "highcharts-text-outline" === I.getAttribute("class") && c(f, b.removeChild(I)); g = b.firstChild; f.forEach(function (a, f) { 0 === f && (a.setAttribute("x", b.getAttribute("x")), f = b.getAttribute("y"), a.setAttribute("y", f || 0), null === f && b.setAttribute("y", 0)); a = a.cloneNode(1); d(a, { "class": "highcharts-text-outline", fill: y, stroke: y, "stroke-width": l, "stroke-linejoin": "round" }); b.insertBefore(a, g) })
                    }
                }, symbolCustomAttribs: "x y width height r start end innerR anchorX anchorY rounded".split(" "), attr: function (f, b, c,
                    y) {
                        var I, l = this.element, g, e = this, m, q, u = this.symbolCustomAttribs; "string" === typeof f && void 0 !== b && (I = f, f = {}, f[I] = b); "string" === typeof f ? e = (this[f + "Getter"] || this._defaultGetter).call(this, f, l) : (F(f, function (b, c) {
                            m = !1; y || J(this, c); this.symbolName && -1 !== a.inArray(c, u) && (g || (this.symbolAttr(f), g = !0), m = !0); !this.rotation || "x" !== c && "y" !== c || (this.doTransform = !0); m || (q = this[c + "Setter"] || this._defaultSetter, q.call(this, b, c, l), !this.styledMode && this.shadows && /^(width|height|visibility|x|y|d|transform|cx|cy|r)$/.test(c) &&
                                this.updateShadows(c, b, q))
                        }, this), this.afterSetters()); c && c.call(this); return e
                }, afterSetters: function () { this.doTransform && (this.updateTransform(), this.doTransform = !1) }, updateShadows: function (a, f, b) { for (var c = this.shadows, y = c.length; y--;)b.call(c[y], "height" === a ? Math.max(f - (c[y].cutHeight || 0), 0) : "d" === a ? this.d : f, a, c[y]) }, addClass: function (a, f) { var b = this.attr("class") || ""; -1 === b.indexOf(a) && (f || (a = (b + (b ? " " : "") + a).replace("  ", " ")), this.attr("class", a)); return this }, hasClass: function (a) {
                    return -1 !==
                        (this.attr("class") || "").split(" ").indexOf(a)
                }, removeClass: function (a) { return this.attr("class", (this.attr("class") || "").replace(a, "")) }, symbolAttr: function (a) { var f = this; "x y r start end width height innerR anchorX anchorY".split(" ").forEach(function (b) { f[b] = G(a[b], f[b]) }); f.attr({ d: f.renderer.symbols[f.symbolName](f.x, f.y, f.width, f.height, f) }) }, clip: function (a) { return this.attr("clip-path", a ? "url(" + this.renderer.url + "#" + a.id + ")" : "none") }, crisp: function (a, f) {
                    var b; f = f || a.strokeWidth || 0; b = Math.round(f) %
                        2 / 2; a.x = Math.floor(a.x || this.x || 0) + b; a.y = Math.floor(a.y || this.y || 0) + b; a.width = Math.floor((a.width || this.width || 0) - 2 * b); a.height = Math.floor((a.height || this.height || 0) - 2 * b); p(a.strokeWidth) && (a.strokeWidth = f); return a
                }, css: function (a) {
                    var b = this.styles, c = {}, y = this.element, l, g = "", I, e = !b, q = ["textOutline", "textOverflow", "width"]; a && a.color && (a.fill = a.color); b && F(a, function (a, f) { a !== b[f] && (c[f] = a, e = !0) }); e && (b && (a = m(b, c)), a && (null === a.width || "auto" === a.width ? delete this.textWidth : "text" === y.nodeName.toLowerCase() &&
                        a.width && (l = this.textWidth = f(a.width))), this.styles = a, l && !T && this.renderer.forExport && delete a.width, y.namespaceURI === this.SVG_NS ? (I = function (a, f) { return "-" + f.toLowerCase() }, F(a, function (a, f) { -1 === q.indexOf(f) && (g += f.replace(/([A-Z])/g, I) + ":" + a + ";") }), g && d(y, "style", g)) : r(y, a), this.added && ("text" === this.element.nodeName && this.renderer.buildText(this), a && a.textOutline && this.applyTextOutline(a.textOutline))); return this
                }, getStyle: function (a) { return Q.getComputedStyle(this.element || this, "").getPropertyValue(a) },
            strokeWidth: function () { if (!this.renderer.styledMode) return this["stroke-width"] || 0; var a = this.getStyle("stroke-width"), b; a.indexOf("px") === a.length - 2 ? a = f(a) : (b = e.createElementNS(K, "rect"), d(b, { width: a, "stroke-width": 0 }), this.element.parentNode.appendChild(b), a = b.getBBox().width, b.parentNode.removeChild(b)); return a }, on: function (a, f) {
                var b = this, c = b.element; g && "click" === a ? (c.ontouchstart = function (a) { b.touchEventFired = Date.now(); a.preventDefault(); f.call(c, a) }, c.onclick = function (a) {
                (-1 === Q.navigator.userAgent.indexOf("Android") ||
                    1100 < Date.now() - (b.touchEventFired || 0)) && f.call(c, a)
                }) : c["on" + a] = f; return this
            }, setRadialReference: function (a) { var f = this.renderer.gradients[this.element.gradient]; this.element.radialReference = a; f && f.radAttr && f.animate(this.renderer.getRadialAttr(a, f.radAttr)); return this }, translate: function (a, f) { return this.attr({ translateX: a, translateY: f }) }, invert: function (a) { this.inverted = a; this.updateTransform(); return this }, updateTransform: function () {
                var a = this.translateX || 0, f = this.translateY || 0, b = this.scaleX,
                c = this.scaleY, y = this.inverted, l = this.rotation, g = this.matrix, e = this.element; y && (a += this.width, f += this.height); a = ["translate(" + a + "," + f + ")"]; p(g) && a.push("matrix(" + g.join(",") + ")"); y ? a.push("rotate(90) scale(-1,1)") : l && a.push("rotate(" + l + " " + G(this.rotationOriginX, e.getAttribute("x"), 0) + " " + G(this.rotationOriginY, e.getAttribute("y") || 0) + ")"); (p(b) || p(c)) && a.push("scale(" + G(b, 1) + " " + G(c, 1) + ")"); a.length && e.setAttribute("transform", a.join(" "))
            }, toFront: function () {
                var a = this.element; a.parentNode.appendChild(a);
                return this
            }, align: function (a, f, b) {
                var y, l, g, e, m = {}; l = this.renderer; g = l.alignedObjects; var I, q; if (a) { if (this.alignOptions = a, this.alignByTranslate = f, !b || D(b)) this.alignTo = y = b || "renderer", c(g, this), g.push(this), b = null } else a = this.alignOptions, f = this.alignByTranslate, y = this.alignTo; b = G(b, l[y], l); y = a.align; l = a.verticalAlign; g = (b.x || 0) + (a.x || 0); e = (b.y || 0) + (a.y || 0); "right" === y ? I = 1 : "center" === y && (I = 2); I && (g += (b.width - (a.width || 0)) / I); m[f ? "translateX" : "x"] = Math.round(g); "bottom" === l ? q = 1 : "middle" === l && (q =
                    2); q && (e += (b.height - (a.height || 0)) / q); m[f ? "translateY" : "y"] = Math.round(e); this[this.placed ? "animate" : "attr"](m); this.placed = !0; this.alignAttr = m; return this
            }, getBBox: function (a, f) {
                var b, c = this.renderer, y, l = this.element, g = this.styles, e, I = this.textStr, q, u = c.cache, A = c.cacheKeys, d = l.namespaceURI === this.SVG_NS, n; f = G(f, this.rotation); y = f * k; e = c.styledMode ? l && z.prototype.getStyle.call(l, "font-size") : g && g.fontSize; p(I) && (n = I.toString(), -1 === n.indexOf("\x3c") && (n = n.replace(/[0-9]/g, "0")), n += ["", f || 0, e, this.textWidth,
                    g && g.textOverflow].join()); n && !a && (b = u[n]); if (!b) {
                        if (d || c.forExport) { try { (q = this.fakeTS && function (a) { [].forEach.call(l.querySelectorAll(".highcharts-text-outline"), function (f) { f.style.display = a }) }) && q("none"), b = l.getBBox ? m({}, l.getBBox()) : { width: l.offsetWidth, height: l.offsetHeight }, q && q("") } catch (X) { } if (!b || 0 > b.width) b = { width: 0, height: 0 } } else b = this.htmlGetBBox(); c.isSVG && (a = b.width, c = b.height, d && (b.height = c = { "11px,17": 14, "13px,20": 16 }[g && g.fontSize + "," + Math.round(c)] || c), f && (b.width = Math.abs(c * Math.sin(y)) +
                            Math.abs(a * Math.cos(y)), b.height = Math.abs(c * Math.cos(y)) + Math.abs(a * Math.sin(y)))); if (n && 0 < b.height) { for (; 250 < A.length;)delete u[A.shift()]; u[n] || A.push(n); u[n] = b }
                    } return b
            }, show: function (a) { return this.attr({ visibility: a ? "inherit" : "visible" }) }, hide: function () { return this.attr({ visibility: "hidden" }) }, fadeOut: function (a) { var f = this; f.animate({ opacity: 0 }, { duration: a || 150, complete: function () { f.attr({ y: -9999 }) } }) }, add: function (a) {
                var f = this.renderer, b = this.element, c; a && (this.parentGroup = a); this.parentInverted =
                    a && a.inverted; void 0 !== this.textStr && f.buildText(this); this.added = !0; if (!a || a.handleZ || this.zIndex) c = this.zIndexSetter(); c || (a ? a.element : f.box).appendChild(b); if (this.onAdd) this.onAdd(); return this
            }, safeRemoveChild: function (a) { var f = a.parentNode; f && f.removeChild(a) }, destroy: function () {
                var a = this, f = a.element || {}, b = a.renderer, y = b.isSVG && "SPAN" === f.nodeName && a.parentGroup, l = f.ownerSVGElement, g = a.clipPath; f.onclick = f.onmouseout = f.onmouseover = f.onmousemove = f.point = null; J(a); g && l && ([].forEach.call(l.querySelectorAll("[clip-path],[CLIP-PATH]"),
                    function (a) { var f = a.getAttribute("clip-path"), b = g.element.id; (-1 < f.indexOf("(#" + b + ")") || -1 < f.indexOf('("#' + b + '")')) && a.removeAttribute("clip-path") }), a.clipPath = g.destroy()); if (a.stops) { for (l = 0; l < a.stops.length; l++)a.stops[l] = a.stops[l].destroy(); a.stops = null } a.safeRemoveChild(f); for (b.styledMode || a.destroyShadows(); y && y.div && 0 === y.div.childNodes.length;)f = y.parentGroup, a.safeRemoveChild(y.div), delete y.div, y = f; a.alignTo && c(b.alignedObjects, a); F(a, function (f, b) { delete a[b] }); return null
            }, shadow: function (a,
                f, b) {
                    var c = [], y, l, g = this.element, e, m, q, I; if (!a) this.destroyShadows(); else if (!this.shadows) {
                        m = G(a.width, 3); q = (a.opacity || .15) / m; I = this.parentInverted ? "(-1,-1)" : "(" + G(a.offsetX, 1) + ", " + G(a.offsetY, 1) + ")"; for (y = 1; y <= m; y++)l = g.cloneNode(0), e = 2 * m + 1 - 2 * y, d(l, { stroke: a.color || "#000000", "stroke-opacity": q * y, "stroke-width": e, transform: "translate" + I, fill: "none" }), l.setAttribute("class", (l.getAttribute("class") || "") + " highcharts-shadow"), b && (d(l, "height", Math.max(d(l, "height") - e, 0)), l.cutHeight = e), f ? f.element.appendChild(l) :
                            g.parentNode && g.parentNode.insertBefore(l, g), c.push(l); this.shadows = c
                    } return this
            }, destroyShadows: function () { (this.shadows || []).forEach(function (a) { this.safeRemoveChild(a) }, this); this.shadows = void 0 }, xGetter: function (a) { "circle" === this.element.nodeName && ("x" === a ? a = "cx" : "y" === a && (a = "cy")); return this._defaultGetter(a) }, _defaultGetter: function (a) { a = G(this[a + "Value"], this[a], this.element ? this.element.getAttribute(a) : null, 0); /^[\-0-9\.]+$/.test(a) && (a = parseFloat(a)); return a }, dSetter: function (a, f, b) {
            a &&
                a.join && (a = a.join(" ")); /(NaN| {2}|^$)/.test(a) && (a = "M 0 0"); this[f] !== a && (b.setAttribute(f, a), this[f] = a)
            }, dashstyleSetter: function (a) {
                var b, c = this["stroke-width"]; "inherit" === c && (c = 1); if (a = a && a.toLowerCase()) {
                    a = a.replace("shortdashdotdot", "3,1,1,1,1,1,").replace("shortdashdot", "3,1,1,1").replace("shortdot", "1,1,").replace("shortdash", "3,1,").replace("longdash", "8,3,").replace(/dot/g, "1,3,").replace("dash", "4,3,").replace(/,$/, "").split(","); for (b = a.length; b--;)a[b] = f(a[b]) * c; a = a.join(",").replace(/NaN/g,
                        "none"); this.element.setAttribute("stroke-dasharray", a)
                }
            }, alignSetter: function (a) { this.alignValue = a; this.element.setAttribute("text-anchor", { left: "start", center: "middle", right: "end" }[a]) }, opacitySetter: function (a, f, b) { this[f] = a; b.setAttribute(f, a) }, titleSetter: function (a) {
                var f = this.element.getElementsByTagName("title")[0]; f || (f = e.createElementNS(this.SVG_NS, "title"), this.element.appendChild(f)); f.firstChild && f.removeChild(f.firstChild); f.appendChild(e.createTextNode(String(G(a), "").replace(/<[^>]*>/g,
                    "").replace(/&lt;/g, "\x3c").replace(/&gt;/g, "\x3e")))
            }, textSetter: function (a) { a !== this.textStr && (delete this.bBox, this.textStr = a, this.added && this.renderer.buildText(this)) }, fillSetter: function (a, f, b) { "string" === typeof a ? b.setAttribute(f, a) : a && this.complexColor(a, f, b) }, visibilitySetter: function (a, f, b) { "inherit" === a ? b.removeAttribute(f) : this[f] !== a && b.setAttribute(f, a); this[f] = a }, zIndexSetter: function (a, b) {
                var c = this.renderer, y = this.parentGroup, l = (y || c).element || c.box, g, e = this.element, m, q, c = l === c.box;
                g = this.added; var u; p(a) ? (e.setAttribute("data-z-index", a), a = +a, this[b] === a && (g = !1)) : p(this[b]) && e.removeAttribute("data-z-index"); this[b] = a; if (g) { (a = this.zIndex) && y && (y.handleZ = !0); b = l.childNodes; for (u = b.length - 1; 0 <= u && !m; u--)if (y = b[u], g = y.getAttribute("data-z-index"), q = !p(g), y !== e) if (0 > a && q && !c && !u) l.insertBefore(e, b[u]), m = !0; else if (f(g) <= a || q && (!p(a) || 0 <= a)) l.insertBefore(e, b[u + 1] || null), m = !0; m || (l.insertBefore(e, b[c ? 3 : 0] || null), m = !0) } return m
            }, _defaultSetter: function (a, f, b) { b.setAttribute(f, a) }
        });
        z.prototype.yGetter = z.prototype.xGetter; z.prototype.translateXSetter = z.prototype.translateYSetter = z.prototype.rotationSetter = z.prototype.verticalAlignSetter = z.prototype.rotationOriginXSetter = z.prototype.rotationOriginYSetter = z.prototype.scaleXSetter = z.prototype.scaleYSetter = z.prototype.matrixSetter = function (a, f) { this[f] = a; this.doTransform = !0 }; z.prototype["stroke-widthSetter"] = z.prototype.strokeSetter = function (a, f, b) {
        this[f] = a; this.stroke && this["stroke-width"] ? (z.prototype.fillSetter.call(this, this.stroke,
            "stroke", b), b.setAttribute("stroke-width", this["stroke-width"]), this.hasStroke = !0) : "stroke-width" === f && 0 === a && this.hasStroke && (b.removeAttribute("stroke"), this.hasStroke = !1)
        }; C = a.SVGRenderer = function () { this.init.apply(this, arguments) }; m(C.prototype, {
            Element: z, SVG_NS: K, init: function (a, f, b, c, y, l, g) {
                var m; m = this.createElement("svg").attr({ version: "1.1", "class": "highcharts-root" }); g || m.css(this.getStyle(c)); c = m.element; a.appendChild(c); d(a, "dir", "ltr"); -1 === a.innerHTML.indexOf("xmlns") && d(c, "xmlns", this.SVG_NS);
                this.isSVG = !0; this.box = c; this.boxWrapper = m; this.alignedObjects = []; this.url = (q || E) && e.getElementsByTagName("base").length ? Q.location.href.split("#")[0].replace(/<[^>]*>/g, "").replace(/([\('\)])/g, "\\$1").replace(/ /g, "%20") : ""; this.createElement("desc").add().element.appendChild(e.createTextNode("Created with Highmaps 7.0.1")); this.defs = this.createElement("defs").add(); this.allowHTML = l; this.forExport = y; this.styledMode = g; this.gradients = {}; this.cache = {}; this.cacheKeys = []; this.imgCount = 0; this.setSize(f,
                    b, !1); var u; q && a.getBoundingClientRect && (f = function () { r(a, { left: 0, top: 0 }); u = a.getBoundingClientRect(); r(a, { left: Math.ceil(u.left) - u.left + "px", top: Math.ceil(u.top) - u.top + "px" }) }, f(), this.unSubPixelFix = B(Q, "resize", f))
            }, definition: function (a) {
                function f(a, c) {
                    var y; L(a).forEach(function (a) {
                        var l = b.createElement(a.tagName), g = {}; F(a, function (a, f) { "tagName" !== f && "children" !== f && "textContent" !== f && (g[f] = a) }); l.attr(g); l.add(c || b.defs); a.textContent && l.element.appendChild(e.createTextNode(a.textContent));
                        f(a.children || [], l); y = l
                    }); return y
                } var b = this; return f(a)
            }, getStyle: function (a) { return this.style = m({ fontFamily: '"Lucida Grande", "Lucida Sans Unicode", Arial, Helvetica, sans-serif', fontSize: "12px" }, a) }, setStyle: function (a) { this.boxWrapper.css(this.getStyle(a)) }, isHidden: function () { return !this.boxWrapper.getBBox().width }, destroy: function () {
                var a = this.defs; this.box = null; this.boxWrapper = this.boxWrapper.destroy(); n(this.gradients || {}); this.gradients = null; a && (this.defs = a.destroy()); this.unSubPixelFix &&
                    this.unSubPixelFix(); return this.alignedObjects = null
            }, createElement: function (a) { var f = new this.Element; f.init(this, a); return f }, draw: A, getRadialAttr: function (a, f) { return { cx: a[0] - a[2] / 2 + f.cx * a[2], cy: a[1] - a[2] / 2 + f.cy * a[2], r: f.r * a[2] } }, truncate: function (a, f, b, c, y, l, g) {
                var m = this, q = a.rotation, u, A = c ? 1 : 0, n = (b || c).length, d = n, K = [], k = function (a) { f.firstChild && f.removeChild(f.firstChild); a && f.appendChild(e.createTextNode(a)) }, x = function (l, e) {
                    e = e || l; if (void 0 === K[e]) if (f.getSubStringLength) try {
                    K[e] = y + f.getSubStringLength(0,
                        c ? e + 1 : e)
                    } catch (Y) { } else m.getSpanWidth && (k(g(b || c, l)), K[e] = y + m.getSpanWidth(a, f)); return K[e]
                }, I, J; a.rotation = 0; I = x(f.textContent.length); if (J = y + I > l) { for (; A <= n;)d = Math.ceil((A + n) / 2), c && (u = g(c, d)), I = x(d, u && u.length - 1), A === n ? A = n + 1 : I > l ? n = d - 1 : A = d; 0 === n ? k("") : b && n === b.length - 1 || k(u || g(b || c, d)) } c && c.splice(0, d); a.actualWidth = I; a.rotation = q; return J
            }, escapes: { "\x26": "\x26amp;", "\x3c": "\x26lt;", "\x3e": "\x26gt;", "'": "\x26#39;", '"': "\x26quot;" }, buildText: function (a) {
                var b = a.element, c = this, y = c.forExport, l = G(a.textStr,
                    "").toString(), g = -1 !== l.indexOf("\x3c"), m = b.childNodes, q, u = d(b, "x"), A = a.styles, n = a.textWidth, k = A && A.lineHeight, x = A && A.textOutline, J = A && "ellipsis" === A.textOverflow, I = A && "nowrap" === A.whiteSpace, p = A && A.fontSize, L, v, h = m.length, A = n && !a.added && this.box, D = function (a) { var y; c.styledMode || (y = /(px|em)$/.test(a && a.style.fontSize) ? a.style.fontSize : p || c.style.fontSize || 12); return k ? f(k) : c.fontMetrics(y, a.getAttribute("style") ? a : b).h }, E = function (a, f) {
                        F(c.escapes, function (b, c) {
                        f && -1 !== f.indexOf(b) || (a = a.toString().replace(new RegExp(b,
                            "g"), c))
                        }); return a
                    }, M = function (a, f) { var b; b = a.indexOf("\x3c"); a = a.substring(b, a.indexOf("\x3e") - b); b = a.indexOf(f + "\x3d"); if (-1 !== b && (b = b + f.length + 1, f = a.charAt(b), '"' === f || "'" === f)) return a = a.substring(b + 1), a.substring(0, a.indexOf(f)) }; L = [l, J, I, k, x, p, n].join(); if (L !== a.textCache) {
                        for (a.textCache = L; h--;)b.removeChild(m[h]); g || x || J || n || -1 !== l.indexOf(" ") ? (A && A.appendChild(b), g ? (l = c.styledMode ? l.replace(/<(b|strong)>/g, '\x3cspan class\x3d"highcharts-strong"\x3e').replace(/<(i|em)>/g, '\x3cspan class\x3d"highcharts-emphasized"\x3e') :
                            l.replace(/<(b|strong)>/g, '\x3cspan style\x3d"font-weight:bold"\x3e').replace(/<(i|em)>/g, '\x3cspan style\x3d"font-style:italic"\x3e'), l = l.replace(/<a/g, "\x3cspan").replace(/<\/(b|strong|i|em|a)>/g, "\x3c/span\x3e").split(/<br.*?>/g)) : l = [l], l = l.filter(function (a) { return "" !== a }), l.forEach(function (f, l) {
                                var g, m = 0, A = 0; f = f.replace(/^\s+|\s+$/g, "").replace(/<span/g, "|||\x3cspan").replace(/<\/span>/g, "\x3c/span\x3e|||"); g = f.split("|||"); g.forEach(function (f) {
                                    if ("" !== f || 1 === g.length) {
                                        var k = {}, x = e.createElementNS(c.SVG_NS,
                                            "tspan"), L, F; (L = M(f, "class")) && d(x, "class", L); if (L = M(f, "style")) L = L.replace(/(;| |^)color([ :])/, "$1fill$2"), d(x, "style", L); (F = M(f, "href")) && !y && (d(x, "onclick", 'location.href\x3d"' + F + '"'), d(x, "class", "highcharts-anchor"), c.styledMode || r(x, { cursor: "pointer" })); f = E(f.replace(/<[a-zA-Z\/](.|\n)*?>/g, "") || " "); if (" " !== f) {
                                                x.appendChild(e.createTextNode(f)); m ? k.dx = 0 : l && null !== u && (k.x = u); d(x, k); b.appendChild(x); !m && v && (!T && y && r(x, { display: "block" }), d(x, "dy", D(x))); if (n) {
                                                    var h = f.replace(/([^\^])-/g, "$1- ").split(" "),
                                                    k = !I && (1 < g.length || l || 1 < h.length); F = 0; var G = D(x); if (J) q = c.truncate(a, x, f, void 0, 0, Math.max(0, n - parseInt(p || 12, 10)), function (a, f) { return a.substring(0, f) + "\u2026" }); else if (k) for (; h.length;)h.length && !I && 0 < F && (x = e.createElementNS(K, "tspan"), d(x, { dy: G, x: u }), L && d(x, "style", L), x.appendChild(e.createTextNode(h.join(" ").replace(/- /g, "-"))), b.appendChild(x)), c.truncate(a, x, null, h, 0 === F ? A : 0, n, function (a, f) { return h.slice(0, f).join(" ").replace(/- /g, "-") }), A = a.actualWidth, F++
                                                } m++
                                            }
                                    }
                                }); v = v || b.childNodes.length
                            }),
                            J && q && a.attr("title", E(a.textStr, ["\x26lt;", "\x26gt;"])), A && A.removeChild(b), x && a.applyTextOutline && a.applyTextOutline(x)) : b.appendChild(e.createTextNode(E(l)))
                    }
            }, getContrast: function (a) { a = w(a).rgba; a[0] *= 1; a[1] *= 1.2; a[2] *= .5; return 459 < a[0] + a[1] + a[2] ? "#000000" : "#FFFFFF" }, button: function (a, f, b, c, y, g, e, q, A) {
                var n = this.label(a, f, b, A, null, null, null, null, "button"), d = 0, k = this.styledMode; n.attr(l({ padding: 8, r: 2 }, y)); if (!k) {
                    var x, K, J, p; y = l({
                        fill: "#f7f7f7", stroke: "#cccccc", "stroke-width": 1, style: {
                            color: "#333333",
                            cursor: "pointer", fontWeight: "normal"
                        }
                    }, y); x = y.style; delete y.style; g = l(y, { fill: "#e6e6e6" }, g); K = g.style; delete g.style; e = l(y, { fill: "#e6ebf5", style: { color: "#000000", fontWeight: "bold" } }, e); J = e.style; delete e.style; q = l(y, { style: { color: "#cccccc" } }, q); p = q.style; delete q.style
                } B(n.element, u ? "mouseover" : "mouseenter", function () { 3 !== d && n.setState(1) }); B(n.element, u ? "mouseout" : "mouseleave", function () { 3 !== d && n.setState(d) }); n.setState = function (a) {
                1 !== a && (n.state = d = a); n.removeClass(/highcharts-button-(normal|hover|pressed|disabled)/).addClass("highcharts-button-" +
                    ["normal", "hover", "pressed", "disabled"][a || 0]); k || n.attr([y, g, e, q][a || 0]).css([x, K, J, p][a || 0])
                }; k || n.attr(y).css(m({ cursor: "default" }, x)); return n.on("click", function (a) { 3 !== d && c.call(n, a) })
            }, crispLine: function (a, f) { a[1] === a[4] && (a[1] = a[4] = Math.round(a[1]) - f % 2 / 2); a[2] === a[5] && (a[2] = a[5] = Math.round(a[2]) + f % 2 / 2); return a }, path: function (a) { var f = this.styledMode ? {} : { fill: "none" }; b(a) ? f.d = a : x(a) && m(f, a); return this.createElement("path").attr(f) }, circle: function (a, f, b) {
                a = x(a) ? a : void 0 === a ? {} : { x: a, y: f, r: b };
                f = this.createElement("circle"); f.xSetter = f.ySetter = function (a, f, b) { b.setAttribute("c" + f, a) }; return f.attr(a)
            }, arc: function (a, f, b, c, y, l) { x(a) ? (c = a, f = c.y, b = c.r, a = c.x) : c = { innerR: c, start: y, end: l }; a = this.symbol("arc", a, f, b, b, c); a.r = b; return a }, rect: function (a, f, b, c, y, l) {
                y = x(a) ? a.r : y; var g = this.createElement("rect"); a = x(a) ? a : void 0 === a ? {} : { x: a, y: f, width: Math.max(b, 0), height: Math.max(c, 0) }; this.styledMode || (void 0 !== l && (a.strokeWidth = l, a = g.crisp(a)), a.fill = "none"); y && (a.r = y); g.rSetter = function (a, f, b) {
                    d(b,
                        { rx: a, ry: a })
                }; return g.attr(a)
            }, setSize: function (a, f, b) { var c = this.alignedObjects, y = c.length; this.width = a; this.height = f; for (this.boxWrapper.animate({ width: a, height: f }, { step: function () { this.attr({ viewBox: "0 0 " + this.attr("width") + " " + this.attr("height") }) }, duration: G(b, !0) ? void 0 : 0 }); y--;)c[y].align() }, g: function (a) { var f = this.createElement("g"); return a ? f.attr({ "class": "highcharts-" + a }) : f }, image: function (a, f, b, c, y, l) {
                var g = { preserveAspectRatio: "none" }, e, q = function (a, f) {
                    a.setAttributeNS ? a.setAttributeNS("http://www.w3.org/1999/xlink",
                        "href", f) : a.setAttribute("hc-svg-href", f)
                }, u = function (f) { q(e.element, a); l.call(e, f) }; 1 < arguments.length && m(g, { x: f, y: b, width: c, height: y }); e = this.createElement("image").attr(g); l ? (q(e.element, "data:image/gif;base64,R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw\x3d\x3d"), g = new Q.Image, B(g, "load", u), g.src = a, g.complete && u({})) : q(e.element, a); return e
            }, symbol: function (a, f, b, c, y, l) {
                var g = this, q, u = /^url\((.*?)\)$/, A = u.test(a), n = !A && (this.symbols[a] ? a : "circle"), d = n && this.symbols[n], k = p(f) && d && d.call(this.symbols,
                    Math.round(f), Math.round(b), c, y, l), x, K; d ? (q = this.path(k), g.styledMode || q.attr("fill", "none"), m(q, { symbolName: n, x: f, y: b, width: c, height: y }), l && m(q, l)) : A && (x = a.match(u)[1], q = this.image(x), q.imgwidth = G(M[x] && M[x].width, l && l.width), q.imgheight = G(M[x] && M[x].height, l && l.height), K = function () { q.attr({ width: q.width, height: q.height }) }, ["width", "height"].forEach(function (a) {
                    q[a + "Setter"] = function (a, f) {
                        var b = {}, c = this["img" + f], y = "width" === f ? "translateX" : "translateY"; this[f] = a; p(c) && (this.element && this.element.setAttribute(f,
                            c), this.alignByTranslate || (b[y] = ((this[f] || 0) - c) / 2, this.attr(b)))
                    }
                    }), p(f) && q.attr({ x: f, y: b }), q.isImg = !0, p(q.imgwidth) && p(q.imgheight) ? K() : (q.attr({ width: 0, height: 0 }), v("img", {
                        onload: function () { var a = t[g.chartIndex]; 0 === this.width && (r(this, { position: "absolute", top: "-999em" }), e.body.appendChild(this)); M[x] = { width: this.width, height: this.height }; q.imgwidth = this.width; q.imgheight = this.height; q.element && K(); this.parentNode && this.parentNode.removeChild(this); g.imgCount--; if (!g.imgCount && a && a.onload) a.onload() },
                        src: x
                    }), this.imgCount++)); return q
            }, symbols: {
                circle: function (a, f, b, c) { return this.arc(a + b / 2, f + c / 2, b / 2, c / 2, { start: 0, end: 2 * Math.PI, open: !1 }) }, square: function (a, f, b, c) { return ["M", a, f, "L", a + b, f, a + b, f + c, a, f + c, "Z"] }, triangle: function (a, f, b, c) { return ["M", a + b / 2, f, "L", a + b, f + c, a, f + c, "Z"] }, "triangle-down": function (a, f, b, c) { return ["M", a, f, "L", a + b, f, a + b / 2, f + c, "Z"] }, diamond: function (a, f, b, c) { return ["M", a + b / 2, f, "L", a + b, f + c / 2, a + b / 2, f + c, a, f + c / 2, "Z"] }, arc: function (a, f, b, c, y) {
                    var l = y.start, g = y.r || b, e = y.r || c || b, m = y.end -
                        .001; b = y.innerR; c = G(y.open, .001 > Math.abs(y.end - y.start - 2 * Math.PI)); var q = Math.cos(l), u = Math.sin(l), A = Math.cos(m), m = Math.sin(m); y = .001 > y.end - l - Math.PI ? 0 : 1; g = ["M", a + g * q, f + e * u, "A", g, e, 0, y, 1, a + g * A, f + e * m]; p(b) && g.push(c ? "M" : "L", a + b * A, f + b * m, "A", b, b, 0, y, 0, a + b * q, f + b * u); g.push(c ? "" : "Z"); return g
                }, callout: function (a, f, b, c, y) {
                    var l = Math.min(y && y.r || 0, b, c), g = l + 6, e = y && y.anchorX; y = y && y.anchorY; var m; m = ["M", a + l, f, "L", a + b - l, f, "C", a + b, f, a + b, f, a + b, f + l, "L", a + b, f + c - l, "C", a + b, f + c, a + b, f + c, a + b - l, f + c, "L", a + l, f + c, "C", a, f +
                        c, a, f + c, a, f + c - l, "L", a, f + l, "C", a, f, a, f, a + l, f]; e && e > b ? y > f + g && y < f + c - g ? m.splice(13, 3, "L", a + b, y - 6, a + b + 6, y, a + b, y + 6, a + b, f + c - l) : m.splice(13, 3, "L", a + b, c / 2, e, y, a + b, c / 2, a + b, f + c - l) : e && 0 > e ? y > f + g && y < f + c - g ? m.splice(33, 3, "L", a, y + 6, a - 6, y, a, y - 6, a, f + l) : m.splice(33, 3, "L", a, c / 2, e, y, a, c / 2, a, f + l) : y && y > c && e > a + g && e < a + b - g ? m.splice(23, 3, "L", e + 6, f + c, e, f + c + 6, e - 6, f + c, a + l, f + c) : y && 0 > y && e > a + g && e < a + b - g && m.splice(3, 3, "L", e - 6, f, e, f - 6, e + 6, f, b - l, f); return m
                }
            }, clipRect: function (f, b, c, y) {
                var l = a.uniqueKey(), g = this.createElement("clipPath").attr({ id: l }).add(this.defs);
                f = this.rect(f, b, c, y, 0).add(g); f.id = l; f.clipPath = g; f.count = 0; return f
            }, text: function (a, f, b, c) { var y = {}; if (c && (this.allowHTML || !this.forExport)) return this.html(a, f, b); y.x = Math.round(f || 0); b && (y.y = Math.round(b)); p(a) && (y.text = a); a = this.createElement("text").attr(y); c || (a.xSetter = function (a, f, b) { var c = b.getElementsByTagName("tspan"), y, l = b.getAttribute(f), g; for (g = 0; g < c.length; g++)y = c[g], y.getAttribute(f) === l && y.setAttribute(f, a); b.setAttribute(f, a) }); return a }, fontMetrics: function (a, b) {
                a = this.styledMode ?
                    b && z.prototype.getStyle.call(b, "font-size") : a || b && b.style && b.style.fontSize || this.style && this.style.fontSize; a = /px/.test(a) ? f(a) : /em/.test(a) ? parseFloat(a) * (b ? this.fontMetrics(null, b.parentNode).f : 16) : 12; b = 24 > a ? a + 3 : Math.round(1.2 * a); return { h: b, b: Math.round(.8 * b), f: a }
            }, rotCorr: function (a, f, b) { var c = a; f && b && (c = Math.max(c * Math.cos(f * k), 4)); return { x: -a / 3 * Math.sin(f * k), y: c } }, label: function (f, b, c, g, e, q, u, A, n) {
                var d = this, x = d.styledMode, k = d.g("button" !== n && "label"), K = k.text = d.text("", 0, 0, u).attr({ zIndex: 1 }),
                J, L, F = 0, h = 3, v = 0, D, E, G, T, M, r = {}, I, Q, t = /^url\((.*?)\)$/.test(g), w = x || t, N = function () { return x ? J.strokeWidth() % 2 / 2 : (I ? parseInt(I, 10) : 0) % 2 / 2 }, P, B, R; n && k.addClass("highcharts-" + n); P = function () {
                    var a = K.element.style, f = {}; L = (void 0 === D || void 0 === E || M) && p(K.textStr) && K.getBBox(); k.width = (D || L.width || 0) + 2 * h + v; k.height = (E || L.height || 0) + 2 * h; Q = h + Math.min(d.fontMetrics(a && a.fontSize, K).b, L ? L.height : Infinity); w && (J || (k.box = J = d.symbols[g] || t ? d.symbol(g) : d.rect(), J.addClass(("button" === n ? "" : "highcharts-label-box") +
                        (n ? " highcharts-" + n + "-box" : "")), J.add(k), a = N(), f.x = a, f.y = (A ? -Q : 0) + a), f.width = Math.round(k.width), f.height = Math.round(k.height), J.attr(m(f, r)), r = {})
                }; B = function () { var a = v + h, f; f = A ? 0 : Q; p(D) && L && ("center" === M || "right" === M) && (a += { center: .5, right: 1 }[M] * (D - L.width)); if (a !== K.x || f !== K.y) K.attr("x", a), K.hasBoxWidthChanged && (L = K.getBBox(!0), P()), void 0 !== f && K.attr("y", f); K.x = a; K.y = f }; R = function (a, f) { J ? J.attr(a, f) : r[a] = f }; k.onAdd = function () {
                    K.add(k); k.attr({ text: f || 0 === f ? f : "", x: b, y: c }); J && p(e) && k.attr({
                        anchorX: e,
                        anchorY: q
                    })
                }; k.widthSetter = function (f) { D = a.isNumber(f) ? f : null }; k.heightSetter = function (a) { E = a }; k["text-alignSetter"] = function (a) { M = a }; k.paddingSetter = function (a) { p(a) && a !== h && (h = k.padding = a, B()) }; k.paddingLeftSetter = function (a) { p(a) && a !== v && (v = a, B()) }; k.alignSetter = function (a) { a = { left: 0, center: .5, right: 1 }[a]; a !== F && (F = a, L && k.attr({ x: G })) }; k.textSetter = function (a) { void 0 !== a && K.textSetter(a); P(); B() }; k["stroke-widthSetter"] = function (a, f) { a && (w = !0); I = this["stroke-width"] = a; R(f, a) }; x ? k.rSetter = function (a,
                    f) { R(f, a) } : k.strokeSetter = k.fillSetter = k.rSetter = function (a, f) { "r" !== f && ("fill" === f && a && (w = !0), k[f] = a); R(f, a) }; k.anchorXSetter = function (a, f) { e = k.anchorX = a; R(f, Math.round(a) - N() - G) }; k.anchorYSetter = function (a, f) { q = k.anchorY = a; R(f, a - T) }; k.xSetter = function (a) { k.x = a; F && (a -= F * ((D || L.width) + 2 * h), k["forceAnimate:x"] = !0); G = Math.round(a); k.attr("translateX", G) }; k.ySetter = function (a) { T = k.y = Math.round(a); k.attr("translateY", T) }; var C = k.css; u = {
                        css: function (a) {
                            if (a) {
                                var f = {}; a = l(a); k.textProps.forEach(function (b) {
                                void 0 !==
                                    a[b] && (f[b] = a[b], delete a[b])
                                }); K.css(f); "width" in f && P(); "fontSize" in f && (P(), B())
                            } return C.call(k, a)
                        }, getBBox: function () { return { width: L.width + 2 * h, height: L.height + 2 * h, x: L.x - h, y: L.y - h } }, destroy: function () { y(k.element, "mouseenter"); y(k.element, "mouseleave"); K && (K = K.destroy()); J && (J = J.destroy()); z.prototype.destroy.call(k); k = d = P = B = R = null }
                    }; x || (u.shadow = function (a) { a && (P(), J && J.shadow(a)); return k }); return m(k, u)
            }
        }); a.Renderer = C
    })(H); (function (a) {
        var z = a.attr, C = a.createElement, B = a.css, h = a.defined, d =
            a.extend, t = a.isFirefox, w = a.isMS, r = a.isWebKit, v = a.pick, p = a.pInt, k = a.SVGRenderer, n = a.win, e = a.wrap; d(a.SVGElement.prototype, {
                htmlCss: function (a) { var c = "SPAN" === this.element.tagName && a && "width" in a, g = v(c && a.width, void 0), b; c && (delete a.width, this.textWidth = g, b = !0); a && "ellipsis" === a.textOverflow && (a.whiteSpace = "nowrap", a.overflow = "hidden"); this.styles = d(this.styles, a); B(this.element, a); b && this.htmlUpdateTransform(); return this }, htmlGetBBox: function () {
                    var a = this.element; return {
                        x: a.offsetLeft, y: a.offsetTop,
                        width: a.offsetWidth, height: a.offsetHeight
                    }
                }, htmlUpdateTransform: function () {
                    if (this.added) {
                        var a = this.renderer, c = this.element, g = this.translateX || 0, b = this.translateY || 0, e = this.x || 0, u = this.y || 0, k = this.textAlign || "left", n = { left: 0, center: .5, right: 1 }[k], d = this.styles, l = d && d.whiteSpace; B(c, { marginLeft: g, marginTop: b }); !a.styledMode && this.shadows && this.shadows.forEach(function (a) { B(a, { marginLeft: g + 1, marginTop: b + 1 }) }); this.inverted && c.childNodes.forEach(function (f) { a.invertChild(f, c) }); if ("SPAN" === c.tagName) {
                            var d =
                                this.rotation, A = this.textWidth && p(this.textWidth), F = [d, k, c.innerHTML, this.textWidth, this.textAlign].join(), v; (v = A !== this.oldTextWidth) && !(v = A > this.oldTextWidth) && ((v = this.textPxLength) || (B(c, { width: "", whiteSpace: l || "nowrap" }), v = c.offsetWidth), v = v > A); v && (/[ \-]/.test(c.textContent || c.innerText) || "ellipsis" === c.style.textOverflow) ? (B(c, { width: A + "px", display: "block", whiteSpace: l || "normal" }), this.oldTextWidth = A, this.hasBoxWidthChanged = !0) : this.hasBoxWidthChanged = !1; F !== this.cTT && (l = a.fontMetrics(c.style.fontSize,
                                    c).b, !h(d) || d === (this.oldRotation || 0) && k === this.oldAlign || this.setSpanRotation(d, n, l), this.getSpanCorrection(!h(d) && this.textPxLength || c.offsetWidth, l, n, d, k)); B(c, { left: e + (this.xCorr || 0) + "px", top: u + (this.yCorr || 0) + "px" }); this.cTT = F; this.oldRotation = d; this.oldAlign = k
                        }
                    } else this.alignOnAdd = !0
                }, setSpanRotation: function (a, c, g) { var b = {}, e = this.renderer.getTransformKey(); b[e] = b.transform = "rotate(" + a + "deg)"; b[e + (t ? "Origin" : "-origin")] = b.transformOrigin = 100 * c + "% " + g + "px"; B(this.element, b) }, getSpanCorrection: function (a,
                    c, g) { this.xCorr = -a * g; this.yCorr = -c }
            }); d(k.prototype, {
                getTransformKey: function () { return w && !/Edge/.test(n.navigator.userAgent) ? "-ms-transform" : r ? "-webkit-transform" : t ? "MozTransform" : n.opera ? "-o-transform" : "" }, html: function (m, c, g) {
                    var b = this.createElement("span"), q = b.element, u = b.renderer, k = u.isSVG, n = function (a, b) { ["opacity", "visibility"].forEach(function (c) { e(a, c + "Setter", function (a, f, c, l) { a.call(this, f, c, l); b[c] = f }) }); a.addedSetters = !0 }, p = a.charts[u.chartIndex], p = p && p.styledMode; b.textSetter = function (a) {
                    a !==
                        q.innerHTML && delete this.bBox; this.textStr = a; q.innerHTML = v(a, ""); b.doTransform = !0
                    }; k && n(b, b.element.style); b.xSetter = b.ySetter = b.alignSetter = b.rotationSetter = function (a, c) { "align" === c && (c = "textAlign"); b[c] = a; b.doTransform = !0 }; b.afterSetters = function () { this.doTransform && (this.htmlUpdateTransform(), this.doTransform = !1) }; b.attr({ text: m, x: Math.round(c), y: Math.round(g) }).css({ position: "absolute" }); p || b.css({ fontFamily: this.style.fontFamily, fontSize: this.style.fontSize }); q.style.whiteSpace = "nowrap"; b.css =
                        b.htmlCss; k && (b.add = function (a) {
                            var c, l = u.box.parentNode, g = []; if (this.parentGroup = a) {
                                if (c = a.div, !c) {
                                    for (; a;)g.push(a), a = a.parentGroup; g.reverse().forEach(function (a) {
                                        function f(f, b) { a[b] = f; "translateX" === b ? e.left = f + "px" : e.top = f + "px"; a.doTransform = !0 } var e, m = z(a.element, "class"); m && (m = { className: m }); c = a.div = a.div || C("div", m, { position: "absolute", left: (a.translateX || 0) + "px", top: (a.translateY || 0) + "px", display: a.display, opacity: a.opacity, pointerEvents: a.styles && a.styles.pointerEvents }, c || l); e = c.style;
                                        d(a, { classSetter: function (a) { return function (f) { this.element.setAttribute("class", f); a.className = f } }(c), on: function () { g[0].div && b.on.apply({ element: g[0].div }, arguments); return a }, translateXSetter: f, translateYSetter: f }); a.addedSetters || n(a, e)
                                    })
                                }
                            } else c = l; c.appendChild(q); b.added = !0; b.alignOnAdd && b.htmlUpdateTransform(); return b
                        }); return b
                }
            })
    })(H); (function (a) {
        var z = a.correctFloat, C = a.defined, B = a.destroyObjectProperties, h = a.fireEvent, d = a.isNumber, t = a.merge, w = a.pick, r = a.deg2rad; a.Tick = function (a, d, k,
            n, e) { this.axis = a; this.pos = d; this.type = k || ""; this.isNewLabel = this.isNew = !0; this.parameters = e || {}; this.tickmarkOffset = this.parameters.tickmarkOffset; this.options = this.parameters.options; k || n || this.addLabel() }; a.Tick.prototype = {
                addLabel: function () {
                    var d = this, p = d.axis, k = p.options, n = p.chart, e = p.categories, m = p.names, c = d.pos, g = w(d.options && d.options.labels, k.labels), b = p.tickPositions, q = c === b[0], u = c === b[b.length - 1], e = this.parameters.category || (e ? w(e[c], m[c], c) : c), x = d.label, b = b.info, h, E, l, A; p.isDatetimeAxis &&
                        b && (E = n.time.resolveDTLFormat(k.dateTimeLabelFormats[!k.grid && b.higherRanks[c] || b.unitName]), h = E.main); d.isFirst = q; d.isLast = u; d.formatCtx = { axis: p, chart: n, isFirst: q, isLast: u, dateTimeLabelFormat: h, tickPositionInfo: b, value: p.isLog ? z(p.lin2log(e)) : e, pos: c }; k = p.labelFormatter.call(d.formatCtx, this.formatCtx); if (A = E && E.list) d.shortenLabel = function () {
                            for (l = 0; l < A.length; l++)if (x.attr({ text: p.labelFormatter.call(a.extend(d.formatCtx, { dateTimeLabelFormat: A[l] })) }), x.getBBox().width < p.getSlotWidth(d) - 2 * w(g.padding,
                                5)) return; x.attr({ text: "" })
                        }; if (C(x)) x && x.textStr !== k && (!x.textWidth || g.style && g.style.width || x.styles.width || x.css({ width: null }), x.attr({ text: k })); else { if (d.label = x = C(k) && g.enabled ? n.renderer.text(k, 0, 0, g.useHTML).add(p.labelGroup) : null) n.styledMode || x.css(t(g.style)), x.textPxLength = x.getBBox().width; d.rotation = 0 }
                }, getLabelSize: function () { return this.label ? this.label.getBBox()[this.axis.horiz ? "height" : "width"] : 0 }, handleOverflow: function (a) {
                    var d = this.axis, k = d.options.labels, n = a.x, e = d.chart.chartWidth,
                    m = d.chart.spacing, c = w(d.labelLeft, Math.min(d.pos, m[3])), m = w(d.labelRight, Math.max(d.isRadial ? 0 : d.pos + d.len, e - m[1])), g = this.label, b = this.rotation, q = { left: 0, center: .5, right: 1 }[d.labelAlign || g.attr("align")], u = g.getBBox().width, x = d.getSlotWidth(this), h = x, v = 1, l, A = {}; if (b || "justify" !== w(k.overflow, "justify")) 0 > b && n - q * u < c ? l = Math.round(n / Math.cos(b * r) - c) : 0 < b && n + q * u > m && (l = Math.round((e - n) / Math.cos(b * r))); else if (e = n + (1 - q) * u, n - q * u < c ? h = a.x + h * (1 - q) - c : e > m && (h = m - a.x + h * q, v = -1), h = Math.min(x, h), h < x && "center" === d.labelAlign &&
                        (a.x += v * (x - h - q * (x - Math.min(u, h)))), u > h || d.autoRotation && (g.styles || {}).width) l = h; l && (this.shortenLabel ? this.shortenLabel() : (A.width = Math.floor(l), (k.style || {}).textOverflow || (A.textOverflow = "ellipsis"), g.css(A)))
                }, getPosition: function (d, p, k, n) {
                    var e = this.axis, m = e.chart, c = n && m.oldChartHeight || m.chartHeight; d = {
                        x: d ? a.correctFloat(e.translate(p + k, null, null, n) + e.transB) : e.left + e.offset + (e.opposite ? (n && m.oldChartWidth || m.chartWidth) - e.right - e.left : 0), y: d ? c - e.bottom + e.offset - (e.opposite ? e.height : 0) : a.correctFloat(c -
                            e.translate(p + k, null, null, n) - e.transB)
                    }; h(this, "afterGetPosition", { pos: d }); return d
                }, getLabelPosition: function (a, d, k, n, e, m, c, g) {
                    var b = this.axis, q = b.transA, u = b.reversed, x = b.staggerLines, p = b.tickRotCorr || { x: 0, y: 0 }, v = e.y, l = n || b.reserveSpaceDefault ? 0 : -b.labelOffset * ("center" === b.labelAlign ? .5 : 1), A = {}; C(v) || (v = 0 === b.side ? k.rotation ? -8 : -k.getBBox().height : 2 === b.side ? p.y + 8 : Math.cos(k.rotation * r) * (p.y - k.getBBox(!1, 0).height / 2)); a = a + e.x + l + p.x - (m && n ? m * q * (u ? -1 : 1) : 0); d = d + v - (m && !n ? m * q * (u ? 1 : -1) : 0); x && (k = c / (g ||
                        1) % x, b.opposite && (k = x - k - 1), d += b.labelOffset / x * k); A.x = a; A.y = Math.round(d); h(this, "afterGetLabelPosition", { pos: A }); return A
                }, getMarkPath: function (a, d, k, n, e, m) { return m.crispLine(["M", a, d, "L", a + (e ? 0 : -k), d + (e ? k : 0)], n) }, renderGridLine: function (a, d, k) {
                    var n = this.axis, e = n.options, m = this.gridLine, c = {}, g = this.pos, b = this.type, q = w(this.tickmarkOffset, n.tickmarkOffset), u = n.chart.renderer, x = b ? b + "Grid" : "grid", h = e[x + "LineWidth"], p = e[x + "LineColor"], e = e[x + "LineDashStyle"]; m || (n.chart.styledMode || (c.stroke = p, c["stroke-width"] =
                        h, e && (c.dashstyle = e)), b || (c.zIndex = 1), a && (d = 0), this.gridLine = m = u.path().attr(c).addClass("highcharts-" + (b ? b + "-" : "") + "grid-line").add(n.gridGroup)); if (m && (k = n.getPlotLinePath(g + q, m.strokeWidth() * k, a, "pass"))) m[a || this.isNew ? "attr" : "animate"]({ d: k, opacity: d })
                }, renderMark: function (a, d, k) {
                    var n = this.axis, e = n.options, m = n.chart.renderer, c = this.type, g = c ? c + "Tick" : "tick", b = n.tickSize(g), q = this.mark, u = !q, x = a.x; a = a.y; var h = w(e[g + "Width"], !c && n.isXAxis ? 1 : 0), e = e[g + "Color"]; b && (n.opposite && (b[0] = -b[0]), u && (this.mark =
                        q = m.path().addClass("highcharts-" + (c ? c + "-" : "") + "tick").add(n.axisGroup), n.chart.styledMode || q.attr({ stroke: e, "stroke-width": h })), q[u ? "attr" : "animate"]({ d: this.getMarkPath(x, a, b[0], q.strokeWidth() * k, n.horiz, m), opacity: d }))
                }, renderLabel: function (a, h, k, n) {
                    var e = this.axis, m = e.horiz, c = e.options, g = this.label, b = c.labels, q = b.step, e = w(this.tickmarkOffset, e.tickmarkOffset), u = !0, x = a.x; a = a.y; g && d(x) && (g.xy = a = this.getLabelPosition(x, a, g, m, b, e, n, q), this.isFirst && !this.isLast && !w(c.showFirstLabel, 1) || this.isLast &&
                        !this.isFirst && !w(c.showLastLabel, 1) ? u = !1 : !m || b.step || b.rotation || h || 0 === k || this.handleOverflow(a), q && n % q && (u = !1), u && d(a.y) ? (a.opacity = k, g[this.isNewLabel ? "attr" : "animate"](a), this.isNewLabel = !1) : (g.attr("y", -9999), this.isNewLabel = !0))
                }, render: function (d, h, k) {
                    var n = this.axis, e = n.horiz, m = this.pos, c = w(this.tickmarkOffset, n.tickmarkOffset), m = this.getPosition(e, m, c, h), c = m.x, g = m.y, n = e && c === n.pos + n.len || !e && g === n.pos ? -1 : 1; k = w(k, 1); this.isActive = !0; this.renderGridLine(h, k, n); this.renderMark(m, k, n); this.renderLabel(m,
                        h, k, d); this.isNew = !1; a.fireEvent(this, "afterRender")
                }, destroy: function () { B(this, this.axis) }
            }
    })(H); var W = function (a) {
        var z = a.addEvent, C = a.animObject, B = a.arrayMax, h = a.arrayMin, d = a.color, t = a.correctFloat, w = a.defaultOptions, r = a.defined, v = a.deg2rad, p = a.destroyObjectProperties, k = a.extend, n = a.fireEvent, e = a.format, m = a.getMagnitude, c = a.isArray, g = a.isNumber, b = a.isString, q = a.merge, u = a.normalizeTickInterval, x = a.objectEach, D = a.pick, E = a.removeEvent, l = a.splat, A = a.syncTimeout, F = a.Tick, G = function () {
            this.init.apply(this,
                arguments)
        }; a.extend(G.prototype, {
            defaultOptions: {
                dateTimeLabelFormats: { millisecond: { main: "%H:%M:%S.%L", range: !1 }, second: { main: "%H:%M:%S", range: !1 }, minute: { main: "%H:%M", range: !1 }, hour: { main: "%H:%M", range: !1 }, day: { main: "%e. %b" }, week: { main: "%e. %b" }, month: { main: "%b '%y" }, year: { main: "%Y" } }, endOnTick: !1, labels: { enabled: !0, indentation: 10, x: 0, style: { color: "#666666", cursor: "default", fontSize: "11px" } }, maxPadding: .01, minorTickLength: 2, minorTickPosition: "outside", minPadding: .01, startOfWeek: 1, startOnTick: !1,
                tickLength: 10, tickPixelInterval: 100, tickmarkPlacement: "between", tickPosition: "outside", title: { align: "middle", style: { color: "#666666" } }, type: "linear", minorGridLineColor: "#f2f2f2", minorGridLineWidth: 1, minorTickColor: "#999999", lineColor: "#ccd6eb", lineWidth: 1, gridLineColor: "#e6e6e6", tickColor: "#ccd6eb"
            }, defaultYAxisOptions: {
                endOnTick: !0, maxPadding: .05, minPadding: .05, tickPixelInterval: 72, showLastLabel: !0, labels: { x: -8 }, startOnTick: !0, title: { rotation: 270, text: "Values" }, stackLabels: {
                    allowOverlap: !1, enabled: !1,
                    formatter: function () { return a.numberFormat(this.total, -1) }, style: { color: "#000000", fontSize: "11px", fontWeight: "bold", textOutline: "1px contrast" }
                }, gridLineWidth: 1, lineWidth: 0
            }, defaultLeftAxisOptions: { labels: { x: -15 }, title: { rotation: 270 } }, defaultRightAxisOptions: { labels: { x: 15 }, title: { rotation: 90 } }, defaultBottomAxisOptions: { labels: { autoRotation: [-45], x: 0 }, title: { rotation: 0 } }, defaultTopAxisOptions: { labels: { autoRotation: [-45], x: 0 }, title: { rotation: 0 } }, init: function (a, b) {
                var f = b.isX, c = this; c.chart = a; c.horiz =
                    a.inverted && !c.isZAxis ? !f : f; c.isXAxis = f; c.coll = c.coll || (f ? "xAxis" : "yAxis"); n(this, "init", { userOptions: b }); c.opposite = b.opposite; c.side = b.side || (c.horiz ? c.opposite ? 0 : 2 : c.opposite ? 1 : 3); c.setOptions(b); var y = this.options, g = y.type; c.labelFormatter = y.labels.formatter || c.defaultLabelFormatter; c.userOptions = b; c.minPixelPadding = 0; c.reversed = y.reversed; c.visible = !1 !== y.visible; c.zoomEnabled = !1 !== y.zoomEnabled; c.hasNames = "category" === g || !0 === y.categories; c.categories = y.categories || c.hasNames; c.names || (c.names =
                        [], c.names.keys = {}); c.plotLinesAndBandsGroups = {}; c.isLog = "logarithmic" === g; c.isDatetimeAxis = "datetime" === g; c.positiveValuesOnly = c.isLog && !c.allowNegativeLog; c.isLinked = r(y.linkedTo); c.ticks = {}; c.labelEdge = []; c.minorTicks = {}; c.plotLinesAndBands = []; c.alternateBands = {}; c.len = 0; c.minRange = c.userMinRange = y.minRange || y.maxZoom; c.range = y.range; c.offset = y.offset || 0; c.stacks = {}; c.oldStacks = {}; c.stacksTouched = 0; c.max = null; c.min = null; c.crosshair = D(y.crosshair, l(a.options.tooltip.crosshairs)[f ? 0 : 1], !1); b = c.options.events;
                -1 === a.axes.indexOf(c) && (f ? a.axes.splice(a.xAxis.length, 0, c) : a.axes.push(c), a[c.coll].push(c)); c.series = c.series || []; a.inverted && !c.isZAxis && f && void 0 === c.reversed && (c.reversed = !0); x(b, function (a, f) { z(c, f, a) }); c.lin2log = y.linearToLogConverter || c.lin2log; c.isLog && (c.val2lin = c.log2lin, c.lin2val = c.lin2log); n(this, "afterInit")
            }, setOptions: function (a) {
            this.options = q(this.defaultOptions, "yAxis" === this.coll && this.defaultYAxisOptions, [this.defaultTopAxisOptions, this.defaultRightAxisOptions, this.defaultBottomAxisOptions,
            this.defaultLeftAxisOptions][this.side], q(w[this.coll], a)); n(this, "afterSetOptions", { userOptions: a })
            }, defaultLabelFormatter: function () {
                var f = this.axis, b = this.value, c = f.chart.time, l = f.categories, g = this.dateTimeLabelFormat, m = w.lang, q = m.numericSymbols, m = m.numericSymbolMagnitude || 1E3, u = q && q.length, d, k = f.options.labels.format, f = f.isLog ? Math.abs(b) : f.tickInterval; if (k) d = e(k, this, c); else if (l) d = b; else if (g) d = c.dateFormat(g, b); else if (u && 1E3 <= f) for (; u-- && void 0 === d;)c = Math.pow(m, u + 1), f >= c && 0 === 10 * b % c && null !==
                    q[u] && 0 !== b && (d = a.numberFormat(b / c, -1) + q[u]); void 0 === d && (d = 1E4 <= Math.abs(b) ? a.numberFormat(b, -1) : a.numberFormat(b, -1, void 0, "")); return d
            }, getSeriesExtremes: function () {
                var a = this, b = a.chart; n(this, "getSeriesExtremes", null, function () {
                a.hasVisibleSeries = !1; a.dataMin = a.dataMax = a.threshold = null; a.softThreshold = !a.isXAxis; a.buildStacks && a.buildStacks(); a.series.forEach(function (f) {
                    if (f.visible || !b.options.chart.ignoreHiddenSeries) {
                        var c = f.options, y = c.threshold, l; a.hasVisibleSeries = !0; a.positiveValuesOnly &&
                            0 >= y && (y = null); if (a.isXAxis) c = f.xData, c.length && (f = h(c), l = B(c), g(f) || f instanceof Date || (c = c.filter(g), f = h(c), l = B(c)), c.length && (a.dataMin = Math.min(D(a.dataMin, c[0], f), f), a.dataMax = Math.max(D(a.dataMax, c[0], l), l))); else if (f.getExtremes(), l = f.dataMax, f = f.dataMin, r(f) && r(l) && (a.dataMin = Math.min(D(a.dataMin, f), f), a.dataMax = Math.max(D(a.dataMax, l), l)), r(y) && (a.threshold = y), !c.softThreshold || a.positiveValuesOnly) a.softThreshold = !1
                    }
                })
                }); n(this, "afterGetSeriesExtremes")
            }, translate: function (a, b, c, l, e, m) {
                var f =
                    this.linkedParent || this, y = 1, q = 0, u = l ? f.oldTransA : f.transA; l = l ? f.oldMin : f.min; var d = f.minPixelPadding; e = (f.isOrdinal || f.isBroken || f.isLog && e) && f.lin2val; u || (u = f.transA); c && (y *= -1, q = f.len); f.reversed && (y *= -1, q -= y * (f.sector || f.len)); b ? (a = (a * y + q - d) / u + l, e && (a = f.lin2val(a))) : (e && (a = f.val2lin(a)), a = g(l) ? y * (a - l) * u + q + y * d + (g(m) ? u * m : 0) : void 0); return a
            }, toPixels: function (a, b) { return this.translate(a, !1, !this.horiz, null, !0) + (b ? 0 : this.pos) }, toValue: function (a, b) {
                return this.translate(a - (b ? 0 : this.pos), !0, !this.horiz,
                    null, !0)
            }, getPlotLinePath: function (a, b, c, l, e) {
                var f = this.chart, y = this.left, m = this.top, q, u, d = c && f.oldChartHeight || f.chartHeight, k = c && f.oldChartWidth || f.chartWidth, A; q = this.transB; var n = function (a, f, b) { if ("pass" !== l && a < f || a > b) l ? a = Math.min(Math.max(f, a), b) : A = !0; return a }; e = D(e, this.translate(a, null, null, c)); e = Math.min(Math.max(-1E5, e), 1E5); a = c = Math.round(e + q); q = u = Math.round(d - e - q); g(e) ? this.horiz ? (q = m, u = d - this.bottom, a = c = n(a, y, y + this.width)) : (a = y, c = k - this.right, q = u = n(q, m, m + this.height)) : (A = !0, l = !1);
                return A && !l ? null : f.renderer.crispLine(["M", a, q, "L", c, u], b || 1)
            }, getLinearTickPositions: function (a, b, c) { var f, y = t(Math.floor(b / a) * a); c = t(Math.ceil(c / a) * a); var l = [], g; t(y + a) === y && (g = 20); if (this.single) return [b]; for (b = y; b <= c;) { l.push(b); b = t(b + a, g); if (b === f) break; f = b } return l }, getMinorTickInterval: function () { var a = this.options; return !0 === a.minorTicks ? D(a.minorTickInterval, "auto") : !1 === a.minorTicks ? null : a.minorTickInterval }, getMinorTickPositions: function () {
                var a = this, b = a.options, c = a.tickPositions, l = a.minorTickInterval,
                g = [], e = a.pointRangePadding || 0, m = a.min - e, e = a.max + e, q = e - m; if (q && q / l < a.len / 3) if (a.isLog) this.paddedTicks.forEach(function (f, b, c) { b && g.push.apply(g, a.getLogTickPositions(l, c[b - 1], c[b], !0)) }); else if (a.isDatetimeAxis && "auto" === this.getMinorTickInterval()) g = g.concat(a.getTimeTicks(a.normalizeTimeTickInterval(l), m, e, b.startOfWeek)); else for (b = m + (c[0] - m) % l; b <= e && b !== g[0]; b += l)g.push(b); 0 !== g.length && a.trimTicks(g); return g
            }, adjustForMinRange: function () {
                var a = this.options, b = this.min, c = this.max, l, g, e, m, q, u,
                d, k; this.isXAxis && void 0 === this.minRange && !this.isLog && (r(a.min) || r(a.max) ? this.minRange = null : (this.series.forEach(function (a) { u = a.xData; for (m = d = a.xIncrement ? 1 : u.length - 1; 0 < m; m--)if (q = u[m] - u[m - 1], void 0 === e || q < e) e = q }), this.minRange = Math.min(5 * e, this.dataMax - this.dataMin))); c - b < this.minRange && (g = this.dataMax - this.dataMin >= this.minRange, k = this.minRange, l = (k - c + b) / 2, l = [b - l, D(a.min, b - l)], g && (l[2] = this.isLog ? this.log2lin(this.dataMin) : this.dataMin), b = B(l), c = [b + k, D(a.max, b + k)], g && (c[2] = this.isLog ? this.log2lin(this.dataMax) :
                    this.dataMax), c = h(c), c - b < k && (l[0] = c - k, l[1] = D(a.min, c - k), b = B(l))); this.min = b; this.max = c
            }, getClosest: function () { var a; this.categories ? a = 1 : this.series.forEach(function (f) { var b = f.closestPointRange, c = f.visible || !f.chart.options.chart.ignoreHiddenSeries; !f.noSharedTooltip && r(b) && c && (a = r(a) ? Math.min(a, b) : b) }); return a }, nameToX: function (a) {
                var f = c(this.categories), b = f ? this.categories : this.names, l = a.options.x, g; a.series.requireSorting = !1; r(l) || (l = !1 === this.options.uniqueNames ? a.series.autoIncrement() : f ? b.indexOf(a.name) :
                    D(b.keys[a.name], -1)); -1 === l ? f || (g = b.length) : g = l; void 0 !== g && (this.names[g] = a.name, this.names.keys[a.name] = g); return g
            }, updateNames: function () {
                var a = this, b = this.names; 0 < b.length && (Object.keys(b.keys).forEach(function (a) { delete b.keys[a] }), b.length = 0, this.minRange = this.userMinRange, (this.series || []).forEach(function (f) {
                f.xIncrement = null; if (!f.points || f.isDirtyData) a.max = Math.max(a.max, f.xData.length - 1), f.processData(), f.generatePoints(); f.data.forEach(function (b, c) {
                    var l; b && b.options && void 0 !== b.name &&
                        (l = a.nameToX(b), void 0 !== l && l !== b.x && (b.x = l, f.xData[c] = l))
                })
                }))
            }, setAxisTranslation: function (a) {
                var f = this, c = f.max - f.min, l = f.axisPointRange || 0, g, e = 0, m = 0, q = f.linkedParent, u = !!f.categories, d = f.transA, k = f.isXAxis; if (k || u || l) g = f.getClosest(), q ? (e = q.minPointOffset, m = q.pointRangePadding) : f.series.forEach(function (a) { var c = u ? 1 : k ? D(a.options.pointRange, g, 0) : f.axisPointRange || 0; a = a.options.pointPlacement; l = Math.max(l, c); f.single || (e = Math.max(e, b(a) ? 0 : c / 2), m = Math.max(m, "on" === a ? 0 : c)) }), q = f.ordinalSlope && g ?
                    f.ordinalSlope / g : 1, f.minPointOffset = e *= q, f.pointRangePadding = m *= q, f.pointRange = Math.min(l, c), k && (f.closestPointRange = g); a && (f.oldTransA = d); f.translationSlope = f.transA = d = f.staticScale || f.len / (c + m || 1); f.transB = f.horiz ? f.left : f.bottom; f.minPixelPadding = d * e; n(this, "afterSetAxisTranslation")
            }, minFromRange: function () { return this.max - this.range }, setTickInterval: function (f) {
                var b = this, c = b.chart, l = b.options, e = b.isLog, q = b.isDatetimeAxis, d = b.isXAxis, k = b.isLinked, A = l.maxPadding, x = l.minPadding, h, F = l.tickInterval,
                p = l.tickPixelInterval, v = b.categories, E = g(b.threshold) ? b.threshold : null, G = b.softThreshold, w, B, z; q || v || k || this.getTickAmount(); B = D(b.userMin, l.min); z = D(b.userMax, l.max); k ? (b.linkedParent = c[b.coll][l.linkedTo], h = b.linkedParent.getExtremes(), b.min = D(h.min, h.dataMin), b.max = D(h.max, h.dataMax), l.type !== b.linkedParent.options.type && a.error(11, 1, c)) : (!G && r(E) && (b.dataMin >= E ? (h = E, x = 0) : b.dataMax <= E && (w = E, A = 0)), b.min = D(B, h, b.dataMin), b.max = D(z, w, b.dataMax)); e && (b.positiveValuesOnly && !f && 0 >= Math.min(b.min, D(b.dataMin,
                    b.min)) && a.error(10, 1, c), b.min = t(b.log2lin(b.min), 15), b.max = t(b.log2lin(b.max), 15)); b.range && r(b.max) && (b.userMin = b.min = B = Math.max(b.dataMin, b.minFromRange()), b.userMax = z = b.max, b.range = null); n(b, "foundExtremes"); b.beforePadding && b.beforePadding(); b.adjustForMinRange(); !(v || b.axisPointRange || b.usePercentage || k) && r(b.min) && r(b.max) && (c = b.max - b.min) && (!r(B) && x && (b.min -= c * x), !r(z) && A && (b.max += c * A)); g(l.softMin) && !g(b.userMin) && (b.min = Math.min(b.min, l.softMin)); g(l.softMax) && !g(b.userMax) && (b.max = Math.max(b.max,
                        l.softMax)); g(l.floor) && (b.min = Math.min(Math.max(b.min, l.floor), Number.MAX_VALUE)); g(l.ceiling) && (b.max = Math.max(Math.min(b.max, l.ceiling), D(b.userMax, -Number.MAX_VALUE))); G && r(b.dataMin) && (E = E || 0, !r(B) && b.min < E && b.dataMin >= E ? b.min = E : !r(z) && b.max > E && b.dataMax <= E && (b.max = E)); b.tickInterval = b.min === b.max || void 0 === b.min || void 0 === b.max ? 1 : k && !F && p === b.linkedParent.options.tickPixelInterval ? F = b.linkedParent.tickInterval : D(F, this.tickAmount ? (b.max - b.min) / Math.max(this.tickAmount - 1, 1) : void 0, v ? 1 : (b.max -
                            b.min) * p / Math.max(b.len, p)); d && !f && b.series.forEach(function (a) { a.processData(b.min !== b.oldMin || b.max !== b.oldMax) }); b.setAxisTranslation(!0); b.beforeSetTickPositions && b.beforeSetTickPositions(); b.postProcessTickInterval && (b.tickInterval = b.postProcessTickInterval(b.tickInterval)); b.pointRange && !F && (b.tickInterval = Math.max(b.pointRange, b.tickInterval)); f = D(l.minTickInterval, b.isDatetimeAxis && b.closestPointRange); !F && b.tickInterval < f && (b.tickInterval = f); q || e || F || (b.tickInterval = u(b.tickInterval, null,
                                m(b.tickInterval), D(l.allowDecimals, !(.5 < b.tickInterval && 5 > b.tickInterval && 1E3 < b.max && 9999 > b.max)), !!this.tickAmount)); this.tickAmount || (b.tickInterval = b.unsquish()); this.setTickPositions()
            }, setTickPositions: function () {
                var b = this.options, c, l = b.tickPositions; c = this.getMinorTickInterval(); var g = b.tickPositioner, e = b.startOnTick, m = b.endOnTick; this.tickmarkOffset = this.categories && "between" === b.tickmarkPlacement && 1 === this.tickInterval ? .5 : 0; this.minorTickInterval = "auto" === c && this.tickInterval ? this.tickInterval /
                    5 : c; this.single = this.min === this.max && r(this.min) && !this.tickAmount && (parseInt(this.min, 10) === this.min || !1 !== b.allowDecimals); this.tickPositions = c = l && l.slice(); !c && (!this.ordinalPositions && (this.max - this.min) / this.tickInterval > Math.max(2 * this.len, 200) ? (c = [this.min, this.max], a.error(19, !1, this.chart)) : c = this.isDatetimeAxis ? this.getTimeTicks(this.normalizeTimeTickInterval(this.tickInterval, b.units), this.min, this.max, b.startOfWeek, this.ordinalPositions, this.closestPointRange, !0) : this.isLog ? this.getLogTickPositions(this.tickInterval,
                        this.min, this.max) : this.getLinearTickPositions(this.tickInterval, this.min, this.max), c.length > this.len && (c = [c[0], c.pop()], c[0] === c[1] && (c.length = 1)), this.tickPositions = c, g && (g = g.apply(this, [this.min, this.max]))) && (this.tickPositions = c = g); this.paddedTicks = c.slice(0); this.trimTicks(c, e, m); this.isLinked || (this.single && 2 > c.length && (this.min -= .5, this.max += .5), l || g || this.adjustTickAmount()); n(this, "afterSetTickPositions")
            }, trimTicks: function (a, b, c) {
                var f = a[0], l = a[a.length - 1], g = this.minPointOffset || 0; if (!this.isLinked) {
                    if (b &&
                        -Infinity !== f) this.min = f; else for (; this.min - g > a[0];)a.shift(); if (c) this.max = l; else for (; this.max + g < a[a.length - 1];)a.pop(); 0 === a.length && r(f) && !this.options.tickPositions && a.push((l + f) / 2)
                }
            }, alignToOthers: function () {
                var a = {}, b, c = this.options; !1 === this.chart.options.chart.alignTicks || !1 === c.alignTicks || !1 === c.startOnTick || !1 === c.endOnTick || this.isLog || this.chart[this.coll].forEach(function (f) { var c = f.options, c = [f.horiz ? c.left : c.top, c.width, c.height, c.pane].join(); f.series.length && (a[c] ? b = !0 : a[c] = 1) });
                return b
            }, getTickAmount: function () { var a = this.options, b = a.tickAmount, c = a.tickPixelInterval; !r(a.tickInterval) && this.len < c && !this.isRadial && !this.isLog && a.startOnTick && a.endOnTick && (b = 2); !b && this.alignToOthers() && (b = Math.ceil(this.len / c) + 1); 4 > b && (this.finalTickAmt = b, b = 5); this.tickAmount = b }, adjustTickAmount: function () {
                var a = this.tickInterval, b = this.tickPositions, c = this.tickAmount, l = this.finalTickAmt, g = b && b.length, e = D(this.threshold, this.softThreshold ? 0 : null); if (this.hasData()) {
                    if (g < c) {
                        for (; b.length <
                            c;)b.length % 2 || this.min === e ? b.push(t(b[b.length - 1] + a)) : b.unshift(t(b[0] - a)); this.transA *= (g - 1) / (c - 1); this.min = b[0]; this.max = b[b.length - 1]
                    } else g > c && (this.tickInterval *= 2, this.setTickPositions()); if (r(l)) { for (a = c = b.length; a--;)(3 === l && 1 === a % 2 || 2 >= l && 0 < a && a < c - 1) && b.splice(a, 1); this.finalTickAmt = void 0 }
                }
            }, setScale: function () {
                var a, b; this.oldMin = this.min; this.oldMax = this.max; this.oldAxisLength = this.len; this.setAxisSize(); b = this.len !== this.oldAxisLength; this.series.forEach(function (b) {
                    if (b.isDirtyData ||
                        b.isDirty || b.xAxis.isDirty) a = !0
                }); b || a || this.isLinked || this.forceRedraw || this.userMin !== this.oldUserMin || this.userMax !== this.oldUserMax || this.alignToOthers() ? (this.resetStacks && this.resetStacks(), this.forceRedraw = !1, this.getSeriesExtremes(), this.setTickInterval(), this.oldUserMin = this.userMin, this.oldUserMax = this.userMax, this.isDirty || (this.isDirty = b || this.min !== this.oldMin || this.max !== this.oldMax)) : this.cleanStacks && this.cleanStacks(); n(this, "afterSetScale")
            }, setExtremes: function (a, b, c, l, g) {
                var f =
                    this, e = f.chart; c = D(c, !0); f.series.forEach(function (a) { delete a.kdTree }); g = k(g, { min: a, max: b }); n(f, "setExtremes", g, function () { f.userMin = a; f.userMax = b; f.eventArgs = g; c && e.redraw(l) })
            }, zoom: function (a, b) {
                var f = this.dataMin, c = this.dataMax, l = this.options, g = Math.min(f, D(l.min, f)), l = Math.max(c, D(l.max, c)); if (a !== this.min || b !== this.max) this.allowZoomOutside || (r(f) && (a < g && (a = g), a > l && (a = l)), r(c) && (b < g && (b = g), b > l && (b = l))), this.displayBtn = void 0 !== a || void 0 !== b, this.setExtremes(a, b, !1, void 0, { trigger: "zoom" });
                return !0
            }, setAxisSize: function () {
                var b = this.chart, c = this.options, l = c.offsets || [0, 0, 0, 0], g = this.horiz, e = this.width = Math.round(a.relativeLength(D(c.width, b.plotWidth - l[3] + l[1]), b.plotWidth)), m = this.height = Math.round(a.relativeLength(D(c.height, b.plotHeight - l[0] + l[2]), b.plotHeight)), q = this.top = Math.round(a.relativeLength(D(c.top, b.plotTop + l[0]), b.plotHeight, b.plotTop)), c = this.left = Math.round(a.relativeLength(D(c.left, b.plotLeft + l[3]), b.plotWidth, b.plotLeft)); this.bottom = b.chartHeight - m - q; this.right =
                    b.chartWidth - e - c; this.len = Math.max(g ? e : m, 0); this.pos = g ? c : q
            }, getExtremes: function () { var a = this.isLog; return { min: a ? t(this.lin2log(this.min)) : this.min, max: a ? t(this.lin2log(this.max)) : this.max, dataMin: this.dataMin, dataMax: this.dataMax, userMin: this.userMin, userMax: this.userMax } }, getThreshold: function (a) { var b = this.isLog, c = b ? this.lin2log(this.min) : this.min, b = b ? this.lin2log(this.max) : this.max; null === a || -Infinity === a ? a = c : Infinity === a ? a = b : c > a ? a = c : b < a && (a = b); return this.translate(a, 0, 1, 0, 1) }, autoLabelAlign: function (a) {
                a =
                (D(a, 0) - 90 * this.side + 720) % 360; return 15 < a && 165 > a ? "right" : 195 < a && 345 > a ? "left" : "center"
            }, tickSize: function (a) { var b = this.options, c = b[a + "Length"], f = D(b[a + "Width"], "tick" === a && this.isXAxis ? 1 : 0); if (f && c) return "inside" === b[a + "Position"] && (c = -c), [c, f] }, labelMetrics: function () { var a = this.tickPositions && this.tickPositions[0] || 0; return this.chart.renderer.fontMetrics(this.options.labels.style && this.options.labels.style.fontSize, this.ticks[a] && this.ticks[a].label) }, unsquish: function () {
                var a = this.options.labels,
                b = this.horiz, c = this.tickInterval, l = c, g = this.len / (((this.categories ? 1 : 0) + this.max - this.min) / c), e, m = a.rotation, q = this.labelMetrics(), u, d = Number.MAX_VALUE, k, A = function (a) { a /= g || 1; a = 1 < a ? Math.ceil(a) : 1; return t(a * c) }; b ? (k = !a.staggerLines && !a.step && (r(m) ? [m] : g < D(a.autoRotationLimit, 80) && a.autoRotation)) && k.forEach(function (a) { var b; if (a === m || a && -90 <= a && 90 >= a) u = A(Math.abs(q.h / Math.sin(v * a))), b = u + Math.abs(a / 360), b < d && (d = b, e = a, l = u) }) : a.step || (l = A(q.h)); this.autoRotation = k; this.labelRotation = D(e, m); return l
            },
            getSlotWidth: function (a) { var b = this.chart, c = this.horiz, f = this.options.labels, l = Math.max(this.tickPositions.length - (this.categories ? 0 : 1), 1), g = b.margin[3]; return a && a.slotWidth || c && 2 > (f.step || 0) && !f.rotation && (this.staggerLines || 1) * this.len / l || !c && (f.style && parseInt(f.style.width, 10) || g && g - b.spacing[3] || .33 * b.chartWidth) }, renderUnsquish: function () {
                var a = this.chart, c = a.renderer, l = this.tickPositions, g = this.ticks, e = this.options.labels, m = e && e.style || {}, q = this.horiz, u = this.getSlotWidth(), d = Math.max(1, Math.round(u -
                    2 * (e.padding || 5))), k = {}, A = this.labelMetrics(), n = e.style && e.style.textOverflow, x, h, F = 0, p; b(e.rotation) || (k.rotation = e.rotation || 0); l.forEach(function (a) { (a = g[a]) && a.label && a.label.textPxLength > F && (F = a.label.textPxLength) }); this.maxLabelLength = F; if (this.autoRotation) F > d && F > A.h ? k.rotation = this.labelRotation : this.labelRotation = 0; else if (u && (x = d, !n)) for (h = "clip", d = l.length; !q && d--;)if (p = l[d], p = g[p].label) p.styles && "ellipsis" === p.styles.textOverflow ? p.css({ textOverflow: "clip" }) : p.textPxLength > u && p.css({
                        width: u +
                            "px"
                    }), p.getBBox().height > this.len / l.length - (A.h - A.f) && (p.specificTextOverflow = "ellipsis"); k.rotation && (x = F > .5 * a.chartHeight ? .33 * a.chartHeight : F, n || (h = "ellipsis")); if (this.labelAlign = e.align || this.autoLabelAlign(this.labelRotation)) k.align = this.labelAlign; l.forEach(function (a) {
                        var b = (a = g[a]) && a.label, c = m.width, f = {}; b && (b.attr(k), a.shortenLabel ? a.shortenLabel() : x && !c && "nowrap" !== m.whiteSpace && (x < b.textPxLength || "SPAN" === b.element.tagName) ? (f.width = x, n || (f.textOverflow = b.specificTextOverflow || h), b.css(f)) :
                            b.styles && b.styles.width && !f.width && !c && b.css({ width: null }), delete b.specificTextOverflow, a.rotation = k.rotation)
                    }, this); this.tickRotCorr = c.rotCorr(A.b, this.labelRotation || 0, 0 !== this.side)
            }, hasData: function () { return this.hasVisibleSeries || r(this.min) && r(this.max) && this.tickPositions && 0 < this.tickPositions.length }, addTitle: function (a) {
                var b = this.chart.renderer, c = this.horiz, f = this.opposite, l = this.options.title, g, e = this.chart.styledMode; this.axisTitle || ((g = l.textAlign) || (g = (c ? {
                    low: "left", middle: "center",
                    high: "right"
                } : { low: f ? "right" : "left", middle: "center", high: f ? "left" : "right" })[l.align]), this.axisTitle = b.text(l.text, 0, 0, l.useHTML).attr({ zIndex: 7, rotation: l.rotation || 0, align: g }).addClass("highcharts-axis-title"), e || this.axisTitle.css(q(l.style)), this.axisTitle.add(this.axisGroup), this.axisTitle.isNew = !0); e || l.style.width || this.isRadial || this.axisTitle.css({ width: this.len }); this.axisTitle[a ? "show" : "hide"](!0)
            }, generateTick: function (a) { var b = this.ticks; b[a] ? b[a].addLabel() : b[a] = new F(this, a) }, getOffset: function () {
                var a =
                    this, b = a.chart, c = b.renderer, l = a.options, g = a.tickPositions, e = a.ticks, m = a.horiz, q = a.side, u = b.inverted && !a.isZAxis ? [1, 0, 3, 2][q] : q, d, k, A = 0, h, F = 0, p = l.title, E = l.labels, v = 0, G = b.axisOffset, b = b.clipOffset, t = [-1, 1, 1, -1][q], w = l.className, B = a.axisParent; d = a.hasData(); a.showAxis = k = d || D(l.showEmpty, !0); a.staggerLines = a.horiz && E.staggerLines; a.axisGroup || (a.gridGroup = c.g("grid").attr({ zIndex: l.gridZIndex || 1 }).addClass("highcharts-" + this.coll.toLowerCase() + "-grid " + (w || "")).add(B), a.axisGroup = c.g("axis").attr({
                        zIndex: l.zIndex ||
                            2
                    }).addClass("highcharts-" + this.coll.toLowerCase() + " " + (w || "")).add(B), a.labelGroup = c.g("axis-labels").attr({ zIndex: E.zIndex || 7 }).addClass("highcharts-" + a.coll.toLowerCase() + "-labels " + (w || "")).add(B)); d || a.isLinked ? (g.forEach(function (b, c) { a.generateTick(b, c) }), a.renderUnsquish(), a.reserveSpaceDefault = 0 === q || 2 === q || { 1: "left", 3: "right" }[q] === a.labelAlign, D(E.reserveSpace, "center" === a.labelAlign ? !0 : null, a.reserveSpaceDefault) && g.forEach(function (a) { v = Math.max(e[a].getLabelSize(), v) }), a.staggerLines &&
                        (v *= a.staggerLines), a.labelOffset = v * (a.opposite ? -1 : 1)) : x(e, function (a, b) { a.destroy(); delete e[b] }); p && p.text && !1 !== p.enabled && (a.addTitle(k), k && !1 !== p.reserveSpace && (a.titleOffset = A = a.axisTitle.getBBox()[m ? "height" : "width"], h = p.offset, F = r(h) ? 0 : D(p.margin, m ? 5 : 10))); a.renderLine(); a.offset = t * D(l.offset, G[q]); a.tickRotCorr = a.tickRotCorr || { x: 0, y: 0 }; c = 0 === q ? -a.labelMetrics().h : 2 === q ? a.tickRotCorr.y : 0; F = Math.abs(v) + F; v && (F = F - c + t * (m ? D(E.y, a.tickRotCorr.y + 8 * t) : E.x)); a.axisTitleMargin = D(h, F); a.getMaxLabelDimensions &&
                            (a.maxLabelDimensions = a.getMaxLabelDimensions(e, g)); m = this.tickSize("tick"); G[q] = Math.max(G[q], a.axisTitleMargin + A + t * a.offset, F, d && g.length && m ? m[0] + t * a.offset : 0); l = l.offset ? 0 : 2 * Math.floor(a.axisLine.strokeWidth() / 2); b[u] = Math.max(b[u], l); n(this, "afterGetOffset")
            }, getLinePath: function (a) {
                var b = this.chart, c = this.opposite, f = this.offset, l = this.horiz, g = this.left + (c ? this.width : 0) + f, f = b.chartHeight - this.bottom - (c ? this.height : 0) + f; c && (a *= -1); return b.renderer.crispLine(["M", l ? this.left : g, l ? f : this.top, "L",
                    l ? b.chartWidth - this.right : g, l ? f : b.chartHeight - this.bottom], a)
            }, renderLine: function () { this.axisLine || (this.axisLine = this.chart.renderer.path().addClass("highcharts-axis-line").add(this.axisGroup), this.chart.styledMode || this.axisLine.attr({ stroke: this.options.lineColor, "stroke-width": this.options.lineWidth, zIndex: 7 })) }, getTitlePosition: function () {
                var a = this.horiz, b = this.left, c = this.top, l = this.len, g = this.options.title, e = a ? b : c, m = this.opposite, q = this.offset, u = g.x || 0, d = g.y || 0, k = this.axisTitle, A = this.chart.renderer.fontMetrics(g.style &&
                    g.style.fontSize, k), k = Math.max(k.getBBox(null, 0).height - A.h - 1, 0), l = { low: e + (a ? 0 : l), middle: e + l / 2, high: e + (a ? l : 0) }[g.align], b = (a ? c + this.height : b) + (a ? 1 : -1) * (m ? -1 : 1) * this.axisTitleMargin + [-k, k, A.f, -k][this.side]; return { x: a ? l + u : b + (m ? this.width : 0) + q + u, y: a ? b + d - (m ? this.height : 0) + q : l + d }
            }, renderMinorTick: function (a) { var b = this.chart.hasRendered && g(this.oldMin), c = this.minorTicks; c[a] || (c[a] = new F(this, a, "minor")); b && c[a].isNew && c[a].render(null, !0); c[a].render(null, !1, 1) }, renderTick: function (a, b) {
                var c = this.isLinked,
                f = this.ticks, l = this.chart.hasRendered && g(this.oldMin); if (!c || a >= this.min && a <= this.max) f[a] || (f[a] = new F(this, a)), l && f[a].isNew && f[a].render(b, !0, -1), f[a].render(b)
            }, render: function () {
                var b = this, c = b.chart, l = b.options, e = b.isLog, m = b.isLinked, q = b.tickPositions, u = b.axisTitle, d = b.ticks, k = b.minorTicks, h = b.alternateBands, p = l.stackLabels, E = l.alternateGridColor, v = b.tickmarkOffset, D = b.axisLine, G = b.showAxis, r = C(c.renderer.globalAnimation), t, w; b.labelEdge.length = 0; b.overlap = !1;[d, k, h].forEach(function (a) {
                    x(a,
                        function (a) { a.isActive = !1 })
                }); if (b.hasData() || m) b.minorTickInterval && !b.categories && b.getMinorTickPositions().forEach(function (a) { b.renderMinorTick(a) }), q.length && (q.forEach(function (a, c) { b.renderTick(a, c) }), v && (0 === b.min || b.single) && (d[-1] || (d[-1] = new F(b, -1, null, !0)), d[-1].render(-1))), E && q.forEach(function (f, l) {
                    w = void 0 !== q[l + 1] ? q[l + 1] + v : b.max - v; 0 === l % 2 && f < b.max && w <= b.max + (c.polar ? -v : v) && (h[f] || (h[f] = new a.PlotLineOrBand(b)), t = f + v, h[f].options = { from: e ? b.lin2log(t) : t, to: e ? b.lin2log(w) : w, color: E },
                        h[f].render(), h[f].isActive = !0)
                }), b._addedPlotLB || ((l.plotLines || []).concat(l.plotBands || []).forEach(function (a) { b.addPlotBandOrLine(a) }), b._addedPlotLB = !0);[d, k, h].forEach(function (a) { var b, f = [], l = r.duration; x(a, function (a, b) { a.isActive || (a.render(b, !1, 0), a.isActive = !1, f.push(b)) }); A(function () { for (b = f.length; b--;)a[f[b]] && !a[f[b]].isActive && (a[f[b]].destroy(), delete a[f[b]]) }, a !== h && c.hasRendered && l ? l : 0) }); D && (D[D.isPlaced ? "animate" : "attr"]({ d: this.getLinePath(D.strokeWidth()) }), D.isPlaced = !0, D[G ?
                    "show" : "hide"](!0)); u && G && (l = b.getTitlePosition(), g(l.y) ? (u[u.isNew ? "attr" : "animate"](l), u.isNew = !1) : (u.attr("y", -9999), u.isNew = !0)); p && p.enabled && b.renderStackTotals(); b.isDirty = !1; n(this, "afterRender")
            }, redraw: function () { this.visible && (this.render(), this.plotLinesAndBands.forEach(function (a) { a.render() })); this.series.forEach(function (a) { a.isDirty = !0 }) }, keepProps: "extKey hcEvents names series userMax userMin".split(" "), destroy: function (a) {
                var b = this, c = b.stacks, f = b.plotLinesAndBands, l; n(this, "destroy",
                    { keepEvents: a }); a || E(b); x(c, function (a, b) { p(a); c[b] = null });[b.ticks, b.minorTicks, b.alternateBands].forEach(function (a) { p(a) }); if (f) for (a = f.length; a--;)f[a].destroy(); "stackTotalGroup axisLine axisTitle axisGroup gridGroup labelGroup cross scrollbar".split(" ").forEach(function (a) { b[a] && (b[a] = b[a].destroy()) }); for (l in b.plotLinesAndBandsGroups) b.plotLinesAndBandsGroups[l] = b.plotLinesAndBandsGroups[l].destroy(); x(b, function (a, c) { -1 === b.keepProps.indexOf(c) && delete b[c] })
            }, drawCrosshair: function (a,
                b) {
                    var c, f = this.crosshair, l = D(f.snap, !0), g, e = this.cross; n(this, "drawCrosshair", { e: a, point: b }); a || (a = this.cross && this.cross.e); if (this.crosshair && !1 !== (r(b) || !l)) {
                        l ? r(b) && (g = D(b.crosshairPos, this.isXAxis ? b.plotX : this.len - b.plotY)) : g = a && (this.horiz ? a.chartX - this.pos : this.len - a.chartY + this.pos); r(g) && (c = this.getPlotLinePath(b && (this.isXAxis ? b.x : D(b.stackY, b.y)), null, null, null, g) || null); if (!r(c)) { this.hideCrosshair(); return } l = this.categories && !this.isRadial; e || (this.cross = e = this.chart.renderer.path().addClass("highcharts-crosshair highcharts-crosshair-" +
                            (l ? "category " : "thin ") + f.className).attr({ zIndex: D(f.zIndex, 2) }).add(), this.chart.styledMode || (e.attr({ stroke: f.color || (l ? d("#ccd6eb").setOpacity(.25).get() : "#cccccc"), "stroke-width": D(f.width, 1) }).css({ "pointer-events": "none" }), f.dashStyle && e.attr({ dashstyle: f.dashStyle }))); e.show().attr({ d: c }); l && !f.width && e.attr({ "stroke-width": this.transA }); this.cross.e = a
                    } else this.hideCrosshair(); n(this, "afterDrawCrosshair", { e: a, point: b })
            }, hideCrosshair: function () { this.cross && this.cross.hide() }
        }); return a.Axis =
            G
    }(H); (function (a) {
        var z = a.Axis, C = a.getMagnitude, B = a.normalizeTickInterval, h = a.pick; z.prototype.getLogTickPositions = function (a, t, w, r) {
            var d = this.options, p = this.len, k = []; r || (this._minorAutoInterval = null); if (.5 <= a) a = Math.round(a), k = this.getLinearTickPositions(a, t, w); else if (.08 <= a) for (var p = Math.floor(t), n, e, m, c, g, d = .3 < a ? [1, 2, 4] : .15 < a ? [1, 2, 4, 6, 8] : [1, 2, 3, 4, 5, 6, 7, 8, 9]; p < w + 1 && !g; p++)for (e = d.length, n = 0; n < e && !g; n++)m = this.log2lin(this.lin2log(p) * d[n]), m > t && (!r || c <= w) && void 0 !== c && k.push(c), c > w && (g = !0), c =
                m; else t = this.lin2log(t), w = this.lin2log(w), a = r ? this.getMinorTickInterval() : d.tickInterval, a = h("auto" === a ? null : a, this._minorAutoInterval, d.tickPixelInterval / (r ? 5 : 1) * (w - t) / ((r ? p / this.tickPositions.length : p) || 1)), a = B(a, null, C(a)), k = this.getLinearTickPositions(a, t, w).map(this.log2lin), r || (this._minorAutoInterval = a / 5); r || (this.tickInterval = a); return k
        }; z.prototype.log2lin = function (a) { return Math.log(a) / Math.LN10 }; z.prototype.lin2log = function (a) { return Math.pow(10, a) }
    })(H); (function (a, z) {
        var C = a.arrayMax,
        B = a.arrayMin, h = a.defined, d = a.destroyObjectProperties, t = a.erase, w = a.merge, r = a.pick; a.PlotLineOrBand = function (a, d) { this.axis = a; d && (this.options = d, this.id = d.id) }; a.PlotLineOrBand.prototype = {
            render: function () {
                a.fireEvent(this, "render"); var d = this, p = d.axis, k = p.horiz, n = d.options, e = n.label, m = d.label, c = n.to, g = n.from, b = n.value, q = h(g) && h(c), u = h(b), x = d.svgElem, D = !x, E = [], l = n.color, A = r(n.zIndex, 0), F = n.events, E = { "class": "highcharts-plot-" + (q ? "band " : "line ") + (n.className || "") }, G = {}, f = p.chart.renderer, y = q ? "bands" :
                    "lines"; p.isLog && (g = p.log2lin(g), c = p.log2lin(c), b = p.log2lin(b)); p.chart.styledMode || (u ? (E.stroke = l, E["stroke-width"] = n.width, n.dashStyle && (E.dashstyle = n.dashStyle)) : q && (l && (E.fill = l), n.borderWidth && (E.stroke = n.borderColor, E["stroke-width"] = n.borderWidth))); G.zIndex = A; y += "-" + A; (l = p.plotLinesAndBandsGroups[y]) || (p.plotLinesAndBandsGroups[y] = l = f.g("plot-" + y).attr(G).add()); D && (d.svgElem = x = f.path().attr(E).add(l)); if (u) E = p.getPlotLinePath(b, x.strokeWidth()); else if (q) E = p.getPlotBandPath(g, c, n); else return;
                D && E && E.length ? (x.attr({ d: E }), F && a.objectEach(F, function (a, b) { x.on(b, function (a) { F[b].apply(d, [a]) }) })) : x && (E ? (x.show(), x.animate({ d: E })) : (x.hide(), m && (d.label = m = m.destroy()))); e && h(e.text) && E && E.length && 0 < p.width && 0 < p.height && !E.isFlat ? (e = w({ align: k && q && "center", x: k ? !q && 4 : 10, verticalAlign: !k && q && "middle", y: k ? q ? 16 : 10 : q ? 6 : -4, rotation: k && !q && 90 }, e), this.renderLabel(e, E, q, A)) : m && m.hide(); return d
            }, renderLabel: function (a, d, k, n) {
                var e = this.label, m = this.axis.chart.renderer; e || (e = {
                    align: a.textAlign || a.align,
                    rotation: a.rotation, "class": "highcharts-plot-" + (k ? "band" : "line") + "-label " + (a.className || "")
                }, e.zIndex = n, this.label = e = m.text(a.text, 0, 0, a.useHTML).attr(e).add(), this.axis.chart.styledMode || e.css(a.style)); n = d.xBounds || [d[1], d[4], k ? d[6] : d[1]]; d = d.yBounds || [d[2], d[5], k ? d[7] : d[2]]; k = B(n); m = B(d); e.align(a, !1, { x: k, y: m, width: C(n) - k, height: C(d) - m }); e.show()
            }, destroy: function () { t(this.axis.plotLinesAndBands, this); delete this.axis; d(this) }
        }; a.extend(z.prototype, {
            getPlotBandPath: function (a, d) {
                var k = this.getPlotLinePath(d,
                    null, null, !0), n = this.getPlotLinePath(a, null, null, !0), e = [], m = this.horiz, c = 1, g; a = a < this.min && d < this.min || a > this.max && d > this.max; if (n && k) for (a && (g = n.toString() === k.toString(), c = 0), a = 0; a < n.length; a += 6)m && k[a + 1] === n[a + 1] ? (k[a + 1] += c, k[a + 4] += c) : m || k[a + 2] !== n[a + 2] || (k[a + 2] += c, k[a + 5] += c), e.push("M", n[a + 1], n[a + 2], "L", n[a + 4], n[a + 5], k[a + 4], k[a + 5], k[a + 1], k[a + 2], "z"), e.isFlat = g; return e
            }, addPlotBand: function (a) { return this.addPlotBandOrLine(a, "plotBands") }, addPlotLine: function (a) {
                return this.addPlotBandOrLine(a,
                    "plotLines")
            }, addPlotBandOrLine: function (d, h) { var k = (new a.PlotLineOrBand(this, d)).render(), n = this.userOptions; k && (h && (n[h] = n[h] || [], n[h].push(d)), this.plotLinesAndBands.push(k)); return k }, removePlotBandOrLine: function (a) { for (var d = this.plotLinesAndBands, k = this.options, n = this.userOptions, e = d.length; e--;)d[e].id === a && d[e].destroy();[k.plotLines || [], n.plotLines || [], k.plotBands || [], n.plotBands || []].forEach(function (m) { for (e = m.length; e--;)m[e].id === a && t(m, m[e]) }) }, removePlotBand: function (a) { this.removePlotBandOrLine(a) },
            removePlotLine: function (a) { this.removePlotBandOrLine(a) }
        })
    })(H, W); (function (a) {
        var z = a.doc, C = a.extend, B = a.format, h = a.isNumber, d = a.merge, t = a.pick, w = a.splat, r = a.syncTimeout, v = a.timeUnits; a.Tooltip = function () { this.init.apply(this, arguments) }; a.Tooltip.prototype = {
            init: function (a, d) { this.chart = a; this.options = d; this.crosshairs = []; this.now = { x: 0, y: 0 }; this.isHidden = !0; this.split = d.split && !a.inverted; this.shared = d.shared || this.split; this.outside = d.outside && !this.split }, cleanSplit: function (a) {
                this.chart.series.forEach(function (d) {
                    var k =
                        d && d.tt; k && (!k.isActive || a ? d.tt = k.destroy() : k.isActive = !1)
                })
            }, applyFilter: function () {
                var a = this.chart; a.renderer.definition({ tagName: "filter", id: "drop-shadow-" + a.index, opacity: .5, children: [{ tagName: "feGaussianBlur", "in": "SourceAlpha", stdDeviation: 1 }, { tagName: "feOffset", dx: 1, dy: 1 }, { tagName: "feComponentTransfer", children: [{ tagName: "feFuncA", type: "linear", slope: .3 }] }, { tagName: "feMerge", children: [{ tagName: "feMergeNode" }, { tagName: "feMergeNode", "in": "SourceGraphic" }] }] }); a.renderer.definition({
                    tagName: "style",
                    textContent: ".highcharts-tooltip-" + a.index + "{filter:url(#drop-shadow-" + a.index + ")}"
                })
            }, getLabel: function () {
                var d = this.chart.renderer, k = this.chart.styledMode, n = this.options, e; this.label || (this.outside && (this.container = e = a.doc.createElement("div"), e.className = "highcharts-tooltip-container", a.css(e, { position: "absolute", top: "1px", pointerEvents: n.style && n.style.pointerEvents }), a.doc.body.appendChild(e), this.renderer = d = new a.Renderer(e, 0, 0)), this.split ? this.label = d.g("tooltip") : (this.label = d.label("", 0,
                    0, n.shape || "callout", null, null, n.useHTML, null, "tooltip").attr({ padding: n.padding, r: n.borderRadius }), k || this.label.attr({ fill: n.backgroundColor, "stroke-width": n.borderWidth }).css(n.style).shadow(n.shadow)), k && (this.applyFilter(), this.label.addClass("highcharts-tooltip-" + this.chart.index)), this.outside && (this.label.attr({ x: this.distance, y: this.distance }), this.label.xSetter = function (a) { e.style.left = a + "px" }, this.label.ySetter = function (a) { e.style.top = a + "px" }), this.label.attr({ zIndex: 8 }).add()); return this.label
            },
            update: function (a) { this.destroy(); d(!0, this.chart.options.tooltip.userOptions, a); this.init(this.chart, d(!0, this.options, a)) }, destroy: function () { this.label && (this.label = this.label.destroy()); this.split && this.tt && (this.cleanSplit(this.chart, !0), this.tt = this.tt.destroy()); this.renderer && (this.renderer = this.renderer.destroy(), a.discardElement(this.container)); a.clearTimeout(this.hideTimer); a.clearTimeout(this.tooltipTimeout) }, move: function (d, k, n, e) {
                var m = this, c = m.now, g = !1 !== m.options.animation && !m.isHidden &&
                    (1 < Math.abs(d - c.x) || 1 < Math.abs(k - c.y)), b = m.followPointer || 1 < m.len; C(c, { x: g ? (2 * c.x + d) / 3 : d, y: g ? (c.y + k) / 2 : k, anchorX: b ? void 0 : g ? (2 * c.anchorX + n) / 3 : n, anchorY: b ? void 0 : g ? (c.anchorY + e) / 2 : e }); m.getLabel().attr(c); g && (a.clearTimeout(this.tooltipTimeout), this.tooltipTimeout = setTimeout(function () { m && m.move(d, k, n, e) }, 32))
            }, hide: function (d) { var k = this; a.clearTimeout(this.hideTimer); d = t(d, this.options.hideDelay, 500); this.isHidden || (this.hideTimer = r(function () { k.getLabel()[d ? "fadeOut" : "hide"](); k.isHidden = !0 }, d)) },
            getAnchor: function (a, d) {
                var k = this.chart, e = k.pointer, m = k.inverted, c = k.plotTop, g = k.plotLeft, b = 0, q = 0, u, x; a = w(a); this.followPointer && d ? (void 0 === d.chartX && (d = e.normalize(d)), a = [d.chartX - k.plotLeft, d.chartY - c]) : a[0].tooltipPos ? a = a[0].tooltipPos : (a.forEach(function (a) { u = a.series.yAxis; x = a.series.xAxis; b += a.plotX + (!m && x ? x.left - g : 0); q += (a.plotLow ? (a.plotLow + a.plotHigh) / 2 : a.plotY) + (!m && u ? u.top - c : 0) }), b /= a.length, q /= a.length, a = [m ? k.plotWidth - q : b, this.shared && !m && 1 < a.length && d ? d.chartY - c : m ? k.plotHeight - b : q]);
                return a.map(Math.round)
            }, getPosition: function (a, d, n) {
                var e = this.chart, m = this.distance, c = {}, g = e.inverted && n.h || 0, b, q = this.outside, u = q ? z.documentElement.clientWidth - 2 * m : e.chartWidth, k = q ? Math.max(z.body.scrollHeight, z.documentElement.scrollHeight, z.body.offsetHeight, z.documentElement.offsetHeight, z.documentElement.clientHeight) : e.chartHeight, h = e.pointer.chartPosition, E = ["y", k, d, (q ? h.top - m : 0) + n.plotY + e.plotTop, q ? 0 : e.plotTop, q ? k : e.plotTop + e.plotHeight], l = ["x", u, a, (q ? h.left - m : 0) + n.plotX + e.plotLeft, q ? 0 :
                    e.plotLeft, q ? u : e.plotLeft + e.plotWidth], A = !this.followPointer && t(n.ttBelow, !e.inverted === !!n.negative), F = function (a, b, f, l, e, q) { var d = f < l - m, u = l + m + f < b, k = l - m - f; l += m; if (A && u) c[a] = l; else if (!A && d) c[a] = k; else if (d) c[a] = Math.min(q - f, 0 > k - g ? k : k - g); else if (u) c[a] = Math.max(e, l + g + f > b ? l : l + g); else return !1 }, p = function (a, b, f, l) { var g; l < m || l > b - m ? g = !1 : c[a] = l < f / 2 ? 1 : l > b - f / 2 ? b - f - 2 : l - f / 2; return g }, f = function (a) { var c = E; E = l; l = c; b = a }, y = function () { !1 !== F.apply(0, E) ? !1 !== p.apply(0, l) || b || (f(!0), y()) : b ? c.x = c.y = 0 : (f(!0), y()) };
                (e.inverted || 1 < this.len) && f(); y(); return c
            }, defaultFormatter: function (a) { var d = this.points || w(this), n; n = [a.tooltipFooterHeaderFormatter(d[0])]; n = n.concat(a.bodyFormatter(d)); n.push(a.tooltipFooterHeaderFormatter(d[0], !0)); return n }, refresh: function (d, k) {
                var n, e = this.options, m, c = d, g, b = {}, q = []; n = e.formatter || this.defaultFormatter; var b = this.shared, u, x = this.chart.styledMode; e.enabled && (a.clearTimeout(this.hideTimer), this.followPointer = w(c)[0].series.tooltipOptions.followPointer, g = this.getAnchor(c, k),
                    k = g[0], m = g[1], !b || c.series && c.series.noSharedTooltip ? b = c.getLabelConfig() : (c.forEach(function (a) { a.setState("hover"); q.push(a.getLabelConfig()) }), b = { x: c[0].category, y: c[0].y }, b.points = q, c = c[0]), this.len = q.length, b = n.call(b, this), u = c.series, this.distance = t(u.tooltipOptions.distance, 16), !1 === b ? this.hide() : (n = this.getLabel(), this.isHidden && n.attr({ opacity: 1 }).show(), this.split ? this.renderSplit(b, w(d)) : (e.style.width && !x || n.css({ width: this.chart.spacingBox.width }), n.attr({ text: b && b.join ? b.join("") : b }),
                        n.removeClass(/highcharts-color-[\d]+/g).addClass("highcharts-color-" + t(c.colorIndex, u.colorIndex)), x || n.attr({ stroke: e.borderColor || c.color || u.color || "#666666" }), this.updatePosition({ plotX: k, plotY: m, negative: c.negative, ttBelow: c.ttBelow, h: g[2] || 0 })), this.isHidden = !1))
            }, renderSplit: function (d, k) {
                var n = this, e = [], m = this.chart, c = m.renderer, g = !0, b = this.options, q = 0, u, x = this.getLabel(), h = m.plotTop; a.isString(d) && (d = [!1, d]); d.slice(0, k.length + 1).forEach(function (a, l) {
                    if (!1 !== a && "" !== a) {
                        l = k[l - 1] || {
                            isHeader: !0,
                            plotX: k[0].plotX, plotY: m.plotHeight
                        }; var d = l.series || n, F = d.tt, E = l.series || {}, f = "highcharts-color-" + t(l.colorIndex, E.colorIndex, "none"); F || (F = { padding: b.padding, r: b.borderRadius }, m.styledMode || (F.fill = b.backgroundColor, F.stroke = b.borderColor || l.color || E.color || "#333333", F["stroke-width"] = b.borderWidth), d.tt = F = c.label(null, null, null, (l.isHeader ? b.headerShape : b.shape) || "callout", null, null, b.useHTML).addClass("highcharts-tooltip-box " + f).attr(F).add(x)); F.isActive = !0; F.attr({ text: a }); m.styledMode || F.css(b.style).shadow(b.shadow);
                        a = F.getBBox(); E = a.width + F.strokeWidth(); l.isHeader ? (q = a.height, m.xAxis[0].opposite && (u = !0, h -= q), E = Math.max(0, Math.min(l.plotX + m.plotLeft - E / 2, m.chartWidth + (m.scrollablePixels ? m.scrollablePixels - m.marginRight : 0) - E))) : E = l.plotX + m.plotLeft - t(b.distance, 16) - E; 0 > E && (g = !1); a = (l.series && l.series.yAxis && l.series.yAxis.pos) + (l.plotY || 0); a -= h; l.isHeader && (a = u ? -q : m.plotHeight + q); e.push({ target: a, rank: l.isHeader ? 1 : 0, size: d.tt.getBBox().height + 1, point: l, x: E, tt: F })
                    }
                }); this.cleanSplit(); b.positioner && e.forEach(function (a) {
                    var c =
                        b.positioner.call(n, a.tt.getBBox().width, a.size, a.point); a.x = c.x; a.align = 0; a.target = c.y; a.rank = t(c.rank, a.rank)
                }); a.distribute(e, m.plotHeight + q); e.forEach(function (a) { var c = a.point, e = c.series; a.tt.attr({ visibility: void 0 === a.pos ? "hidden" : "inherit", x: g || c.isHeader || b.positioner ? a.x : c.plotX + m.plotLeft + t(b.distance, 16), y: a.pos + h, anchorX: c.isHeader ? c.plotX + m.plotLeft : c.plotX + e.xAxis.pos, anchorY: c.isHeader ? m.plotTop + m.plotHeight / 2 : c.plotY + e.yAxis.pos }) })
            }, updatePosition: function (a) {
                var d = this.chart, n =
                    this.getLabel(), e = (this.options.positioner || this.getPosition).call(this, n.width, n.height, a), m = a.plotX + d.plotLeft; a = a.plotY + d.plotTop; var c; this.outside && (c = (this.options.borderWidth || 0) + 2 * this.distance, this.renderer.setSize(n.width + c, n.height + c, !1), m += d.pointer.chartPosition.left - e.x, a += d.pointer.chartPosition.top - e.y); this.move(Math.round(e.x), Math.round(e.y || 0), m, a)
            }, getDateFormat: function (a, d, n, e) {
                var m = this.chart.time, c = m.dateFormat("%m-%d %H:%M:%S.%L", d), g, b, q = {
                    millisecond: 15, second: 12, minute: 9,
                    hour: 6, day: 3
                }, u = "millisecond"; for (b in v) { if (a === v.week && +m.dateFormat("%w", d) === n && "00:00:00.000" === c.substr(6)) { b = "week"; break } if (v[b] > a) { b = u; break } if (q[b] && c.substr(q[b]) !== "01-01 00:00:00.000".substr(q[b])) break; "week" !== b && (u = b) } b && (g = m.resolveDTLFormat(e[b]).main); return g
            }, getXDateFormat: function (a, d, n) { d = d.dateTimeLabelFormats; var e = n && n.closestPointRange; return (e ? this.getDateFormat(e, a.x, n.options.startOfWeek, d) : d.day) || d.year }, tooltipFooterHeaderFormatter: function (a, d) {
                d = d ? "footer" : "header";
                var k = a.series, e = k.tooltipOptions, m = e.xDateFormat, c = k.xAxis, g = c && "datetime" === c.options.type && h(a.key), b = e[d + "Format"]; g && !m && (m = this.getXDateFormat(a, e, c)); g && m && (a.point && a.point.tooltipDateKeys || ["key"]).forEach(function (a) { b = b.replace("{point." + a + "}", "{point." + a + ":" + m + "}") }); k.chart.styledMode && (b = this.styledModeFormat(b)); return B(b, { point: a, series: k }, this.chart.time)
            }, bodyFormatter: function (a) {
                return a.map(function (a) {
                    var d = a.series.tooltipOptions; return (d[(a.point.formatPrefix || "point") + "Formatter"] ||
                        a.point.tooltipFormatter).call(a.point, d[(a.point.formatPrefix || "point") + "Format"] || "")
                })
            }, styledModeFormat: function (a) { return a.replace('style\x3d"font-size: 10px"', 'class\x3d"highcharts-header"').replace(/style="color:{(point|series)\.color}"/g, 'class\x3d"highcharts-color-{$1.colorIndex}"') }
        }
    })(H); (function (a) {
        var z = a.addEvent, C = a.attr, B = a.charts, h = a.color, d = a.css, t = a.defined, w = a.extend, r = a.find, v = a.fireEvent, p = a.isNumber, k = a.isObject, n = a.offset, e = a.pick, m = a.splat, c = a.Tooltip; a.Pointer = function (a,
            b) { this.init(a, b) }; a.Pointer.prototype = {
                init: function (a, b) { this.options = b; this.chart = a; this.runChartClick = b.chart.events && !!b.chart.events.click; this.pinchDown = []; this.lastValidTouch = {}; c && (a.tooltip = new c(a, b.tooltip), this.followTouchMove = e(b.tooltip.followTouchMove, !0)); this.setDOMEvents() }, zoomOption: function (a) {
                    var b = this.chart, c = b.options.chart, g = c.zoomType || "", b = b.inverted; /touch/.test(a.type) && (g = e(c.pinchType, g)); this.zoomX = a = /x/.test(g); this.zoomY = g = /y/.test(g); this.zoomHor = a && !b || g && b; this.zoomVert =
                        g && !b || a && b; this.hasZoom = a || g
                }, normalize: function (a, b) { var c; c = a.touches ? a.touches.length ? a.touches.item(0) : a.changedTouches[0] : a; b || (this.chartPosition = b = n(this.chart.container)); return w(a, { chartX: Math.round(c.pageX - b.left), chartY: Math.round(c.pageY - b.top) }) }, getCoordinates: function (a) { var b = { xAxis: [], yAxis: [] }; this.chart.axes.forEach(function (c) { b[c.isXAxis ? "xAxis" : "yAxis"].push({ axis: c, value: c.toValue(a[c.horiz ? "chartX" : "chartY"]) }) }); return b }, findNearestKDPoint: function (a, b, c) {
                    var g; a.forEach(function (a) {
                        var e =
                            !(a.noSharedTooltip && b) && 0 > a.options.findNearestPointBy.indexOf("y"); a = a.searchPoint(c, e); if ((e = k(a, !0)) && !(e = !k(g, !0))) var e = g.distX - a.distX, d = g.dist - a.dist, l = (a.series.group && a.series.group.zIndex) - (g.series.group && g.series.group.zIndex), e = 0 < (0 !== e && b ? e : 0 !== d ? d : 0 !== l ? l : g.series.index > a.series.index ? -1 : 1); e && (g = a)
                    }); return g
                }, getPointFromEvent: function (a) { a = a.target; for (var b; a && !b;)b = a.point, a = a.parentNode; return b }, getChartCoordinatesFromPoint: function (a, b) {
                    var c = a.series, g = c.xAxis, c = c.yAxis, d =
                        e(a.clientX, a.plotX), m = a.shapeArgs; if (g && c) return b ? { chartX: g.len + g.pos - d, chartY: c.len + c.pos - a.plotY } : { chartX: d + g.pos, chartY: a.plotY + c.pos }; if (m && m.x && m.y) return { chartX: m.x, chartY: m.y }
                }, getHoverData: function (a, b, c, d, m, n, h) {
                    var l, g = [], q = h && h.isBoosting; d = !(!d || !a); h = b && !b.stickyTracking ? [b] : c.filter(function (a) { return a.visible && !(!m && a.directTouch) && e(a.options.enableMouseTracking, !0) && a.stickyTracking }); b = (l = d ? a : this.findNearestKDPoint(h, m, n)) && l.series; l && (m && !b.noSharedTooltip ? (h = c.filter(function (a) {
                        return a.visible &&
                            !(!m && a.directTouch) && e(a.options.enableMouseTracking, !0) && !a.noSharedTooltip
                    }), h.forEach(function (a) { var b = r(a.points, function (a) { return a.x === l.x && !a.isNull }); k(b) && (q && (b = a.getPoint(b)), g.push(b)) })) : g.push(l)); return { hoverPoint: l, hoverSeries: b, hoverPoints: g }
                }, runPointActions: function (c, b) {
                    var g = this.chart, d = g.tooltip && g.tooltip.options.enabled ? g.tooltip : void 0, m = d ? d.shared : !1, k = b || g.hoverPoint, n = k && k.series || g.hoverSeries, n = this.getHoverData(k, n, g.series, "touchmove" !== c.type && (!!b || n && n.directTouch &&
                        this.isDirectTouch), m, c, { isBoosting: g.isBoosting }), l, k = n.hoverPoint; l = n.hoverPoints; b = (n = n.hoverSeries) && n.tooltipOptions.followPointer; m = m && n && !n.noSharedTooltip; if (k && (k !== g.hoverPoint || d && d.isHidden)) {
                            (g.hoverPoints || []).forEach(function (a) { -1 === l.indexOf(a) && a.setState() }); (l || []).forEach(function (a) { a.setState("hover") }); if (g.hoverSeries !== n) n.onMouseOver(); g.hoverPoint && g.hoverPoint.firePointEvent("mouseOut"); if (!k.series) return; k.firePointEvent("mouseOver"); g.hoverPoints = l; g.hoverPoint = k; d &&
                                d.refresh(m ? l : k, c)
                        } else b && d && !d.isHidden && (k = d.getAnchor([{}], c), d.updatePosition({ plotX: k[0], plotY: k[1] })); this.unDocMouseMove || (this.unDocMouseMove = z(g.container.ownerDocument, "mousemove", function (b) { var c = B[a.hoverChartIndex]; if (c) c.pointer.onDocumentMouseMove(b) })); g.axes.forEach(function (b) { var g = e(b.crosshair.snap, !0), d = g ? a.find(l, function (a) { return a.series[b.coll] === b }) : void 0; d || !g ? b.drawCrosshair(c, d) : b.hideCrosshair() })
                }, reset: function (a, b) {
                    var c = this.chart, g = c.hoverSeries, e = c.hoverPoint,
                    d = c.hoverPoints, k = c.tooltip, l = k && k.shared ? d : e; a && l && m(l).forEach(function (b) { b.series.isCartesian && void 0 === b.plotX && (a = !1) }); if (a) k && l && l.length && (k.refresh(l), k.shared && d ? d.forEach(function (a) { a.setState(a.state, !0); a.series.isCartesian && (a.series.xAxis.crosshair && a.series.xAxis.drawCrosshair(null, a), a.series.yAxis.crosshair && a.series.yAxis.drawCrosshair(null, a)) }) : e && (e.setState(e.state, !0), c.axes.forEach(function (a) { a.crosshair && a.drawCrosshair(null, e) }))); else {
                        if (e) e.onMouseOut(); d && d.forEach(function (a) { a.setState() });
                        if (g) g.onMouseOut(); k && k.hide(b); this.unDocMouseMove && (this.unDocMouseMove = this.unDocMouseMove()); c.axes.forEach(function (a) { a.hideCrosshair() }); this.hoverX = c.hoverPoints = c.hoverPoint = null
                    }
                }, scaleGroups: function (a, b) { var c = this.chart, g; c.series.forEach(function (e) { g = a || e.getPlotBox(); e.xAxis && e.xAxis.zoomEnabled && e.group && (e.group.attr(g), e.markerGroup && (e.markerGroup.attr(g), e.markerGroup.clip(b ? c.clipRect : null)), e.dataLabelsGroup && e.dataLabelsGroup.attr(g)) }); c.clipRect.attr(b || c.clipBox) }, dragStart: function (a) {
                    var b =
                        this.chart; b.mouseIsDown = a.type; b.cancelClick = !1; b.mouseDownX = this.mouseDownX = a.chartX; b.mouseDownY = this.mouseDownY = a.chartY
                }, drag: function (a) {
                    var b = this.chart, c = b.options.chart, g = a.chartX, e = a.chartY, d = this.zoomHor, m = this.zoomVert, l = b.plotLeft, k = b.plotTop, n = b.plotWidth, p = b.plotHeight, f, y = this.selectionMarker, v = this.mouseDownX, r = this.mouseDownY, t = c.panKey && a[c.panKey + "Key"]; y && y.touch || (g < l ? g = l : g > l + n && (g = l + n), e < k ? e = k : e > k + p && (e = k + p), this.hasDragged = Math.sqrt(Math.pow(v - g, 2) + Math.pow(r - e, 2)), 10 < this.hasDragged &&
                        (f = b.isInsidePlot(v - l, r - k), b.hasCartesianSeries && (this.zoomX || this.zoomY) && f && !t && !y && (this.selectionMarker = y = b.renderer.rect(l, k, d ? 1 : n, m ? 1 : p, 0).attr({ "class": "highcharts-selection-marker", zIndex: 7 }).add(), b.styledMode || y.attr({ fill: c.selectionMarkerFill || h("#335cad").setOpacity(.25).get() })), y && d && (g -= v, y.attr({ width: Math.abs(g), x: (0 < g ? 0 : g) + v })), y && m && (g = e - r, y.attr({ height: Math.abs(g), y: (0 < g ? 0 : g) + r })), f && !y && c.panning && b.pan(a, c.panning)))
                }, drop: function (a) {
                    var b = this, c = this.chart, g = this.hasPinched;
                    if (this.selectionMarker) {
                        var e = { originalEvent: a, xAxis: [], yAxis: [] }, m = this.selectionMarker, k = m.attr ? m.attr("x") : m.x, l = m.attr ? m.attr("y") : m.y, n = m.attr ? m.attr("width") : m.width, h = m.attr ? m.attr("height") : m.height, G; if (this.hasDragged || g) c.axes.forEach(function (c) {
                            if (c.zoomEnabled && t(c.min) && (g || b[{ xAxis: "zoomX", yAxis: "zoomY" }[c.coll]])) {
                                var f = c.horiz, d = "touchend" === a.type ? c.minPixelPadding : 0, m = c.toValue((f ? k : l) + d), f = c.toValue((f ? k + n : l + h) - d); e[c.coll].push({ axis: c, min: Math.min(m, f), max: Math.max(m, f) });
                                G = !0
                            }
                        }), G && v(c, "selection", e, function (a) { c.zoom(w(a, g ? { animation: !1 } : null)) }); p(c.index) && (this.selectionMarker = this.selectionMarker.destroy()); g && this.scaleGroups()
                    } c && p(c.index) && (d(c.container, { cursor: c._cursor }), c.cancelClick = 10 < this.hasDragged, c.mouseIsDown = this.hasDragged = this.hasPinched = !1, this.pinchDown = [])
                }, onContainerMouseDown: function (a) { a = this.normalize(a); 2 !== a.button && (this.zoomOption(a), a.preventDefault && a.preventDefault(), this.dragStart(a)) }, onDocumentMouseUp: function (c) {
                B[a.hoverChartIndex] &&
                    B[a.hoverChartIndex].pointer.drop(c)
                }, onDocumentMouseMove: function (a) { var b = this.chart, c = this.chartPosition; a = this.normalize(a, c); !c || this.inClass(a.target, "highcharts-tracker") || b.isInsidePlot(a.chartX - b.plotLeft, a.chartY - b.plotTop) || this.reset() }, onContainerMouseLeave: function (c) { var b = B[a.hoverChartIndex]; b && (c.relatedTarget || c.toElement) && (b.pointer.reset(), b.pointer.chartPosition = null) }, onContainerMouseMove: function (c) {
                    var b = this.chart; t(a.hoverChartIndex) && B[a.hoverChartIndex] && B[a.hoverChartIndex].mouseIsDown ||
                        (a.hoverChartIndex = b.index); c = this.normalize(c); c.returnValue = !1; "mousedown" === b.mouseIsDown && this.drag(c); !this.inClass(c.target, "highcharts-tracker") && !b.isInsidePlot(c.chartX - b.plotLeft, c.chartY - b.plotTop) || b.openMenu || this.runPointActions(c)
                }, inClass: function (a, b) { for (var c; a;) { if (c = C(a, "class")) { if (-1 !== c.indexOf(b)) return !0; if (-1 !== c.indexOf("highcharts-container")) return !1 } a = a.parentNode } }, onTrackerMouseOut: function (a) {
                    var b = this.chart.hoverSeries; a = a.relatedTarget || a.toElement; this.isDirectTouch =
                        !1; if (!(!b || !a || b.stickyTracking || this.inClass(a, "highcharts-tooltip") || this.inClass(a, "highcharts-series-" + b.index) && this.inClass(a, "highcharts-tracker"))) b.onMouseOut()
                }, onContainerClick: function (a) { var b = this.chart, c = b.hoverPoint, e = b.plotLeft, g = b.plotTop; a = this.normalize(a); b.cancelClick || (c && this.inClass(a.target, "highcharts-tracker") ? (v(c.series, "click", w(a, { point: c })), b.hoverPoint && c.firePointEvent("click", a)) : (w(a, this.getCoordinates(a)), b.isInsidePlot(a.chartX - e, a.chartY - g) && v(b, "click", a))) },
                setDOMEvents: function () {
                    var c = this, b = c.chart.container, e = b.ownerDocument; b.onmousedown = function (a) { c.onContainerMouseDown(a) }; b.onmousemove = function (a) { c.onContainerMouseMove(a) }; b.onclick = function (a) { c.onContainerClick(a) }; this.unbindContainerMouseLeave = z(b, "mouseleave", c.onContainerMouseLeave); a.unbindDocumentMouseUp || (a.unbindDocumentMouseUp = z(e, "mouseup", c.onDocumentMouseUp)); a.hasTouch && (b.ontouchstart = function (a) { c.onContainerTouchStart(a) }, b.ontouchmove = function (a) { c.onContainerTouchMove(a) },
                        a.unbindDocumentTouchEnd || (a.unbindDocumentTouchEnd = z(e, "touchend", c.onDocumentTouchEnd)))
                }, destroy: function () { var c = this; c.unDocMouseMove && c.unDocMouseMove(); this.unbindContainerMouseLeave(); a.chartCount || (a.unbindDocumentMouseUp && (a.unbindDocumentMouseUp = a.unbindDocumentMouseUp()), a.unbindDocumentTouchEnd && (a.unbindDocumentTouchEnd = a.unbindDocumentTouchEnd())); clearInterval(c.tooltipTimeout); a.objectEach(c, function (a, e) { c[e] = null }) }
            }
    })(H); (function (a) {
        var z = a.charts, C = a.extend, B = a.noop, h = a.pick;
        C(a.Pointer.prototype, {
            pinchTranslate: function (a, h, w, r, v, p) { this.zoomHor && this.pinchTranslateDirection(!0, a, h, w, r, v, p); this.zoomVert && this.pinchTranslateDirection(!1, a, h, w, r, v, p) }, pinchTranslateDirection: function (a, h, w, r, v, p, k, n) {
                var e = this.chart, m = a ? "x" : "y", c = a ? "X" : "Y", g = "chart" + c, b = a ? "width" : "height", d = e["plot" + (a ? "Left" : "Top")], u, x, D = n || 1, E = e.inverted, l = e.bounds[a ? "h" : "v"], A = 1 === h.length, F = h[0][g], G = w[0][g], f = !A && h[1][g], y = !A && w[1][g], t; w = function () {
                !A && 20 < Math.abs(F - f) && (D = n || Math.abs(G - y) / Math.abs(F -
                    f)); x = (d - G) / D + F; u = e["plot" + (a ? "Width" : "Height")] / D
                }; w(); h = x; h < l.min ? (h = l.min, t = !0) : h + u > l.max && (h = l.max - u, t = !0); t ? (G -= .8 * (G - k[m][0]), A || (y -= .8 * (y - k[m][1])), w()) : k[m] = [G, y]; E || (p[m] = x - d, p[b] = u); p = E ? 1 / D : D; v[b] = u; v[m] = h; r[E ? a ? "scaleY" : "scaleX" : "scale" + c] = D; r["translate" + c] = p * d + (G - p * F)
            }, pinch: function (a) {
                var d = this, w = d.chart, r = d.pinchDown, v = a.touches, p = v.length, k = d.lastValidTouch, n = d.hasZoom, e = d.selectionMarker, m = {}, c = 1 === p && (d.inClass(a.target, "highcharts-tracker") && w.runTrackerClick || d.runChartClick),
                g = {}; 1 < p && (d.initiated = !0); n && d.initiated && !c && a.preventDefault();[].map.call(v, function (a) { return d.normalize(a) }); "touchstart" === a.type ? ([].forEach.call(v, function (a, c) { r[c] = { chartX: a.chartX, chartY: a.chartY } }), k.x = [r[0].chartX, r[1] && r[1].chartX], k.y = [r[0].chartY, r[1] && r[1].chartY], w.axes.forEach(function (a) {
                    if (a.zoomEnabled) {
                        var b = w.bounds[a.horiz ? "h" : "v"], c = a.minPixelPadding, e = a.toPixels(h(a.options.min, a.dataMin)), g = a.toPixels(h(a.options.max, a.dataMax)), d = Math.max(e, g); b.min = Math.min(a.pos, Math.min(e,
                            g) - c); b.max = Math.max(a.pos + a.len, d + c)
                    }
                }), d.res = !0) : d.followTouchMove && 1 === p ? this.runPointActions(d.normalize(a)) : r.length && (e || (d.selectionMarker = e = C({ destroy: B, touch: !0 }, w.plotBox)), d.pinchTranslate(r, v, m, e, g, k), d.hasPinched = n, d.scaleGroups(m, g), d.res && (d.res = !1, this.reset(!1, 0)))
            }, touch: function (d, t) {
                var w = this.chart, r, v; if (w.index !== a.hoverChartIndex) this.onContainerMouseLeave({ relatedTarget: !0 }); a.hoverChartIndex = w.index; 1 === d.touches.length ? (d = this.normalize(d), (v = w.isInsidePlot(d.chartX - w.plotLeft,
                    d.chartY - w.plotTop)) && !w.openMenu ? (t && this.runPointActions(d), "touchmove" === d.type && (t = this.pinchDown, r = t[0] ? 4 <= Math.sqrt(Math.pow(t[0].chartX - d.chartX, 2) + Math.pow(t[0].chartY - d.chartY, 2)) : !1), h(r, !0) && this.pinch(d)) : t && this.reset()) : 2 === d.touches.length && this.pinch(d)
            }, onContainerTouchStart: function (a) { this.zoomOption(a); this.touch(a, !0) }, onContainerTouchMove: function (a) { this.touch(a) }, onDocumentTouchEnd: function (d) { z[a.hoverChartIndex] && z[a.hoverChartIndex].pointer.drop(d) }
        })
    })(H); (function (a) {
        var z =
            a.addEvent, C = a.charts, B = a.css, h = a.doc, d = a.extend, t = a.noop, w = a.Pointer, r = a.removeEvent, v = a.win, p = a.wrap; if (!a.hasTouch && (v.PointerEvent || v.MSPointerEvent)) {
                var k = {}, n = !!v.PointerEvent, e = function () { var c = []; c.item = function (a) { return this[a] }; a.objectEach(k, function (a) { c.push({ pageX: a.pageX, pageY: a.pageY, target: a.target }) }); return c }, m = function (c, g, b, d) {
                "touch" !== c.pointerType && c.pointerType !== c.MSPOINTER_TYPE_TOUCH || !C[a.hoverChartIndex] || (d(c), d = C[a.hoverChartIndex].pointer, d[g]({
                    type: b, target: c.currentTarget,
                    preventDefault: t, touches: e()
                }))
                }; d(w.prototype, {
                    onContainerPointerDown: function (a) { m(a, "onContainerTouchStart", "touchstart", function (a) { k[a.pointerId] = { pageX: a.pageX, pageY: a.pageY, target: a.currentTarget } }) }, onContainerPointerMove: function (a) { m(a, "onContainerTouchMove", "touchmove", function (a) { k[a.pointerId] = { pageX: a.pageX, pageY: a.pageY }; k[a.pointerId].target || (k[a.pointerId].target = a.currentTarget) }) }, onDocumentPointerUp: function (a) { m(a, "onDocumentTouchEnd", "touchend", function (a) { delete k[a.pointerId] }) },
                    batchMSEvents: function (a) { a(this.chart.container, n ? "pointerdown" : "MSPointerDown", this.onContainerPointerDown); a(this.chart.container, n ? "pointermove" : "MSPointerMove", this.onContainerPointerMove); a(h, n ? "pointerup" : "MSPointerUp", this.onDocumentPointerUp) }
                }); p(w.prototype, "init", function (a, e, b) { a.call(this, e, b); this.hasZoom && B(e.container, { "-ms-touch-action": "none", "touch-action": "none" }) }); p(w.prototype, "setDOMEvents", function (a) { a.apply(this); (this.hasZoom || this.followTouchMove) && this.batchMSEvents(z) });
                p(w.prototype, "destroy", function (a) { this.batchMSEvents(r); a.call(this) })
            }
    })(H); (function (a) {
        var z = a.addEvent, C = a.css, B = a.discardElement, h = a.defined, d = a.fireEvent, t = a.isFirefox, w = a.marginNames, r = a.merge, v = a.pick, p = a.setAnimation, k = a.stableSort, n = a.win, e = a.wrap; a.Legend = function (a, c) { this.init(a, c) }; a.Legend.prototype = {
            init: function (a, c) {
            this.chart = a; this.setOptions(c); c.enabled && (this.render(), z(this.chart, "endResize", function () { this.legend.positionCheckboxes() }), this.proximate ? this.unchartrender =
                z(this.chart, "render", function () { this.legend.proximatePositions(); this.legend.positionItems() }) : this.unchartrender && this.unchartrender())
            }, setOptions: function (a) { var c = v(a.padding, 8); this.options = a; this.chart.styledMode || (this.itemStyle = a.itemStyle, this.itemHiddenStyle = r(this.itemStyle, a.itemHiddenStyle)); this.itemMarginTop = a.itemMarginTop || 0; this.padding = c; this.initialItemY = c - 5; this.symbolWidth = v(a.symbolWidth, 16); this.pages = []; this.proximate = "proximate" === a.layout && !this.chart.inverted }, update: function (a,
                c) { var e = this.chart; this.setOptions(r(!0, this.options, a)); this.destroy(); e.isDirtyLegend = e.isDirtyBox = !0; v(c, !0) && e.redraw(); d(this, "afterUpdate") }, colorizeItem: function (a, c) {
                a.legendGroup[c ? "removeClass" : "addClass"]("highcharts-legend-item-hidden"); if (!this.chart.styledMode) {
                    var e = this.options, b = a.legendItem, m = a.legendLine, k = a.legendSymbol, n = this.itemHiddenStyle.color, e = c ? e.itemStyle.color : n, h = c ? a.color || n : n, E = a.options && a.options.marker, l = { fill: h }; b && b.css({ fill: e, color: e }); m && m.attr({ stroke: h });
                    k && (E && k.isMarker && (l = a.pointAttribs(), c || (l.stroke = l.fill = n)), k.attr(l))
                } d(this, "afterColorizeItem", { item: a, visible: c })
                }, positionItems: function () { this.allItems.forEach(this.positionItem, this); this.chart.isResizing || this.positionCheckboxes() }, positionItem: function (a) { var c = this.options, e = c.symbolPadding, c = !c.rtl, b = a._legendItemPos, d = b[0], b = b[1], m = a.checkbox; if ((a = a.legendGroup) && a.element) a[h(a.translateY) ? "animate" : "attr"]({ translateX: c ? d : this.legendWidth - d - 2 * e - 4, translateY: b }); m && (m.x = d, m.y = b) },
            destroyItem: function (a) { var c = a.checkbox;["legendItem", "legendLine", "legendSymbol", "legendGroup"].forEach(function (c) { a[c] && (a[c] = a[c].destroy()) }); c && B(a.checkbox) }, destroy: function () { function a(a) { this[a] && (this[a] = this[a].destroy()) } this.getAllItems().forEach(function (c) { ["legendItem", "legendGroup"].forEach(a, c) }); "clipRect up down pager nav box title group".split(" ").forEach(a, this); this.display = null }, positionCheckboxes: function () {
                var a = this.group && this.group.alignAttr, c, e = this.clipHeight || this.legendHeight,
                b = this.titleHeight; a && (c = a.translateY, this.allItems.forEach(function (d) { var g = d.checkbox, m; g && (m = c + b + g.y + (this.scrollOffset || 0) + 3, C(g, { left: a.translateX + d.checkboxOffset + g.x - 20 + "px", top: m + "px", display: this.proximate || m > c - 6 && m < c + e - 6 ? "" : "none" })) }, this))
            }, renderTitle: function () {
                var a = this.options, c = this.padding, e = a.title, b = 0; e.text && (this.title || (this.title = this.chart.renderer.label(e.text, c - 3, c - 4, null, null, null, a.useHTML, null, "legend-title").attr({ zIndex: 1 }), this.chart.styledMode || this.title.css(e.style),
                    this.title.add(this.group)), a = this.title.getBBox(), b = a.height, this.offsetWidth = a.width, this.contentGroup.attr({ translateY: b })); this.titleHeight = b
            }, setText: function (e) { var c = this.options; e.legendItem.attr({ text: c.labelFormat ? a.format(c.labelFormat, e, this.chart.time) : c.labelFormatter.call(e) }) }, renderItem: function (a) {
                var c = this.chart, e = c.renderer, b = this.options, d = this.symbolWidth, m = b.symbolPadding, k = this.itemStyle, n = this.itemHiddenStyle, h = "horizontal" === b.layout ? v(b.itemDistance, 20) : 0, l = !b.rtl, A = a.legendItem,
                F = !a.series, p = !F && a.series.drawLegendSymbol ? a.series : a, f = p.options, f = this.createCheckboxForItem && f && f.showCheckbox, h = d + m + h + (f ? 20 : 0), y = b.useHTML, t = a.options.className; A || (a.legendGroup = e.g("legend-item").addClass("highcharts-" + p.type + "-series highcharts-color-" + a.colorIndex + (t ? " " + t : "") + (F ? " highcharts-series-" + a.index : "")).attr({ zIndex: 1 }).add(this.scrollGroup), a.legendItem = A = e.text("", l ? d + m : -m, this.baseline || 0, y), c.styledMode || A.css(r(a.visible ? k : n)), A.attr({ align: l ? "left" : "right", zIndex: 2 }).add(a.legendGroup),
                    this.baseline || (this.fontMetrics = e.fontMetrics(c.styledMode ? 12 : k.fontSize, A), this.baseline = this.fontMetrics.f + 3 + this.itemMarginTop, A.attr("y", this.baseline)), this.symbolHeight = b.symbolHeight || this.fontMetrics.f, p.drawLegendSymbol(this, a), this.setItemEvents && this.setItemEvents(a, A, y), f && this.createCheckboxForItem(a)); this.colorizeItem(a, a.visible); !c.styledMode && k.width || A.css({ width: (b.itemWidth || b.width || c.spacingBox.width) - h }); this.setText(a); c = A.getBBox(); a.itemWidth = a.checkboxOffset = b.itemWidth ||
                        a.legendItemWidth || c.width + h; this.maxItemWidth = Math.max(this.maxItemWidth, a.itemWidth); this.totalItemWidth += a.itemWidth; this.itemHeight = a.itemHeight = Math.round(a.legendItemHeight || c.height || this.symbolHeight)
            }, layoutItem: function (a) {
                var c = this.options, e = this.padding, b = "horizontal" === c.layout, d = a.itemHeight, m = c.itemMarginBottom || 0, k = this.itemMarginTop, n = b ? v(c.itemDistance, 20) : 0, h = c.width, l = h || this.chart.spacingBox.width - 2 * e - c.x, c = c.alignColumns && this.totalItemWidth > l ? this.maxItemWidth : a.itemWidth;
                b && this.itemX - e + c > l && (this.itemX = e, this.itemY += k + this.lastLineHeight + m, this.lastLineHeight = 0); this.lastItemY = k + this.itemY + m; this.lastLineHeight = Math.max(d, this.lastLineHeight); a._legendItemPos = [this.itemX, this.itemY]; b ? this.itemX += c : (this.itemY += k + d + m, this.lastLineHeight = d); this.offsetWidth = h || Math.max((b ? this.itemX - e - (a.checkbox ? 0 : n) : c) + e, this.offsetWidth)
            }, getAllItems: function () {
                var a = []; this.chart.series.forEach(function (c) {
                    var e = c && c.options; c && v(e.showInLegend, h(e.linkedTo) ? !1 : void 0, !0) && (a =
                        a.concat(c.legendItems || ("point" === e.legendType ? c.data : c)))
                }); d(this, "afterGetAllItems", { allItems: a }); return a
            }, getAlignment: function () { var a = this.options; return this.proximate ? a.align.charAt(0) + "tv" : a.floating ? "" : a.align.charAt(0) + a.verticalAlign.charAt(0) + a.layout.charAt(0) }, adjustMargins: function (a, c) {
                var e = this.chart, b = this.options, d = this.getAlignment(); d && [/(lth|ct|rth)/, /(rtv|rm|rbv)/, /(rbh|cb|lbh)/, /(lbv|lm|ltv)/].forEach(function (g, m) {
                    g.test(d) && !h(a[m]) && (e[w[m]] = Math.max(e[w[m]], e.legend[(m +
                        1) % 2 ? "legendHeight" : "legendWidth"] + [1, -1, -1, 1][m] * b[m % 2 ? "x" : "y"] + v(b.margin, 12) + c[m] + (0 === m && void 0 !== e.options.title.margin ? e.titleOffset + e.options.title.margin : 0)))
                })
            }, proximatePositions: function () {
                var e = this.chart, c = [], d = "left" === this.options.align; this.allItems.forEach(function (b) {
                    var g, m; g = d; b.xAxis && b.points && (b.xAxis.options.reversed && (g = !g), g = a.find(g ? b.points : b.points.slice(0).reverse(), function (b) { return a.isNumber(b.plotY) }), m = b.legendGroup.getBBox().height, c.push({
                        target: b.visible ? (g ?
                            g.plotY : b.xAxis.height) - .3 * m : e.plotHeight, size: m, item: b
                    }))
                }, this); a.distribute(c, e.plotHeight); c.forEach(function (a) { a.item._legendItemPos[1] = e.plotTop - e.spacing[0] + a.pos })
            }, render: function () {
                var a = this.chart, c = a.renderer, e = this.group, b, d, n, h = this.box, p = this.options, E = this.padding; this.itemX = E; this.itemY = this.initialItemY; this.lastItemY = this.offsetWidth = 0; e || (this.group = e = c.g("legend").attr({ zIndex: 7 }).add(), this.contentGroup = c.g().attr({ zIndex: 1 }).add(e), this.scrollGroup = c.g().add(this.contentGroup));
                this.renderTitle(); b = this.getAllItems(); k(b, function (a, b) { return (a.options && a.options.legendIndex || 0) - (b.options && b.options.legendIndex || 0) }); p.reversed && b.reverse(); this.allItems = b; this.display = d = !!b.length; this.itemHeight = this.totalItemWidth = this.maxItemWidth = this.lastLineHeight = 0; b.forEach(this.renderItem, this); b.forEach(this.layoutItem, this); b = (p.width || this.offsetWidth) + E; n = this.lastItemY + this.lastLineHeight + this.titleHeight; n = this.handleOverflow(n); n += E; h || (this.box = h = c.rect().addClass("highcharts-legend-box").attr({ r: p.borderRadius }).add(e),
                    h.isNew = !0); a.styledMode || h.attr({ stroke: p.borderColor, "stroke-width": p.borderWidth || 0, fill: p.backgroundColor || "none" }).shadow(p.shadow); 0 < b && 0 < n && (h[h.isNew ? "attr" : "animate"](h.crisp.call({}, { x: 0, y: 0, width: b, height: n }, h.strokeWidth())), h.isNew = !1); h[d ? "show" : "hide"](); a.styledMode && "none" === e.getStyle("display") && (b = n = 0); this.legendWidth = b; this.legendHeight = n; d && (c = a.spacingBox, /(lth|ct|rth)/.test(this.getAlignment()) && (c = r(c, { y: c.y + a.titleOffset + a.options.title.margin })), e.align(r(p, {
                        width: b, height: n,
                        verticalAlign: this.proximate ? "top" : p.verticalAlign
                    }), !0, c)); this.proximate || this.positionItems()
            }, handleOverflow: function (a) {
                var c = this, e = this.chart, b = e.renderer, d = this.options, m = d.y, k = this.padding, m = e.spacingBox.height + ("top" === d.verticalAlign ? -m : m) - k, n = d.maxHeight, h, l = this.clipRect, A = d.navigation, F = v(A.animation, !0), p = A.arrowSize || 12, f = this.nav, y = this.pages, r, t = this.allItems, w = function (a) {
                    "number" === typeof a ? l.attr({ height: a }) : l && (c.clipRect = l.destroy(), c.contentGroup.clip()); c.contentGroup.div &&
                        (c.contentGroup.div.style.clip = a ? "rect(" + k + "px,9999px," + (k + a) + "px,0)" : "auto")
                }; "horizontal" !== d.layout || "middle" === d.verticalAlign || d.floating || (m /= 2); n && (m = Math.min(m, n)); y.length = 0; a > m && !1 !== A.enabled ? (this.clipHeight = h = Math.max(m - 20 - this.titleHeight - k, 0), this.currentPage = v(this.currentPage, 1), this.fullHeight = a, t.forEach(function (a, b) {
                    var c = a._legendItemPos[1], f = Math.round(a.legendItem.getBBox().height), l = y.length; if (!l || c - y[l - 1] > h && (r || c) !== y[l - 1]) y.push(r || c), l++; a.pageIx = l - 1; r && (t[b - 1].pageIx =
                        l - 1); b === t.length - 1 && c + f - y[l - 1] > h && c !== r && (y.push(c), a.pageIx = l); c !== r && (r = c)
                }), l || (l = c.clipRect = b.clipRect(0, k, 9999, 0), c.contentGroup.clip(l)), w(h), f || (this.nav = f = b.g().attr({ zIndex: 1 }).add(this.group), this.up = b.symbol("triangle", 0, 0, p, p).on("click", function () { c.scroll(-1, F) }).add(f), this.pager = b.text("", 15, 10).addClass("highcharts-legend-navigation"), e.styledMode || this.pager.css(A.style), this.pager.add(f), this.down = b.symbol("triangle-down", 0, 0, p, p).on("click", function () { c.scroll(1, F) }).add(f)),
                    c.scroll(0), a = m) : f && (w(), this.nav = f.destroy(), this.scrollGroup.attr({ translateY: 1 }), this.clipHeight = 0); return a
            }, scroll: function (a, c) {
                var e = this.pages, b = e.length; a = this.currentPage + a; var d = this.clipHeight, m = this.options.navigation, k = this.pager, n = this.padding; a > b && (a = b); 0 < a && (void 0 !== c && p(c, this.chart), this.nav.attr({ translateX: n, translateY: d + this.padding + 7 + this.titleHeight, visibility: "visible" }), this.up.attr({ "class": 1 === a ? "highcharts-legend-nav-inactive" : "highcharts-legend-nav-active" }), k.attr({
                    text: a +
                        "/" + b
                }), this.down.attr({ x: 18 + this.pager.getBBox().width, "class": a === b ? "highcharts-legend-nav-inactive" : "highcharts-legend-nav-active" }), this.chart.styledMode || (this.up.attr({ fill: 1 === a ? m.inactiveColor : m.activeColor }).css({ cursor: 1 === a ? "default" : "pointer" }), this.down.attr({ fill: a === b ? m.inactiveColor : m.activeColor }).css({ cursor: a === b ? "default" : "pointer" })), this.scrollOffset = -e[a - 1] + this.initialItemY, this.scrollGroup.animate({ translateY: this.scrollOffset }), this.currentPage = a, this.positionCheckboxes())
            }
        };
        a.LegendSymbolMixin = {
            drawRectangle: function (a, c) { var e = a.symbolHeight, b = a.options.squareSymbol; c.legendSymbol = this.chart.renderer.rect(b ? (a.symbolWidth - e) / 2 : 0, a.baseline - e + 1, b ? e : a.symbolWidth, e, v(a.options.symbolRadius, e / 2)).addClass("highcharts-point").attr({ zIndex: 3 }).add(c.legendGroup) }, drawLineMarker: function (a) {
                var c = this.options, e = c.marker, b = a.symbolWidth, d = a.symbolHeight, m = d / 2, k = this.chart.renderer, n = this.legendGroup; a = a.baseline - Math.round(.3 * a.fontMetrics.b); var h = {}; this.chart.styledMode ||
                    (h = { "stroke-width": c.lineWidth || 0 }, c.dashStyle && (h.dashstyle = c.dashStyle)); this.legendLine = k.path(["M", 0, a, "L", b, a]).addClass("highcharts-graph").attr(h).add(n); e && !1 !== e.enabled && b && (c = Math.min(v(e.radius, m), m), 0 === this.symbol.indexOf("url") && (e = r(e, { width: d, height: d }), c = 0), this.legendSymbol = e = k.symbol(this.symbol, b / 2 - c, a - c, 2 * c, 2 * c, e).addClass("highcharts-point").add(n), e.isMarker = !0)
            }
        }; (/Trident\/7\.0/.test(n.navigator.userAgent) || t) && e(a.Legend.prototype, "positionItem", function (a, c) {
            var e = this,
            b = function () { c._legendItemPos && a.call(e, c) }; b(); e.bubbleLegend || setTimeout(b)
        })
    })(H); (function (a) {
        var z = a.addEvent, C = a.animate, B = a.animObject, h = a.attr, d = a.doc, t = a.Axis, w = a.createElement, r = a.defaultOptions, v = a.discardElement, p = a.charts, k = a.css, n = a.defined, e = a.extend, m = a.find, c = a.fireEvent, g = a.isNumber, b = a.isObject, q = a.isString, u = a.Legend, x = a.marginNames, D = a.merge, E = a.objectEach, l = a.Pointer, A = a.pick, F = a.pInt, G = a.removeEvent, f = a.seriesTypes, y = a.splat, L = a.syncTimeout, J = a.win, T = a.Chart = function () {
            this.getArgs.apply(this,
                arguments)
        }; a.chart = function (a, b, c) { return new T(a, b, c) }; e(T.prototype, {
            callbacks: [], getArgs: function () { var a = [].slice.call(arguments); if (q(a[0]) || a[0].nodeName) this.renderTo = a.shift(); this.init(a[0], a[1]) }, init: function (b, f) {
                var l, e, d = b.series, g = b.plotOptions || {}; c(this, "init", { args: arguments }, function () {
                b.series = null; l = D(r, b); for (e in l.plotOptions) l.plotOptions[e].tooltip = g[e] && D(g[e].tooltip) || void 0; l.tooltip.userOptions = b.chart && b.chart.forExport && b.tooltip.userOptions || b.tooltip; l.series = b.series =
                    d; this.userOptions = b; var m = l.chart, k = m.events; this.margin = []; this.spacing = []; this.bounds = { h: {}, v: {} }; this.labelCollectors = []; this.callback = f; this.isResizing = 0; this.options = l; this.axes = []; this.series = []; this.time = b.time && Object.keys(b.time).length ? new a.Time(b.time) : a.time; this.styledMode = m.styledMode; this.hasCartesianSeries = m.showAxes; var n = this; n.index = p.length; p.push(n); a.chartCount++; k && E(k, function (a, b) { z(n, b, a) }); n.xAxis = []; n.yAxis = []; n.pointCount = n.colorCounter = n.symbolCounter = 0; c(n, "afterInit");
                    n.firstRender()
                })
            }, initSeries: function (b) { var c = this.options.chart; (c = f[b.type || c.type || c.defaultSeriesType]) || a.error(17, !0, this); c = new c; c.init(this, b); return c }, orderSeries: function (a) { var b = this.series; for (a = a || 0; a < b.length; a++)b[a] && (b[a].index = a, b[a].name = b[a].getName()) }, isInsidePlot: function (a, b, c) { var f = c ? b : a; a = c ? a : b; return 0 <= f && f <= this.plotWidth && 0 <= a && a <= this.plotHeight }, redraw: function (b) {
                c(this, "beforeRedraw"); var f = this.axes, l = this.series, d = this.pointer, g = this.legend, m = this.userOptions.legend,
                    k = this.isDirtyLegend, n, h, q = this.hasCartesianSeries, A = this.isDirtyBox, u, y = this.renderer, F = y.isHidden(), x = []; this.setResponsive && this.setResponsive(!1); a.setAnimation(b, this); F && this.temporaryDisplay(); this.layOutTitles(); for (b = l.length; b--;)if (u = l[b], u.options.stacking && (n = !0, u.isDirty)) { h = !0; break } if (h) for (b = l.length; b--;)u = l[b], u.options.stacking && (u.isDirty = !0); l.forEach(function (a) {
                    a.isDirty && ("point" === a.options.legendType ? (a.updateTotals && a.updateTotals(), k = !0) : m && (m.labelFormatter || m.labelFormat) &&
                        (k = !0)); a.isDirtyData && c(a, "updatedData")
                    }); k && g && g.options.enabled && (g.render(), this.isDirtyLegend = !1); n && this.getStacks(); q && f.forEach(function (a) { a.updateNames(); a.updateYNames && a.updateYNames(); a.setScale() }); this.getMargins(); q && (f.forEach(function (a) { a.isDirty && (A = !0) }), f.forEach(function (a) { var b = a.min + "," + a.max; a.extKey !== b && (a.extKey = b, x.push(function () { c(a, "afterSetExtremes", e(a.eventArgs, a.getExtremes())); delete a.eventArgs })); (A || n) && a.redraw() })); A && this.drawChartBox(); c(this, "predraw");
                l.forEach(function (a) { (A || a.isDirty) && a.visible && a.redraw(); a.isDirtyData = !1 }); d && d.reset(!0); y.draw(); c(this, "redraw"); c(this, "render"); F && this.temporaryDisplay(!0); x.forEach(function (a) { a.call() })
            }, get: function (a) { function b(b) { return b.id === a || b.options && b.options.id === a } var c, f = this.series, l; c = m(this.axes, b) || m(this.series, b); for (l = 0; !c && l < f.length; l++)c = m(f[l].points || [], b); return c }, getAxes: function () {
                var a = this, b = this.options, f = b.xAxis = y(b.xAxis || {}), b = b.yAxis = y(b.yAxis || {}); c(this, "getAxes");
                f.forEach(function (a, b) { a.index = b; a.isX = !0 }); b.forEach(function (a, b) { a.index = b }); f.concat(b).forEach(function (b) { new t(a, b) }); c(this, "afterGetAxes")
            }, getSelectedPoints: function () { var a = []; this.series.forEach(function (b) { a = a.concat((b.data || []).filter(function (a) { return a.selected })) }); return a }, getSelectedSeries: function () { return this.series.filter(function (a) { return a.selected }) }, setTitle: function (a, b, c) {
                var f = this, l = f.options, e = f.styledMode, d; d = l.title = D(!e && {
                    style: {
                        color: "#333333", fontSize: l.isStock ?
                            "16px" : "18px"
                    }
                }, l.title, a); l = l.subtitle = D(!e && { style: { color: "#666666" } }, l.subtitle, b);[["title", a, d], ["subtitle", b, l]].forEach(function (a, b) { var c = a[0], l = f[c], d = a[1]; a = a[2]; l && d && (f[c] = l = l.destroy()); a && !l && (f[c] = f.renderer.text(a.text, 0, 0, a.useHTML).attr({ align: a.align, "class": "highcharts-" + c, zIndex: a.zIndex || 4 }).add(), f[c].update = function (a) { f.setTitle(!b && a, b && a) }, e || f[c].css(a.style)) }); f.layOutTitles(c)
            }, layOutTitles: function (a) {
                var b = 0, c, f = this.renderer, l = this.spacingBox;["title", "subtitle"].forEach(function (a) {
                    var c =
                        this[a], d = this.options[a]; a = "title" === a ? -3 : d.verticalAlign ? 0 : b + 2; var g; c && (this.styledMode || (g = d.style.fontSize), g = f.fontMetrics(g, c).b, c.css({ width: (d.width || l.width + d.widthAdjust) + "px" }).align(e({ y: a + g }, d), !1, "spacingBox"), d.floating || d.verticalAlign || (b = Math.ceil(b + c.getBBox(d.useHTML).height)))
                }, this); c = this.titleOffset !== b; this.titleOffset = b; !this.isDirtyBox && c && (this.isDirtyBox = this.isDirtyLegend = c, this.hasRendered && A(a, !0) && this.isDirtyBox && this.redraw())
            }, getChartSize: function () {
                var b = this.options.chart,
                c = b.width, b = b.height, f = this.renderTo; n(c) || (this.containerWidth = a.getStyle(f, "width")); n(b) || (this.containerHeight = a.getStyle(f, "height")); this.chartWidth = Math.max(0, c || this.containerWidth || 600); this.chartHeight = Math.max(0, a.relativeLength(b, this.chartWidth) || (1 < this.containerHeight ? this.containerHeight : 400))
            }, temporaryDisplay: function (b) {
                var c = this.renderTo; if (b) for (; c && c.style;)c.hcOrigStyle && (a.css(c, c.hcOrigStyle), delete c.hcOrigStyle), c.hcOrigDetached && (d.body.removeChild(c), c.hcOrigDetached =
                    !1), c = c.parentNode; else for (; c && c.style;) { d.body.contains(c) || c.parentNode || (c.hcOrigDetached = !0, d.body.appendChild(c)); if ("none" === a.getStyle(c, "display", !1) || c.hcOricDetached) c.hcOrigStyle = { display: c.style.display, height: c.style.height, overflow: c.style.overflow }, b = { display: "block", overflow: "hidden" }, c !== this.renderTo && (b.height = 0), a.css(c, b), c.offsetWidth || c.style.setProperty("display", "block", "important"); c = c.parentNode; if (c === d.body) break }
            }, setClassName: function (a) {
                this.container.className = "highcharts-container " +
                    (a || "")
            }, getContainer: function () {
                var b, f = this.options, l = f.chart, m, n; b = this.renderTo; var u = a.uniqueKey(), A, y; b || (this.renderTo = b = l.renderTo); q(b) && (this.renderTo = b = d.getElementById(b)); b || a.error(13, !0, this); m = F(h(b, "data-highcharts-chart")); g(m) && p[m] && p[m].hasRendered && p[m].destroy(); h(b, "data-highcharts-chart", this.index); b.innerHTML = ""; l.skipClone || b.offsetWidth || this.temporaryDisplay(); this.getChartSize(); m = this.chartWidth; n = this.chartHeight; k(b, { overflow: "hidden" }); this.styledMode || (A = e({
                    position: "relative",
                    overflow: "hidden", width: m + "px", height: n + "px", textAlign: "left", lineHeight: "normal", zIndex: 0, "-webkit-tap-highlight-color": "rgba(0,0,0,0)"
                }, l.style)); this.container = b = w("div", { id: u }, A, b); this._cursor = b.style.cursor; this.renderer = new (a[l.renderer] || a.Renderer)(b, m, n, null, l.forExport, f.exporting && f.exporting.allowHTML, this.styledMode); this.setClassName(l.className); if (this.styledMode) for (y in f.defs) this.renderer.definition(f.defs[y]); else this.renderer.setStyle(l.style); this.renderer.chartIndex = this.index;
                c(this, "afterGetContainer")
            }, getMargins: function (a) { var b = this.spacing, f = this.margin, l = this.titleOffset; this.resetMargins(); l && !n(f[0]) && (this.plotTop = Math.max(this.plotTop, l + this.options.title.margin + b[0])); this.legend && this.legend.display && this.legend.adjustMargins(f, b); c(this, "getMargins"); a || this.getAxisMargins() }, getAxisMargins: function () {
                var a = this, b = a.axisOffset = [0, 0, 0, 0], c = a.margin; a.hasCartesianSeries && a.axes.forEach(function (a) { a.visible && a.getOffset() }); x.forEach(function (f, l) {
                n(c[l]) ||
                    (a[f] += b[l])
                }); a.setChartSize()
            }, reflow: function (b) { var c = this, f = c.options.chart, l = c.renderTo, e = n(f.width) && n(f.height), g = f.width || a.getStyle(l, "width"), f = f.height || a.getStyle(l, "height"), l = b ? b.target : J; if (!e && !c.isPrinting && g && f && (l === J || l === d)) { if (g !== c.containerWidth || f !== c.containerHeight) a.clearTimeout(c.reflowTimeout), c.reflowTimeout = L(function () { c.container && c.setSize(void 0, void 0, !1) }, b ? 100 : 0); c.containerWidth = g; c.containerHeight = f } }, setReflow: function (a) {
                var b = this; !1 === a || this.unbindReflow ?
                    !1 === a && this.unbindReflow && (this.unbindReflow = this.unbindReflow()) : (this.unbindReflow = z(J, "resize", function (a) { b.reflow(a) }), z(this, "destroy", this.unbindReflow))
            }, setSize: function (b, f, l) {
                var e = this, d = e.renderer, g; e.isResizing += 1; a.setAnimation(l, e); e.oldChartHeight = e.chartHeight; e.oldChartWidth = e.chartWidth; void 0 !== b && (e.options.chart.width = b); void 0 !== f && (e.options.chart.height = f); e.getChartSize(); e.styledMode || (g = d.globalAnimation, (g ? C : k)(e.container, {
                    width: e.chartWidth + "px", height: e.chartHeight +
                        "px"
                }, g)); e.setChartSize(!0); d.setSize(e.chartWidth, e.chartHeight, l); e.axes.forEach(function (a) { a.isDirty = !0; a.setScale() }); e.isDirtyLegend = !0; e.isDirtyBox = !0; e.layOutTitles(); e.getMargins(); e.redraw(l); e.oldChartHeight = null; c(e, "resize"); L(function () { e && c(e, "endResize", null, function () { --e.isResizing }) }, B(g).duration)
            }, setChartSize: function (a) {
                var b = this.inverted, f = this.renderer, l = this.chartWidth, e = this.chartHeight, d = this.options.chart, g = this.spacing, m = this.clipOffset, k, n, h, q; this.plotLeft = k = Math.round(this.plotLeft);
                this.plotTop = n = Math.round(this.plotTop); this.plotWidth = h = Math.max(0, Math.round(l - k - this.marginRight)); this.plotHeight = q = Math.max(0, Math.round(e - n - this.marginBottom)); this.plotSizeX = b ? q : h; this.plotSizeY = b ? h : q; this.plotBorderWidth = d.plotBorderWidth || 0; this.spacingBox = f.spacingBox = { x: g[3], y: g[0], width: l - g[3] - g[1], height: e - g[0] - g[2] }; this.plotBox = f.plotBox = { x: k, y: n, width: h, height: q }; l = 2 * Math.floor(this.plotBorderWidth / 2); b = Math.ceil(Math.max(l, m[3]) / 2); f = Math.ceil(Math.max(l, m[0]) / 2); this.clipBox = {
                    x: b,
                    y: f, width: Math.floor(this.plotSizeX - Math.max(l, m[1]) / 2 - b), height: Math.max(0, Math.floor(this.plotSizeY - Math.max(l, m[2]) / 2 - f))
                }; a || this.axes.forEach(function (a) { a.setAxisSize(); a.setAxisTranslation() }); c(this, "afterSetChartSize", { skipAxes: a })
            }, resetMargins: function () {
                c(this, "resetMargins"); var a = this, f = a.options.chart;["margin", "spacing"].forEach(function (c) { var l = f[c], e = b(l) ? l : [l, l, l, l];["Top", "Right", "Bottom", "Left"].forEach(function (b, l) { a[c][l] = A(f[c + b], e[l]) }) }); x.forEach(function (b, c) {
                a[b] = A(a.margin[c],
                    a.spacing[c])
                }); a.axisOffset = [0, 0, 0, 0]; a.clipOffset = [0, 0, 0, 0]
            }, drawChartBox: function () {
                var a = this.options.chart, b = this.renderer, f = this.chartWidth, l = this.chartHeight, e = this.chartBackground, d = this.plotBackground, g = this.plotBorder, m, k = this.styledMode, n = this.plotBGImage, h = a.backgroundColor, q = a.plotBackgroundColor, u = a.plotBackgroundImage, A, y = this.plotLeft, F = this.plotTop, x = this.plotWidth, p = this.plotHeight, r = this.plotBox, v = this.clipRect, E = this.clipBox, G = "animate"; e || (this.chartBackground = e = b.rect().addClass("highcharts-background").add(),
                    G = "attr"); if (k) m = A = e.strokeWidth(); else { m = a.borderWidth || 0; A = m + (a.shadow ? 8 : 0); h = { fill: h || "none" }; if (m || e["stroke-width"]) h.stroke = a.borderColor, h["stroke-width"] = m; e.attr(h).shadow(a.shadow) } e[G]({ x: A / 2, y: A / 2, width: f - A - m % 2, height: l - A - m % 2, r: a.borderRadius }); G = "animate"; d || (G = "attr", this.plotBackground = d = b.rect().addClass("highcharts-plot-background").add()); d[G](r); k || (d.attr({ fill: q || "none" }).shadow(a.plotShadow), u && (n ? n.animate(r) : this.plotBGImage = b.image(u, y, F, x, p).add())); v ? v.animate({
                        width: E.width,
                        height: E.height
                    }) : this.clipRect = b.clipRect(E); G = "animate"; g || (G = "attr", this.plotBorder = g = b.rect().addClass("highcharts-plot-border").attr({ zIndex: 1 }).add()); k || g.attr({ stroke: a.plotBorderColor, "stroke-width": a.plotBorderWidth || 0, fill: "none" }); g[G](g.crisp({ x: y, y: F, width: x, height: p }, -g.strokeWidth())); this.isDirtyBox = !1; c(this, "afterDrawChartBox")
            }, propFromSeries: function () {
                var a = this, b = a.options.chart, c, l = a.options.series, e, d;["inverted", "angular", "polar"].forEach(function (g) {
                    c = f[b.type || b.defaultSeriesType];
                    d = b[g] || c && c.prototype[g]; for (e = l && l.length; !d && e--;)(c = f[l[e].type]) && c.prototype[g] && (d = !0); a[g] = d
                })
            }, linkSeries: function () { var a = this, b = a.series; b.forEach(function (a) { a.linkedSeries.length = 0 }); b.forEach(function (b) { var c = b.options.linkedTo; q(c) && (c = ":previous" === c ? a.series[b.index - 1] : a.get(c)) && c.linkedParent !== b && (c.linkedSeries.push(b), b.linkedParent = c, b.visible = A(b.options.visible, c.options.visible, b.visible)) }); c(this, "afterLinkSeries") }, renderSeries: function () {
                this.series.forEach(function (a) {
                    a.translate();
                    a.render()
                })
            }, renderLabels: function () { var a = this, b = a.options.labels; b.items && b.items.forEach(function (c) { var f = e(b.style, c.style), l = F(f.left) + a.plotLeft, d = F(f.top) + a.plotTop + 12; delete f.left; delete f.top; a.renderer.text(c.html, l, d).attr({ zIndex: 2 }).css(f).add() }) }, render: function () {
                var a = this.axes, b = this.renderer, c = this.options, f, l, e; this.setTitle(); this.legend = new u(this, c.legend); this.getStacks && this.getStacks(); this.getMargins(!0); this.setChartSize(); c = this.plotWidth; f = this.plotHeight = Math.max(this.plotHeight -
                    21, 0); a.forEach(function (a) { a.setScale() }); this.getAxisMargins(); l = 1.1 < c / this.plotWidth; e = 1.05 < f / this.plotHeight; if (l || e) a.forEach(function (a) { (a.horiz && l || !a.horiz && e) && a.setTickInterval(!0) }), this.getMargins(); this.drawChartBox(); this.hasCartesianSeries && a.forEach(function (a) { a.visible && a.render() }); this.seriesGroup || (this.seriesGroup = b.g("series-group").attr({ zIndex: 3 }).add()); this.renderSeries(); this.renderLabels(); this.addCredits(); this.setResponsive && this.setResponsive(); this.hasRendered = !0
            },
            addCredits: function (a) { var b = this; a = D(!0, this.options.credits, a); a.enabled && !this.credits && (this.credits = this.renderer.text(a.text + (this.mapCredits || ""), 0, 0).addClass("highcharts-credits").on("click", function () { a.href && (J.location.href = a.href) }).attr({ align: a.position.align, zIndex: 8 }), b.styledMode || this.credits.css(a.style), this.credits.add().align(a.position), this.credits.update = function (a) { b.credits = b.credits.destroy(); b.addCredits(a) }) }, destroy: function () {
                var b = this, f = b.axes, l = b.series, e = b.container,
                d, g = e && e.parentNode; c(b, "destroy"); b.renderer.forExport ? a.erase(p, b) : p[b.index] = void 0; a.chartCount--; b.renderTo.removeAttribute("data-highcharts-chart"); G(b); for (d = f.length; d--;)f[d] = f[d].destroy(); this.scroller && this.scroller.destroy && this.scroller.destroy(); for (d = l.length; d--;)l[d] = l[d].destroy(); "title subtitle chartBackground plotBackground plotBGImage plotBorder seriesGroup clipRect credits pointer rangeSelector legend resetZoomButton tooltip renderer".split(" ").forEach(function (a) {
                    var c = b[a];
                    c && c.destroy && (b[a] = c.destroy())
                }); e && (e.innerHTML = "", G(e), g && v(e)); E(b, function (a, c) { delete b[c] })
            }, firstRender: function () { var b = this, f = b.options; if (!b.isReadyToRender || b.isReadyToRender()) { b.getContainer(); b.resetMargins(); b.setChartSize(); b.propFromSeries(); b.getAxes(); (a.isArray(f.series) ? f.series : []).forEach(function (a) { b.initSeries(a) }); b.linkSeries(); c(b, "beforeRender"); l && (b.pointer = new l(b, f)); b.render(); if (!b.renderer.imgCount && b.onload) b.onload(); b.temporaryDisplay(!0) } }, onload: function () {
                [this.callback].concat(this.callbacks).forEach(function (a) {
                a &&
                    void 0 !== this.index && a.apply(this, [this])
                }, this); c(this, "load"); c(this, "render"); n(this.index) && this.setReflow(this.options.chart.reflow); this.onload = null
            }
        })
    })(H); (function (a) {
        var z, C = a.extend, B = a.erase, h = a.fireEvent, d = a.format, t = a.isArray, w = a.isNumber, r = a.pick, v = a.uniqueKey, p = a.defined, k = a.removeEvent; a.Point = z = function () { }; a.Point.prototype = {
            init: function (a, e, d) {
                var c; c = a.chart.options.chart.colorCount; var g = a.chart.styledMode; this.series = a; g || (this.color = a.color); this.applyOptions(e, d); this.id =
                    p(this.id) ? this.id : v(); a.options.colorByPoint ? (g || (c = a.options.colors || a.chart.options.colors, this.color = this.color || c[a.colorCounter], c = c.length), e = a.colorCounter, a.colorCounter++ , a.colorCounter === c && (a.colorCounter = 0)) : e = a.colorIndex; this.colorIndex = r(this.colorIndex, e); a.chart.pointCount++; h(this, "afterInit"); return this
            }, applyOptions: function (a, e) {
                var d = this.series, c = d.options.pointValKey || d.pointValKey; a = z.prototype.optionsToObject.call(this, a); C(this, a); this.options = this.options ? C(this.options,
                    a) : a; a.group && delete this.group; a.dataLabels && delete this.dataLabels; c && (this.y = this[c]); this.isNull = r(this.isValid && !this.isValid(), null === this.x || !w(this.y, !0)); this.selected && (this.state = "select"); "name" in this && void 0 === e && d.xAxis && d.xAxis.hasNames && (this.x = d.xAxis.nameToX(this)); void 0 === this.x && d && (this.x = void 0 === e ? d.autoIncrement(this) : e); return this
            }, setNestedProperty: function (d, e, m) { m.split(".").reduce(function (c, d, b, m) { c[d] = m.length - 1 === b ? e : a.isObject(c[d], !0) ? c[d] : {}; return c[d] }, d); return d },
            optionsToObject: function (d) { var e = {}, m = this.series, c = m.options.keys, g = c || m.pointArrayMap || ["y"], b = g.length, k = 0, h = 0; if (w(d) || null === d) e[g[0]] = d; else if (t(d)) for (!c && d.length > b && (m = typeof d[0], "string" === m ? e.name = d[0] : "number" === m && (e.x = d[0]), k++); h < b;)c && void 0 === d[k] || (0 < g[h].indexOf(".") ? a.Point.prototype.setNestedProperty(e, d[k], g[h]) : e[g[h]] = d[k]), k++ , h++; else "object" === typeof d && (e = d, d.dataLabels && (m._hasPointLabels = !0), d.marker && (m._hasPointMarkers = !0)); return e }, getClassName: function () {
                return "highcharts-point" +
                    (this.selected ? " highcharts-point-select" : "") + (this.negative ? " highcharts-negative" : "") + (this.isNull ? " highcharts-null-point" : "") + (void 0 !== this.colorIndex ? " highcharts-color-" + this.colorIndex : "") + (this.options.className ? " " + this.options.className : "") + (this.zone && this.zone.className ? " " + this.zone.className.replace("highcharts-negative", "") : "")
            }, getZone: function () {
                var a = this.series, e = a.zones, a = a.zoneAxis || "y", d = 0, c; for (c = e[d]; this[a] >= c.value;)c = e[++d]; this.nonZonedColor || (this.nonZonedColor = this.color);
                this.color = c && c.color && !this.options.color ? c.color : this.nonZonedColor; return c
            }, destroy: function () { var a = this.series.chart, e = a.hoverPoints, d; a.pointCount--; e && (this.setState(), B(e, this), e.length || (a.hoverPoints = null)); if (this === a.hoverPoint) this.onMouseOut(); if (this.graphic || this.dataLabel || this.dataLabels) k(this), this.destroyElements(); this.legendItem && a.legend.destroyItem(this); for (d in this) this[d] = null }, destroyElements: function () {
                for (var a = ["graphic", "dataLabel", "dataLabelUpper", "connector", "shadowGroup"],
                    e, d = 6; d--;)e = a[d], this[e] && (this[e] = this[e].destroy()); this.dataLabels && (this.dataLabels.forEach(function (a) { a.element && a.destroy() }), delete this.dataLabels); this.connectors && (this.connectors.forEach(function (a) { a.element && a.destroy() }), delete this.connectors)
            }, getLabelConfig: function () { return { x: this.category, y: this.y, color: this.color, colorIndex: this.colorIndex, key: this.name || this.category, series: this.series, point: this, percentage: this.percentage, total: this.total || this.stackTotal } }, tooltipFormatter: function (a) {
                var e =
                    this.series, m = e.tooltipOptions, c = r(m.valueDecimals, ""), g = m.valuePrefix || "", b = m.valueSuffix || ""; e.chart.styledMode && (a = e.chart.tooltip.styledModeFormat(a)); (e.pointArrayMap || ["y"]).forEach(function (e) { e = "{point." + e; if (g || b) a = a.replace(RegExp(e + "}", "g"), g + e + "}" + b); a = a.replace(RegExp(e + "}", "g"), e + ":,." + c + "f}") }); return d(a, { point: this, series: this.series }, e.chart.time)
            }, firePointEvent: function (a, e, d) {
                var c = this, g = this.series.options; (g.point.events[a] || c.options && c.options.events && c.options.events[a]) &&
                    this.importEvents(); "click" === a && g.allowPointSelect && (d = function (a) { c.select && c.select(null, a.ctrlKey || a.metaKey || a.shiftKey) }); h(this, a, e, d)
            }, visible: !0
        }
    })(H); (function (a) {
        var z = a.addEvent, C = a.animObject, B = a.arrayMax, h = a.arrayMin, d = a.correctFloat, t = a.defaultOptions, w = a.defaultPlotOptions, r = a.defined, v = a.erase, p = a.extend, k = a.fireEvent, n = a.isArray, e = a.isNumber, m = a.isString, c = a.merge, g = a.objectEach, b = a.pick, q = a.removeEvent, u = a.splat, x = a.SVGElement, D = a.syncTimeout, E = a.win; a.Series = a.seriesType("line",
            null, {
                lineWidth: 2, allowPointSelect: !1, showCheckbox: !1, animation: { duration: 1E3 }, events: {}, marker: { lineWidth: 0, lineColor: "#ffffff", enabledThreshold: 2, radius: 4, states: { normal: { animation: !0 }, hover: { animation: { duration: 50 }, enabled: !0, radiusPlus: 2, lineWidthPlus: 1 }, select: { fillColor: "#cccccc", lineColor: "#000000", lineWidth: 2 } } }, point: { events: {} }, dataLabels: {
                    align: "center", formatter: function () { return null === this.y ? "" : a.numberFormat(this.y, -1) }, style: { fontSize: "11px", fontWeight: "bold", color: "contrast", textOutline: "1px contrast" },
                    verticalAlign: "bottom", x: 0, y: 0, padding: 5
                }, cropThreshold: 300, pointRange: 0, softThreshold: !0, states: { normal: { animation: !0 }, hover: { animation: { duration: 50 }, lineWidthPlus: 1, marker: {}, halo: { size: 10, opacity: .25 } }, select: {} }, stickyTracking: !0, turboThreshold: 1E3, findNearestPointBy: "x"
            }, {
                isCartesian: !0, pointClass: a.Point, sorted: !0, requireSorting: !0, directTouch: !1, axisTypes: ["xAxis", "yAxis"], colorCounter: 0, parallelArrays: ["x", "y"], coll: "series", init: function (a, c) {
                    k(this, "init", { options: c }); var l = this, e, f = a.series,
                        d; l.chart = a; l.options = c = l.setOptions(c); l.linkedSeries = []; l.bindAxes(); p(l, { name: c.name, state: "", visible: !1 !== c.visible, selected: !0 === c.selected }); e = c.events; g(e, function (a, b) { z(l, b, a) }); if (e && e.click || c.point && c.point.events && c.point.events.click || c.allowPointSelect) a.runTrackerClick = !0; l.getColor(); l.getSymbol(); l.parallelArrays.forEach(function (a) { l[a + "Data"] = [] }); l.setData(c.data, !1); l.isCartesian && (a.hasCartesianSeries = !0); f.length && (d = f[f.length - 1]); l._i = b(d && d._i, -1) + 1; a.orderSeries(this.insert(f));
                    k(this, "afterInit")
                }, insert: function (a) { var c = this.options.index, l; if (e(c)) { for (l = a.length; l--;)if (c >= b(a[l].options.index, a[l]._i)) { a.splice(l + 1, 0, this); break } -1 === l && a.unshift(this); l += 1 } else a.push(this); return b(l, a.length - 1) }, bindAxes: function () {
                    var b = this, c = b.options, e = b.chart, d; (b.axisTypes || []).forEach(function (f) {
                        e[f].forEach(function (a) { d = a.options; if (c[f] === d.index || void 0 !== c[f] && c[f] === d.id || void 0 === c[f] && 0 === d.index) b.insert(a.series), b[f] = a, a.isDirty = !0 }); b[f] || b.optionalAxis === f ||
                            a.error(18, !0, e)
                    })
                }, updateParallelArrays: function (a, b) { var c = a.series, l = arguments, f = e(b) ? function (f) { var l = "y" === f && c.toYData ? c.toYData(a) : a[f]; c[f + "Data"][b] = l } : function (a) { Array.prototype[b].apply(c[a + "Data"], Array.prototype.slice.call(l, 2)) }; c.parallelArrays.forEach(f) }, autoIncrement: function () {
                    var a = this.options, c = this.xIncrement, e, d = a.pointIntervalUnit, f = this.chart.time, c = b(c, a.pointStart, 0); this.pointInterval = e = b(this.pointInterval, a.pointInterval, 1); d && (a = new f.Date(c), "day" === d ? f.set("Date",
                        a, f.get("Date", a) + e) : "month" === d ? f.set("Month", a, f.get("Month", a) + e) : "year" === d && f.set("FullYear", a, f.get("FullYear", a) + e), e = a.getTime() - c); this.xIncrement = c + e; return c
                }, setOptions: function (a) {
                    var e = this.chart, l = e.options, d = l.plotOptions, f = (e.userOptions || {}).plotOptions || {}, g = d[this.type], m = e.styledMode; this.userOptions = a; e = c(g, d.series, a); this.tooltipOptions = c(t.tooltip, t.plotOptions.series && t.plotOptions.series.tooltip, t.plotOptions[this.type].tooltip, l.tooltip.userOptions, d.series && d.series.tooltip,
                        d[this.type].tooltip, a.tooltip); this.stickyTracking = b(a.stickyTracking, f[this.type] && f[this.type].stickyTracking, f.series && f.series.stickyTracking, this.tooltipOptions.shared && !this.noSharedTooltip ? !0 : e.stickyTracking); null === g.marker && delete e.marker; this.zoneAxis = e.zoneAxis; a = this.zones = (e.zones || []).slice(); !e.negativeColor && !e.negativeFillColor || e.zones || (l = { value: e[this.zoneAxis + "Threshold"] || e.threshold || 0, className: "highcharts-negative" }, m || (l.color = e.negativeColor, l.fillColor = e.negativeFillColor),
                            a.push(l)); a.length && r(a[a.length - 1].value) && a.push(m ? {} : { color: this.color, fillColor: this.fillColor }); k(this, "afterSetOptions", { options: e }); return e
                }, getName: function () { return b(this.options.name, "Series " + (this.index + 1)) }, getCyclic: function (a, c, e) { var l, f = this.chart, d = this.userOptions, g = a + "Index", m = a + "Counter", k = e ? e.length : b(f.options.chart[a + "Count"], f[a + "Count"]); c || (l = b(d[g], d["_" + g]), r(l) || (f.series.length || (f[m] = 0), d["_" + g] = l = f[m] % k, f[m] += 1), e && (c = e[l])); void 0 !== l && (this[g] = l); this[a] = c }, getColor: function () {
                    this.chart.styledMode ?
                    this.getCyclic("color") : this.options.colorByPoint ? this.options.color = null : this.getCyclic("color", this.options.color || w[this.type].color, this.chart.options.colors)
                }, getSymbol: function () { this.getCyclic("symbol", this.options.marker.symbol, this.chart.options.symbols) }, drawLegendSymbol: a.LegendSymbolMixin.drawLineMarker, updateData: function (b) {
                    var c = this.options, l = this.points, d = [], f, g, m, k = this.requireSorting; this.xIncrement = null; b.forEach(function (b) {
                        var g, h, q; g = a.defined(b) && this.pointClass.prototype.optionsToObject.call({ series: this },
                            b) || {}; q = g.x; if ((g = g.id) || e(q)) g && (h = (h = this.chart.get(g)) && h.x), void 0 === h && e(q) && (h = this.xData.indexOf(q, m)), -1 === h || void 0 === h || l[h].touched ? d.push(b) : b !== c.data[h] ? (l[h].update(b, !1, null, !1), l[h].touched = !0, k && (m = h + 1)) : l[h] && (l[h].touched = !0), f = !0
                    }, this); if (f) for (b = l.length; b--;)g = l[b], g.touched || g.remove(!1), g.touched = !1; else if (b.length === l.length) b.forEach(function (a, b) { l[b].update && a !== c.data[b] && l[b].update(a, !1, null, !1) }); else return !1; d.forEach(function (a) { this.addPoint(a, !1) }, this); return !0
                },
                setData: function (c, d, g, k) {
                    var f = this, l = f.points, h = l && l.length || 0, q, u = f.options, A = f.chart, x = null, p = f.xAxis, F = u.turboThreshold, r = this.xData, v = this.yData, E = (q = f.pointArrayMap) && q.length, G; c = c || []; q = c.length; d = b(d, !0); !1 !== k && q && h && !f.cropped && !f.hasGroupedData && f.visible && !f.isSeriesBoosting && (G = this.updateData(c)); if (!G) {
                    f.xIncrement = null; f.colorCounter = 0; this.parallelArrays.forEach(function (a) { f[a + "Data"].length = 0 }); if (F && q > F) {
                        for (g = 0; null === x && g < q;)x = c[g], g++; if (e(x)) for (g = 0; g < q; g++)r[g] = this.autoIncrement(),
                            v[g] = c[g]; else if (n(x)) if (E) for (g = 0; g < q; g++)x = c[g], r[g] = x[0], v[g] = x.slice(1, E + 1); else for (g = 0; g < q; g++)x = c[g], r[g] = x[0], v[g] = x[1]; else a.error(12, !1, A)
                    } else for (g = 0; g < q; g++)void 0 !== c[g] && (x = { series: f }, f.pointClass.prototype.applyOptions.apply(x, [c[g]]), f.updateParallelArrays(x, g)); v && m(v[0]) && a.error(14, !0, A); f.data = []; f.options.data = f.userOptions.data = c; for (g = h; g--;)l[g] && l[g].destroy && l[g].destroy(); p && (p.minRange = p.userMinRange); f.isDirty = A.isDirtyBox = !0; f.isDirtyData = !!l; g = !1
                    } "point" === u.legendType &&
                        (this.processData(), this.generatePoints()); d && A.redraw(g)
                }, processData: function (b) {
                    var c = this.xData, e = this.yData, l = c.length, f; f = 0; var d, g, m = this.xAxis, k, h = this.options; k = h.cropThreshold; var q = this.getExtremesFromAll || h.getExtremesFromAll, n = this.isCartesian, h = m && m.val2lin, u = m && m.isLog, x = this.requireSorting, p, r; if (n && !this.isDirty && !m.isDirty && !this.yAxis.isDirty && !b) return !1; m && (b = m.getExtremes(), p = b.min, r = b.max); n && this.sorted && !q && (!k || l > k || this.forceCrop) && (c[l - 1] < p || c[0] > r ? (c = [], e = []) : this.yData &&
                        (c[0] < p || c[l - 1] > r) && (f = this.cropData(this.xData, this.yData, p, r), c = f.xData, e = f.yData, f = f.start, d = !0)); for (k = c.length || 1; --k;)l = u ? h(c[k]) - h(c[k - 1]) : c[k] - c[k - 1], 0 < l && (void 0 === g || l < g) ? g = l : 0 > l && x && (a.error(15, !1, this.chart), x = !1); this.cropped = d; this.cropStart = f; this.processedXData = c; this.processedYData = e; this.closestPointRange = g
                }, cropData: function (a, c, e, d, f) {
                    var l = a.length, g = 0, m = l, k; f = b(f, this.cropShoulder, 1); for (k = 0; k < l; k++)if (a[k] >= e) { g = Math.max(0, k - f); break } for (e = k; e < l; e++)if (a[e] > d) { m = e + f; break } return {
                        xData: a.slice(g,
                            m), yData: c.slice(g, m), start: g, end: m
                    }
                }, generatePoints: function () {
                    var a = this.options, b = a.data, c = this.data, e, f = this.processedXData, d = this.processedYData, g = this.pointClass, m = f.length, k = this.cropStart || 0, h, q = this.hasGroupedData, a = a.keys, n, x = [], r; c || q || (c = [], c.length = b.length, c = this.data = c); a && q && (this.options.keys = !1); for (r = 0; r < m; r++)h = k + r, q ? (n = (new g).init(this, [f[r]].concat(u(d[r]))), n.dataGroup = this.groupMap[r], n.dataGroup.options && (n.options = n.dataGroup.options, p(n, n.dataGroup.options))) : (n = c[h]) ||
                        void 0 === b[h] || (c[h] = n = (new g).init(this, b[h], f[r])), n && (n.index = h, x[r] = n); this.options.keys = a; if (c && (m !== (e = c.length) || q)) for (r = 0; r < e; r++)r !== k || q || (r += m), c[r] && (c[r].destroyElements(), c[r].plotX = void 0); this.data = c; this.points = x
                }, getExtremes: function (a) {
                    var b = this.yAxis, c = this.processedXData, l, f = [], d = 0; l = this.xAxis.getExtremes(); var g = l.min, m = l.max, k, q, u = this.requireSorting ? 1 : 0, x, p; a = a || this.stackedYData || this.processedYData || []; l = a.length; for (p = 0; p < l; p++)if (q = c[p], x = a[p], k = (e(x, !0) || n(x)) && (!b.positiveValuesOnly ||
                        x.length || 0 < x), q = this.getExtremesFromAll || this.options.getExtremesFromAll || this.cropped || (c[p + u] || q) >= g && (c[p - u] || q) <= m, k && q) if (k = x.length) for (; k--;)"number" === typeof x[k] && (f[d++] = x[k]); else f[d++] = x; this.dataMin = h(f); this.dataMax = B(f)
                }, translate: function () {
                this.processedXData || this.processData(); this.generatePoints(); var a = this.options, c = a.stacking, g = this.xAxis, m = g.categories, f = this.yAxis, h = this.points, q = h.length, n = !!this.modifyValue, u = a.pointPlacement, x = "between" === u || e(u), p = a.threshold, v = a.startFromThreshold ?
                    p : 0, E, D, t, w, B = Number.MAX_VALUE; "between" === u && (u = .5); e(u) && (u *= b(a.pointRange || g.pointRange)); for (a = 0; a < q; a++) {
                        var z = h[a], C = z.x, H = z.y; D = z.low; var V = c && f.stacks[(this.negStacks && H < (v ? 0 : p) ? "-" : "") + this.stackKey], U; f.positiveValuesOnly && null !== H && 0 >= H && (z.isNull = !0); z.plotX = E = d(Math.min(Math.max(-1E5, g.translate(C, 0, 0, 0, 1, u, "flags" === this.type)), 1E5)); c && this.visible && !z.isNull && V && V[C] && (w = this.getStackIndicator(w, C, this.index), U = V[C], H = U.points[w.key], D = H[0], H = H[1], D === v && w.key === V[C].base && (D = b(e(p) &&
                            p, f.min)), f.positiveValuesOnly && 0 >= D && (D = null), z.total = z.stackTotal = U.total, z.percentage = U.total && z.y / U.total * 100, z.stackY = H, U.setOffset(this.pointXOffset || 0, this.barW || 0)); z.yBottom = r(D) ? Math.min(Math.max(-1E5, f.translate(D, 0, 1, 0, 1)), 1E5) : null; n && (H = this.modifyValue(H, z)); z.plotY = D = "number" === typeof H && Infinity !== H ? Math.min(Math.max(-1E5, f.translate(H, 0, 1, 0, 1)), 1E5) : void 0; z.isInside = void 0 !== D && 0 <= D && D <= f.len && 0 <= E && E <= g.len; z.clientX = x ? d(g.translate(C, 0, 0, 0, 1, u)) : E; z.negative = z.y < (p || 0); z.category =
                                m && void 0 !== m[z.x] ? m[z.x] : z.x; z.isNull || (void 0 !== t && (B = Math.min(B, Math.abs(E - t))), t = E); z.zone = this.zones.length && z.getZone()
                    } this.closestPointRangePx = B; k(this, "afterTranslate")
                }, getValidPoints: function (a, b) { var c = this.chart; return (a || this.points || []).filter(function (a) { return b && !c.isInsidePlot(a.plotX, a.plotY, c.inverted) ? !1 : !a.isNull }) }, setClip: function (a) {
                    var b = this.chart, c = this.options, e = b.renderer, f = b.inverted, l = this.clipBox, d = l || b.clipBox, g = this.sharedClipKey || ["_sharedClip", a && a.duration, a &&
                        a.easing, d.height, c.xAxis, c.yAxis].join(), m = b[g], k = b[g + "m"]; m || (a && (d.width = 0, f && (d.x = b.plotSizeX), b[g + "m"] = k = e.clipRect(f ? b.plotSizeX + 99 : -99, f ? -b.plotLeft : -b.plotTop, 99, f ? b.chartWidth : b.chartHeight)), b[g] = m = e.clipRect(d), m.count = { length: 0 }); a && !m.count[this.index] && (m.count[this.index] = !0, m.count.length += 1); !1 !== c.clip && (this.group.clip(a || l ? m : b.clipRect), this.markerGroup.clip(k), this.sharedClipKey = g); a || (m.count[this.index] && (delete m.count[this.index], --m.count.length), 0 === m.count.length && g && b[g] &&
                            (l || (b[g] = b[g].destroy()), b[g + "m"] && (b[g + "m"] = b[g + "m"].destroy())))
                }, animate: function (a) { var b = this.chart, c = C(this.options.animation), e; a ? this.setClip(c) : (e = this.sharedClipKey, (a = b[e]) && a.animate({ width: b.plotSizeX, x: 0 }, c), b[e + "m"] && b[e + "m"].animate({ width: b.plotSizeX + 99, x: 0 }, c), this.animate = null) }, afterAnimate: function () { this.setClip(); k(this, "afterAnimate"); this.finishedAnimating = !0 }, drawPoints: function () {
                    var a = this.points, c = this.chart, e, d, f, g, m = this.options.marker, k, h, q, n = this[this.specialGroup] ||
                        this.markerGroup; e = this.xAxis; var u, x = b(m.enabled, !e || e.isRadial ? !0 : null, this.closestPointRangePx >= m.enabledThreshold * m.radius); if (!1 !== m.enabled || this._hasPointMarkers) for (e = 0; e < a.length; e++)d = a[e], g = d.graphic, k = d.marker || {}, h = !!d.marker, f = x && void 0 === k.enabled || k.enabled, q = !1 !== d.isInside, f && !d.isNull ? (f = b(k.symbol, this.symbol), u = this.markerAttribs(d, d.selected && "select"), g ? g[q ? "show" : "hide"](!0).animate(u) : q && (0 < u.width || d.hasImage) && (d.graphic = g = c.renderer.symbol(f, u.x, u.y, u.width, u.height, h ?
                            k : m).add(n)), g && !c.styledMode && g.attr(this.pointAttribs(d, d.selected && "select")), g && g.addClass(d.getClassName(), !0)) : g && (d.graphic = g.destroy())
                }, markerAttribs: function (a, c) { var e = this.options.marker, l = a.marker || {}, f = l.symbol || e.symbol, d = b(l.radius, e.radius); c && (e = e.states[c], c = l.states && l.states[c], d = b(c && c.radius, e && e.radius, d + (e && e.radiusPlus || 0))); a.hasImage = f && 0 === f.indexOf("url"); a.hasImage && (d = 0); a = { x: Math.floor(a.plotX) - d, y: a.plotY - d }; d && (a.width = a.height = 2 * d); return a }, pointAttribs: function (a,
                    c) { var e = this.options.marker, l = a && a.options, f = l && l.marker || {}, d = this.color, g = l && l.color, m = a && a.color, l = b(f.lineWidth, e.lineWidth); a = a && a.zone && a.zone.color; d = g || a || m || d; a = f.fillColor || e.fillColor || d; d = f.lineColor || e.lineColor || d; c && (e = e.states[c], c = f.states && f.states[c] || {}, l = b(c.lineWidth, e.lineWidth, l + b(c.lineWidthPlus, e.lineWidthPlus, 0)), a = c.fillColor || e.fillColor || a, d = c.lineColor || e.lineColor || d); return { stroke: d, "stroke-width": l, fill: a } }, destroy: function () {
                        var b = this, c = b.chart, e = /AppleWebKit\/533/.test(E.navigator.userAgent),
                        d, f, m = b.data || [], h, n; k(b, "destroy"); q(b); (b.axisTypes || []).forEach(function (a) { (n = b[a]) && n.series && (v(n.series, b), n.isDirty = n.forceRedraw = !0) }); b.legendItem && b.chart.legend.destroyItem(b); for (f = m.length; f--;)(h = m[f]) && h.destroy && h.destroy(); b.points = null; a.clearTimeout(b.animationTimeout); g(b, function (a, b) { a instanceof x && !a.survive && (d = e && "group" === b ? "hide" : "destroy", a[d]()) }); c.hoverSeries === b && (c.hoverSeries = null); v(c.series, b); c.orderSeries(); g(b, function (a, c) { delete b[c] })
                    }, getGraphPath: function (a,
                        b, c) {
                            var e = this, f = e.options, d = f.step, l, g = [], m = [], k; a = a || e.points; (l = a.reversed) && a.reverse(); (d = { right: 1, center: 2 }[d] || d && 3) && l && (d = 4 - d); !f.connectNulls || b || c || (a = this.getValidPoints(a)); a.forEach(function (l, h) {
                                var q = l.plotX, n = l.plotY, u = a[h - 1]; (l.leftCliff || u && u.rightCliff) && !c && (k = !0); l.isNull && !r(b) && 0 < h ? k = !f.connectNulls : l.isNull && !b ? k = !0 : (0 === h || k ? h = ["M", l.plotX, l.plotY] : e.getPointSpline ? h = e.getPointSpline(a, l, h) : d ? (h = 1 === d ? ["L", u.plotX, n] : 2 === d ? ["L", (u.plotX + q) / 2, u.plotY, "L", (u.plotX + q) / 2, n] :
                                    ["L", q, u.plotY], h.push("L", q, n)) : h = ["L", q, n], m.push(l.x), d && (m.push(l.x), 2 === d && m.push(l.x)), g.push.apply(g, h), k = !1)
                            }); g.xMap = m; return e.graphPath = g
                    }, drawGraph: function () {
                        var a = this, b = this.options, c = (this.gappedPath || this.getGraphPath).call(this), e = this.chart.styledMode, f = [["graph", "highcharts-graph"]]; e || f[0].push(b.lineColor || this.color, b.dashStyle); f = a.getZonesGraphs(f); f.forEach(function (f, d) {
                            var l = f[0], g = a[l]; g ? (g.endX = a.preventGraphAnimation ? null : c.xMap, g.animate({ d: c })) : c.length && (a[l] = a.chart.renderer.path(c).addClass(f[1]).attr({ zIndex: 1 }).add(a.group),
                                e || (g = { stroke: f[2], "stroke-width": b.lineWidth, fill: a.fillGraph && a.color || "none" }, f[3] ? g.dashstyle = f[3] : "square" !== b.linecap && (g["stroke-linecap"] = g["stroke-linejoin"] = "round"), g = a[l].attr(g).shadow(2 > d && b.shadow))); g && (g.startX = c.xMap, g.isArea = c.isArea)
                        })
                    }, getZonesGraphs: function (a) {
                        this.zones.forEach(function (b, c) { c = ["zone-graph-" + c, "highcharts-graph highcharts-zone-graph-" + c + " " + (b.className || "")]; this.chart.styledMode || c.push(b.color || this.color, b.dashStyle || this.options.dashStyle); a.push(c) },
                            this); return a
                    }, applyZones: function () {
                        var a = this, c = this.chart, e = c.renderer, d = this.zones, f, g, m = this.clips || [], k, h = this.graph, q = this.area, n = Math.max(c.chartWidth, c.chartHeight), u = this[(this.zoneAxis || "y") + "Axis"], x, p, r = c.inverted, v, E, D, t, w = !1; d.length && (h || q) && u && void 0 !== u.min && (p = u.reversed, v = u.horiz, h && !this.showLine && h.hide(), q && q.hide(), x = u.getExtremes(), d.forEach(function (d, l) {
                            f = p ? v ? c.plotWidth : 0 : v ? 0 : u.toPixels(x.min) || 0; f = Math.min(Math.max(b(g, f), 0), n); g = Math.min(Math.max(Math.round(u.toPixels(b(d.value,
                                x.max), !0) || 0), 0), n); w && (f = g = u.toPixels(x.max)); E = Math.abs(f - g); D = Math.min(f, g); t = Math.max(f, g); u.isXAxis ? (k = { x: r ? t : D, y: 0, width: E, height: n }, v || (k.x = c.plotHeight - k.x)) : (k = { x: 0, y: r ? t : D, width: n, height: E }, v && (k.y = c.plotWidth - k.y)); r && e.isVML && (k = u.isXAxis ? { x: 0, y: p ? D : t, height: k.width, width: c.chartWidth } : { x: k.y - c.plotLeft - c.spacingBox.x, y: 0, width: k.height, height: c.chartHeight }); m[l] ? m[l].animate(k) : (m[l] = e.clipRect(k), h && a["zone-graph-" + l].clip(m[l]), q && a["zone-area-" + l].clip(m[l])); w = d.value > x.max; a.resetZones &&
                                    0 === g && (g = void 0)
                        }), this.clips = m)
                    }, invertGroups: function (a) { function b() { ["group", "markerGroup"].forEach(function (b) { c[b] && (e.renderer.isVML && c[b].attr({ width: c.yAxis.len, height: c.xAxis.len }), c[b].width = c.yAxis.len, c[b].height = c.xAxis.len, c[b].invert(a)) }) } var c = this, e = c.chart, f; c.xAxis && (f = z(e, "resize", b), z(c, "destroy", f), b(a), c.invertGroups = b) }, plotGroup: function (a, b, c, e, f) {
                        var d = this[a], l = !d; l && (this[a] = d = this.chart.renderer.g().attr({ zIndex: e || .1 }).add(f)); d.addClass("highcharts-" + b + " highcharts-series-" +
                            this.index + " highcharts-" + this.type + "-series " + (r(this.colorIndex) ? "highcharts-color-" + this.colorIndex + " " : "") + (this.options.className || "") + (d.hasClass("highcharts-tracker") ? " highcharts-tracker" : ""), !0); d.attr({ visibility: c })[l ? "attr" : "animate"](this.getPlotBox()); return d
                    }, getPlotBox: function () { var a = this.chart, b = this.xAxis, c = this.yAxis; a.inverted && (b = c, c = this.xAxis); return { translateX: b ? b.left : a.plotLeft, translateY: c ? c.top : a.plotTop, scaleX: 1, scaleY: 1 } }, render: function () {
                        var a = this, b = a.chart, c, e =
                            a.options, f = !!a.animate && b.renderer.isSVG && C(e.animation).duration, d = a.visible ? "inherit" : "hidden", g = e.zIndex, m = a.hasRendered, h = b.seriesGroup, q = b.inverted; c = a.plotGroup("group", "series", d, g, h); a.markerGroup = a.plotGroup("markerGroup", "markers", d, g, h); f && a.animate(!0); c.inverted = a.isCartesian ? q : !1; a.drawGraph && (a.drawGraph(), a.applyZones()); a.drawDataLabels && a.drawDataLabels(); a.visible && a.drawPoints(); a.drawTracker && !1 !== a.options.enableMouseTracking && a.drawTracker(); a.invertGroups(q); !1 === e.clip ||
                                a.sharedClipKey || m || c.clip(b.clipRect); f && a.animate(); m || (a.animationTimeout = D(function () { a.afterAnimate() }, f)); a.isDirty = !1; a.hasRendered = !0; k(a, "afterRender")
                    }, redraw: function () { var a = this.chart, c = this.isDirty || this.isDirtyData, e = this.group, d = this.xAxis, f = this.yAxis; e && (a.inverted && e.attr({ width: a.plotWidth, height: a.plotHeight }), e.animate({ translateX: b(d && d.left, a.plotLeft), translateY: b(f && f.top, a.plotTop) })); this.translate(); this.render(); c && delete this.kdTree }, kdAxisArray: ["clientX", "plotY"],
                searchPoint: function (a, b) { var c = this.xAxis, e = this.yAxis, f = this.chart.inverted; return this.searchKDTree({ clientX: f ? c.len - a.chartY + c.pos : a.chartX - c.pos, plotY: f ? e.len - a.chartX + e.pos : a.chartY - e.pos }, b) }, buildKDTree: function () {
                    function a(c, e, d) { var f, l; if (l = c && c.length) return f = b.kdAxisArray[e % d], c.sort(function (a, b) { return a[f] - b[f] }), l = Math.floor(l / 2), { point: c[l], left: a(c.slice(0, l), e + 1, d), right: a(c.slice(l + 1), e + 1, d) } } this.buildingKdTree = !0; var b = this, c = -1 < b.options.findNearestPointBy.indexOf("y") ? 2 :
                        1; delete b.kdTree; D(function () { b.kdTree = a(b.getValidPoints(null, !b.directTouch), c, c); b.buildingKdTree = !1 }, b.options.kdNow ? 0 : 1)
                }, searchKDTree: function (a, b) {
                    function c(a, b, g, m) {
                        var k = b.point, h = e.kdAxisArray[g % m], q, n, u = k; n = r(a[f]) && r(k[f]) ? Math.pow(a[f] - k[f], 2) : null; q = r(a[d]) && r(k[d]) ? Math.pow(a[d] - k[d], 2) : null; q = (n || 0) + (q || 0); k.dist = r(q) ? Math.sqrt(q) : Number.MAX_VALUE; k.distX = r(n) ? Math.sqrt(n) : Number.MAX_VALUE; h = a[h] - k[h]; q = 0 > h ? "left" : "right"; n = 0 > h ? "right" : "left"; b[q] && (q = c(a, b[q], g + 1, m), u = q[l] < u[l] ?
                            q : k); b[n] && Math.sqrt(h * h) < u[l] && (a = c(a, b[n], g + 1, m), u = a[l] < u[l] ? a : u); return u
                    } var e = this, f = this.kdAxisArray[0], d = this.kdAxisArray[1], l = b ? "distX" : "dist"; b = -1 < e.options.findNearestPointBy.indexOf("y") ? 2 : 1; this.kdTree || this.buildingKdTree || this.buildKDTree(); if (this.kdTree) return c(a, this.kdTree, b, b)
                }
            })
    })(H); (function (a) {
        var z = a.addEvent, C = a.animate, B = a.Axis, h = a.Chart, d = a.createElement, t = a.css, w = a.defined, r = a.erase, v = a.extend, p = a.fireEvent, k = a.isNumber, n = a.isObject, e = a.isArray, m = a.merge, c = a.objectEach,
        g = a.pick, b = a.Point, q = a.Series, u = a.seriesTypes, x = a.setAnimation, D = a.splat; a.cleanRecursively = function (b, e) { var d = {}; c(b, function (c, l) { if (n(b[l], !0) && e[l]) c = a.cleanRecursively(b[l], e[l]), Object.keys(c).length && (d[l] = c); else if (n(b[l]) || b[l] !== e[l]) d[l] = b[l] }); return d }; v(h.prototype, {
            addSeries: function (a, b, c) { var e, d = this; a && (b = g(b, !0), p(d, "addSeries", { options: a }, function () { e = d.initSeries(a); d.isDirtyLegend = !0; d.linkSeries(); p(d, "afterAddSeries"); b && d.redraw(c) })); return e }, addAxis: function (a, b, c, e) {
                var d =
                    b ? "xAxis" : "yAxis", f = this.options; a = m(a, { index: this[d].length, isX: b }); b = new B(this, a); f[d] = D(f[d] || {}); f[d].push(a); g(c, !0) && this.redraw(e); return b
            }, showLoading: function (a) {
                var b = this, c = b.options, e = b.loadingDiv, g = c.loading, f = function () { e && t(e, { left: b.plotLeft + "px", top: b.plotTop + "px", width: b.plotWidth + "px", height: b.plotHeight + "px" }) }; e || (b.loadingDiv = e = d("div", { className: "highcharts-loading highcharts-loading-hidden" }, null, b.container), b.loadingSpan = d("span", { className: "highcharts-loading-inner" },
                    null, e), z(b, "redraw", f)); e.className = "highcharts-loading"; b.loadingSpan.innerHTML = a || c.lang.loading; b.styledMode || (t(e, v(g.style, { zIndex: 10 })), t(b.loadingSpan, g.labelStyle), b.loadingShown || (t(e, { opacity: 0, display: "" }), C(e, { opacity: g.style.opacity || .5 }, { duration: g.showDuration || 0 }))); b.loadingShown = !0; f()
            }, hideLoading: function () {
                var a = this.options, b = this.loadingDiv; b && (b.className = "highcharts-loading highcharts-loading-hidden", this.styledMode || C(b, { opacity: 0 }, {
                    duration: a.loading.hideDuration || 100,
                    complete: function () { t(b, { display: "none" }) }
                })); this.loadingShown = !1
            }, propsRequireDirtyBox: "backgroundColor borderColor borderWidth margin marginTop marginRight marginBottom marginLeft spacing spacingTop spacingRight spacingBottom spacingLeft borderRadius plotBackgroundColor plotBackgroundImage plotBorderColor plotBorderWidth plotShadow shadow".split(" "), propsRequireUpdateSeries: "chart.inverted chart.polar chart.ignoreHiddenSeries chart.type colors plotOptions time tooltip".split(" "), collectionsWithUpdate: "xAxis yAxis zAxis series colorAxis pane".split(" "),
            update: function (b, e, d, h) {
                var l = this, f = { credits: "addCredits", title: "setTitle", subtitle: "setSubtitle" }, q, n, u, x = []; p(l, "update", { options: b }); b = a.cleanRecursively(b, l.options); if (q = b.chart) {
                    m(!0, l.options.chart, q); "className" in q && l.setClassName(q.className); "reflow" in q && l.setReflow(q.reflow); if ("inverted" in q || "polar" in q || "type" in q) l.propFromSeries(), n = !0; "alignTicks" in q && (n = !0); c(q, function (a, b) {
                    -1 !== l.propsRequireUpdateSeries.indexOf("chart." + b) && (u = !0); -1 !== l.propsRequireDirtyBox.indexOf(b) &&
                        (l.isDirtyBox = !0)
                    }); !l.styledMode && "style" in q && l.renderer.setStyle(q.style)
                } !l.styledMode && b.colors && (this.options.colors = b.colors); b.plotOptions && m(!0, this.options.plotOptions, b.plotOptions); c(b, function (a, b) { if (l[b] && "function" === typeof l[b].update) l[b].update(a, !1); else if ("function" === typeof l[f[b]]) l[f[b]](a); "chart" !== b && -1 !== l.propsRequireUpdateSeries.indexOf(b) && (u = !0) }); this.collectionsWithUpdate.forEach(function (a) {
                    var c; b[a] && ("series" === a && (c = [], l[a].forEach(function (a, b) {
                        a.options.isInternal ||
                        c.push(g(a.options.index, b))
                    })), D(b[a]).forEach(function (b, e) { (e = w(b.id) && l.get(b.id) || l[a][c ? c[e] : e]) && e.coll === a && (e.update(b, !1), d && (e.touched = !0)); if (!e && d) if ("series" === a) l.addSeries(b, !1).touched = !0; else if ("xAxis" === a || "yAxis" === a) l.addAxis(b, "xAxis" === a, !1).touched = !0 }), d && l[a].forEach(function (a) { a.touched || a.options.isInternal ? delete a.touched : x.push(a) }))
                }); x.forEach(function (a) { a.remove && a.remove(!1) }); n && l.axes.forEach(function (a) { a.update({}, !1) }); u && l.series.forEach(function (a) {
                    a.update({},
                        !1)
                }); b.loading && m(!0, l.options.loading, b.loading); n = q && q.width; q = q && q.height; k(n) && n !== l.chartWidth || k(q) && q !== l.chartHeight ? l.setSize(n, q, h) : g(e, !0) && l.redraw(h); p(l, "afterUpdate", { options: b })
            }, setSubtitle: function (a) { this.setTitle(void 0, a) }
        }); v(b.prototype, {
            update: function (a, b, c, e) {
                function d() {
                    f.applyOptions(a); null === f.y && m && (f.graphic = m.destroy()); n(a, !0) && (m && m.element && a && a.marker && void 0 !== a.marker.symbol && (f.graphic = m.destroy()), a && a.dataLabels && f.dataLabel && (f.dataLabel = f.dataLabel.destroy()),
                        f.connector && (f.connector = f.connector.destroy())); k = f.index; l.updateParallelArrays(f, k); q.data[k] = n(q.data[k], !0) || n(a, !0) ? f.options : g(a, q.data[k]); l.isDirty = l.isDirtyData = !0; !l.fixedBox && l.hasCartesianSeries && (h.isDirtyBox = !0); "point" === q.legendType && (h.isDirtyLegend = !0); b && h.redraw(c)
                } var f = this, l = f.series, m = f.graphic, k, h = l.chart, q = l.options; b = g(b, !0); !1 === e ? d() : f.firePointEvent("update", { options: a }, d)
            }, remove: function (a, b) { this.series.removePoint(this.series.data.indexOf(this), a, b) }
        }); v(q.prototype,
            {
                addPoint: function (a, b, c, e) {
                    var d = this.options, f = this.data, l = this.chart, m = this.xAxis, m = m && m.hasNames && m.names, k = d.data, h, q, n = this.xData, u, x; b = g(b, !0); h = { series: this }; this.pointClass.prototype.applyOptions.apply(h, [a]); x = h.x; u = n.length; if (this.requireSorting && x < n[u - 1]) for (q = !0; u && n[u - 1] > x;)u--; this.updateParallelArrays(h, "splice", u, 0, 0); this.updateParallelArrays(h, u); m && h.name && (m[x] = h.name); k.splice(u, 0, a); q && (this.data.splice(u, 0, null), this.processData()); "point" === d.legendType && this.generatePoints();
                    c && (f[0] && f[0].remove ? f[0].remove(!1) : (f.shift(), this.updateParallelArrays(h, "shift"), k.shift())); this.isDirtyData = this.isDirty = !0; b && l.redraw(e)
                }, removePoint: function (a, b, c) { var e = this, d = e.data, f = d[a], l = e.points, m = e.chart, k = function () { l && l.length === d.length && l.splice(a, 1); d.splice(a, 1); e.options.data.splice(a, 1); e.updateParallelArrays(f || { series: e }, "splice", a, 1); f && f.destroy(); e.isDirty = !0; e.isDirtyData = !0; b && m.redraw() }; x(c, m); b = g(b, !0); f ? f.firePointEvent("remove", null, k) : k() }, remove: function (a,
                    b, c) { function e() { d.destroy(); d.remove = null; f.isDirtyLegend = f.isDirtyBox = !0; f.linkSeries(); g(a, !0) && f.redraw(b) } var d = this, f = d.chart; !1 !== c ? p(d, "remove", null, e) : e() }, update: function (b, c) {
                        b = a.cleanRecursively(b, this.userOptions); var e = this, d = e.chart, l = e.userOptions, f = e.oldType || e.type, k = b.type || l.type || d.options.chart.type, h = u[f].prototype, q, n = ["group", "markerGroup", "dataLabelsGroup"], x = ["navigatorSeries", "baseSeries"], r = e.finishedAnimating && { animation: !1 }, D = ["data", "name", "turboThreshold"], t = Object.keys(b),
                            E = 0 < t.length; t.forEach(function (a) { -1 === D.indexOf(a) && (E = !1) }); if (E) b.data && this.setData(b.data, !1), b.name && this.setName(b.name, !1); else {
                                x = n.concat(x); x.forEach(function (a) { x[a] = e[a]; delete e[a] }); b = m(l, r, { index: e.index, pointStart: g(l.pointStart, e.xData[0]) }, { data: e.options.data }, b); e.remove(!1, null, !1); for (q in h) e[q] = void 0; u[k || f] ? v(e, u[k || f].prototype) : a.error(17, !0, d); x.forEach(function (a) { e[a] = x[a] }); e.init(d, b); b.zIndex !== l.zIndex && n.forEach(function (a) { e[a] && e[a].attr({ zIndex: b.zIndex }) });
                                e.oldType = f; d.linkSeries()
                            } p(this, "afterUpdate"); g(c, !0) && d.redraw(E ? void 0 : !1)
                    }, setName: function (a) { this.name = this.options.name = this.userOptions.name = a; this.chart.isDirtyLegend = !0 }
            }); v(B.prototype, {
                update: function (a, b) {
                    var e = this.chart, d = a && a.events || {}; a = m(this.userOptions, a); e.options[this.coll].indexOf && (e.options[this.coll][e.options[this.coll].indexOf(this.userOptions)] = a); c(e.options[this.coll].events, function (a, b) { "undefined" === typeof d[b] && (d[b] = void 0) }); this.destroy(!0); this.init(e, v(a,
                        { events: d })); e.isDirtyBox = !0; g(b, !0) && e.redraw()
                }, remove: function (a) { for (var b = this.chart, c = this.coll, d = this.series, m = d.length; m--;)d[m] && d[m].remove(!1); r(b.axes, this); r(b[c], this); e(b.options[c]) ? b.options[c].splice(this.options.index, 1) : delete b.options[c]; b[c].forEach(function (a, b) { a.options.index = a.userOptions.index = b }); this.destroy(); b.isDirtyBox = !0; g(a, !0) && b.redraw() }, setTitle: function (a, b) { this.update({ title: a }, b) }, setCategories: function (a, b) { this.update({ categories: a }, b) }
            })
    })(H); (function (a) {
        var z =
            a.animObject, C = a.color, B = a.extend, h = a.defined, d = a.isNumber, t = a.merge, w = a.pick, r = a.Series, v = a.seriesType, p = a.svg; v("column", "line", { borderRadius: 0, crisp: !0, groupPadding: .2, marker: null, pointPadding: .1, minPointLength: 0, cropThreshold: 50, pointRange: null, states: { hover: { halo: !1, brightness: .1 }, select: { color: "#cccccc", borderColor: "#000000" } }, dataLabels: { align: null, verticalAlign: null, y: null }, softThreshold: !1, startFromThreshold: !0, stickyTracking: !1, tooltip: { distance: 6 }, threshold: 0, borderColor: "#ffffff" }, {
                cropShoulder: 0,
                directTouch: !0, trackerGroups: ["group", "dataLabelsGroup"], negStacks: !0, init: function () { r.prototype.init.apply(this, arguments); var a = this, d = a.chart; d.hasRendered && d.series.forEach(function (e) { e.type === a.type && (e.isDirty = !0) }) }, getColumnMetrics: function () {
                    var a = this, d = a.options, e = a.xAxis, m = a.yAxis, c = e.options.reversedStacks, c = e.reversed && !c || !e.reversed && c, g, b = {}, h = 0; !1 === d.grouping ? h = 1 : a.chart.series.forEach(function (c) {
                        var e = c.options, d = c.yAxis, k; c.type !== a.type || !c.visible && a.chart.options.chart.ignoreHiddenSeries ||
                            m.len !== d.len || m.pos !== d.pos || (e.stacking ? (g = c.stackKey, void 0 === b[g] && (b[g] = h++), k = b[g]) : !1 !== e.grouping && (k = h++), c.columnIndex = k)
                    }); var u = Math.min(Math.abs(e.transA) * (e.ordinalSlope || d.pointRange || e.closestPointRange || e.tickInterval || 1), e.len), x = u * d.groupPadding, p = (u - 2 * x) / (h || 1), d = Math.min(d.maxPointWidth || e.len, w(d.pointWidth, p * (1 - 2 * d.pointPadding))); a.columnMetrics = { width: d, offset: (p - d) / 2 + (x + ((a.columnIndex || 0) + (c ? 1 : 0)) * p - u / 2) * (c ? -1 : 1) }; return a.columnMetrics
                }, crispCol: function (a, d, e, m) {
                    var c =
                        this.chart, g = this.borderWidth, b = -(g % 2 ? .5 : 0), g = g % 2 ? .5 : 1; c.inverted && c.renderer.isVML && (g += 1); this.options.crisp && (e = Math.round(a + e) + b, a = Math.round(a) + b, e -= a); m = Math.round(d + m) + g; b = .5 >= Math.abs(d) && .5 < m; d = Math.round(d) + g; m -= d; b && m && (--d, m += 1); return { x: a, y: d, width: e, height: m }
                }, translate: function () {
                    var a = this, d = a.chart, e = a.options, m = a.dense = 2 > a.closestPointRange * a.xAxis.transA, m = a.borderWidth = w(e.borderWidth, m ? 0 : 1), c = a.yAxis, g = e.threshold, b = a.translatedThreshold = c.getThreshold(g), q = w(e.minPointLength,
                        5), u = a.getColumnMetrics(), x = u.width, p = a.barW = Math.max(x, 1 + 2 * m), v = a.pointXOffset = u.offset; d.inverted && (b -= .5); e.pointPadding && (p = Math.ceil(p)); r.prototype.translate.apply(a); a.points.forEach(function (e) {
                            var l = w(e.yBottom, b), m = 999 + Math.abs(l), k = x, m = Math.min(Math.max(-m, e.plotY), c.len + m), f = e.plotX + v, u = p, n = Math.min(m, l), r, D = Math.max(m, l) - n; q && Math.abs(D) < q && (D = q, r = !c.reversed && !e.negative || c.reversed && e.negative, e.y === g && a.dataMax <= g && c.min < g && (r = !r), n = Math.abs(n - b) > q ? l - q : b - (r ? q : 0)); h(e.options.pointWidth) &&
                                (k = u = Math.ceil(e.options.pointWidth), f -= Math.round((k - x) / 2)); e.barX = f; e.pointWidth = k; e.tooltipPos = d.inverted ? [c.len + c.pos - d.plotLeft - m, a.xAxis.len - f - u / 2, D] : [f + u / 2, m + c.pos - d.plotTop, D]; e.shapeType = e.shapeType || "rect"; e.shapeArgs = a.crispCol.apply(a, e.isNull ? [f, b, u, 0] : [f, n, u, D])
                        })
                }, getSymbol: a.noop, drawLegendSymbol: a.LegendSymbolMixin.drawRectangle, drawGraph: function () { this.group[this.dense ? "addClass" : "removeClass"]("highcharts-dense-data") }, pointAttribs: function (a, d) {
                    var e = this.options, m, c = this.pointAttrToOptions ||
                        {}; m = c.stroke || "borderColor"; var g = c["stroke-width"] || "borderWidth", b = a && a.color || this.color, k = a && a[m] || e[m] || this.color || b, h = a && a[g] || e[g] || this[g] || 0, c = e.dashStyle; a && this.zones.length && (b = a.getZone(), b = a.options.color || b && b.color || this.color); d && (a = t(e.states[d], a.options.states && a.options.states[d] || {}), d = a.brightness, b = a.color || void 0 !== d && C(b).brighten(a.brightness).get() || b, k = a[m] || k, h = a[g] || h, c = a.dashStyle || c); m = { fill: b, stroke: k, "stroke-width": h }; c && (m.dashstyle = c); return m
                }, drawPoints: function () {
                    var a =
                        this, h = this.chart, e = a.options, m = h.renderer, c = e.animationLimit || 250, g; a.points.forEach(function (b) { var k = b.graphic, u = k && h.pointCount < c ? "animate" : "attr"; if (d(b.plotY) && null !== b.y) { g = b.shapeArgs; if (k) k[u](t(g)); else b.graphic = k = m[b.shapeType](g).add(b.group || a.group); e.borderRadius && k.attr({ r: e.borderRadius }); h.styledMode || k[u](a.pointAttribs(b, b.selected && "select")).shadow(e.shadow, null, e.stacking && !e.borderRadius); k.addClass(b.getClassName(), !0) } else k && (b.graphic = k.destroy()) })
                }, animate: function (a) {
                    var d =
                        this, e = this.yAxis, m = d.options, c = this.chart.inverted, g = {}, b = c ? "translateX" : "translateY", k; p && (a ? (g.scaleY = .001, a = Math.min(e.pos + e.len, Math.max(e.pos, e.toPixels(m.threshold))), c ? g.translateX = a - e.len : g.translateY = a, d.group.attr(g)) : (k = d.group.attr(b), d.group.animate({ scaleY: 1 }, B(z(d.options.animation), { step: function (a, c) { g[b] = k + c.pos * (e.pos - k); d.group.attr(g) } })), d.animate = null))
                }, remove: function () {
                    var a = this, d = a.chart; d.hasRendered && d.series.forEach(function (e) { e.type === a.type && (e.isDirty = !0) }); r.prototype.remove.apply(a,
                        arguments)
                }
            })
    })(H); (function (a) {
        var z = a.Series; a = a.seriesType; a("scatter", "line", { lineWidth: 0, findNearestPointBy: "xy", marker: { enabled: !0 }, tooltip: { headerFormat: '\x3cspan style\x3d"color:{point.color}"\x3e\u25cf\x3c/span\x3e \x3cspan style\x3d"font-size: 10px"\x3e {series.name}\x3c/span\x3e\x3cbr/\x3e', pointFormat: "x: \x3cb\x3e{point.x}\x3c/b\x3e\x3cbr/\x3ey: \x3cb\x3e{point.y}\x3c/b\x3e\x3cbr/\x3e" } }, {
            sorted: !1, requireSorting: !1, noSharedTooltip: !0, trackerGroups: ["group", "markerGroup", "dataLabelsGroup"],
            takeOrdinalPosition: !1, drawGraph: function () { this.options.lineWidth && z.prototype.drawGraph.call(this) }
        })
    })(H); (function (a) {
        var z = a.addEvent, C = a.arrayMax, B = a.defined, h = a.extend, d = a.format, t = a.merge, w = a.noop, r = a.pick, v = a.relativeLength, p = a.Series, k = a.seriesTypes, n = a.stableSort, e = a.isArray, m = a.splat; a.distribute = function (c, e, b) {
            function d(a, b) { return a.target - b.target } var g, m = !0, k = c, h = [], l; l = 0; var p = k.reducedLen || e; for (g = c.length; g--;)l += c[g].size; if (l > p) {
                n(c, function (a, b) {
                    return (b.rank || 0) - (a.rank ||
                        0)
                }); for (l = g = 0; l <= p;)l += c[g].size, g++; h = c.splice(g - 1, c.length)
            } n(c, d); for (c = c.map(function (a) { return { size: a.size, targets: [a.target], align: r(a.align, .5) } }); m;) {
                for (g = c.length; g--;)m = c[g], l = (Math.min.apply(0, m.targets) + Math.max.apply(0, m.targets)) / 2, m.pos = Math.min(Math.max(0, l - m.size * m.align), e - m.size); g = c.length; for (m = !1; g--;)0 < g && c[g - 1].pos + c[g - 1].size > c[g].pos && (c[g - 1].size += c[g].size, c[g - 1].targets = c[g - 1].targets.concat(c[g].targets), c[g - 1].align = .5, c[g - 1].pos + c[g - 1].size > e && (c[g - 1].pos = e - c[g - 1].size),
                    c.splice(g, 1), m = !0)
            } k.push.apply(k, h); g = 0; c.some(function (c) { var d = 0; if (c.targets.some(function () { k[g].pos = c.pos + d; if (Math.abs(k[g].pos - k[g].target) > b) return k.slice(0, g + 1).forEach(function (a) { delete a.pos }), k.reducedLen = (k.reducedLen || e) - .1 * e, k.reducedLen > .1 * e && a.distribute(k, e, b), !0; d += k[g].size; g++ })) return !0 }); n(k, d)
        }; p.prototype.drawDataLabels = function () {
            function c(a, b) {
                var c = b.filter; return c ? (b = c.operator, a = a[c.property], c = c.value, "\x3e" === b && a > c || "\x3c" === b && a < c || "\x3e\x3d" === b && a >= c || "\x3c\x3d" ===
                    b && a <= c || "\x3d\x3d" === b && a == c || "\x3d\x3d\x3d" === b && a === c ? !0 : !1) : !0
            } function g(a, b) { var c = [], d; if (e(a) && !e(b)) c = a.map(function (a) { return t(a, b) }); else if (e(b) && !e(a)) c = b.map(function (b) { return t(a, b) }); else if (e(a) || e(b)) for (d = Math.max(a.length, b.length); d--;)c[d] = t(a[d], b[d]); else c = t(a, b); return c } var b = this, k = b.chart, h = b.options, n = h.dataLabels, p = b.points, v, l = b.hasRendered || 0, A, w = r(n.defer, !!h.animation), G = k.renderer, n = g(g(k.options.plotOptions && k.options.plotOptions.series && k.options.plotOptions.series.dataLabels,
                k.options.plotOptions && k.options.plotOptions[b.type] && k.options.plotOptions[b.type].dataLabels), n); a.fireEvent(this, "drawDataLabels"); if (e(n) || n.enabled || b._hasPointLabels) A = b.plotGroup("dataLabelsGroup", "data-labels", w && !l ? "hidden" : "visible", n.zIndex || 6), w && (A.attr({ opacity: +l }), l || z(b, "afterAnimate", function () { b.visible && A.show(!0); A[h.animation ? "animate" : "attr"]({ opacity: 1 }, { duration: 200 }) })), p.forEach(function (e) {
                    v = m(g(n, e.dlOptions || e.options && e.options.dataLabels)); v.forEach(function (f, g) {
                        var l =
                            f.enabled && !e.isNull && c(e, f), m, q, n, u, x = e.dataLabels ? e.dataLabels[g] : e.dataLabel, p = e.connectors ? e.connectors[g] : e.connector, v = !x; l && (m = e.getLabelConfig(), q = f[e.formatPrefix + "Format"] || f.format, m = B(q) ? d(q, m, k.time) : (f[e.formatPrefix + "Formatter"] || f.formatter).call(m, f), q = f.style, n = f.rotation, k.styledMode || (q.color = r(f.color, q.color, b.color, "#000000"), "contrast" === q.color && (e.contrastColor = G.getContrast(e.color || b.color), q.color = f.inside || 0 > r(f.distance, e.labelDistance) || h.stacking ? e.contrastColor : "#000000"),
                                h.cursor && (q.cursor = h.cursor)), u = { r: f.borderRadius || 0, rotation: n, padding: f.padding, zIndex: 1 }, k.styledMode || (u.fill = f.backgroundColor, u.stroke = f.borderColor, u["stroke-width"] = f.borderWidth), a.objectEach(u, function (a, b) { void 0 === a && delete u[b] })); !x || l && B(m) ? l && B(m) && (x ? u.text = m : (e.dataLabels = e.dataLabels || [], x = e.dataLabels[g] = n ? G.text(m, 0, -9999).addClass("highcharts-data-label") : G.label(m, 0, -9999, f.shape, null, null, f.useHTML, null, "data-label"), g || (e.dataLabel = x), x.addClass(" highcharts-data-label-color-" +
                                    e.colorIndex + " " + (f.className || "") + (f.useHTML ? " highcharts-tracker" : ""))), x.options = f, x.attr(u), k.styledMode || x.css(q).shadow(f.shadow), x.added || x.add(A), b.alignDataLabel(e, x, f, null, v)) : (e.dataLabel = e.dataLabel && e.dataLabel.destroy(), e.dataLabels && (1 === e.dataLabels.length ? delete e.dataLabels : delete e.dataLabels[g]), g || delete e.dataLabel, p && (e.connector = e.connector.destroy(), e.connectors && (1 === e.connectors.length ? delete e.connectors : delete e.connectors[g])))
                    })
                }); a.fireEvent(this, "afterDrawDataLabels")
        };
        p.prototype.alignDataLabel = function (a, e, b, d, m) {
            var c = this.chart, g = this.isCartesian && c.inverted, k = r(a.dlBox && a.dlBox.centerX, a.plotX, -9999), l = r(a.plotY, -9999), q = e.getBBox(), n, u = b.rotation, f = b.align, p = this.visible && (a.series.forceDL || c.isInsidePlot(k, Math.round(l), g) || d && c.isInsidePlot(k, g ? d.x + 1 : d.y + d.height - 1, g)), v = "justify" === r(b.overflow, "justify"); if (p && (n = c.renderer.fontMetrics(c.styledMode ? void 0 : b.style.fontSize, e).b, d = h({ x: g ? this.yAxis.len - l : k, y: Math.round(g ? this.xAxis.len - k : l), width: 0, height: 0 },
                d), h(b, { width: q.width, height: q.height }), u ? (v = !1, k = c.renderer.rotCorr(n, u), k = { x: d.x + b.x + d.width / 2 + k.x, y: d.y + b.y + { top: 0, middle: .5, bottom: 1 }[b.verticalAlign] * d.height }, e[m ? "attr" : "animate"](k).attr({ align: f }), l = (u + 720) % 360, l = 180 < l && 360 > l, "left" === f ? k.y -= l ? q.height : 0 : "center" === f ? (k.x -= q.width / 2, k.y -= q.height / 2) : "right" === f && (k.x -= q.width, k.y -= l ? 0 : q.height), e.placed = !0, e.alignAttr = k) : (e.align(b, null, d), k = e.alignAttr), v && 0 <= d.height ? a.isLabelJustified = this.justifyDataLabel(e, b, k, q, d, m) : r(b.crop, !0) && (p =
                    c.isInsidePlot(k.x, k.y) && c.isInsidePlot(k.x + q.width, k.y + q.height)), b.shape && !u)) e[m ? "attr" : "animate"]({ anchorX: g ? c.plotWidth - a.plotY : a.plotX, anchorY: g ? c.plotHeight - a.plotX : a.plotY }); p || (e.attr({ y: -9999 }), e.placed = !1)
        }; p.prototype.justifyDataLabel = function (a, e, b, d, m, k) {
            var c = this.chart, g = e.align, l = e.verticalAlign, h, q, n = a.box ? 0 : a.padding || 0; h = b.x + n; 0 > h && ("right" === g ? e.align = "left" : e.x = -h, q = !0); h = b.x + d.width - n; h > c.plotWidth && ("left" === g ? e.align = "right" : e.x = c.plotWidth - h, q = !0); h = b.y + n; 0 > h && ("bottom" ===
                l ? e.verticalAlign = "top" : e.y = -h, q = !0); h = b.y + d.height - n; h > c.plotHeight && ("top" === l ? e.verticalAlign = "bottom" : e.y = c.plotHeight - h, q = !0); q && (a.placed = !k, a.align(e, null, m)); return q
        }; k.pie && (k.pie.prototype.dataLabelPositioners = {
            radialDistributionY: function (a) { return a.top + a.distributeBox.pos }, radialDistributionX: function (a, e, b, d) { return a.getX(b < e.top + 2 || b > e.bottom - 2 ? d : b, e.half, e) }, justify: function (a, e, b) { return b[0] + (a.half ? -1 : 1) * (e + a.labelDistance) }, alignToPlotEdges: function (a, e, b, d) {
                a = a.getBBox().width;
                return e ? a + d : b - a - d
            }, alignToConnectors: function (a, e, b, d) { var c = 0, g; a.forEach(function (a) { g = a.dataLabel.getBBox().width; g > c && (c = g) }); return e ? c + d : b - c - d }
        }, k.pie.prototype.drawDataLabels = function () {
            var c = this, e = c.data, b, d = c.chart, m = c.options.dataLabels, k = m.connectorPadding, h = r(m.connectorWidth, 1), n = d.plotWidth, l = d.plotHeight, v = d.plotLeft, t = Math.round(d.chartWidth / 3), w, f = c.center, y = f[2] / 2, z = f[1], J, H, K, M, Q = [[], []], I, N, P, R, S = [0, 0, 0, 0], O = c.dataLabelPositioners; c.visible && (m.enabled || c._hasPointLabels) && (e.forEach(function (a) {
            a.dataLabel &&
                a.visible && a.dataLabel.shortened && (a.dataLabel.attr({ width: "auto" }).css({ width: "auto", textOverflow: "clip" }), a.dataLabel.shortened = !1)
            }), p.prototype.drawDataLabels.apply(c), e.forEach(function (a) {
            a.dataLabel && (a.visible ? (Q[a.half].push(a), a.dataLabel._pos = null, !B(m.style.width) && !B(a.options.dataLabels && a.options.dataLabels.style && a.options.dataLabels.style.width) && a.dataLabel.getBBox().width > t && (a.dataLabel.css({ width: .7 * t }), a.dataLabel.shortened = !0)) : (a.dataLabel = a.dataLabel.destroy(), a.dataLabels &&
                1 === a.dataLabels.length && delete a.dataLabels))
            }), Q.forEach(function (e, g) {
                var h, q, u = e.length, x = [], p; if (u) for (c.sortByAngle(e, g - .5), 0 < c.maxLabelDistance && (h = Math.max(0, z - y - c.maxLabelDistance), q = Math.min(z + y + c.maxLabelDistance, d.plotHeight), e.forEach(function (a) { 0 < a.labelDistance && a.dataLabel && (a.top = Math.max(0, z - y - a.labelDistance), a.bottom = Math.min(z + y + a.labelDistance, d.plotHeight), p = a.dataLabel.getBBox().height || 21, a.distributeBox = { target: a.labelPosition.natural.y - a.top + p / 2, size: p, rank: a.y }, x.push(a.distributeBox)) }),
                    h = q + p - h, a.distribute(x, h, h / 5)), R = 0; R < u; R++) {
                        b = e[R]; K = b.labelPosition; J = b.dataLabel; P = !1 === b.visible ? "hidden" : "inherit"; N = h = K.natural.y; x && B(b.distributeBox) && (void 0 === b.distributeBox.pos ? P = "hidden" : (M = b.distributeBox.size, N = O.radialDistributionY(b))); delete b.positionIndex; if (m.justify) I = O.justify(b, y, f); else switch (m.alignTo) { case "connectors": I = O.alignToConnectors(e, g, n, v); break; case "plotEdges": I = O.alignToPlotEdges(J, g, n, v); break; default: I = O.radialDistributionX(c, b, N, h) }J._attr = {
                            visibility: P,
                            align: K.alignment
                        }; J._pos = { x: I + m.x + ({ left: k, right: -k }[K.alignment] || 0), y: N + m.y - 10 }; K.final.x = I; K.final.y = N; r(m.crop, !0) && (H = J.getBBox().width, h = null, I - H < k && 1 === g ? (h = Math.round(H - I + k), S[3] = Math.max(h, S[3])) : I + H > n - k && 0 === g && (h = Math.round(I + H - n + k), S[1] = Math.max(h, S[1])), 0 > N - M / 2 ? S[0] = Math.max(Math.round(-N + M / 2), S[0]) : N + M / 2 > l && (S[2] = Math.max(Math.round(N + M / 2 - l), S[2])), J.sideOverflow = h)
                }
            }), 0 === C(S) || this.verifyDataLabelOverflow(S)) && (this.placeDataLabels(), h && this.points.forEach(function (a) {
                var b; w = a.connector;
                if ((J = a.dataLabel) && J._pos && a.visible && 0 < a.labelDistance) { P = J._attr.visibility; if (b = !w) a.connector = w = d.renderer.path().addClass("highcharts-data-label-connector  highcharts-color-" + a.colorIndex + (a.className ? " " + a.className : "")).add(c.dataLabelsGroup), d.styledMode || w.attr({ "stroke-width": h, stroke: m.connectorColor || a.color || "#666666" }); w[b ? "attr" : "animate"]({ d: a.getConnectorPath() }); w.attr("visibility", P) } else w && (a.connector = w.destroy())
            }))
        }, k.pie.prototype.placeDataLabels = function () {
            this.points.forEach(function (a) {
                var c =
                    a.dataLabel; c && a.visible && ((a = c._pos) ? (c.sideOverflow && (c._attr.width = c.getBBox().width - c.sideOverflow, c.css({ width: c._attr.width + "px", textOverflow: (this.options.dataLabels.style || {}).textOverflow || "ellipsis" }), c.shortened = !0), c.attr(c._attr), c[c.moved ? "animate" : "attr"](a), c.moved = !0) : c && c.attr({ y: -9999 }))
            }, this)
        }, k.pie.prototype.alignDataLabel = w, k.pie.prototype.verifyDataLabelOverflow = function (a) {
            var c = this.center, b = this.options, e = b.center, d = b.minSize || 80, m, k = null !== b.size; k || (null !== e[0] ? m = Math.max(c[2] -
                Math.max(a[1], a[3]), d) : (m = Math.max(c[2] - a[1] - a[3], d), c[0] += (a[3] - a[1]) / 2), null !== e[1] ? m = Math.max(Math.min(m, c[2] - Math.max(a[0], a[2])), d) : (m = Math.max(Math.min(m, c[2] - a[0] - a[2]), d), c[1] += (a[0] - a[2]) / 2), m < c[2] ? (c[2] = m, c[3] = Math.min(v(b.innerSize || 0, m), m), this.translate(c), this.drawDataLabels && this.drawDataLabels()) : k = !0); return k
        }); k.column && (k.column.prototype.alignDataLabel = function (a, e, b, d, m) {
            var c = this.chart.inverted, g = a.series, k = a.dlBox || a.shapeArgs, l = r(a.below, a.plotY > r(this.translatedThreshold,
                g.yAxis.len)), h = r(b.inside, !!this.options.stacking); k && (d = t(k), 0 > d.y && (d.height += d.y, d.y = 0), k = d.y + d.height - g.yAxis.len, 0 < k && (d.height -= k), c && (d = { x: g.yAxis.len - d.y - d.height, y: g.xAxis.len - d.x - d.width, width: d.height, height: d.width }), h || (c ? (d.x += l ? 0 : d.width, d.width = 0) : (d.y += l ? d.height : 0, d.height = 0))); b.align = r(b.align, !c || h ? "center" : l ? "right" : "left"); b.verticalAlign = r(b.verticalAlign, c || h ? "middle" : l ? "top" : "bottom"); p.prototype.alignDataLabel.call(this, a, e, b, d, m); a.isLabelJustified && a.contrastColor && e.css({ color: a.contrastColor })
        })
    })(H);
    (function (a) {
        var z = a.Chart, C = a.isArray, B = a.objectEach, h = a.pick, d = a.addEvent, t = a.fireEvent; d(z, "render", function () {
            var a = []; (this.labelCollectors || []).forEach(function (d) { a = a.concat(d()) }); (this.yAxis || []).forEach(function (d) { d.options.stackLabels && !d.options.stackLabels.allowOverlap && B(d.stacks, function (d) { B(d, function (d) { a.push(d.label) }) }) }); (this.series || []).forEach(function (d) {
                var r = d.options.dataLabels; d.visible && (!1 !== r.enabled || d._hasPointLabels) && d.points.forEach(function (d) {
                d.visible && (C(d.dataLabels) ?
                    d.dataLabels : d.dataLabel ? [d.dataLabel] : []).forEach(function (k) { var n = k.options; k.labelrank = h(n.labelrank, d.labelrank, d.shapeArgs && d.shapeArgs.height); n.allowOverlap || a.push(k) })
                })
            }); this.hideOverlappingLabels(a)
        }); z.prototype.hideOverlappingLabels = function (a) {
            var d = this, h = a.length, p = d.renderer, k, n, e, m, c, g, b = function (a, b, c, e, d, l, g, m) { return !(d > a + c || d + g < a || l > b + e || l + m < b) }; e = function (a) {
                var b, c, e, d = a.box ? 0 : a.padding || 0; e = 0; if (a && (!a.alignAttr || a.placed)) return b = a.alignAttr || { x: a.attr("x"), y: a.attr("y") },
                    c = a.parentGroup, a.width || (e = a.getBBox(), a.width = e.width, a.height = e.height, e = p.fontMetrics(null, a.element).h), { x: b.x + (c.translateX || 0) + d, y: b.y + (c.translateY || 0) + d - e, width: a.width - 2 * d, height: a.height - 2 * d }
            }; for (n = 0; n < h; n++)if (k = a[n]) k.oldOpacity = k.opacity, k.newOpacity = 1, k.absoluteBox = e(k); a.sort(function (a, b) { return (b.labelrank || 0) - (a.labelrank || 0) }); for (n = 0; n < h; n++)for (g = (e = a[n]) && e.absoluteBox, k = n + 1; k < h; ++k)if (c = (m = a[k]) && m.absoluteBox, g && c && e !== m && 0 !== e.newOpacity && 0 !== m.newOpacity && (c = b(g.x, g.y,
                g.width, g.height, c.x, c.y, c.width, c.height))) (e.labelrank < m.labelrank ? e : m).newOpacity = 0; a.forEach(function (a) { var b, c; a && (c = a.newOpacity, a.oldOpacity !== c && (a.alignAttr && a.placed ? (c ? a.show(!0) : b = function () { a.hide() }, a.alignAttr.opacity = c, a[a.isOld ? "animate" : "attr"](a.alignAttr, null, b), t(d, "afterHideOverlappingLabels")) : a.attr({ opacity: c })), a.isOld = !0) })
        }
    })(H); (function (a) {
        var z = a.addEvent, C = a.Chart, B = a.createElement, h = a.css, d = a.defaultOptions, t = a.defaultPlotOptions, w = a.extend, r = a.fireEvent, v = a.hasTouch,
        p = a.isObject, k = a.Legend, n = a.merge, e = a.pick, m = a.Point, c = a.Series, g = a.seriesTypes, b = a.svg, q; q = a.TrackerMixin = {
            drawTrackerPoint: function () {
                var a = this, b = a.chart, c = b.pointer, e = function (a) { var b = c.getPointFromEvent(a); void 0 !== b && (c.isDirectTouch = !0, b.onMouseOver(a)) }; a.points.forEach(function (a) { a.graphic && (a.graphic.element.point = a); a.dataLabel && (a.dataLabel.div ? a.dataLabel.div.point = a : a.dataLabel.element.point = a) }); a._hasTracking || (a.trackerGroups.forEach(function (d) {
                    if (a[d]) {
                        a[d].addClass("highcharts-tracker").on("mouseover",
                            e).on("mouseout", function (a) { c.onTrackerMouseOut(a) }); if (v) a[d].on("touchstart", e); !b.styledMode && a.options.cursor && a[d].css(h).css({ cursor: a.options.cursor })
                    }
                }), a._hasTracking = !0); r(this, "afterDrawTracker")
            }, drawTrackerGraph: function () {
                var a = this, c = a.options, e = c.trackByArea, d = [].concat(e ? a.areaPath : a.graphPath), l = d.length, g = a.chart, m = g.pointer, k = g.renderer, f = g.options.tooltip.snap, h = a.tracker, n, q = function () { if (g.hoverSeries !== a) a.onMouseOver() }, p = "rgba(192,192,192," + (b ? .0001 : .002) + ")"; if (l && !e) for (n =
                    l + 1; n--;)"M" === d[n] && d.splice(n + 1, 0, d[n + 1] - f, d[n + 2], "L"), (n && "M" === d[n] || n === l) && d.splice(n, 0, "L", d[n - 2] + f, d[n - 1]); h ? h.attr({ d: d }) : a.graph && (a.tracker = k.path(d).attr({ visibility: a.visible ? "visible" : "hidden", zIndex: 2 }).addClass(e ? "highcharts-tracker-area" : "highcharts-tracker-line").add(a.group), g.styledMode || a.tracker.attr({ "stroke-linejoin": "round", stroke: p, fill: e ? p : "none", "stroke-width": a.graph.strokeWidth() + (e ? 0 : 2 * f) }), [a.tracker, a.markerGroup].forEach(function (a) {
                        a.addClass("highcharts-tracker").on("mouseover",
                            q).on("mouseout", function (a) { m.onTrackerMouseOut(a) }); c.cursor && !g.styledMode && a.css({ cursor: c.cursor }); if (v) a.on("touchstart", q)
                    })); r(this, "afterDrawTracker")
            }
        }; g.column && (g.column.prototype.drawTracker = q.drawTrackerPoint); g.pie && (g.pie.prototype.drawTracker = q.drawTrackerPoint); g.scatter && (g.scatter.prototype.drawTracker = q.drawTrackerPoint); w(k.prototype, {
            setItemEvents: function (a, b, c) {
                var e = this, d = e.chart.renderer.boxWrapper, g = "highcharts-legend-" + (a instanceof m ? "point" : "series") + "-active", k = e.chart.styledMode;
                (c ? b : a.legendGroup).on("mouseover", function () { a.setState("hover"); d.addClass(g); k || b.css(e.options.itemHoverStyle) }).on("mouseout", function () { e.styledMode || b.css(n(a.visible ? e.itemStyle : e.itemHiddenStyle)); d.removeClass(g); a.setState() }).on("click", function (b) { var c = function () { a.setVisible && a.setVisible() }; d.removeClass(g); b = { browserEvent: b }; a.firePointEvent ? a.firePointEvent("legendItemClick", b, c) : r(a, "legendItemClick", b, c) })
            }, createCheckboxForItem: function (a) {
            a.checkbox = B("input", {
                type: "checkbox",
                className: "highcharts-legend-checkbox", checked: a.selected, defaultChecked: a.selected
            }, this.options.itemCheckboxStyle, this.chart.container); z(a.checkbox, "click", function (b) { r(a.series || a, "checkboxClick", { checked: b.target.checked, item: a }, function () { a.select() }) })
            }
        }); w(C.prototype, {
            showResetZoom: function () {
                function a() { b.zoomOut() } var b = this, c = d.lang, e = b.options.chart.resetZoomButton, g = e.theme, m = g.states, k = "chart" === e.relativeTo ? null : "plotBox"; r(this, "beforeShowResetZoom", null, function () {
                b.resetZoomButton =
                    b.renderer.button(c.resetZoom, null, null, a, g, m && m.hover).attr({ align: e.position.align, title: c.resetZoomTitle }).addClass("highcharts-reset-zoom").add().align(e.position, !1, k)
                })
            }, zoomOut: function () { r(this, "selection", { resetSelection: !0 }, this.zoom) }, zoom: function (a) {
                var b, c = this.pointer, d = !1, g; !a || a.resetSelection ? (this.axes.forEach(function (a) { b = a.zoom() }), c.initiated = !1) : a.xAxis.concat(a.yAxis).forEach(function (a) { var e = a.axis; c[e.isXAxis ? "zoomX" : "zoomY"] && (b = e.zoom(a.min, a.max), e.displayBtn && (d = !0)) });
                g = this.resetZoomButton; d && !g ? this.showResetZoom() : !d && p(g) && (this.resetZoomButton = g.destroy()); b && this.redraw(e(this.options.chart.animation, a && a.animation, 100 > this.pointCount))
            }, pan: function (a, b) {
                var c = this, e = c.hoverPoints, d; e && e.forEach(function (a) { a.setState() }); ("xy" === b ? [1, 0] : [1]).forEach(function (b) {
                    b = c[b ? "xAxis" : "yAxis"][0]; var e = b.horiz, g = a[e ? "chartX" : "chartY"], e = e ? "mouseDownX" : "mouseDownY", f = c[e], l = (b.pointRange || 0) / 2, m = b.reversed && !c.inverted || !b.reversed && c.inverted ? -1 : 1, k = b.getExtremes(),
                        h = b.toValue(f - g, !0) + l * m, m = b.toValue(f + b.len - g, !0) - l * m, n = m < h, f = n ? m : h, h = n ? h : m, m = Math.min(k.dataMin, l ? k.min : b.toValue(b.toPixels(k.min) - b.minPixelPadding)), l = Math.max(k.dataMax, l ? k.max : b.toValue(b.toPixels(k.max) + b.minPixelPadding)), n = m - f; 0 < n && (h += n, f = m); n = h - l; 0 < n && (h = l, f -= n); b.series.length && f !== k.min && h !== k.max && (b.setExtremes(f, h, !1, !1, { trigger: "pan" }), d = !0); c[e] = g
                }); d && c.redraw(!1); h(c.container, { cursor: "move" })
            }
        }); w(m.prototype, {
            select: function (a, b) {
                var c = this, d = c.series, g = d.chart; a = e(a, !c.selected);
                c.firePointEvent(a ? "select" : "unselect", { accumulate: b }, function () { c.selected = c.options.selected = a; d.options.data[d.data.indexOf(c)] = c.options; c.setState(a && "select"); b || g.getSelectedPoints().forEach(function (a) { a.selected && a !== c && (a.selected = a.options.selected = !1, d.options.data[d.data.indexOf(a)] = a.options, a.setState(""), a.firePointEvent("unselect")) }) })
            }, onMouseOver: function (a) {
                var b = this.series.chart, c = b.pointer; a = a ? c.normalize(a) : c.getChartCoordinatesFromPoint(this, b.inverted); c.runPointActions(a,
                    this)
            }, onMouseOut: function () { var a = this.series.chart; this.firePointEvent("mouseOut"); (a.hoverPoints || []).forEach(function (a) { a.setState() }); a.hoverPoints = a.hoverPoint = null }, importEvents: function () { if (!this.hasImportedEvents) { var b = this, c = n(b.series.options.point, b.options).events; b.events = c; a.objectEach(c, function (a, c) { z(b, c, a) }); this.hasImportedEvents = !0 } }, setState: function (a, b) {
                var c = Math.floor(this.plotX), d = this.plotY, g = this.series, m = g.options.states[a || "normal"] || {}, k = t[g.type].marker && g.options.marker,
                h = k && !1 === k.enabled, f = k && k.states && k.states[a || "normal"] || {}, n = !1 === f.enabled, q = g.stateMarkerGraphic, u = this.marker || {}, p = g.chart, v = g.halo, x, z = k && g.markerAttribs; a = a || ""; if (!(a === this.state && !b || this.selected && "select" !== a || !1 === m.enabled || a && (n || h && !1 === f.enabled) || a && u.states && u.states[a] && !1 === u.states[a].enabled)) {
                    z && (x = g.markerAttribs(this, a)); if (this.graphic) this.state && this.graphic.removeClass("highcharts-point-" + this.state), a && this.graphic.addClass("highcharts-point-" + a), p.styledMode || this.graphic.animate(g.pointAttribs(this,
                        a), e(p.options.chart.animation, m.animation)), x && this.graphic.animate(x, e(p.options.chart.animation, f.animation, k.animation)), q && q.hide(); else { if (a && f) { k = u.symbol || g.symbol; q && q.currentSymbol !== k && (q = q.destroy()); if (q) q[b ? "animate" : "attr"]({ x: x.x, y: x.y }); else k && (g.stateMarkerGraphic = q = p.renderer.symbol(k, x.x, x.y, x.width, x.height).add(g.markerGroup), q.currentSymbol = k); !p.styledMode && q && q.attr(g.pointAttribs(this, a)) } q && (q[a && p.isInsidePlot(c, d, p.inverted) ? "show" : "hide"](), q.element.point = this) } (c =
                            m.halo) && c.size ? (v || (g.halo = v = p.renderer.path().add((this.graphic || q).parentGroup)), v.show()[b ? "animate" : "attr"]({ d: this.haloPath(c.size) }), v.attr({ "class": "highcharts-halo highcharts-color-" + e(this.colorIndex, g.colorIndex) + (this.className ? " " + this.className : ""), zIndex: -1 }), v.point = this, p.styledMode || v.attr(w({ fill: this.color || g.color, "fill-opacity": c.opacity }, c.attributes))) : v && v.point && v.point.haloPath && v.animate({ d: v.point.haloPath(0) }, null, v.hide); this.state = a; r(this, "afterSetState")
                }
            }, haloPath: function (a) {
                return this.series.chart.renderer.symbols.circle(Math.floor(this.plotX) -
                    a, this.plotY - a, 2 * a, 2 * a)
            }
        }); w(c.prototype, {
            onMouseOver: function () { var a = this.chart, b = a.hoverSeries; if (b && b !== this) b.onMouseOut(); this.options.events.mouseOver && r(this, "mouseOver"); this.setState("hover"); a.hoverSeries = this }, onMouseOut: function () { var a = this.options, b = this.chart, c = b.tooltip, e = b.hoverPoint; b.hoverSeries = null; if (e) e.onMouseOut(); this && a.events.mouseOut && r(this, "mouseOut"); !c || this.stickyTracking || c.shared && !this.noSharedTooltip || c.hide(); this.setState() }, setState: function (a) {
                var b = this,
                c = b.options, d = b.graph, g = c.states, m = c.lineWidth, c = 0; a = a || ""; if (b.state !== a && ([b.group, b.markerGroup, b.dataLabelsGroup].forEach(function (c) { c && (b.state && c.removeClass("highcharts-series-" + b.state), a && c.addClass("highcharts-series-" + a)) }), b.state = a, !(b.chart.styledMode || g[a] && !1 === g[a].enabled) && (a && (m = g[a].lineWidth || m + (g[a].lineWidthPlus || 0)), d && !d.dashstyle))) for (m = { "stroke-width": m }, d.animate(m, e(g[a || "normal"] && g[a || "normal"].animation, b.chart.options.chart.animation)); b["zone-graph-" + c];)b["zone-graph-" +
                    c].attr(m), c += 1
            }, setVisible: function (a, b) {
                var c = this, e = c.chart, d = c.legendItem, g, m = e.options.chart.ignoreHiddenSeries, k = c.visible; g = (c.visible = a = c.options.visible = c.userOptions.visible = void 0 === a ? !k : a) ? "show" : "hide";["group", "dataLabelsGroup", "markerGroup", "tracker", "tt"].forEach(function (a) { if (c[a]) c[a][g]() }); if (e.hoverSeries === c || (e.hoverPoint && e.hoverPoint.series) === c) c.onMouseOut(); d && e.legend.colorizeItem(c, a); c.isDirty = !0; c.options.stacking && e.series.forEach(function (a) {
                    a.options.stacking &&
                    a.visible && (a.isDirty = !0)
                }); c.linkedSeries.forEach(function (b) { b.setVisible(a, !1) }); m && (e.isDirtyBox = !0); r(c, g); !1 !== b && e.redraw()
            }, show: function () { this.setVisible(!0) }, hide: function () { this.setVisible(!1) }, select: function (a) { this.selected = a = this.options.selected = void 0 === a ? !this.selected : a; this.checkbox && (this.checkbox.checked = a); r(this, a ? "select" : "unselect") }, drawTracker: q.drawTrackerGraph
        })
    })(H); (function (a) {
        var z = a.Chart, C = a.isArray, B = a.isObject, h = a.pick, d = a.splat; z.prototype.setResponsive = function (d) {
            var h =
                this.options.responsive, r = [], v = this.currentResponsive; h && h.rules && h.rules.forEach(function (k) { void 0 === k._id && (k._id = a.uniqueKey()); this.matchResponsiveRule(k, r, d) }, this); var p = a.merge.apply(0, r.map(function (d) { return a.find(h.rules, function (a) { return a._id === d }).chartOptions })), r = r.toString() || void 0; r !== (v && v.ruleIds) && (v && this.update(v.undoOptions, d), r ? (this.currentResponsive = { ruleIds: r, mergedOptions: p, undoOptions: this.currentOptions(p) }, this.update(p, d)) : this.currentResponsive = void 0)
        }; z.prototype.matchResponsiveRule =
            function (a, d) { var r = a.condition; (r.callback || function () { return this.chartWidth <= h(r.maxWidth, Number.MAX_VALUE) && this.chartHeight <= h(r.maxHeight, Number.MAX_VALUE) && this.chartWidth >= h(r.minWidth, 0) && this.chartHeight >= h(r.minHeight, 0) }).call(this) && d.push(a._id) }; z.prototype.currentOptions = function (h) {
                function t(h, p, k, n) {
                    var e; a.objectEach(h, function (a, c) {
                        if (!n && -1 < ["series", "xAxis", "yAxis"].indexOf(c)) for (a = d(a), k[c] = [], e = 0; e < a.length; e++)p[c][e] && (k[c][e] = {}, t(a[e], p[c][e], k[c][e], n + 1)); else B(a) ?
                            (k[c] = C(a) ? [] : {}, t(a, p[c] || {}, k[c], n + 1)) : k[c] = p[c] || null
                    })
                } var r = {}; t(h, this.options, r, 0); return r
            }
    })(H); (function (a) {
        var z = a.addEvent, C = a.Axis, B = a.pick; z(C, "getSeriesExtremes", function () { var a = []; this.isXAxis && (this.series.forEach(function (d, h) { d.useMapGeometry && (a[h] = d.xData, d.xData = []) }), this.seriesXData = a) }); z(C, "afterGetSeriesExtremes", function () {
            var a = this.seriesXData, d, t, w; this.isXAxis && (d = B(this.dataMin, Number.MAX_VALUE), t = B(this.dataMax, -Number.MAX_VALUE), this.series.forEach(function (h, v) {
            h.useMapGeometry &&
                (d = Math.min(d, B(h.minX, d)), t = Math.max(t, B(h.maxX, t)), h.xData = a[v], w = !0)
            }), w && (this.dataMin = d, this.dataMax = t), delete this.seriesXData)
        }); z(C, "afterSetAxisTranslation", function () {
            var a = this.chart, d; d = a.plotWidth / a.plotHeight; var a = a.xAxis[0], t; "yAxis" === this.coll && void 0 !== a.transA && this.series.forEach(function (a) { a.preserveAspectRatio && (t = !0) }); if (t && (this.transA = a.transA = Math.min(this.transA, a.transA), d /= (a.max - a.min) / (this.max - this.min), d = 1 > d ? this : a, a = (d.max - d.min) * d.transA, d.pixelPadding = d.len -
                a, d.minPixelPadding = d.pixelPadding / 2, a = d.fixTo)) { a = a[1] - d.toValue(a[0], !0); a *= d.transA; if (Math.abs(a) > d.minPixelPadding || d.min === d.dataMin && d.max === d.dataMax) a = 0; d.minPixelPadding -= a }
        }); z(C, "render", function () { this.fixTo = null })
    })(H); (function (a) {
        var z = a.addEvent, C = a.Axis, B = a.Chart, h = a.color, d, t = a.extend, w = a.isNumber, r = a.Legend, v = a.LegendSymbolMixin, p = a.noop, k = a.merge, n = a.pick; a.ColorAxis || (d = a.ColorAxis = function () { this.init.apply(this, arguments) }, t(d.prototype, C.prototype), t(d.prototype, {
            defaultColorAxisOptions: {
                lineWidth: 0,
                minPadding: 0, maxPadding: 0, gridLineWidth: 1, tickPixelInterval: 72, startOnTick: !0, endOnTick: !0, offset: 0, marker: { animation: { duration: 50 }, width: .01, color: "#999999" }, labels: { overflow: "justify", rotation: 0 }, minColor: "#e6ebf5", maxColor: "#003399", tickLength: 5, showInLegend: !0
            }, keepProps: ["legendGroup", "legendItemHeight", "legendItemWidth", "legendItem", "legendSymbol"].concat(C.prototype.keepProps), init: function (a, d) {
                var c = "vertical" !== a.options.legend.layout, e; this.coll = "colorAxis"; e = k(this.defaultColorAxisOptions,
                    { side: c ? 2 : 1, reversed: !c }, d, { opposite: !c, showEmpty: !1, title: null, visible: a.options.legend.enabled }); C.prototype.init.call(this, a, e); d.dataClasses && this.initDataClasses(d); this.initStops(); this.horiz = c; this.zoomEnabled = !1; this.defaultLegendLength = 200
            }, initDataClasses: function (a) {
                var e = this.chart, c, d = 0, b = e.options.chart.colorCount, n = this.options, p = a.dataClasses.length; this.dataClasses = c = []; this.legendItems = []; a.dataClasses.forEach(function (a, g) {
                    a = k(a); c.push(a); if (e.styledMode || !a.color) "category" ===
                        n.dataClassColor ? (e.styledMode || (g = e.options.colors, b = g.length, a.color = g[d]), a.colorIndex = d, d++ , d === b && (d = 0)) : a.color = h(n.minColor).tweenTo(h(n.maxColor), 2 > p ? .5 : g / (p - 1))
                })
            }, setTickPositions: function () { if (!this.dataClasses) return C.prototype.setTickPositions.call(this) }, initStops: function () { this.stops = this.options.stops || [[0, this.options.minColor], [1, this.options.maxColor]]; this.stops.forEach(function (a) { a.color = h(a[1]) }) }, setOptions: function (a) {
                C.prototype.setOptions.call(this, a); this.options.crosshair =
                    this.options.marker
            }, setAxisSize: function () { var a = this.legendSymbol, d = this.chart, c = d.options.legend || {}, g, b; a ? (this.left = c = a.attr("x"), this.top = g = a.attr("y"), this.width = b = a.attr("width"), this.height = a = a.attr("height"), this.right = d.chartWidth - c - b, this.bottom = d.chartHeight - g - a, this.len = this.horiz ? b : a, this.pos = this.horiz ? c : g) : this.len = (this.horiz ? c.symbolWidth : c.symbolHeight) || this.defaultLegendLength }, normalizedValue: function (a) {
            this.isLog && (a = this.val2lin(a)); return 1 - (this.max - a) / (this.max - this.min ||
                1)
            }, toColor: function (a, d) { var c = this.stops, e, b, m = this.dataClasses, k, h; if (m) for (h = m.length; h--;) { if (k = m[h], e = k.from, c = k.to, (void 0 === e || a >= e) && (void 0 === c || a <= c)) { b = k.color; d && (d.dataClass = h, d.colorIndex = k.colorIndex); break } } else { a = this.normalizedValue(a); for (h = c.length; h-- && !(a > c[h][0]);); e = c[h] || c[h + 1]; c = c[h + 1] || e; a = 1 - (c[0] - a) / (c[0] - e[0] || 1); b = e.color.tweenTo(c.color, a) } return b }, getOffset: function () {
                var a = this.legendGroup, d = this.chart.axisOffset[this.side]; a && (this.axisParent = a, C.prototype.getOffset.call(this),
                    this.added || (this.added = !0, this.labelLeft = 0, this.labelRight = this.width), this.chart.axisOffset[this.side] = d)
            }, setLegendColor: function () { var a, d = this.reversed; a = d ? 1 : 0; d = d ? 0 : 1; a = this.horiz ? [a, 0, d, 0] : [0, d, 0, a]; this.legendColor = { linearGradient: { x1: a[0], y1: a[1], x2: a[2], y2: a[3] }, stops: this.stops } }, drawLegendSymbol: function (a, d) {
                var c = a.padding, e = a.options, b = this.horiz, m = n(e.symbolWidth, b ? this.defaultLegendLength : 12), k = n(e.symbolHeight, b ? 12 : this.defaultLegendLength), h = n(e.labelPadding, b ? 16 : 30), e = n(e.itemDistance,
                    10); this.setLegendColor(); d.legendSymbol = this.chart.renderer.rect(0, a.baseline - 11, m, k).attr({ zIndex: 1 }).add(d.legendGroup); this.legendItemWidth = m + c + (b ? e : h); this.legendItemHeight = k + c + (b ? h : 0)
            }, setState: function (a) { this.series.forEach(function (e) { e.setState(a) }) }, visible: !0, setVisible: p, getSeriesExtremes: function () {
                var a = this.series, d = a.length; this.dataMin = Infinity; for (this.dataMax = -Infinity; d--;)a[d].getExtremes(), void 0 !== a[d].valueMin && (this.dataMin = Math.min(this.dataMin, a[d].valueMin), this.dataMax =
                    Math.max(this.dataMax, a[d].valueMax))
            }, drawCrosshair: function (a, d) { var c = d && d.plotX, e = d && d.plotY, b, k = this.pos, m = this.len; d && (b = this.toPixels(d[d.series.colorKey]), b < k ? b = k - 2 : b > k + m && (b = k + m + 2), d.plotX = b, d.plotY = this.len - b, C.prototype.drawCrosshair.call(this, a, d), d.plotX = c, d.plotY = e, this.cross && !this.cross.addedToColorAxis && this.legendGroup && (this.cross.addClass("highcharts-coloraxis-marker").add(this.legendGroup), this.cross.addedToColorAxis = !0, this.chart.styledMode || this.cross.attr({ fill: this.crosshair.color }))) },
            getPlotLinePath: function (a, d, c, g, b) { return w(b) ? this.horiz ? ["M", b - 4, this.top - 6, "L", b + 4, this.top - 6, b, this.top, "Z"] : ["M", this.left, b, "L", this.left - 6, b + 6, this.left - 6, b - 6, "Z"] : C.prototype.getPlotLinePath.call(this, a, d, c, g) }, update: function (a, d) {
                var c = this.chart, e = c.legend; this.series.forEach(function (a) { a.isDirtyData = !0 }); a.dataClasses && e.allItems && (e.allItems.forEach(function (a) { a.isDataClass && a.legendGroup && a.legendGroup.destroy() }), c.isDirtyLegend = !0); c.options[this.coll] = k(this.userOptions, a); C.prototype.update.call(this,
                    a, d); this.legendItem && (this.setLegendColor(), e.colorizeItem(this, !0))
            }, remove: function () { this.legendItem && this.chart.legend.destroyItem(this); C.prototype.remove.call(this) }, getDataClassLegendSymbols: function () {
                var e = this, d = this.chart, c = this.legendItems, g = d.options.legend, b = g.valueDecimals, k = g.valueSuffix || "", h; c.length || this.dataClasses.forEach(function (g, m) {
                    var n = !0, l = g.from, q = g.to; h = ""; void 0 === l ? h = "\x3c " : void 0 === q && (h = "\x3e "); void 0 !== l && (h += a.numberFormat(l, b) + k); void 0 !== l && void 0 !== q && (h +=
                        " - "); void 0 !== q && (h += a.numberFormat(q, b) + k); c.push(t({ chart: d, name: h, options: {}, drawLegendSymbol: v.drawRectangle, visible: !0, setState: p, isDataClass: !0, setVisible: function () { n = this.visible = !n; e.series.forEach(function (a) { a.points.forEach(function (a) { a.dataClass === m && a.setVisible(n) }) }); d.legend.colorizeItem(this, n) } }, g))
                }); return c
            }, name: ""
        }), ["fill", "stroke"].forEach(function (e) { a.Fx.prototype[e + "Setter"] = function () { this.elem.attr(e, h(this.start).tweenTo(h(this.end), this.pos), null, !0) } }), z(B, "afterGetAxes",
            function () { var a = this.options.colorAxis; this.colorAxis = []; a && new d(this, a) }), z(r, "afterGetAllItems", function (e) { var d = [], c = this.chart.colorAxis[0]; c && c.options && c.options.showInLegend && (c.options.dataClasses ? d = c.getDataClassLegendSymbols() : d.push(c), c.series.forEach(function (c) { a.erase(e.allItems, c) })); for (c = d.length; c--;)e.allItems.unshift(d[c]) }), z(r, "afterColorizeItem", function (a) { a.visible && a.item.legendColor && a.item.legendSymbol.attr({ fill: a.item.legendColor }) }), z(r, "afterUpdate", function (a,
                d, c) { this.chart.colorAxis[0] && this.chart.colorAxis[0].update({}, c) }))
    })(H); (function (a) {
        var z = a.defined, C = a.noop, B = a.seriesTypes; a.colorPointMixin = { isValid: function () { return null !== this.value && Infinity !== this.value && -Infinity !== this.value }, setVisible: function (a) { var d = this, h = a ? "show" : "hide"; d.visible = !!a;["graphic", "dataLabel"].forEach(function (a) { if (d[a]) d[a][h]() }) }, setState: function (h) { a.Point.prototype.setState.call(this, h); this.graphic && this.graphic.attr({ zIndex: "hover" === h ? 1 : 0 }) } }; a.colorSeriesMixin =
            {
                pointArrayMap: ["value"], axisTypes: ["xAxis", "yAxis", "colorAxis"], optionalAxis: "colorAxis", trackerGroups: ["group", "markerGroup", "dataLabelsGroup"], getSymbol: C, parallelArrays: ["x", "y", "value"], colorKey: "value", pointAttribs: B.column.prototype.pointAttribs, translateColors: function () { var a = this, d = this.options.nullColor, t = this.colorAxis, w = this.colorKey; this.data.forEach(function (h) { var v = h[w]; if (v = h.options.color || (h.isNull ? d : t && void 0 !== v ? t.toColor(v, h) : h.color || a.color)) h.color = v }) }, colorAttribs: function (a) {
                    var d =
                        {}; z(a.color) && (d[this.colorProp || "fill"] = a.color); return d
                }
            }
    })(H); (function (a) {
        function z(a) { a && (a.preventDefault && a.preventDefault(), a.stopPropagation && a.stopPropagation(), a.cancelBubble = !0) } function C(a) { this.init(a) } var B = a.addEvent, h = a.Chart, d = a.doc, t = a.extend, w = a.merge, r = a.pick; C.prototype.init = function (a) { this.chart = a; a.mapNavButtons = [] }; C.prototype.update = function (d) {
            var h = this.chart, k = h.options.mapNavigation, n, e, m, c, g, b = function (a) { this.handler.call(h, a); z(a) }, q = h.mapNavButtons; d && (k = h.options.mapNavigation =
                w(h.options.mapNavigation, d)); for (; q.length;)q.pop().destroy(); r(k.enableButtons, k.enabled) && !h.renderer.forExport && a.objectEach(k.buttons, function (a, d) {
                    n = w(k.buttonOptions, a); h.styledMode || (e = n.theme, e.style = w(n.theme.style, n.style), c = (m = e.states) && m.hover, g = m && m.select); a = h.renderer.button(n.text, 0, 0, b, e, c, g, 0, "zoomIn" === d ? "topbutton" : "bottombutton").addClass("highcharts-map-navigation highcharts-" + { zoomIn: "zoom-in", zoomOut: "zoom-out" }[d]).attr({
                        width: n.width, height: n.height, title: h.options.lang[d],
                        padding: n.padding, zIndex: 5
                    }).add(); a.handler = n.onclick; a.align(t(n, { width: a.width, height: 2 * a.height }), null, n.alignTo); B(a.element, "dblclick", z); q.push(a)
                }); this.updateEvents(k)
        }; C.prototype.updateEvents = function (a) {
            var h = this.chart; r(a.enableDoubleClickZoom, a.enabled) || a.enableDoubleClickZoomTo ? this.unbindDblClick = this.unbindDblClick || B(h.container, "dblclick", function (a) { h.pointer.onContainerDblClick(a) }) : this.unbindDblClick && (this.unbindDblClick = this.unbindDblClick()); r(a.enableMouseWheelZoom, a.enabled) ?
                this.unbindMouseWheel = this.unbindMouseWheel || B(h.container, void 0 === d.onmousewheel ? "DOMMouseScroll" : "mousewheel", function (a) { h.pointer.onContainerMouseWheel(a); z(a); return !1 }) : this.unbindMouseWheel && (this.unbindMouseWheel = this.unbindMouseWheel())
        }; t(h.prototype, {
            fitToBox: function (a, d) { [["x", "width"], ["y", "height"]].forEach(function (k) { var h = k[0]; k = k[1]; a[h] + a[k] > d[h] + d[k] && (a[k] > d[k] ? (a[k] = d[k], a[h] = d[h]) : a[h] = d[h] + d[k] - a[k]); a[k] > d[k] && (a[k] = d[k]); a[h] < d[h] && (a[h] = d[h]) }); return a }, mapZoom: function (a,
                d, k, h, e) {
                    var m = this.xAxis[0], c = m.max - m.min, g = r(d, m.min + c / 2), b = c * a, c = this.yAxis[0], n = c.max - c.min, p = r(k, c.min + n / 2), n = n * a, g = this.fitToBox({ x: g - b * (h ? (h - m.pos) / m.len : .5), y: p - n * (e ? (e - c.pos) / c.len : .5), width: b, height: n }, { x: m.dataMin, y: c.dataMin, width: m.dataMax - m.dataMin, height: c.dataMax - c.dataMin }), b = g.x <= m.dataMin && g.width >= m.dataMax - m.dataMin && g.y <= c.dataMin && g.height >= c.dataMax - c.dataMin; h && (m.fixTo = [h - m.pos, d]); e && (c.fixTo = [e - c.pos, k]); void 0 === a || b ? (m.setExtremes(void 0, void 0, !1), c.setExtremes(void 0,
                        void 0, !1)) : (m.setExtremes(g.x, g.x + g.width, !1), c.setExtremes(g.y, g.y + g.height, !1)); this.redraw()
            }
        }); B(h, "beforeRender", function () { this.mapNavigation = new C(this); this.mapNavigation.update() }); a.MapNavigation = C
    })(H); (function (a) {
        var z = a.extend, C = a.pick, B = a.Pointer; a = a.wrap; z(B.prototype, {
            onContainerDblClick: function (a) {
                var d = this.chart; a = this.normalize(a); d.options.mapNavigation.enableDoubleClickZoomTo ? d.pointer.inClass(a.target, "highcharts-tracker") && d.hoverPoint && d.hoverPoint.zoomTo() : d.isInsidePlot(a.chartX -
                    d.plotLeft, a.chartY - d.plotTop) && d.mapZoom(.5, d.xAxis[0].toValue(a.chartX), d.yAxis[0].toValue(a.chartY), a.chartX, a.chartY)
            }, onContainerMouseWheel: function (a) { var d = this.chart, h; a = this.normalize(a); h = a.detail || -(a.wheelDelta / 120); d.isInsidePlot(a.chartX - d.plotLeft, a.chartY - d.plotTop) && d.mapZoom(Math.pow(d.options.mapNavigation.mouseWheelSensitivity, h), d.xAxis[0].toValue(a.chartX), d.yAxis[0].toValue(a.chartY), a.chartX, a.chartY) }
        }); a(B.prototype, "zoomOption", function (a) {
            var d = this.chart.options.mapNavigation;
            C(d.enableTouchZoom, d.enabled) && (this.chart.options.chart.pinchType = "xy"); a.apply(this, [].slice.call(arguments, 1))
        }); a(B.prototype, "pinchTranslate", function (a, d, t, w, r, v, p) { a.call(this, d, t, w, r, v, p); "map" === this.chart.options.chart.type && this.hasZoom && (a = w.scaleX > w.scaleY, this.pinchTranslateDirection(!a, d, t, w, r, v, p, a ? w.scaleX : w.scaleY)) })
    })(H); (function (a) {
        var z = a.colorPointMixin, C = a.extend, B = a.isNumber, h = a.merge, d = a.noop, t = a.pick, w = a.isArray, r = a.Point, v = a.Series, p = a.seriesType, k = a.seriesTypes, n = a.splat;
        p("map", "scatter", { animation: !1, dataLabels: { crop: !1, formatter: function () { return this.point.value }, inside: !0, overflow: !1, padding: 0, verticalAlign: "middle" }, marker: null, nullColor: "#f7f7f7", stickyTracking: !1, tooltip: { followPointer: !0, pointFormat: "{point.name}: {point.value}\x3cbr/\x3e" }, turboThreshold: 0, allAreas: !0, borderColor: "#cccccc", borderWidth: 1, joinBy: "hc-key", states: { hover: { halo: null, brightness: .2 }, normal: { animation: !0 }, select: { color: "#cccccc" } } }, h(a.colorSeriesMixin, {
            type: "map", getExtremesFromAll: !0,
            useMapGeometry: !0, forceDL: !0, searchPoint: d, directTouch: !0, preserveAspectRatio: !0, pointArrayMap: ["value"], getBox: function (d) {
                var e = Number.MAX_VALUE, c = -e, g = e, b = -e, k = e, h = e, n = this.xAxis, p = this.yAxis, r; (d || []).forEach(function (d) {
                    if (d.path) {
                    "string" === typeof d.path && (d.path = a.splitPath(d.path)); var l = d.path || [], m = l.length, n = !1, f = -e, q = e, p = -e, u = e, v = d.properties; if (!d._foundBox) {
                        for (; m--;)B(l[m]) && (n ? (f = Math.max(f, l[m]), q = Math.min(q, l[m])) : (p = Math.max(p, l[m]), u = Math.min(u, l[m])), n = !n); d._midX = q + (f - q) * t(d.middleX,
                            v && v["hc-middle-x"], .5); d._midY = u + (p - u) * t(d.middleY, v && v["hc-middle-y"], .5); d._maxX = f; d._minX = q; d._maxY = p; d._minY = u; d.labelrank = t(d.labelrank, (f - q) * (p - u)); d._foundBox = !0
                    } c = Math.max(c, d._maxX); g = Math.min(g, d._minX); b = Math.max(b, d._maxY); k = Math.min(k, d._minY); h = Math.min(d._maxX - d._minX, d._maxY - d._minY, h); r = !0
                    }
                }); r && (this.minY = Math.min(k, t(this.minY, e)), this.maxY = Math.max(b, t(this.maxY, -e)), this.minX = Math.min(g, t(this.minX, e)), this.maxX = Math.max(c, t(this.maxX, -e)), n && void 0 === n.options.minRange && (n.minRange =
                    Math.min(5 * h, (this.maxX - this.minX) / 5, n.minRange || e)), p && void 0 === p.options.minRange && (p.minRange = Math.min(5 * h, (this.maxY - this.minY) / 5, p.minRange || e)))
            }, getExtremes: function () { v.prototype.getExtremes.call(this, this.valueData); this.chart.hasRendered && this.isDirtyData && this.getBox(this.options.data); this.valueMin = this.dataMin; this.valueMax = this.dataMax; this.dataMin = this.minY; this.dataMax = this.maxY }, translatePath: function (a) {
                var d = !1, c = this.xAxis, e = this.yAxis, b = c.min, k = c.transA, c = c.minPixelPadding, h =
                    e.min, n = e.transA, e = e.minPixelPadding, p, r = []; if (a) for (p = a.length; p--;)B(a[p]) ? (r[p] = d ? (a[p] - b) * k + c : (a[p] - h) * n + e, d = !d) : r[p] = a[p]; return r
            }, setData: function (d, k, c, g) {
                var b = this.options, e = this.chart.options.chart, m = e && e.map, p = b.mapData, r = b.joinBy, t = null === r, l = b.keys || this.pointArrayMap, A = [], z = {}, C = this.chart.mapTransforms; !p && m && (p = "string" === typeof m ? a.maps[m] : m); t && (r = "_i"); r = this.joinBy = n(r); r[1] || (r[1] = r[0]); d && d.forEach(function (c, e) {
                    var f = 0; if (B(c)) d[e] = { value: c }; else if (w(c)) {
                    d[e] = {}; !b.keys &&
                        c.length > l.length && "string" === typeof c[0] && (d[e]["hc-key"] = c[0], ++f); for (var g = 0; g < l.length; ++g, ++f)l[g] && void 0 !== c[f] && (0 < l[g].indexOf(".") ? a.Point.prototype.setNestedProperty(d[e], c[f], l[g]) : d[e][l[g]] = c[f])
                    } t && (d[e]._i = e)
                }); this.getBox(d); (this.chart.mapTransforms = C = e && e.mapTransforms || p && p["hc-transform"] || C) && a.objectEach(C, function (a) { a.rotation && (a.cosAngle = Math.cos(a.rotation), a.sinAngle = Math.sin(a.rotation)) }); if (p) {
                "FeatureCollection" === p.type && (this.mapTitle = p.title, p = a.geojson(p, this.type,
                    this)); this.mapData = p; this.mapMap = {}; for (C = 0; C < p.length; C++)e = p[C], m = e.properties, e._i = C, r[0] && m && m[r[0]] && (e[r[0]] = m[r[0]]), z[e[r[0]]] = e; this.mapMap = z; d && r[1] && d.forEach(function (a) { z[a[r[1]]] && A.push(z[a[r[1]]]) }); b.allAreas ? (this.getBox(p), d = d || [], r[1] && d.forEach(function (a) { A.push(a[r[1]]) }), A = "|" + A.map(function (a) { return a && a[r[0]] }).join("|") + "|", p.forEach(function (a) { r[0] && -1 !== A.indexOf("|" + a[r[0]] + "|") || (d.push(h(a, { value: null })), g = !1) })) : this.getBox(A)
                } v.prototype.setData.call(this, d, k,
                    c, g)
            }, drawGraph: d, drawDataLabels: d, doFullTranslate: function () { return this.isDirtyData || this.chart.isResizing || this.chart.renderer.isVML || !this.baseTrans }, translate: function () { var a = this, d = a.xAxis, c = a.yAxis, g = a.doFullTranslate(); a.generatePoints(); a.data.forEach(function (b) { b.plotX = d.toPixels(b._midX, !0); b.plotY = c.toPixels(b._midY, !0); g && (b.shapeType = "path", b.shapeArgs = { d: a.translatePath(b.path) }) }); a.translateColors() }, pointAttribs: function (a, d) {
                d = a.series.chart.styledMode ? this.colorAttribs(a) : k.column.prototype.pointAttribs.call(this,
                    a, d); d["stroke-width"] = t(a.options[this.pointAttrToOptions && this.pointAttrToOptions["stroke-width"] || "borderWidth"], "inherit"); return d
            }, drawPoints: function () {
                var a = this, d = a.xAxis, c = a.yAxis, g = a.group, b = a.chart, h = b.renderer, n, p, r, v, l = this.baseTrans, t, w, z, f, y; a.transformGroup || (a.transformGroup = h.g().attr({ scaleX: 1, scaleY: 1 }).add(g), a.transformGroup.survive = !0); a.doFullTranslate() ? (b.hasRendered && !b.styledMode && a.points.forEach(function (b) { b.shapeArgs && (b.shapeArgs.fill = a.pointAttribs(b, b.state).fill) }),
                    a.group = a.transformGroup, k.column.prototype.drawPoints.apply(a), a.group = g, a.points.forEach(function (c) { c.graphic && (c.name && c.graphic.addClass("highcharts-name-" + c.name.replace(/ /g, "-").toLowerCase()), c.properties && c.properties["hc-key"] && c.graphic.addClass("highcharts-key-" + c.properties["hc-key"].toLowerCase()), b.styledMode && c.graphic.css(a.pointAttribs(c, c.selected && "select"))) }), this.baseTrans = {
                        originX: d.min - d.minPixelPadding / d.transA, originY: c.min - c.minPixelPadding / c.transA + (c.reversed ? 0 : c.len /
                            c.transA), transAX: d.transA, transAY: c.transA
                    }, this.transformGroup.animate({ translateX: 0, translateY: 0, scaleX: 1, scaleY: 1 })) : (n = d.transA / l.transAX, p = c.transA / l.transAY, r = d.toPixels(l.originX, !0), v = c.toPixels(l.originY, !0), .99 < n && 1.01 > n && .99 < p && 1.01 > p && (p = n = 1, r = Math.round(r), v = Math.round(v)), t = this.transformGroup, b.renderer.globalAnimation ? (w = t.attr("translateX"), z = t.attr("translateY"), f = t.attr("scaleX"), y = t.attr("scaleY"), t.attr({ animator: 0 }).animate({ animator: 1 }, {
                        step: function (a, b) {
                            t.attr({
                                translateX: w +
                                    (r - w) * b.pos, translateY: z + (v - z) * b.pos, scaleX: f + (n - f) * b.pos, scaleY: y + (p - y) * b.pos
                            })
                        }
                    })) : t.attr({ translateX: r, translateY: v, scaleX: n, scaleY: p })); b.styledMode || g.element.setAttribute("stroke-width", (a.options[a.pointAttrToOptions && a.pointAttrToOptions["stroke-width"] || "borderWidth"] || 1) / (n || 1)); this.drawMapDataLabels()
            }, drawMapDataLabels: function () { v.prototype.drawDataLabels.call(this); this.dataLabelsGroup && this.dataLabelsGroup.clip(this.chart.clipRect) }, render: function () {
                var a = this, d = v.prototype.render;
                a.chart.renderer.isVML && 3E3 < a.data.length ? setTimeout(function () { d.call(a) }) : d.call(a)
            }, animate: function (a) { var d = this.options.animation, c = this.group, e = this.xAxis, b = this.yAxis, k = e.pos, h = b.pos; this.chart.renderer.isSVG && (!0 === d && (d = { duration: 1E3 }), a ? c.attr({ translateX: k + e.len / 2, translateY: h + b.len / 2, scaleX: .001, scaleY: .001 }) : (c.animate({ translateX: k, translateY: h, scaleX: 1, scaleY: 1 }, d), this.animate = null)) }, animateDrilldown: function (a) {
                var d = this.chart.plotBox, c = this.chart.drilldownLevels[this.chart.drilldownLevels.length -
                    1], e = c.bBox, b = this.chart.options.drilldown.animation; a || (a = Math.min(e.width / d.width, e.height / d.height), c.shapeArgs = { scaleX: a, scaleY: a, translateX: e.x, translateY: e.y }, this.points.forEach(function (a) { a.graphic && a.graphic.attr(c.shapeArgs).animate({ scaleX: 1, scaleY: 1, translateX: 0, translateY: 0 }, b) }), this.animate = null)
            }, drawLegendSymbol: a.LegendSymbolMixin.drawRectangle, animateDrillupFrom: function (a) { k.column.prototype.animateDrillupFrom.call(this, a) }, animateDrillupTo: function (a) {
                k.column.prototype.animateDrillupTo.call(this,
                    a)
            }
        }), C({
            applyOptions: function (a, d) { a = r.prototype.applyOptions.call(this, a, d); d = this.series; var c = d.joinBy; d.mapData && ((c = void 0 !== a[c[1]] && d.mapMap[a[c[1]]]) ? (d.xyFromShape && (a.x = c._midX, a.y = c._midY), C(a, c)) : a.value = a.value || null); return a }, onMouseOver: function (d) { a.clearTimeout(this.colorInterval); if (null !== this.value || this.series.options.nullInteraction) r.prototype.onMouseOver.call(this, d); else this.series.onMouseOut(d) }, zoomTo: function () {
                var a = this.series; a.xAxis.setExtremes(this._minX, this._maxX,
                    !1); a.yAxis.setExtremes(this._minY, this._maxY, !1); a.chart.redraw()
            }
        }, z))
    })(H); (function (a) { var z = a.seriesType, C = a.seriesTypes; z("mapline", "map", { lineWidth: 1, fillColor: "none" }, { type: "mapline", colorProp: "stroke", pointAttrToOptions: { stroke: "color", "stroke-width": "lineWidth" }, pointAttribs: function (a, h) { a = C.map.prototype.pointAttribs.call(this, a, h); a.fill = this.options.fillColor; return a }, drawLegendSymbol: C.line.prototype.drawLegendSymbol }) })(H); (function (a) {
        var z = a.merge, C = a.Point; a = a.seriesType; a("mappoint",
            "scatter", { dataLabels: { enabled: !0, formatter: function () { return this.point.name }, crop: !1, defer: !1, overflow: !1, style: { color: "#000000" } } }, { type: "mappoint", forceDL: !0 }, { applyOptions: function (a, h) { a = void 0 !== a.lat && void 0 !== a.lon ? z(a, this.series.chart.fromLatLonToPoint(a)) : a; return C.prototype.applyOptions.call(this, a, h) } })
    })(H); (function (a) {
        var z = a.Series, C = a.Legend, B = a.Chart, h = a.addEvent, d = a.wrap, t = a.color, w = a.isNumber, r = a.numberFormat, v = a.objectEach, p = a.merge, k = a.noop, n = a.pick, e = a.stableSort, m = a.setOptions,
        c = a.arrayMin, g = a.arrayMax; m({ legend: { bubbleLegend: { borderColor: void 0, borderWidth: 2, className: void 0, color: void 0, connectorClassName: void 0, connectorColor: void 0, connectorDistance: 60, connectorWidth: 1, enabled: !1, labels: { className: void 0, allowOverlap: !1, format: "", formatter: void 0, align: "right", style: { fontSize: 10, color: void 0 }, x: 0, y: 0 }, maxSize: 60, minSize: 10, legendIndex: 0, ranges: { value: void 0, borderColor: void 0, color: void 0, connectorColor: void 0 }, sizeBy: "area", sizeByAbsoluteValue: !1, zIndex: 1, zThreshold: 0 } } });
        a.BubbleLegend = function (a, c) { this.init(a, c) }; a.BubbleLegend.prototype = {
            init: function (a, c) { this.options = a; this.visible = !0; this.chart = c.chart; this.legend = c }, setState: k, addToLegend: function (a) { a.splice(this.options.legendIndex, 0, this) }, drawLegendSymbol: function (a) {
                var b = this.chart, c = this.options, d = n(a.options.itemDistance, 20), g, k = c.ranges; g = c.connectorDistance; this.fontMetrics = b.renderer.fontMetrics(c.labels.style.fontSize.toString() + "px"); k && k.length && w(k[0].value) ? (e(k, function (a, b) {
                    return b.value -
                        a.value
                }), this.ranges = k, this.setOptions(), this.render(), b = this.getMaxLabelSize(), k = this.ranges[0].radius, a = 2 * k, g = g - k + b.width, g = 0 < g ? g : 0, this.maxLabel = b, this.movementX = "left" === c.labels.align ? g : 0, this.legendItemWidth = a + g + d, this.legendItemHeight = a + this.fontMetrics.h / 2) : a.options.bubbleLegend.autoRanges = !0
            }, setOptions: function () {
                var a = this, c = a.ranges, d = a.options, e = a.chart.series[d.seriesIndex], g = a.legend.baseline, k = { "z-index": d.zIndex, "stroke-width": d.borderWidth }, l = { "z-index": d.zIndex, "stroke-width": d.connectorWidth },
                h = a.getLabelStyles(), m = e.options.marker.fillOpacity, r = a.chart.styledMode; c.forEach(function (b, q) { r || (k.stroke = n(b.borderColor, d.borderColor, e.color), k.fill = n(b.color, d.color, 1 !== m ? t(e.color).setOpacity(m).get("rgba") : e.color), l.stroke = n(b.connectorColor, d.connectorColor, e.color)); c[q].radius = a.getRangeRadius(b.value); c[q] = p(c[q], { center: c[0].radius - c[q].radius + g }); r || p(!0, c[q], { bubbleStyle: p(!1, k), connectorStyle: p(!1, l), labelStyle: h }) })
            }, getLabelStyles: function () {
                var a = this.options, c = {}, d = "left" ===
                    a.labels.align, e = this.legend.options.rtl; v(a.labels.style, function (a, b) { "color" !== b && "fontSize" !== b && "z-index" !== b && (c[b] = a) }); return p(!1, c, { "font-size": a.labels.style.fontSize, fill: n(a.labels.style.color, "#000000"), "z-index": a.zIndex, align: e || d ? "right" : "left" })
            }, getRangeRadius: function (a) { var b = this.options; return this.chart.series[this.options.seriesIndex].getRadius.call(this, b.ranges[b.ranges.length - 1].value, b.ranges[0].value, b.minSize, b.maxSize, a) }, render: function () {
                var a = this, c = a.chart.renderer,
                d = a.options.zThreshold; a.symbols || (a.symbols = { connectors: [], bubbleItems: [], labels: [] }); a.legendSymbol = c.g("bubble-legend"); a.legendItem = c.g("bubble-legend-item"); a.legendSymbol.translateX = 0; a.legendSymbol.translateY = 0; a.ranges.forEach(function (b) { b.value >= d && a.renderRange(b) }); a.legendSymbol.add(a.legendItem); a.legendItem.add(a.legendGroup); a.hideOverlappingLabels()
            }, renderRange: function (a) {
                var b = this.options, c = b.labels, d = this.chart.renderer, e = this.symbols, g = e.labels, l = a.center, k = Math.abs(a.radius),
                h = b.connectorDistance, m = c.align, f = c.style.fontSize, h = this.legend.options.rtl || "left" === m ? -h : h, c = b.connectorWidth, n = this.ranges[0].radius, p = l - k - b.borderWidth / 2 + c / 2, r, f = f / 2 - (this.fontMetrics.h - f) / 2, v = d.styledMode; "center" === m && (h = 0, b.connectorDistance = 0, a.labelStyle.align = "center"); m = p + b.labels.y; r = n + h + b.labels.x; e.bubbleItems.push(d.circle(n, l + ((p % 1 ? 1 : .5) - (c % 2 ? 0 : .5)), k).attr(v ? {} : a.bubbleStyle).addClass((v ? "highcharts-color-" + this.options.seriesIndex + " " : "") + "highcharts-bubble-legend-symbol " + (b.className ||
                    "")).add(this.legendSymbol)); e.connectors.push(d.path(d.crispLine(["M", n, p, "L", n + h, p], b.connectorWidth)).attr(v ? {} : a.connectorStyle).addClass((v ? "highcharts-color-" + this.options.seriesIndex + " " : "") + "highcharts-bubble-legend-connectors " + (b.connectorClassName || "")).add(this.legendSymbol)); a = d.text(this.formatLabel(a), r, m + f).attr(v ? {} : a.labelStyle).addClass("highcharts-bubble-legend-labels " + (b.labels.className || "")).add(this.legendSymbol); g.push(a); a.placed = !0; a.alignAttr = { x: r, y: m + f }
            }, getMaxLabelSize: function () {
                var a,
                c; this.symbols.labels.forEach(function (b) { c = b.getBBox(!0); a = a ? c.width > a.width ? c : a : c }); return a || {}
            }, formatLabel: function (b) { var c = this.options, d = c.labels.formatter; return (c = c.labels.format) ? a.format(c, b) : d ? d.call(b) : r(b.value, 1) }, hideOverlappingLabels: function () { var a = this.chart, c = this.symbols; !this.options.labels.allowOverlap && c && (a.hideOverlappingLabels(c.labels), c.labels.forEach(function (a, b) { a.newOpacity ? a.newOpacity !== a.oldOpacity && c.connectors[b].show() : c.connectors[b].hide() })) }, getRanges: function () {
                var a =
                    this.legend.bubbleLegend, d, e = a.options.ranges, k, h = Number.MAX_VALUE, m = -Number.MAX_VALUE; a.chart.series.forEach(function (a) { a.isBubble && !a.ignoreSeries && (k = a.zData.filter(w), k.length && (h = n(a.options.zMin, Math.min(h, Math.max(c(k), !1 === a.options.displayNegative ? a.options.zThreshold : -Number.MAX_VALUE))), m = n(a.options.zMax, Math.max(m, g(k))))) }); d = h === m ? [{ value: m }] : [{ value: h }, { value: (h + m) / 2 }, { value: m, autoRanges: !0 }]; e.length && e[0].radius && d.reverse(); d.forEach(function (a, b) { e && e[b] && (d[b] = p(!1, e[b], a)) });
                return d
            }, predictBubbleSizes: function () { var a = this.chart, c = this.fontMetrics, d = a.legend.options, e = "horizontal" === d.layout, g = e ? a.legend.lastLineHeight : 0, k = a.plotSizeX, l = a.plotSizeY, h = a.series[this.options.seriesIndex], a = Math.ceil(h.minPxSize), m = Math.ceil(h.maxPxSize), h = h.options.maxSize, n = Math.min(l, k); if (d.floating || !/%$/.test(h)) c = m; else if (h = parseFloat(h), c = (n + g - c.h / 2) * h / 100 / (h / 100 + 1), e && l - c >= k || !e && k - c >= l) c = m; return [a, Math.ceil(c)] }, updateRanges: function (a, c) {
                var b = this.legend.options.bubbleLegend;
                b.minSize = a; b.maxSize = c; b.ranges = this.getRanges()
            }, correctSizes: function () { var a = this.legend, c = this.chart.series[this.options.seriesIndex]; 1 < Math.abs(Math.ceil(c.maxPxSize) - this.options.maxSize) && (this.updateRanges(this.options.minSize, c.maxPxSize), a.render()) }
        }; h(a.Legend, "afterGetAllItems", function (b) {
            var c = this.bubbleLegend, d = this.options, e = d.bubbleLegend, g = this.chart.getVisibleBubbleSeriesIndex(); c && c.ranges && c.ranges.length && (e.ranges.length && (e.autoRanges = e.ranges[0].autoRanges ? !0 : !1), this.destroyItem(c));
            0 <= g && d.enabled && e.enabled && (e.seriesIndex = g, this.bubbleLegend = new a.BubbleLegend(e, this), this.bubbleLegend.addToLegend(b.allItems))
        }); B.prototype.getVisibleBubbleSeriesIndex = function () { for (var a = this.series, c = 0; c < a.length;) { if (a[c] && a[c].isBubble && a[c].visible && a[c].zData.length) return c; c++ } return -1 }; C.prototype.getLinesHeights = function () {
            var a = this.allItems, c = [], d, e = a.length, g, k = 0; for (g = 0; g < e; g++)if (a[g].legendItemHeight && (a[g].itemHeight = a[g].legendItemHeight), a[g] === a[e - 1] || a[g + 1] && a[g]._legendItemPos[1] !==
                a[g + 1]._legendItemPos[1]) { c.push({ height: 0 }); d = c[c.length - 1]; for (k; k <= g; k++)a[k].itemHeight > d.height && (d.height = a[k].itemHeight); d.step = g } return c
        }; C.prototype.retranslateItems = function (a) {
            var b, c, d, e = this.options.rtl, g = 0; this.allItems.forEach(function (k, h) {
                b = k.legendGroup.translateX; c = k._legendItemPos[1]; if ((d = k.movementX) || e && k.ranges) d = e ? b - k.options.maxSize / 2 : b + d, k.legendGroup.attr({ translateX: d }); h > a[g].step && g++; k.legendGroup.attr({ translateY: Math.round(c + a[g].height / 2) }); k._legendItemPos[1] =
                    c + a[g].height / 2
            })
        }; h(z, "legendItemClick", function () { var a = this.chart, c = this.visible, d = this.chart.legend; d && d.bubbleLegend && (this.visible = !c, this.ignoreSeries = c, a = 0 <= a.getVisibleBubbleSeriesIndex(), d.bubbleLegend.visible !== a && (d.update({ bubbleLegend: { enabled: a } }), d.bubbleLegend.visible = a), this.visible = c) }); d(B.prototype, "drawChartBox", function (a, c, d) {
            var b = this.legend, e = 0 <= this.getVisibleBubbleSeriesIndex(), g; b && b.options.enabled && b.bubbleLegend && b.options.bubbleLegend.autoRanges && e ? (g = b.bubbleLegend.options,
                e = b.bubbleLegend.predictBubbleSizes(), b.bubbleLegend.updateRanges(e[0], e[1]), g.placed || (b.group.placed = !1, b.allItems.forEach(function (a) { a.legendGroup.translateY = null })), b.render(), this.getMargins(), this.axes.forEach(function (a) { a.render(); g.placed || (a.setScale(), a.updateNames(), v(a.ticks, function (a) { a.isNew = !0; a.isNewLabel = !0 })) }), g.placed = !0, this.getMargins(), a.call(this, c, d), b.bubbleLegend.correctSizes(), b.retranslateItems(b.getLinesHeights())) : (a.call(this, c, d), b && b.options.enabled && b.bubbleLegend &&
                    (b.render(), b.retranslateItems(b.getLinesHeights())))
        })
    })(H); (function (a) {
        var z = a.arrayMax, C = a.arrayMin, B = a.Axis, h = a.color, d = a.isNumber, t = a.noop, w = a.pick, r = a.pInt, v = a.Point, p = a.Series, k = a.seriesType, n = a.seriesTypes; k("bubble", "scatter", {
            dataLabels: { formatter: function () { return this.point.z }, inside: !0, verticalAlign: "middle" }, animationLimit: 250, marker: { lineColor: null, lineWidth: 1, fillOpacity: .5, radius: null, states: { hover: { radiusPlus: 0 } }, symbol: "circle" }, minSize: 8, maxSize: "20%", softThreshold: !1, states: { hover: { halo: { size: 5 } } },
            tooltip: { pointFormat: "({point.x}, {point.y}), Size: {point.z}" }, turboThreshold: 0, zThreshold: 0, zoneAxis: "z"
        }, {
            pointArrayMap: ["y", "z"], parallelArrays: ["x", "y", "z"], trackerGroups: ["group", "dataLabelsGroup"], specialGroup: "group", bubblePadding: !0, zoneAxis: "z", directTouch: !0, isBubble: !0, pointAttribs: function (a, d) { var c = this.options.marker.fillOpacity; a = p.prototype.pointAttribs.call(this, a, d); 1 !== c && (a.fill = h(a.fill).setOpacity(c).get("rgba")); return a }, getRadii: function (a, d, c) {
                var e, b = this.zData, k = c.minPxSize,
                h = c.maxPxSize, m = [], n; e = 0; for (c = b.length; e < c; e++)n = b[e], m.push(this.getRadius(a, d, k, h, n)); this.radii = m
            }, getRadius: function (a, k, c, g, b) { var e = this.options, h = "width" !== e.sizeBy, m = e.zThreshold, n = k - a; e.sizeByAbsoluteValue && null !== b && (b = Math.abs(b - m), n = Math.max(k - m, Math.abs(a - m)), a = 0); d(b) ? b < a ? c = c / 2 - 1 : (a = 0 < n ? (b - a) / n : .5, h && 0 <= a && (a = Math.sqrt(a)), c = Math.ceil(c + a * (g - c)) / 2) : c = null; return c }, animate: function (a) {
            !a && this.points.length < this.options.animationLimit && (this.points.forEach(function (a) {
                var c = a.graphic,
                d; c && c.width && (d = { x: c.x, y: c.y, width: c.width, height: c.height }, c.attr({ x: a.plotX, y: a.plotY, width: 1, height: 1 }), c.animate(d, this.options.animation))
            }, this), this.animate = null)
            }, translate: function () { var e, k = this.data, c, g, b = this.radii; n.scatter.prototype.translate.call(this); for (e = k.length; e--;)c = k[e], g = b ? b[e] : 0, d(g) && g >= this.minPxSize / 2 ? (c.marker = a.extend(c.marker, { radius: g, width: 2 * g, height: 2 * g }), c.dlBox = { x: c.plotX - g, y: c.plotY - g, width: 2 * g, height: 2 * g }) : c.shapeArgs = c.plotY = c.dlBox = void 0 }, alignDataLabel: n.column.prototype.alignDataLabel,
                buildKDTree: t, applyZones: t
            }, { haloPath: function (a) { return v.prototype.haloPath.call(this, 0 === a ? 0 : (this.marker ? this.marker.radius || 0 : 0) + a) }, ttBelow: !1 }); B.prototype.beforePadding = function () {
                var e = this, k = this.len, c = this.chart, g = 0, b = k, h = this.isXAxis, n = h ? "xData" : "yData", p = this.min, v = {}, t = Math.min(c.plotWidth, c.plotHeight), l = Number.MAX_VALUE, A = -Number.MAX_VALUE, B = this.max - p, G = k / B, f = []; this.series.forEach(function (b) {
                    var d = b.options; !b.bubblePadding || !b.visible && c.options.chart.ignoreHiddenSeries || (e.allowZoomOutside =
                        !0, f.push(b), h && (["minSize", "maxSize"].forEach(function (a) { var b = d[a], c = /%$/.test(b), b = r(b); v[a] = c ? t * b / 100 : b }), b.minPxSize = v.minSize, b.maxPxSize = Math.max(v.maxSize, v.minSize), b = b.zData.filter(a.isNumber), b.length && (l = w(d.zMin, Math.min(l, Math.max(C(b), !1 === d.displayNegative ? d.zThreshold : -Number.MAX_VALUE))), A = w(d.zMax, Math.max(A, z(b))))))
                }); f.forEach(function (a) {
                    var c = a[n], f = c.length, k; h && a.getRadii(l, A, a); if (0 < B) for (; f--;)d(c[f]) && e.dataMin <= c[f] && c[f] <= e.dataMax && (k = a.radii[f], g = Math.min((c[f] - p) *
                        G - k, g), b = Math.max((c[f] - p) * G + k, b))
                }); f.length && 0 < B && !this.isLog && (b -= k, G *= (k + Math.max(0, g) - Math.min(b, k)) / k, [["min", "userMin", g], ["max", "userMax", b]].forEach(function (a) { void 0 === w(e.options[a[0]], e[a[1]]) && (e[a[0]] += a[2] / G) }))
            }
    })(H); (function (a) {
        var z = a.merge, C = a.Point, B = a.seriesType, h = a.seriesTypes; h.bubble && B("mapbubble", "bubble", { animationLimit: 500, tooltip: { pointFormat: "{point.name}: {point.z}" } }, {
            xyFromShape: !0, type: "mapbubble", pointArrayMap: ["z"], getMapData: h.map.prototype.getMapData, getBox: h.map.prototype.getBox,
            setData: h.map.prototype.setData
        }, { applyOptions: function (a, t) { return a && void 0 !== a.lat && void 0 !== a.lon ? C.prototype.applyOptions.call(this, z(a, this.series.chart.fromLatLonToPoint(a)), t) : h.map.prototype.pointClass.prototype.applyOptions.call(this, a, t) }, isValid: function () { return "number" === typeof this.z }, ttBelow: !1 })
    })(H); (function (a) {
        var z = a.colorPointMixin, C = a.merge, B = a.noop, h = a.pick, d = a.Series, t = a.seriesType, w = a.seriesTypes; t("heatmap", "scatter", {
            animation: !1, borderWidth: 0, nullColor: "#f7f7f7", dataLabels: {
                formatter: function () { return this.point.value },
                inside: !0, verticalAlign: "middle", crop: !1, overflow: !1, padding: 0
            }, marker: null, pointRange: null, tooltip: { pointFormat: "{point.x}, {point.y}: {point.value}\x3cbr/\x3e" }, states: { hover: { halo: !1, brightness: .2 } }
        }, C(a.colorSeriesMixin, {
            pointArrayMap: ["y", "value"], hasPointSpecificOptions: !0, getExtremesFromAll: !0, directTouch: !0, init: function () { var a; w.scatter.prototype.init.apply(this, arguments); a = this.options; a.pointRange = h(a.pointRange, a.colsize || 1); this.yAxis.axisPointRange = a.rowsize || 1 }, translate: function () {
                var a =
                    this.options, d = this.xAxis, p = this.yAxis, k = a.pointPadding || 0, n = function (a, d, c) { return Math.min(Math.max(d, a), c) }; this.generatePoints(); this.points.forEach(function (e) {
                        var m = (a.colsize || 1) / 2, c = (a.rowsize || 1) / 2, g = n(Math.round(d.len - d.translate(e.x - m, 0, 1, 0, 1)), -d.len, 2 * d.len), m = n(Math.round(d.len - d.translate(e.x + m, 0, 1, 0, 1)), -d.len, 2 * d.len), b = n(Math.round(p.translate(e.y - c, 0, 1, 0, 1)), -p.len, 2 * p.len), c = n(Math.round(p.translate(e.y + c, 0, 1, 0, 1)), -p.len, 2 * p.len), q = h(e.pointPadding, k); e.plotX = e.clientX = (g + m) /
                            2; e.plotY = (b + c) / 2; e.shapeType = "rect"; e.shapeArgs = { x: Math.min(g, m) + q, y: Math.min(b, c) + q, width: Math.abs(m - g) - 2 * q, height: Math.abs(c - b) - 2 * q }
                    }); this.translateColors()
            }, drawPoints: function () { var a = this.chart.styledMode ? "css" : "attr"; w.column.prototype.drawPoints.call(this); this.points.forEach(function (d) { d.graphic[a](this.colorAttribs(d)) }, this) }, animate: B, getBox: B, drawLegendSymbol: a.LegendSymbolMixin.drawRectangle, alignDataLabel: w.column.prototype.alignDataLabel, getExtremes: function () {
                d.prototype.getExtremes.call(this,
                    this.valueData); this.valueMin = this.dataMin; this.valueMax = this.dataMax; d.prototype.getExtremes.call(this)
            }
        }), a.extend({ haloPath: function (a) { if (!a) return []; var d = this.shapeArgs; return ["M", d.x - a, d.y - a, "L", d.x - a, d.y + d.height + a, d.x + d.width + a, d.y + d.height + a, d.x + d.width + a, d.y - a, "Z"] } }, z))
    })(H); (function (a) {
        function z(a, d) { var h, k, n, e = !1, m = a.x, c = a.y; a = 0; for (h = d.length - 1; a < d.length; h = a++)k = d[a][1] > c, n = d[h][1] > c, k !== n && m < (d[h][0] - d[a][0]) * (c - d[a][1]) / (d[h][1] - d[a][1]) + d[a][0] && (e = !e); return e } var C = a.Chart,
            B = a.extend, h = a.format, d = a.merge, t = a.win, w = a.wrap; C.prototype.transformFromLatLon = function (d, h) {
                if (void 0 === t.proj4) return a.error(21, !1, this), { x: 0, y: null }; d = t.proj4(h.crs, [d.lon, d.lat]); var p = h.cosAngle || h.rotation && Math.cos(h.rotation), k = h.sinAngle || h.rotation && Math.sin(h.rotation); d = h.rotation ? [d[0] * p + d[1] * k, -d[0] * k + d[1] * p] : d; return {
                    x: ((d[0] - (h.xoffset || 0)) * (h.scale || 1) + (h.xpan || 0)) * (h.jsonres || 1) + (h.jsonmarginX || 0), y: (((h.yoffset || 0) - d[1]) * (h.scale || 1) + (h.ypan || 0)) * (h.jsonres || 1) - (h.jsonmarginY ||
                        0)
                }
            }; C.prototype.transformToLatLon = function (d, h) { if (void 0 === t.proj4) a.error(21, !1, this); else { d = { x: ((d.x - (h.jsonmarginX || 0)) / (h.jsonres || 1) - (h.xpan || 0)) / (h.scale || 1) + (h.xoffset || 0), y: ((-d.y - (h.jsonmarginY || 0)) / (h.jsonres || 1) + (h.ypan || 0)) / (h.scale || 1) + (h.yoffset || 0) }; var p = h.cosAngle || h.rotation && Math.cos(h.rotation), k = h.sinAngle || h.rotation && Math.sin(h.rotation); h = t.proj4(h.crs, "WGS84", h.rotation ? { x: d.x * p + d.y * -k, y: d.x * k + d.y * p } : d); return { lat: h.y, lon: h.x } } }; C.prototype.fromPointToLatLon = function (d) {
                var h =
                    this.mapTransforms, p; if (h) { for (p in h) if (h.hasOwnProperty(p) && h[p].hitZone && z({ x: d.x, y: -d.y }, h[p].hitZone.coordinates[0])) return this.transformToLatLon(d, h[p]); return this.transformToLatLon(d, h["default"]) } a.error(22, !1, this)
            }; C.prototype.fromLatLonToPoint = function (d) {
                var h = this.mapTransforms, p, k; if (!h) return a.error(22, !1, this), { x: 0, y: null }; for (p in h) if (h.hasOwnProperty(p) && h[p].hitZone && (k = this.transformFromLatLon(d, h[p]), z({ x: k.x, y: -k.y }, h[p].hitZone.coordinates[0]))) return k; return this.transformFromLatLon(d,
                    h["default"])
            }; a.geojson = function (a, d, p) {
                var k = [], n = [], e = function (a) { var c, d = a.length; n.push("M"); for (c = 0; c < d; c++)1 === c && n.push("L"), n.push(a[c][0], -a[c][1]) }; d = d || "map"; a.features.forEach(function (a) {
                    var c = a.geometry, g = c.type, c = c.coordinates; a = a.properties; var b; n = []; "map" === d || "mapbubble" === d ? ("Polygon" === g ? (c.forEach(e), n.push("Z")) : "MultiPolygon" === g && (c.forEach(function (a) { a.forEach(e) }), n.push("Z")), n.length && (b = { path: n })) : "mapline" === d ? ("LineString" === g ? e(c) : "MultiLineString" === g && c.forEach(e),
                        n.length && (b = { path: n })) : "mappoint" === d && "Point" === g && (b = { x: c[0], y: -c[1] }); b && k.push(B(b, { name: a.name || a.NAME, properties: a }))
                }); p && a.copyrightShort && (p.chart.mapCredits = h(p.chart.options.credits.mapText, { geojson: a }), p.chart.mapCreditsFull = h(p.chart.options.credits.mapTextFull, { geojson: a })); return k
            }; w(C.prototype, "addCredits", function (a, h) { h = d(!0, this.options.credits, h); this.mapCredits && (h.href = null); a.call(this, h); this.credits && this.mapCreditsFull && this.credits.attr({ title: this.mapCreditsFull }) })
    })(H);
    (function (a) {
        function z(a, d, h, e, m, c, g, b) { return ["M", a + m, d, "L", a + h - c, d, "C", a + h - c / 2, d, a + h, d + c / 2, a + h, d + c, "L", a + h, d + e - g, "C", a + h, d + e - g / 2, a + h - g / 2, d + e, a + h - g, d + e, "L", a + b, d + e, "C", a + b / 2, d + e, a, d + e - b / 2, a, d + e - b, "L", a, d + m, "C", a, d + m / 2, a + m / 2, d, a + m, d, "Z"] } var C = a.Chart, B = a.defaultOptions, h = a.extend, d = a.merge, t = a.pick, w = a.Renderer, r = a.SVGRenderer, v = a.VMLRenderer; h(B.lang, { zoomIn: "Zoom in", zoomOut: "Zoom out" }); B.mapNavigation = {
            buttonOptions: {
                alignTo: "plotBox", align: "left", verticalAlign: "top", x: 0, width: 18, height: 18,
                padding: 5, style: { fontSize: "15px", fontWeight: "bold" }, theme: { "stroke-width": 1, "text-align": "center" }
            }, buttons: { zoomIn: { onclick: function () { this.mapZoom(.5) }, text: "+", y: 0 }, zoomOut: { onclick: function () { this.mapZoom(2) }, text: "-", y: 28 } }, mouseWheelSensitivity: 1.1
        }; a.splitPath = function (a) { var d; a = a.replace(/([A-Za-z])/g, " $1 "); a = a.replace(/^\s*/, "").replace(/\s*$/, ""); a = a.split(/[ ,]+/); for (d = 0; d < a.length; d++)/[a-zA-Z]/.test(a[d]) || (a[d] = parseFloat(a[d])); return a }; a.maps = {}; r.prototype.symbols.topbutton =
            function (a, d, h, e, m) { return z(a - 1, d - 1, h, e, m.r, m.r, 0, 0) }; r.prototype.symbols.bottombutton = function (a, d, h, e, m) { return z(a - 1, d - 1, h, e, 0, 0, m.r, m.r) }; w === v && ["topbutton", "bottombutton"].forEach(function (a) { v.prototype.symbols[a] = r.prototype.symbols[a] }); a.Map = a.mapChart = function (h, k, n) {
                var e = "string" === typeof h || h.nodeName, m = arguments[e ? 1 : 0], c = m, g = { endOnTick: !1, visible: !1, minPadding: 0, maxPadding: 0, startOnTick: !1 }, b, p = a.getOptions().credits; b = m.series; m.series = null; m = d({
                    chart: { panning: "xy", type: "map" }, credits: {
                        mapText: t(p.mapText,
                            ' \u00a9 \x3ca href\x3d"{geojson.copyrightUrl}"\x3e{geojson.copyrightShort}\x3c/a\x3e'), mapTextFull: t(p.mapTextFull, "{geojson.copyright}")
                    }, tooltip: { followTouchMove: !1 }, xAxis: g, yAxis: d(g, { reversed: !0 })
                }, m, { chart: { inverted: !1, alignTicks: !1 } }); m.series = c.series = b; return e ? new C(h, m, n) : new C(m, k)
            }
    })(H); return H
});
//# sourceMappingURL=highmaps.js.map