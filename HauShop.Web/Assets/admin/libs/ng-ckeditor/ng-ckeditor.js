(function(angular, factory) {
    if (typeof define === 'function' && define.amd) {
        define(['angular', 'ckeditor'], function(angular) {
            return factory(angular);
        });
    } else {
        return factory(angular);
    }
}(angular || null, function(angular) {
var app = angular.module('ngCkeditor', []);
var $defer, loaded = false;

app.run(['$q', '$timeout', function($q, $timeout) {
    $defer = $q.defer();

    if (angular.isUndefined(CKEDITOR)) {
        throw new Error('CKEDITOR not found');
    }
    CKEDITOR.disableAutoInline = true;
    function checkLoaded() {
        if (CKEDITOR.status == 'loaded') {
            loaded = true;
            $defer.resolve();
        } else {
            checkLoaded();
        }
    }
    CKEDITOR.on('loaded', checkLoaded);
    $timeout(checkLoaded, 100);
}])

app.directive('ckeditor', ['$timeout', '$q', function ($timeout, $q) {
    'use strict';

    return {
        restrict: 'AC',
        require: ['ngModel', '^?form'],
        scope: false,
        link: function (scope, element, attrs, ctrls) {
            var ngModel = ctrls[0];
            var form    = ctrls[1] || null;
            var EMPTY_HTML = '<p></p>',
                isTextarea = element[0].tagName.toLowerCase() == 'textarea',
                data = [],
                isReady = false;

            if (!isTextarea) {
                element.attr('contenteditable', true);
            }

            var onLoad = function () {
                var options = {
                    toolbar: 'full',
                    //toolbar_full: [
                    //    { name: 'document', groups: ['mode', 'document', 'doctools'], items: ['FontAwesome', 'colordialog', 'colorbutton', 'Source', 'uicolor'] },
	                //    { name: 'clipboard', groups: ['clipboard', 'undo'], items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo'] },
	                //    { name: 'editing', groups: ['find', 'selection', 'spellchecker'], items: ['Scayt'] },
	                //    '/',
	                //    { name: 'basicstyles', groups: ['basicstyles', 'cleanup'], items: ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', '-', 'RemoveFormat'] },
	                //    { name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi'], items: ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote'] },
	                //    { name: 'links', items: ['Link', 'Unlink', 'Anchor'] },
	                //    { name: 'insert', items: ['Image', 'Table', 'HorizontalRule', 'SpecialChar'] },
	                //    '/',
	                //    { name: 'styles', items: ['Styles', 'Format'] },
	                //    { name: 'tools', items: ['Maximize', 'colorbutton', 'colordialog', 'uicolor'] },
	                //    { name: 'others', items: ['-'] },
	                //    //{ name: 'about', items: ['About'] }
		     
                    //],

                    //// Toolbar groups configuration.
                    //toolbarGroups : [
                    //    { name: 'document', groups: [ 'mode', 'document', 'doctools' ] },
                    //    { name: 'clipboard', groups: [ 'clipboard', 'undo' ] },
                    //    { name: 'editing', groups: [ 'find', 'selection', 'spellchecker' ] },
                    //    { name: 'forms' },
                    //    '/',
                    //    { name: 'basicstyles', groups: [ 'basicstyles', 'cleanup' ] },
                    //    { name: 'paragraph', groups: [ 'list', 'indent', 'blocks', 'align', 'bidi' ] },
                    //    { name: 'links' },
                    //    { name: 'insert' },
                    //    '/',
                    //    { name: 'styles' },
                    //    { name: 'colors' },
                    //    { name: 'tools' },
                    //    { name: 'others' },
                    //    { name: 'about' }
                    //],


                    disableNativeSpellChecker: true,
                    uiColor: '#FAFAFA',
                    height: '400px',
                    width: '100%',
                    toolbarCanCollapse: false,
                    removeButtons: 'About'
                };
                options = angular.extend(options, scope[attrs.ckeditor]);

                var instance = (isTextarea) ? CKEDITOR.replace(element[0], options) : CKEDITOR.inline(element[0], options),
                    configLoaderDef = $q.defer();

                element.bind('$destroy', function () {
                    instance.destroy(
                        false //If the instance is replacing a DOM element, this parameter indicates whether or not to update the element with the instance contents.
                    );
                });
                var setModelData = function(setPristine) {
                    var data = instance.getData();
                    if (data == '') {
                        data = null;
                    }
                    $timeout(function () { // for key up event
                        (setPristine !== true || data != ngModel.$viewValue) && ngModel.$setViewValue(data);
                        (setPristine === true && form) && form.$setPristine();
                    }, 0);
                }, onUpdateModelData = function(setPristine) {
                    if (!data.length) { return; }


                    var item = data.pop() || EMPTY_HTML;
                    isReady = false;
                    instance.setData(item, function () {
                        setModelData(setPristine);
                        isReady = true;
                    });
                }

                //instance.on('pasteState',   setModelData);
                instance.on('change',       setModelData);
                instance.on('blur',         setModelData);
                //instance.on('key',          setModelData); // for source view

                instance.on('instanceReady', function() {
                    scope.$broadcast("ckeditor.ready");
                    scope.$apply(function() {
                        onUpdateModelData(true);
                    });

                    instance.document.on("keyup", setModelData);
                });
                instance.on('customConfigLoaded', function() {
                    configLoaderDef.resolve();
                });

                ngModel.$render = function() {
                    data.push(ngModel.$viewValue);
                    if (isReady) {
                        onUpdateModelData();
                    }
                };
            };

            if (CKEDITOR.status == 'loaded') {
                loaded = true;
            }
            if (loaded) {
                onLoad();
            } else {
                $defer.promise.then(onLoad);
            }
        }
    };
}]);

    return app;
}));