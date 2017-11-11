

function lbyAlert(type, message) {

    var iconimg;

    switch (type) {
        case "success":
            iconimg = "/content/images/check.png";
            break;

        case "error":
            iconimg = "/content/images/cross.png";
            break;
    }

    var alertmessage = function () {
        iosOverlay({
            text: message,
            duration: 2e3,
            icon: iconimg
        });
        return false;
    }

    alertmessage();
}
