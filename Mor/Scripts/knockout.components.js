/// <reference path="../Content/summernote/summernote.min.js" />
/// <reference path="../Content/js/bootstrap.datepicker.js" />
/// <reference path="jquery-1.10.2.js" />
/// <reference path="knockout-3.2.0.js" /> 

// ----------------------------------------------------------------------
// Title    : KO组件库
// Version  : 1.0 
// Author   : jackie
// Remark   :  
// Created  : 2014-12-03
// ----------------------------------------------------------------------

ko.components.register('uploader', {

    viewModel: function (params) {

        var me = this;

        me.value = typeof params.url === 'function' ? params.url
            : ko.observable(params && params.url || '');

        me.onUpload = function () {
            $.dialog.uploadImage(function (res) {
                me.value(res.filename);
            });
        };
    },
    template: '<div class="input-group">\
            <input type="text" class="form-control" data-bind="value:value, enable: false" />\
            <span class="input-group-btn">\
            <button class="btn btn-info" data-bind="click: onUpload" type="button">\
            <i class="fa fa-cloud-upload"></i> 上传 </button>\
            </span>\
        </div>'
});

// 日期选择
ko.components.register('datepicker', {

    viewModel: function (params) {

        var me = this;

        me.id = 'dp_' + Math.random().toString().substring(2);   // 随机id
        me.placeholder = params.placeholder || '请选择日期';
        me.value = params.value;

        // 初始化
        require(['/content/js/bootstrap.datepicker.js'], function () {
            $('#' + me.id).addClass('datepicker').on('show', function (e) {

                // 隐藏其它项 
                $('.datepicker').not(e.target).each(function () {
                    $(this).data('datepicker') && $(this).data('datepicker').hide();
                });

                // 处理z-index;
                $(e.target).parents().each(function () {
                    var index = $(this).css('z-index');
                    if (!(index > 0)) return true;
                    $(e.target).data('datepicker').picker.css('z-index', ++index);
                    return false;
                });

            }).on('changeDate', function (e) {
                me.value($(e.target).val());
                $(this).data('datepicker').hide();
            }).datepicker({
                weekStart: 1,   //
                format: 'yyyy-mm-dd'
            });
        });

    },
    template: '<input class="form-control input-sm " \
        type="text" data-bind="attr:{ id:id, placeholder:placeholder}, value:value" />'
});

// 输入框
ko.components.register('inputcontrol', {

    viewModel: function (params) {

        var me = this;
        me.id = 'input_' + new Date().valueOf();
        me.value = params.value;    // bind value;
        me.onKeydown = function (d, e) {

            //    console.log(e.keyCode);
            //    // 忽略辅助键
            //    if ($.inArray(e.keyCode, [8, 9, 46]) > -1) {
            //        return true;
            //    }

            //    // on enter;
            //    if (e.keyCode === 13) {
            //        params.onEnter && params.onEnter.apply(me, [e]);
            //        return false;
            //    }

            //    if (params.type === 'numeric') {
            //        return (e.keyCode > 47 && e.keyCode < 58)
            //            || (e.keyCode > 95 && e.keyCode < 106) ;
            //    } else if (params.type === 'integer') {
            //        return (e.keyCode > 47 && e.keyCode < 58)
            //            || (e.keyCode > 95 && e.keyCode < 106);
            //    }

            //    //var dataType = params.type || 'text';

            //    //if (!dataType || dataType === "text") return true;  // default is text;
            //    //if (dataType === 'numeric' && e.keyCode === 110) return true;

            //    return false;
        }

        // 初始化
        require(['/content/js/jquery.numeric.min.js'], function () {
            $('#' + me.id).numeric();
        });

    },
    template: '<input class="form-control input-sm" \
        data-bind="id:id, value:value"  />'
});


// 编辑器
ko.components.register('htmleditor', {

    viewModel: function (params) {

        var me = this, _st = null, _editor;
        me.id = 'summernote_' + new Date().valueOf();
        me.value = params.value;    // bind value; 

        // 监听value值
        var subscription = me.value.subscribe(function (content) {
            if (!_editor) return;
            window.clearInterval(_st);
            _st = window.setInterval(function () {
                subscription.dispose();     // 销毁订阅方法 
                _editor.code(content);
                window.clearInterval(_st);
            }, 300);
        });


        define.amd = false;

        require([
            '/content/summernote/summernote.js',
            //'/content/summernote/summernote-zh-CN.js'
        ], function () {


            // ### INIT
            window.setTimeout(function () {

                _editor = $('#' + me.id).summernote({
                    height: params.height || 200,
                    //lang: 'zh-CN',
                    onChange: function (html, $editable) {
                        me.value(html);
                    },
                    onImageUpload: function (files, editor, $editable) {

                        $.upload('image', files[0], function (res) {

                            // 插入图片
                            editor.insertImage($editable, res.filename);

                            // 更新当前值（延时处理）
                            window.setTimeout(function () { me.value($editable.code()); }, 0);
                        });
                    }
                });

            }, 0);



        });
    },

    template: '<link href="/content/summernote/summernote.css" rel="stylesheet" />\
        <textarea class="summernote"  data-bind="value:value,attr:{id:id}"></textarea>'

});

