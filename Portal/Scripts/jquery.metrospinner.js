// Official Windows 8 Loading Indicator extracted from SkyDrive
var metroSpinner = {
    createLoading: function (n, loaderType, width, height, dotSize, dotColor, loaderText) {
        // 1. fuck with parameter
        var markup = n;
        var v, k;
        typeof n == "object" && (markup = n.returnMarkup, loaderType = n.type, width = n.width, height = n.height, dotSize = n.size, dotColor = n.color, loaderText = n.text);

        // assign default if parameter is null
        loaderType = loaderType || metroSpinner.loadingType.spinning;
        width = width || "20px";
        height = height || width;
        dotSize = dotSize || "3px";
        dotColor = dotColor && dotColor.toLowerCase() || "#666";

        var containerClass = "c_spinningDots";
        if (loaderType == metroSpinner.loadingType.flying) {
            containerClass = "c_flyingDots";
        }

        // 2. generate html
        var loaderContainer = document.createElement("div");
        $(loaderContainer).css({
            width: width,
            height: height,
            "font-size": dotSize
        }),
        loaderContainer.className = containerClass + " c_loadingDots c_dotsPlaying";
        
        var html = '<div class="c_loadingTracks">'; 
        for (var i = 5; i > 0; i--) {
            html += '<div class="c_loadingTrack c_loadingTrack' + i + '">' +
                        '<div class="c_loadingDot" style="background-color:' + dotColor + "; width:" + dotSize + "; height:" + dotSize + '">' +
                        '<\/div>' +
                     '<\/div>';
        }
        html += "<\/div>";

        loaderContainer.innerHTML = html;
        var c = $("<div><\/div>").append(loaderContainer);
        loaderType == metroSpinner.loadingType.flying && c.css({
             width: width
        });
        
        return (loaderText && c.append('<span class="c_loadingText">' + loaderText.encodeHtml() + "<\/span>"), markup) ?
                    (v = $("<span><\/span>").append(c), k = v.html(), v.remove(), k) :
                    c[0];
    },
    loadingType: {
        spinning: 0,
        flying: 1
    },
    hideLoading: function (obj) {
        $(obj).hide(),
        $(obj).removeClass("c_dotsPlaying");
    },
    showLoading: function (obj) {
        $(obj).show();
        $(obj).addClass("c_dotsPlaying");
    }
};