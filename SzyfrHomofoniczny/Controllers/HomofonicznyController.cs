using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Text;
using SzyfrHomofoniczny.Models;

namespace SzyfrHomofoniczny.Controllers
{


    public class HomofonicznyController : Controller
    {
        private readonly HomofonicznySzyfr _homofonicznySzyfr;

        public HomofonicznyController()
        {
            _homofonicznySzyfr = new HomofonicznySzyfr();
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Szyfrowanie()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Szyfrowanie(Homofoniczny model)
        {
            if (ModelState.IsValid)
            {

                string tekst = model.Tekst.Replace(" ", "").ToLower();

                // Przygotuj slownik homofonów
                Dictionary<char, List<string>> dictionary = _homofonicznySzyfr.Dictionary;

                StringBuilder zaszyfrowanyTekst = new StringBuilder();
                Random random = new Random();

                foreach (char znak in tekst)
                {
                    if (dictionary.ContainsKey(znak))
                    {
                        List<string> homofony = dictionary[znak];
                        int losowyIndex = random.Next(homofony.Count);
                        string wybranyHomofon = homofony[losowyIndex];
                        zaszyfrowanyTekst.Append(wybranyHomofon);
                    }
                   
                }
                model.ZaszyfrowanyTekst = zaszyfrowanyTekst.ToString();
            }

            return View(model);
        }

        


        [HttpGet]
        public IActionResult Deszyfrowanie()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Deszyfrowanie(Homofoniczny model)
        {
            if (ModelState.IsValid)
            {
                string zaszyfrowanyTekst = model.Tekst;

                // Przygotuj slownik homofonów
                Dictionary<char, List<string>> dictionary = _homofonicznySzyfr.Dictionary;
                StringBuilder odszyfrowanyTekst = new StringBuilder();
                int index = 0;

                while (index < zaszyfrowanyTekst.Length)
                {
                    // Sprawdź, czy aktualny fragment jest homofonem
                    bool jestHomofonem = false;
                    foreach (var kvp in dictionary)
                    {
                        foreach (var homofon in kvp.Value)
                        {
                            if (zaszyfrowanyTekst.Substring(index).StartsWith(homofon))
                            {
                                // Znaleziono homofon, odszyfruj literę i dodaj na koniec 
                                odszyfrowanyTekst.Append(kvp.Key);
                                index += homofon.Length;
                                jestHomofonem = true;
                                break;
                            }
                        }
                        if (jestHomofonem)
                        {
                            break;
                        }
                    }

                    if (!jestHomofonem)
                    {
                        // To nie jest homofon, ignoruj i tłumacz dalej      
                        index++;
                    }
                }
                model.ZaszyfrowanyTekst = odszyfrowanyTekst.ToString();
            }

            return View(model);
        }

    }
}