// 图表
ko.components.register('chart', new function () {

    // 
    require.config({
        paths: {
            'echarts': '/Scripts/echarts'
        }
    });

    this.viewModel = function (params) {

        // 
        var me = this
            , chartType = params.type || 'bar'
            , ele = null
            , myChart = null

        // ## properties
        me._id = params.id || 'chart-' + new Date().valueOf();
        me._height = params.height || '300px';

        me.status = ko.observable(0);   // 0=未加载, 1=加载中, 2=已完成, -1=无数据

        // ## methods  
        me.reload = function () {
            /// <summary>刷新图表数据</summary>

            if (!params.url) {
                console.log('[ERROR] url is not define!');
                return;
            }

            me.status(1);

            var data = typeof params.args === 'function' ? params.args() : params.args;
            $.post(params.url, data, $.proxy(me, "build"));
        }

        me.build = function (data) {
            /// <summary>构建图形</summary>

            if (!data || data.length == 0) {
                me.status(-1);
                myChart.clear();// 图表清空-------------------
                return;
            }

            var option = null, series = [];

            if (chartType === 'bar') {

                series = $.map(data, function (o) {
                    return {
                        name: o.name,
                        type: 'bar',
                        data: $.map(o.data, function (c) { return c.value; })
                    }
                });

                option = {
                    tooltip: { trigger: 'axis' },
                    legend: { data: $.map(data, function (o) { return o.name; }) },
                    calculable: true,
                    xAxis: [{ type: 'category', data: $.map(data[0]['data'], function (o) { return o.name; }) }],
                    yAxis: [{ type: 'value' }],
                    series: series
                };

            } else if (chartType === 'pie') {

                series = [{
                    name: data[0].name,
                    type: 'pie',
                    radius: '55%',
                    center: ['50%', '60%'],
                    data: $.map(data[0].data, function (o) { return { value: o.value, name: o.name }; })
                }];

                option = {
                    tooltip: { trigger: 'item', formatter: "{a} <br/>{b} : {c} ({d}%)" },
                    legend: { orient: 'vertical', x: 'left', data: $.map(data[0].data, function (o) { return o.name; }) },
                    calculable: true,
                    series: series
                };

            } else if (chartType === 'line') {
                option = {
                    tooltip: { trigger: 'axis' },
                    legend: { data: $.map(data[0].data, function (o) { return o.type; }) },
                    calculable: true,
                    xAxis: [
                        {
                            type: 'category',
                            boundaryGap: false,
                            data: $.map(data[0].data[0].obj, function (o) { return o.name; })
                        }
                    ],
                    yAxis: [{ type: 'value' }],
                    series: $.map(data[0].data, function (o) {
                        return {
                            name: o.type,
                            type: 'line',
                            data: $.map(o.obj, function (c) { return c.value; })
                        }
                    })
                }

                //option = {
                //    tooltip: { trigger: 'axis' },
                //    legend: { data: $.map(data, function (o) { return o.name; }) },
                //    calculable: true,
                //    xAxis: [
                //        {
                //            type: 'category',
                //            boundaryGap: false,
                //            data: $.map(data[0].data, function (o) { console.log(o); return o.name; })
                //        }
                //    ],
                //    yAxis: [{ type: 'value' }],
                //    series: $.map(data, function (o) {
                //        console.log(o);
                //        return {
                //            name: o.name,
                //            type: 'line',
                //            data: $.map(o.data, function (c) { return c.value; })
                //        }
                //    })

                // }

            }

            // set options
            myChart.setOption(option);

            // update status
            me.status(2);
        }

        // ############# init ############## 

        // get modules
        var modules = ['echarts', 'echarts/theme/infographic'];

        if (chartType === 'bar') {
            modules.push('echarts/chart/bar');
        } else if (chartType === 'pie') {
            modules.push('echarts/chart/pie');
        } else if (chartType === 'line') {
            modules.push('echarts/chart/line');
        }

        // load modules
        require(modules, function (ec, theme) {

            // ## target element;
            ele = document.getElementById(me._id);
            $(ele).data('chart', me);  // bind data

            myChart = ec.init(ele, theme);
            params.autoLoad !== false && me.reload();
        });
    };

    this.template = '<div>\
        <div data-bind="visible:status()!==2" style="position:absolute;">\
            <i class="fa fa-spin fa-spinner fa-3x" data-bind="visible:status()===1"></i>\
            <h4 data-bind="visible:status()===-1">NO DATA!</h4>\
        </div>\
        <div data-bind="attr:{id:_id}, style:{height:_height}"></div>\
    </div>'
});

