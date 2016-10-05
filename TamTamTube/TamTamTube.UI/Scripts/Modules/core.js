/// <reference path="_references.js" />
(function (windowParam) {

    var _window = windowParam;
    var self = this;
    var $ = _window.$;

    var revapi;

    /// Core javascript type. 
    var Core = function () {

        var self = this;

        /// string constant of hiddenValue attribute name in html element.
        self.HiddenValueConstant = "hiddenValue";

        /// Does ajax request by parameters.
        self.AjaxRequest = function (ajaxOptions) {

            if (ajaxOptions.showMask) {
                $core.ShowMask(ajaxOptions.maskDiv);
            }
            
            try {

                var token = $("input[name='__RequestVerificationToken']").val();
                if (token) {
                    ajaxOptions.headers["__RequestVerificationToken"] = token;
                }
                $.ajax({
                    cache: false,
                    url: ajaxOptions.url,
                    type: ajaxOptions.method,
                    data: ajaxOptions.data,
                    dataType: ajaxOptions.dataType,
                    contentType: ajaxOptions.contentType,
                    headers: ajaxOptions.headers,
                    success: function (result) {
                        if (ajaxOptions.dataType == "json" && result) {
                            try {
                                result = $.parseJSON(result);
                            }
                            catch (error) {
                                ajaxOptions.successCallBack(result);
                                if (ajaxOptions.showMask) {
                                    $core.HideMask(ajaxOptions.maskDiv);
                                }
                                return;
                            }
                        }
                        ajaxOptions.successCallBack(result);
                        if (ajaxOptions.showMask) {
                            $core.HideMask(ajaxOptions.maskDiv);
                        }
                    },
                    async: ajaxOptions.isAsync,
                    error: function (jqXhr, status, thrownError) {
                        //b.SendErrorMail(s.responseText, q, r, p);
                        if (ajaxOptions.showMask) {
                            $core.HideMask(ajaxOptions.maskDiv);
                        }
                        ajaxOptions.errorCallBack(jqXhr, status, thrownError);
                    }
                });
            }
            catch (error) {
                if (ajaxOptions.showMask) {
                    $core.HideMask(ajaxOptions.maskDiv);
                    ajaxOptions.errorCallBack();
                }
            }
        };

        /// Does async ajax json post request with mask.
        self.AjaxJsonAsyncPostWithMask = function (url, data, parameterName, succesCallBak, maskContainerSelector, errorCallBack) {
            var jsonDataToPost = JSON.stringify({ parameterName: data });
            jsonDataToPost = jsonDataToPost.replace("parameterName", parameterName);
            self.AjaxRequest(url, "POST", jsonDataToPost, "json", "application/json; charset=UTF-8", function (result) {
                succesCallBak(result);
            }, function (jqxhr, status, errorThrown) {
                errorCallBack(jqxhr, status, errorThrown);
            }, true, true, maskContainerSelector);
        };

        /// Shows a loading like mask in place of a selector.
        self.ShowMask = function (maskContainerSelector) {
            var $maskContainer = $(maskContainerSelector);
            $maskContainer.mask($resources.Loading);
        };

        /// Hides loading like mask in place of a selector.
        self.HideMask = function (maskContainerSelector) {
            var $maskContainer = $(maskContainerSelector);
            $maskContainer.unmask();
        };

        /// Shows error information.
        self.ShowError = function (errorHtml) {
            var errorDivName = "errorInformationDiv";
            var $errorDiv = $("[#" + errorDivName + "]");

            if ($errorDiv.length == 0) {
                //Create error div.
                $errorDiv = $("<div style='display:none;'></div>");
            }

            $errorDiv.html(errorHtml);
            $errorDiv.show();
            var hideError = function () {
                $errorDiv.hide();
            };
        };

        self.GetVerificationTokenHeaders = function () {
            var ajaxHeaders = new Array();
            ajaxHeaders["__RequestVerificationToken"] = $("input[name='__RequestVerificationToken']").val();
            return ajaxHeaders;
        };

        self.GetVerificationToken = function () {
            var token = $("input[name='__RequestVerificationToken']").val();
            return token;
        };

        /// Does initilization process of core library
        self.Initialize = function () {
            //self.ui.DataPicker();
        };

        self.serializeObject = function (selector) {
            var o = {};
            var a = $(selector).serializeArray();
            $.each(a, function () {
                if (o[this.name] !== undefined) {
                    if (!o[this.name].push) {
                        o[this.name] = [o[this.name]];
                    }
                    o[this.name].push(this.value || '');
                } else {
                    o[this.name] = this.value || '';
                }
            });
            return o;
        };

        self.IsJson = function (data) {
            try {
                var jsonModel = $.parseJSON(data);
                return true;
            } catch (e) {
                return false;
            }
        };

        /// When document ready, do core jobs.
        $(document).ready(function () {
            self.Initialize();
        });
    };

    var core = new Core();
    _window.$core = core;

})(this.window);


var ajaxOptions = (function () {

    var self = this;
    var _constructor = function () {
        //this.url = null;
        //this.method = null;
        //this.data = null;
        //this.dataType = "json;charset=UTF-8";;
        //this.contentType = "applicatiom/json;"; //"application/x-www-form-urlencoded; charset=UTF-8";
        //this.successCallBack = null;
        //this.errorCallBack = null;
        //this.isAsync = true;
        //this.showMask = true;
        //this.maskDiv = "#divMask";
        //this.headers = [];
    };

    _constructor.prototype = {
        url: null,
        method: null,
        data: null,
        dataType: "json",
        contentType: "application/json", //"application/x-www-form-urlencoded, charset:UTF-8",
        successCallBack: null,
        errorCallBack: null,
        isAsync: true,
        showMask: true,
        maskDiv: "#divMask",
        headers: [],
    };
    return _constructor;
})();
