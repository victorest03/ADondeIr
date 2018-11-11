namespace ADondeIr.DataAccess.Repository
{
    using Model;
    using NPoco;
    using NPoco.Linq;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public class DbContext
    {
        public static Database GetInstance()
        {
            return new CustomDatabase("DataBaseConnection");
        }
    }

    public class CustomDatabase : Database
    {
        public CustomDatabase(string connectionStringName) : base(connectionStringName)
        {
        }

//        protected override void OnExecutedCommand(DbCommand cmd)
//        {
//#if DEBUG
//            var date = DateTime.Now;
//            //File.WriteAllText("//.txt", FormatCommand(cmd));
//            var path = $"{AppDomain.CurrentDomain.BaseDirectory}SqlLogs\\Sql_{date.Year}-{date.Month:00}-{date.Day:00}.txt";

//            if (!File.Exists(path))
//            {
//                File.Create(path).Dispose();
//                using (var tw = File.AppendText(path))
//                {
//                    tw.WriteLine($"[SQL] {date.TimeOfDay.ToString()}:");
//                    tw.WriteLine(FormatCommand(cmd));
//                    tw.WriteLine("--------------------------------------------------");
//                    tw.Close();
//                }

//            }

//            else if (File.Exists(path))
//            {
//                using (var tw = File.AppendText(path))
//                {
//                    tw.WriteLine($"[SQL] {date.TimeOfDay.ToString()}:");
//                    tw.WriteLine(FormatCommand(cmd));
//                    tw.WriteLine("--------------------------------------------------");
//                    tw.Close();
//                }
//            }
//#endif


//            base.OnExecutedCommand(cmd);
//        }

        protected override bool OnInserting(InsertContext insertContext)
        {
            if (insertContext.Poco is BaseEntity entity)
            {
                entity.fFechaCrea = DateTime.UtcNow;
            }
            return base.OnInserting(insertContext);
        }

        protected override bool OnUpdating(UpdateContext updateContext)
        {
            if (updateContext.Poco is BaseEntity entity)
            {
                entity.fFechaEdita = DateTime.UtcNow;
            }
            return base.OnUpdating(updateContext);
        }

    }

    public static class ExtensionsNPoco
    {
        public static IQueryProvider<T> OrderByField<T>(this IQueryProvider<T> q, string sortField, bool @ascending)
        {
            var param = Expression.Parameter(typeof(T), "p");
            var prop = Expression.Property(param, sortField);
            var methodName = @ascending ? "OrderBy" : "OrderByDescending";
            Expression conversion = Expression.Convert(prop, typeof(object));
            var lambda = Expression.Lambda(conversion, param);
            var result = typeof(IQueryProvider<T>).GetMethods().Single(
                    method => method.Name == methodName)
                .Invoke(q, new object[] { lambda });
            return (IQueryProvider<T>)result;
        }
        
    }
}
