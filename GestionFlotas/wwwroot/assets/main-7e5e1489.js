(function() {
    const t = document.createElement("link").relList;
    if (t && t.supports && t.supports("modulepreload"))
        return;
    for (const r of document.querySelectorAll('link[rel="modulepreload"]'))
        i(r);
    new MutationObserver(r => {
        for (const o of r)
            if (o.type === "childList")
                for (const s of o.addedNodes)
                    s.tagName === "LINK" && s.rel === "modulepreload" && i(s)
    }
    ).observe(document, {
        childList: !0,
        subtree: !0
    });
    function n(r) {
        const o = {};
        return r.integrity && (o.integrity = r.integrity),
        r.referrerPolicy && (o.referrerPolicy = r.referrerPolicy),
        r.crossOrigin === "use-credentials" ? o.credentials = "include" : r.crossOrigin === "anonymous" ? o.credentials = "omit" : o.credentials = "same-origin",
        o
    }
    function i(r) {
        if (r.ep)
            return;
        r.ep = !0;
        const o = n(r);
        fetch(r.href, o)
    }
}
)();
var Te = !1
  , Ie = !1
  , N = []
  , $e = -1;
function zn(e) {
    qn(e)
}
function qn(e) {
    N.includes(e) || N.push(e),
    Hn()
}
function Mt(e) {
    let t = N.indexOf(e);
    t !== -1 && t > $e && N.splice(t, 1)
}
function Hn() {
    !Ie && !Te && (Te = !0,
    queueMicrotask(Wn))
}
function Wn() {
    Te = !1,
    Ie = !0;
    for (let e = 0; e < N.length; e++)
        N[e](),
        $e = e;
    N.length = 0,
    $e = -1,
    Ie = !1
}
var H, K, W, Ct, Pe = !0;
function Un(e) {
    Pe = !1,
    e(),
    Pe = !0
}
function Jn(e) {
    H = e.reactive,
    W = e.release,
    K = t => e.effect(t, {
        scheduler: n => {
            Pe ? zn(n) : n()
        }
    }),
    Ct = e.raw
}
function pt(e) {
    K = e
}
function Vn(e) {
    let t = () => {}
    ;
    return [i => {
        let r = K(i);
        return e._x_effects || (e._x_effects = new Set,
        e._x_runEffects = () => {
            e._x_effects.forEach(o => o())
        }
        ),
        e._x_effects.add(r),
        t = () => {
            r !== void 0 && (e._x_effects.delete(r),
            W(r))
        }
        ,
        r
    }
    , () => {
        t()
    }
    ]
}
function Tt(e, t) {
    let n = !0, i, r = K( () => {
        let o = e();
        JSON.stringify(o),
        n ? i = o : queueMicrotask( () => {
            t(o, i),
            i = o
        }
        ),
        n = !1
    }
    );
    return () => W(r)
}
var It = []
  , $t = []
  , Pt = [];
function Yn(e) {
    Pt.push(e)
}
function Ue(e, t) {
    typeof t == "function" ? (e._x_cleanups || (e._x_cleanups = []),
    e._x_cleanups.push(t)) : (t = e,
    $t.push(t))
}
function Rt(e) {
    It.push(e)
}
function Lt(e, t, n) {
    e._x_attributeCleanups || (e._x_attributeCleanups = {}),
    e._x_attributeCleanups[t] || (e._x_attributeCleanups[t] = []),
    e._x_attributeCleanups[t].push(n)
}
function jt(e, t) {
    e._x_attributeCleanups && Object.entries(e._x_attributeCleanups).forEach( ([n,i]) => {
        (t === void 0 || t.includes(n)) && (i.forEach(r => r()),
        delete e._x_attributeCleanups[n])
    }
    )
}
function Gn(e) {
    if (e._x_cleanups)
        for (; e._x_cleanups.length; )
            e._x_cleanups.pop()()
}
var Je = new MutationObserver(Xe)
  , Ve = !1;
function Ye() {
    Je.observe(document, {
        subtree: !0,
        childList: !0,
        attributes: !0,
        attributeOldValue: !0
    }),
    Ve = !0
}
function Nt() {
    Xn(),
    Je.disconnect(),
    Ve = !1
}
var V = [];
function Xn() {
    let e = Je.takeRecords();
    V.push( () => e.length > 0 && Xe(e));
    let t = V.length;
    queueMicrotask( () => {
        if (V.length === t)
            for (; V.length > 0; )
                V.shift()()
    }
    )
}
function x(e) {
    if (!Ve)
        return e();
    Nt();
    let t = e();
    return Ye(),
    t
}
var Ge = !1
  , pe = [];
function Zn() {
    Ge = !0
}
function Qn() {
    Ge = !1,
    Xe(pe),
    pe = []
}
function Xe(e) {
    if (Ge) {
        pe = pe.concat(e);
        return
    }
    let t = new Set
      , n = new Set
      , i = new Map
      , r = new Map;
    for (let o = 0; o < e.length; o++)
        if (!e[o].target._x_ignoreMutationObserver && (e[o].type === "childList" && (e[o].addedNodes.forEach(s => s.nodeType === 1 && t.add(s)),
        e[o].removedNodes.forEach(s => s.nodeType === 1 && n.add(s))),
        e[o].type === "attributes")) {
            let s = e[o].target
              , a = e[o].attributeName
              , l = e[o].oldValue
              , u = () => {
                i.has(s) || i.set(s, []),
                i.get(s).push({
                    name: a,
                    value: s.getAttribute(a)
                })
            }
              , c = () => {
                r.has(s) || r.set(s, []),
                r.get(s).push(a)
            }
            ;
            s.hasAttribute(a) && l === null ? u() : s.hasAttribute(a) ? (c(),
            u()) : c()
        }
    r.forEach( (o, s) => {
        jt(s, o)
    }
    ),
    i.forEach( (o, s) => {
        It.forEach(a => a(s, o))
    }
    );
    for (let o of n)
        t.has(o) || $t.forEach(s => s(o));
    t.forEach(o => {
        o._x_ignoreSelf = !0,
        o._x_ignore = !0
    }
    );
    for (let o of t)
        n.has(o) || o.isConnected && (delete o._x_ignoreSelf,
        delete o._x_ignore,
        Pt.forEach(s => s(o)),
        o._x_ignore = !0,
        o._x_ignoreSelf = !0);
    t.forEach(o => {
        delete o._x_ignoreSelf,
        delete o._x_ignore
    }
    ),
    t = null,
    n = null,
    i = null,
    r = null
}
function Ft(e) {
    return ne(z(e))
}
function te(e, t, n) {
    return e._x_dataStack = [t, ...z(n || e)],
    () => {
        e._x_dataStack = e._x_dataStack.filter(i => i !== t)
    }
}
function z(e) {
    return e._x_dataStack ? e._x_dataStack : typeof ShadowRoot == "function" && e instanceof ShadowRoot ? z(e.host) : e.parentNode ? z(e.parentNode) : []
}
function ne(e) {
    return new Proxy({
        objects: e
    },er)
}
var er = {
    ownKeys({objects: e}) {
        return Array.from(new Set(e.flatMap(t => Object.keys(t))))
    },
    has({objects: e}, t) {
        return t == Symbol.unscopables ? !1 : e.some(n => Object.prototype.hasOwnProperty.call(n, t) || Reflect.has(n, t))
    },
    get({objects: e}, t, n) {
        return t == "toJSON" ? tr : Reflect.get(e.find(i => Reflect.has(i, t)) || {}, t, n)
    },
    set({objects: e}, t, n, i) {
        const r = e.find(s => Object.prototype.hasOwnProperty.call(s, t)) || e[e.length - 1]
          , o = Object.getOwnPropertyDescriptor(r, t);
        return o != null && o.set && (o != null && o.get) ? Reflect.set(r, t, n, i) : Reflect.set(r, t, n)
    }
};
function tr() {
    return Reflect.ownKeys(this).reduce( (t, n) => (t[n] = Reflect.get(this, n),
    t), {})
}
function Dt(e) {
    let t = i => typeof i == "object" && !Array.isArray(i) && i !== null
      , n = (i, r="") => {
        Object.entries(Object.getOwnPropertyDescriptors(i)).forEach( ([o,{value: s, enumerable: a}]) => {
            if (a === !1 || s === void 0 || typeof s == "object" && s !== null && s.__v_skip)
                return;
            let l = r === "" ? o : `${r}.${o}`;
            typeof s == "object" && s !== null && s._x_interceptor ? i[o] = s.initialize(e, l, o) : t(s) && s !== i && !(s instanceof Element) && n(s, l)
        }
        )
    }
    ;
    return n(e)
}
function Bt(e, t= () => {}
) {
    let n = {
        initialValue: void 0,
        _x_interceptor: !0,
        initialize(i, r, o) {
            return e(this.initialValue, () => nr(i, r), s => Re(i, r, s), r, o)
        }
    };
    return t(n),
    i => {
        if (typeof i == "object" && i !== null && i._x_interceptor) {
            let r = n.initialize.bind(n);
            n.initialize = (o, s, a) => {
                let l = i.initialize(o, s, a);
                return n.initialValue = l,
                r(o, s, a)
            }
        } else
            n.initialValue = i;
        return n
    }
}
function nr(e, t) {
    return t.split(".").reduce( (n, i) => n[i], e)
}
function Re(e, t, n) {
    if (typeof t == "string" && (t = t.split(".")),
    t.length === 1)
        e[t[0]] = n;
    else {
        if (t.length === 0)
            throw error;
        return e[t[0]] || (e[t[0]] = {}),
        Re(e[t[0]], t.slice(1), n)
    }
}
var Kt = {};
function S(e, t) {
    Kt[e] = t
}
function Le(e, t) {
    return Object.entries(Kt).forEach( ([n,i]) => {
        let r = null;
        function o() {
            if (r)
                return r;
            {
                let[s,a] = Ut(t);
                return r = {
                    interceptor: Bt,
                    ...s
                },
                Ue(t, a),
                r
            }
        }
        Object.defineProperty(e, `$${n}`, {
            get() {
                return i(t, o())
            },
            enumerable: !1
        })
    }
    ),
    e
}
function rr(e, t, n, ...i) {
    try {
        return n(...i)
    } catch (r) {
        ee(r, e, t)
    }
}
function ee(e, t, n=void 0) {
    e = Object.assign(e ?? {
        message: "No error message given."
    }, {
        el: t,
        expression: n
    }),
    console.warn(`Alpine Expression Error: ${e.message}

${n ? 'Expression: "' + n + `"

` : ""}`, t),
    setTimeout( () => {
        throw e
    }
    , 0)
}
var fe = !0;
function kt(e) {
    let t = fe;
    fe = !1;
    let n = e();
    return fe = t,
    n
}
function F(e, t, n={}) {
    let i;
    return v(e, t)(r => i = r, n),
    i
}
function v(...e) {
    return zt(...e)
}
var zt = qt;
function ir(e) {
    zt = e
}
function qt(e, t) {
    let n = {};
    Le(n, e);
    let i = [n, ...z(e)]
      , r = typeof t == "function" ? or(i, t) : ar(i, t, e);
    return rr.bind(null, e, t, r)
}
function or(e, t) {
    return (n= () => {}
    , {scope: i={}, params: r=[]}={}) => {
        let o = t.apply(ne([i, ...e]), r);
        he(n, o)
    }
}
var Ee = {};
function sr(e, t) {
    if (Ee[e])
        return Ee[e];
    let n = Object.getPrototypeOf(async function() {}).constructor
      , i = /^[\n\s]*if.*\(.*\)/.test(e.trim()) || /^(let|const)\s/.test(e.trim()) ? `(async()=>{ ${e} })()` : e
      , o = ( () => {
        try {
            let s = new n(["__self", "scope"],`with (scope) { __self.result = ${i} }; __self.finished = true; return __self.result;`);
            return Object.defineProperty(s, "name", {
                value: `[Alpine] ${e}`
            }),
            s
        } catch (s) {
            return ee(s, t, e),
            Promise.resolve()
        }
    }
    )();
    return Ee[e] = o,
    o
}
function ar(e, t, n) {
    let i = sr(t, n);
    return (r= () => {}
    , {scope: o={}, params: s=[]}={}) => {
        i.result = void 0,
        i.finished = !1;
        let a = ne([o, ...e]);
        if (typeof i == "function") {
            let l = i(i, a).catch(u => ee(u, n, t));
            i.finished ? (he(r, i.result, a, s, n),
            i.result = void 0) : l.then(u => {
                he(r, u, a, s, n)
            }
            ).catch(u => ee(u, n, t)).finally( () => i.result = void 0)
        }
    }
}
function he(e, t, n, i, r) {
    if (fe && typeof t == "function") {
        let o = t.apply(n, i);
        o instanceof Promise ? o.then(s => he(e, s, n, i)).catch(s => ee(s, r, t)) : e(o)
    } else
        typeof t == "object" && t instanceof Promise ? t.then(o => e(o)) : e(t)
}
var Ze = "x-";
function U(e="") {
    return Ze + e
}
function lr(e) {
    Ze = e
}
var _e = {};
function m(e, t) {
    return _e[e] = t,
    {
        before(n) {
            if (!_e[n]) {
                console.warn(String.raw`Cannot find directive \`${n}\`. \`${e}\` will use the default order of execution`);
                return
            }
            const i = j.indexOf(n);
            j.splice(i >= 0 ? i : j.indexOf("DEFAULT"), 0, e)
        }
    }
}
function ur(e) {
    return Object.keys(_e).includes(e)
}
function Qe(e, t, n) {
    if (t = Array.from(t),
    e._x_virtualDirectives) {
        let o = Object.entries(e._x_virtualDirectives).map( ([a,l]) => ({
            name: a,
            value: l
        }))
          , s = Ht(o);
        o = o.map(a => s.find(l => l.name === a.name) ? {
            name: `x-bind:${a.name}`,
            value: `"${a.value}"`
        } : a),
        t = t.concat(o)
    }
    let i = {};
    return t.map(Yt( (o, s) => i[o] = s)).filter(Xt).map(dr(i, n)).sort(pr).map(o => fr(e, o))
}
function Ht(e) {
    return Array.from(e).map(Yt()).filter(t => !Xt(t))
}
var je = !1
  , X = new Map
  , Wt = Symbol();
