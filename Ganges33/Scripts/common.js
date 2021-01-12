// history.back対応
history.forward();

jQuery(document).ready(function () {
    // F5,Esc制御 テスト中は許可
    var keydown = function (e) {
        if (e.keyCode == 116 || e.keyCode == 27) {
            e.keyCode = null;
            if (!!event) event.keyCode = null;
            e.preventDefault();
            return false;
        }
        // Enterキーの不要なSubmit抑止
        if (e.keyCode == 13) {
            if (window.event.srcElement.type != 'submit' && window.event.srcElement.type != 'textarea') {
                if (window.event.srcElement.type != 'image') {
                    return false;
                }
            }
        }
    };
    $(document).keydown(keydown);

    // 入力チェック（validation engine）
    $("#form1").validationEngine();
    $(".back, .logout, .id, .pass").click(function () {
        $("#form1").validationEngine('hideAll');
        $("#form1").validationEngine('detach');
        return true;
    });

    // Submit時
    $("#form1").submit(function () {
        // 2度押し防止
        //$(this).find(':submit').attr('visibility', 'hidden');
        //全trを探索
        $("tr").each(function () {
            var self = $(this);
            if (self.is(':hidden')) {
                //非表示状態の値は送出しない
                self.find("input:text").val('');
                self.find("input:checkbox").attr("checked", false);
                self.find("select").val('');
                self.find("textarea").val('');
            }
        });
    });

    //* 【 オンマウス処理 】-----------------------------------*
    $("body *").filter(function () {
        return this.title && this.title.length > 0;
    }).each(function () {
        var self = $(this), title = self.attr("title");

        // TITLE属性を持っている要素にhover()で
        self.hover(
            // mouseover
            function (e) {
                // TITLEがあるとブラウザのチップが出るので一時的に空にしておく
                self.attr("title", "");
                // // とりあえず表示するtip要素を生成しておく
                // $("body").append("<div id='title-tip'>" + title + "</div>");
                // $("#title-tip").css({
                // position: "absolute",
                // // e.pageX(Y)でカーソルが要素に乗った時点でのX(Y)座標を取得する
                // top: e.pageY + (-15), // カーソルと表示したtipが重なるとチラつくので少しずらす
                // left: e.pageX + 15
                //});
            },
            // mouseout
            function () {
                // mouseoverで空にしたTITLEを戻す
                self.attr("title", title);
                // 要素から離れた場合はtipを非表示にして削除しておく
                //$("#title-tip").hide().remove();
            }
        );

        // 要素上でカーソルが移動した場合は、逐一tipの位置を変える
        /*
        self.mousemove(function (e) {
            $("#title-tip").css({
                top: e.pageY + (-15),
                left: e.pageX + 15
            });
        });*/

        // 選択時に表示
        self.focus(
        // focus
			function (e) {
			    // TITLEがあるとブラウザのチップが出るので一時的に空にしておく
			    self.attr("title", "");
			    // とりあえず表示するtip要素を生成しておく
			    $("body").append("<div id='title-tip'>" + title + "</div>");
			    var offset = self.offset();
			    $("#title-tip").css({
			        position: "absolute",
			        top: offset.top + 0, // カーソルと表示したtipが重なるとチラつくので少しずらす
			        left: offset.left + 15
			    });
			}
		);

        // 選択が外れたら非表示
        self.blur(
        // blur
			function () {
			    // mouseoverで空にしたTITLEを戻す
			    self.attr("title", title);
			    // 要素から離れた場合はtipを非表示にして削除しておく
			    $("#title-tip").hide().remove();
			}
    	);
    });

});