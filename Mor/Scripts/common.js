

var dialog = function () {

    var __createMessage = function (type, msg, fn, timeout) {

        var icon = type === 'danger' ? 'fa-times' : type === 'success' ? 'fa-check' : 'fa-warning';

        // build modal
        var dialog = $('<div class="modal fade"/>')
            .html([
                '<div class="modal-dialog">',
                ' <div class="modal-content">',
                '  <div class="modal-body">',
                '   <div class="text-', type, ' col-md-offset-1">',
                '   <i class="fa fa-3x pull-left ', icon, '"></i>',
                '   <h4 style="line-height:200%">', msg, '</h4>',
                '   </div>',
                '  </div>',
                ' </div>',
                '</div>'
            ].join(''))
            .on('hidden.bs.modal', function () {
                if (typeof fn === 'function') {
                    fn();
                }
                dialog.remove();
            }).appendTo('body').modal('show');

        // set close timeout 
        if (timeout > 0) {
            window.setTimeout(function () { dialog.modal('hide'); }, timeout);
        }

        return dialog;
    }

    this.open = function (url, args, callback) {

        var name = 'fn' + new Date().valueOf();

        args = jQuery.extend({}, args, { 'callback': name });

        url += url.indexOf('?') == -1 ? '?' : '&';
        url += jQuery.param(args);

        var dialog = $('<div class="modal fade" />').appendTo('body')
            .on('hidden.bs.modal', function () {
                dialog.remove();
            }).modal({ remote: url });

        window[name] = function (result) {

            dialog.modal('hide');
            if (typeof callback === 'function')
                callback(result);
        };
    }

    this.success = function (msg, fn, t) {
        return __createMessage('success', msg, fn, t || 1500);
    };

    this.error = function (msg, fn, t) {
        return __createMessage('danger', msg, fn, t);
    }

    this.warning = function (msg, fn, t) {
        return __createMessage('warning', msg, fn, t || 1500);
    }

    //this.confirm = function (msg, fnYes, fnNo) {

    //    // build modal
    //    var dialog = $('<div class="modal"/>')
    //        .html([
    //            '<div class="modal-dialog" style="width:500px;">',
    //            ' <div class="modal-content">',
    //            '  <div class="modal-header">',
    //            '     <h5 class="modal-title">', '请确认', '</h5>',
    //            '  </div>',
    //            '  <div class="modal-body">',
    //            '   <div class="col-md-offset-1">',
    //            '   <i class="fa fa-2x pull-left fa-warning  text-warning"></i>',
    //            '   <h4 style="line-height:150%">', msg, '</h4>',
    //            '   </div>',
    //            '  </div>',
    //            '  <div class="modal-footer">',
    //            '     <button type="button" class="btn btn-sm btn-primary">确定</button>',
    //            '     <button type="button" class="btn btn-sm btn-default" data-dismiss="modal">取消</button>',
    //            '  </div>',
    //            ' </div>',
    //            '</div>'
    //        ].join(''))
    //        .on('hidden.bs.modal', function () {
    //            var fn = dialog.data('_accept') ? fnYes : fnNo;
    //            typeof fn === 'function' && fn();
    //            dialog.remove();
    //        }).appendTo('body').modal('show');

    //    dialog.find('button.btn-primary').click(function () {
    //        dialog.data('_accept', true);
    //        dialog.modal('hide');
    //    });

    //}


    this.upload = function (type, callback) {
        return this.open('/apps/dialog/upload', { type: type }, callback);
    }

    this.uploadImage = function (callback) {
        return this.upload('image', callback);
    }

    this.uploadFile = function (callback) {
        return this.upload('file', callback);
    }

}

/*
string.format, ref: http://stackoverflow.com/questions/610406/javascript-equivalent-to-printf-string-format/4673436#4673436
*/
if (!String.prototype.format) {
    String.prototype.format = function () {
        var args = arguments;
        return this.replace(/{(\d+)}/g, function (match, number) {
            return typeof args[number] != 'undefined'
              ? args[number]
              : match
            ;
        });
    };
}



