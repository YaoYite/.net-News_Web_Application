using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using _5032HD.Models;

namespace _5032HD.Interface
{
    interface ArticleInterface
    {
        IEnumerable<Article> GetAll();
        Article Get(int id);
        Article Add(Article item);
        bool Update(Article item);
        bool Delete(int id);
    }
}