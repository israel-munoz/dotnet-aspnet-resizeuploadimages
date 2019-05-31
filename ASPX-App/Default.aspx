<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ASPX_App.Default" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta charset="utf-8" />
    <title>Subir imágenes</title>
    <link rel="stylesheet" href="<%= ResolveClientUrl("~/css/index.css") %>" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <label>
                <asp:RadioButton id="SaveDisk" runat="server" GroupName="save"
                    Checked="true" />
                Guardar imagen en disco
            </label>
            <label>
                <asp:RadioButton id="SaveDb" runat="server" GroupName="save" />
                Guardar imagen en base de datos
            </label>
        </div>
        <div>
            <asp:Label AssociatedControlId="FileUploader" runat="server"
                Text="Seleccionar una imagen:" />
            <asp:FileUpload id="FileUploader" runat="server" />
        </div>
        <div>
            <asp:Button id="UploadImage" runat="server"
                Text="Cargar imagenes" OnClick="UploadImage_Click"/>
        </div>
        <div class="message">
            <asp:Literal ID="Message" runat="server" />
        </div>
    </form>
</body>
</html>
