using SensiveProject.BusinessLayer.Abstract;
using SensiveProject.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensiveProject.BusinessLayer.Concrete
{
	public class AppUserManager : IAppUserService
	{
		// dal yok
		public void TDelete(int id)
		{
			throw new NotImplementedException();
		}

		public List<AppUser> TGetAll()
		{
			throw new NotImplementedException();
		}

		public AppUser TGetById(int id)
		{
			throw new NotImplementedException();
		}

		public void TInsert(AppUser entity)
		{
			throw new NotImplementedException();
		}

		public void TUpdate(AppUser entity)
		{
			throw new NotImplementedException();
		}
	}
}
