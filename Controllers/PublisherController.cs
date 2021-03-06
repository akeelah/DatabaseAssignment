﻿using BookShopInventoryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using BookInventoryApp.DAL;

namespace BookInventoryApp.Controllers
{
    public class PublisherController : Controller
    {
        private NewOperation context;
        public PublisherController()
        {
            context = new NewOperation();
        }

        public ActionResult Index()
        {
            IList<PublisherModel> publisherList = new List<PublisherModel>();
            var query = from publisher in context.Publishers
                        select publisher;
            //7query = context.
            var publishers = query.ToList();
            foreach (var publisherData in publishers)
            {
                publisherList.Add(new PublisherModel()
                {
                    Id = publisherData.Id,
                    Name = publisherData.Name,
                    Address = publisherData.Address,
                    PhoneNumber = publisherData.PhoneNumber
                  // Year = Convert.ToInt32(publisherData.Year)
                });
            }
            return View(publisherList);
        }

        public ActionResult Create()
        {
            PublisherModel model = new PublisherModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(PublisherModel model)
        {
            try
            {
                Publisher publisher = new Publisher()
                {
                    Name = model.Name,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber
                    //Year = model.Year.ToString()
                };
                context.Publishers.InsertOnSubmit(publisher);
                context.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }
        public ActionResult Details(int id)
        {
            PublisherModel model = context.Publishers.Where(x => x.Id == id).Select(x =>
                                                new PublisherModel()
                                                {
                                                    Id = x.Id,
                                                    Name = x.Name,
                                                    Address = x.Address,
                                                    PhoneNumber = x.PhoneNumber
                                                }).SingleOrDefault();

            return View(model);
        }
        public ActionResult Edit(int id)
        {
            PublisherModel model = context.Publishers.Where(x => x.Id == id).Select(x =>
                                new PublisherModel()
                                {
                                    Id = x.Id,
                                    Name = x.Name,
                                    Address = x.Address,
                                    PhoneNumber = x.PhoneNumber
                                }).SingleOrDefault();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(PublisherModel model)
        {
            try
            {

                Publisher publisher = context.Publishers.Where(x => x.Id == model.Id).Single<Publisher>();
                publisher.Name = model.Name;
                publisher.Address = model.Address;
                publisher.PhoneNumber = model.PhoneNumber;
                context.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }
        public ActionResult Delete(int id)
        {
            PublisherModel model = context.Publishers.Where(x => x.Id == id).Select(x =>
                                new PublisherModel()
                                {
                                    Id = x.Id,
                                    Name = x.Name,
                                    Address = x.Address,
                                    PhoneNumber = x.PhoneNumber
                                }).SingleOrDefault();
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(PublisherModel model)
        {
            try
            {

                Publisher publisher = context.Publishers.Where(x => x.Id == model.Id).Single<Publisher>();
                context.Publishers.DeleteOnSubmit(publisher);
                context.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }
    }
}
