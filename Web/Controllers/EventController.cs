﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using Service;
using PagedList;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Web.Controllers
{
    public class EventController : Controller
    {
        EventService eventService ;
        CategoryService categoryService;
        public EventController()
        {
            String token = (String)System.Web.HttpContext.Current.Session["AccessToken"];

            User usergarten = (User)System.Web.HttpContext.Current.Session["User"];

            categoryService = new CategoryService(token);
            eventService = new EventService(token);
        }
        // GET: Event


        public ActionResult Index(string price,int? Page_No)
        {
            
            IEnumerable<Event> listevents = eventService.GetAll();

            int size_of_page = 4;
            int No_Of_Page = (Page_No ?? 1);

            
            if (String.IsNullOrEmpty(price))
            {
                return View(listevents.ToPagedList(No_Of_Page, size_of_page));
            }
            return View(eventService.getAllEventbyprice(price).ToPagedList(No_Of_Page, size_of_page));
        }

        public ActionResult IndexFront(string price)
        {
            if (String.IsNullOrEmpty(price))
            {
                return View(eventService.GetAllEvent());
            }
            return View();
        }


        [HttpGet]
        public ActionResult GetEstimateByEvent(int id)
        {
        
            {
                var lists = eventService.GetEstimateByEvent(id);

                ViewBag.List = lists;

                return View();
            }

        }

        public ActionResult EventForChild()
        {
            int idChild = 1;
                return View(eventService.getEventForChild(idChild));
            
           
        }

        public ActionResult GetEventToday()
        {
          
            return View(eventService.GetEventToday());


        }

        // GET: Event/Details/5
        public ActionResult Details(int id)
        {
            Event Event = eventService.getEventById(id);
            if (Event != null)
            {
                return View(Event);
            }
            return View();
        }

        // GET: Event/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(categoryService.GetAll(), "Id", "Description");
            return View();
        }

        // POST: Event/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Description,Date,Price,Object,CategoryId")] Event e)
        {
            if (ModelState.IsValid)
            {
                if (eventService.Add(e))
                {
                    return RedirectToAction("Index");
                }
            }
            return View();

        }

        // GET: Event/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.CategoryId = new SelectList(categoryService.GetAll(), "Id", "Description");
            Event e = eventService.getEventById(id);
            return View(e);
        }

        public ActionResult Sms()
        {
            return View();
        }

            [HttpPost]
        public ActionResult Sms([Bind(Include = " ")] Event e, int id_event)
        {
                if (eventService.smsSubmit(e,id_event))
                {
                    return RedirectToAction("Index");
                }

            return View();

        }



        // POST: Event/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, [Bind(Include = "Description,Date,Price,Object,CategoryId")] Event e)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (eventService.Update(id, e))
                    {
                        return RedirectToAction("Index");
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Event/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Event/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if (eventService.deleteEventById(id))
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Participate(int id, [Bind(Include = "NParticipate")] Event e)
        {

            e.Id = id;
            if (eventService.AddParticipate(id, e)) 
            {

                return RedirectToAction("IndexFront");
            }
            return View();
        }
  


    }
}


