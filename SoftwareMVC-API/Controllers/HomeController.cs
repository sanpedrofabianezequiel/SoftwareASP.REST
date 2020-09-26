using Newtonsoft.Json;
using SoftwareWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace SoftwareMVC_API.Controllers
{
    public class HomeController : Controller
    {
        //URL https://jsonplaceholder.typicode.com del SERVICIO
        public async System.Threading.Tasks.Task<ActionResult> Index()
        {
            //URL + "/Router"
            string URL= "https://jsonplaceholder.typicode.com/posts";


            //Creamos una instancia de HttpCliente
            var httpCliente = new HttpClient();
            


            //NOS DEVUELVE EN FORMATO JSON=>String
            //NOS DEVUELVE EN FORMATO JSON
            //Nos permite hacer una consulta por GET a una URL
            //GetSringAsync return un TASK por ende => necesitamos "castiarle" un AWAIT
            //Necesito convertir el METODO en metodo ASYNC
            //Retorna un Task<ActionResult>
            var json = await httpCliente.GetStringAsync(URL);



            //Convirtamos el JSON en una LISTA de CONTINENTES
            //Instalamos en la consola de Nugeet
            //1 => Herramietas =>Administracion de PackeNugget=>Consola PackNugget
            //2 => Install-Package Newtonsoft.Json

            //Necesitamos Linkiar para NO Mantener Modelo en 2 Proyectos
            //LINKIARLO desde otro proyecto (Agregar como Vinculo)


            //JsonConvert.DeserializeObject<Objeto del TIPO>(parametro a convertir);
            //=>Puede ser .DeserializeObject<Objeto del TIPO>
            //=> <Lista<TIPO>>
            //Nos da un error por que NO coinciden las propiedades donde estoy consultand
            //  var continentesList = JsonConvert.DeserializeObject<List<Continent>>(json);
            var jsonPlaceHolderList = JsonConvert.DeserializeObject<List<JsonPlaceHolder>>(json);
            Debug.Write(json);
            
            //Le enviamos la vista
            //En la vista de este Metodo/controller tenemos que usar un ForEach()
            return View(jsonPlaceHolderList);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}