function cr(e) {
    je = !0;
    let t = Symbol();
    Wt = t,
    X.set(t, []);
    let n = () => {
        for (; X.get(t).length; )
            X.get(t).shift()();
        X.delete(t)
    }
      , i = () => {
        je = !1,
        n()
    }
    ;
    e(n),
    i()
}
function Ut(e) {
    let t = []
      , n = a => t.push(a)
      , [i,r] = Vn(e);
    return t.push(r),
    [{
        Alpine: ie,
        effect: i,
        cleanup: n,
        evaluateLater: v.bind(v, e),
        evaluate: F.bind(F, e)
    }, () => t.forEach(a => a())]
}
function fr(e, t) {
    let n = () => {}
      , i = _e[t.type] || n
      , [r,o] = Ut(e);
    Lt(e, t.original, o);
    let s = () => {
        e._x_ignore || e._x_ignoreSelf || (i.inline && i.inline(e, t, r),
        i = i.bind(i, e, t, r),
        je ? X.get(Wt).push(i) : i())
    }
    ;
    return s.runCleanups = o,
    s
}
var Jt = (e, t) => ({name: n, value: i}) => (n.startsWith(e) && (n = n.replace(e, t)),
{
    name: n,
    value: i
})
  , Vt = e => e;
function Yt(e= () => {}
) {
    return ({name: t, value: n}) => {
        let {name: i, value: r} = Gt.reduce( (o, s) => s(o), {
            name: t,
            value: n
        });
        return i !== t && e(i, t),
        {
            name: i,
            value: r
        }
    }
}
var Gt = [];
function et(e) {
    Gt.push(e)
}
function Xt({name: e}) {
    return Zt().test(e)
}
var Zt = () => new RegExp(`^${Ze}([^:^.]+)\\b`);
function dr(e, t) {
    return ({name: n, value: i}) => {
        let r = n.match(Zt())
          , o = n.match(/:([a-zA-Z0-9\-_:]+)/)
          , s = n.match(/\.[^.\]]+(?=[^\]]*$)/g) || []
          , a = t || e[n] || n;
        return {
            type: r ? r[1] : null,
            value: o ? o[1] : null,
            modifiers: s.map(l => l.replace(".", "")),
            expression: i,
            original: a
        }
    }
}
var Ne = "DEFAULT"
  , j = ["ignore", "ref", "data", "id", "anchor", "bind", "init", "for", "model", "modelable", "transition", "show", "if", Ne, "teleport"];
function pr(e, t) {
    let n = j.indexOf(e.type) === -1 ? Ne : e.type
      , i = j.indexOf(t.type) === -1 ? Ne : t.type;
    return j.indexOf(n) - j.indexOf(i)
}
function Z(e, t, n={}) {
    e.dispatchEvent(new CustomEvent(t,{
        detail: n,
        bubbles: !0,
        composed: !0,
        cancelable: !0
    }))
}
function I(e, t) {
    if (typeof ShadowRoot == "function" && e instanceof ShadowRoot) {
        Array.from(e.children).forEach(r => I(r, t));
        return
    }
    let n = !1;
    if (t(e, () => n = !0),
    n)
        return;
    let i = e.firstElementChild;
    for (; i; )
        I(i, t),
        i = i.nextElementSibling
}
function A(e, ...t) {
    console.warn(`Alpine Warning: ${e}`, ...t)
}
var ht = !1;
function hr() {
    ht && A("Alpine has already been initialized on this page. Calling Alpine.start() more than once can cause problems."),
    ht = !0,
    document.body || A("Unable to initialize. Trying to load Alpine before `<body>` is available. Did you forget to add `defer` in Alpine's `<script>` tag?"),
    Z(document, "alpine:init"),
    Z(document, "alpine:initializing"),
    Ye(),
    Yn(t => M(t, I)),
    Ue(t => sn(t)),
    Rt( (t, n) => {
        Qe(t, n).forEach(i => i())
    }
    );
    let e = t => !me(t.parentElement, !0);
    Array.from(document.querySelectorAll(tn().join(","))).filter(e).forEach(t => {
        M(t)
    }
    ),
    Z(document, "alpine:initialized"),
    setTimeout( () => {
        mr()
    }
    )
}
var tt = []
  , Qt = [];
function en() {
    return tt.map(e => e())
}
function tn() {
    return tt.concat(Qt).map(e => e())
}
function nn(e) {
    tt.push(e)
}
function rn(e) {
    Qt.push(e)
}
function me(e, t=!1) {
    return re(e, n => {
        if ((t ? tn() : en()).some(r => n.matches(r)))
            return !0
    }
    )
}
function re(e, t) {
    if (e) {
        if (t(e))
            return e;
        if (e._x_teleportBack && (e = e._x_teleportBack),
        !!e.parentElement)
            return re(e.parentElement, t)
    }
}
function _r(e) {
    return en().some(t => e.matches(t))
}
var on = [];
function gr(e) {
    on.push(e)
}
function M(e, t=I, n= () => {}
) {
    cr( () => {
        t(e, (i, r) => {
            n(i, r),
            on.forEach(o => o(i, r)),
            Qe(i, i.attributes).forEach(o => o()),
            i._x_ignore && r()
        }
        )
    }
    )
}
function sn(e, t=I) {
    t(e, n => {
        jt(n),
        Gn(n)
    }
    )
}
function mr() {
    [["ui", "dialog", ["[x-dialog], [x-popover]"]], ["anchor", "anchor", ["[x-anchor]"]], ["sort", "sort", ["[x-sort]"]]].forEach( ([t,n,i]) => {
        ur(n) || i.some(r => {
            if (document.querySelector(r))
                return A(`found "${r}", but missing ${t} plugin`),
                !0
        }
        )
    }
    )
}
var Fe = []
  , nt = !1;
