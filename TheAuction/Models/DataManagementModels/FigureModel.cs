using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using TheAuction.Infrastructure;
using CodeFirst;

namespace TheAuction.Models
{
    public class FigureModel
    {
        protected MyDbContext _dbContext;
        public FigureModel(MyDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public List<Figure> getFigures()
        {
            return _dbContext.Figures.ToList();
        }
        public Figure getLastFigure()
        {
            var figure = _dbContext.Figures.ToList().Last();
            return figure;
        }
        public Figure getLastFigure(List<Figure> figures)
        {

            var figure = figures.Last();
            return figure;
        }
        public Figure setFigure(Figure _figure)
        {
            _dbContext.Figures.Add(_figure);
            _dbContext.SaveChanges();
            return _figure;
        }
        public void deleteFigure(Figure _figure)
        {
            _dbContext.Figures.Remove(_figure);
            _dbContext.SaveChanges();
        }
    }
}