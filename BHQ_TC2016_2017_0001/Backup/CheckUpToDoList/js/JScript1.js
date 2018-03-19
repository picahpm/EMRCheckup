//CtrlName.Attributes("onkeypress") = "return checknum(arguments[0] , 'd','" & CtrlName.ClientID & "')"
function checknum(e, n, values) {
    if (!e) {
        e = window.event;
    }

    if (typeof e.which == "number") {
        e = e.which;
    } else if (typeof e.keyCode == "number") {
        e = e.keyCode;
    } else if (typeof e.charCode == "number") {
        e = e.charCode;
    }

    if (e >= 48 && e <= 57 || e == 13 || e == 8 || e == 0 || n == "d" && e == 46) {
        if ((e == 46 && values != "") && document.getElementById(values).value.indexOf(".") > -1) {
            return false;
        } else {
            return true;
        }
    } else {
        return false;
    }
}

function reloadparent() {
    try {
        parent.parent.document.getElementById('frm2').location.reload();

    } catch (e) {

    }
    //var reloadParent = self.parent.location.reload();
   // setTimeout("reloadParent", 500);
}