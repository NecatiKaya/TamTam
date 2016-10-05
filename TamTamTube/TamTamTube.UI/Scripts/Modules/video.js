/// <reference path="_references.js" />
(function (windowParam) {

    var _window = windowParam;
    var self = this;
    var $ = _window.$;

    /// Core javascript type. 
    var VideoCollector = function () {

        var self = this;

        self.VideoItemHtml = '<div class="col-md-3 portfolio-item">'
        + '<div>'
              + '<h3>{0}</h3>'
              + '<h6>{1}</h4>'
              + '<h5>Provider : {2}</h5>'
              + '<h4 style="font-size:9px;"><a class="myhtml5lightbox" target="_self" href="{3}">{3}</a></h4>'
              + '<h3>{4}</h3>'
            + '</div>'
        + '</div>';

        self.ImdbVideLinkHtml = "http://www.imdb.com/title/tt3344680/?ref_=nv_sr_1";

        self.YotubueVideLinkHtml = "https://www.youtube.com/watch?v={0}";

        self.FaceBookShareUrl = "<a href=\"javascript:fbShare('{0}', '{1}', '{2}')\">Share</a>";

        /// Searches video by parametes
        self.Search = function (autoCompleteVal) {

            var replace = function (str) {
                return str.replace("'", "`").replace('"', "`");
            }

            var verificationToken = $core.GetVerificationToken();
            var query = $("#txtQuery").val();
            if (autoCompleteVal) {
                query = autoCompleteVal;
            }
            var options = new ajaxOptions();
            options.url = "http://localhost:41766/api/Video/search?SearchQuery=" + query + "&MaxResult=50&VerificationToken=" + verificationToken;
            options.method = "POST";
            options.showMask = false;
            //options.dataType = "json"; //no need to set, it is default
            //options.contentType = "application/json"; //no need to set, it is default
            options.successCallBack = function (result) {
                $(".container-fluid").empty();
                if (result) {
                    if (result.Status == 1) { // Success
                        var html = "";
                        var imdbLink = "";
                        var youtubeLink = "";
                        var tempItem = null;
                        var shareUrl = null;
                        for (var i = 0; i < result.Data.length; i++) {
                            tempItem = result.Data[i];
                            shareUrl = String.format(self.FaceBookShareUrl, tempItem.VideoUrl, replace(tempItem.Title), "");
                            if (tempItem.VideoSourceProvider == 1) {
                                youtubeLink = String.format(self.YotubueVideLinkHtml, result.Data[0].VideoUrl);
                                html = String.format(self.VideoItemHtml, tempItem.Title, tempItem.Description, "Youtube", tempItem.VideoUrl, shareUrl);
                            }
                            else {
                                imdbLink = String.format(self.ImdbVideLinkHtml, result.Data[0].VideoUrl);
                                html = String.format(self.VideoItemHtml, tempItem.Title, tempItem.Description, "Imdb", tempItem.VideoUrl, shareUrl);
                            }
                            $(".container-fluid").append(html);
                            jQuery(".myhtml5lightbox").html5lightbox();
                        }
                    }
                    else { // Error
                        alert("Ooops error occured. Check browser log");
                        console.log(result);
                    }
                }
                $('.hoveranimation').hover(function () {

                    $('.realContent', this).hide();
                    $('.hoverContent', this).show();
                }, function () {

                    $('.hoverContent', this).hide();
                    $('.realContent', this).show();
                });
            };
            options.errorCallBack = function (a, b, c) {
                console.log(a);
                console.log(b);
                console.log(c);
                alert("Error occured. You can check browser console log");
            };
            $core.AjaxRequest(options);
        }

        /// Changes a textbox in an autocomplete
        self.MakeAutoComplete = function (jqueryControlSelector) {
            $(jqueryControlSelector).autocomplete({
                source: function (request, response) {
                    var verificationToken = $core.GetVerificationToken();
                    var query = $(jqueryControlSelector).val();
                    var link = "http://localhost:41766/api/Video/Suggestions?query=" + query + "&token=" + verificationToken;                    
                    var options = new ajaxOptions();
                    options.url = link;
                    options.method = "POST";
                    options.showMask = false;
                    options.successCallBack = function (result) {
                        response($.map(result, function (item) {
                            return {
                                label: item.Title,
                                value: item.Value
                            };
                        }));
                    },
                    options.errorCallBack = function (a, b, c) {
                        console.log(a);
                        console.log(b);
                        console.log(c);
                        alert("Error occured. You can check browser console log");
                    };
                    $core.AjaxRequest(options);
                },
                minLength: 1,
                select: function (event, ui) {

                    self.Search(ui.item.label);
                    //console.log(ui.item ?
                    //  "Selected: " + ui.item.label :
                    //  "Nothing selected, input was " + this.value);

                    //alert("Error occured. You can check browser console log");
                },
                open: function () {
                    $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
                },
                close: function () {
                    $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
                }
            });
        };
    };

    var videoCollector = new VideoCollector();
    _window.$video = videoCollector;
    $video.MakeAutoComplete("#txtQuery");
})(this.window);