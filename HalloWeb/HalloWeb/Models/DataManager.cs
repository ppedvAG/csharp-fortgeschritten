using System;
using System.Collections.Generic;
using System.Linq;

namespace HalloWeb.Models
{
    public class DataManager
    {
        static List<Eis> db = new List<Eis>();

        static DataManager()
        {
            db.Add(new Eis()
            {
                Id = 1,
                Herstelldatum = DateTime.Now.AddDays(-1),
                Name = "Schoko",
                IsMilcheis = true,
                Preis = 3.4m
            });

            db.Add(new Eis()
            {
                Id = 2,
                Herstelldatum = DateTime.Now.AddDays(-3),
                Name = "Erdbeer",
                IsMilcheis = !true,
                Preis = 2.4m
            });
        }

        public void Add(Eis eis) => db.Add(eis);

        public Eis GetById(int id) => db.FirstOrDefault(x => x.Id == id);

        public IEnumerable<Eis> GetAll() => db;

        public void Delete(Eis eis)
        {
            var killMe = GetById(eis.Id);
            db.Remove(killMe);
        }

        public void Update(Eis eis)
        {
            var old = GetById(eis.Id);
            Delete(eis);
            Add(eis);
        }

    }
}
