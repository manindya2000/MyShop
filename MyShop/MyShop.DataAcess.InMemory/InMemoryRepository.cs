﻿using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;


namespace MyShop.DataAcess.InMemory
{
    public class InMemoryRepository<T> : IRepository<T> where T: BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;
        List<T> items;
        string classname;
        public InMemoryRepository()
        {
            classname = typeof(T).Name;
            items = cache[classname] as List<T>;
            if(items==null)
            {
                items = new List<T>();
            }
        }
        public void Commit()
        {
            cache[classname] = items;
        }

        public void Insert(T t)
        {
            items.Add(t);
        }

        public void Update(T t)
        {
            T tToUpdate = items.Find(i=>i.Id == t.Id);
            if (tToUpdate != null)
            {
                tToUpdate = t;
            }
            else
            {
                throw new Exception(classname+" Not found!!!");
            }
        }

        public T Find(String Id)
        {
            T tToFind = items.Find(t => t.Id == Id);
            if (tToFind != null)
            {
                return (tToFind);
            }
            else
            {
                throw new Exception( classname+" Not Found!!!");
            }
        }

        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }

        public void Delete(String Id)
        {
            T tToDelete = items.Find(t => t.Id == Id);
            if (tToDelete != null)
            {
                items.Remove(tToDelete);
            }
            else
            {
                throw new Exception(classname+" Not Found!!!");
            }
        }

    }
}
