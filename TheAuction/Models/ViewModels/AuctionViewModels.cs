using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CodeFirst;

namespace TheAuction.Models
{
    public class AddLotModel
    {
        [Required]
        [Display(Name = "Название")]
        [DataType(DataType.Text)]
        public string Name { get; set; }


        [Required]
        [Display(Name = "Описание")]
        [DataType(DataType.MultilineText)]
        [Remote("CheckDescription", "Home", ErrorMessage = "Плохое описание")]
        public string Description { get; set; }


        [Range(1, 999999, ErrorMessage = "Недопустимая начальная ставка")]
        [Required]
        public double Start_bet { get; set; }

        [Required]
        [Display(Name = "Категория")]
        public string CategoryName { get; set; }

        [Required]
        public string PictureUrl { get; set; }


    }
    public class AddBetModel
    {
        [Display(Name = "Минимальный размер ставки")]
        [Required]
        public double Amount { get; set; }
    }
    public class SellerLot
    {
        [Required]
        public List<Lot> Lots { get; set; }

        [Required]
        public Seller Seller { get; set; }
    }
    public class CallingMethodName_Lot
    {
        [Required]
        public Lot Lot { get; set; }

        [Required]
        public string CallingMethodName { get; set; }
    }
    public class TestModel
    {
        public int id { get; set; }
        public string Name { get; set; }

    }
    public class UploadingResult
    {
        public string Error { get; set; }
        public List<string> Files { get; set; }
    }

}