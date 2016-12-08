﻿using Scutum.Infra.Context;
using Scutum.Model.Interface;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Scutum.Library.Helper;

namespace Scutum.Data
{
    public abstract class Base<TModel>
        where TModel : class, IEntityWithID
    {
        protected ScutumContext context = new ScutumContext();
        protected DbSet<TModel> dbSet;

        public Base()
        {
            this.dbSet = this.context.Set<TModel>();
        }

        public virtual IQueryable<TModel> Get()
        {
            return this.dbSet;
        }

        public virtual IQueryable<TModel> Get(int id)
        {
            return this.Get().Where(x => x.Id == id);
        }

        public virtual TModel Find(int id)
        {
            return this.Get(id).FirstOrDefault();
        }

        public virtual bool Exists(int id)
        {
            return this.Get(id).Any();
        }

        public virtual int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        public virtual int Save(TModel model)
        {
            this.dbSet.Add(model);
            return this.SaveChanges();
        }

        public virtual int Update(TModel model)
        {
            this.dbSet.Attach(model);
            this.context.Entry(model).State = EntityState.Modified;
            return this.SaveChanges();
        }

        public virtual int Remove(TModel model)
        {
            this.dbSet.Remove(model);
            return this.SaveChanges();
        }
    }
}