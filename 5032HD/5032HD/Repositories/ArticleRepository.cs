using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _5032HD.Interface;
using _5032HD.Models;

namespace _5032HD.Repositories
{
    public class ArticleRepository : ArticleInterface
    {
        private Entities db = new Entities();

        IEnumerable<Article> ArticleInterface.GetAll()
        {
            //throw new NotImplementedException();
            return db.Articles.ToList();
        }

        Article ArticleInterface.Get(int id)
        {
            //throw new NotImplementedException();
            return db.Articles.Find(id);
        }


        Article ArticleInterface.Add(Models.Article item)
        {
            //throw new NotImplementedException();
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            // TO DO : Code to save record into database
            db.Articles.Add(item);
            db.SaveChanges();
            return item;
        }

        bool ArticleInterface.Update(Article item)
        {
            //throw new NotImplementedException();
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            // TO DO : Code to update record into database
            var articles = db.Articles.Single(a => a.ArticleID == item.ArticleID);
            articles.title = item.title;
            articles.datePub = item.datePub;
            articles.description = item.description;
            articles.comments = articles.comments;
            articles.topic = articles.topic;
            db.SaveChanges();

            return true;
        }

        bool ArticleInterface.Delete(int id)
        {
            //throw new NotImplementedException();
            Article articles = db.Articles.Find(id);
            db.Articles.Remove(articles);
            db.SaveChanges();
            return true;
        }
    }
}