function rt(e= () => {}
) {
    return queueMicrotask( () => {
        nt || setTimeout( () => {
            De()
        }
        )
    }
    ),
    new Promise(t => {
        Fe.push( () => {
            e(),
            t()
        }
        )
    }
    )
}
function De() {
    for (nt = !1; Fe.length; )
        Fe.shift()()
}
function xr() {
    nt = !0
}
function it(e, t) {
    return Array.isArray(t) ? _t(e, t.join(" ")) : typeof t == "object" && t !== null ? br(e, t) : typeof t == "function" ? it(e, t()) : _t(e, t)
}
function _t(e, t) {
    let n = r => r.split(" ").filter(o => !e.classList.contains(o)).filter(Boolean)
      , i = r => (e.classList.add(...r),
    () => {
        e.classList.remove(...r)
    }
    );
    return t = t === !0 ? t = "" : t || "",
    i(n(t))
}
function br(e, t) {
    let n = a => a.split(" ").filter(Boolean)
      , i = Object.entries(t).flatMap( ([a,l]) => l ? n(a) : !1).filter(Boolean)
      , r = Object.entries(t).flatMap( ([a,l]) => l ? !1 : n(a)).filter(Boolean)
      , o = []
      , s = [];
    return r.forEach(a => {
        e.classList.contains(a) && (e.classList.remove(a),
        s.push(a))
    }
    ),
    i.forEach(a => {
        e.classList.contains(a) || (e.classList.add(a),
        o.push(a))
    }
    ),
    () => {
        s.forEach(a => e.classList.add(a)),
        o.forEach(a => e.classList.remove(a))
    }
}
function xe(e, t) {
    return typeof t == "object" && t !== null ? yr(e, t) : vr(e, t)
}
function yr(e, t) {
    let n = {};
    return Object.entries(t).forEach( ([i,r]) => {
        n[i] = e.style[i],
        i.startsWith("--") || (i = wr(i)),
        e.style.setProperty(i, r)
    }
    ),
    setTimeout( () => {
        e.style.length === 0 && e.removeAttribute("style")
    }
    ),
    () => {
        xe(e, n)
    }
}
function vr(e, t) {
    let n = e.getAttribute("style", t);
    return e.setAttribute("style", t),
    () => {
        e.setAttribute("style", n || "")
    }
}
function wr(e) {
    return e.replace(/([a-z])([A-Z])/g, "$1-$2").toLowerCase()
}
function Be(e, t= () => {}
) {
    let n = !1;
    return function() {
        n ? t.apply(this, arguments) : (n = !0,
        e.apply(this, arguments))
    }
}
m("transition", (e, {value: t, modifiers: n, expression: i}, {evaluate: r}) => {
    typeof i == "function" && (i = r(i)),
    i !== !1 && (!i || typeof i == "boolean" ? Er(e, n, t) : Ar(e, i, t))
}
);
function Ar(e, t, n) {
    an(e, it, ""),
    {
        enter: r => {
            e._x_transition.enter.during = r
        }
        ,
        "enter-start": r => {
            e._x_transition.enter.start = r
        }
        ,
        "enter-end": r => {
            e._x_transition.enter.end = r
        }
        ,
        leave: r => {
            e._x_transition.leave.during = r
        }
        ,
        "leave-start": r => {
            e._x_transition.leave.start = r
        }
        ,
        "leave-end": r => {
            e._x_transition.leave.end = r
        }
    }[n](t)
}
function Er(e, t, n) {
    an(e, xe);
    let i = !t.includes("in") && !t.includes("out") && !n
      , r = i || t.includes("in") || ["enter"].includes(n)
      , o = i || t.includes("out") || ["leave"].includes(n);
    t.includes("in") && !i && (t = t.filter( (h, g) => g < t.indexOf("out"))),
    t.includes("out") && !i && (t = t.filter( (h, g) => g > t.indexOf("out")));
    let s = !t.includes("opacity") && !t.includes("scale")
      , a = s || t.includes("opacity")
      , l = s || t.includes("scale")
      , u = a ? 0 : 1
      , c = l ? Y(t, "scale", 95) / 100 : 1
      , d = Y(t, "delay", 0) / 1e3
      , p = Y(t, "origin", "center")
      , b = "opacity, transform"
      , C = Y(t, "duration", 150) / 1e3
      , oe = Y(t, "duration", 75) / 1e3
      , f = "cubic-bezier(0.4, 0.0, 0.2, 1)";
    r && (e._x_transition.enter.during = {
        transformOrigin: p,
        transitionDelay: `${d}s`,
        transitionProperty: b,
        transitionDuration: `${C}s`,
        transitionTimingFunction: f
    },
    e._x_transition.enter.start = {
        opacity: u,
        transform: `scale(${c})`
    },
    e._x_transition.enter.end = {
        opacity: 1,
        transform: "scale(1)"
    }),
    o && (e._x_transition.leave.during = {
        transformOrigin: p,
        transitionDelay: `${d}s`,
        transitionProperty: b,
        transitionDuration: `${oe}s`,
        transitionTimingFunction: f
    },
    e._x_transition.leave.start = {
        opacity: 1,
        transform: "scale(1)"
    },
    e._x_transition.leave.end = {
        opacity: u,
        transform: `scale(${c})`
    })
}
function an(e, t, n={}) {
    e._x_transition || (e._x_transition = {
        enter: {
            during: n,
            start: n,
            end: n
        },
        leave: {
            during: n,
            start: n,
            end: n
        },
        in(i= () => {}
        , r= () => {}
        ) {
            Ke(e, t, {
                during: this.enter.during,
                start: this.enter.start,
                end: this.enter.end
            }, i, r)
        },
        out(i= () => {}
        , r= () => {}
        ) {
            Ke(e, t, {
                during: this.leave.during,
                start: this.leave.start,
                end: this.leave.end
            }, i, r)
        }
    })
}
window.Element.prototype._x_toggleAndCascadeWithTransitions = function(e, t, n, i) {
    const r = document.visibilityState === "visible" ? requestAnimationFrame : setTimeout;
    let o = () => r(n);
    if (t) {
        e._x_transition && (e._x_transition.enter || e._x_transition.leave) ? e._x_transition.enter && (Object.entries(e._x_transition.enter.during).length || Object.entries(e._x_transition.enter.start).length || Object.entries(e._x_transition.enter.end).length) ? e._x_transition.in(n) : o() : e._x_transition ? e._x_transition.in(n) : o();
        return
    }
    e._x_hidePromise = e._x_transition ? new Promise( (s, a) => {
        e._x_transition.out( () => {}
        , () => s(i)),
        e._x_transitioning && e._x_transitioning.beforeCancel( () => a({
            isFromCancelledTransition: !0
        }))
    }
    ) : Promise.resolve(i),
    queueMicrotask( () => {
        let s = ln(e);
        s ? (s._x_hideChildren || (s._x_hideChildren = []),
        s._x_hideChildren.push(e)) : r( () => {
            let a = l => {
                let u = Promise.all([l._x_hidePromise, ...(l._x_hideChildren || []).map(a)]).then( ([c]) => c());
                return delete l._x_hidePromise,
                delete l._x_hideChildren,
                u
            }
            ;
            a(e).catch(l => {
                if (!l.isFromCancelledTransition)
                    throw l
            }
            )
        }
        )
    }
    )
}
;
function ln(e) {
    let t = e.parentNode;
    if (t)
        return t._x_hidePromise ? t : ln(t)
}
function Ke(e, t, {during: n, start: i, end: r}={}, o= () => {}
, s= () => {}
) {
    if (e._x_transitioning && e._x_transitioning.cancel(),
    Object.keys(n).length === 0 && Object.keys(i).length === 0 && Object.keys(r).length === 0) {
        o(),
        s();
        return
    }
    let a, l, u;
    Sr(e, {
        start() {
            a = t(e, i)
        },
        during() {
            l = t(e, n)
        },
        before: o,
        end() {
            a(),
            u = t(e, r)
        },
        after: s,
        cleanup() {
            l(),
            u()
        }
    })
}
function Sr(e, t) {
    let n, i, r, o = Be( () => {
        x( () => {
            n = !0,
            i || t.before(),
            r || (t.end(),
            De()),
            t.after(),
            e.isConnected && t.cleanup(),
            delete e._x_transitioning
        }
        )
    }
    );
    e._x_transitioning = {
        beforeCancels: [],
        beforeCancel(s) {
            this.beforeCancels.push(s)
        },
        cancel: Be(function() {
            for (; this.beforeCancels.length; )
                this.beforeCancels.shift()();
            o()
        }),
        finish: o
    },
    x( () => {
        t.start(),
        t.during()
    }
    ),
    xr(),
    requestAnimationFrame( () => {
        if (n)
            return;
        let s = Number(getComputedStyle(e).transitionDuration.replace(/,.*/, "").replace("s", "")) * 1e3
          , a = Number(getComputedStyle(e).transitionDelay.replace(/,.*/, "").replace("s", "")) * 1e3;
        s === 0 && (s = Number(getComputedStyle(e).animationDuration.replace("s", "")) * 1e3),
        x( () => {
            t.before()
        }
        ),
        i = !0,
        requestAnimationFrame( () => {
            n || (x( () => {
                t.end()
            }
            ),
            De(),
            setTimeout(e._x_transitioning.finish, s + a),
            r = !0)
        }
        )
    }
    )
}
function Y(e, t, n) {
    if (e.indexOf(t) === -1)
        return n;
    const i = e[e.indexOf(t) + 1];
    if (!i || t === "scale" && isNaN(i))
        return n;
    if (t === "duration" || t === "delay") {
        let r = i.match(/([0-9]+)ms/);
        if (r)
            return r[1]
    }
    return t === "origin" && ["top", "right", "left", "center", "bottom"].includes(e[e.indexOf(t) + 2]) ? [i, e[e.indexOf(t) + 2]].join(" ") : i
}
var $ = !1;
function R(e, t= () => {}
) {
    return (...n) => $ ? t(...n) : e(...n)
}
function Or(e) {
    return (...t) => $ && e(...t)
}
var un = [];
function be(e) {
    un.push(e)
}
function Mr(e, t) {
    un.forEach(n => n(e, t)),
    $ = !0,
    cn( () => {
        M(t, (n, i) => {
            i(n, () => {}
            )
        }
        )
    }
    ),
    $ = !1
}
var ke = !1;
function Cr(e, t) {
    t._x_dataStack || (t._x_dataStack = e._x_dataStack),
    $ = !0,
    ke = !0,
    cn( () => {
        Tr(t)
    }
    ),
    $ = !1,
    ke = !1
}
function Tr(e) {
    let t = !1;
    M(e, (i, r) => {
        I(i, (o, s) => {
            if (t && _r(o))
                return s();
            t = !0,
            r(o, s)
        }
        )
    }
    )
}
function cn(e) {
    let t = K;
    pt( (n, i) => {
        let r = t(n);
        return W(r),
        () => {}
    }
    ),
    e(),
    pt(t)
}
function fn(e, t, n, i=[]) {
    switch (e._x_bindings || (e._x_bindings = H({})),
    e._x_bindings[t] = n,
    t = i.includes("camel") ? Fr(t) : t,
    t) {
    case "value":
        Ir(e, n);
        break;
    case "style":
        Pr(e, n);
        break;
    case "class":
        $r(e, n);
        break;
    case "selected":
    case "checked":
        Rr(e, t, n);
        break;
    default:
        dn(e, t, n);
        break
    }
}
function Ir(e, t) {
    if (e.type === "radio")
        e.attributes.value === void 0 && (e.value = t),
        window.fromModel && (typeof t == "boolean" ? e.checked = de(e.value) === t : e.checked = gt(e.value, t));
    else if (e.type === "checkbox")
        Number.isInteger(t) ? e.value = t : !Array.isArray(t) && typeof t != "boolean" && ![null, void 0].includes(t) ? e.value = String(t) : Array.isArray(t) ? e.checked = t.some(n => gt(n, e.value)) : e.checked = !!t;
    else if (e.tagName === "SELECT")
        Nr(e, t);
    else {
        if (e.value === t)
            return;
        e.value = t === void 0 ? "" : t
    }
}
function $r(e, t) {
    e._x_undoAddedClasses && e._x_undoAddedClasses(),
    e._x_undoAddedClasses = it(e, t)
}
function Pr(e, t) {
    e._x_undoAddedStyles && e._x_undoAddedStyles(),
    e._x_undoAddedStyles = xe(e, t)
}
function Rr(e, t, n) {
    dn(e, t, n),
    jr(e, t, n)
}
function dn(e, t, n) {
    [null, void 0, !1].includes(n) && Dr(t) ? e.removeAttribute(t) : (pn(t) && (n = t),
    Lr(e, t, n))
}
function Lr(e, t, n) {
    e.getAttribute(t) != n && e.setAttribute(t, n)
}
function jr(e, t, n) {
    e[t] !== n && (e[t] = n)
}
function Nr(e, t) {
    const n = [].concat(t).map(i => i + "");
    Array.from(e.options).forEach(i => {
        i.selected = n.includes(i.value)
    }
    )
}
function Fr(e) {
    return e.toLowerCase().replace(/-(\w)/g, (t, n) => n.toUpperCase())
}
function gt(e, t) {
    return e == t
}
function de(e) {
    return [1, "1", "true", "on", "yes", !0].includes(e) ? !0 : [0, "0", "false", "off", "no", !1].includes(e) ? !1 : e ? !!e : null
}
function pn(e) {
    return ["disabled", "checked", "required", "readonly", "open", "selected", "autofocus", "itemscope", "multiple", "novalidate", "allowfullscreen", "allowpaymentrequest", "formnovalidate", "autoplay", "controls", "loop", "muted", "playsinline", "default", "ismap", "reversed", "async", "defer", "nomodule"].includes(e)
}
function Dr(e) {
    return !["aria-pressed", "aria-checked", "aria-expanded", "aria-selected"].includes(e)
}
function Br(e, t, n) {
    return e._x_bindings && e._x_bindings[t] !== void 0 ? e._x_bindings[t] : hn(e, t, n)
}
function Kr(e, t, n, i=!0) {
    if (e._x_bindings && e._x_bindings[t] !== void 0)
        return e._x_bindings[t];
    if (e._x_inlineBindings && e._x_inlineBindings[t] !== void 0) {
        let r = e._x_inlineBindings[t];
        return r.extract = i,
        kt( () => F(e, r.expression))
    }
    return hn(e, t, n)
}
function hn(e, t, n) {
    let i = e.getAttribute(t);
    return i === null ? typeof n == "function" ? n() : n : i === "" ? !0 : pn(t) ? !![t, "true"].includes(i) : i
}
function _n(e, t) {
    var n;
    return function() {
        var i = this
          , r = arguments
          , o = function() {
            n = null,
            e.apply(i, r)
        };
        clearTimeout(n),
        n = setTimeout(o, t)
    }
}
function gn(e, t) {
    let n;
    return function() {
        let i = this
          , r = arguments;
        n || (e.apply(i, r),
        n = !0,
        setTimeout( () => n = !1, t))
    }
}
function mn({get: e, set: t}, {get: n, set: i}) {
    let r = !0, o, s = K( () => {
        let a = e()
          , l = n();
        if (r)
            i(Se(a)),
            r = !1;
        else {
            let u = JSON.stringify(a)
              , c = JSON.stringify(l);
            u !== o ? i(Se(a)) : u !== c && t(Se(l))
        }
        o = JSON.stringify(e()),
        JSON.stringify(n())
    }
    );
    return () => {
        W(s)
    }
}
function Se(e) {
    return typeof e == "object" ? JSON.parse(JSON.stringify(e)) : e
}
function kr(e) {
    (Array.isArray(e) ? e : [e]).forEach(n => n(ie))
}
var L = {}
  , mt = !1;
