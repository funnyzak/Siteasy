/**
 * @license Copyright (c) 2003-2013, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.html or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    //config.uiColor = '#AADC6E';
    config.toolbar = 'I';
    config.toolbar_I =
	[
        ["Source", 'Bold', 'Italic', '-', 'NumberedList', 'BulletedList', '-', 'Link', 'Unlink', '-'],
        '/',
        ['Styles', 'Format', 'Font', 'FontSize'],
        ['TextColor', 'BGColor'],
        ['Maximize', 'ShowBlocks', '-', 'NextPage', 'TypeSetting']
    ];

    //图片预览文字
    config.image_previewText = "  ";
    //_BR、_P 换行默认标签
    config.autoParagraph = false;
    config.enterMode = CKEDITOR.ENTER_BR;
    config.shiftEnterMode = CKEDITOR.ENTER_P;
    config.toolbar = 'Full';
    //编辑器高区
    config.height = 240;

    //config.skin = 'moonocolor';

    config.filebrowserWindowWidth = 650;
    config.filebrowserWindowHeight = 270;
    //config.filebrowserWindowFeatures = 'location=no,menubar=no,toolbar=no,dependent=yes,minimizable=no,modal=yes,alwaysRaised=yes,resizable=no,scrollbars=no';

    config.allowedContent = true;
    config.language = "zh-cn";
    config.resize_dir = 'vertical';
    config.toolbarCanCollapse = true;
    //设置从Word粘贴的内容 plugins/pastefromword/plugin.js    
    config.pasteFromWordPromptCleanup = false;
    config.pasteFromWordCleanupFile = false;
    config.pasteFromWordRemoveFontStyles = false;
    config.pasteFromWordNumberedHeadingToList = false;
    config.pasteFromWordRemoveStyles = false;
    config.extraPlugins = "addon,stapage";
    config.toolbar_Full =
	[
		["Source", "-", "NewPage", "Preview", "-", "Templates"],
        ["Cut", "Copy", "Paste", "PasteText", "PasteFromWord", "-", "Print", "SpellChecker", "Scayt"],
        ['Undo', 'Redo', '-', 'Find', 'Replace', '-', 'SelectAll', 'RemoveFormat'],
        ['Addon', 'Link', 'Unlink', 'Anchor'],
        '/',
        ['Image', 'Flash', 'Table', 'HorizontalRule', 'SpecialChar'],
        ['Bold', 'Italic', 'Underline', 'Strike', '-', 'Subscript', 'Superscript'],
        ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', 'Blockquote', 'CreateDiv'],
        ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
        '/',
        ['Styles', 'Format', 'Font', 'FontSize'],
        ['TextColor', 'BGColor'],
        ['Maximize', 'ShowBlocks', '-', 'MyPage']
	];
    config.font_names = '宋体/宋体;黑体/黑体;仿宋/仿宋_GB2312;楷体/楷体_GB2312;隶书/隶书;幼圆/幼圆;微软雅黑/微软雅黑;' + config.font_names;
};
