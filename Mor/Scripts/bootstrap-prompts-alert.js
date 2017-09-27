
// alert
window._originalAlert = window.alert;
window.alert = function (text, fn) {
    var bootStrapAlert = function () {
        if (!$.fn.modal.Constructor)
            return false;
        if ($('#windowAlertModal').length == 1)
            return true;
        $('body').append(' \
    <div id="windowAlertModal" class="modal" tabindex="-1" role="dialog" aria-hidden="true"> \
        <div class="modal-dialog modal-sm"> \
            <div class="modal-content"> \
              <div class="modal-body"> \
              <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button> \
                <h4 class="text-muted"><i class="fa fa-info-circle"></i> 提示</h4> \
                <p style="margin:10px 10px 0 10px;font-size:120%;"> alert text </p> \
              </div> \
              <div class="modal-footer"> \
                <button class="btn btn-info" >确定</button> \
            </div> \
        </div> \
      </div> \
    </div> \
    ');

        $('#windowAlertModal button.btn-info').bind('click',function(){
            $('#windowAlertModal').modal('hide');   //data-dismiss="modal" aria-hidden="true"
            fn && fn();
        });

        return true;
    }
    if (bootStrapAlert()) {
        $('#windowAlertModal .modal-body > p').text(text);
        $('#windowAlertModal').on('shown.bs.modal', function () {
            $('button.btn-info', this).focus();
        }).modal();
    } else {
        console.log('bootstrap was not found');
        window._originalAlert(text);
    }
}

// confirm
window._originalConfirm = window.confirm;
window.confirm = function (text, cb) {

    if (typeof desc === 'function') {
        cb = arguments[1]; desc = null;
    }

    var initTemplate = function () {
        if ($('#windowConfirmModal').length == 1)
            return true;
        $('body').append(' \
        <div id="windowConfirmModal" class="modal" tabindex="-1" role="dialog" > \
          <div class="modal-dialog modal-sm"> \
            <div class="modal-content"> \
              <div class="modal-body"> \
              <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button> \
                <h4 class="text-muted"><i class="fa fa-question-circle"></i> 请确认</h4> \
                <p style="margin:10px 10px 0 10px;font-size:120%;"> alert text </p> \
              </div> \
              <div class="modal-footer"> \
                <button class="btn btn-warning" data-dismiss="modal" aria-hidden="true">确定</button> \
                <button class="btn btn-default" data-dismiss="modal" aria-hidden="true">取消</button> \
              </div> \
            </div> \
          </div> \
        </div> \
      ');
    }

    var bootStrapConfirm = function () {
        if (!$.fn.modal.Constructor)
            return false;

        $('body').off('click', '#windowConfirmModal .btn-warning');
        $('body').off('click', '#windowConfirmModal .btn-default');

        function confirm() { cb(true); }
        function deny() { cb(false); }

        $('body').on('click', '#windowConfirmModal .btn-warning', confirm);
        $('body').on('click', '#windowConfirmModal .btn-default', deny);

        return true;
    }

    initTemplate()

    if (bootStrapConfirm()) {

        $('#windowConfirmModal .modal-body p').text(text);
        $('#windowConfirmModal').on('shown.bs.modal', function () {
            $('button.btn-warning', this).focus();
        }).modal();


    } else {
        console.log('bootstrap was not found');
        cb(window._originalConfirm(text));
    }
}

// prompt
window._originalPrompt = window.prompt;
window.prompt = function (text, cb, defVal) {

    var initTemplate = function () {
        if ($('#windowPromptModal').length == 1)
            return true;
        $('body').append(' \
        <div id="windowPromptModal" class="modal" tabindex="-1" role="dialog" > \
          <div class="modal-dialog modal-sm"> \
            <div class="modal-content"> \
              <form >\
              <div class="modal-body"> \
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button> \
                <h4> alert text </h4> \
                <input type="text" class="form-control"/> \
              </div> \
              <div class="modal-footer"> \
                <button class="btn btn-primary" data-dismiss="modal" aria-hidden="true">确定</button> \
                <button class="btn btn-default" data-dismiss="modal" aria-hidden="true">取消</button> \
              </div> \
            </form >\
            </div> \
          </div> \
        </div> \
      ');

        $('#windowPromptModal form').submit(function () {
            $('button:first', this).trigger('click');
            return false;
        });
    }

    var bootStrapPrompt = function () {
        if (!$.fn.modal.Constructor)
            return false;

        $('body').off('click', '#windowPromptModal .btn-primary');
        $('body').off('click', '#windowPromptModal .btn-default');

        function ok() { cb && cb($('#windowPromptModal :text').val()); }
        function deny() { cb && cb(null); }

        $('body').on('click', '#windowPromptModal .btn-primary', ok);
        $('body').on('click', '#windowPromptModal .btn-default', deny);

        return true;
    }

    initTemplate()

    if (bootStrapPrompt()) {

        $('#windowPromptModal .modal-body h4').text(text);
        $('#windowPromptModal').on('shown.bs.modal', function () {
            $(':text:first', this).val(defVal || '').select().focus();
        }).modal();

    } else {
        console.log('bootstrap was not found');
        cb(window._originalPrompt(text));
    }
}