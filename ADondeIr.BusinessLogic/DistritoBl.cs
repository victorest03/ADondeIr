namespace ADondeIr.BusinessLogic
{
    using System.Collections.Generic;
    using DataAccess;
    using Model;

    public class DistritoBl
    {
        private readonly DistritoDa _da = new DistritoDa();
        public List<Distrito> GetAll()
        {
            return _da.GetAll();
        }
    }
}
