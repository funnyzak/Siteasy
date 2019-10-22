/*
* JQuery zTree core 3.0
* http://code.google.com/p/jquerytree/
*
* Copyright (c) 2010 Hunter.z (baby666.cn)
*
* Licensed same as jquery - MIT License
* http://www.opensource.org/licenses/mit-license.php
*
* email: hunter.z@263.net
* Date: 2012-01-10
*/
(function (l) {
    var D, E, F, G, H, I, o = {}, J = {}, r = {}, M = 0, K = { treeId: "", treeObj: null, view: { addDiyDom: null, autoCancelSelected: !0, dblClickExpand: !0, expandSpeed: "fast", fontCss: {}, nameIsHTML: !1, selectedMulti: !0, showIcon: !0, showLine: !0, showTitle: !0 }, data: { key: { children: "children", name: "name", title: "" }, simpleData: { enable: !1, idKey: "id", pIdKey: "pId", rootPId: null }, keep: { parent: !1, leaf: !1} }, async: { enable: !1, type: "post", dataType: "text", url: "", autoParam: [], otherParam: [], dataFilter: null }, callback: { beforeAsync: null, beforeClick: null,
        beforeRightClick: null, beforeMouseDown: null, beforeMouseUp: null, beforeExpand: null, beforeCollapse: null, onAsyncError: null, onAsyncSuccess: null, onNodeCreated: null, onClick: null, onRightClick: null, onMouseDown: null, onMouseUp: null, onExpand: null, onCollapse: null
    }
    }, s = [function (b) {
        var a = b.treeObj, c = f.event; a.unbind(c.NODECREATED); a.bind(c.NODECREATED, function (a, c, g) { k.apply(b.callback.onNodeCreated, [a, c, g]) }); a.unbind(c.CLICK); a.bind(c.CLICK, function (a, c, g, j) { k.apply(b.callback.onClick, [a, c, g, j]) }); a.unbind(c.EXPAND);
        a.bind(c.EXPAND, function (a, c, g) { k.apply(b.callback.onExpand, [a, c, g]) }); a.unbind(c.COLLAPSE); a.bind(c.COLLAPSE, function (a, c, g) { k.apply(b.callback.onCollapse, [a, c, g]) }); a.unbind(c.ASYNC_SUCCESS); a.bind(c.ASYNC_SUCCESS, function (a, c, g, j) { k.apply(b.callback.onAsyncSuccess, [a, c, g, j]) }); a.unbind(c.ASYNC_ERROR); a.bind(c.ASYNC_ERROR, function (a, c, g, j, h, f) { k.apply(b.callback.onAsyncError, [a, c, g, j, h, f]) })
    } ], p = [function (b) { var a = h.getCache(b); a || (a = {}, h.setCache(b, a)); a.nodes = []; a.doms = [] } ], v = [function (b, a, c,
d, e, g) {
        if (c) {
            var j = b.data.key.children; c.level = a; c.tId = b.treeId + "_" + ++M; c.parentTId = d ? d.tId : null; if (c[j] && c[j].length > 0) { if (typeof c.open == "string") c.open = k.eqs(c.open, "true"); c.open = !!c.open; c.isParent = !0 } else { c.open = !1; if (typeof c.isParent == "string") c.isParent = k.eqs(c.isParent, "true"); c.isParent = !!c.isParent } c.isFirstNode = e; c.isLastNode = g; c.getParentNode = function () { return h.getNodeCache(b, c.parentTId) }; c.getPreNode = function () { return h.getPreNode(b, c) }; c.getNextNode = function () {
                return h.getNextNode(b,
c)
            }; c.isAjaxing = !1; h.fixPIdKeyValue(b, c)
        } 
    } ], w = [function (b) {
        var a = b.target, c = o[b.data.treeId], d = "", e = null, g = "", j = "", i = null, l = null, m = null; if (k.eqs(b.type, "mousedown")) j = "mousedown"; else if (k.eqs(b.type, "mouseup")) j = "mouseup"; else if (k.eqs(b.type, "contextmenu")) j = "contextmenu"; else if (k.eqs(b.type, "click")) if (k.eqs(a.tagName, "button") && a.blur(), k.eqs(a.tagName, "button") && a.getAttribute("treeNode" + f.id.SWITCH) !== null) d = a.parentNode.id, g = "switchNode"; else {
            if (m = k.getMDom(c, a, [{ tagName: "a", attrName: "treeNode" +
f.id.A
            }])) d = m.parentNode.id, g = "clickNode"
        } else if (k.eqs(b.type, "dblclick") && (j = "dblclick", m = k.getMDom(c, a, [{ tagName: "a", attrName: "treeNode" + f.id.A}]))) d = m.parentNode.id, g = "switchNode"; if (j.length > 0 && d.length == 0 && (m = k.getMDom(c, a, [{ tagName: "a", attrName: "treeNode" + f.id.A}]))) d = m.parentNode.id; if (d.length > 0) switch (e = h.getNodeCache(c, d), g) {
            case "switchNode": e.isParent ? k.eqs(b.type, "click") || k.eqs(b.type, "dblclick") && k.apply(c.view.dblClickExpand, [c.treeId, e], c.view.dblClickExpand) ? i = D : g = "" : g = ""; break;
            case "clickNode": i = E
        } switch (j) { case "mousedown": l = F; break; case "mouseup": l = G; break; case "dblclick": l = H; break; case "contextmenu": l = I } return { stop: !1, node: e, nodeEventType: g, nodeEventCallback: i, treeEventType: j, treeEventCallback: l}
    } ], x = [function (b) { var a = h.getRoot(b); a || (a = {}, h.setRoot(b, a)); a.children = []; a.expandTriggerFlag = !1; a.curSelectedList = []; a.noSelection = !0; a.createdNodes = [] } ], y = [], z = [], A = [], B = [], C = [], h = { addNodeCache: function (b, a) { h.getCache(b).nodes[a.tId] = a }, addAfterA: function (b) { z.push(b) },
        addBeforeA: function (b) { y.push(b) }, addInnerAfterA: function (b) { B.push(b) }, addInnerBeforeA: function (b) { A.push(b) }, addInitBind: function (b) { s.push(b) }, addInitCache: function (b) { p.push(b) }, addInitNode: function (b) { v.push(b) }, addInitProxy: function (b) { w.push(b) }, addInitRoot: function (b) { x.push(b) }, addNodesData: function (b, a, c) { var d = b.data.key.children; a[d] || (a[d] = []); if (a[d].length > 0) a[d][a[d].length - 1].isLastNode = !1, i.setNodeLineIcos(b, a[d][a[d].length - 1]); a.isParent = !0; a[d] = a[d].concat(c) }, addSelectedNode: function (b,
a) { var c = h.getRoot(b); h.isSelectedNode(b, a) || c.curSelectedList.push(a) }, addCreatedNode: function (b, a) { (b.callback.onNodeCreated || b.view.addDiyDom) && h.getRoot(b).createdNodes.push(a) }, addZTreeTools: function (b) { C.push(b) }, exSetting: function (b) { l.extend(!0, K, b) }, fixPIdKeyValue: function (b, a) { b.data.simpleData.enable && (a[b.data.simpleData.pIdKey] = a.parentTId ? a.getParentNode()[b.data.simpleData.idKey] : b.data.simpleData.rootPId) }, getAfterA: function (b, a, c) { for (var d = 0, e = z.length; d < e; d++) z[d].apply(this, arguments) },
        getBeforeA: function (b, a, c) { for (var d = 0, e = y.length; d < e; d++) y[d].apply(this, arguments) }, getInnerAfterA: function (b, a, c) { for (var d = 0, e = B.length; d < e; d++) B[d].apply(this, arguments) }, getInnerBeforeA: function (b, a, c) { for (var d = 0, e = A.length; d < e; d++) A[d].apply(this, arguments) }, getCache: function (b) { return r[b.treeId] }, getNextNode: function (b, a) {
            if (!a) return null; var c = b.data.key.children, d = a.parentTId ? a.getParentNode() : h.getRoot(b); if (!a.isLastNode) if (a.isFirstNode) return d[c][1]; else for (var e = 1, g = d[c].length -
1; e < g; e++) if (d[c][e] === a) return d[c][e + 1]; return null
        }, getNodeByParam: function (b, a, c, d) { if (!a || !c) return null; for (var e = b.data.key.children, g = 0, j = a.length; g < j; g++) { if (a[g][c] == d) return a[g]; var f = h.getNodeByParam(b, a[g][e], c, d); if (f) return f } return null }, getNodeCache: function (b, a) { if (!a) return null; var c = r[b.treeId].nodes[a]; return c ? c : null }, getNodes: function (b) { return h.getRoot(b)[b.data.key.children] }, getNodesByParam: function (b, a, c, d) {
            if (!a || !c) return []; for (var e = b.data.key.children, g = [], j = 0, f =
a.length; j < f; j++) a[j][c] == d && g.push(a[j]), g = g.concat(h.getNodesByParam(b, a[j][e], c, d)); return g
        }, getNodesByParamFuzzy: function (b, a, c, d) { if (!a || !c) return []; for (var e = b.data.key.children, g = [], j = 0, f = a.length; j < f; j++) typeof a[j][c] == "string" && a[j][c].indexOf(d) > -1 && g.push(a[j]), g = g.concat(h.getNodesByParamFuzzy(b, a[j][e], c, d)); return g }, getPreNode: function (b, a) {
            if (!a) return null; var c = b.data.key.children, d = a.parentTId ? a.getParentNode() : h.getRoot(b); if (!a.isFirstNode) if (a.isLastNode) return d[c][d[c].length -
2]; else for (var e = 1, g = d[c].length - 1; e < g; e++) if (d[c][e] === a) return d[c][e - 1]; return null
        }, getRoot: function (b) { return b ? J[b.treeId] : null }, getSetting: function (b) { return o[b] }, getSettings: function () { return o }, getTitleKey: function (b) { return b.data.key.title === "" ? b.data.key.name : b.data.key.title }, getZTreeTools: function (b) { return (b = this.getRoot(this.getSetting(b))) ? b.treeTools : null }, initCache: function (b) { for (var a = 0, c = p.length; a < c; a++) p[a].apply(this, arguments) }, initNode: function (b, a, c, d, e, g) {
            for (var j =
0, f = v.length; j < f; j++) v[j].apply(this, arguments)
        }, initRoot: function (b) { for (var a = 0, c = x.length; a < c; a++) x[a].apply(this, arguments) }, isSelectedNode: function (b, a) { for (var c = h.getRoot(b), d = 0, e = c.curSelectedList.length; d < e; d++) if (a === c.curSelectedList[d]) return !0; return !1 }, removeNodeCache: function (b, a) { var c = b.data.key.children; if (a[c]) for (var d = 0, e = a[c].length; d < e; d++) arguments.callee(b, a[c][d]); delete h.getCache(b).nodes[a.tId] }, removeSelectedNode: function (b, a) {
            for (var c = h.getRoot(b), d = 0, e = c.curSelectedList.length; d <
e; d++) if (a === c.curSelectedList[d] || !h.getNodeCache(b, c.curSelectedList[d].tId)) c.curSelectedList.splice(d, 1), d--, e--
        }, setCache: function (b, a) { r[b.treeId] = a }, setRoot: function (b, a) { J[b.treeId] = a }, setZTreeTools: function (b, a) { for (var c = 0, d = C.length; c < d; c++) C[c].apply(this, arguments) }, transformToArrayFormat: function (b, a) {
            if (!a) return []; var c = b.data.key.children, d = []; if (k.isArray(a)) for (var e = 0, g = a.length; e < g; e++) d.push(a[e]), a[e][c] && (d = d.concat(h.transformToArrayFormat(b, a[e][c]))); else d.push(a), a[c] &&
(d = d.concat(h.transformToArrayFormat(b, a[c]))); return d
        }, transformTozTreeFormat: function (b, a) { var c, d, e = b.data.simpleData.idKey, g = b.data.simpleData.pIdKey, j = b.data.key.children; if (!e || e == "" || !a) return []; if (k.isArray(a)) { var f = [], h = []; for (c = 0, d = a.length; c < d; c++) h[a[c][e]] = a[c]; for (c = 0, d = a.length; c < d; c++) h[a[c][g]] && a[c][e] != a[c][g] ? (h[a[c][g]][j] || (h[a[c][g]][j] = []), h[a[c][g]][j].push(a[c])) : f.push(a[c]); return f } else return [a] } 
    }, m = { bindEvent: function (b) {
        for (var a = 0, c = s.length; a < c; a++) s[a].apply(this,
arguments)
    }, bindTree: function (b) { var a = { treeId: b.treeId }, b = b.treeObj; b.unbind("click", m.proxy); b.bind("click", a, m.proxy); b.unbind("dblclick", m.proxy); b.bind("dblclick", a, m.proxy); b.unbind("mouseover", m.proxy); b.bind("mouseover", a, m.proxy); b.unbind("mouseout", m.proxy); b.bind("mouseout", a, m.proxy); b.unbind("mousedown", m.proxy); b.bind("mousedown", a, m.proxy); b.unbind("mouseup", m.proxy); b.bind("mouseup", a, m.proxy); b.unbind("contextmenu", m.proxy); b.bind("contextmenu", a, m.proxy) }, doProxy: function (b) {
        for (var a =
[], c = 0, d = w.length; c < d; c++) { var e = w[c].apply(this, arguments); a.push(e); if (e.stop) break } return a
    }, proxy: function (b) { var a = h.getSetting(b.data.treeId); if (!k.uCanDo(a, b)) return !0; for (var c = m.doProxy(b), d = !0, e = !1, g = 0, j = c.length; g < j; g++) { var f = c[g]; f.nodeEventCallback && (e = !0, d = f.nodeEventCallback.apply(f, [b, f.node]) && d); f.treeEventCallback && (e = !0, d = f.treeEventCallback.apply(f, [b, f.node]) && d) } try { e && l("input:focus").length == 0 && k.noSel(a) } catch (i) { } return d } 
    }; D = function (b, a) {
        var c = o[b.data.treeId]; if (a.open) {
            if (k.apply(c.callback.beforeCollapse,
[c.treeId, a], !0) == !1) return !0
        } else if (k.apply(c.callback.beforeExpand, [c.treeId, a], !0) == !1) return !0; h.getRoot(c).expandTriggerFlag = !0; i.switchNode(c, a); return !0
    }; E = function (b, a) {
        var c = o[b.data.treeId], d = c.view.autoCancelSelected && b.ctrlKey && h.isSelectedNode(c, a) ? 0 : c.view.autoCancelSelected && b.ctrlKey && c.view.selectedMulti ? 2 : 1; if (k.apply(c.callback.beforeClick, [c.treeId, a, d], !0) == !1) return !0; d === 0 ? i.cancelPreSelectedNode(c, a) : i.selectNode(c, a, d === 2); c.treeObj.trigger(f.event.CLICK, [c.treeId, a, d]);
        return !0
    }; F = function (b, a) { var c = o[b.data.treeId]; k.apply(c.callback.beforeMouseDown, [c.treeId, a], !0) && k.apply(c.callback.onMouseDown, [b, c.treeId, a]); return !0 }; G = function (b, a) { var c = o[b.data.treeId]; k.apply(c.callback.beforeMouseUp, [c.treeId, a], !0) && k.apply(c.callback.onMouseUp, [b, c.treeId, a]); return !0 }; H = function (b, a) { var c = o[b.data.treeId]; k.apply(c.callback.beforeDblClick, [c.treeId, a], !0) && k.apply(c.callback.onDblClick, [b, c.treeId, a]); return !0 }; I = function (b, a) {
        var c = o[b.data.treeId]; k.apply(c.callback.beforeRightClick,
[c.treeId, a], !0) && k.apply(c.callback.onRightClick, [b, c.treeId, a]); return typeof c.callback.onRightClick != "function"
    }; var k = { apply: function (b, a, c) { return typeof b == "function" ? b.apply(L, a ? a : []) : c }, clone: function (b) { var a; if (b instanceof Array) { a = []; for (var c = b.length; c--; ) a[c] = arguments.callee(b[c]); return a } else if (typeof b == "function") return b; else if (b instanceof Object) { a = {}; for (c in b) a[c] = arguments.callee(b[c]); return a } else return b }, eqs: function (b, a) { return b.toLowerCase() === a.toLowerCase() },
        isArray: function (b) { return Object.prototype.toString.apply(b) === "[object Array]" }, getMDom: function (b, a, c) { if (!a) return null; for (; a && a.id !== b.treeId; ) { for (var d = 0, e = c.length; a.tagName && d < e; d++) if (k.eqs(a.tagName, c[d].tagName) && a.getAttribute(c[d].attrName) !== null) return a; a = a.parentNode } return null }, noSel: function (b) { if (h.getRoot(b).noSelection) try { window.getSelection ? window.getSelection().removeAllRanges() : document.selection.empty() } catch (a) { } }, uCanDo: function () { return !0 } 
    }, i = { addNodes: function (b,
a, c, d) { if (!b.data.keep.leaf || !a || a.isParent) if (k.isArray(c) || (c = [c]), b.data.simpleData.enable && (c = h.transformTozTreeFormat(b, c)), a) { var e = l("#" + a.tId + f.id.SWITCH), g = l("#" + a.tId + f.id.ICON), j = l("#" + a.tId + f.id.UL); if (!a.open) i.replaceSwitchClass(a, e, f.folder.CLOSE), i.replaceIcoClass(a, g, f.folder.CLOSE), a.open = !1, j.css({ display: "none" }); h.addNodesData(b, a, c); i.createNodes(b, a.level + 1, c, a); d || i.expandCollapseParentNode(b, a, !0) } else h.addNodesData(b, h.getRoot(b), c), i.createNodes(b, 0, c, null) }, appendNodes: function (b,
a, c, d, e, g) {
    if (!c) return []; for (var j = [], l = b.data.key.children, m = b.data.key.name, o = h.getTitleKey(b), t = 0, N = c.length; t < N; t++) {
        var n = c[t], u = (d ? d : h.getRoot(b))[l].length == c.length && t == 0, q = t == c.length - 1; e && (h.initNode(b, a, n, d, u, q, g), h.addNodeCache(b, n)); u = []; n[l] && n[l].length > 0 && (u = i.appendNodes(b, a + 1, n[l], n, e, g && n.open)); if (g) {
            var q = i.makeNodeUrl(b, n), r = i.makeNodeFontCss(b, n), s = [], p; for (p in r) s.push(p, ":", r[p], ";"); j.push("<li id='", n.tId, "' class='level", n.level, "' treenode>", "<button type='button' hidefocus='true'",
n.isParent ? "" : "disabled", " id='", n.tId, f.id.SWITCH, "' title='' class='", i.makeNodeLineClass(b, n), "' treeNode", f.id.SWITCH, "></button>"); h.getBeforeA(b, n, j); j.push("<a id='", n.tId, f.id.A, "' class='level", n.level, "' treeNode", f.id.A, ' onclick="', n.click || "", '" ', q != null && q.length > 0 ? "href='" + q + "'" : "", " target='", i.makeNodeTarget(n), "' style='", s.join(""), "'"); k.apply(b.view.showTitle, [b.treeId, n], b.view.showTitle) && j.push("title='", n[o].replace(/'/g, "&#39;"), "'"); j.push(">"); h.getInnerBeforeA(b, n, j);
            q = b.view.nameIsHTML ? n[m] : n[m].replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;"); j.push("<button type='button' hidefocus='true' id='", n.tId, f.id.ICON, "' title='' treeNode", f.id.ICON, " class='", i.makeNodeIcoClass(b, n), "' style='", i.makeNodeIcoStyle(b, n), "'></button><span id='", n.tId, f.id.SPAN, "'>", q, "</span>"); h.getInnerAfterA(b, n, j); j.push("</a>"); h.getAfterA(b, n, j); n.isParent && n.open && i.makeUlHtml(b, n, j, u.join("")); j.push("</li>"); h.addCreatedNode(b, n)
        } 
    } return j
}, appendParentULDom: function (b,
a) { var c = [], d = l("#" + a.tId), e = l("#" + a.tId + f.id.UL), g = i.appendNodes(b, a.level + 1, a[b.data.key.children], a, !1, !0); i.makeUlHtml(b, a, c, g.join("")); !d.get(0) && a.parentTId && (i.appendParentULDom(b, a.getParentNode()), d = l("#" + a.tId)); e.get(0) && e.remove(); d.append(c.join("")); i.createNodeCallback(b) }, asyncNode: function (b, a, c, d) {
    var e, g; if (a && !a.isParent) return k.apply(d), !1; else if (a && a.isAjaxing) return !1; else if (k.apply(b.callback.beforeAsync, [b.treeId, a], !0) == !1) return k.apply(d), !1; if (a) a.isAjaxing = !0, l("#" +
a.tId + f.id.ICON).attr({ style: "", "class": "ico_loading" }); var j = ""; for (e = 0, g = b.async.autoParam.length; a && e < g; e++) { var h = b.async.autoParam[e].split("="), m = h; h.length > 1 && (m = h[1], h = h[0]); j += (j.length > 0 ? "&" : "") + m + "=" + a[h] } if (k.isArray(b.async.otherParam)) for (e = 0, g = b.async.otherParam.length; e < g; e += 2) j += (j.length > 0 ? "&" : "") + b.async.otherParam[e] + "=" + b.async.otherParam[e + 1]; else for (var o in b.async.otherParam) j += (j.length > 0 ? "&" : "") + o + "=" + b.async.otherParam[o]; l.ajax({ type: b.async.type, url: k.apply(b.async.url,
[b.treeId, a], b.async.url), data: j, dataType: b.async.dataType, success: function (e) { var g = []; try { g = !e || e.length == 0 ? [] : typeof e == "string" ? eval("(" + e + ")") : e } catch (j) { } if (a) a.isAjaxing = null; i.setNodeLineIcos(b, a); g && g != "" ? (g = k.apply(b.async.dataFilter, [b.treeId, a, g], g), i.addNodes(b, a, k.clone(g), !!c)) : i.addNodes(b, a, [], !!c); b.treeObj.trigger(f.event.ASYNC_SUCCESS, [b.treeId, a, e]); k.apply(d) }, error: function (c, d, e) {
    i.setNodeLineIcos(b, a); if (a) a.isAjaxing = null; b.treeObj.trigger(f.event.ASYNC_ERROR, [b.treeId, a,
c, d, e])
} 
}); return !0
}, cancelPreSelectedNode: function (b, a) { for (var c = h.getRoot(b).curSelectedList, d = c.length - 1; d >= 0; d--) if (!a || a === c[d]) if (l("#" + c[d].tId + f.id.A).removeClass(f.node.CURSELECTED), i.setNodeName(b, c[d]), a) { h.removeSelectedNode(b, a); break } if (!a) h.getRoot(b).curSelectedList = [] }, createNodeCallback: function (b) {
    if (b.callback.onNodeCreated || b.view.addDiyDom) for (var a = h.getRoot(b); a.createdNodes.length > 0; ) {
        var c = a.createdNodes.shift(); k.apply(b.view.addDiyDom, [b.treeId, c]); b.callback.onNodeCreated &&
b.treeObj.trigger(f.event.NODECREATED, [b.treeId, c])
    } 
}, createNodes: function (b, a, c, d) { if (c && c.length != 0) { var e = h.getRoot(b), g = b.data.key.children, g = !d || d.open || !!l("#" + d[g][0].tId).get(0); e.createdNodes = []; a = i.appendNodes(b, a, c, d, !0, g); d ? (d = l("#" + d.tId + f.id.UL), d.get(0) && d.append(a.join(""))) : b.treeObj.append(a.join("")); i.createNodeCallback(b) } }, expandCollapseNode: function (b, a, c, d, e) {
    var g = h.getRoot(b), j = b.data.key.children; if (a) {
        if (g.expandTriggerFlag) {
            var m = e, e = function () {
                m && m(); a.open ? b.treeObj.trigger(f.event.EXPAND,
[b.treeId, a]) : b.treeObj.trigger(f.event.COLLAPSE, [b.treeId, a])
            }; g.expandTriggerFlag = !1
        } if (a.open == c) k.apply(e, []); else {
            !a.open && a.isParent && (!l("#" + a.tId + f.id.UL).get(0) || a[j] && a[j].length > 0 && !l("#" + a[j][0].tId).get(0)) && i.appendParentULDom(b, a); var c = l("#" + a.tId + f.id.UL), g = l("#" + a.tId + f.id.SWITCH), o = l("#" + a.tId + f.id.ICON); a.isParent ? (a.open = !a.open, a.iconOpen && a.iconClose && o.attr("style", i.makeNodeIcoStyle(b, a)), a.open ? (i.replaceSwitchClass(a, g, f.folder.OPEN), i.replaceIcoClass(a, o, f.folder.OPEN),
d == !1 || b.view.expandSpeed == "" ? (c.show(), k.apply(e, [])) : a[j] && a[j].length > 0 ? c.slideDown(b.view.expandSpeed, e) : (c.show(), k.apply(e, []))) : (i.replaceSwitchClass(a, g, f.folder.CLOSE), i.replaceIcoClass(a, o, f.folder.CLOSE), d == !1 || b.view.expandSpeed == "" ? (c.hide(), k.apply(e, [])) : c.slideUp(b.view.expandSpeed, e))) : k.apply(e, [])
        } 
    } else k.apply(e, [])
}, expandCollapseParentNode: function (b, a, c, d, e) {
    a && (a.parentTId ? (i.expandCollapseNode(b, a, c, d), a.parentTId && i.expandCollapseParentNode(b, a.getParentNode(), c, d, e)) : i.expandCollapseNode(b,
a, c, d, e))
}, expandCollapseSonNode: function (b, a, c, d, e) { var g = h.getRoot(b), f = b.data.key.children, g = a ? a[f] : g[f], f = a ? !1 : d, k = h.getRoot(b).expandTriggerFlag; h.getRoot(b).expandTriggerFlag = !1; if (g) for (var l = 0, m = g.length; l < m; l++) g[l] && i.expandCollapseSonNode(b, g[l], c, f); h.getRoot(b).expandTriggerFlag = k; i.expandCollapseNode(b, a, c, d, e) }, makeNodeFontCss: function (b, a) { var c = k.apply(b.view.fontCss, [b.treeId, a], b.view.fontCss); return c && typeof c != "function" ? c : {} }, makeNodeIcoClass: function (b, a) {
    var c = ["ico"]; a.isAjaxing ||
(c[0] = (a.iconSkin ? a.iconSkin + "_" : "") + c[0], a.isParent ? c.push(a.open ? f.folder.OPEN : f.folder.CLOSE) : c.push(f.folder.DOCU)); return c.join("_")
}, makeNodeIcoStyle: function (b, a) { var c = []; if (!a.isAjaxing) { var d = a.isParent && a.iconOpen && a.iconClose ? a.open ? a.iconOpen : a.iconClose : a.icon; d && c.push("background:url(", d, ") 0 0 no-repeat;"); (b.view.showIcon == !1 || !k.apply(b.view.showIcon, [b.treeId, a], !0)) && c.push("width:0px;height:0px;") } return c.join("") }, makeNodeLineClass: function (b, a) {
    var c = []; b.view.showLine ? a.level ==
0 && a.isFirstNode && a.isLastNode ? c.push(f.line.ROOT) : a.level == 0 && a.isFirstNode ? c.push(f.line.ROOTS) : a.isLastNode ? c.push(f.line.BOTTOM) : c.push(f.line.CENTER) : c.push(f.line.NOLINE); a.isParent ? c.push(a.open ? f.folder.OPEN : f.folder.CLOSE) : c.push(f.folder.DOCU); return i.makeNodeLineClassEx(a) + c.join("_")
}, makeNodeLineClassEx: function (b) { return "level" + b.level + " switch " }, makeNodeTarget: function (b) { return b.target || "_blank" }, makeNodeUrl: function (b, a) { return a.url ? a.url : null }, makeUlHtml: function (b, a, c, d) {
    c.push("<ul id='",
a.tId, f.id.UL, "' class='level", a.level, " ", i.makeUlLineClass(b, a), "' style='display:", a.open ? "block" : "none", "'>"); c.push(d); c.push("</ul>")
}, makeUlLineClass: function (b, a) { return b.view.showLine && !a.isLastNode ? f.line.LINE : "" }, replaceIcoClass: function (b, a, c) { if (a && !b.isAjaxing && (b = a.attr("class"), b != void 0)) { b = b.split("_"); switch (c) { case f.folder.OPEN: case f.folder.CLOSE: case f.folder.DOCU: b[b.length - 1] = c } a.attr("class", b.join("_")) } }, replaceSwitchClass: function (b, a, c) {
    if (a) {
        var d = a.attr("class"); if (d !=
void 0) { d = d.split("_"); switch (c) { case f.line.ROOT: case f.line.ROOTS: case f.line.CENTER: case f.line.BOTTOM: case f.line.NOLINE: d[0] = i.makeNodeLineClassEx(b) + c; break; case f.folder.OPEN: case f.folder.CLOSE: case f.folder.DOCU: d[1] = c } a.attr("class", d.join("_")); c !== f.folder.DOCU ? a.removeAttr("disabled") : a.attr("disabled", "disabled") } 
    } 
}, selectNode: function (b, a, c) { c || i.cancelPreSelectedNode(b); l("#" + a.tId + f.id.A).addClass(f.node.CURSELECTED); h.addSelectedNode(b, a) }, setNodeFontCss: function (b, a) {
    var c = l("#" +
a.tId + f.id.A), d = i.makeNodeFontCss(b, a); d && c.css(d)
}, setNodeLineIcos: function (b, a) { if (a) { var c = l("#" + a.tId + f.id.SWITCH), d = l("#" + a.tId + f.id.UL), e = l("#" + a.tId + f.id.ICON), g = i.makeUlLineClass(b, a); g.length == 0 ? d.removeClass(f.line.LINE) : d.addClass(g); c.attr("class", i.makeNodeLineClass(b, a)); a.isParent ? c.removeAttr("disabled") : c.attr("disabled", "disabled"); e.removeAttr("style"); e.attr("style", i.makeNodeIcoStyle(b, a)); e.attr("class", i.makeNodeIcoClass(b, a)) } }, setNodeName: function (b, a) {
    var c = b.data.key.name,
d = h.getTitleKey(b), e = l("#" + a.tId + f.id.SPAN); e.empty(); b.view.nameIsHTML ? e.html(a[c]) : e.text(a[c]); k.apply(b.view.showTitle, [b.treeId, a], b.view.showTitle) && l("#" + a.tId + f.id.A).attr("title", a[d])
}, setNodeTarget: function (b) { l("#" + b.tId + f.id.A).attr("target", i.makeNodeTarget(b)) }, setNodeUrl: function (b, a) { var c = l("#" + a.tId + f.id.A), d = i.makeNodeUrl(b, a); d == null || d.length == 0 ? c.removeAttr("href") : c.attr("href", d) }, switchNode: function (b, a) {
    var c = b.data.key.children; a.open || a && a[c] && a[c].length > 0 ? i.expandCollapseNode(b,
a, !a.open) : b.async.enable ? i.asyncNode(b, a) || i.expandCollapseNode(b, a, !a.open) : a && i.expandCollapseNode(b, a, !a.open)
} 
    }; l.fn.zTree = { consts: { event: { NODECREATED: "ztree_nodeCreated", CLICK: "ztree_click", EXPAND: "ztree_expand", COLLAPSE: "ztree_collapse", ASYNC_SUCCESS: "ztree_async_success", ASYNC_ERROR: "ztree_async_error" }, id: { A: "_a", ICON: "_ico", SPAN: "_span", SWITCH: "_switch", UL: "_ul" }, line: { ROOT: "root", ROOTS: "roots", CENTER: "center", BOTTOM: "bottom", NOLINE: "noline", LINE: "line" }, folder: { OPEN: "open", CLOSE: "close",
        DOCU: "docu"
    }, node: { CURSELECTED: "curSelectedNode"}
    }, _z: { tools: k, view: i, event: m, data: h }, getZTreeObj: function (b) { return (b = h.getZTreeTools(b)) ? b : null }, init: function (b, a, c) {
        var d = k.clone(K); l.extend(!0, d, a); d.treeId = b.attr("id"); d.treeObj = b; d.treeObj.empty(); o[d.treeId] = d; if (l.browser.msie && parseInt(l.browser.version) < 7) d.view.expandSpeed = ""; h.initRoot(d); b = h.getRoot(d); a = d.data.key.children; c = c ? k.clone(k.isArray(c) ? c : [c]) : []; b[a] = d.data.simpleData.enable ? h.transformTozTreeFormat(d, c) : c; h.initCache(d);
        m.bindTree(d); m.bindEvent(d); c = { setting: d, cancelSelectedNode: function (a) { i.cancelPreSelectedNode(this.setting, a) }, expandAll: function (a) { a = !!a; i.expandCollapseSonNode(this.setting, null, a, !0); return a }, expandNode: function (a, b, c, m, o) {
            if (!a || !a.isParent) return null; b !== !0 && b !== !1 && (b = !a.open); if ((o = !!o) && b && k.apply(d.callback.beforeExpand, [d.treeId, a], !0) == !1) return null; else if (o && !b && k.apply(d.callback.beforeCollapse, [d.treeId, a], !0) == !1) return null; b && a.parentTId && i.expandCollapseParentNode(this.setting,
a.getParentNode(), b, !1); if (b === a.open && !c) return null; h.getRoot(d).expandTriggerFlag = o; c ? i.expandCollapseSonNode(this.setting, a, b, !0, function () { m !== !1 && l("#" + a.tId + f.id.ICON).focus().blur() }) : (a.open = !b, i.switchNode(this.setting, a), m !== !1 && l("#" + a.tId + f.id.ICON).focus().blur()); return b
        }, getNodes: function () { return h.getNodes(this.setting) }, getNodeByParam: function (a, b, c) { return !a ? null : h.getNodeByParam(this.setting, c ? c[this.setting.data.key.children] : h.getNodes(this.setting), a, b) }, getNodeByTId: function (a) {
            return h.getNodeCache(this.setting,
a)
        }, getNodesByParam: function (a, b, c) { return !a ? null : h.getNodesByParam(this.setting, c ? c[this.setting.data.key.children] : h.getNodes(this.setting), a, b) }, getNodesByParamFuzzy: function (a, b, c) { return !a ? null : h.getNodesByParamFuzzy(this.setting, c ? c[this.setting.data.key.children] : h.getNodes(this.setting), a, b) }, getNodeIndex: function (a) { if (!a) return null; for (var b = d.data.key.children, c = a.parentTId ? a.getParentNode() : h.getRoot(this.setting), f = 0, i = c[b].length; f < i; f++) if (c[b][f] == a) return f; return -1 }, getSelectedNodes: function () {
            for (var a =
[], b = h.getRoot(this.setting).curSelectedList, c = 0, d = b.length; c < d; c++) a.push(b[c]); return a
        }, isSelectedNode: function (a) { return h.isSelectedNode(this.setting, a) }, reAsyncChildNodes: function (a, b, c) { if (this.setting.async.enable) { var d = !a; d && (a = h.getRoot(this.setting)); b == "refresh" && (a[this.setting.data.key.children] = [], d ? this.setting.treeObj.empty() : l("#" + a.tId + f.id.UL).empty()); i.asyncNode(this.setting, d ? null : a, !!c) } }, refresh: function () {
            this.setting.treeObj.empty(); var a = h.getRoot(this.setting), b = a[this.setting.data.key.children];
            h.initRoot(this.setting); a[this.setting.data.key.children] = b; h.initCache(this.setting); i.createNodes(this.setting, 0, a[this.setting.data.key.children])
        }, selectNode: function (a, b) { a && k.uCanDo(this.setting) && (b = d.view.selectedMulti && b, a.parentTId ? i.expandCollapseParentNode(this.setting, a.getParentNode(), !0, !1, function () { l("#" + a.tId + f.id.ICON).focus().blur() }) : l("#" + a.tId + f.id.ICON).focus().blur(), i.selectNode(this.setting, a, b)) }, transformTozTreeNodes: function (a) {
            return h.transformTozTreeFormat(this.setting,
a)
        }, transformToArray: function (a) { return h.transformToArrayFormat(this.setting, a) }, updateNode: function (a) { a && l("#" + a.tId).get(0) && k.uCanDo(this.setting) && (i.setNodeName(this.setting, a), i.setNodeTarget(a), i.setNodeUrl(this.setting, a), i.setNodeLineIcos(this.setting, a), i.setNodeFontCss(this.setting, a)) } 
        }; b.treeTools = c; h.setZTreeTools(d, c); b[a] && b[a].length > 0 ? i.createNodes(d, 0, b[a]) : d.async.enable && d.async.url && d.async.url !== "" && i.asyncNode(d); return c
    } 
    }; var L = l.fn.zTree, f = L.consts
})(jQuery);

/*
* JQuery zTree excheck 3.0
* http://code.google.com/p/jquerytree/
*
* Copyright (c) 2010 Hunter.z (baby666.cn)
*
* Licensed same as jquery - MIT License
* http://www.opensource.org/licenses/mit-license.php
*
* email: hunter.z@263.net
* Date: 2012-01-10
*/
(function (h) {
    var p, q, r, o = { event: { CHECK: "ztree_check" }, id: { CHECK: "_check" }, checkbox: { STYLE: "checkbox", DEFAULT: "chk", DISABLED: "disable", FALSE: "false", TRUE: "true", FULL: "full", PART: "part", FOCUS: "focus" }, radio: { STYLE: "radio", TYPE_ALL: "all", TYPE_LEVEL: "level"} }, t = { check: { enable: !1, autoCheckTrigger: !1, chkStyle: o.checkbox.STYLE, nocheckInherit: !1, radioType: o.radio.TYPE_LEVEL, chkboxType: { Y: "ps", N: "ps"} }, data: { key: { checked: "checked"} }, callback: { beforeCheck: null, onCheck: null} }; p = function (c, a) {
        if (a.chkDisabled ===
!0) return !1; var b = g.getSetting(c.data.treeId), d = b.data.key.checked; if (n.apply(b.callback.beforeCheck, [b.treeId, a], !0) == !1) return !0; a[d] = !a[d]; e.checkNodeRelation(b, a); d = h("#" + a.tId + j.id.CHECK); e.setChkClass(b, d, a); e.repairParentChkClassWithSelf(b, a); b.treeObj.trigger(j.event.CHECK, [b.treeId, a]); return !0
    }; q = function (c, a) { if (a.chkDisabled === !0) return !1; var b = g.getSetting(c.data.treeId), d = h("#" + a.tId + j.id.CHECK); a.check_Focus = !0; e.setChkClass(b, d, a); return !0 }; r = function (c, a) {
        if (a.chkDisabled === !0) return !1;
        var b = g.getSetting(c.data.treeId), d = h("#" + a.tId + j.id.CHECK); a.check_Focus = !1; e.setChkClass(b, d, a); return !0
    }; h.extend(!0, h.fn.zTree.consts, o); h.extend(!0, h.fn.zTree._z, { tools: {}, view: { checkNodeRelation: function (c, a) {
        var b, d, f, k = c.data.key.children, l = c.data.key.checked; b = j.radio; if (c.check.chkStyle == b.STYLE) {
            var i = g.getRadioCheckedList(c); if (a[l]) if (c.check.radioType == b.TYPE_ALL) {
                for (d = i.length - 1; d >= 0; d--) b = i[d], b[l] = !1, i.splice(d, 1), e.setChkClass(c, h("#" + b.tId + j.id.CHECK), b), b.parentTId != a.parentTId &&
e.repairParentChkClassWithSelf(c, b); i.push(a)
            } else { i = a.parentTId ? a.getParentNode() : g.getRoot(c); for (d = 0, f = i[k].length; d < f; d++) b = i[k][d], b[l] && b != a && (b[l] = !1, e.setChkClass(c, h("#" + b.tId + j.id.CHECK), b)) } else if (c.check.radioType == b.TYPE_ALL) for (d = 0, f = i.length; d < f; d++) if (a == i[d]) { i.splice(d, 1); break } 
        } else a[l] && (!a[k] || a[k].length == 0 || c.check.chkboxType.Y.indexOf("s") > -1) && e.setSonNodeCheckBox(c, a, !0), !a[l] && (!a[k] || a[k].length == 0 || c.check.chkboxType.N.indexOf("s") > -1) && e.setSonNodeCheckBox(c, a, !1),
a[l] && c.check.chkboxType.Y.indexOf("p") > -1 && e.setParentNodeCheckBox(c, a, !0), !a[l] && c.check.chkboxType.N.indexOf("p") > -1 && e.setParentNodeCheckBox(c, a, !1)
    }, makeChkClass: function (c, a) {
        var b = c.data.key.checked, d = j.checkbox, f = j.radio, k = "", k = a.chkDisabled === !0 ? d.DISABLED : a.halfCheck ? d.PART : c.check.chkStyle == f.STYLE ? a.check_Child_State < 1 ? d.FULL : d.PART : a[b] ? a.check_Child_State === 2 || a.check_Child_State === -1 ? d.FULL : d.PART : a.check_Child_State < 1 ? d.FULL : d.PART, b = c.check.chkStyle + "_" + (a[b] ? d.TRUE : d.FALSE) + "_" +
k, b = a.check_Focus && a.chkDisabled !== !0 ? b + "_" + d.FOCUS : b; return d.DEFAULT + " " + b
    }, repairAllChk: function (c, a) { if (c.check.enable && c.check.chkStyle === j.checkbox.STYLE) for (var b = c.data.key.checked, d = c.data.key.children, f = g.getRoot(c), k = 0, l = f[d].length; k < l; k++) { var i = f[d][k]; i.nocheck !== !0 && (i[b] = a); e.setSonNodeCheckBox(c, i, a) } }, repairChkClass: function (c, a) { if (a) { g.makeChkFlag(c, a); var b = h("#" + a.tId + j.id.CHECK); e.setChkClass(c, b, a) } }, repairParentChkClass: function (c, a) {
        if (a && a.parentTId) {
            var b = a.getParentNode();
            e.repairChkClass(c, b); e.repairParentChkClass(c, b)
        } 
    }, repairParentChkClassWithSelf: function (c, a) { if (a) { var b = c.data.key.children; a[b] && a[b].length > 0 ? e.repairParentChkClass(c, a[b][0]) : e.repairParentChkClass(c, a) } }, repairSonChkDisabled: function (c, a, b) { if (a) { var d = c.data.key.children; if (a.chkDisabled != b) a.chkDisabled = b, a.nocheck !== !0 && e.repairChkClass(c, a); if (a[d]) for (var f = 0, k = a[d].length; f < k; f++) e.repairSonChkDisabled(c, a[d][f], b) } }, repairParentChkDisabled: function (c, a, b) {
        if (a) {
            if (a.chkDisabled != b) a.chkDisabled =
b, a.nocheck !== !0 && e.repairChkClass(c, a); e.repairParentChkDisabled(c, a.getParentNode(), b)
        } 
    }, setChkClass: function (c, a, b) { a && (b.nocheck === !0 ? a.hide() : a.show(), a.removeClass(), a.addClass(e.makeChkClass(c, b))) }, setParentNodeCheckBox: function (c, a, b, d) {
        var f = c.data.key.children, k = c.data.key.checked, l = h("#" + a.tId + j.id.CHECK); d || (d = a); g.makeChkFlag(c, a); a.nocheck !== !0 && a.chkDisabled !== !0 && (a[k] = b, e.setChkClass(c, l, a), c.check.autoCheckTrigger && a != d && a.nocheck !== !0 && c.treeObj.trigger(j.event.CHECK, [c.treeId,
a])); if (a.parentTId) { l = !0; if (!b) for (var f = a.getParentNode()[f], i = 0, m = f.length; i < m; i++) if (f[i].nocheck !== !0 && f[i][k] || f[i].nocheck === !0 && f[i].check_Child_State > 0) { l = !1; break } l && e.setParentNodeCheckBox(c, a.getParentNode(), b, d) } 
    }, setSonNodeCheckBox: function (c, a, b, d) {
        if (a) {
            var f = c.data.key.children, k = c.data.key.checked, l = h("#" + a.tId + j.id.CHECK); d || (d = a); var i = !1; if (a[f]) for (var m = 0, n = a[f].length; m < n && a.chkDisabled !== !0; m++) { var o = a[f][m]; e.setSonNodeCheckBox(c, o, b, d); o.chkDisabled === !0 && (i = !0) } if (a !=
g.getRoot(c) && a.chkDisabled !== !0) { i && a.nocheck !== !0 && g.makeChkFlag(c, a); if (a.nocheck !== !0) { if (a[k] = b, !i) a.check_Child_State = a[f] && a[f].length > 0 ? b ? 2 : 0 : -1 } else a.check_Child_State = -1; e.setChkClass(c, l, a); c.check.autoCheckTrigger && a != d && a.nocheck !== !0 && c.treeObj.trigger(j.event.CHECK, [c.treeId, a]) } 
        } 
    } 
    }, event: {}, data: { getRadioCheckedList: function (c) { for (var a = g.getRoot(c).radioCheckedList, b = 0, d = a.length; b < d; b++) g.getNodeCache(c, a[b].tId) || (a.splice(b, 1), b--, d--); return a }, getCheckStatus: function (c, a) {
        if (!c.check.enable ||
a.nocheck) return null; var b = c.data.key.checked; return { checked: a[b], half: a.halfCheck ? a.halfCheck : c.check.chkStyle == j.radio.STYLE ? a.check_Child_State === 2 : a[b] ? a.check_Child_State > -1 && a.check_Child_State < 2 : a.check_Child_State > 0}
    }, getTreeCheckedNodes: function (c, a, b, d) { if (!a) return []; for (var f = c.data.key.children, k = c.data.key.checked, d = !d ? [] : d, e = 0, i = a.length; e < i; e++) a[e].nocheck !== !0 && a[e][k] == b && d.push(a[e]), g.getTreeCheckedNodes(c, a[e][f], b, d); return d }, getTreeChangeCheckedNodes: function (c, a, b) {
        if (!a) return [];
        for (var d = c.data.key.children, f = c.data.key.checked, b = !b ? [] : b, k = 0, e = a.length; k < e; k++) a[k].nocheck !== !0 && a[k][f] != a[k].checkedOld && b.push(a[k]), g.getTreeChangeCheckedNodes(c, a[k][d], b); return b
    }, makeChkFlag: function (c, a) {
        if (a) {
            var b = c.data.key.children, d = c.data.key.checked, f = -1; if (a[b]) for (var e = !1, g = 0, i = a[b].length; g < i; g++) {
                var m = a[b][g], h = -1; if (c.check.chkStyle == j.radio.STYLE) if (h = m.nocheck === !0 ? m.check_Child_State : m.halfCheck === !0 ? 2 : m.nocheck !== !0 && m[d] ? 2 : m.check_Child_State > 0 ? 2 : 0, h == 2) { f = 2; break } else h ==
0 && (f = 0); else if (c.check.chkStyle == j.checkbox.STYLE) { h = m.nocheck === !0 ? m.check_Child_State : m.halfCheck === !0 ? 1 : m.nocheck !== !0 && m[d] ? m.check_Child_State === -1 || m.check_Child_State === 2 ? 2 : 1 : m.check_Child_State > 0 ? 1 : 0; if (h === 1) { f = 1; break } else if (h === 2 && e && h !== f) { f = 1; break } else if (f === 2 && h > -1 && h < 2) { f = 1; break } else h > -1 && (f = h); e || (e = m.nocheck !== !0) } 
            } a.check_Child_State = f
        } 
    } 
    }
    }); var o = h.fn.zTree, n = o._z.tools, j = o.consts, e = o._z.view, g = o._z.data; g.exSetting(t); g.addInitBind(function (c) {
        var a = c.treeObj, b = j.event; a.unbind(b.CHECK);
        a.bind(b.CHECK, function (a, b, e) { n.apply(c.callback.onCheck, [a, b, e]) })
    }); g.addInitCache(function () { }); g.addInitNode(function (c, a, b, d, f, e) {
        if (b) {
            a = c.data.key.checked; typeof b[a] == "string" && (b[a] = n.eqs(b[a], "true")); b[a] = !!b[a]; b.checkedOld = b[a]; b.nocheck = !!b.nocheck || c.check.nocheckInherit && d && !!d.nocheck; b.chkDisabled = !!b.chkDisabled || d && !!d.chkDisabled; if (typeof b.halfCheck == "string") b.halfCheck = n.eqs(b.halfCheck, "true"); b.halfCheck = !!b.halfCheck; b.check_Child_State = -1; b.check_Focus = !1; b.getCheckStatus =
function () { return g.getCheckStatus(c, b) }; e && g.makeChkFlag(c, d)
        } 
    }); g.addInitProxy(function (c) {
        var a = c.target, b = g.getSetting(c.data.treeId), d = "", f = null, e = "", h = null; if (n.eqs(c.type, "mouseover")) { if (b.check.enable && n.eqs(a.tagName, "button") && a.getAttribute("treeNode" + j.id.CHECK) !== null) d = a.parentNode.id, e = "mouseoverCheck" } else if (n.eqs(c.type, "mouseout")) { if (b.check.enable && n.eqs(a.tagName, "button") && a.getAttribute("treeNode" + j.id.CHECK) !== null) d = a.parentNode.id, e = "mouseoutCheck" } else if (n.eqs(c.type,
"click") && b.check.enable && n.eqs(a.tagName, "button") && a.getAttribute("treeNode" + j.id.CHECK) !== null) d = a.parentNode.id, e = "checkNode"; if (d.length > 0) switch (f = g.getNodeCache(b, d), e) { case "checkNode": h = p; break; case "mouseoverCheck": h = q; break; case "mouseoutCheck": h = r } return { stop: !1, node: f, nodeEventType: e, nodeEventCallback: h, treeEventType: "", treeEventCallback: null}
    }); g.addInitRoot(function (c) { g.getRoot(c).radioCheckedList = [] }); g.addBeforeA(function (c, a, b) {
        var d = c.data.key.checked; c.check.enable && (g.makeChkFlag(c,
a), c.check.chkStyle == j.radio.STYLE && c.check.radioType == j.radio.TYPE_ALL && a[d] && g.getRoot(c).radioCheckedList.push(a), b.push("<button type='button' ID='", a.tId, j.id.CHECK, "' class='", e.makeChkClass(c, a), "' treeNode", j.id.CHECK, " onfocus='this.blur();' ", a.nocheck === !0 ? "style='display:none;'" : "", "></button>"))
    }); g.addZTreeTools(function (c, a) {
        a.checkNode = function (a, b, g, l) {
            var i = this.setting.data.key.checked; if (a.chkDisabled !== !0 && (b !== !0 && b !== !1 && (b = !a[i]), l = !!l, (a[i] !== b || g) && !(l && n.apply(this.setting.callback.beforeCheck,
[this.setting.treeId, a], !0) == !1) && n.uCanDo(this.setting) && this.setting.check.enable && a.nocheck !== !0)) a[i] = b, b = h("#" + a.tId + j.id.CHECK), (g || this.setting.check.chkStyle === j.radio.STYLE) && e.checkNodeRelation(this.setting, a), e.setChkClass(this.setting, b, a), e.repairParentChkClassWithSelf(this.setting, a), l && c.treeObj.trigger(j.event.CHECK, [c.treeId, a])
        }; a.checkAllNodes = function (a) { e.repairAllChk(this.setting, !!a) }; a.getCheckedNodes = function (a) {
            var b = this.setting.data.key.children; return g.getTreeCheckedNodes(this.setting,
g.getRoot(c)[b], a !== !1)
        }; a.getChangeCheckedNodes = function () { var a = this.setting.data.key.children; return g.getTreeChangeCheckedNodes(this.setting, g.getRoot(c)[a]) }; a.setChkDisabled = function (a, b) { b = !!b; e.repairSonChkDisabled(this.setting, a, b); b || e.repairParentChkDisabled(this.setting, a, b) }; var b = a.updateNode; a.updateNode = function (c, f) {
            b && b.apply(a, arguments); if (c && this.setting.check.enable && h("#" + c.tId).get(0) && n.uCanDo(this.setting)) {
                var g = h("#" + c.tId + j.id.CHECK); (f == !0 || this.setting.check.chkStyle ===
j.radio.STYLE) && e.checkNodeRelation(this.setting, c); e.setChkClass(this.setting, g, c); e.repairParentChkClassWithSelf(this.setting, c)
            } 
        } 
    }); var s = e.createNodes; e.createNodes = function (c, a, b, d) { s && s.apply(e, arguments); b && e.repairParentChkClassWithSelf(c, d) } 
})(jQuery);

/*
* JQuery zTree exedit 3.0
* http://code.google.com/p/jquerytree/
*
* Copyright (c) 2010 Hunter.z (baby666.cn)
*
* Licensed same as jquery - MIT License
* http://www.opensource.org/licenses/mit-license.php
*
* email: hunter.z@263.net
* Date: 2012-01-10
*/
(function (m) {
    var C = { onHoverOverNode: function (b, a) { var c = o.getSetting(b.data.treeId), i = o.getRoot(c); if (i.curHoverNode != a) C.onHoverOutNode(b); i.curHoverNode = a; g.addHoverDom(c, a) }, onHoverOutNode: function (b) { var b = o.getSetting(b.data.treeId), a = o.getRoot(b); if (a.curHoverNode && !o.isSelectedNode(b, a.curHoverNode)) g.removeTreeDom(b, a.curHoverNode), a.curHoverNode = null }, onMousedownNode: function (b, a) {
        function c(b) {
            if (A.dragFlag == 0 && Math.abs(H - b.clientX) < f.edit.drag.minMoveSize && Math.abs(I - b.clientY) < f.edit.drag.minMoveSize) return !0;
            var a, c, e, l, j; j = f.data.key.children; h.noSel(f); m("body").css("cursor", "pointer"); if (A.dragFlag == 0) {
                if (h.apply(f.callback.beforeDrag, [f.treeId, n], !0) == !1) return i(b), !0; for (a = 0, c = n.length; a < c; a++) { if (a == 0) A.dragNodeShowBefore = []; e = n[a]; e.isParent && e.open ? (g.expandCollapseNode(f, e, !e.open), A.dragNodeShowBefore[e.tId] = !0) : A.dragNodeShowBefore[e.tId] = !1 } A.dragFlag = 1; A.showHoverDom = !1; h.showIfameMask(f, !0); e = !0; l = -1; if (n.length > 1) {
                    var t = n[0].parentTId ? n[0].getParentNode()[j] : o.getNodes(f); j = []; for (a = 0,
c = t.length; a < c; a++) if (A.dragNodeShowBefore[t[a].tId] !== void 0 && (e && l > -1 && l + 1 !== a && (e = !1), j.push(t[a]), l = a), n.length === j.length) { n = j; break } 
                } e && (C = n[0].getPreNode(), K = n[n.length - 1].getNextNode()); x = m("<ul class='zTreeDragUL'></ul>"); for (a = 0, c = n.length; a < c; a++) if (e = n[a], e.editNameFlag = !1, g.selectNode(f, e, a > 0), g.removeTreeDom(f, e), l = m("<li id='" + e.tId + "_tmp'></li>"), l.append(m("#" + e.tId + d.id.A).clone()), l.css("padding", "0"), l.children("#" + e.tId + d.id.A).removeClass(d.node.CURSELECTED), x.append(l), a == f.edit.drag.maxShowNodeNum -
1) { l = m("<li id='" + e.tId + "_moretmp'><a>  ...  </a></li>"); x.append(l); break } x.attr("id", n[0].tId + d.id.UL + "_tmp"); x.addClass(f.treeObj.attr("class")); x.appendTo("body"); v = m("<button class='tmpzTreeMove_arrow'></button>"); v.attr("id", "zTreeMove_arrow_tmp"); v.appendTo("body"); f.treeObj.trigger(d.event.DRAG, [f.treeId, n])
            } if (A.dragFlag == 1 && v.attr("id") != b.target.id) {
                p && (p.removeClass(d.node.TMPTARGET_TREE), w && m("#" + w + d.id.A, p).removeClass(d.node.TMPTARGET_NODE)); w = p = null; D = !1; k = f; e = o.getSettings(); for (var r in e) if (e[r].treeId &&
e[r].edit.enable && e[r].treeId != f.treeId && (b.target.id == e[r].treeId || m(b.target).parents("#" + e[r].treeId).length > 0)) D = !0, k = e[r]; r = y.scrollTop(); l = y.scrollLeft(); j = k.treeObj.offset(); a = k.treeObj.get(0).scrollHeight; e = k.treeObj.get(0).scrollWidth; c = b.clientY + r - j.top; var E = k.treeObj.height() + j.top - b.clientY - r, F = b.clientX + l - j.left, q = k.treeObj.width() + j.left - b.clientX - l; j = c < f.edit.drag.borderMax && c > f.edit.drag.borderMin; var t = E < f.edit.drag.borderMax && E > f.edit.drag.borderMin, s = F < f.edit.drag.borderMax && F >
f.edit.drag.borderMin, O = q < f.edit.drag.borderMax && q > f.edit.drag.borderMin, E = c > f.edit.drag.borderMin && E > f.edit.drag.borderMin && F > f.edit.drag.borderMin && q > f.edit.drag.borderMin, F = j && k.treeObj.scrollTop() <= 0, q = t && k.treeObj.scrollTop() + k.treeObj.height() + 10 >= a, J = s && k.treeObj.scrollLeft() <= 0, P = O && k.treeObj.scrollLeft() + k.treeObj.width() + 10 >= e; if (b.target.id && k.treeObj.find("#" + b.target.id).length > 0) {
                    for (var B = b.target; B && B.tagName && !h.eqs(B.tagName, "li") && B.id != k.treeId; ) B = B.parentNode; var L = !0; for (a = 0,
c = n.length; a < c; a++) if (e = n[a], B.id === e.tId) { L = !1; break } else if (m("#" + e.tId).find("#" + B.id).length > 0) { L = !1; break } if (L && b.target.id && (b.target.id == B.id + d.id.A || m(b.target).parents("#" + B.id + d.id.A).length > 0)) p = m(B), w = B.id
                } e = n[0]; if (E && (b.target.id == k.treeId || m(b.target).parents("#" + k.treeId).length > 0)) {
                    if (!p && (b.target.id == k.treeId || F || q || J || P) && (D || !D && e.parentTId)) p = k.treeObj; j ? k.treeObj.scrollTop(k.treeObj.scrollTop() - 10) : t && k.treeObj.scrollTop(k.treeObj.scrollTop() + 10); s ? k.treeObj.scrollLeft(k.treeObj.scrollLeft() -
10) : O && k.treeObj.scrollLeft(k.treeObj.scrollLeft() + 10); p && p != k.treeObj && p.offset().left < k.treeObj.offset().left && k.treeObj.scrollLeft(k.treeObj.scrollLeft() + p.offset().left - k.treeObj.offset().left)
                } x.css({ top: b.clientY + r + 3 + "px", left: b.clientX + l + 3 + "px" }); l = a = 0; if (p && p.attr("id") != k.treeId) {
                    var z = w == null ? null : o.getNodeCache(k, w); c = b.ctrlKey && f.edit.drag.isMove && f.edit.drag.isCopy || !f.edit.drag.isMove && f.edit.drag.isCopy; a = !!(C && w === C.tId); j = !!(K && w === K.tId); l = e.parentTId && e.parentTId == w; e = (c || !j) &&
h.apply(k.edit.drag.prev, [k.treeId, n, z], !!k.edit.drag.prev); a = (c || !a) && h.apply(k.edit.drag.next, [k.treeId, n, z], !!k.edit.drag.next); j = (c || !l) && !(k.data.keep.leaf && !z.isParent) && h.apply(k.edit.drag.inner, [k.treeId, n, z], !!k.edit.drag.inner); if (!e && !a && !j) { if (p = null, w = "", u = d.move.TYPE_INNER, v.css({ display: "none" }), window.zTreeMoveTimer) clearTimeout(window.zTreeMoveTimer), window.zTreeMoveTargetNodeTId = null } else {
                        c = m("#" + w + d.id.A, p); c.addClass(d.node.TMPTARGET_NODE); l = e ? j ? 0.25 : a ? 0.5 : 1 : -1; j = a ? j ? 0.75 : e ? 0.5 :
0 : -1; b = (b.clientY + r - c.offset().top) / c.height(); (l == 1 || b <= l && b >= -0.2) && e ? (a = 1 - v.width(), l = 0 - v.height() / 2, u = d.move.TYPE_PREV) : (j == 0 || b >= j && b <= 1.2) && a ? (a = 1 - v.width(), l = c.height() - v.height() / 2, u = d.move.TYPE_NEXT) : (a = 5 - v.width(), l = 0, u = d.move.TYPE_INNER); v.css({ display: "block", top: c.offset().top + l + "px", left: c.offset().left + a + "px" }); if (M != w || N != u) G = (new Date).getTime(); if (z && z.isParent && u == d.move.TYPE_INNER && (b = !0, window.zTreeMoveTimer && window.zTreeMoveTargetNodeTId !== z.tId ? (clearTimeout(window.zTreeMoveTimer),
window.zTreeMoveTargetNodeTId = null) : window.zTreeMoveTimer && window.zTreeMoveTargetNodeTId === z.tId && (b = !1), b)) window.zTreeMoveTimer = setTimeout(function () { u == d.move.TYPE_INNER && z && z.isParent && !z.open && (new Date).getTime() - G > k.edit.drag.autoOpenTime && h.apply(k.callback.beforeDragOpen, [k.treeId, z], !0) && (g.switchNode(k, z), k.edit.drag.autoExpandTrigger && k.treeObj.trigger(d.event.EXPAND, [k.treeId, z])) }, k.edit.drag.autoOpenTime + 50), window.zTreeMoveTargetNodeTId = z.tId
                    } 
                } else if (u = d.move.TYPE_INNER, p && h.apply(k.edit.drag.inner,
[k.treeId, n, null], !!k.edit.drag.inner) ? p.addClass(d.node.TMPTARGET_TREE) : p = null, v.css({ display: "none" }), window.zTreeMoveTimer) clearTimeout(window.zTreeMoveTimer), window.zTreeMoveTargetNodeTId = null; M = w; N = u
            } return !1
        } function i(b) {
            if (window.zTreeMoveTimer) clearTimeout(window.zTreeMoveTimer), window.zTreeMoveTargetNodeTId = null; N = M = null; y.unbind("mousemove", c); y.unbind("mouseup", i); y.unbind("selectstart", e); m("body").css("cursor", "auto"); p && (p.removeClass(d.node.TMPTARGET_TREE), w && m("#" + w + d.id.A, p).removeClass(d.node.TMPTARGET_NODE));
            h.showIfameMask(f, !1); A.showHoverDom = !0; if (A.dragFlag != 0) {
                A.dragFlag = 0; var a, l, j, q = f.data.key.children; for (a = 0, l = n.length; a < l; a++) j = n[a], j.isParent && A.dragNodeShowBefore[j.tId] && !j.open && (g.expandCollapseNode(f, j, !j.open), delete A.dragNodeShowBefore[j.tId]); x && x.remove(); v && v.remove(); var s = b.ctrlKey && f.edit.drag.isMove && f.edit.drag.isCopy || !f.edit.drag.isMove && f.edit.drag.isCopy; !s && p && w && n[0].parentTId && w == n[0].parentTId && u == d.move.TYPE_INNER && (p = null); if (p) {
                    var t = w == null ? null : o.getNodeCache(k,
w); if (h.apply(f.callback.beforeDrop, [k.treeId, n, t, u], !0) != !1) {
                        var r = s ? h.clone(n) : n, b = function () {
                            if (D) { if (!s) for (var b = 0, a = n.length; b < a; b++) g.removeNode(f, n[b]); if (u == d.move.TYPE_INNER) g.addNodes(k, t, r); else if (g.addNodes(k, t.getParentNode(), r), u == d.move.TYPE_PREV) for (b = 0, a = r.length; b < a; b++) g.moveNode(k, t, r[b], u, !1); else for (b = -1, a = r.length - 1; b < a; a--) g.moveNode(k, t, r[a], u, !1) } else if (s && u == d.move.TYPE_INNER) g.addNodes(k, t, r); else if (s && g.addNodes(k, t.getParentNode(), r), u == d.move.TYPE_PREV) for (b = 0,
a = r.length; b < a; b++) g.moveNode(k, t, r[b], u, !1); else for (b = -1, a = r.length - 1; b < a; a--) g.moveNode(k, t, r[a], u, !1); for (b = 0, a = r.length; b < a; b++) g.selectNode(k, r[b], b > 0); m("#" + r[0].tId + d.id.ICON).focus().blur()
                        }; u == d.move.TYPE_INNER && k.async.enable && t && t.isParent && (!t[q] || t[q].length === 0) ? g.asyncNode(k, t, !1, b) : b(); f.treeObj.trigger(d.event.DROP, [k.treeId, r, t, u])
                    } 
                } else { for (a = 0, l = n.length; a < l; a++) g.selectNode(k, n[a], a > 0); f.treeObj.trigger(d.event.DROP, [f.treeId, null, null, null]) } 
            } 
        } function e() { return !1 } var l, j,
f = o.getSetting(b.data.treeId), A = o.getRoot(f); if (b.button == 2 || !f.edit.enable || !f.edit.drag.isCopy && !f.edit.drag.isMove) return !0; var s = b.target, q = o.getRoot(f).curSelectedList, n = []; if (o.isSelectedNode(f, a)) for (l = 0, j = q.length; l < j; l++) { if (q[l].editNameFlag && h.eqs(s.tagName, "input") && s.getAttribute("treeNode" + d.id.INPUT) !== null) return !0; n.push(q[l]); if (n[0].parentTId !== q[l].parentTId) { n = [a]; break } } else n = [a]; g.editNodeBlur = !0; g.cancelCurEditNode(f, null, !0); var y = m(document), x, v, p, D = !1, k = f, C, K, M = null, N =
null, w = null, u = d.move.TYPE_INNER, H = b.clientX, I = b.clientY, G = (new Date).getTime(); h.uCanDo(f) && y.bind("mousemove", c); y.bind("mouseup", i); y.bind("selectstart", e); b.preventDefault && b.preventDefault(); return !0
    } 
    }, s = { tools: { getAbs: function (b) { b = b.getBoundingClientRect(); return [b.left, b.top] }, inputFocus: function (b) { b.get(0) && (b.focus(), h.setCursorPosition(b.get(0), b.val().length)) }, setCursorPosition: function (b, a) {
        if (b.setSelectionRange) b.focus(), b.setSelectionRange(a, a); else if (b.createTextRange) {
            var c = b.createTextRange();
            c.collapse(!0); c.moveEnd("character", a); c.moveStart("character", a); c.select()
        } 
    }, showIfameMask: function (b, a) {
        for (var c = o.getRoot(b); c.dragMaskList.length > 0; ) c.dragMaskList[0].remove(), c.dragMaskList.shift(); if (a) for (var i = m("iframe"), e = 0, d = i.length; e < d; e++) {
            var g = i.get(e), f = h.getAbs(g), g = m("<div id='zTreeMask_" + e + "' class='zTreeMask' style='background-color:yellow;opacity: 0.3;filter: alpha(opacity=30);    top:" + f[1] + "px; left:" + f[0] + "px; width:" + g.offsetWidth + "px; height:" + g.offsetHeight + "px;'></div>");
            g.appendTo("body"); c.dragMaskList.push(g)
        } 
    } 
    }, view: { addEditBtn: function (b, a) {
        if (!(a.editNameFlag || m("#" + a.tId + d.id.EDIT).length > 0) && h.apply(b.edit.showRenameBtn, [b.treeId, a], b.edit.showRenameBtn)) {
            var c = m("#" + a.tId + d.id.A), i = "<button type='button' class='edit' id='" + a.tId + d.id.EDIT + "' title='" + h.apply(b.edit.renameTitle, [b.treeId, a], b.edit.renameTitle) + "' treeNode" + d.id.EDIT + " onfocus='this.blur();' style='display:none;'></button>"; c.append(i); m("#" + a.tId + d.id.EDIT).bind("click", function () {
                if (!h.uCanDo(b) ||
h.apply(b.callback.beforeEditName, [b.treeId, a], !0) == !1) return !0; g.editNode(b, a); return !1
            }).show()
        } 
    }, addRemoveBtn: function (b, a) {
        if (!(a.editNameFlag || m("#" + a.tId + d.id.REMOVE).length > 0) && h.apply(b.edit.showRemoveBtn, [b.treeId, a], b.edit.showRemoveBtn)) {
            var c = m("#" + a.tId + d.id.A), i = "<button type='button' class='remove' id='" + a.tId + d.id.REMOVE + "' title='" + h.apply(b.edit.removeTitle, [b.treeId, a], b.edit.removeTitle) + "' treeNode" + d.id.REMOVE + " onfocus='this.blur();' style='display:none;'></button>"; c.append(i);
            m("#" + a.tId + d.id.REMOVE).bind("click", function () { if (!h.uCanDo(b) || h.apply(b.callback.beforeRemove, [b.treeId, a], !0) == !1) return !0; g.removeNode(b, a); b.treeObj.trigger(d.event.REMOVE, [b.treeId, a]); return !1 }).bind("mousedown", function () { return !0 }).show()
        } 
    }, addHoverDom: function (b, a) { if (o.getRoot(b).showHoverDom) a.isHover = !0, b.edit.enable && (g.addEditBtn(b, a), g.addRemoveBtn(b, a)), h.apply(b.view.addHoverDom, [b.treeId, a]) }, cancelCurEditNode: function (b, a) {
        var c = o.getRoot(b), i = b.data.key.name, e = c.curEditNode;
        if (e) { var l = c.curEditInput, j = a ? a : l.val(); if (!a && h.apply(b.callback.beforeRename, [b.treeId, e, j], !0) === !1) return e.editNameFlag = !0, !1; else e[i] = j ? j : l.val(), a || b.treeObj.trigger(d.event.RENAME, [b.treeId, e]); m("#" + e.tId + d.id.A).removeClass(d.node.CURSELECTED_EDIT); l.unbind(); g.setNodeName(b, e); e.editNameFlag = !1; c.curEditNode = null; c.curEditInput = null; g.selectNode(b, e, !1) } return c.noSelection = !0
    }, editNode: function (b, a) {
        var c = o.getRoot(b); g.editNodeBlur = !1; if (o.isSelectedNode(b, a) && c.curEditNode == a && a.editNameFlag) setTimeout(function () { h.inputFocus(c.curEditInput) },
0); else {
            var i = b.data.key.name; a.editNameFlag = !0; g.removeTreeDom(b, a); g.cancelCurEditNode(b); g.selectNode(b, a, !1); m("#" + a.tId + d.id.SPAN).html("<input type=text class='rename' id='" + a.tId + d.id.INPUT + "' treeNode" + d.id.INPUT + " >"); var e = m("#" + a.tId + d.id.INPUT); e.attr("value", a[i]); h.inputFocus(e); e.bind("blur", function () { g.editNodeBlur || g.cancelCurEditNode(b) }).bind("keydown", function (c) { c.keyCode == "13" ? (g.editNodeBlur = !0, g.cancelCurEditNode(b, null, !0)) : c.keyCode == "27" && g.cancelCurEditNode(b, a[i]) }).bind("click",
function () { return !1 }).bind("dblclick", function () { return !1 }); m("#" + a.tId + d.id.A).addClass(d.node.CURSELECTED_EDIT); c.curEditInput = e; c.noSelection = !1; c.curEditNode = a
        } 
    }, moveNode: function (b, a, c, i, e, l) {
        var j = o.getRoot(b), f = b.data.key.children; if (a != c && (!b.data.keep.leaf || !a || a.isParent || i != d.move.TYPE_INNER)) {
            var h = c.parentTId ? c.getParentNode() : j, s = a === null || a == j; s && a === null && (a = j); if (s) i = d.move.TYPE_INNER; j = a.parentTId ? a.getParentNode() : j; if (i != d.move.TYPE_PREV && i != d.move.TYPE_NEXT) i = d.move.TYPE_INNER;
            var q, n; s ? n = q = b.treeObj : l || (i == d.move.TYPE_INNER ? g.expandCollapseNode(b, a, !0, !1) : g.expandCollapseNode(b, a.getParentNode(), !0, !1), q = m("#" + a.tId), n = m("#" + a.tId + d.id.UL)); var y = m("#" + c.tId).remove(); n && i == d.move.TYPE_INNER ? n.append(y) : q && i == d.move.TYPE_PREV ? q.before(y) : q && i == d.move.TYPE_NEXT && q.after(y); var x = -1, v = 0, p = null; q = null; var C = c.level; if (c.isFirstNode) { if (x = 0, h[f].length > 1) p = h[f][1], p.isFirstNode = !0 } else if (c.isLastNode) x = h[f].length - 1, p = h[f][x - 1], p.isLastNode = !0; else for (n = 0, y = h[f].length; n <
y; n++) if (h[f][n].tId == c.tId) { x = n; break } x >= 0 && h[f].splice(x, 1); if (i != d.move.TYPE_INNER) for (n = 0, y = j[f].length; n < y; n++) j[f][n].tId == a.tId && (v = n); if (i == d.move.TYPE_INNER) { s ? c.parentTId = null : (a.isParent = !0, a.open = !1, c.parentTId = a.tId); a[f] || (a[f] = []); if (a[f].length > 0) q = a[f][a[f].length - 1], q.isLastNode = !1; a[f].splice(a[f].length, 0, c); c.isLastNode = !0; c.isFirstNode = a[f].length == 1 } else a.isFirstNode && i == d.move.TYPE_PREV ? (j[f].splice(v, 0, c), q = a, q.isFirstNode = !1, c.parentTId = a.parentTId, c.isFirstNode = !0, c.isLastNode =
!1) : a.isLastNode && i == d.move.TYPE_NEXT ? (j[f].splice(v + 1, 0, c), q = a, q.isLastNode = !1, c.parentTId = a.parentTId, c.isFirstNode = !1, c.isLastNode = !0) : (i == d.move.TYPE_PREV ? j[f].splice(v, 0, c) : j[f].splice(v + 1, 0, c), c.parentTId = a.parentTId, c.isFirstNode = !1, c.isLastNode = !1); o.fixPIdKeyValue(b, c); o.setSonNodeLevel(b, c.getParentNode(), c); g.setNodeLineIcos(b, c); g.repairNodeLevelClass(b, c, C); !b.data.keep.parent && h[f].length < 1 ? (h.isParent = !1, h.open = !1, a = m("#" + h.tId + d.id.UL), i = m("#" + h.tId + d.id.SWITCH), f = m("#" + h.tId + d.id.ICON),
g.replaceSwitchClass(h, i, d.folder.DOCU), g.replaceIcoClass(h, f, d.folder.DOCU), a.css("display", "none")) : p && g.setNodeLineIcos(b, p); q && g.setNodeLineIcos(b, q); b.check.enable && g.repairChkClass && (g.repairChkClass(b, h), g.repairParentChkClassWithSelf(b, h), h != c.parent && g.repairParentChkClassWithSelf(b, c)); l || g.expandCollapseParentNode(b, c.getParentNode(), !0, e)
        } 
    }, removeChildNodes: function (b, a) {
        if (a) {
            var c = b.data.key.children, i = a[c]; if (i) {
                m("#" + a.tId + d.id.UL).remove(); for (var e = 0, l = i.length; e < l; e++) o.removeNodeCache(b,
i[e]); o.removeSelectedNode(b); delete a[c]; if (!b.data.keep.parent) a.isParent = !1, a.open = !1, c = m("#" + a.tId + d.id.SWITCH), i = m("#" + a.tId + d.id.ICON), g.replaceSwitchClass(a, c, d.folder.DOCU), g.replaceIcoClass(a, i, d.folder.DOCU)
            } 
        } 
    }, removeEditBtn: function (b) { m("#" + b.tId + d.id.EDIT).unbind().remove() }, removeNode: function (b, a) {
        var c = o.getRoot(b), i = b.data.key.children, e = a.parentTId ? a.getParentNode() : c; if (c.curEditNode === a) c.curEditNode = null; a.isFirstNode = !1; a.isLastNode = !1; a.getPreNode = function () { return null }; a.getNextNode =
function () { return null }; m("#" + a.tId).remove(); o.removeNodeCache(b, a); o.removeSelectedNode(b, a); for (var l = 0, j = e[i].length; l < j; l++) if (e[i][l].tId == a.tId) { e[i].splice(l, 1); break } var f; if (!b.data.keep.parent && e[i].length < 1) e.isParent = !1, e.open = !1, l = m("#" + e.tId + d.id.UL), j = m("#" + e.tId + d.id.SWITCH), f = m("#" + e.tId + d.id.ICON), g.replaceSwitchClass(e, j, d.folder.DOCU), g.replaceIcoClass(e, f, d.folder.DOCU), l.css("display", "none"); else if (b.view.showLine && e[i].length > 0) {
            var h = e[i][e[i].length - 1]; h.isLastNode = !0;
            h.isFirstNode = e[i].length == 1; l = m("#" + h.tId + d.id.UL); j = m("#" + h.tId + d.id.SWITCH); f = m("#" + h.tId + d.id.ICON); e == c ? e[i].length == 1 ? g.replaceSwitchClass(h, j, d.line.ROOT) : (c = m("#" + e[i][0].tId + d.id.SWITCH), g.replaceSwitchClass(e[i][0], c, d.line.ROOTS), g.replaceSwitchClass(h, j, d.line.BOTTOM)) : g.replaceSwitchClass(h, j, d.line.BOTTOM); l.removeClass(d.line.LINE)
        } 
    }, removeRemoveBtn: function (b) { m("#" + b.tId + d.id.REMOVE).unbind().remove() }, removeTreeDom: function (b, a) {
        a.isHover = !1; g.removeEditBtn(a); g.removeRemoveBtn(a);
        h.apply(b.view.removeHoverDom, [b.treeId, a])
    }, repairNodeLevelClass: function (b, a, c) { if (c !== a.level) { var b = m("#" + a.tId), i = m("#" + a.tId + d.id.A), e = m("#" + a.tId + d.id.UL), c = "level" + c, a = "level" + a.level; b.removeClass(c); b.addClass(a); i.removeClass(c); i.addClass(a); e.removeClass(c); e.addClass(a) } } 
    }, event: s, data: { setSonNodeLevel: function (b, a, c) { if (c) { var d = b.data.key.children; c.level = a ? a.level + 1 : 0; if (c[d]) for (var a = 0, e = c[d].length; a < e; a++) c[d][a] && o.setSonNodeLevel(b, c, c[d][a]) } } }
    }; m.extend(!0, m.fn.zTree.consts,
{ event: { DRAG: "ztree_drag", DROP: "ztree_drop", REMOVE: "ztree_remove", RENAME: "ztree_rename" }, id: { EDIT: "_edit", INPUT: "_input", REMOVE: "_remove" }, move: { TYPE_INNER: "inner", TYPE_PREV: "prev", TYPE_NEXT: "next" }, node: { CURSELECTED_EDIT: "curSelectedNode_Edit", TMPTARGET_TREE: "tmpTargetzTree", TMPTARGET_NODE: "tmpTargetNode"} }); m.extend(!0, m.fn.zTree._z, s); var s = m.fn.zTree, h = s._z.tools, d = s.consts, g = s._z.view, o = s._z.data, s = s._z.event; o.exSetting({ edit: { enable: !1, showRemoveBtn: !0, showRenameBtn: !0, removeTitle: "remove",
    renameTitle: "rename", drag: { autoExpandTrigger: !1, isCopy: !0, isMove: !0, prev: !0, next: !0, inner: !0, minMoveSize: 5, borderMax: 10, borderMin: -5, maxShowNodeNum: 5, autoOpenTime: 500}
}, view: { addHoverDom: null, removeHoverDom: null }, callback: { beforeDrag: null, beforeDragOpen: null, beforeDrop: null, beforeEditName: null, beforeRemove: null, beforeRename: null, onDrag: null, onDrop: null, onRemove: null, onRename: null}
}); o.addInitBind(function (b) {
    var a = b.treeObj, c = d.event; a.unbind(c.RENAME); a.bind(c.RENAME, function (a, c, d) {
        h.apply(b.callback.onRename,
[a, c, d])
    }); a.unbind(c.REMOVE); a.bind(c.REMOVE, function (a, c, d) { h.apply(b.callback.onRemove, [a, c, d]) }); a.unbind(c.DRAG); a.bind(c.DRAG, function (a, c, d) { h.apply(b.callback.onDrag, [a, c, d]) }); a.unbind(c.DROP); a.bind(c.DROP, function (a, c, d, g, f) { h.apply(b.callback.onDrop, [a, c, d, g, f]) })
}); o.addInitCache(function () { }); o.addInitNode(function (b, a, c) { if (c) c.isHover = !1, c.editNameFlag = !1 }); o.addInitProxy(function (b) {
    var a = b.target, c = o.getSetting(b.data.treeId), g = b.relatedTarget, e = "", l = null, j = "", f = null, m = null; if (h.eqs(b.type,
"mouseover")) { if (m = h.getMDom(c, a, [{ tagName: "a", attrName: "treeNode" + d.id.A}])) e = m.parentNode.id, j = "hoverOverNode" } else if (h.eqs(b.type, "mouseout")) m = h.getMDom(c, g, [{ tagName: "a", attrName: "treeNode" + d.id.A}]), m || (e = "remove", j = "hoverOutNode"); else if (h.eqs(b.type, "mousedown") && (m = h.getMDom(c, a, [{ tagName: "a", attrName: "treeNode" + d.id.A}]))) e = m.parentNode.id, j = "mousedownNode"; if (e.length > 0) switch (l = o.getNodeCache(c, e), j) {
        case "mousedownNode": f = C.onMousedownNode; break; case "hoverOverNode": f = C.onHoverOverNode;
            break; case "hoverOutNode": f = C.onHoverOutNode
    } return { stop: !1, node: l, nodeEventType: j, nodeEventCallback: f, treeEventType: "", treeEventCallback: null}
}); o.addInitRoot(function (b) { b = o.getRoot(b); b.curEditNode = null; b.curEditInput = null; b.curHoverNode = null; b.dragFlag = 0; b.dragNodeShowBefore = []; b.dragMaskList = []; b.showHoverDom = !0 }); o.addZTreeTools(function (b, a) {
    a.addNodes = function (a, d, e) {
        function l() { g.addNodes(b, a, f, e == !0) } if (!d) return null; a || (a = null); if (a && !a.isParent && b.data.keep.leaf) return null; var j = b.data.key.children,
f = h.clone(h.isArray(d) ? d : [d]); this.setting.async.enable && a && a.isParent && (!a[j] || a[j].length === 0) ? g.asyncNode(b, a, e, l) : l(); return f
    }; a.cancelEditName = function (a) { var d = o.getRoot(b), e = b.data.key.name, h = d.curEditNode; d.curEditNode && g.cancelCurEditNode(b, a ? a : h[e]) }; a.copyNode = function (a, i, e, l) {
        if (!i) return null; if (a && !a.isParent && b.data.keep.leaf && e === d.move.TYPE_INNER) return null; var j = b.data.key.children, f = h.clone(i); if (!a) a = null, e = d.move.TYPE_INNER; e == d.move.TYPE_INNER ? (i = function () {
            g.addNodes(b, a,
[f], l)
        }, b.async.enable && a && a.isParent && (!a[j] || a[j].length === 0) ? g.asyncNode(b, a, l, i) : i()) : (g.addNodes(b, a.parentNode, [f], l), g.moveNode(b, a, f, e, !1, l)); return f
    }; a.editName = function (a) { a && a.tId && a === o.getNodeCache(b, a.tId) && (g.expandCollapseParentNode(b, a, !0), g.editNode(b, a)) }; a.moveNode = function (a, i, e, h) {
        function j() { g.moveNode(b, a, i, e, !1, h) } if (!i) return i; if (a && !a.isParent && b.data.keep.leaf && e === d.move.TYPE_INNER) return null; else if (a && (i.parentTId == a.tId && e == d.move.TYPE_INNER || m("#" + i.tId).find("#" +
a.tId).length > 0)) return null; else a || (a = null); var f = b.data.key.children; b.async.enable && a && a.isParent && (!a[f] || a[f].length === 0) ? g.asyncNode(b, a, h, j) : j(); return i
    }; a.removeNode = function (a, i) { a && (i = !!i, i && h.apply(b.callback.beforeRemove, [b.treeId, a], !0) == !1 || (g.removeNode(b, a), i && this.setting.treeObj.trigger(d.event.REMOVE, [b.treeId, a]))) }; a.removeChildNodes = function (a) { if (!a) return null; var d = a[b.data.key.children]; g.removeChildNodes(b, a); return d ? d : null }; a.setEditable = function (a) {
        b.edit.enable = a;
        return this.refresh()
    } 
}); var H = g.cancelPreSelectedNode; g.cancelPreSelectedNode = function (b, a) { for (var c = o.getRoot(b).curSelectedList, d = 0, e = c.length; d < e; d++) if (!a || a === c[d]) if (g.removeTreeDom(b, c[d]), a) break; H && H.apply(g, arguments) }; var I = g.createNodes; g.createNodes = function (b, a, c, d) { I && I.apply(g, arguments); c && g.repairParentChkClassWithSelf && g.repairParentChkClassWithSelf(b, d) }; g.makeNodeUrl = function (b, a) { return a.url && !b.edit.enable ? a.url : null }; var G = g.selectNode; g.selectNode = function (b, a, c) {
    var d =
o.getRoot(b); if (o.isSelectedNode(b, a) && d.curEditNode == a && a.editNameFlag) return !1; G && G.apply(g, arguments); g.addHoverDom(b, a); return !0
}; var J = h.uCanDo; h.uCanDo = function (b, a) { var c = o.getRoot(b); return a && (h.eqs(a.type, "mouseover") || h.eqs(a.type, "mouseout") || h.eqs(a.type, "mousedown") || h.eqs(a.type, "mouseup")) ? !0 : !c.curEditNode && (J ? J.apply(g, arguments) : !0) } 
})(jQuery);
