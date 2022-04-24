﻿using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using Web.Extensions;


namespace Web.Controllers
{
    public class FolderMedicalController : Controller
    {
     

        FolderMedicalService folderMedicalService ;

        VaccineChildService vaccineChildService;

        private static List<ChildVaccine>  listG= new List<ChildVaccine>();
        private static int idF;

        public FolderMedicalController()
        {
             String token = (String)System.Web.HttpContext.Current.Session["AccessToken"];
            folderMedicalService = new FolderMedicalService(token);
            vaccineChildService = new VaccineChildService(token);

        }

        // GET: FolderMedical
        public ActionResult Index(String filtre)
        {

            

           

            if (String.IsNullOrEmpty(filtre))
            {

                return View(folderMedicalService.GetAll());
            }


            return View(folderMedicalService.GetAll().Where(f=>f.Child.Name.ToLower().Contains(filtre.ToLower())));
        }

        // GET: FolderMedical/Details/5
        public ActionResult Details(int id)
        {
            FolderMedical folderMedical = folderMedicalService.GetById(id);

            

           

                  if (folderMedical != null)
                   {
                      if (folderMedical.ListVaccinesToDo.Count() != 0)
                      {
                    this.AddNotification("Vaccine to do for child", NotificationType.WARNING);
                          }

                  return View(folderMedical);
                   }
            

            return View();
        }

       

        // GET: FolderMedical/Create
        public ActionResult Create()
        {
             


            ViewBag.ChildId = new SelectList(folderMedicalService.getAllChild(), "Id", "Name");


             

            return View();
        }

        // POST: FolderMedical/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Allergy,Particularity,ChildId")]FolderMedical folder)
        {
           


                if (  folderMedicalService.Add(folder))
                {
                    return RedirectToAction("Index");
                }

 
           
                return View();
            
        }

        // GET: FolderMedical/Edit/5
        public ActionResult Edit(int id)
        {


            ViewBag.ChildId = new SelectList(folderMedicalService.getAllChild(), "Id", "Name");

            ViewBag.VaccineId = new SelectList(vaccineChildService.GetAll(), "Id", "Description");


            FolderMedical folderMedical = folderMedicalService.GetById(id);
 

            listG.Clear();
            listG.AddRange(folderMedical.LisChildVaccines);
 
            idF = id;


                if (folderMedical != null)
                {

                    

                    return View(folderMedical);
                }
             

            return View();

          
        }

        // POST: FolderMedical/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, [Bind(Include = "Id,Allergy,Particularity,ChildId,DateC,VaccineId,LisChildVaccines")] FolderMedical folderMedical)
        {


            ChildVaccine childVaccine = vaccineChildService.GetById(folderMedical.VaccineId);

            if (childVaccine != null)
            {

                if (listG.Contains(childVaccine) == false)
                {

                    listG.Add(childVaccine);
                }

                folderMedical.LisChildVaccines = listG;

               

            }



                if (  folderMedicalService.UpdateFolder(folderMedical))
                {
                    return RedirectToAction("Index");
                }

           
                return View();
             
        }

       
        public ActionResult DeleteVaccine(int? id, FormCollection collection)
        {


           // ChildVaccine childVaccine = vaccineChildService.GetById(id);

            ViewBag.ChildId = new SelectList(folderMedicalService.getAllChild(), "Id", "Name");

            ViewBag.VaccineId = new SelectList(vaccineChildService.GetAll(), "Id", "Description");



            // listG.Remove(childVaccine);

            folderMedicalService.DeleteVaccineFolder(idF, id);


            FolderMedical folderMedical = folderMedicalService.GetById(idF);

            


            if (folderMedical != null)
            {

                //folderMedical.LisChildVaccines = listG;

                //folderMedicalService.UpdateFolder(folderMedical);

                return View("~/Views/FolderMedical/Edit.cshtml",folderMedical);
            }

            return RedirectToAction("Index");
        }

       
       /*  public ActionResult AddVaccine(int? id, [Bind(Include = "Id,Allergy,Particularity,ChildId,DateC,VaccineId,LisChildVaccines")] FolderMedical f)
        {
            

            System.Diagnostics.Debug.WriteLine(f.VaccineId);
             
            ViewBag.ChildId = new SelectList(folderMedicalService.getAllChild(), "Id", "Name");

            ViewBag.VaccineId = new SelectList(vaccineChildService.GetAll(), "Id", "Description");

             

            folderMedicalService.AddVaccineFolder(idF, id);


            FolderMedical folderMedical = folderMedicalService.GetById(idF);




            if (folderMedical != null)
            {

                 

                return View("~/Views/FolderMedical/Edit.cshtml", folderMedical);
            }

            return RedirectToAction("Index");
        }*/

        // GET: FolderMedical/Delete/5
        public ActionResult Delete(int id)
        {

            FolderMedical folderMedical = folderMedicalService.GetById(id);

            if (folderMedical != null)
            {
                return View(folderMedical);
            }

            return View();
        }

        // POST: FolderMedical/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            
                if (folderMedicalService.DeleteFolder(id))
                {
                    return RedirectToAction("Index");
                }

                
            
                return View();
             
        }
    }
}
