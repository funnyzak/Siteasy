/**
 * @file 附件发布插件
 */

(function () {
    var addonDialog = function (editor, dialogType) {
        return {
            title: '附件添加',
            minWidth: 420,
            minHeight: 160,
            onOk: function () {
                var addonTitle = this.getValueOf('Link', 'txtTitle');
                var addonUrl = this.getValueOf('Link', 'txtUrl');
                addonUrl = addonUrl.indexOf("/attachment.aspx") >= 0 ? (addonUrl + "&attname=" + addonTitle) : addonUrl;
                var tempvar = '<table width="550" class="attachment">\r    <tbody>\r        <tr>\r            <td style="width:20px;height:30px;"><a target="_blank" href="' + addonUrl + '"><img border="0" align="middle" src="/sta/pics/attach2.gif" alt="' + addonTitle + '" /></a></td>\r            <td><a target="_blank" href="' + addonUrl + '" class="attach"><u>' + addonTitle + '</u></a></td>\r        </tr>\r    </tbody>\r</table>';
                editor.insertHtml(tempvar);

            },
            contents: [
				{
				    id: 'Link',
				    label: '附件',
				    padding: 0,
				    type: 'vbox',
				    elements:
					[
						{
						    type: 'vbox',
						    padding: 0,
						    children:
							[
								{
								    id: 'txtTitle',
								    type: 'text',
								    label: '附件标题',
								    style: 'width: 60%',
								    'default': ''
								},
								{
								    id: 'txtUrl',
								    type: 'text',
								    label: '选择附件',
								    style: 'width: 100%',
								    'default': ''
								},
								{
								    type: 'button',
								    id: 'browse',
								    filebrowser:
									{
									    action: 'Browse',
									    target: 'Link:txtUrl',
									    url: '../tools/selectfile.aspx?atturl=1&root=/files'
									},
								    style: 'float:right',
								    hidden: true,
								    label: editor.lang.common.browseServer
								}
							]
						}
					]
				}
			]
        };
    };

    CKEDITOR.dialog.add('addon', function (editor) {
        return addonDialog(editor, 'addon');
    });
})();
