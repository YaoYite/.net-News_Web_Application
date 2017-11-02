using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using _5032HD.Interface;
using _5032HD.Models;
using _5032HD.Repositories;

namespace _5032HD.Controllers
{
    public class AngularController : ApiController
    {
        static readonly ArticleInterface repository = new ArticleRepository();

        public IEnumerable<Article> GetAllArticles()
        {
            return repository.GetAll();
        }

        public Article PostArticle(Article item)
        {
            return repository.Add(item);
        }

        public IEnumerable<Article> PutArticle(int id, Article article)
        {
            article.ArticleID= id;
            if (repository.Update(article))
            {
                return repository.GetAll();
            }
            else
            {
                return null;
            }
        }

        public bool DeleteArticle(int id)
        {
            if (repository.Delete(id))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
