namespace dotnet_store.Models;

public class KategoriCreateModel
{
    [Display(Name = "Kategori Adı")]
    public string KategoriAdi { get; set; } = null!;

    [Display(Name = "Url")]
    public string Url { get; set; } = null!;
}