// jQuery extends
$.extend($, {

    dialog: new dialog()

   , post: function (url, data, fn, settings) {
       // 重写post方法

       var defaultSettings = {
           url: url,
           type: 'post',
           dataType: 'json',
           data: data,
           error: function (e, txt) {
               $.dialog.error('request error!!!');
           },
           beforeSend: function () {

               if (!$('#loading_mask').size()) {
                   $('<div id="loading_mask" class="loading" style="display:none;"><i class="fa fa-4x fa-spinner fa-spin"></i></div>').prependTo('body');
               }

               window.clearTimeout(window.__st);
               window.__st = window.setTimeout(function () {
                   $('#loading_mask').css('top', window.scrollY + 300).show();
               }, 500);    // 请求超过0.5秒才显示
           },
           complete: function () {
               window.clearTimeout(window.__st);
               $('#loading_mask').hide();
           },
           success: function (res) {

               // has error;
               if (res && res.status === -1) {
                   $.dialog.error(res.msg);
                   return;
               }

               // success;
               if (typeof fn === 'function') {
                   fn.call(this, res);
               } else {
                   $.dialog.success(typeof fn === 'string' ? fn : '操作成功！');
               }

           }
       };

       settings = $.extend(defaultSettings, settings);
       return $.ajax(settings);

   }
    ,  getExpress: function (type, postid,ele) {

        // var url = 'http://' + 'm.kuaidi100.com/result.jsp?com=' + type + '&nu=' + postid;

        var url = [
            'http://', 'm.kuaidi100.com', '/index_all.html?'
            , 'type=', type
            , '&postid=', postid
            , '&callbackurl='
            , encodeURIComponent('http://baidu.com')
        ].join('');

        var iframe = null, isExists = false;
        if ($('#iframeModal').length > 0) {
            iframe = document.getElementById("iframeModal");
            isExists = true;
        } else {
            iframe = document.createElement("iframe");

            iframe.id = 'iframeModal';
            iframe.name = 'iframeModal';
            iframe.style.border = 'none';
            iframe.style.width = '100%';
            iframe.style.height = '500px';
        }

        iframe.src = url;
        console.info(url);
        if (!isExists) {
            $('#'+ele).append(iframe);
           // document.getElementById(ele).appendChild(iframe);
           // document.body.appendChild(iframe);
        }

    }
   , postHiddenForm: function (action, obj, target) {

       var f = $('#hidden_form').size() > 0 ? $('#hidden_form').empty() : $('<form id="hidden_form" method="POST"/>').hide().appendTo('body');
       for (var key in obj)
           $('<input type="hidden" />')
               .attr({ name: key, value: obj[key] }).appendTo(f);
       f.attr({ 'action': action, 'target': target }).submit();
   }

   , upload: function (type, file, callback) {

       var data = new FormData();
       data.append('file', file);

       $.ajax({
           url: '/Apps/Dialog/Upload?type=' + type,
           data: data,
           cache: false,
           contentType: false,
           processData: false,
           type: 'POST',
           success: function (res) {
               if (res.errmsg) {
                   alert(res.errmsg);
               } else if (typeof callback === 'function') {
                   callback(res);
               }
           }
       });

   }

   , preview: function (token, url) {
       var url = '/guest?token=' + token + '&url=' + window.encodeURIComponent(url);
       window.open(url, 'newwindow', 'height=600,width=400,top=0,left=0,toolbar=no,menubar=no,scrollbars=no, resizable=no,location=no, status=no')
   }
   , queryString: function (name) {

       var result = location.search.match(new RegExp("[\?\&]" + name + "=([^\&]+)", "i"));
       if (result == null || result.length < 1) {
           return "";
       }
       return result[1];
   }
});