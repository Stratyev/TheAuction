using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodeFirst
{
    public class Figure
    {
        [Key]
        public int Figure_id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public Location Location { get; set; }
        public AppUser AppUser { get; set; }
        public string PictureUrl { get; set; }

    }
    public class Customer
    {
        [Key]
        public int Customer_id { get; set; }
        public Figure Figure { get; set; }
        public virtual List<Bet> Bets { get; set; }
        public virtual List<Favorite> Favorites { get; set; }
        public virtual List<Lot> Lots { get; set; }
    }
    public class Seller
    {
        [Key]
        public int Seller_id { get; set; }
        public double Rating { get; set; }
        public Figure Figure { get; set; }
        public virtual List<Lot> Lots { get; set; }
    }
    public class Lot
    {
        [Key]
        public int Lot_id { get; set; }
        public string Name { get; set; }
        public LotGroup LotGroup { get; set; }
        public Category Category { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double Start_bet { get; set; }
        public double Min_bet { get; set; }
        public int Bets_count { get; set; }
        public Location Location { get; set; }
        public DateTime Auction_start { get; set; }
        public DateTime Auction_end { get; set; }
        //public bool IsExpired
        //{
        //    get
        //    {
        //        if (Auction_end >= DateTime.Now)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    private set
        //    {

        //    }
        //}
        public Condition Condition { get; set; }
        public Seller Seller { get; set; }
        public Customer Winner { get; set; }
        public virtual List<Bet> Bets { get; set; }
        public virtual List<Check> Checks { get; set; }
        public virtual List<Favorite> Favorites { get; set; }
        public string PictureUrl { get; set; }
        //public virtual List<LotPicture> LotPictures { get; set; }
    }
    public class LotGroup
    {
        [Key]
        public int LotGroup_id { get; set; }
        public int LotRangeStart { get; set; }
        public int LotRangeEnd { get; set; }
        public virtual List<Lot> Lots { get; set; }
    }
    public class Category
    {
        [Key]
        public int Category_id { get; set; }
        public string Name { get; set; }
        public virtual List<Lot> Lots { get; set; }
    }
    public class Location
    {
        [Key]
        public int Location_id { get; set; }
        public string Name { get; set; }
        public virtual List<Lot> Lots { get; set; }
        public virtual List<Figure> Figures { get; set; }
    }
    public class Bet
    {
        [Key]
        public int Bet_id { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public Lot Lot { get; set; }
        public Customer Customer { get; set; }
        public virtual List<Check> Checks { get; set; } //Ну вдруг не прошла оплата, придется еще раз
    }
    public class Favorite
    {
        [Key]
        public int Favorite_id { get; set; }
        public Customer Customer { get; set; }
        public Lot Lot { get; set; }
    }
    public class Check
    {
        [Key]
        public int Check_id { get; set; }
        public double Cost { get; set; } //Lot Price + Shipment Cost
        public Condition Condition { get; set; }
        public Lot Lot { get; set; }
        public Bet Bet { get; set; }
        public DateTime ShipmentLastDate { get; set; }
        public ShipmentOption ShipmentOption { get; set; }
    }

    public class ShipmentOption //При создании доставки будет производиться проверка на предмет существования текущего варианта (составленного на основе Location продавца и покупателя). Если не сущ., создается, если сущ., то производится поиск по имени.
    {
        [Key]
        public int ShipmentOption_id { get; set; }
        public string Name { get; set; }
        public Location Source { get; set; }
        public Location Destination { get; set; }
        public double Cost { get; set; }
        public virtual List<Check> Checks { get; set; }
    }
    public class Shipment
    {
        [Key]
        public int Shipment_id { get; set; }
        public string Track { get; set; }
        public Check Check { get; set; }
        public Condition Condition { get; set; }
        public virtual List<ShipmentDeliveringCondition> ShipmentDeliveringCondition { get; set; }
    }
    public class DeliveringCondition //На каком пункте находится посылка и т.д (пока не используется).
    {
        [Key]
        public int DeliveringCondition_id { get; set; }
        public string Name { get; set; }
        public virtual List<ShipmentDeliveringCondition> ShipmentDeliveringCondition { get; set; }
    }
    public class ShipmentDeliveringCondition
    {
        [Key]
        public int ShipmentDeliveringCondition_id { get; set; }
        public Shipment Shipment { get; set; }
        public DeliveringCondition DeliveryCondition { get; set; }
    }
    public class Condition //имя Status не подходит, т.к. это ключевое слово MS SQL Server
    {
        [Key]
        public int Condition_id { get; set; } 
        public string Name { get; set; } //Разработай систему имен статусов для разных таблиц, плес
        public virtual List<Check> Checks { get; set; }
        public virtual List<Lot> Lots { get; set; }
        public virtual List<Shipment> Shipments { get; set; }
    }
    public class Setting
    {
        [Key]
        public int Setting_id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
    //public class Trade
    //{
    //    [Key]
    //    public int Trade_id { get; set; }
    //    public DateTime Close_date { get; set; }
    //    public string Condition { get; set; }
    //    public Check Check { get; set; }
    //    public Shipment Shipment { get; set; }
    //    public virtual List<Dispute> Disputes { get; set; }
    //}
    //public class LotPicture
    //{
    //    [Key]
    //    public int LotPicture_id { get; set; }
    //    public string Name { get; set; } // название картинки
    //    public byte[] Image { get; set; }
    //    public virtual List<Lot> Lots { get; set; }
    //}
    //public class FigurePicture
    //{
    //    [Key]
    //    public int UserPicture_id { get; set; }
    //    public string Name { get; set; } // название картинки
    //    public byte[] Image { get; set; }
    //    public virtual List<Figure> Figures { get; set; }
    //}

    //public class Dispute
    //{
    //    [Key]
    //    public int Dispute_id { get; set; }
    //    public string Reason { get; set; }
    //    public string Condition { get; set; }
    //    public Trade Trade { get; set; }
    //}
    //public class Refund
    //{
    //    [Key]
    //    public int Refund_id { get; set; }
    //    public Dispute Dispute { get; set; }
    //}
    //public class Return
    //{
    //    [Key]
    //    public int Return_id { get; set; }
    //    public Dispute Dispute { get; set; }
    //    public Shipment Shipment { get; set; }
    //}
    /*
     Лоты: "active", "payment_expected (использоваться пока не будет)", "shipment_expected", "unclaimed", "shipped (использоваться пока не будет)", "delivered"
     Чеки: "complete", "incomplete (использоваться пока не будет)" 
     Отправки: "shipped (использоваться пока не будет)", "delivered"
     */

