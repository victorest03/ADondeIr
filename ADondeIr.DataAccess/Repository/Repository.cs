namespace ADondeIr.DataAccess.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Common.Model;
    using Model;
    using NLog;
    using NPoco;
    using NPoco.Linq;

    public abstract class Repository<TEntity> where TEntity : class
    {
        public static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public virtual List<TEntity> GetAll(Expression<Func<TEntity, bool>> expression = null , Action<IQueryProviderWithIncludes<TEntity>> action = null)
        {
            var result = new List<TEntity>();

            try
            {
                using (IDatabase db = DbContext.GetInstance())
                {
                    #region Select All

                    var query = db.Query<TEntity>();
                    action?.Invoke(query);

                    if (expression != null) query.Where(expression);

                    result = query.ToList();
                    #endregion
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }

            return result;
        }

        public virtual TEntity Get(int id)
        {
            TEntity result = null;

            try
            {
                using (IDatabase db = DbContext.GetInstance())
                {
                    result = db.SingleOrDefaultById<TEntity>(id);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }

            return result;
        }

        public virtual Result Delete(TEntity entity)
        {
            var result = new Result();
            try
            {
                using (IDatabase db = DbContext.GetInstance())
                {
                    if (typeof(TEntity).BaseType == typeof(BaseEntity))
                    {
                        var e = entity as BaseEntity;
                        if (e != null) e.isDeleted = true;
                        db.Update(entity, new[] {"isDeleted", "fFechaEdita", "fkUsuarioEdita"});
                    }
                    else
                    {
                        db.Delete<TEntity>(entity);
                    }
                }

                result.Success = true;
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                result.Message = e.Message;
            }

            return result;
        }

        public virtual Result Insert(TEntity entity)
        {
            var result = new Result();
            try
            {
                using (IDatabase db = DbContext.GetInstance())
                {
                    db.Insert(entity);
                }

                result.Success = true;
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                result.Message = e.Message;
            }

            return result;
        }

        public virtual Result Update(TEntity entity, string[] columns = null)
        {
            var result = new Result();
            try
            {
                using (IDatabase db = DbContext.GetInstance())
                {
                    db.Update(entity, columns);
                }

                result.Success = true;
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                result.Message = e.Message;
            }

            return result;
        }
    }
}
