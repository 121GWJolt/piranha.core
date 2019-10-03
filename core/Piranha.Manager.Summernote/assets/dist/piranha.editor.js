/*global
    piranha, summernote
*/

var ImageButton = function (context) {
    var ui = $.summernote.ui;

    // create button
    var button = ui.button({
        contents: '<i class="fa fa-image"/>',
        tooltip: 'Image',
        click: function () {
            context.invoke('saveRange');
            piranha.mediapicker.open(function (img) {
                    context.invoke('restoreRange');
                    context.invoke('insertImage', img.publicUrl, img.filename);
                },
                'image');
        }
    });

    return button.render();
};

//
// Create a new inline editor
//
piranha.editor.addInline = function (id, toolbarId, blurcallback) {
    var contentContainer = $("#" + id);
    contentContainer.summernote({
        toolbar: [
            ['style', ['bold', 'italic', 'underline', 'clear']],
            ['para', ['ul', 'ol', 'paragraph']],
            ['media', ['piranhaimage']],
            ['view', ['fullscreen', 'codeview']],
        ],
        buttons: {
            piranhaimage: ImageButton
        },
        popover: {
            image: [],
            link: [],
            air: []
        },
        codemirror: {
            lineNumbers: true,
            mode: "text/html",
            lineWrapping: true,
            extraKeys: { "Ctrl-Space": "autocomplete" }
        },
        callbacks: {
            onBlur: function () {
                blurcallback(contentContainer.summernote("code"));
            }
        }
    });

    return {
        getCode: function (e) {
            return contentContainer.summernote("code");
        }
    };
};

//
// Remove the editor instance with the given id.
//
piranha.editor.remove = function (id) {
    $("#" + id).summernote("destroy");
};