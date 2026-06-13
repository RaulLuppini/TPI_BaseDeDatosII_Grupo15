document.addEventListener('DOMContentLoaded', () => {
    console.log("js cargado");

    const textuser = document.getElementById("pnlUsuario");
    if (textuser) {
        textuser.style.setProperty("display", "none", "important");
    }
});