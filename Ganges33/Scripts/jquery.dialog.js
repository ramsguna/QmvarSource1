(function( $ ){
  $.alert = function(message) {
    var dfd = $.Deferred();
    // ダイアログを作成
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
    // ダイアログを作成
    var dlg = $( "<div></div>" ).dialog({
        modal: true,
        buttons: {
            "OK": function() {
                $( this ).dialog( "close" );
          dfd.resolve("yes");
            },
            "キャンセル": function(event, ui) {
                $( this ).dialog( "close" );
          dfd.resolve("cancel");
            }
        }
    });
    dlg.html(message);
    return dfd.promise();
  };
})( jQuery );
