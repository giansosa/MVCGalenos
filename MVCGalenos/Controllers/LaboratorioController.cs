using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCGalenos.Models;

namespace MVCGalenos.Controllers
{
    public class LaboratorioController : Controller
    {
        // GET: LaboratorioController
        public ActionResult Index()
        {
            return View();
        }


        // GET: LaboratorioController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LaboratorioController/Create
        public ActionResult Create()
        {
            return View();
        }
        // Acción para manejar la carga de archivos
        [HttpPost]
        public IActionResult SubirEstudio(Laboratorio model, IFormFile archivo)
        {
            if (ModelState.IsValid && archivo != null)
            {
                // Validar que el archivo sea un PDF
                if (Path.GetExtension(archivo.FileName).ToLower() != ".pdf")
                {
                    ModelState.AddModelError("ArchivoEstudio", "Solo se permiten archivos en formato PDF.");
                    return View("Index", model);
                }

                // Guardar archivo en una carpeta específica
                var rutaCarpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "archivos");
                if (!Directory.Exists(rutaCarpeta))
                {
                    Directory.CreateDirectory(rutaCarpeta);
                }

                var rutaArchivo = Path.Combine(rutaCarpeta, archivo.FileName);
                using (var stream = new FileStream(rutaArchivo, FileMode.Create))
                {
                    archivo.CopyTo(stream);
                }

                // Simular guardado en base de datos
                // En un entorno real, aquí se guardaría la información en una base de datos.
                ViewBag.Mensaje = "El archivo se ha subido correctamente.";
                return RedirectToAction("Index");
            }

            return View("Index", model);
        }
    

    // POST: LaboratorioController/Create
    [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LaboratorioController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LaboratorioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LaboratorioController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
        public IActionResult SubirEstudio()
        {
            return View(new Laboratorio());
        }
        [HttpPost]
        public async Task<IActionResult> SubirEstudio(Laboratorio laboratorio)
        {
            if (laboratorio.ArchivoEstudio != null && laboratorio.ArchivoEstudio.Length > 0)
            {
                // Validar tipo de archivo
                var fileExtension = Path.GetExtension(laboratorio.ArchivoEstudio.FileName).ToLower();
                if (fileExtension != ".pdf")
                {
                    ModelState.AddModelError("ArchivoEstudio", "Solo se permiten archivos PDF.");
                    return View(laboratorio);
                }

                // Validar tamaño del archivo (por ejemplo, no mayor a 5MB)
                if (laboratorio.ArchivoEstudio.Length > 5 * 1024 * 1024)
                {
                    ModelState.AddModelError("ArchivoEstudio", "El archivo no debe exceder los 5 MB.");
                    return View(laboratorio);
                }

                // Guardar el archivo (si pasa las validaciones)
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", laboratorio.ArchivoEstudio.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await laboratorio.ArchivoEstudio.CopyToAsync(stream);
                }

                ViewBag.Mensaje = "Estudio subido correctamente.";
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("ArchivoEstudio", "El archivo de estudio es obligatorio.");
            }


            // Si no es válido, devolver el formulario con los errores
            return View(laboratorio);
        }


        // POST: LaboratorioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
