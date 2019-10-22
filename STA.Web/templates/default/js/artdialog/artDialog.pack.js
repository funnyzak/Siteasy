artDialog.alert = function (msg, second, icon) {
    var dialog = art.dialog({
        id: "alert",
        title: webconfig.webname + '提示',
        content: msg,
        opacity: 0.2,
        icon: icon || "face-smile",
        padding: '25px 15px',
        time: second || 1,
        drag: true,
        width: 330,
        lock: true
    });

};

artDialog.confirm = function (msg, yes, no) {
    art.dialog({
        id: "confirm",
        content: msg || "确定执行此操作吗？",
        ok: yes,
        cancel: no || true,
        opacity: 0.2,
        icon: "question",
        cancelVal: '取消',
        title: '确认操作',
        width: 330,
        lock: true
    });
};
artDialog.tips = function (content, time, icon, close) {
    art.dialog({
        id: "tips",
        title: false,
        cancel: false,
        icon: icon || "",
        fixed: true,
        opacity: 0.3,
        lock: true,
        close: close || null,
        content: '<div style="padding: 0 1em;">' + content + '</div>',
        time: time || 1
    });
};

artDialog.prompt = function (content, yes, value) {
    value = value || '';
    var input;

    return artDialog({
        id: 'Prompt',
        icon: 'question',
        fixed: true,
        lock: true,
        opacity: .1,
        content: [
            '<div style="margin-bottom:5px;font-size:12px">',
                content,
            '</div>',
            '<div>',
                '<input value="',
                    value,
                '" style="width:18em;padding:6px 4px" />',
            '</div>'
            ].join(''),
        init: function () {
            input = this.DOM.content.find('input')[0];
            input.select();
            input.focus();
        },
        ok: function (here) {
            return yes && yes.call(this, input.value, here);
        },
        cancel: true
    });
};