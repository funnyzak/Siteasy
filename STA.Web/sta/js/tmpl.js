/**
* ΢��ģ������ tmpl 0.2
* 0.2 ����:
* 1. �޸�ת���ַ���id�жϵ�BUG
* 2. ������Ч�� with ���Ӷ��������3.5����ִ��Ч��
* 3. ʹ������ڲ�������ֹ��ģ�����������ͻ
* @example
* ��ʽһ����ҳ��Ƕ��ģ��
* <script type="text/tmpl" id="tmpl-demo">
* <ol title="<%=name%>">
*  < % for (var i = 0, l = list.length; i < l; i ++) { %>
*      <li>< %=list[i]%></li>
*  < % } %>
* </ol>
* </script>
* tmpl('tmpl-demo', {name: 'demo data', list: [202, 96, 133, 134]})
*
* ��ʽ����ֱ�Ӵ���ģ�壺
* var demoTmpl =
* '<ol title="<%=name%>">'
* + '< % for (var i = 0, l = list.length; i < l; i ++) { %>'
* +    '<li>< %=list[i]%></li>'
* + '< % } %>'
* +'</ol>';
* var render = tmpl(demoTmpl);
* render({name: 'demo data', list: [202, 96, 133, 134]});
*
* �����ַ�ʽ�������ڵ�һ�����Զ��������õ�ģ�壬
* ���ڶ��ֻ��潻���ⲿ������ƣ��������е� render ������
* @blog http://www.planeart.cn/?p=1594
* @see		http://ejohn.org/blog/javascript-micro-templating/
* @param	{String}	ģ�����ݻ���װ��ģ�����ݵ�Ԫ��ID
* @param	{Object}	���ӵ�����
* @return	{String}	�����õ�ģ��
*/
var tmpl = (function (cache, $) {
    return function (str, data) {
        var fn = !/\s/.test(str)
	? cache[str] = cache[str]
		|| tmpl(document.getElementById(str).innerHTML)

	: function (data) {
	    var i, variable = [$], value = [[]];
	    for (i in data) {
	        variable.push(i);
	        value.push(data[i]);
	    };
	    return (new Function(variable, fn.$))
		.apply(data, value).join("");
	};

        fn.$ = fn.$ || $ + ".push('"
	+ str.replace(/\\/g, "\\\\")
		 .replace(/[\r\t\n]/g, " ")
		 .split("<%").join("\t")
		 .replace(/((^|%>)[^\t]*)'/g, "$1\r")
		 .replace(/\t=(.*?)%>/g, "',$1,'")
		 .split("\t").join("');")
		 .split("%>").join($ + ".push('")
		 .split("\r").join("\\'")
	+ "');return " + $;

        return data ? fn(data) : fn;
    }
})({}, '$' + (+new Date));