function zr(e, t) {
    if (mt || (L = H(L),
    mt = !0),
    t === void 0)
        return L[e];
    L[e] = t,
    typeof t == "object" && t !== null && t.hasOwnProperty("init") && typeof t.init == "function" && L[e].init(),
    Dt(L[e])
}
function qr() {
    return L
}
var xn = {};
function Hr(e, t) {
    let n = typeof t != "function" ? () => t : t;
    return e instanceof Element ? bn(e, n()) : (xn[e] = n,
    () => {}
    )
}
function Wr(e) {
    return Object.entries(xn).forEach( ([t,n]) => {
        Object.defineProperty(e, t, {
            get() {
                return (...i) => n(...i)
            }
        })
    }
    ),
    e
}
function bn(e, t, n) {
    let i = [];
    for (; i.length; )
        i.pop()();
    let r = Object.entries(t).map( ([s,a]) => ({
        name: s,
        value: a
    }))
      , o = Ht(r);
    return r = r.map(s => o.find(a => a.name === s.name) ? {
        name: `x-bind:${s.name}`,
        value: `"${s.value}"`
    } : s),
    Qe(e, r, n).map(s => {
        i.push(s.runCleanups),
        s()
    }
    ),
    () => {
        for (; i.length; )
            i.pop()()
    }
}
var yn = {};
function Ur(e, t) {
    yn[e] = t
}
function Jr(e, t) {
    return Object.entries(yn).forEach( ([n,i]) => {
        Object.defineProperty(e, n, {
            get() {
                return (...r) => i.bind(t)(...r)
            },
            enumerable: !1
        })
    }
    ),
    e
}
var Vr = {
    get reactive() {
        return H
    },
    get release() {
        return W
    },
    get effect() {
        return K
    },
    get raw() {
        return Ct
    },
    version: "3.13.10",
    flushAndStopDeferringMutations: Qn,
    dontAutoEvaluateFunctions: kt,
    disableEffectScheduling: Un,
    startObservingMutations: Ye,
    stopObservingMutations: Nt,
    setReactivityEngine: Jn,
    onAttributeRemoved: Lt,
    onAttributesAdded: Rt,
    closestDataStack: z,
    skipDuringClone: R,
    onlyDuringClone: Or,
    addRootSelector: nn,
    addInitSelector: rn,
    interceptClone: be,
    addScopeToNode: te,
    deferMutations: Zn,
    mapAttributes: et,
    evaluateLater: v,
    interceptInit: gr,
    setEvaluator: ir,
    mergeProxies: ne,
    extractProp: Kr,
    findClosest: re,
    onElRemoved: Ue,
    closestRoot: me,
    destroyTree: sn,
    interceptor: Bt,
    transition: Ke,
    setStyles: xe,
    mutateDom: x,
    directive: m,
    entangle: mn,
    throttle: gn,
    debounce: _n,
    evaluate: F,
    initTree: M,
    nextTick: rt,
    prefixed: U,
    prefix: lr,
    plugin: kr,
    magic: S,
    store: zr,
    start: hr,
    clone: Cr,
    cloneNode: Mr,
    bound: Br,
    $data: Ft,
    watch: Tt,
    walk: I,
    data: Ur,
    bind: Hr
}
  , ie = Vr;
function Yr(e, t) {
    const n = Object.create(null)
      , i = e.split(",");
    for (let r = 0; r < i.length; r++)
        n[i[r]] = !0;
    return t ? r => !!n[r.toLowerCase()] : r => !!n[r]
}
var Gr = Object.freeze({}), Xr = Object.prototype.hasOwnProperty, ye = (e, t) => Xr.call(e, t), D = Array.isArray, Q = e => vn(e) === "[object Map]", Zr = e => typeof e == "string", ot = e => typeof e == "symbol", ve = e => e !== null && typeof e == "object", Qr = Object.prototype.toString, vn = e => Qr.call(e), wn = e => vn(e).slice(8, -1), st = e => Zr(e) && e !== "NaN" && e[0] !== "-" && "" + parseInt(e, 10) === e, ei = e => {
    const t = Object.create(null);
    return n => t[n] || (t[n] = e(n))
}
, ti = ei(e => e.charAt(0).toUpperCase() + e.slice(1)), An = (e, t) => e !== t && (e === e || t === t), ze = new WeakMap, G = [], O, B = Symbol("iterate"), qe = Symbol("Map key iterate");
function ni(e) {
    return e && e._isEffect === !0
}
function ri(e, t=Gr) {
    ni(e) && (e = e.raw);
    const n = si(e, t);
    return t.lazy || n(),
    n
}
function ii(e) {
    e.active && (En(e),
    e.options.onStop && e.options.onStop(),
    e.active = !1)
}
var oi = 0;
function si(e, t) {
    const n = function() {
        if (!n.active)
            return e();
        if (!G.includes(n)) {
            En(n);
            try {
                return li(),
                G.push(n),
                O = n,
                e()
            } finally {
                G.pop(),
                Sn(),
                O = G[G.length - 1]
            }
        }
    };
    return n.id = oi++,
    n.allowRecurse = !!t.allowRecurse,
    n._isEffect = !0,
    n.active = !0,
    n.raw = e,
    n.deps = [],
    n.options = t,
    n
}
function En(e) {
    const {deps: t} = e;
    if (t.length) {
        for (let n = 0; n < t.length; n++)
            t[n].delete(e);
        t.length = 0
    }
}
var q = !0
  , at = [];
