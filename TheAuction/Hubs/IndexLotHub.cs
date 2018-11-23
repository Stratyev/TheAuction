using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using CodeFirst;
using Microsoft.AspNet.Identity;
using TheAuction.Infrastructure;
using Microsoft.Owin;
using Microsoft.AspNet.Identity.Owin;
using TheAuction.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using TheAuction.Models.HubModels;


namespace TheAuction.Hubs
{
    public class IndexLotHub : Hub
    {
        MyDbContext Mdb { get { return MyDbContext.Create(); } }
        HubAppUserManager UserM { get{ return HubAppUserManager.Create(Mdb); } }

        static List<HubUser> Users = new List<HubUser>();

        // Отправка сообщений
        public void Send()
        {
            Clients.All.addMessage();
        }

        // Подключение нового пользователя
        //public void Connect()
        //{
        //    var id = Context.ConnectionId;

        //    if (!Users.Any(x => x.ConnectionId == id))
        //    {
        //        Users.Add(new HubUser { ConnectionId = id});

        //        // Посылаем сообщение текущему пользователю
        //        Clients.Caller.onConnected(id, Users);

        //        // Посылаем сообщение всем пользователям, кроме текущего
        //        Clients.AllExcept(id).onNewUserConnected(id);
        //    }
        //}

        // Отключение пользователя
        //public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        //{
        //    var item = Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
        //    if (item != null)
        //    {
        //        Users.Remove(item);
        //        var id = Context.ConnectionId;
        //        Clients.All.onUserDisconnected(id);
        //    }

        //    return base.OnDisconnected(stopCalled);
        //}
    }
}