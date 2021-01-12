(function( $ ){
  $.alert = function(message) {
    var dfd = $.Deferred();
    // �_�C�A���O���쐬
    var dlg = $( "<div></div>" ).dialog({
        modal: true,
        buttons: {
            "OK": function() {
                $( this ).dialog( "close" );
          dfd.resolve();
            }
        }
    });
    dlg.html(message);
    return dfd.promise();
  };

  $.confirm = function(message) {
    var dfd = $.Deferred();
    // �_�C�A���O���쐬
    var dlg = $( "<div></div>" ).dialog({
        modal: true,
        buttons: {
            "OK": function() {
                $( this ).dialog( "close" );
          dfd.resolve("yes");
            },
            "�L�����Z��": function(event, ui) {
                $( this ).dialog( "close" );
          dfd.resolve("cancel");
            }
        }
    });
    dlg.html(message);
    return dfd.promise();
  };
})( jQuery );