function ai() {
    at.push(q),
    q = !1
}
function li() {
    at.push(q),
    q = !0
}
function Sn() {
    const e = at.pop();
    q = e === void 0 ? !0 : e
}
function E(e, t, n) {
    if (!q || O === void 0)
        return;
    let i = ze.get(e);
    i || ze.set(e, i = new Map);
    let r = i.get(n);
    r || i.set(n, r = new Set),
    r.has(O) || (r.add(O),
    O.deps.push(r),
    O.options.onTrack && O.options.onTrack({
        effect: O,
        target: e,
        type: t,
        key: n
    }))
}
function P(e, t, n, i, r, o) {
    const s = ze.get(e);
    if (!s)
        return;
    const a = new Set
      , l = c => {
        c && c.forEach(d => {
            (d !== O || d.allowRecurse) && a.add(d)
        }
        )
    }
    ;
    if (t === "clear")
        s.forEach(l);
    else if (n === "length" && D(e))
        s.forEach( (c, d) => {
            (d === "length" || d >= i) && l(c)
        }
        );
    else
        switch (n !== void 0 && l(s.get(n)),
        t) {
        case "add":
            D(e) ? st(n) && l(s.get("length")) : (l(s.get(B)),
            Q(e) && l(s.get(qe)));
            break;
        case "delete":
            D(e) || (l(s.get(B)),
            Q(e) && l(s.get(qe)));
            break;
        case "set":
            Q(e) && l(s.get(B));
            break
        }
    const u = c => {
        c.options.onTrigger && c.options.onTrigger({
            effect: c,
            target: e,
            key: n,
            type: t,
            newValue: i,
            oldValue: r,
            oldTarget: o
        }),
        c.options.scheduler ? c.options.scheduler(c) : c()
    }
    ;
    a.forEach(u)
}
var ui = Yr("__proto__,__v_isRef,__isVue")
  , On = new Set(Object.getOwnPropertyNames(Symbol).map(e => Symbol[e]).filter(ot))
  , ci = Mn()
  , fi = Mn(!0)
  , xt = di();
function di() {
    const e = {};
    return ["includes", "indexOf", "lastIndexOf"].forEach(t => {
        e[t] = function(...n) {
            const i = _(this);
            for (let o = 0, s = this.length; o < s; o++)
                E(i, "get", o + "");
            const r = i[t](...n);
            return r === -1 || r === !1 ? i[t](...n.map(_)) : r
        }
    }
    ),
    ["push", "pop", "shift", "unshift", "splice"].forEach(t => {
        e[t] = function(...n) {
            ai();
            const i = _(this)[t].apply(this, n);
            return Sn(),
            i
        }
    }
    ),
    e
}
function Mn(e=!1, t=!1) {
    return function(i, r, o) {
        if (r === "__v_isReactive")
            return !e;
        if (r === "__v_isReadonly")
            return e;
        if (r === "__v_raw" && o === (e ? t ? Ci : $n : t ? Mi : In).get(i))
            return i;
        const s = D(i);
        if (!e && s && ye(xt, r))
            return Reflect.get(xt, r, o);
        const a = Reflect.get(i, r, o);
        return (ot(r) ? On.has(r) : ui(r)) || (e || E(i, "get", r),
        t) ? a : He(a) ? !s || !st(r) ? a.value : a : ve(a) ? e ? Pn(a) : ft(a) : a
    }
}
var pi = hi();
function hi(e=!1) {
    return function(n, i, r, o) {
        let s = n[i];
        if (!e && (r = _(r),
        s = _(s),
        !D(n) && He(s) && !He(r)))
            return s.value = r,
            !0;
        const a = D(n) && st(i) ? Number(i) < n.length : ye(n, i)
          , l = Reflect.set(n, i, r, o);
        return n === _(o) && (a ? An(r, s) && P(n, "set", i, r, s) : P(n, "add", i, r)),
        l
    }
}
function _i(e, t) {
    const n = ye(e, t)
      , i = e[t]
      , r = Reflect.deleteProperty(e, t);
    return r && n && P(e, "delete", t, void 0, i),
    r
}
function gi(e, t) {
    const n = Reflect.has(e, t);
    return (!ot(t) || !On.has(t)) && E(e, "has", t),
    n
}
function mi(e) {
    return E(e, "iterate", D(e) ? "length" : B),
    Reflect.ownKeys(e)
}
var xi = {
    get: ci,
    set: pi,
    deleteProperty: _i,
    has: gi,
    ownKeys: mi
}
  , bi = {
    get: fi,
    set(e, t) {
        return console.warn(`Set operation on key "${String(t)}" failed: target is readonly.`, e),
        !0
    },
    deleteProperty(e, t) {
        return console.warn(`Delete operation on key "${String(t)}" failed: target is readonly.`, e),
        !0
    }
}
  , lt = e => ve(e) ? ft(e) : e
  , ut = e => ve(e) ? Pn(e) : e
  , ct = e => e
  , we = e => Reflect.getPrototypeOf(e);
function se(e, t, n=!1, i=!1) {
    e = e.__v_raw;
    const r = _(e)
      , o = _(t);
    t !== o && !n && E(r, "get", t),
    !n && E(r, "get", o);
    const {has: s} = we(r)
      , a = i ? ct : n ? ut : lt;
    if (s.call(r, t))
        return a(e.get(t));
    if (s.call(r, o))
        return a(e.get(o));
    e !== r && e.get(t)
}
function ae(e, t=!1) {
    const n = this.__v_raw
      , i = _(n)
      , r = _(e);
    return e !== r && !t && E(i, "has", e),
    !t && E(i, "has", r),
    e === r ? n.has(e) : n.has(e) || n.has(r)
}
function le(e, t=!1) {
    return e = e.__v_raw,
    !t && E(_(e), "iterate", B),
    Reflect.get(e, "size", e)
}
function bt(e) {
    e = _(e);
    const t = _(this);
    return we(t).has.call(t, e) || (t.add(e),
    P(t, "add", e, e)),
    this
}
function yt(e, t) {
    t = _(t);
    const n = _(this)
      , {has: i, get: r} = we(n);
    let o = i.call(n, e);
    o ? Tn(n, i, e) : (e = _(e),
    o = i.call(n, e));
    const s = r.call(n, e);
    return n.set(e, t),
    o ? An(t, s) && P(n, "set", e, t, s) : P(n, "add", e, t),
    this
}
function vt(e) {
    const t = _(this)
      , {has: n, get: i} = we(t);
    let r = n.call(t, e);
    r ? Tn(t, n, e) : (e = _(e),
    r = n.call(t, e));
    const o = i ? i.call(t, e) : void 0
      , s = t.delete(e);
    return r && P(t, "delete", e, void 0, o),
    s
}
function wt() {
    const e = _(this)
      , t = e.size !== 0
      , n = Q(e) ? new Map(e) : new Set(e)
      , i = e.clear();
    return t && P(e, "clear", void 0, void 0, n),
    i
}
function ue(e, t) {
    return function(i, r) {
        const o = this
          , s = o.__v_raw
          , a = _(s)
          , l = t ? ct : e ? ut : lt;
        return !e && E(a, "iterate", B),
        s.forEach( (u, c) => i.call(r, l(u), l(c), o))
    }
}
function ce(e, t, n) {
    return function(...i) {
        const r = this.__v_raw
          , o = _(r)
          , s = Q(o)
          , a = e === "entries" || e === Symbol.iterator && s
          , l = e === "keys" && s
          , u = r[e](...i)
          , c = n ? ct : t ? ut : lt;
        return !t && E(o, "iterate", l ? qe : B),
        {
            next() {
                const {value: d, done: p} = u.next();
                return p ? {
                    value: d,
                    done: p
                } : {
                    value: a ? [c(d[0]), c(d[1])] : c(d),
                    done: p
                }
            },
            [Symbol.iterator]() {
                return this
            }
        }
    }
}
function T(e) {
    return function(...t) {
        {
            const n = t[0] ? `on key "${t[0]}" ` : "";
            console.warn(`${ti(e)} operation ${n}failed: target is readonly.`, _(this))
        }
        return e === "delete" ? !1 : this
    }
}
function yi() {
    const e = {
        get(o) {
            return se(this, o)
        },
        get size() {
            return le(this)
        },
        has: ae,
        add: bt,
        set: yt,
        delete: vt,
        clear: wt,
        forEach: ue(!1, !1)
    }
      , t = {
        get(o) {
            return se(this, o, !1, !0)
        },
        get size() {
            return le(this)
        },
        has: ae,
        add: bt,
        set: yt,
        delete: vt,
        clear: wt,
        forEach: ue(!1, !0)
    }
      , n = {
        get(o) {
            return se(this, o, !0)
        },
        get size() {
            return le(this, !0)
        },
        has(o) {
            return ae.call(this, o, !0)
        },
        add: T("add"),
        set: T("set"),
        delete: T("delete"),
        clear: T("clear"),
        forEach: ue(!0, !1)
    }
      , i = {
        get(o) {
            return se(this, o, !0, !0)
        },
        get size() {
            return le(this, !0)
        },
        has(o) {
            return ae.call(this, o, !0)
        },
        add: T("add"),
        set: T("set"),
        delete: T("delete"),
        clear: T("clear"),
        forEach: ue(!0, !0)
    };
    return ["keys", "values", "entries", Symbol.iterator].forEach(o => {
        e[o] = ce(o, !1, !1),
        n[o] = ce(o, !0, !1),
        t[o] = ce(o, !1, !0),
        i[o] = ce(o, !0, !0)
    }
    ),
    [e, n, t, i]
}
var [vi,wi,Ai,Ei] = yi();
function Cn(e, t) {
    const n = t ? e ? Ei : Ai : e ? wi : vi;
    return (i, r, o) => r === "__v_isReactive" ? !e : r === "__v_isReadonly" ? e : r === "__v_raw" ? i : Reflect.get(ye(n, r) && r in i ? n : i, r, o)
}
var Si = {
    get: Cn(!1, !1)
}
  , Oi = {
    get: Cn(!0, !1)
};
function Tn(e, t, n) {
    const i = _(n);
    if (i !== n && t.call(e, i)) {
        const r = wn(e);
        console.warn(`Reactive ${r} contains both the raw and reactive versions of the same object${r === "Map" ? " as keys" : ""}, which can lead to inconsistencies. Avoid differentiating between the raw and reactive versions of an object and only use the reactive version if possible.`)
    }
}
var In = new WeakMap
  , Mi = new WeakMap
  , $n = new WeakMap
  , Ci = new WeakMap;
