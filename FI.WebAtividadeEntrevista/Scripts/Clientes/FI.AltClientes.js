
$(document).ready(function () {
    if (obj) {
        $('#formCadastro #Nome').val(obj.Nome);
        $('#formCadastro #CEP').val(obj.CEP);
        $('#formCadastro #Email').val(obj.Email);
        $('#formCadastro #Sobrenome').val(obj.Sobrenome);
        $('#formCadastro #Nacionalidade').val(obj.Nacionalidade);
        $('#formCadastro #Estado').val(obj.Estado);
        $('#formCadastro #Cidade').val(obj.Cidade);
        $('#formCadastro #Logradouro').val(obj.Logradouro);
        $('#formCadastro #Telefone').val(obj.Telefone);
        $('#formCadastro #Cpf').val(obj.Cpf);

        $('#formCadastro #Cpf').mask('999.999.999-99', { placeholder: "___.___.___-__" });
    }

    $(function () {
        $(".beneficiario").click(function () {
            var id = $(this).attr("data-id");
            $("#modalBeneficiario").load("/Cliente/Beneficiario/" + id).attr("title", "Beneficiários");
            $("#modalBeneficiario").modal('show');
        });
    });

    $(".salvarBeneficiario").click(function () {
        $.ajax({
            url: urlBeneficiario,
            method: "POST",
            data: {
                "Nome": $(this).find("#Nome").val(),
                "Cpf": $(this).find("#Cpf").val().replace(/[^\d]+/g, ''),
                "IdCliente": $(this).find("#IdCliente").val()
            },
            error:
                function (r) {
                    if (r.status == 400)
                        ModalDialog("Ocorreu um erro", r.responseJSON);
                    else if (r.status == 500)
                        ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
                },
            success:
                function (r) {
                    ModalDialog("Sucesso!", r)
                    $("#formCadastro")[0].reset();
                    window.location.href = urlRetorno;
                }
        });
    });

    $('#formCadastro').submit(function (e) {
        e.preventDefault();

        $.ajax({
            url: urlPost,
            method: "POST",
            data: {
                "NOME": $(this).find("#Nome").val(),
                "CEP": $(this).find("#CEP").val(),
                "Email": $(this).find("#Email").val(),
                "Sobrenome": $(this).find("#Sobrenome").val(),
                "Nacionalidade": $(this).find("#Nacionalidade").val(),
                "Estado": $(this).find("#Estado").val(),
                "Cidade": $(this).find("#Cidade").val(),
                "Logradouro": $(this).find("#Logradouro").val(),
                "Telefone": $(this).find("#Telefone").val(),
                "Cpf": $(this).find("#Cpf").val().replace(/[^\d]+/g, '')
            },
            error:
                function (r) {
                    if (r.status == 400)
                        ModalDialog("Ocorreu um erro", r.responseJSON);
                    else if (r.status == 500)
                        ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
                },
            success:
                function (r) {
                    ModalDialog("Sucesso!", r)
                    $("#formCadastro")[0].reset();
                    window.location.href = urlRetorno;
                }
        });
    });
})

function ModalDialog(titulo, texto) {
    var random = Math.random().toString().replace('.', '');
    var texto = '<div id="' + random + '" class="modal fade">                                                               ' +
        '        <div class="modal-dialog">                                                                                 ' +
        '            <div class="modal-content">                                                                            ' +
        '                <div class="modal-header">                                                                         ' +
        '                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>         ' +
        '                    <h4 class="modal-title">' + titulo + '</h4>                                                    ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-body">                                                                           ' +
        '                    <p>' + texto + '</p>                                                                           ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-footer">                                                                         ' +
        '                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>             ' +
        '                                                                                                                   ' +
        '                </div>                                                                                             ' +
        '            </div><!-- /.modal-content -->                                                                         ' +
        '  </div><!-- /.modal-dialog -->                                                                                    ' +
        '</div> <!-- /.modal -->                                                                                        ';

    $('body').append(texto);
    $('#' + random).modal('show');
}

function ModalIncluirBeneficiario(IdCliente) {
    var texto =

        '<div id="modalIncluirBeneficiario" class="modal fade">  ' +
        '< form id = "formBeneficiario" method = "post" >' +
        '	<div class="modal-dialog">  ' +
        '		<div class="modal-content">  ' +
        '			<div class="modal-header">  ' +
        '				<h4 class="modal-title">Beneficiários</h4> ' +
        '			</div>  ' +
        '        <div class="modal-body">  ' +
        '			<div class="row">  ' +
        '				<input type="hidden" id="IdCliente" name="IdCliente" value="  IdCliente  "> ' +
        '				<div class="col-md-5">  ' +
        '					<div class="form-group">  ' +
        '						<label for="Cpf">CPF:</label>  ' +
        '						<input required="required" type="text" class="form-control" id="Cpf" name="Cpf" placeholder="Ex.: 010.011.111-00" maxlength="14"> ' +
        '					</div>  ' +
        '				</div>  ' +
        '				<div class="col-md-5">  ' +
        '					<div class="form-group">  ' +
        '						<label for="Nome">Nome:</label>  ' +
        '						<input required="required" type="text" class="form-control" id="Nome" name="Nome" placeholder="Ex.: João" maxlength="50"> ' +
        '					</div>  ' +
        '				</div>' +
        '				<div class="col-md-2 pull-left" style="margin-top:26px;">  ' +
        '					<div class="form-group">   ' +
        '						<button type="submit" class="btn btn-sm btn-success salvarBeneficiario">Salvar</button>  ' +
        '					</div>  ' +
        '				</div>  ' +
        '			</div> 	' +
        '		<div class="modal-footer">  ' +
        '			<button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button> ' +
        '		</div>  ' +
        '		</div>  ' +
        '		</div>  ' +
        '	</div>  ' +
        '</form>' +
        '</div> '
        ;

    $('body').append(texto);
    $('#modalIncluirBeneficiario').modal('show');
}


