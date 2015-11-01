
var vzaApplicationName = "vza";
var vzaIdleTime = null;
var vzaActionTime = null;
var vzaPageId = null;
var vzaGuID =0;

(function ($) {
    
    $.fn.getCookies = function () {
        var pairs = document.cookie.split(";");
        var cookies = {};
        for (var i = 0; i < pairs.length; i++) {
            var pair = pairs[i].split("=");
            cookies[pair[0]] = unescape(pair[1]);
        }
        return cookies;
    }

    $.fn.getGUID=function() {
        function s4() {
            return Math.floor((1 + Math.random()) * 0x10000)
              .toString(16)
              .substring(1);
        }
        return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
          s4() + '-' + s4() + s4() + s4();
    }

    var getPageName = function (url) {
        var index = url.lastIndexOf("/") + 1;
        var filenameWithExtension = url.substr(index);
        var filename = filenameWithExtension.split(".")[0];
        return filename;
    };

    var details = { action:'', actionname:'',device: navigator.userAgent.toLowerCase(), pagename:getPageName(document.location.href),resolution:$(window).width()+"X"+$(window).height(),city:'',country:'',ip:'',postal:'',region:'' };

    $.get("http://ipinfo.io", function (response) {
        //alert(response.ip);
        details.city = response.city;
        details.country = response.country;
        details.ip = response.ip;
        details.postal = response.postal;
        details.region = response.region;
        details.latitue=response.loc;
    }, "jsonp");


    
    $.fn.vzalog = function (details) {
        var img = new Image();
        img.src = "logger.aspx?&appid="
            + vzaApplicationName + "&pgId=" + vzaPageId + "&pglt=" + vzaPageloadtime + "&ac=" + details.action + "&acn=" + details.actionname +
            "&dt=" + new Date() + "&pgit=" + vzaIdleTime + "&dc=" + details.device + "&cc=" + details.ccookies +
            "&city=" + details.city + "&ctry=" + details.country + "&rgn=" + details.region + "&ip=" + details.ip + "&zip=" + details.postal +
            "&lat=" + details.latitue + "&pgtl=" + document.title + "&pgnm=" + details.pagename + "&hst=" + document.location.hostname + "&res=" + details.resolution +
            "&uid=" + vzaGuID + "&ref=" + document.referrer;

    };

    var start = new Date();
    $(window).on("click", function (e) {
        var eleID="";
        if (event.target)
            eleID = event.target.id;
        
            vzaIdleTime = parseInt(new Date() - vzaActionTime);
            details.action = e.originalEvent.type;
            details.actionname = eleID;
            $.fn.vzalog(details);
        
    });

    $(window).load(function (e) {
        //$('body').html(new Date() - start);
        vzaGuID = $.fn.getGUID();
        vzaPageloadtime = parseInt(new Date() - start);
        details.action=e.originalEvent.type;
        details.actionname='PageLoad';
        $.fn.vzalog(details);
        this.vzaActionTime = new Date();
    });

    $(window).unload(function (e) {
        //$('body').html(new Date() - start);
        vzaPageloadtime = parseInt(new Date() - start);
        details.action = e.originalEvent.type;
        details.actionname = 'PageUnload';
        $.fn.vzalog(details);
        this.vzaActionTime = new Date();
    });

    $(window).error(function (e) {
        vzaPageloadtime = parseInt(new Date() - start);
        details.action = 'Error';
        details.actionname = 'Error message';
        $.fn.vzalog(details);
        this.vzaActionTime = new Date();
    });

   

}(jQuery));