@ModelType dbstructure.products



<!-- CARD -->
<div class="card" style="width: 15rem; height:16rem">
    @If Model.ProductPicture IsNot Nothing Then
        Dim base64Image As String = Convert.ToBase64String(Model.ProductPicture)
        Dim imageSrc As String = "data:image/png;base64," & base64Image
        @<button type="button" class="p-0 border-0 bg-transparent" onclick="handleProductClick(@Model.IdProduct)">
            <img src="@imageSrc" style="width: 10rem; height:10rem" class="card-img-top" alt="@Model.ProductName" />
        </button>
    End If

    <div class="card-body text-center">
        <h5 class="card-title">@Model.ProductName</h5>
        <p class="card-text fw-bold">@Model.ProductPrice.ToString("C")</p>
    </div>
</div>

