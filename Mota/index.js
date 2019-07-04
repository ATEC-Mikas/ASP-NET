function ValidarCampos(event) {
    let error = "";
    let pname = document.getElementById("pname");
    let uname = document.getElementById("uname");
    if (pname.value == "" || pname.value == null)
        error += "\nCampo do Primeiro nome precisa de estar preenchido";
    if (uname.value == "" || uname.value == null)
        error += "\nCampo do Ultimo nome precisa de estar preenchido";
    
    if (error != "") {
        event.preventDefault();
        document.getElementById("erro").innerText = error;
    }
    else {
        //Comentado para o ASPX
        //event.preventDefault();
        //document.getElementById("erro").innerText = "";
        //document.getElementById("resposta").innerText = "Bem vindo "+pname.value+" "+uname.value;
    }
}

window.addEventListener("load", function () {
    document.getElementById("formP").addEventListener("submit", ValidarCampos);
});

