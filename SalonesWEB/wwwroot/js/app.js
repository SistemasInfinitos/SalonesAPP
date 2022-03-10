function redictTo(ruta, fast) {
    setTimeout(() => { window.location.href = ruta }, fast == "fast" ? 2000 : 5000);
};

function UpdateStateEntity(RecordId, newState, Grupo, url, urlRedirect) {
    $.ajax({
        type: "POST",
        url: url,
        dataType: 'json',
    data: {
        registroId: RecordId,
        estadoNuevo: newState,
        grupo: Grupo
    },
    success: function (response) {
        if (response.Ok) {
            toastr["success"](response.Message, "Actualizado");
            redictTo($("#urlRedirect").val(), "fast");
        } else {
            toastr["error"](response.Message, "Error");
        }
    },
    error: function (error) {
        
    }
})
}