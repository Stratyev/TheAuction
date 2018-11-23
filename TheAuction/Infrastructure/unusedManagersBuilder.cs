//using TheAuction.Models;
//using Microsoft.AspNet.Identity.Owin;
//using Microsoft.Owin.Security;
//using Microsoft.Owin;

//namespace TheAuction.Infrastructure
//{
//    public class ManagersBuilder
//    {
//        IOwinContext _context;
//        public ManagersBuilder(IOwinContext context)
//        {
//            this._context = context;
//        }

//        public DataManager dManager
//        {
//            get
//            {
//                return new DataManager(_context);
//            }
//        }
//        public AppUserManager userManager
//        {
//            get
//            {
//                return _context.GetUserManager<AppUserManager>();
//            }
//        }
//        private IAuthenticationManager authManager
//        {
//            get
//            {
//                return _context.Authentication;
//            }
//        }



////          ............................Это в контроллере:
//////        ManagersBuilder _mBuilder
//////        {
//////            get
//////            {
//////                return new ManagersBuilder(_context);
//////            }
//////        }
//////    }
//////}