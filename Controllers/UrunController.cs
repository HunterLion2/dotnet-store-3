using dotnet_store.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_store.Controllers;

public class UrunController : Controller
{
    private readonly DataContext _context;
    public UrunController(DataContext context)
    {
        _context = context;
    }

    public ActionResult Index()
    {
        var urunler = _context.Urunler.Select(i => new UrunGetModel
        {
            Id = i.Id,
            UrunAdi = i.UrunAdi,
            Fiyat = i.Fiyat,
            Aktif = i.Aktif,
            Anasayfa = i.Anasayfa,
            KategoriAdi = i.Kategori.KategoriAdi,
            Resim = i.Resim
        }).ToList();

        return View(urunler);
    }

    public ActionResult List(string url, string q)
    {
        var query = _context.Urunler.Where(i => i.Aktif);

        if (!string.IsNullOrEmpty(url))
        {
            query = query.Where(i => i.Kategori.Url == url);
        }

        if (!string.IsNullOrEmpty(q))
        {
            query = query.Where(i => i.UrunAdi.ToLower().Contains(q.ToLower()));

            ViewData["q"] = q;
        }

        return View(query.ToList());
    }

    public ActionResult Details(int id)
    {
        var urun = _context.Urunler.Find(id);

        if (urun == null)
        {
            return RedirectToAction("Index", "Home");
        }

        ViewData["BenzerUrunler"] = _context.Urunler
                                        .Where(i => i.Aktif && i.KategoriId == urun.KategoriId && i.Id != id)
                                        .Take(4)
                                        .ToList();

        return View(urun);
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(string urunAdi, string urunAciklama, double UrunFiyat)
    {
        var entity = new Urun()
        {
            UrunAdi = urunAdi,
            Aciklama = urunAciklama,
            Fiyat = UrunFiyat,
            Aktif = true,
            Anasayfa = true,
            KategoriId = 1,
            Resim = "1.jpeg"
        };

        _context.Urunler.Add(entity);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }
}