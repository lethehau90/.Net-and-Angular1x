/**
 * @license Copyright (c) 2003-2015, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here.
    // For complete reference see:
    // http://docs.ckeditor.com/#!/api/CKEDITOR.config

    //The toolbar groups arrangement, optimized for two toolbar rows.
    config.toolbarGroups = [
		{ name: 'document', groups: ['mode', 'document', 'doctools'] },
                        { name: 'clipboard', groups: ['clipboard', 'undo'] },
                        { name: 'editing', groups: ['find', 'selection', 'spellchecker'] },
                        { name: 'forms' },
                        '/',
                        { name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
                        { name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi'] },
                        { name: 'links' },
                        { name: 'insert' },
                        '/',
                        { name: 'styles' },
                        { name: 'colors' },
                        { name: 'tools' },
                        { name: 'others' },
                        { name: 'about' }
    ];

    config.toolbar = [
                        { name: 'document', groups: ['mode', 'document', 'doctools'], items: ['FontAwesome', 'colordialog', 'colorbutton', 'Source', 'uicolor'] },
	                    { name: 'clipboard', groups: ['clipboard', 'undo'], items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo'] },
	                    { name: 'editing', groups: ['find', 'selection', 'spellchecker'], items: ['Scayt'] },
	                    '/',
	                    { name: 'basicstyles', groups: ['basicstyles', 'cleanup'], items: ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', '-', 'RemoveFormat'] },
	                    { name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi'], items: ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote'] },
	                    { name: 'links', items: ['Link', 'Unlink', 'Anchor'] },
	                    { name: 'insert', items: ['Image', 'Table', 'HorizontalRule', 'SpecialChar'] },
	                    '/',
	                    { name: 'styles', items: ['Styles', 'Format'] },
	                    { name: 'tools', items: ['Maximize', 'colorbutton', 'colordialog', 'uicolor'] },
	                    { name: 'others', items: ['-'] },
	                    //{ name: 'about', items: ['About'] }
    ];

    config.uiColor = '#AADC6E';

    // Remove some buttons provided by the standard plugins, which are
    // not needed in the Standard(s) toolbar.
    //config.removeButtons = 'Underline,Subscript,Superscript';

    // Set the most common block elements.
    //config.format_tags = 'p;h1;h2;h3;h4;pre';

    // Simplify the dialog windows.
    //config.removeDialogTabs = 'image:advanced;link:advanced';

    //config.filebrowserBrowseUrl = '/Assets/admin/libs/ckfinder/ckfinder.html',
    //config.filebrowserUploadUrl = '/Assets/admin/libs/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files',
    //config.filebrowserWindowWidth = '1000',
    //config.filebrowserWindowHeight = '700'

    config.enterMode = CKEDITOR.ENTER_BR;
    config.toolbar = 'Full';
    config.filebrowserBrowseUrl = '/Assets/admin/libs/ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl = '/Assets/admin/libs/ckfinder/ckfinder.html?type=Images';
    config.filebrowserFlashBrowseUrl = '/Assets/admin/libs/ckfinder/ckfinder.html?type=Flash';
    config.filebrowserUploadUrl = '/Assets/admin/libs/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    config.filebrowserImageUploadUrl = '/Assets/admin/libs/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images';
    config.filebrowserFlashUploadUrl = '/Assets/admin/libs/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';
    config.filebrowserWindowWidth = '1000';
    config.filebrowserWindowHeight = '700';

    config.extraPlugins = 'widget,lineutils,fontawesome,colordialog,colorbutton,uicolor';
    config.contentsCss = '/Assets/admin/libs/ckeditor/plugins/fontawesome/font-awesome/css/font-awesome.min.css';
    config.allowedContent = true;
};
CKEDITOR.dtd.$removeEmpty['span'] = false;