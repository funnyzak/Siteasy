// Register a plugin named "stapage".
(function()
{
    CKEDITOR.plugins.add( 'stapage',
    {
        init : function( editor )
        {
            // Register the command.
            editor.addCommand( 'stapage',{
                exec : function( editor )
                {
                    // Create the element that represents a print break.
                    editor.insertHtml("[STA:PAGE=副标题]");
                }
            });
            // Register the toolbar button.
            editor.ui.addButton( 'MyPage',
            {
                label : '插入分页符',
                command : 'stapage',
                icon: this.path + 'stapage.gif'
            });
            // alert(editor.name);
        },
        requires : [ 'fakeobjects' ]
    });
})();