function Ti(e) {
    switch (e) {
    case "Object":
    case "Array":
        return 1;
    case "Map":
    case "Set":
    case "WeakMap":
    case "WeakSet":
        return 2;
    default:
        return 0
    }
}
function Ii(e) {
    return e.__v_skip || !Object.isExtensible(e) ? 0 : Ti(wn(e))
}
function ft(e) {
    return e && e.__v_isReadonly ? e : Rn(e, !1, xi, Si, In)
}
function Pn(e) {
    return Rn(e, !0, bi, Oi, $n)
}
function Rn(e, t, n, i, r) {
    if (!ve(e))
        return console.warn(`value cannot be made reactive: ${String(e)}`),
        e;
    if (e.__v_raw && !(t && e.__v_isReactive))
        return e;
    const o = r.get(e);
    if (o)
        return o;
    const s = Ii(e);
    if (s === 0)
        return e;
    const a = new Proxy(e,s === 2 ? i : n);
    return r.set(e, a),
    a
}
function _(e) {
    return e && _(e.__v_raw) || e
}
function He(e) {
    return !!(e && e.__v_isRef === !0)
}
S("nextTick", () => rt);
S("dispatch", e => Z.bind(Z, e));
S("watch", (e, {evaluateLater: t, cleanup: n}) => (i, r) => {
    let o = t(i)
      , a = Tt( () => {
        let l;
        return o(u => l = u),
        l
    }
    , r);
    n(a)
}
);
S("store", qr);
S("data", e => Ft(e));
S("root", e => me(e));
S("refs", e => (e._x_refs_proxy || (e._x_refs_proxy = ne($i(e))),
e._x_refs_proxy));
function $i(e) {
    let t = [];
    return re(e, n => {
        n._x_refs && t.push(n._x_refs)
    }
    ),
    t
}
var Oe = {};
function Ln(e) {
    return Oe[e] || (Oe[e] = 0),
    ++Oe[e]
}
function Pi(e, t) {
    return re(e, n => {
        if (n._x_ids && n._x_ids[t])
            return !0
    }
    )
}
function Ri(e, t) {
    e._x_ids || (e._x_ids = {}),
    e._x_ids[t] || (e._x_ids[t] = Ln(t))
}
S("id", (e, {cleanup: t}) => (n, i=null) => {
    let r = `${n}${i ? `-${i}` : ""}`;
    return Li(e, r, t, () => {
        let o = Pi(e, n)
          , s = o ? o._x_ids[n] : Ln(n);
        return i ? `${n}-${s}-${i}` : `${n}-${s}`
    }
    )
}
);
be( (e, t) => {
    e._x_id && (t._x_id = e._x_id)
}
);
function Li(e, t, n, i) {
    if (e._x_id || (e._x_id = {}),
    e._x_id[t])
        return e._x_id[t];
    let r = i();
    return e._x_id[t] = r,
    n( () => {
        delete e._x_id[t]
    }
    ),
    r
}
S("el", e => e);
jn("Focus", "focus", "focus");
jn("Persist", "persist", "persist");
function jn(e, t, n) {
    S(t, i => A(`You can't use [$${t}] without first installing the "${e}" plugin here: https://alpinejs.dev/plugins/${n}`, i))
}
m("modelable", (e, {expression: t}, {effect: n, evaluateLater: i, cleanup: r}) => {
    let o = i(t)
      , s = () => {
        let c;
        return o(d => c = d),
        c
    }
      , a = i(`${t} = __placeholder`)
      , l = c => a( () => {}
    , {
        scope: {
            __placeholder: c
        }
    })
      , u = s();
    l(u),
    queueMicrotask( () => {
        if (!e._x_model)
            return;
        e._x_removeModelListeners.default();
        let c = e._x_model.get
          , d = e._x_model.set
          , p = mn({
            get() {
                return c()
            },
            set(b) {
                d(b)
            }
        }, {
            get() {
                return s()
            },
            set(b) {
                l(b)
            }
        });
        r(p)
    }
    )
}
);
m("teleport", (e, {modifiers: t, expression: n}, {cleanup: i}) => {
    e.tagName.toLowerCase() !== "template" && A("x-teleport can only be used on a <template> tag", e);
    let r = At(n)
      , o = e.content.cloneNode(!0).firstElementChild;
    e._x_teleport = o,
    o._x_teleportBack = e,
    e.setAttribute("data-teleport-template", !0),
    o.setAttribute("data-teleport-target", !0),
    e._x_forwardEvents && e._x_forwardEvents.forEach(a => {
        o.addEventListener(a, l => {
            l.stopPropagation(),
            e.dispatchEvent(new l.constructor(l.type,l))
        }
        )
    }
    ),
    te(o, {}, e);
    let s = (a, l, u) => {
        u.includes("prepend") ? l.parentNode.insertBefore(a, l) : u.includes("append") ? l.parentNode.insertBefore(a, l.nextSibling) : l.appendChild(a)
    }
    ;
    x( () => {
        s(o, r, t),
        R( () => {
            M(o),
            o._x_ignore = !0
        }
        )()
    }
    ),
    e._x_teleportPutBack = () => {
        let a = At(n);
        x( () => {
            s(e._x_teleport, a, t)
        }
        )
    }
    ,
    i( () => o.remove())
}
);
var ji = document.createElement("div");
function At(e) {
    let t = R( () => document.querySelector(e), () => ji)();
    return t || A(`Cannot find x-teleport element for selector: "${e}"`),
    t
}
var Nn = () => {}
;
Nn.inline = (e, {modifiers: t}, {cleanup: n}) => {
    t.includes("self") ? e._x_ignoreSelf = !0 : e._x_ignore = !0,
    n( () => {
        t.includes("self") ? delete e._x_ignoreSelf : delete e._x_ignore
    }
    )
}
;
m("ignore", Nn);
m("effect", R( (e, {expression: t}, {effect: n}) => {
    n(v(e, t))
}
));
function We(e, t, n, i) {
    let r = e
      , o = l => i(l)
      , s = {}
      , a = (l, u) => c => u(l, c);
    if (n.includes("dot") && (t = Ni(t)),
    n.includes("camel") && (t = Fi(t)),
    n.includes("passive") && (s.passive = !0),
    n.includes("capture") && (s.capture = !0),
    n.includes("window") && (r = window),
    n.includes("document") && (r = document),
    n.includes("debounce")) {
        let l = n[n.indexOf("debounce") + 1] || "invalid-wait"
          , u = ge(l.split("ms")[0]) ? Number(l.split("ms")[0]) : 250;
        o = _n(o, u)
    }
    if (n.includes("throttle")) {
        let l = n[n.indexOf("throttle") + 1] || "invalid-wait"
          , u = ge(l.split("ms")[0]) ? Number(l.split("ms")[0]) : 250;
        o = gn(o, u)
    }
    return n.includes("prevent") && (o = a(o, (l, u) => {
        u.preventDefault(),
        l(u)
    }
    )),
    n.includes("stop") && (o = a(o, (l, u) => {
        u.stopPropagation(),
        l(u)
    }
    )),
    n.includes("once") && (o = a(o, (l, u) => {
        l(u),
        r.removeEventListener(t, o, s)
    }
    )),
    (n.includes("away") || n.includes("outside")) && (r = document,
    o = a(o, (l, u) => {
        e.contains(u.target) || u.target.isConnected !== !1 && (e.offsetWidth < 1 && e.offsetHeight < 1 || e._x_isShown !== !1 && l(u))
    }
    )),
    n.includes("self") && (o = a(o, (l, u) => {
        u.target === e && l(u)
    }
    )),
    o = a(o, (l, u) => {
        Bi(t) && Ki(u, n) || l(u)
    }
    ),
    r.addEventListener(t, o, s),
    () => {
        r.removeEventListener(t, o, s)
    }
}
function Ni(e) {
    return e.replace(/-/g, ".")
}
function Fi(e) {
    return e.toLowerCase().replace(/-(\w)/g, (t, n) => n.toUpperCase())
}
function ge(e) {
    return !Array.isArray(e) && !isNaN(e)
}
function Di(e) {
    return [" ", "_"].includes(e) ? e : e.replace(/([a-z])([A-Z])/g, "$1-$2").replace(/[_\s]/, "-").toLowerCase()
}
function Bi(e) {
    return ["keydown", "keyup"].includes(e)
}
function Ki(e, t) {
    let n = t.filter(o => !["window", "document", "prevent", "stop", "once", "capture"].includes(o));
    if (n.includes("debounce")) {
        let o = n.indexOf("debounce");
        n.splice(o, ge((n[o + 1] || "invalid-wait").split("ms")[0]) ? 2 : 1)
    }
    if (n.includes("throttle")) {
        let o = n.indexOf("throttle");
        n.splice(o, ge((n[o + 1] || "invalid-wait").split("ms")[0]) ? 2 : 1)
    }
    if (n.length === 0 || n.length === 1 && Et(e.key).includes(n[0]))
        return !1;
    const r = ["ctrl", "shift", "alt", "meta", "cmd", "super"].filter(o => n.includes(o));
    return n = n.filter(o => !r.includes(o)),
    !(r.length > 0 && r.filter(s => ((s === "cmd" || s === "super") && (s = "meta"),
    e[`${s}Key`])).length === r.length && Et(e.key).includes(n[0]))
}
function Et(e) {
    if (!e)
        return [];
    e = Di(e);
    let t = {
        ctrl: "control",
        slash: "/",
        space: " ",
        spacebar: " ",
        cmd: "meta",
        esc: "escape",
        up: "arrow-up",
        down: "arrow-down",
        left: "arrow-left",
        right: "arrow-right",
        period: ".",
        comma: ",",
        equal: "=",
        minus: "-",
        underscore: "_"
    };
    return t[e] = e,
    Object.keys(t).map(n => {
        if (t[n] === e)
            return n
    }
    ).filter(n => n)
}
m("model", (e, {modifiers: t, expression: n}, {effect: i, cleanup: r}) => {
    let o = e;
    t.includes("parent") && (o = e.parentNode);
    let s = v(o, n), a;
    typeof n == "string" ? a = v(o, `${n} = __placeholder`) : typeof n == "function" && typeof n() == "string" ? a = v(o, `${n()} = __placeholder`) : a = () => {}
    ;
    let l = () => {
        let p;
        return s(b => p = b),
        St(p) ? p.get() : p
    }
      , u = p => {
        let b;
        s(C => b = C),
        St(b) ? b.set(p) : a( () => {}
        , {
            scope: {
                __placeholder: p
            }
        })
    }
    ;
    typeof n == "string" && e.type === "radio" && x( () => {
        e.hasAttribute("name") || e.setAttribute("name", n)
    }
    );
    var c = e.tagName.toLowerCase() === "select" || ["checkbox", "radio"].includes(e.type) || t.includes("lazy") ? "change" : "input";
    let d = $ ? () => {}
    : We(e, c, t, p => {
        u(Me(e, t, p, l()))
    }
    );
    if (t.includes("fill") && ([void 0, null, ""].includes(l()) || e.type === "checkbox" && Array.isArray(l()) || e.tagName.toLowerCase() === "select" && e.multiple) && u(Me(e, t, {
        target: e
    }, l())),
    e._x_removeModelListeners || (e._x_removeModelListeners = {}),
    e._x_removeModelListeners.default = d,
    r( () => e._x_removeModelListeners.default()),
    e.form) {
        let p = We(e.form, "reset", [], b => {
            rt( () => e._x_model && e._x_model.set(Me(e, t, {
                target: e
            }, l())))
        }
        );
        r( () => p())
    }
    e._x_model = {
        get() {
            return l()
        },
        set(p) {
            u(p)
        }
    },
    e._x_forceModelUpdate = p => {
        p === void 0 && typeof n == "string" && n.match(/\./) && (p = ""),
        window.fromModel = !0,
        x( () => fn(e, "value", p)),
        delete window.fromModel
    }
    ,
    i( () => {
        let p = l();
        t.includes("unintrusive") && document.activeElement.isSameNode(e) || e._x_forceModelUpdate(p)
    }
    )
}
);
function Me(e, t, n, i) {
    return x( () => {
        if (n instanceof CustomEvent && n.detail !== void 0)
            return n.detail !== null && n.detail !== void 0 ? n.detail : n.target.value;
        if (e.type === "checkbox")
            if (Array.isArray(i)) {
                let r = null;
                return t.includes("number") ? r = Ce(n.target.value) : t.includes("boolean") ? r = de(n.target.value) : r = n.target.value,
                n.target.checked ? i.includes(r) ? i : i.concat([r]) : i.filter(o => !ki(o, r))
            } else
                return n.target.checked;
        else {
            if (e.tagName.toLowerCase() === "select" && e.multiple)
                return t.includes("number") ? Array.from(n.target.selectedOptions).map(r => {
                    let o = r.value || r.text;
                    return Ce(o)
                }
                ) : t.includes("boolean") ? Array.from(n.target.selectedOptions).map(r => {
                    let o = r.value || r.text;
                    return de(o)
                }
                ) : Array.from(n.target.selectedOptions).map(r => r.value || r.text);
            {
                let r;
                return e.type === "radio" ? n.target.checked ? r = n.target.value : r = i : r = n.target.value,
                t.includes("number") ? Ce(r) : t.includes("boolean") ? de(r) : t.includes("trim") ? r.trim() : r
            }
        }
    }
    )
}
function Ce(e) {
    let t = e ? parseFloat(e) : null;
    return zi(t) ? t : e
}
function ki(e, t) {
    return e == t
}
function zi(e) {
    return !Array.isArray(e) && !isNaN(e)
}
function St(e) {
    return e !== null && typeof e == "object" && typeof e.get == "function" && typeof e.set == "function"
}
m("cloak", e => queueMicrotask( () => x( () => e.removeAttribute(U("cloak")))));
rn( () => `[${U("init")}]`);
m("init", R( (e, {expression: t}, {evaluate: n}) => typeof t == "string" ? !!t.trim() && n(t, {}, !1) : n(t, {}, !1)));
m("text", (e, {expression: t}, {effect: n, evaluateLater: i}) => {
    let r = i(t);
    n( () => {
        r(o => {
            x( () => {
                e.textContent = o
            }
            )
        }
        )
    }
    )
}
);
m("html", (e, {expression: t}, {effect: n, evaluateLater: i}) => {
    let r = i(t);
    n( () => {
        r(o => {
            x( () => {
                e.innerHTML = o,
                e._x_ignoreSelf = !0,
                M(e),
                delete e._x_ignoreSelf
            }
            )
        }
        )
    }
    )
}
);
et(Jt(":", Vt(U("bind:"))));
var Fn = (e, {value: t, modifiers: n, expression: i, original: r}, {effect: o, cleanup: s}) => {
    if (!t) {
        let l = {};
        Wr(l),
        v(e, i)(c => {
            bn(e, c, r)
        }
        , {
            scope: l
        });
        return
    }
    if (t === "key")
        return qi(e, i);
    if (e._x_inlineBindings && e._x_inlineBindings[t] && e._x_inlineBindings[t].extract)
        return;
    let a = v(e, i);
    o( () => a(l => {
        l === void 0 && typeof i == "string" && i.match(/\./) && (l = ""),
        x( () => fn(e, t, l, n))
    }
    )),
    s( () => {
        e._x_undoAddedClasses && e._x_undoAddedClasses(),
        e._x_undoAddedStyles && e._x_undoAddedStyles()
    }
    )
}
;
Fn.inline = (e, {value: t, modifiers: n, expression: i}) => {
    t && (e._x_inlineBindings || (e._x_inlineBindings = {}),
    e._x_inlineBindings[t] = {
        expression: i,
        extract: !1
    })
}
;
m("bind", Fn);
function qi(e, t) {
    e._x_keyExpression = t
}
nn( () => `[${U("data")}]`);
m("data", (e, {expression: t}, {cleanup: n}) => {
    if (Hi(e))
        return;
    t = t === "" ? "{}" : t;
    let i = {};
    Le(i, e);
    let r = {};
    Jr(r, i);
    let o = F(e, t, {
        scope: r
    });
    (o === void 0 || o === !0) && (o = {}),
    Le(o, e);
    let s = H(o);
    Dt(s);
    let a = te(e, s);
    s.init && F(e, s.init),
    n( () => {
        s.destroy && F(e, s.destroy),
        a()
    }
    )
}
);
be( (e, t) => {
    e._x_dataStack && (t._x_dataStack = e._x_dataStack,
    t.setAttribute("data-has-alpine-state", !0))
}
);
function Hi(e) {
    return $ ? ke ? !0 : e.hasAttribute("data-has-alpine-state") : !1
}
m("show", (e, {modifiers: t, expression: n}, {effect: i}) => {
    let r = v(e, n);
    e._x_doHide || (e._x_doHide = () => {
        x( () => {
            e.style.setProperty("display", "none", t.includes("important") ? "important" : void 0)
        }
        )
    }
    ),
    e._x_doShow || (e._x_doShow = () => {
        x( () => {
            e.style.length === 1 && e.style.display === "none" ? e.removeAttribute("style") : e.style.removeProperty("display")
        }
        )
    }
    );
    let o = () => {
        e._x_doHide(),
        e._x_isShown = !1
    }
    , s = () => {
        e._x_doShow(),
        e._x_isShown = !0
    }
    , a = () => setTimeout(s), l = Be(d => d ? s() : o(), d => {
        typeof e._x_toggleAndCascadeWithTransitions == "function" ? e._x_toggleAndCascadeWithTransitions(e, d, s, o) : d ? a() : o()
    }
    ), u, c = !0;
    i( () => r(d => {
        !c && d === u || (t.includes("immediate") && (d ? a() : o()),
        l(d),
        u = d,
        c = !1)
    }
    ))
}
);
m("for", (e, {expression: t}, {effect: n, cleanup: i}) => {
    let r = Ui(t)
      , o = v(e, r.items)
      , s = v(e, e._x_keyExpression || "index");
    e._x_prevKeys = [],
    e._x_lookup = {},
    n( () => Wi(e, r, o, s)),
    i( () => {
        Object.values(e._x_lookup).forEach(a => a.remove()),
        delete e._x_prevKeys,
        delete e._x_lookup
    }
    )
}
);
function Wi(e, t, n, i) {
    let r = s => typeof s == "object" && !Array.isArray(s)
      , o = e;
    n(s => {
        Ji(s) && s >= 0 && (s = Array.from(Array(s).keys(), f => f + 1)),
        s === void 0 && (s = []);
        let a = e._x_lookup
          , l = e._x_prevKeys
          , u = []
          , c = [];
        if (r(s))
            s = Object.entries(s).map( ([f,h]) => {
                let g = Ot(t, h, f, s);
                i(y => {
                    c.includes(y) && A("Duplicate key on x-for", e),
                    c.push(y)
                }
                , {
                    scope: {
                        index: f,
                        ...g
                    }
                }),
                u.push(g)
            }
            );
        else
            for (let f = 0; f < s.length; f++) {
                let h = Ot(t, s[f], f, s);
                i(g => {
                    c.includes(g) && A("Duplicate key on x-for", e),
                    c.push(g)
                }
                , {
                    scope: {
                        index: f,
                        ...h
                    }
                }),
                u.push(h)
            }
        let d = []
          , p = []
          , b = []
          , C = [];
        for (let f = 0; f < l.length; f++) {
            let h = l[f];
            c.indexOf(h) === -1 && b.push(h)
        }
        l = l.filter(f => !b.includes(f));
        let oe = "template";
        for (let f = 0; f < c.length; f++) {
            let h = c[f]
              , g = l.indexOf(h);
            if (g === -1)
                l.splice(f, 0, h),
                d.push([oe, f]);
            else if (g !== f) {
                let y = l.splice(f, 1)[0]
                  , w = l.splice(g - 1, 1)[0];
                l.splice(f, 0, w),
                l.splice(g, 0, y),
                p.push([y, w])
            } else
                C.push(h);
            oe = h
        }
        for (let f = 0; f < b.length; f++) {
            let h = b[f];
            a[h]._x_effects && a[h]._x_effects.forEach(Mt),
            a[h].remove(),
            a[h] = null,
            delete a[h]
        }
        for (let f = 0; f < p.length; f++) {
            let[h,g] = p[f]
              , y = a[h]
              , w = a[g]
              , k = document.createElement("div");
            x( () => {
                w || A('x-for ":key" is undefined or invalid', o, g, a),
                w.after(k),
                y.after(w),
                w._x_currentIfEl && w.after(w._x_currentIfEl),
                k.before(y),
                y._x_currentIfEl && y.after(y._x_currentIfEl),
                k.remove()
            }
            ),
            w._x_refreshXForScope(u[c.indexOf(g)])
        }
        for (let f = 0; f < d.length; f++) {
            let[h,g] = d[f]
              , y = h === "template" ? o : a[h];
            y._x_currentIfEl && (y = y._x_currentIfEl);
            let w = u[g]
              , k = c[g]
              , J = document.importNode(o.content, !0).firstElementChild
              , dt = H(w);
            te(J, dt, o),
            J._x_refreshXForScope = Bn => {
                Object.entries(Bn).forEach( ([Kn,kn]) => {
                    dt[Kn] = kn
                }
                )
            }
            ,
            x( () => {
                y.after(J),
                R( () => M(J))()
            }
            ),
            typeof k == "object" && A("x-for key cannot be an object, it must be a string or an integer", o),
            a[k] = J
        }
        for (let f = 0; f < C.length; f++)
            a[C[f]]._x_refreshXForScope(u[c.indexOf(C[f])]);
        o._x_prevKeys = c
    }
    )
}
function Ui(e) {
    let t = /,([^,\}\]]*)(?:,([^,\}\]]*))?$/
      , n = /^\s*\(|\)\s*$/g
      , i = /([\s\S]*?)\s+(?:in|of)\s+([\s\S]*)/
      , r = e.match(i);
    if (!r)
        return;
    let o = {};
    o.items = r[2].trim();
    let s = r[1].replace(n, "").trim()
      , a = s.match(t);
    return a ? (o.item = s.replace(t, "").trim(),
    o.index = a[1].trim(),
    a[2] && (o.collection = a[2].trim())) : o.item = s,
    o
}
function Ot(e, t, n, i) {
    let r = {};
    return /^\[.*\]$/.test(e.item) && Array.isArray(t) ? e.item.replace("[", "").replace("]", "").split(",").map(s => s.trim()).forEach( (s, a) => {
        r[s] = t[a]
    }
    ) : /^\{.*\}$/.test(e.item) && !Array.isArray(t) && typeof t == "object" ? e.item.replace("{", "").replace("}", "").split(",").map(s => s.trim()).forEach(s => {
        r[s] = t[s]
    }
    ) : r[e.item] = t,
    e.index && (r[e.index] = n),
    e.collection && (r[e.collection] = i),
    r
}
function Ji(e) {
    return !Array.isArray(e) && !isNaN(e)
}
function Dn() {}
Dn.inline = (e, {expression: t}, {cleanup: n}) => {
    let i = me(e);
    i._x_refs || (i._x_refs = {}),
    i._x_refs[t] = e,
    n( () => delete i._x_refs[t])
}
;
m("ref", Dn);
m("if", (e, {expression: t}, {effect: n, cleanup: i}) => {
    e.tagName.toLowerCase() !== "template" && A("x-if can only be used on a <template> tag", e);
    let r = v(e, t)
      , o = () => {
        if (e._x_currentIfEl)
            return e._x_currentIfEl;
        let a = e.content.cloneNode(!0).firstElementChild;
        return te(a, {}, e),
        x( () => {
            e.after(a),
            R( () => M(a))()
        }
        ),
        e._x_currentIfEl = a,
        e._x_undoIf = () => {
            I(a, l => {
                l._x_effects && l._x_effects.forEach(Mt)
            }
            ),
            a.remove(),
            delete e._x_currentIfEl
        }
        ,
        a
    }
      , s = () => {
        e._x_undoIf && (e._x_undoIf(),
        delete e._x_undoIf)
    }
    ;
    n( () => r(a => {
        a ? o() : s()
    }
    )),
    i( () => e._x_undoIf && e._x_undoIf())
}
);
m("id", (e, {expression: t}, {evaluate: n}) => {
    n(t).forEach(r => Ri(e, r))
}
);
be( (e, t) => {
    e._x_ids && (t._x_ids = e._x_ids)
}
);
et(Jt("@", Vt(U("on:"))));
m("on", R( (e, {value: t, modifiers: n, expression: i}, {cleanup: r}) => {
    let o = i ? v(e, i) : () => {}
    ;
    e.tagName.toLowerCase() === "template" && (e._x_forwardEvents || (e._x_forwardEvents = []),
    e._x_forwardEvents.includes(t) || e._x_forwardEvents.push(t));
    let s = We(e, t, n, a => {
        o( () => {}
        , {
            scope: {
                $event: a
            },
            params: [a]
        })
    }
    );
    r( () => s())
}
));
Ae("Collapse", "collapse", "collapse");
Ae("Intersect", "intersect", "intersect");
Ae("Focus", "trap", "focus");
Ae("Mask", "mask", "mask");
function Ae(e, t, n) {
    m(t, i => A(`You can't use [x-${t}] without first installing the "${e}" plugin here: https://alpinejs.dev/plugins/${n}`, i))
}
ie.setEvaluator(qt);
ie.setReactivityEngine({
    reactive: ft,
    effect: ri,
    release: ii,
    raw: _
});
var Vi = ie
  , Yi = Vi;
