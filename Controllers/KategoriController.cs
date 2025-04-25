using dotnet_store.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_store.Controllers;

public class KategoriController : Controller
{
    private readonly DataContext _context;
    public KategoriController(DataContext context)
    {
        _context = context;
    }

    public ActionResult Index()
    {
        var kategoriler = _context.Kategoriler.Select(i => new KategoriGetModel
        {
            Id = i.Id,
            KategoriAdi = i.KategoriAdi,
            Url = i.Url,
            UrunSayisi = i.Uruns.Count
        }).ToList();
        return View(kategoriler);
    }

    [HttpGet]
    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(KategoriCreateModel model)
    {
        var entity = new Kategori
        {
            KategoriAdi = model,
            Url = model
        };

        _context.Kategoriler.Add(entity);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }

}