window.Alpine = Yi;
(function() {
    document.addEventListener("alpine:init", () => {
        Alpine.data("collapse", () => ({
            collapse: !1,
            collapseSidebar() {
                this.collapse = !this.collapse
            }
        })),
        Alpine.data("dropdown", (r=!1) => ({
            open: r,
            toggle() {
                this.open = !this.open
            }
        })),
        Alpine.data("modals", (r=!1) => ({
            open: r,
            toggle() {
                this.open = !this.open
            }
        })),
        Alpine.data("main", r => ({})),
        Alpine.store("app", {
            sidebar: !1,
            toggleSidebar() {
                this.sidebar = !this.sidebar
            },
            mode: Alpine.$persist("light"),
            sidebarMode: Alpine.$persist("light"),
            layout: Alpine.$persist("vertical"),
            direction: Alpine.$persist("ltr"),
            showSettings: !1,
            toggleMode(r) {
                r || (r = this.mode || "light"),
                this.mode = r
            },
            toggleFullScreen() {
                document.fullscreenElement ? document.exitFullscreen() : document.documentElement.requestFullscreen()
            },
            setLayout() {
                this.layout = this.layout || "vertical",
                this.mode = this.mode || "light",
                this.sidebarMode = this.sidebarMode || "light",
                this.direction = this.direction || "ltr",
                this.open = !1
            },
            resetLayout() {
                this.layout = "vertical",
                this.mode = "light",
                this.sidebarMode = "light",
                this.direction = "ltr",
                this.open = !1
            }
        });
        function e() {
            const r = window.location.pathname
              , o = {
                "project-dashboard.html": "dashboard",
                "ecommerce-dashboard.html": "dashboard",
                "index.html": "dashboard",
                "email.html": "apps",
                "chat.html": "apps",
                "contact.html": "apps",
                "invoice.html": "apps",
                "calendar.html": "apps",
                "ui-tabs.html": "components",
                "ui-accordions.html": "components",
                "ui-modals.html": "components",
                "ui-clipboard.html": "components",
                "ui-notification.html": "components",
                "ui-carousel.html": "components",
                "ui-pricing.html": "components",
                "ui-lightbox.html": "components",
                "ui-countdown.html": "components",
                "ui-counter.html": "components",
                "ui-flatpickr.html": "components",
                "ui-alerts.html": "elements",
                "ui-buttons.html": "elements",
                "ui-buttons-group.html": "elements",
                "ui-badge.html": "elements",
                "ui-breadcrumb.html": "elements",
                "ui-videos.html": "elements",
                "ui-images.html": "elements",
                "ui-dropdowns.html": "elements",
                "ui-typography.html": "elements",
                "ui-avatar.html": "elements",
                "ui-tooltips.html": "elements",
                "ui-loader.html": "elements",
                "ui-pagination.html": "elements",
                "ui-progress-bar.html": "elements",
                "chart.html": "chart",
                "icons.html": "icons",
                "drag-and-drop.html": "drag",
                "forms-basic.html": "forms",
                "forms-input-group.html": "forms",
                "forms-editors.html": "forms",
                "forms-validation.html": "forms",
                "forms-checkbox.html": "forms",
                "forms-radio.html": "forms",
                "forms-switches.html": "forms",
                "tables-basic.html": "table",
                "tables-datatables.html": "table",
                "tables-editable.html": "table",
                "pages-users-profile.html": "users",
                "pages-users-settings.html": "users",
                "blank.html": "pages",
                "pages-maintenance.html": "pages",
                "pages-coming-soon.html": "pages",
                "pages-404.html": "pages",
                "pages-500.html": "pages",
                "pages-503.html": "pages",
                "creative.html": "layout",
                "detached.html": "layout",
                "login.html": "authentication",
                "signup.html": "authentication",
                "reset-pw.html": "authentication"
            };
            for (const s in o)
                if (r.includes(s))
                    return o[s];
            return "unknown"
        }
        const t = localStorage.getItem("activeMenu")
          , n = t || e();
        Alpine.store("sidebar", {
            activeMenu: n,
            toggleMenu(r) {
                this.activeMenu = this.activeMenu === r ? null : r,
                localStorage.setItem("activeMenu", this.activeMenu)
            },
            setActiveMenuFromURL() {
                this.activeMenu = e(),
                localStorage.setItem("activeMenu", this.activeMenu)
            }
        });
        function i() {
            const r = window.location.pathname;
            document.querySelectorAll("a").forEach(s => {
                const a = s.getAttribute("href");
                r.includes(a) ? s.classList.add("active") : s.classList.remove("active")
            }
            )
        }
        i(),
        Alpine.data("sidebarMenu", () => ({
            init() {
                this.$store.sidebar.setActiveMenuFromURL(),
                i()
            },
            isActive(r) {
                return this.$store.sidebar.activeMenu === r
            },
            toggle(r) {
                this.$store.sidebar.toggleMenu(r)
            }
        }))
    }
    ),
    window.Alpine.start()
}
)();
